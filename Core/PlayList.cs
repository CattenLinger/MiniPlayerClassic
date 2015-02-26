using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MiniPlayerClassic
{

    #region PlayList Item Define.
    public class PlayListItem //播放列表项对象
    {
        private string _infomations;
        private string _fileaddress;
        public string FileAddress { get { return _fileaddress; } }
        public string Infomations { get { return _infomations; } }

        public string[] InfoList { get { return _infomations.Split(SplitChar); } }
        public static string[] KeyWords = { "Title", "Artist", "Album", "Others" };
        public char SplitChar = ';';

        public PlayListItem(string file, string info)
        {
            _fileaddress = file;

            if (info != "")
                _infomations = info;
            else
                _infomations = "{title};{artist};{album};{others}";
        }

        public PlayListItem()
        {
            _fileaddress = "";
            _infomations = "{title};{artist};{album};{others}";
        }

        public void UpdateInfo(string file, string info)
        {
            if(file != "") _fileaddress = file;
            if(info != "") _infomations = info;
        }

        public override string ToString() 
        { 
            return "FileAddress:" + _fileaddress + "\n" + "Infomations:" + _infomations; 
        }
    }
    #endregion

    public class PlayList : List<PlayListItem> //播放列表对象
    {
        private string _filepath = "";//文件名
        public string FilePath { get { return _filepath; } }
        private int _operationscount = 0;//操作计次
        public int OperationCount { get { return _operationscount; } set { _operationscount = value; } }

        private IListProcessor ListProcessor = null;//列表处理接口对象

        public PlayList(string file) //预定文件名的播放列表对象构造函数
        {
            if(File.Exists(file))
            {
                ListProcessor = _create_listprocessor(Path.GetExtension(file));
                if(ListProcessor == null) throw new FileLoadException("File not Supported",file);
            }
            if(ListProcessor == null) throw new FileNotFoundException("File not found",file);

            if (ListProcessor.ReadListFile(file))
            {
                for(int i = 0; i < ListProcessor.ItemCount; i++)
                    Add(new PlayListItem(ListProcessor.AddressList[i],ListProcessor.InfomationsList[i]));

                _filepath = file;
            }
            else
                throw new FileLoadException("File Opening Error, Please check the file is Exits or Accessable.", file);
        }

        public PlayList() {}//谁知道有啥用捏┑(￣Д ￣)┍

        public bool OpenListFile(string Filename) //打开列表文件
        {
            string ext;
            if (File.Exists(Filename))
            {
                ext = Path.GetExtension(Filename);
                if (ListProcessor == null || ListProcessor.Extension != ext)
                    ListProcessor = _create_listprocessor(ext);//自动判断并创建合适的列表处理对象

                if (ListProcessor == null) return false;//啥？塞了我不认识的文件？臣妾打不开啊 QwQ
                
                if(ListProcessor.ReadListFile(Filename))//把列表处理对象里预读的列表塞进播放列表里
                {
                    Clear();//先扫走不要的项目
                    for (int i = 0; i < ListProcessor.ItemCount; i++)
                        Add(new PlayListItem(ListProcessor.AddressList[i], ListProcessor.InfomationsList[i]));
                    _filepath = Filename;
                    _operationscount = 0;
                    return true;//处理成功惹(・∀・)
                }
            }
            return false;
        }

        public bool AddFromFile(string Filename)//追加列表文件
        {
            string ext;
            if(File.Exists(Filename))
            {
                ext = Path.GetExtension(Filename);
                if (ListProcessor == null || ListProcessor.Extension != ext)
                    ListProcessor = _create_listprocessor(ext);

                if (ListProcessor == null) return false;

                if(ListProcessor.ReadListFile(Filename))
                {
                    for (int i = 0; i < ListProcessor.ItemCount; i++)
                        Add(new PlayListItem(ListProcessor.AddressList[i], ListProcessor.InfomationsList[i]));
                    _operationscount += ListProcessor.ItemCount;//好像告诉你塞了多少个文件也没啥不好ԾㅂԾ
                    return true;
                }
            }
            return false;
        }

        public bool SaveToFile(string Filename)
        {
            string ext = Path.GetExtension(Filename);
            if (ListProcessor == null || ListProcessor.Extension != ext)
                ListProcessor = _create_listprocessor(ext);

            if (ListProcessor.SaveListFile(this, Filename))
                return true;

            return false;
        }

        private IListProcessor _create_listprocessor(string Extension)
        {
            //-------------------------------------------------------------------------------
            //判断列表文件类型的代码放在这里。根据不同的文件类型使用相应的“列表处理对象”
            //
            if(Extension == ".spl")
                return new SimpleListReader();

            //-------------------------------------------------------------------------------
            return null;
        }   
    }

    #region Old PlayList Codes
    /*
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

        public PlayListItem GetItem(int index) 
        {
            return ResearchByIndex(index).Value;
        }

        public LinkedListNode<PlayListItem> GetNode(int index)
        {
            return ResearchByIndex(index);
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
     */
    #endregion

    interface IListProcessor //列表读取对象接口
    {
        List<string> AddressList { get; }//地址列表
        List<string> InfomationsList { get; }//信息列表
        string Extension { get; }//扩展名（一般只读
        int ItemCount { get; }//项目数量（地址列表项目数量，如果AL表null返回0

        bool ReadListFile(string Filename);//当然是加载列表啦
        bool SaveListFile(PlayList List, string Filename);//当然是保存列表啦
    }

    public class SimpleListReader : IListProcessor // .spl，Simple List File（简单列表文件） 读取对象
    {
        public List<string> AddressList { get { return _addresslist; } }
        public List<string> InfomationsList { get { return _infomationslist; } }
        public string Extension { get { return ".spl"; } }
        public int ItemCount { get { if (_addresslist == null) return 0; else return _addresslist.Count; } }

        private List<string> _addresslist;
        private List<string> _infomationslist;

        public bool ReadListFile(string Filename)
        {
            if (! File.Exists(Filename))
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
            if(!File.Exists(Filename))
            {
                try { file = File.CreateText(Filename); }
                catch (Exception) { return false; }
            }

            if (file == null) file = new StreamWriter(Filename);
            if(List.Count != 0)
            {
                for (int i = 0; i < List.Count; i++)
                    file.WriteLine(List[i].FileAddress);
            }
            file.Close();
            return true;
        }
    }

    #region Old List Processing Methods
    /*
    public class ListProcessingMethods //列表处理函数
    {
        public static List<string> ReadListFile(string FilePath)
        {
            if (! File.Exists(FilePath))
                return null;

            List<string> temp = new List<string>();
            StreamReader file = new StreamReader(FilePath);
            string temp2;
            while ((temp2 = file.ReadLine()) != null)
            {
                
                temp.Add(temp2);
            }
            file.Close();
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
            return true;
        }
    }*/
    #endregion
}
