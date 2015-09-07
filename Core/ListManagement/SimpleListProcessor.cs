using System;
using System.Collections.Generic;
using System.IO;

namespace MiniPlayerClassic.Core
{
    /// <summary>
    /// Simple List File（简单列表文件） 读取对象
    /// </summary>
    public class SimpleListReader : IListProcessor
    {
        public List<string> AddressList { get { return _addresslist; } }
        public List<string> InfomationsList { get { return _infomationslist; } }
        public string Extension { get { return ".spl"; } }
        public int ItemCount { get { if (_addresslist == null) return 0; else return _addresslist.Count; } }

        private List<string> _addresslist;
        private List<string> _infomationslist;

        public bool ReadListFile(string Filename)
        {
            if (!File.Exists(Filename))
                return false;

            _addresslist = new List<string>();
            StreamReader file = new StreamReader(Filename);
            string temp;
            while ((temp = file.ReadLine()) != null)
                _addresslist.Add(temp);
            file.Close();
            _infomationslist = new List<string>();
            for (int i = 0; i < _addresslist.Count; i++) _infomationslist.Add("");
            return true;
        }

        public bool SaveListFile(PlayList List, string Filename)
        {
            StreamWriter file = null;
            if (!File.Exists(Filename))
            {
                try { file = File.CreateText(Filename); }
                catch (Exception) { return false; }
            }

            if (file == null) file = new StreamWriter(Filename);
            if (List.Count != 0)
            {
                for (int i = 0; i < List.Count; i++)
                    file.WriteLine(List[i].FileAddress);
            }
            file.Close();
            return true;
        }
    }
}
