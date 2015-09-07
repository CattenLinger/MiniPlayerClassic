using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniPlayerClassic.Core
{
    /// <summary>
    /// 包含播放、暂停、停止三个基本播放控制接口
    /// </summary>
    public interface IBasicPlayControl
    {
        /// <summary>
        /// 播放
        /// </summary>
        /// <returns>成功则返回true</returns>
        bool Play();
        /// <summary>
        /// 暂停
        /// </summary>
        /// <returns>成功则返回true</returns>
        bool Pause();
        /// <summary>
        /// 停止
        /// </summary>
        /// <returns>成功则返回true</returns>
        bool Stop();
    }
}
