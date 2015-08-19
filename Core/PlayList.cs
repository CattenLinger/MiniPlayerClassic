using MiniPlayerClassic.Core;
using System;
using System.Collections.Generic;
using System.IO;

namespace MiniPlayerClassic
{
    /// <summary>
    /// 播放列表对象
    /// </summary>
    public class PlayList : LinkedList<PlayListItem>
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

        public static int MaxValue { get { return Int16.MaxValue / 2 + 1; } }

        private LinkedListNode<PlayListItem> indexor(int index)
        {
            LinkedListNode<PlayListItem> temp;

            if (index < 0 || index > this.Count)
            {
                throw new IndexOutOfRangeException();
            }

            int i = 0;

            if (index / 2 < (Count - 1))
            {
                temp = First;
                while (i != index && temp != null)
                {
                    temp = temp.Next;
                    i++;
                }
            }
            else
            {
                i = Count - 1;
                temp = First;

                while (i != index && temp != null)
                {
                    temp = temp.Previous;
                    i--;
                }
            }
            return temp;
        }

        public PlayListItem this[int index]
        {
            get { return indexor(index).Value; }
            set
            {
                LinkedListNode<PlayListItem> temp = indexor(index);
                AddAfter(temp, value);
                Remove(temp);
            }
        }
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
                    AddLast(new PlayListItem(ListProcessor.AddressList[i],ListProcessor.InfomationsList[i]));

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
                        AddLast(new PlayListItem(ListProcessor.AddressList[i], ListProcessor.InfomationsList[i]));
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
                        AddLast(new PlayListItem(ListProcessor.AddressList[i], ListProcessor.InfomationsList[i]));
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
}
