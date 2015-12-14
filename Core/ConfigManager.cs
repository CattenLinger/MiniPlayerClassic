using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace MiniPlayerClassic.Core
{
    public class ConfigManager
    {
        private string configFile = "";//配置文件默认空
        public string ConfigFilePath
        {
            get
            {
                return configFile;
            }
        }

        private Dictionary<string, Object> configBuffer = new Dictionary<string, object>();
        public ConfigManager(string ConfigFile)
        {
            configFile = ConfigFile;
            if (!System.IO.File.Exists(ConfigFile))
            {
                Save();
            }
            Read();
        }

        public bool Save()
        {
            XmlTextWriter writer = new XmlTextWriter(configFile, Encoding.Unicode);
            try
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Settings");
                writer.WriteElementString("AlwaysAtTop", WindowAlwaysTop.ToString());
                writer.WriteElementString("DeveloperMode", IsDeveloperMode.ToString());
                writer.WriteElementString("RememberLists", RememberLists.ToString());
                writer.WriteElementString("NewListAtLaunch", NewListAtLaunch.ToString());
                writer.WriteElementString("DefaultListPath", ListFilesPath);
                writer.WriteEndElement();
                writer.Close();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                writer.Close();
            }
        }

        public bool Read()//TODO
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(configFile);
                XmlNodeReader nodeReader = new XmlNodeReader(doc);
                configBuffer.Clear();

                string nodeName = null;
                string currentOption = null;

                while (nodeReader.Read())
                {
                    switch (nodeReader.Depth)
                    {
                        case 0:
                            nodeName = nodeReader.Name;
                            break;
                        case 1:
                            currentOption = nodeReader.Name;
                            break;
                        case 2:
                            configBuffer.Add(currentOption, nodeReader.ReadContentAsString());
                            break;
                    }
                }

                IsDeveloperMode = bool.Parse((string)configBuffer["DeveloperMode"]);
                WindowAlwaysTop = bool.Parse((string)configBuffer["AlwaysAtTop"]);
                NewListAtLaunch = bool.Parse((string)configBuffer["NewListAtLaunch"]);
                RememberLists = bool.Parse((string)configBuffer["RememberLists"]);
                ListFilesPath = (string)configBuffer["DefaultListPath"];
            }
            catch
            {
                return false;
            }
            return true;

        }

        public void SetDefault()
        {
            IsDeveloperMode = false;
            WindowAlwaysTop = false;
            NewListAtLaunch = false;
            RememberLists = false;
            ListFilesPath = "./List/";
        }

        public bool IsDeveloperMode = false;
        public bool WindowAlwaysTop = false;
        public bool NewListAtLaunch = false;
        public bool RememberLists = false;
        public string ListFilesPath = "./List/";
    }
}
