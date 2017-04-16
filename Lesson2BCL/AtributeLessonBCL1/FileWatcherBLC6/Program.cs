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
            //var s = ConfigurationSettings.AppSettings["server"];
            //var ss = s ?? "Haha";
            
            FileWatcherConfigSection s2s = (FileWatcherConfigSection)ConfigurationManager.GetSection("fileWatcher");
            var gs = s2s.Culture;
            var gs1 = s2s.TargetFilesExtensions;
            var gs2 = s2s.FoundedInfoFormat;
            var gs3 = s2s.FileInfoFormat;
            var gs4 = s2s.BackupInfo;
            gs.ToString();
        }
    }

    public class FileWatcherConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("culture")]
        public string Culture
        {
            get { return ((string)(this["culture"])); }
        }

        /// <summary>
        /// Коллекция столбцов таблицы
        /// </summary>
        [ConfigurationProperty("targetFilesExtensions")]
        public FieldsCollection TargetFilesExtensions
        {
            get { return (FieldsCollection)this["targetFilesExtensions"]; }
        }

        /// <summary>
        /// Коллекция столбцов таблицы
        /// </summary>
        [ConfigurationProperty("foundedInfoFormat", DefaultValue = "*.txt")]
        public FieldElement FoundedInfoFormat
        {
            get { return (FieldElement)this["foundedInfoFormat"]; }
        }

        /// <summary>
        /// Коллекция столбцов таблицы
        /// </summary>
        [ConfigurationProperty("fileInfoFormat", DefaultValue = "Ln: {0}")]
        public FieldElement FileInfoFormat
        {
            get { return (FieldElement)this["fileInfoFormat"]; }
        }

        /// <summary>
        /// Коллекция информации об бэкапе
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
