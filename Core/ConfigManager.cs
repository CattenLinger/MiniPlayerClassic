using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace MiniPlayerClassic.Core
{
    public class ConfigManager
    {

        private string configFile = "";
        public string ConfigFilePath
        {
            get
            {
                return configFile;
            }
        }
        public ConfigManager(string ConfigFile)
        {
            configFile = ConfigFile;
        }

        public bool Save()
        {
            try
            {
                XmlTextWriter writer = new XmlTextWriter(configFile, Encoding.Unicode);

                writer.WriteStartElement("MiniplayerSetting");
                writer.WriteElementString("AlwaysAtTop", windowAlwaysTop.ToString());
                writer.WriteElementString("DeveloperMode", developermode.ToString());
                writer.WriteElementString("RememberLists", rememberLists.ToString());
                writer.WriteElementString("NewListAtLaunch", newListAtLaunch.ToString());
                writer.WriteElementString("DefaultListPath", listFilesPath);
                writer.WriteEndElement();
                writer.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Read()
        {
            try
            {
                XmlTextReader reader = new XmlTextReader(configFile);

                
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public void SetDefault()
        {
            developermode = false;
            windowAlwaysTop = false;
            newListAtLaunch = false;
            rememberLists = false;
            listFilesPath = @"./List";
        }

        private bool developermode = false;
        public bool IsDeveloperMode
        {
            get
            {
                return developermode;
            }

            set
            {
                developermode = value;
            }
        }

        private bool windowAlwaysTop = false;
        public bool WindowAlwaysTop
        {
            get
            {
                return windowAlwaysTop;
            }

            set
            {
                windowAlwaysTop = value;
            }
        }

        private bool newListAtLaunch = false;
        public bool NewListAtLaunch
        {
            get
            {
                return newListAtLaunch;
            }

            set
            {
                newListAtLaunch = value;
            }
        }

        private bool rememberLists = false;
        public bool RememberLists
        {
            get
            {
                return rememberLists;
            }

            set
            {
                rememberLists = value;
            }
        }

        private string listFilesPath = "";
        public string ListFilesPath
        {
            get
            {
                return listFilesPath;
            }

            set
            {
                listFilesPath = value;
            }
        }
    }
}
