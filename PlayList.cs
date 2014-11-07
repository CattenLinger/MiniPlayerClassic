using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniPlayerClassic
{

    #region PlayList Item Define
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
        public PlayListItem(uint index)
        {
            FileAddress = "";
            Infomations = "";
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
    #endregion

    public class PlayList
    {
        public LinkedList<PlayListItem> list;
        private LinkedListNode<PlayListItem> marked;

        private int i;

        public PlayList()
        { 
            list = new LinkedList<PlayListItem>();
        }

        public void Add(PlayListItem item)
        {
            list.AddLast(item);
        }

        public bool Remove(int index)
        {
            if (index > list.Count - 1 || index < 0) { return false; }
            marked = list.First;
            for (i = 0; i < list.Count; i++)
            {
                if (i == index) { list.Remove(marked); return true; }
                marked = marked.Next;
            }
            return true;
        }

        public bool Insert(int index, PlayListItem item) 
        {
            if (index > list.Count - 1 || index < 0) { return false; }
            marked = list.First;
            for (i = 0; i < list.Count; i++)
            {
                if (i == index) { list.AddAfter(marked, new LinkedListNode<PlayListItem>(item)); return true; }
                marked = marked.Next;
            }
            return true;
        }

        public PlayListItem GetItem(int index)
        {
            LinkedListNode<PlayListItem> marked;
            int i;
            if (index < 0 || index > list.Count - 1) { return null; }
            marked = list.First;
            for (i = 0; i < list.Count; i++) {
                if (i == index) { return marked.Value; }
                marked = marked.Next;
            }
            return null;
        }

        public bool Savetofile(string Filename)
        {
            return true;
        }
    }
}
