using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniPlayerClassic
{
    public class PlayListItem
    {
        public string FileAddress;
        public string Infomations;

        private string singer;
        private string title;
        private string album;
        private string others;

        public string Singer { get { return singer; } }
        public string Title { get { return title; } }
        public string Album { get { return album; } }
        public string Other { get { return others; } }

        public PlayListItem(string file, string info)
        {
            FileAddress = file;
            Infomations = info;
        }

        public void refresh_info()
        {
            if (Infomations == "") { return; }
            string[] info_s = Infomations.Split(new char[] {';'});
            singer = info_s[0];
            title = info_s[1];
            album = info_s[2];
            if (info_s.Length > 3) 
            {
                for (byte i = 3; i < info_s.Length; i++ ) { others += info_s[i]; }
            }
        }
    }

    public class PlayList
    {
        private LinkedList<PlayListItem> list;

        public PlayList()
        { 
            list = new LinkedList<PlayListItem>();
        }

        public void Add(string FileName, string Infomations)
        {
            
        }

        public void Remove(int index)
        {
            
        }
    }
}
