using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MiniPlayerClassic
{

    #region PlayList Item Define.

    /// <summary>
    /// 播放列表项
    /// </summary>
    public class PlayListItem
    {
        private string _infomations;
        private string _fileaddress;
        /// <summary>
        /// 项目地址
        /// </summary>
        public string FileAddress { get { return _fileaddress; } }
        /// <summary>
        /// 项目附加信息
        /// </summary>
        public string Infomations { get { return _infomations; } }

        /// <summary>
        /// 获得一个基于项目附加信息生成的字符串组
        /// </summary>
        public string[] InfoList { get { return _infomations.Split(SplitChar); } }
        /// <summary>
        /// 项目附加信息关键字
        /// </summary>
        public static string[] KeyWords = { "Title", "Artist", "Album", "Others" };
        /// <summary>
        /// 项目附加信息枚举
        /// </summary>
        public enum KeyWord { Title, Artist, Album, Others }
        /// <summary>
        /// 项目附加信息的分隔符
        /// </summary>
        public char SplitChar = ';';

        /// <summary>
        /// 基于已有信息创建播放列表项
        /// </summary>
        /// <param name="file">描述项目路径</param>
        /// <param name="info">附加信息(一般用“;”分割，可通过改变SplitChar改变分隔符)</param>
        public PlayListItem(string file, string info)
        {
            _fileaddress = file;

            if (info != "")
                _infomations = info;
            else
                _infomations = "{title};{artist};{album};{others}";
        }

        /// <summary>
        /// 创建一个空播放列表项
        /// </summary>
        public PlayListItem()
        {
            _fileaddress = "";
            _infomations = "{title};{artist};{album};{others}";
        }

        /// <summary>
        /// 更新播放项信息
        /// </summary>
        /// <param name="file">项目路径（输入空字符串表示不更新）</param>
        /// <param name="info">项目附加信息（输入空字符串表示不更新）</param>
        public void UpdateInfo(string file, string info)
        {
            if(file != "") _fileaddress = file;
            if(info != "") _infomations = info;
        }

        /// <summary>
        /// 对象的描述性字符串输出
        /// </summary>
        /// <returns>此对象所包含的所有字符串</returns>
        public override string ToString() 
        { 
            return "FileAddress:" + _fileaddress + "\n" + "Infomations:" + _infomations; 
        }
    }
    #endregion

    /// <summary>
    /// 播放列表对象
    /// </summary>
    public class PlayList : List<PlayListItem>
    {
        private string _filepath = "";
        /// <summary>
        /// 列表文件地址
        /// </summary>
        public string FilePath { get { return _filepath; } }
        private int _operationscount = 0;//操作计次
        /// <summary>
        /// 列表操作数计次，
        /// </summary>
        public int OperationCount { get { return _operationscount; } set { _operationscount = value; } }

        private IListProcessor ListProcessor = null;//列表处理接口对象

        /// <summary>
        /// 预定文件名的播放列表对象构造函数
        /// </summary>
        /// <param name="file">列表文件地址</param>
        public PlayList(string file) 
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

        /// <summary>
        /// 谁知道有啥用捏┑(￣Д ￣)┍(创建一个空列表)
        /// </summary>
        public PlayList() { }

        /// <summary>
        /// 从指定文件加载列表
        /// </summary>
        /// <param name="Filename">文件名</param>
        /// <returns>若操作成功则返回true</returns>
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

        /// <summary>
        /// 从指定文件追加列表项
        /// </summary>
        /// <param name="Filename">文件名</param>
        /// <returns>若操作成功则返回true</returns>
        public bool AddFromFile(string Filename)
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

        /// <summary>
        /// 保存列表到指定文件
        /// </summary>
        /// <param name="Filename">文件名</param>
        /// <returns>若操作成功则返回true</returns>
        public bool SaveToFile(string Filename)
        {
            string ext = Path.GetExtension(Filename);
            if (ListProcessor == null || ListProcessor.Extension != ext)
                ListProcessor = _create_listprocessor(ext);

            if (ListProcessor.SaveListFile(this, Filename))
                return true;

            return false;
        }

        /// <summary>
        /// 自动判断文件扩展名并返回合适的ListProcess子类
        /// </summary>
        /// <param name="Extension">扩展名(例如 .spl)</param>
        /// <returns>用于加载对应类型的列表文件的ListProcess子类</returns>
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

    /// <summary>
    /// 列表读取对象接口
    /// </summary>
    interface IListProcessor
    {
        /// <summary>
        /// 地址列表
        /// </summary>
        List<string> AddressList { get; }
        /// <summary>
        /// 信息列表
        /// </summary>
        List<string> InfomationsList { get; }
        /// <summary>
        /// 代表子类对应的文件类型的扩展名
        /// </summary>
        string Extension { get; }
        /// <summary>
        /// 项目数量（地址列表项目数量，如果AL表null返回0
        /// </summary>
        int ItemCount { get; }

        /// <summary>
        /// 加载列表文件
        /// </summary>
        /// <param name="Filename">文件地址</param>
        /// <returns>若操作成功则返回true</returns>
        bool ReadListFile(string Filename);
        /// <summary>
        /// 保存PlayList到指定文件
        /// </summary>
        /// <param name="List">要保存的PlayList对象</param>
        /// <param name="Filename">文件路径</param>
        /// <returns>若操作成功则返回true</returns>
        bool SaveListFile(PlayList List, string Filename);
    }

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
