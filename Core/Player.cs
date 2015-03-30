using System;
using System.Text;
using Un4seen.Bass;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;


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

    /// <summary>
    /// 播放器接口
    /// </summary>
    public interface IPlayer
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
        /// 暂停
        /// </summary>
        /// <returns>成功则返回true</returns>
        bool Stop();

        /// <summary>
        /// 播放状态改变消息
        /// </summary>
        event EventHandler<TrackStateChange> TrackStateChanged;
        /// <summary>
        /// 文件改变消息
        /// </summary>
        event EventHandler<TrackFileChange> TrackFileChanged;

    }

    public interface IVisualEffects
    {
        void GetWaveForm(int width, int height);
        void getData(ref Single[] data);
        void GetLevel(ref int Left, ref int Right);

        Bitmap WaveForm { get; }

        event EventHandler WaveFormFinished;
    }

    public class BassNET_Player : IPlayer, IVisualEffects
    {
        #region const

        const int default_device = -1; //定义默认设别和默认码率
        const int default_rate = 44100;

        #endregion

        #region values
        private String filepath = ""; //保存文件路径
        private int errorcode = 1; //记录播放器在启动过程中的错误
        private TrackStates playstate = TrackStates.Stoped; //存储播放器状态
        private float _volume = 1;//音量

        private BASS_INFO BassInfo;
        private int theStream = 0;//文件流
        private Timer tmrChecker;

        int _waveform_width = 0;
        int _waveform_height = 0;
        //private values end
        public string FilePath { get { return filepath; } }
        public String DecoderInfo { get { return "Bass.NET"; } }
        public TrackStates TrackState { get { return playstate; } }

        public Bitmap WaveForm { get { return _waveform; } }
        private Bitmap _waveform = null;
        public Un4seen.Bass.Misc.WaveForm wf1 = null;
        public string SupportStream { get { return Bass.SupportedStreamExtensions; } }
        
        //public values end

        #endregion

        #region Events

        public event EventHandler<TrackStateChange> TrackStateChanged;
        public event EventHandler<TrackFileChange> TrackFileChanged;
        public event EventHandler WaveFormFinished;

        #endregion

        //progresses

        /* If you want to close the splash of Bass.Net you need to regist at 
        *  www.un4seen.com and input the registration code.
        */
        private void BassReg()
        {
            //BassNet.Registration("email", "code");
        }

        //对象初始化 Object initialization
        private void init()
        {
            //创建一个定时器（时钟），用于检测播放状态
            tmrChecker = new Timer();
            tmrChecker.Interval = 100;
            tmrChecker.Tick += tmrChecker_Tick;
            tmrChecker.Enabled = true;
        }
        
        //状态变化消息
        protected virtual void on_call_StateChanger(TrackStateChange e) //播放器状态改变消息构造函数
        {
            EventHandler<TrackStateChange> handler = TrackStateChanged;
            if (handler != null) { handler(this, e); }
        }

        protected virtual void on_call_FileChange(TrackFileChange e) //播放器文件改变消息构造函数
        {
            EventHandler<TrackFileChange> handler = TrackFileChanged;
            if (handler != null) { handler(this, e); }
        }

        //检查信息的时钟
        private void tmrChecker_Tick(object sender, EventArgs e) //检查播放状态的时钟的代码
        {
            if (errorcode != 0) { return; }

            switch (Bass.BASS_ChannelIsActive(theStream))
            {
                case BASSActive.BASS_ACTIVE_PLAYING:
                    if (playstate != TrackStates.Playing)
                    {
                        playstate = TrackStates.Playing;
                        TrackStateChanged(this, new TrackStateChange(TrackStates.Playing));
                    }
                    break;
                case BASSActive.BASS_ACTIVE_PAUSED:
                    if (playstate != TrackStates.Paused)
                    {
                        playstate = TrackStates.Paused;
                        TrackStateChanged(this, new TrackStateChange(TrackStates.Paused));
                    }
                    break;
                case BASSActive.BASS_ACTIVE_STOPPED:
                    if (playstate != TrackStates.Stoped)
                    {
                        playstate = TrackStates.Stoped;
                        TrackStateChanged(this, new TrackStateChange(TrackStates.Stoped));
                    }
                    break;
                case BASSActive.BASS_ACTIVE_STALLED: 
                    if (playstate != TrackStates.Stalled)
                    {
                        playstate = TrackStates.Stalled;
                        TrackStateChanged(this, new TrackStateChange(TrackStates.Stalled));
                    }
                    break;
            }
        }

        //Input no pamaraters will use default configuration
        public BassNET_Player(IntPtr win) //播放器初始化函数（重载）
        {
            BassReg();
            if (Bass.BASS_Init(default_device, default_rate, BASSInit.BASS_DEVICE_LATENCY, win))
            {
                BassInfo = new BASS_INFO();
                errorcode = 0;
            }
            init();
        }

        //Use custom configuration
        public BassNET_Player(int device, int rate, IntPtr win)//播放器初始化函数
        {
            BassReg();
            
            if ( Bass.BASS_Init(device,rate, BASSInit.BASS_DEVICE_LATENCY , win) ) //Bass initialization
            {
                BassInfo = new BASS_INFO();
                errorcode = 0;
            }
            init();
        }
        
        public void GetWaveForm(int width, int height)
        {
            wf1 = new Un4seen.Bass.Misc.WaveForm(filepath,new Un4seen.Bass.Misc.WAVEFORMPROC(waveformcallback),null);
            wf1.ColorLeft = Color.WhiteSmoke;
            wf1.ColorRight = wf1.ColorLeft;
            wf1.ColorBackground = Color.White;
            wf1.DrawEnvelope = false;
            wf1.DrawVolume = Un4seen.Bass.Misc.WaveForm.VOLUMEDRAWTYPE.None;
            wf1.DrawWaveForm = Un4seen.Bass.Misc.WaveForm.WAVEFORMDRAWTYPE.Mono;
            wf1.RenderStart(true, BASSFlag.BASS_DEFAULT);
            _waveform_height = height;
            _waveform_width = width;
        }

        private void waveformcallback(int framesDone, int framesTotal, TimeSpan elapsedTime, bool finished)
        {
            if(finished)
            {
                if(wf1 != null)
                {
                    try
                    {
                        (_waveform = wf1.CreateBitmap(_waveform_width, _waveform_height, -1, -1, false)).MakeTransparent(Color.White);
                    }
                    catch(Exception)
                    {
                        _waveform = null;
                    }
                    WaveFormFinished(this, new EventArgs());
                }
            }
            //throw new NotImplementedException();
        }

        //读取文件
        public Boolean LoadFile(string Filename) 
        {
            Bass.BASS_StreamFree(theStream); //free the stream
            theStream = Bass.BASS_StreamCreateFile(Filename,0L,0L,BASSFlag.BASS_DEFAULT);
            if (theStream == 0) { return false; } else { Volume = _volume; }
            filepath = Filename;
            TrackFileChanged(this, new TrackFileChange(Filename));
            return true;
        }

        //播放文件流
        public Boolean Play()
        {
            if (playstate == TrackStates.Stoped) { LoadFile(filepath); }
            if (Bass.BASS_ChannelPlay(theStream, false)) { return true; } else { return false; }
        }

        //暂停
        public Boolean Pause()
        {
            if (Bass.BASS_ChannelPause(theStream)) { return true; }
            return false;
        }

        //停止（并释放文件）
        public Boolean Stop()
        {
            if (Bass.BASS_ChannelStop(theStream) && Bass.BASS_StreamFree(theStream)) {  return true; }
            return false;
        }

        //设置音量
        public float Volume
        {
            get
            { 
                float vol = 0;
                if (Bass.BASS_ChannelGetAttribute(theStream, BASSAttribute.BASS_ATTRIB_VOL, ref vol))
                { return vol; } else { return 0; } 
            }
            set 
            {
                _volume = value;
                Bass.BASS_ChannelSetAttribute(theStream, BASSAttribute.BASS_ATTRIB_VOL, _volume);
            }
        }

        //获取左右声道的当前响度
        public void GetLevel(ref int Left,ref int Right)
        {
            Int32 temp = Bass.BASS_ChannelGetLevel(theStream);
            if (temp == -1) { Left = 0; Right = 0; return; }
            Left = Utils.LowWord32(temp);
            Right = Utils.HighWord32(temp);
        }

        public double TrackPosition
        {
            get
            {
                long temp;
                temp = Bass.BASS_ChannelGetPosition(theStream);
                if (temp == -1) { return -1; }
                return Bass.BASS_ChannelBytes2Seconds(theStream, temp);
            }

            set
            {
                Bass.BASS_ChannelSetPosition(theStream, value);
            }
        }

        public double TrackLength
        {
            get
            {
                long temp;
                temp = Bass.BASS_ChannelGetLength(theStream);
                if (temp == -1) { return -1; }
                return Bass.BASS_ChannelBytes2Seconds(theStream, temp);
            }
        }

        //获取频谱元数据
        public void getData(ref Single[] data)
        {
            if (data.Length < 256) { return; }
            if (playstate == TrackStates.Playing)
            { Bass.BASS_ChannelGetData(theStream, data, -2147483647); }
            else { for (int i = 0; i < 256; i++) { data[i] = 0; } }
        }

    }

}
