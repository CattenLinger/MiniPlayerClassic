using System;
using System.Text;
using Un4seen.Bass;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;


namespace MiniPlayerClassic
{   

    public class Player
    {
        #region const

        const int default_device = -1; //定义默认设别和默认码率
        const int default_rate = 44100;

        const int Player_Playing = 1; //定义播放状态对应的数值
        const int Player_Paused = 2;
        const int Player_Stoped = 0;

        const int File_StateChange = 10;

        public enum PlayerStates { Playing, Paused, Stoped, Stalled }

        #endregion

        #region Sub Classes

        public class PlayerStateChange : EventArgs
        {
            private PlayerStates msg;
            public PlayerStates Message { get { return msg; } set { msg = value; } }

            public PlayerStateChange(PlayerStates message) { msg = message; }
        }

        public class PlayerFileChange : EventArgs
        {
            private string msg;
            public string Message { get { return msg; } set { msg = value; } }

            public PlayerFileChange(string filename) { msg = filename; }
        }

        #endregion

        #region values
        private String filepath = ""; //保存文件路径
        private int errorcode = 1; //记录播放器在启动过程中的错误
        private PlayerStates playstate = PlayerStates.Stoped; //存储播放器状态
        private float volume = 1;//音量

        private BASS_INFO BassInfo;
        private int theStream = 0;//文件流
        private Timer tmrChecker;

        int _waveform_width = 0;
        int _waveform_height = 0;
        //private values end
        public string FilePath { get { return filepath; } }
        public int ErrorCode { get { return errorcode; } }
        public PlayerStates PlayState { get { return playstate; } }
        public float Volume { get { return volume; } }
        public Bitmap waveform = null;
        public Un4seen.Bass.Misc.WaveForm wf1 = null;
        public string SupportStream { get { return Bass.SupportedStreamExtensions; } }
        
        //public values end

        #endregion

        #region Events

        public event EventHandler<PlayerStateChange> StateChange;
        public event EventHandler<PlayerFileChange> FileChange;
        public event EventHandler WaveFormFinished;

        #endregion

        //progresses

        /* If you want to close the splash of Bass.Net you need to regist at 
        *  www.un4seen.com and input the registration code.
        */
        private void BassReg()
        {
            //BassNet.Registration("your_email", "your_code");
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
        protected virtual void on_call_StateChanger(PlayerStateChange e) //播放器状态改变消息构造函数
        {
            EventHandler<PlayerStateChange> handler = StateChange;
            if (handler != null) { handler(this, e); }
        }

        protected virtual void on_call_FileChange(PlayerFileChange e) //播放器文件改变消息构造函数
        {
            EventHandler<PlayerFileChange> handler = FileChange;
            if (handler != null) { handler(this, e); }
        }

        //检查信息的时钟
        private void tmrChecker_Tick(object sender, EventArgs e) //检查播放状态的时钟的代码
        {
            if (errorcode != 0) { return; }

            switch (Bass.BASS_ChannelIsActive(theStream))
            {
                case BASSActive.BASS_ACTIVE_PLAYING:
                    if (playstate != PlayerStates.Playing)
                    {
                        playstate = PlayerStates.Playing;
                        StateChange(this, new PlayerStateChange(PlayerStates.Playing));
                    }
                    break;
                case BASSActive.BASS_ACTIVE_PAUSED:
                    if (playstate != PlayerStates.Paused)
                    {
                        playstate = PlayerStates.Paused;
                        StateChange(this, new PlayerStateChange(PlayerStates.Paused));
                    }
                    break;
                case BASSActive.BASS_ACTIVE_STOPPED:
                    if (playstate != PlayerStates.Stoped)
                    {
                        playstate = PlayerStates.Stoped;
                        StateChange(this, new PlayerStateChange(PlayerStates.Stoped));
                    }
                    break;
                case BASSActive.BASS_ACTIVE_STALLED: 
                    if (playstate != PlayerStates.Stalled)
                    {
                        playstate = PlayerStates.Stalled;
                        StateChange(this, new PlayerStateChange(PlayerStates.Stalled));
                    }
                    break;
            }
        }

        //Input no pamaraters will use default configuration
        public Player(IntPtr win) //播放器初始化函数（重载）
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
        public Player(int device,int rate,IntPtr win)//播放器初始化函数
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
            wf1 = new Un4seen.Bass.Misc.WaveForm(filepath,new Un4seen.Bass.Misc.WAVEFORMPROC(waveformvcallback),null);
            wf1.ColorLeft = Color.Cyan;
            wf1.ColorRight = Color.LightCyan;
            wf1.ColorBackground = Color.White;
            wf1.DrawEnvelope = false;
            wf1.DrawVolume = Un4seen.Bass.Misc.WaveForm.VOLUMEDRAWTYPE.None;
            wf1.DrawWaveForm = Un4seen.Bass.Misc.WaveForm.WAVEFORMDRAWTYPE.Mono;
            wf1.RenderStart(true, BASSFlag.BASS_DEFAULT);
            _waveform_height = height;
            _waveform_width = width;
        }

