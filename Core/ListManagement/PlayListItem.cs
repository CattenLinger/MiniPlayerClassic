using System.Collections.Generic;

namespace MiniPlayerClassic.Core
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
            if (file != "") _fileaddress = file;
            if (info != "") _infomations = info;
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
}
