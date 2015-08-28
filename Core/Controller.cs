using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MiniPlayerClassic.Core;

namespace MiniPlayerClassic.Core
{
    /// <summary>
    /// 协调播放器、播放列表和界面的中介类
    /// </summary>
    public class Controller : IBasicPlayControl
    {
        public static Controller contorller = new Controller();

        public static Controller getController()
        {
            return contorller;
        }

        public void setPlayer(IPlayer value)
        {
            player = value;
            player.TrackStateChanged += Player_TrackStateChanged;
        }

        private IPlayer player = null;
        public IPlayer Player
        {
            get
            {
                return player;
            }
        }

        private void Player_TrackStateChanged(object sender, TrackStateChange e)
        {
            if(PlayBackActived)
            {
                switch (playBackMode)
                {
                    case playbackHeadMode.ByIndex:
                        NextSong();
                        break;
                    case playbackHeadMode.List_Cycling:
                        if(playHead.Next == null)
                        {
                            playHead = playHead.List.First;
                            PlayItem(playHead);
                        }
                        else
                        {
                            NextSong();
                        }
                        break;
                    case playbackHeadMode.Shuffle:
                        break;
                    case playbackHeadMode.Single: //单曲播放一次跟关掉playback有什么区别_(:3」∠)_
                        PlayBackActived = false;
                        break;
                    case playbackHeadMode.Single_Cycling:
                        PlayItem(playHead);
                        break;
                }
            }
        }

        public bool PlayItem(LinkedListNode<PlayListItem> item)
        {
            if(player == null)
            {
                throw new Exception("No Player");
            }
            if (playHead == null) playHead = item;
            return player.LoadFile(item.Value.FileAddress) && player.Play();
        }

        private PlayList playList = null;
        private LinkedListNode<PlayListItem> playHead = null;

        public void setPlayList(PlayList list)
        {
            playList = list;
        }

        public PlayList List
        {
            get
            {
                return playList;
            }
        }

        public bool NextSong()
        {
            if(playList == null)
            {
                return false;
            }

            playHead = playHead.Next;
            if(playHead == null)
            {
                if(playBackMode == playbackHeadMode.List_Cycling)
                {
                    playHead = List.First;
                }
                else
                {

                    playbackFlag = false;
                    player.Stop();
                    return false;
                }
            }
            return PlayItem(playHead);
        }

        public bool PrevSong()
        {
            if (playList == null)
            {
                return false;
            }

            playHead = playHead.Previous;
            if(playHead == null)
            {
                if(playBackMode == playbackHeadMode.List_Cycling)
                {
                    playHead = List.Last;
                }
                else
                {
                    playbackFlag = false;
                    player.Stop();
                    return false;
                }
            }
            return PlayItem(playHead);
        }

        public bool Play()
        {
            return player.Play();
        }

        public bool Pause()
        {
            return player.Pause();
        }

        public bool Stop()
        {
            return player.Stop();
        }

        private bool playbackFlag = false;
        public bool PlayBackActived
        {
            get
            {
                return playbackFlag;
            }

            set
            {
                playbackFlag = value;
            }
        }

        playbackHeadMode playBackMode = playbackHeadMode.ByIndex;
        public playbackHeadMode PlayBackMode
        {
            get
            {
                return playBackMode;
            }

            set
            {
                playBackMode = value;
            }
        }

        private Controller()
        {

        }

    }
}
