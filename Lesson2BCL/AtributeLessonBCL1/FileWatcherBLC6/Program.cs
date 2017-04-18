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
using System.Text.RegularExpressions;

namespace FileWatcherBLC6
{
	class Program
	{
		public static void Main(string[] args)
		{
			var item = new Watcher();
			item.MainAccess();
		}
	}

	public class Watcher
	{
		private FileSystemWatcher _watcher;

		public void MainAccess()
		{
			#region configuration part
			FileWatcherConfigSection fwConfig = (FileWatcherConfigSection)ConfigurationManager.GetSection("fileWatcher");
			string confCulture = fwConfig.Culture;
			var collectionOfExt = fwConfig.TargetFilesExtensions;
			List<string> extensions = collectionOfExt.GetValuesAsArray();
			string outputRegexpInfoFormat = fwConfig.FoundedInfoFormat.Value;
			string fileInfoFormat = fwConfig.FileInfoFormat.Value;
			var backupPath = fwConfig.BackupInfo.Path;
			string isSaveConfirmationAvailable = fwConfig.BackupInfo.SaveConfirmation;
			string path = Path.GetFullPath(@"D:\txt");
			#endregion

			var backupDirectory = Path.GetDirectoryName(backupPath);
			if (!Directory.Exists(backupDirectory))
			{
				Directory.CreateDirectory(backupDirectory);
			}
			// поиск русских слов и букв
			Regex regex = new Regex("[а-яА-Я]"); // с + в конце будет искать целые слова, без - по буквам

			Thread.CurrentThread.CurrentCulture = new CultureInfo(confCulture);

			var directory = new DirectoryInfo(path);
			Console.ForegroundColor = ConsoleColor.DarkMagenta;
			Console.WriteLine("Start to finding the items in the {0} folder....", path);
			Console.ForegroundColor = ConsoleColor.Green;

			var files =
				directory.EnumerateFiles("*.*", SearchOption.TopDirectoryOnly).Where(x => extensions.Contains(x.Extension));
			foreach (FileInfo file in files)
			{
				Console.ForegroundColor = ConsoleColor.DarkYellow;
				Console.WriteLine(fileInfoFormat, file.Name, file.Extension, file != null ? file.Length.ToString() : "");
				Console.ForegroundColor = ConsoleColor.Green;
				// открываем файл для чтения
				var reader = file.OpenText();
				var lineNumber = 1; // счетчик для считанных линий строк в файле
				var fullLineBuilder = new StringBuilder(); // билдер, который считывает все содержимое файла + изменения
				var rusFlag = false;
				while (!reader.EndOfStream)
				{
					// считываем пока не достигнем конца потока
					var line = new StringBuilder(reader.ReadLine());
					
					Console.WriteLine(line);
					// Получаем совпадения в экземпляре класса Match
					Match match = regex.Match(line.ToString());
					
					while (match.Success)
					{
						Console.WriteLine(outputRegexpInfoFormat, lineNumber, match.Index, match.Value);  // Ln: {line} Col: {col} Text: {text}
						if (match.Value.Length == 1)
						{
							line[match.Index] = ReplaceCharacter(match.Value);
							rusFlag = true;
						}
						match = match.NextMatch();
					}
					lineNumber++;
					fullLineBuilder.AppendLine(line.ToString());
					if (rusFlag)
					{
						rusFlag = false;
						Console.ForegroundColor = ConsoleColor.Yellow;
						Console.WriteLine("#### After changes we have");
						Console.WriteLine(fullLineBuilder);
						Console.ForegroundColor = ConsoleColor.Green;
					}
				}
				reader.Close(); // не забываем закрыть т.к. после не будет доступа к файлу из других программ
				Console.ForegroundColor = ConsoleColor.Cyan;
				Console.WriteLine("####");
				Console.WriteLine("Readed: {0} lines", lineNumber-1);
				Console.WriteLine("####		##End of File##");
				Console.WriteLine();
				Console.ForegroundColor = ConsoleColor.Green;
			}


			Console.ReadKey();
		}

		public char ReplaceCharacter(string s)
		{
			var v = Char.GetUnicodeCategory(s,0);
			var ctmp = Replacer(s.ToLower()[0]);
			if (Char.GetUnicodeCategory(ctmp) != v) // если символ был в верхнем регистре - возвращаем его
			{
				return Char.ToUpper(ctmp);
			}
			return ctmp;
		}
		public char Replacer(char value)
		{
			switch (value)
			{
				case 'б': return 'b';
				case 'м': return 'm';
				case 'и': return 'i';
				case 'г': return 'g';
				case 'в': return 'v';
				case 'л': return 'l';
				case 'т': return 't';
				case 'р': return 'r';
				case 'д': return 'd';
				case 'ж': return 'j';
				default:
					return value;
			}
		}

	}

}
