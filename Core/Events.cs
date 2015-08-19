using System;
using Un4seen.Bass;
using System.Windows.Forms;
using System.Drawing;
using MiniPlayerClassic.Core;

namespace MiniPlayerClassic
{
    /// <summary>
    /// 播放器状态枚举
    /// </summary>
    public enum TrackStates { Playing, Paused, Stoped, Stalled }
    
    /// <summary>
    /// 播放器状态改变消息
    /// </summary>
    public class TrackStateChange : EventArgs
    {
        private TrackStates msg;
        public TrackStates Message { get { return msg; } set { msg = value; } }

        public TrackStateChange(TrackStates message) { msg = message; }
    }

    /// <summary>
    /// 播放器文件改变消息
    /// </summary>
    public class TrackFileChange : EventArgs
    {
        private string msg;
        public string Message { get { return msg; } set { msg = value; } }

        public TrackFileChange(string filename) { msg = filename; }
    }
}
