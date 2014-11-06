using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Un4seen.Bass;
using System.Windows.Forms;


namespace MiniPlayerClassic
{
    public class PlayerStateMessage : EventArgs
    {
        private int msg;
        public int Message { get { return msg; } set { msg = value; } }
        public PlayerStateMessage(int message) { msg = message; }
    }

    public class Player
    {
        #region const
        const int default_device = -1;
        const int default_rate = 44100;

        const int Player_Playing = 1;
        const int Player_Paused = 2;
        const int Player_Stoped = 0;

        const int File_StateChange = 10;

        public enum t_formate
        {
            full_day = 0,
            full_hour = 1,
            full_minute = 2,
            simple_second = 3,

            day = 4,
            hour = 5,
            minute = 6,
            second = 7,
            ms = 8

        }

        #endregion

        #region values
        public String FilePath = ""; //File Path
        public int ErrorCode = 1; //Any matter comeout while initialization will recored here.
        public int PlayState = Player_Stoped; //Recored playing state
        public float Volume = 1;
        //public values end
        private BASS_INFO BassInfo;
        private int theStream = 0;//The File Stream
        private Timer tmrChecker;
        //private values end
        #endregion

        public event EventHandler<PlayerStateMessage> call_StateChange;
        //progresses

        /* If you want to close the splash of Bass.Net you need to regist at 
        *  www.un4seen.com and input the registration code.
        */
        private void BassReg()
        {
            //BassNet.Registration("your_email","your_code");
        }

        //Object initialization
        private void init()
        {
            //timer
            tmrChecker = new Timer();
            tmrChecker.Interval = 100;
            tmrChecker.Tick += tmrChecker_Tick;
            tmrChecker.Enabled = true;
        }
        
        //State change event
        protected virtual void on_call_StateChanger(PlayerStateMessage e)
        {
            EventHandler<PlayerStateMessage> handler = call_StateChange;
            if (handler != null) { handler(this, e); }
        }

        //timer for check informations
        private void tmrChecker_Tick(object sender, EventArgs e)
        {
            if (ErrorCode != 0) { return; }

            switch (Bass.BASS_ChannelIsActive(theStream))
            {
                case BASSActive.BASS_ACTIVE_PLAYING:
                    if (PlayState != Player_Playing)
                    {
                        PlayState = Player_Playing;
                        call_StateChange(this, new PlayerStateMessage(Player_Playing));
                    }
                    break;
                case BASSActive.BASS_ACTIVE_PAUSED: 
                    if (PlayState != Player_Paused )
                    {
                        PlayState = Player_Paused;
                        call_StateChange(this, new PlayerStateMessage(Player_Paused));
                    }
                    break;
                case BASSActive.BASS_ACTIVE_STOPPED: 
                    if (PlayState != Player_Stoped)
                    {
                        PlayState = Player_Stoped;
                        call_StateChange(this, new PlayerStateMessage(Player_Stoped));
                    }
                    break;
                case BASSActive.BASS_ACTIVE_STALLED: 
                    if (PlayState != -1)
                    {
                        PlayState = -1;
                        call_StateChange(this, new PlayerStateMessage(-1));
                    }
                    break;
            }
            //throw new NotImplementedException();
        }

        //Input no pamaraters will use default configuration
        public Player() 
        {
            BassReg();
            if (Bass.BASS_Init(default_device, default_rate, BASSInit.BASS_DEVICE_LATENCY, IntPtr.Zero))
            {
                BassInfo = new BASS_INFO();
                ErrorCode = 0;
            }
            init();
        }

