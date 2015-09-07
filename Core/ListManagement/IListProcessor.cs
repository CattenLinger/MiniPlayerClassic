using System.Collections.Generic;

namespace MiniPlayerClassic.Core
{
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
}
