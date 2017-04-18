using System;
using System.Collections;
using System.Text;
using System.Configuration;
using System.Xml;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using System.Globalization;
using System.Linq;

namespace FileWatcherBLC6
{
	static class Program
	{
		

		static void Main(string[] args)
		{
			#region configuration part
			FileWatcherConfigSection s2s = (FileWatcherConfigSection)ConfigurationManager.GetSection("fileWatcher");
			string confCulture = s2s.Culture;
			var collectionOfExt = s2s.TargetFilesExtensions;
			List<string> extensions = collectionOfExt.GetValuesAsArray();
			string outputFileInfoFormat = s2s.FoundedInfoFormat.Value;
			string availableFileFormat = s2s.FileInfoFormat.Value;
			var backupPath = s2s.BackupInfo.Path;
			string isSaveConfirmationAvailable = s2s.BackupInfo.SaveConfirmation;
			string path = @"D:/txt";
			#endregion


			Thread.CurrentThread.CurrentCulture = new CultureInfo(confCulture);

			var directory = new DirectoryInfo(path);

			
			Console.WriteLine("Start to finding the items in the {0} folder....", path);

			var files =
				directory.EnumerateFiles("*.*", SearchOption.TopDirectoryOnly).Where(x => extensions.Contains(x.Extension));
			foreach (FileInfo file in files)
			{
				Console.WriteLine(availableFileFormat, file.Name, file.Extension, file != null ? file.Length.ToString() : "");
			}
			Console.ReadKey();
		}

	}



}
