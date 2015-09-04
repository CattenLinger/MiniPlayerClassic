using System;
using System.Collections.Generic;

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
        /// <summary>
        /// 设置播放器
        /// </summary>
        /// <param name="value">播放器</param>
        public void setPlayer(IPlayer value)
        {
            try
            {
                player.TrackStateChanged -= Player_TrackStateChanged;
            }
            catch
            {
                Console.WriteLine("Controller: Event “Player_TrackStateChanged” isn't at the Player.");
            }
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

        /// <summary>
        /// 播放状态改变事件
        /// </summary>
        /// <param name="sender">发送消息的对象</param>
        /// <param name="e">消息内容</param>
        private void Player_TrackStateChanged(object sender, TrackStateChange e)
        {
            if(PlayBackActived && e.Message == TrackStates.Stoped)
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
        /// <summary>
        /// 播放一个链表内的项目
        /// </summary>
        /// <param name="item">链表项目</param>
        /// <returns>播放成功为true否则false</returns>
        public bool PlayItem(LinkedListNode<PlayListItem> item)
        {
            if(player == null)
            {
                throw new Exception("No Player");
            }
            playHead = item;
            playList = (PlayList)playHead.List;//强行转换成子类。即便返回的是父类，也还是会保留子类的信息。
            return player.LoadFile(item.Value.FileAddress) && player.Play();
        }

        //private LinkedList<PlayListItem> playList = null;
        private PlayList playList = null;
        private LinkedListNode<PlayListItem> playHead = null;
        public PlayList List
        {
            get
            {
                return playList;
            }
        }
        /// <summary>
        /// 让播放器播放下一首歌
        /// </summary>
        /// <returns></returns>
        public bool NextSong()
        {
            if(playList == null)
            {
                return false;
            }
            LinkedList<PlayListItem> List = playHead.List;
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
        /// <summary>
        /// 让播放器播放上一首歌
        /// </summary>
        /// <returns></returns>
        public bool PrevSong()
        {
            if (playList == null)
            {
                return false;
            }

            LinkedList<PlayListItem> List = playHead.List;
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

        #region IBasicPlayControl接口实现，用于代理播放器的基本播放操作
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
        #endregion

        private bool playbackFlag = false;
        public bool PlayBackActived
        {
            get
            {
                return playbackFlag;
            }

            set
            {
                if(playHead != null) playbackFlag = value;
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
