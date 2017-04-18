using System;
using System.Collections;
using System.Text;
using System.Configuration;
using System.Xml;

namespace FileWatcherBLC6
{
    class Program
    {
        static void Main(string[] args)
        {
            FileWatcherConfigSection s2s = (FileWatcherConfigSection)ConfigurationManager.GetSection("fileWatcher");
            var gs  = s2s.Culture;
            var gs1 = s2s.TargetFilesExtensions;
            var gs2 = s2s.FoundedInfoFormat;
            var gs3 = s2s.FileInfoFormat;
            var gs4 = s2s.BackupInfo;
        }
    }

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
        [ConfigurationProperty("targetFilesExtensions", DefaultValue = "*.txt")]
        public FieldsCollection TargetFilesExtensions
        {
            get { return (FieldsCollection)this["targetFilesExtensions"]; }
        }

        /// <summary>
        /// Информация о выводе для найденых русских букв в файле
        /// </summary>
        [ConfigurationProperty("foundedInfoFormat", DefaultValue = "Ln: {0}")]
        public FieldElement FoundedInfoFormat
        {
            get { return (FieldElement)this["foundedInfoFormat"]; }
        }

        /// <summary>
        /// Информация об выводе для информации о файле
        /// </summary>
        [ConfigurationProperty("fileInfoFormat", DefaultValue = "{file_name}")]
        public FieldElement FileInfoFormat
        {
            get { return (FieldElement)this["fileInfoFormat"]; }
        }

        /// <summary>
        /// Информации об бэкапе
        /// </summary>
        [ConfigurationProperty("backupInfo"]
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
