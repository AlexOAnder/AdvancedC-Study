using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace HttpGrabLibrary
{
	public static class Grabber
	{
		private static readonly ConsoleWriterInterface writer = new ConsoleWriter();

		public static async Task StartToDownloadAsync(
			string url,
			int analysisnLevel = 1,
			DomainRestriction domainRestriction = DomainRestriction.InCurrentURLOnly,
			string storage = "C:/tmp",
			bool showStatusInConsole = false)
		{
			// set a status one time for all code
			writer.SetStatus(showStatusInConsole);
			writer.Write(string.Format("\nAnalyze on level {0}", analysisnLevel));

			string result;
			Uri uri;
			try
			{
				uri = new Uri(url);
			}
			catch (Exception e)
			{
				writer.Write(string.Format("->Error occured in path ({0}).", url));
				writer.Write(string.Format("->{0}", e.Message));
				return;
			}

			try
			{
				writer.Write(string.Format("Starting to analyze the {0}", uri));
				result = await GetDataAsync(uri);
			}
			catch (Exception e)
			{
				writer.Write(string.Format("->Some problems while loading from {0} are occured.", uri));
				writer.Write(string.Format("->{0}", e.Message));
				return;
			}

			#region saving in files

			writer.Write(string.Format("Create new directory: {0}", storage));
			storage = CreateFolder(storage, uri.Host);

			var filePath = CreateAndFillFile(uri, storage, result);

			writer.Write(string.Format("Saved to {0}", filePath));

			#endregion
			// if we choose to analyze only 1st level -then break the programm
			if (analysisnLevel <= 0)
				return;

			var nodes = GetHtmlNodes(result);
			if (nodes == null)
				return;
			var currentHostwithSheme = uri.Scheme + "://" + uri.Host;
			var currentPageSlash = uri.ToString().LastIndexOf("/", StringComparison.Ordinal);
			var currPage = uri.ToString().Substring(0,currentPageSlash);
			foreach (var node in nodes)
			{
				// get links from nodes
				var reference = node.GetAttributeValue("href", string.Empty);
				Uri newUri;
				try
				{
					if (reference.StartsWith("/"))
						reference = currentHostwithSheme + reference;
					else if (reference.EndsWith(".shtml"))
					{
						reference = currPage + "/" + reference;
					}
					newUri = new Uri(reference);
				}
				catch (Exception e)
				{
					// than we dont care about that - just ignore that and go ahead
					// return;
					continue;
				}

				if ((domainRestriction == DomainRestriction.InCurrentURLOnly) && (newUri.Host != uri.Host))
				{
					writer.Write(string.Format("The url isn't in initial url: {0}", reference));
					// go ahead
					continue;
				}

				if (reference != string.Empty && reference != currentHostwithSheme+"/")
					await StartToDownloadAsync(reference, analysisnLevel - 1, domainRestriction, storage, showStatusInConsole);
			}
		}

		private static HtmlNode[] GetHtmlNodes(string result)
		{
			var doc = new HtmlDocument();
			doc.LoadHtml(result);
			var nodes = doc.DocumentNode.SelectNodes("//a") != null ? doc.DocumentNode.SelectNodes("//a").ToArray() : null;
			return nodes;
		}

		private static string CreateAndFillFile(Uri uri, string folderPath, string result)
		{
			//try to understand which name we should give to a new file. 
			var name = (uri.AbsolutePath == Path.DirectorySeparatorChar.ToString()) ||
			           (uri.AbsolutePath == Path.AltDirectorySeparatorChar.ToString())
				? "main"
				: uri.AbsolutePath;

			var fileName = name + "_1.html";
			//removing separators from pathname because they not allowed in filename
			fileName = fileName.Replace(Path.AltDirectorySeparatorChar.ToString(), string.Empty); 
			fileName = fileName.Replace(Path.DirectorySeparatorChar.ToString(), string.Empty);
			var filePath = string.Format(@"{0}{1}{2}", folderPath, Path.DirectorySeparatorChar, fileName);


			File.Create(filePath).Close();

			var encoding = Encoding.Unicode;
			try
			{
				using (var streamWriter = new StreamWriter(File.Open(filePath, FileMode.OpenOrCreate), encoding))
				{
					streamWriter.Write(result, false, encoding);
				}
			}
			catch (Exception e)
			{
				writer.Write(e.Message);
			}

			return filePath;
		}

		private static string CreateFolder(string folderPath, string Host)
		{
			if (!folderPath.Contains(Host))
				folderPath += Path.DirectorySeparatorChar + Host;

			if (!Directory.Exists(folderPath))
				Directory.CreateDirectory(folderPath);
			return folderPath;
		}

		private static async Task<string> GetDataAsync(Uri uri)
		{
			string result;
			using (var client = new HttpClient())
			{
				if ((uri.Scheme != "http") && (uri.Scheme != "https"))
					throw new ArgumentException(string.Format("Not HTTP or HTTPS schema. (Actualy it's {0})", uri.Scheme));
				result = await client.GetStringAsync(uri);
			}

			return result;
		}

	}
}