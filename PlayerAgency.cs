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
        //const
        const int default_device = -1;
        const int default_rate = 44100;

        const int error_initerror = 1;
        const int error_fileopen = 2;
        const int error_volume = 3;

        const int Player_Playing = 1;
        const int Player_Paused = 2;
        const int Player_Stoped = 0;
        //const end

        //values
        public String FilePath = "";//File Path
        public int ErrorCode = 1;//Any matter comeout while initialization will recored here.
        public int PlayState = Player_Stoped;
        public float Volume = 1;
        //public values end
        private BASS_INFO BassInfo;
        private int theStream = 0;
        //private values end
        //end values
        
        //progresses
        public PlayerAgency(int device,int rate)//Player object initialization
        {
         /* If you want to close the splash of Bass.Net you need to regist at 
         *  www.un4seen.com and input the registration code.
         */
         //BassNet.Registration("your_email","your_code");
            
            //ErrorCode = 1;
            //PlayState = Player_Stoped;
            int int1, int2;

            if ((device == 0) || (rate == 0)) //use 0 in the construction progress will use defaule device and rate
            {
                int1 = default_device;
                int2 = default_rate;
            }
            else
            {
                int1 = device;
                int2 = rate;
            }
            
            if ( Bass.BASS_Init(int1, int2, BASSInit.BASS_DEVICE_LATENCY , IntPtr.Zero) )
            {
                ErrorCode = 0;
                BassInfo = new BASS_INFO();
            }
        }
        public string AgencyTextInfo()
        {
            Bass.BASS_GetInfo(BassInfo);
            string info = BassInfo.ToString();
            return info;
        }
        public int AgencyCodeInfo()
        {
            Bass.BASS_GetInfo(BassInfo);
            return 1;
        }
        public void LoadFile(string Filename) //File load
        {
            Bass.BASS_StreamFree(theStream);//free the stream 
            theStream = Bass.BASS_StreamCreateFile(Filename,0L,0L,BASSFlag.BASS_DEFAULT);
            if (theStream == 0) { ErrorCode = error_fileopen; } else { ErrorCode = 0; }
            FilePath = Filename;
        }
        public void Play()//Play Stream
        {
            if (PlayState == Player_Stoped) 
            { 
                LoadFile(FilePath);
                if (ErrorCode == error_fileopen) { return; }
            }
            if (Bass.BASS_ChannelPlay(theStream, false)) 
            { 
                PlayState = Player_Playing;
                ErrorCode = 0;
            }
        }
        public void Pause()//Pause Stream
        {
            if (Bass.BASS_ChannelPause(theStream)) 
            { 
                PlayState = Player_Paused;
            }
        }
        public void Stop()//Stop Stream, then clean the stream and free the file
        {
            if (Bass.BASS_ChannelStop(theStream) && Bass.BASS_StreamFree(theStream))
            {
                PlayState = Player_Stoped;
            }
        }
        public void SetVolume(float vol) 
        {
            if (Bass.BASS_SetVolume(vol)) { Volume = vol; } else  { ErrorCode = error_volume; }
        }
    }
}
