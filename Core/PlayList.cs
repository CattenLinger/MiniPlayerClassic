using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MiniPlayerClassic
{

    #region PlayList Item Define. value: FileAddress(string),Infomations(string).
    public class PlayListItem //播放列表项对象
    {
        public string FileAddress;
        public string Infomations;

        public PlayListItem(string file, string info)
        {
            FileAddress = file;
            Infomations = info;
        }

        public PlayListItem()
        {
            FileAddress = "";
            Infomations = "";
        }

        public void UpdateInfo(string file, string info)
        {
            FileAddress = file;
            Infomations = info;
        }
    }
    #endregion

    public class PlayList //播放列表对象
    {
        private LinkedList<PlayListItem> items; //新建一个链表用于存储播放列表项
        public LinkedList<PlayListItem> Items { get { return items; } }
        public int Count { get { return items.Count; } }
        private string filepath = "";
        public string FilePath { get { return filepath; } }
        private int operationscount = 0;
        public int OperationCount { get { return operationscount; } }

        private LinkedListNode<PlayListItem> ResearchByIndex(int index)//按序号查找节点并返回节点
        {
            LinkedListNode<PlayListItem> marked;
            if (index > items.Count - 1 || index < 0) { return null; }
            marked = items.First;
            for (int i = 0; i < items.Count; i++)
            {
                if (i == index) return marked;
                marked = marked.Next;
            }
            return null;
        }

        public PlayList() //初始化播放列表对象
        { 
            items = new LinkedList<PlayListItem>();
        }

        public PlayList(string file)
        {
            items = new LinkedList<PlayListItem>();
            if (load_file(file)) filepath = file;
        }

        public void Add(PlayListItem item)//添加节点
        {
            items.AddLast(item);
            operationscount++;
        }

        public bool Remove(int index)//按序号删除节点
        {
            try
            {
                LinkedListNode<PlayListItem> mark = items.Last;
                for (int i = items.Count - 1; i >= 0; i--)//倒序删除，配合外部的倒序取序号可以加快效率
                {
                    if (i == index)
                    {
                        items.Remove(ResearchByIndex(index));
                        break;
                    }
                    if(mark != items.First) mark = mark.Previous;//节点不是第一个的话向前移动
                }
            }
            catch(Exception e)
            {
                if (e != null) return false;
            }
            
            operationscount++;
            

            return true;
        }

        public bool Insert(int index, PlayListItem item)//增加节点
        {
            try
            {
                items.AddAfter(ResearchByIndex(index), new LinkedListNode<PlayListItem>(item));
                operationscount++;
            }
            catch (Exception e)
            {
                if (e != null) return false;
            }
            operationscount++;
            return true;
        }

        public void Clear()
        {
            items.Clear();
            operationscount++;
        }

        public PlayListItem GetItem(int index) //get一个node
        {
            return ResearchByIndex(index).Value;
        }

        public bool OpenListFile(string Filename)
        {
            items.Clear();
            operationscount = 0;
            return load_file(Filename);
        }

        public bool AddFromFile(string Filename)
        {
            operationscount++;
            return load_file(Filename);
        }

        public bool SaveToFile(string FileName)
        {
            if (FileName != filepath) filepath = FileName;
            operationscount = 0;
            return save_file();
        }

        private bool load_file(string Filename)
        {
            List<string> temp1 = ListProcessingMethods.ReadListFile(Filename);
            if (temp1 != null)
            {
                if (temp1.Count == 0) return true;
                for (int i = 0; i < temp1.Count; i++)
                    items.AddLast(new LinkedListNode<PlayListItem>(new PlayListItem(temp1[i],"")));
                return true;
            }
            else
                return false;
        }

        private bool save_file()
        {
            if (filepath != "")
                return ListProcessingMethods.SaveListFile(filepath, this);
            else
                return false;
        }
    }

    public class ListProcessingMethods //列表处理函数
    {
        public static List<string> ReadListFile(string FilePath)
        {
            if (!System.IO.File.Exists(FilePath))
                return null;

            List<string> temp = new List<string>();
            StreamReader file = new StreamReader(FilePath);
            string temp2;
            while ((temp2 = file.ReadLine()) != null)
            {
                
                temp.Add(temp2);
            }
            file.Close();
            System.GC.Collect();
            return temp;
        }

        public static bool SaveListFile(string FilePath,PlayList List)
        {
            StreamWriter file;
            if(!File.Exists(FilePath))
            {
                try
                {
                    file = File.CreateText(FilePath);
                    file.Close();
                }
                catch (Exception e)
                {
                    if (e != null) return false;
                }
            }
            file = new StreamWriter(FilePath);
            if(List.Items.Count != 0)
            {
                LinkedListNode<PlayListItem> item;
                item = List.Items.First;

                do
                {
                    file.WriteLine(item.Value.FileAddress);
                    item = item.Next;
                } while (item != null);
            }
            file.Close();
            System.GC.Collect();
            return true;
        }
    }
}
