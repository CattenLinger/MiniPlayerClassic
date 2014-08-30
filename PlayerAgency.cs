using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Un4seen.Bass;
using System.Windows.Forms;

namespace MiniPlayerClassic
{
    class PlayerAgency
    {
        //const
        const int default_device = -1;
        const int default_rate = 44100;

        const int error_initerror = 1;
        const int error_fileopen = 2;

        const int Player_Playing = 1;
        const int Player_Paused = 2;
        const int Player_Stoped = 0;
        //const end

        //values
        public String FilePath;//File Path
        public int ErrorCode;//Any matter comeout while initialization will recored here.
        public int PlayState;
        //public values end
        private BASS_INFO BassInfo;
        private int theStream;
        //private values end
        //end values
        
        //progresses
        public PlayerAgency(int device,int rate)//Player object initialization
        {
            ErrorCode = 0;
            PlayState = Player_Stoped;

            BassInfo = new BASS_INFO();
            if ((device == 0) || (rate == 0)) //use 0 in the construction progress will use defaule device and rate
            {
                device = default_device;
                rate = default_rate;
            }
            if (Bass.BASS_Init(device, rate, BASSInit.BASS_DEVICE_LATENCY, IntPtr.Zero))
            {
                ErrorCode = 1;
            }
        }
        public string AgencyInfo()
        {
            Bass.BASS_GetInfo(BassInfo);
            string info = BassInfo.ToString();
            return info;
        }
        public void LoadFile(string Filename) //File load
        {
            if (theStream != 0) { Bass.BASS_StreamFree(theStream); }//free the stream if it not empty.
            theStream = Bass.BASS_StreamCreateFile(Filename,0L,0L,BASSFlag.BASS_DEFAULT);
            if (theStream == 0) { ErrorCode = 2; }
            FilePath = Filename;
        }
        public void Play()//Play Stream 
        {
            if (theStream == 0) 
            { 
                LoadFile(FilePath);
                if (ErrorCode == error_fileopen) { return; }
                Bass.BASS_ChannelPlay(theStream, false);
                PlayState = Player_Playing;
            }
            else
            { 
                Bass.BASS_ChannelPlay(theStream, false);
                PlayState = Player_Playing;
            }
        }
        public void Pause()//Pause Stream
        {
            if (theStream != 0) 
            { 
                Bass.BASS_ChannelPause(theStream);
                PlayState = Player_Paused;
            }
        }
        public void Stop()//Stop Stream, then clean the stream and free the file
        {
            if (theStream == 0) { return; }
            if (PlayState != 0) { Bass.BASS_ChannelStop(theStream); }
            Bass.BASS_StreamFree(theStream);
            PlayState = Player_Stoped;
        }
    }
}
