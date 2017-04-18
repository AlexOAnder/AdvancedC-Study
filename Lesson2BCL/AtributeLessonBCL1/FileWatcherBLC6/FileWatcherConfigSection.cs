using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileWatcherBLC6
{
	public class FileWatcherConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("culture", DefaultValue = "ru-RU")]
        public string Culture
        {
            get { return ((string)(this["culture"])); }
        }

        /// <summary>
        /// Коллекция доступных расширений файлов
        /// </summary>
		[ConfigurationProperty("targetFilesExtensions")]
		public FieldsCollection TargetFilesExtensions
		{
			get { return (FieldsCollection)this["targetFilesExtensions"]; }
		}

        /// <summary>
        /// Информация о выводе для найденых русских букв в файле
        /// </summary>
        [ConfigurationProperty("foundedInfoFormat")]
        public FieldElement FoundedInfoFormat
        {
            get { return (FieldElement)this["foundedInfoFormat"]; }
        }

        /// <summary>
        /// Информация об выводе для информации о файле
        /// </summary>
        [ConfigurationProperty("fileInfoFormat")]
        public FieldElement FileInfoFormat
        {
            get { return (FieldElement)this["fileInfoFormat"]; }
        }

        /// <summary>
        /// Информации об бэкапе
        /// </summary>
        [ConfigurationProperty("backupInfo")]
        public BackupFieldElement BackupInfo
        {
            get { return (BackupFieldElement)this["backupInfo"];}
        }


    }

    public class FieldsCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new FieldElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((FieldElement)element).Value;
        }
		public List<string> GetValuesAsArray()
		{
			List<string> array = new List<string>();
			foreach (var item in this)
			{
				array.Add(GetElementKey((FieldElement)item).ToString());
			}
			return array;
		}
    }

    public class FieldElement : ConfigurationElement
    {

        [ConfigurationProperty("value", IsRequired = true)]
        public string Value
        {
            get { return (string)this["value"]; }
        }

    }

    public class BackupFieldElement : ConfigurationElement
    {
        [ConfigurationProperty("saveConfirmation", IsRequired = true, DefaultValue = "false")]
        public string SaveConfirmation
        {
            get { return (string)this["saveConfirmation"]; }
        }

        [ConfigurationProperty("path", IsRequired = true, DefaultValue = "false")]
        public string Path
        {
            get { return (string)this["path"]; }
        }

        
    }
}
