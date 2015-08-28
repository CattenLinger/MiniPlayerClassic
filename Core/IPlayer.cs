using System;


namespace MiniPlayerClassic.Core
{
    /// <summary>
    /// 播放器接口
    /// </summary>
    public interface IPlayer : IBasicPlayControl
    {
        /// <summary>
        /// 解码器信息
        /// </summary>
        string DecoderInfo { get; }
        /// <summary>
        /// 文件路径
        /// </summary>
        string FilePath { get; }
        /// <summary>
        /// 音量信息
        /// </summary>
        float Volume { get; set; }
        /// <summary>
        /// 当前播放进度
        /// </summary>
        double TrackPosition { get; set; }
        /// <summary>
        /// 音轨长度
        /// </summary>
        double TrackLength { get; }
        /// <summary>
        /// 播放状态
        /// </summary>
        TrackStates TrackState { get; }
        /// <summary>
        /// 支持的文件的扩展名
        /// </summary>
        string SupportStream { get; }

        /// <summary>
        /// 载入文件
        /// </summary>
        /// <param name="TrackName">文件路径</param>
        /// <returns>若成功返回true</returns>
        bool LoadFile(string TrackName);

        /// <summary>
        /// 播放状态改变消息
        /// </summary>
        event EventHandler<TrackStateChange> TrackStateChanged;
        /// <summary>
        /// 文件改变消息
        /// </summary>
        event EventHandler<TrackFileChange> TrackFileChanged;

    }
}
