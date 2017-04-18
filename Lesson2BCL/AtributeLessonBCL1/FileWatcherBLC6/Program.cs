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
			item.Init();
		}
	}

	public class Watcher
	{
		public void Init()
		{
			#region configuration part
			FileWatcherConfigSection fwConfig = (FileWatcherConfigSection)ConfigurationManager.GetSection("fileWatcher");
			string confCulture = fwConfig.Culture;
			var collectionOfExt = fwConfig.TargetFilesExtensions;
			List<string> extensions = collectionOfExt.GetValuesAsArray();
			string outputRegexpInfoFormat = fwConfig.FoundedInfoFormat.Value;
			string fileInfoFormat = fwConfig.FileInfoFormat.Value;
			var backupPath = fwConfig.BackupInfo.Path;
			bool isSaveConfirmationAvailable = fwConfig.BackupInfo.SaveConfirmation;
			// Путь к папке с файлами 
			string path = Path.GetFullPath(@"D:\txt");
			// регексп для поиска русских слов и букв
			Regex regex = new Regex("[а-яА-Я]"); // с + в конце будет искать целые слова, без - по буквам
			#endregion

			
			#region Setup backup directory
			var backupDirectory = Path.GetDirectoryName(backupPath);
			if (backupDirectory!=null && !Directory.Exists(backupDirectory))
			{
				Directory.CreateDirectory(backupDirectory);
			}
			#endregion

			//Setup the culture
			Thread.CurrentThread.CurrentCulture = new CultureInfo(confCulture);

			
			Console.ForegroundColor = ConsoleColor.DarkMagenta;
			Console.WriteLine("Start to finding the items in the {0} folder....", path);
			Console.ForegroundColor = ConsoleColor.Green;

			var directory = new DirectoryInfo(path);
			var files = directory.EnumerateFiles("*.*", SearchOption.TopDirectoryOnly).Where(x => extensions.Contains(x.Extension));
			foreach (FileInfo file in files)
			{
				#region Read and Replace RU with Eng characters
				Console.ForegroundColor = ConsoleColor.DarkYellow;
				Console.WriteLine(fileInfoFormat, file.Name, file.Extension, file != null ? file.Length.ToString() : "");
				Console.ForegroundColor = ConsoleColor.Green;
				// открываем файл для чтения
				var reader = file.OpenText();
				var lineNumber = 1; // счетчик для считанных линий строк в файле
				var fullLineBuilder = new StringBuilder(); // билдер, который считывает все содержимое файла + изменения
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
							line[match.Index] = ReplaceCharacter(match.Value[0]);
						}
						match = match.NextMatch();
					}
					lineNumber++;
					fullLineBuilder.AppendLine(line.ToString());
				}
				reader.Close(); // не забываем закрыть т.к. после не будет доступа к файлу из других программ
				// Вывод результата замены букв на экран 
				Console.ForegroundColor = ConsoleColor.Cyan;
				Console.WriteLine("##");
				Console.WriteLine("Readed: {0} lines", lineNumber-1);
				Console.WriteLine("##End of File##");
				Console.ForegroundColor = ConsoleColor.Gray;
				Console.WriteLine("Result after all changes is : ");
				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.WriteLine(fullLineBuilder);
				Console.ForegroundColor = ConsoleColor.Green;
				#endregion

				#region SaveAndBackup
				if (!isSaveConfirmationAvailable)
				{
					CopyToBackupDirectory(backupDirectory, file); // копируем файл в бэкап директорию
					// если конфигурация отключена (флаг в false) мы просто сохраняем без вопроса к пользователю
					UpdateCurrentFileContent(file.FullName, fullLineBuilder);
				}
				else
				{
					Console.WriteLine("Do you wanna replace your file content with new one above?");
					var answ = Console.ReadKey();

					while (true)
					{
						if (answ.Key == ConsoleKey.N)
						{
							break;
						}
						if (answ.Key == ConsoleKey.Y)
						{
							CopyToBackupDirectory(backupDirectory, file); // копируем файл в бэкап
							// создаем новый файл и подменяем существующий - ну и наполняем его новыми данными
							UpdateCurrentFileContent(file.FullName, fullLineBuilder);

							break;
						}
						Console.WriteLine("Try again (Hint: use Y or N)");
						answ = Console.ReadKey();
					}
				}
				#endregion
				Console.WriteLine(Environment.NewLine+"Done");
				// Move to next file if available
			}

			Console.ReadKey();
		}

		/// <summary>
		/// Метод, который скопирует содержимое файла в директорию для бэкапа
		/// </summary>
		/// <param name="backupDirectory"></param>
		/// <param name="file"></param>
		public void CopyToBackupDirectory(string backupDirectory, FileInfo file)
		{
			var backupFilePath = backupDirectory + "\\" + file.Name;
			file.CopyTo(Path.GetFullPath(backupFilePath), true); // копируем файл в бэкап
		}


		/// <summary>
		/// Cоздаем новый файл или подменяем существующий - ну и наполняем его новыми данными
		/// </summary>
		/// <param name="fullPath">Полный путь к файлу</param>
		/// <param name="content">Данные, которые будут записаны</param>
		public void UpdateCurrentFileContent(string fullPath, StringBuilder content)
		{
			
			var file2 = new FileInfo(fullPath);
			var writer = file2.CreateText();
			writer.WriteLine(content.ToString());
			writer.Close();
		}

		/// <summary>
		/// Заменяем символ на аналог английской буквы если существует. Сохраняет регистр буквы.
		/// </summary>
		/// <param name="s">Ожидается строка с одним символом</param>
		/// <returns></returns>
		public char ReplaceCharacter(char s)
		{
			var v = char.GetUnicodeCategory(s);
			var ctmp = Replacer(char.ToLower(s));
			return char.GetUnicodeCategory(ctmp) != v ? char.ToUpper(ctmp) : ctmp;
		}

		/// <summary>
		/// Подменяет (частично) русские буквы на английские.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
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