        private void waveformvcallback(int framesDone, int framesTotal, TimeSpan elapsedTime, bool finished)
        {
            if(finished)
            {
                if(wf1 != null)
                {
                    waveform = wf1.CreateBitmap(_waveform_width, _waveform_height, -1, -1, false);
                    WaveFormFinished(this, new EventArgs());
                }
            }
            //throw new NotImplementedException();
        }
        
        //Get Bass info in text
        public string PlayerTextInfo() 
        {
            Bass.BASS_GetInfo(BassInfo);
            string info = BassInfo.ToString();
            return info;
        }

        //Get BassInfo object's info
        public BASS_INFO PlayerInfo() 
        {
            Bass.BASS_GetInfo(BassInfo);
            return BassInfo;
        }

        //读取文件
        public Boolean LoadFile(string Filename) 
        {
            Bass.BASS_StreamFree(theStream); //free the stream
            theStream = Bass.BASS_StreamCreateFile(Filename,0L,0L,BASSFlag.BASS_DEFAULT);
            if (theStream == 0) { return false; } else { SetVolume(volume); }
            filepath = Filename;
            FileChange(this, new PlayerFileChange(Filename));
            return true;
        }

        //播放文件流
        public Boolean Play()
        {
            if (playstate == PlayerStates.Stoped) { LoadFile(filepath); }
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
        public Boolean SetVolume(float vol) 
        {
            volume = vol;
            if (Bass.BASS_ChannelSetAttribute(theStream,BASSAttribute.BASS_ATTRIB_VOL,vol)) { return true; }
            return false;
        }

        //获取音量
        public float GetValue()
        { 
            float vol = 0;
            if (Bass.BASS_ChannelGetAttribute(theStream, BASSAttribute.BASS_ATTRIB_VOL,ref vol))
            { return vol; }  else  { return 0; }
        }

        //获取左右声道的当前响度
        public void GetLevel(ref int Left,ref int Right)
        {
            Int32 temp = Bass.BASS_ChannelGetLevel(theStream);
            if (temp == -1) { Left = 0; Right = 0; return; }
            Left = Utils.LowWord32(temp);
            Right = Utils.HighWord32(temp);
        }

        //设置播放进度
        public Boolean SetPosition(double seconds)
        {
            if (Bass.BASS_ChannelSetPosition(theStream, seconds)) { return true;}
            return false;
        }

        //获取播放进度
        public double GetPosition()
        {
            long temp;
            temp = Bass.BASS_ChannelGetPosition(theStream);
            if (temp == -1) { return -1; }
            return Bass.BASS_ChannelBytes2Seconds(theStream, temp);
        }

        //获取播放时间长度
        public double GetLength()
        {
            long temp;
            temp = Bass.BASS_ChannelGetLength(theStream);
            if (temp == -1) { return -1; }
            return Bass.BASS_ChannelBytes2Seconds(theStream, temp);
        }

        //获取频谱元数据
        public void getData(ref Single[] data)
        {
            if (data.Length < 256) { return; }
            if (playstate == PlayerStates.Playing)
            { Bass.BASS_ChannelGetData(theStream, data, -2147483647); }
            else { for (int i = 0; i < 256; i++) { data[i] = 0; } }
        }

    }

}