        //Use custom configuration
        public Player(int device,int rate)
        {
            BassReg();
            
            if ( Bass.BASS_Init(device,rate, BASSInit.BASS_DEVICE_LATENCY , IntPtr.Zero) ) //Bass initialization
            {
                BassInfo = new BASS_INFO();
                ErrorCode = 0;
            }
            init();
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

        //File load
        public Boolean LoadFile(string Filename) 
        {
            Bass.BASS_StreamFree(theStream); //free the stream
            theStream = Bass.BASS_StreamCreateFile(Filename,0L,0L,BASSFlag.BASS_DEFAULT);
            if (theStream == 0) { return false; } else { SetVolume(Volume); }
            FilePath = Filename;
            call_StateChange(this, new PlayerStateMessage(File_StateChange));
            return true;
        }

        //Play Stream
        public Boolean Play()
        {
            if (PlayState == Player_Stoped) { LoadFile(FilePath); }
            if (Bass.BASS_ChannelPlay(theStream, false)) { return true; } else { return false; }
        }

        //Pause Stream
        public Boolean Pause()
        {
            if (Bass.BASS_ChannelPause(theStream)) { return true; }
            return false;
        }

        //Stop Stream, then clean the stream and free the file
        public Boolean Stop()
        {
            if (Bass.BASS_ChannelStop(theStream) && Bass.BASS_StreamFree(theStream)) { return true; }
            return false;
        }

        //Set Channel's Volume
        public Boolean SetVolume(float vol) 
        {
            Volume = vol;
            if (Bass.BASS_ChannelSetAttribute(theStream,BASSAttribute.BASS_ATTRIB_VOL,vol)) { return true; }
            return false;
        }

        //Get Channel's Vloume
        public float GetValue()
        { 
            float vol = 0;
            if (Bass.BASS_ChannelGetAttribute(theStream, BASSAttribute.BASS_ATTRIB_VOL,ref vol))
            { return vol; }  else  { return 0; }
        }

        //Get Channel's Level
        public void GetLevel(ref int Left,ref int Right)
        {
            Int32 temp = Bass.BASS_ChannelGetLevel(theStream);
            if (temp == -1) { Left = 0; Right = 0; return; }
            Left = Utils.LowWord32(temp);
            Right = Utils.HighWord32(temp);
        }

        //Set Channel's Position
        public Boolean SetPosition(double seconds)
        {
            if (Bass.BASS_ChannelSetPosition(theStream, seconds)) { return true;}
            return false;
        }

        //Get Channle's Position
        public double GetPosition()
        {
            long temp;
            temp = Bass.BASS_ChannelGetPosition(theStream);
            if (temp == -1) { return -1; }
            return Bass.BASS_ChannelBytes2Seconds(theStream, temp);
        }

        //Get the Channel's length
        public double GetLength()
        {
            long temp;
            temp = Bass.BASS_ChannelGetLength(theStream);
            if (temp == -1) { return -1; }
            return Bass.BASS_ChannelBytes2Seconds(theStream, temp);
        }

        public void getData(byte[] data) 
        {
            if (data.Length < 512) { return; }
            if (PlayState == Player_Playing)
            { Bass.BASS_ChannelGetData(theStream, data, 128); }
            else { for (int i = 0; i < 512; i++) { data[i] = 0; } }
        }

        public void getData(Single[] data)
        {
            if (data.Length < 512) { return; }
            if (PlayState == Player_Playing)
            { Bass.BASS_ChannelGetData(theStream, data, -2147483647); }
            else { for (int i = 0; i < 512; i++) { data[i] = 0; } }
        }

        //trans mm to formated time in string 
        public string trans_Time(Int64 ms) //单变量的重载
        {
            return trans_Time(ms, t_formate.full_day,true);//其实就是填默认值而已
        }

        public string trans_Time(Int64 ms, t_formate formate)//这个也是
        {
            return trans_Time(ms, formate, true);
        }

        public string trans_Time(Int64 ms, t_formate formate,bool full)
        {
            const int msPerSec = 1000;//每秒的毫秒数
            const int sPerMinute = 60;//每分钟的秒数
            const int sPerHour = 60 * 60;//每小时的秒数

            string temp1 = "";//存储结果的临时变量
            Int32 second, minute = 0;
            int hour,day = 0;
            byte tmp_second, tmp_minute, tmp_hour, tmp_day = 0;//这个存放最终算出来的数据
            int tmp_ms;//存储最终的毫秒
            
            second = (Int32)(ms / msPerSec);//把毫秒转换为秒
            minute = second / sPerMinute;//把秒转换为分钟
            hour = second / sPerHour;//把分钟转换为小时
            day = (int)(second / (sPerHour * 24));//把小时转换为日

            tmp_ms = (int)(ms % 1000);if (ms >= 1000){ ms = 0;}//毫秒模1000放进tmp_ms，后面那句防溢出，下面以此类推
            tmp_second = (byte)(second % 60); if (tmp_second >= 60) { tmp_second = 0; }
            tmp_minute = (byte)(minute % 60); if (tmp_minute >= 60) { tmp_minute = 0; }
            tmp_hour = (byte)(hour % 24); if (tmp_hour >= 24) { tmp_hour = 0; }
            tmp_day = (byte)(day);

            if (formate == t_formate.full_day) //根据输入的时间格式整理日、月、小时、分钟、秒以及毫秒的显示
            {
                temp1 = tmp_day.ToString() + ":";
                if ((tmp_hour < 10) && full) { temp1 += "0"; }//不足10补零
                temp1 += tmp_hour.ToString() + ":";
                if ((tmp_minute < 10) && full) { temp1 += "0"; }
                temp1 += tmp_minute.ToString() + ":";
                if ((tmp_second < 10) && full) { temp1 += "0"; }
                temp1 += tmp_second.ToString();
            }

            if (formate == t_formate.full_hour)
            {
                temp1 += tmp_hour.ToString() + ":";
                if ((tmp_minute < 10) && full) { temp1 += "0"; }
                temp1 += tmp_minute.ToString() + ":";
                if ((tmp_second < 10) && full) { temp1 += "0"; }
                temp1 += tmp_second.ToString();
            }

            if (formate == t_formate.full_minute)
            {
                temp1 += tmp_minute.ToString() + ":";
                if ((tmp_second < 10) && full) { temp1 += "0"; }
                temp1 += tmp_second.ToString();
            }

            if (formate == t_formate.simple_second)
            {
                if ((second < 10) && full) { temp1 = "0"; }
                temp1 += minute.ToString();
                if (ms < 1000) { temp1 += "0"; }//位不足补零
                else if (ms < 100) { temp1 += "00"; }
                else if (ms < 10) { temp1 += "000"; }
                temp1 += "." + tmp_ms.ToString();
            }

            switch (formate)//只输出单独的各个数字
            {
                case t_formate.day: return tmp_day.ToString();
                case t_formate.hour: return tmp_hour.ToString();
                case t_formate.minute: return tmp_minute.ToString();
                case t_formate.second: return tmp_second.ToString();
                case t_formate.ms: return (tmp_ms).ToString();
            }
            return temp1;//返回处理好的格式化时间
        }

    }
}
