using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Un4seen.Bass;
using System.Windows.Forms;


namespace MiniPlayerClassic
{
    public class PlayerAgency
    {
        #region const
        const int default_device = -1;
        const int default_rate = 44100;

        const int Player_Playing = 1;
        const int Player_Paused = 2;
        const int Player_Stoped = 0;
        #endregion

        #region values
        public String FilePath = ""; //File Path
        public int ErrorCode = 1; //Any matter comeout while initialization will recored here.
        public int PlayState = Player_Stoped; //Recored playing state
        public float Volume = 1;
        //public values end
        private BASS_INFO BassInfo;
        private int theStream = 0;//The File Stream
        //private values end
        #endregion

        //progresses

        /* If you want to close the splash of Bass.Net you need to regist at 
        *  www.un4seen.com and input the registration code.
        */
        public void BassReg()
        {
            //BassNet.Registration("your_email","your_code");
        }

        //Input no pamaraters will use default configuration
        public PlayerAgency() 
        {
            BassReg();
            if (Bass.BASS_Init(default_device, default_rate, BASSInit.BASS_DEVICE_LATENCY, IntPtr.Zero))
            {
                BassInfo = new BASS_INFO();
                ErrorCode = 0;
            }
        }

        //Use custom configuration
        public PlayerAgency(int device,int rate)
        {
            BassReg();
            
            if ( Bass.BASS_Init(device,rate, BASSInit.BASS_DEVICE_LATENCY , IntPtr.Zero) ) //Bass initialization
            {
                BassInfo = new BASS_INFO();
                ErrorCode = 0;
            }
        }

        //Get Bass info in text
        public string AgencyTextInfo() 
        {
            Bass.BASS_GetInfo(BassInfo);
            string info = BassInfo.ToString();
            return info;
        }

        //Get BassInfo object's info
        public BASS_INFO AgencyInfo() 
        {
            Bass.BASS_GetInfo(BassInfo);
            return BassInfo;
        }

        //File load
        public Boolean LoadFile(string Filename) 
        {
            Bass.BASS_StreamFree(theStream); //free the stream
            theStream = Bass.BASS_StreamCreateFile(Filename,0L,0L,BASSFlag.BASS_DEFAULT);
            if (theStream == 0)
            { return false; } else { SetVolume(Volume); }
            FilePath = Filename;
            return true;
        }

        //Play Stream
        public Boolean Play()
        {
            if (PlayState == Player_Stoped) 
            { 
                LoadFile(FilePath);
            }
            if (Bass.BASS_ChannelPlay(theStream, false)) 
            { 
                PlayState = Player_Playing;
                return true;
            }
            else { return false; }
        }

        //Pause Stream
        public Boolean Pause()
        {
            if (Bass.BASS_ChannelPause(theStream)) { PlayState = Player_Paused; return true; }
            return false;
        }

        //Stop Stream, then clean the stream and free the file
        public Boolean Stop()
        {
            if (Bass.BASS_ChannelStop(theStream) && Bass.BASS_StreamFree(theStream))
            { PlayState = Player_Stoped; return true; }
            return false;
        }

        //Set Channel's Volume
        public Boolean SetVolume(float vol) 
        {
            Volume = vol;
            if (Bass.BASS_ChannelSetAttribute(theStream,BASSAttribute.BASS_ATTRIB_VOL,vol))
            { return true; }
            return false;
        }

        //Get Channel's Vloume
        public float GetValue()
        { 
            float vol = 0;
            if (Bass.BASS_ChannelGetAttribute(theStream, BASSAttribute.BASS_ATTRIB_VOL,ref vol))
            {
                return vol; 
            } 
            else 
            { 
                return 0; 
            }
        }

        //Get Channel's Level
        public Boolean GetLevel(ref int Left, ref int Right)
        {
            Int32 temp = Bass.BASS_ChannelGetLevel(theStream);
            if (temp == -1) { return false; }
            Left = Utils.LowWord32(temp);
            Right = Utils.HighWord32(temp);
            return true;
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
            if (temp == -1) {  return 0; }
            return Bass.BASS_ChannelBytes2Seconds(theStream, temp);
        }

        //Get the Channel's length
        public double GetLength()
        {
            long temp;
            temp = Bass.BASS_ChannelGetLength(theStream);
            if (temp == -1) { return 0; }
            return Bass.BASS_ChannelBytes2Seconds(theStream, temp);
        }
    }
}
