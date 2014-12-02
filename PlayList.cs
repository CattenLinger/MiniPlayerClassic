using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniPlayerClassic
{

    #region PlayList Item Define
    public class PlayListItem //播放列表项对象
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

        public void refresh_info() //用于刷新一次信息（未完成
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

    public class PlayListBoard//播放列表板
    {
        public LinkedList<PlayList> lists;
        private LinkedListNode<PlayList> marked;

        public int Count { get { return lists.Count; } }

        private int i;

        public PlayListBoard()
        {
            lists = new LinkedList<PlayList>();
        }

        public void Create(PlayList playlist)
        {
            lists.AddLast(new LinkedListNode<PlayList>(playlist));
        }

        public Boolean Delete(int index)
        {
            if (index < 0 || index > lists.Count - 1) { return false; }

            marked = lists.First;
            for (i = 0; i < lists.Count; i++)
            {
                if (i == index) lists.Remove(marked);
                marked = marked.Next;
                return true;
            }
            return false;
        }

        public PlayList GetItem(int index)
        {
            if (index < 0 || index > lists.Count - 1) { return null; }

            marked = lists.First;
            for (i = 0; i < lists.Count; i++)
            {
                if (i == index) return marked.Value;
                marked = marked.Next;
            }
            return null;
        }
    }

    public class PlayList //播放列表对象
    {
        public LinkedList<PlayListItem> list; //新建一个链表
        private LinkedListNode<PlayListItem> marked; //新建一个节点对象，因为操作链表需要先有一个节点被读取出来

        private int i;

        public PlayList() //初始化播放列表对象
        { 
            list = new LinkedList<PlayListItem>();
        }

        public void Add(PlayListItem item)//添加节点
        {
            list.AddLast(item);
        }

        public bool Remove(int index)//按序号删除节点
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

        public bool Insert(int index, PlayListItem item)//插入节点
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

        public PlayListItem GetItem(int index) //get一个node
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

        public bool Savetofile(string Filename)//保存到顺序文件里（未完成
        {
            return true;
        }
    }
}
