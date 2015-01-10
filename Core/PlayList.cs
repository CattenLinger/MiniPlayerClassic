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
            refresh_info();
        }
        public PlayListItem()
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
            others = "";
            if (info_s.Length > 3)
                for (byte i = 3; i < info_s.Length; i++ ) others += info_s[i];
        }
    }
    #endregion

    public class PlayListBoard//播放列表板
    {
        public LinkedList<PlayList> lists;//存储列表的链表
        public int CurrentListIndex { get { return currentlistindex; } }
        public PlayList CurrentList;//当前被激活的播放列表
        public int Count { get { return lists.Count; } }//获取列表项目数

        private int currentlistindex = 0;//当前被激活的播放列表的序号

        private LinkedListNode<PlayList> ResearchByIndex(int index)//按序号查找节点并返回节点
        {
            LinkedListNode<PlayList> marked; //新建一个节点对象，因为操作链表需要先有一个节点被读取出来
            if (index > lists.Count - 1 || index < 0) return null;

            if (index > (lists.Count - 1) / 2) //折半查找
            {
                marked = lists.First;
                for (int i = 0; i <= lists.Count - 1; i++)
                {
                    if (i == index) return marked;
                    marked = marked.Next;
                }
            }
            else
            {
                marked = lists.Last;
                for (int i = lists.Count - 1; i >= 0; i--)
                {
                    if (i == index) return marked;
                    marked = marked.Previous;
                }
            }
            return null;
        }

        public PlayListBoard()//构造函数
        {
            lists = new LinkedList<PlayList>();
        }

        public void Create(PlayList playlist)//创建并添加列表
        {
            lists.AddLast(new LinkedListNode<PlayList>(playlist));
        }

        public bool Delete(int index)//按序号删除列表
        {
            try
            {
                lists.Remove(ResearchByIndex(index));
                if (lists.Count != 0) SelectList(currentlistindex - 1);
                else if (lists.Count == 0) currentlistindex = -1;
            }
            catch (Exception e)
            {
                if (e != null) return false;
            }
            return false;
        }

        public PlayList GetItem(int index)//按序号获取一个播放列表对象
        {
            return ResearchByIndex(index).Value;
        }

        public bool SelectList(int index)//按序号选择列表
        {
            try
            {
                CurrentList = ResearchByIndex(index).Value;
                currentlistindex = index;
                return true;
            }
            catch (Exception e)
            {
                if(e != null) return false;
            }
            return false;
        }
    }
//----------------------------------------------------------------------------------------------------------
    public class PlayList //播放列表对象
    {
        public LinkedList<PlayListItem> list; //新建一个链表用于存储播放列表项
        private string filepath = "";
        public string FilePath { get { return FilePath; } }
        
        private LinkedListNode<PlayListItem> ResearchByIndex(int index)//按序号查找节点并返回节点
        {
            LinkedListNode<PlayListItem> marked; //新建一个节点对象，因为操作链表需要先有一个节点被读取出来
            if (index > list.Count - 1 || index < 0) { return null; }
            marked = list.First;
            for (int i = 0; i < list.Count; i++)
            {
                if (i == index) return marked;
                marked = marked.Next;
            }
            return null;
        }

        public PlayList() //初始化播放列表对象
        { 
            list = new LinkedList<PlayListItem>();
        }

        public PlayList(string file)
        {
            list = new LinkedList<PlayListItem>();

        }

        public void Add(PlayListItem item)//添加节点
        {
            list.AddLast(item);
        }

        public bool Remove(int index)//按序号删除节点
        {
            try
            {
                LinkedListNode<PlayListItem> mark = list.Last;
                for (int i = list.Count - 1; i >= 0; i--)//倒序删除，配合外部的倒序取序号可以加快效率
                {
                    if (i == index)
                    {
                        list.Remove(ResearchByIndex(index));
                        return true;
                    }
                    if(mark != list.First) mark = mark.Previous;//节点不是第一个的话向前移动
                }
            }
            catch(Exception e)
            {
                if (e != null) return false;
            }
            return true;
        }

        public bool Insert(int index, PlayListItem item)//增加节点
        {
            try
            {
                list.AddAfter(ResearchByIndex(index), new LinkedListNode<PlayListItem>(item));
            }
            catch (Exception e)
            {
                if (e != null) return false;
            }
            return true;
        }

        public PlayListItem GetItem(int index) //get一个node
        {
            return ResearchByIndex(index).Value;
        }

        public bool ReadFormFile(string Filename)//从文件读取（未完成
        {

            return true;
        }

        public bool SaveToFile(string Filename)//保存到顺序文件里（未完成
        {
            if (filepath == "") { filepath = Filename; }
            return true;
        }
    }
}
