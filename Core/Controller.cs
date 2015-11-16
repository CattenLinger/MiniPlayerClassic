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
                //尝试删除旧的播放器事件
                player.TrackStateChanged -= Player_TrackStateChanged;
            }
            catch
            {
                //如果事件不存在，在控制台报告一下
                Console.WriteLine("Controller: Event “Player_TrackStateChanged” isn't at the Player.");
            }
            //获取播放器对象并设置好事件
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
            //列表回放的控制程序
            if(PlayBackActived && e.Message == TrackStates.Stoped)
            {
                switch (playBackMode)
                {
                    case playbackHeadMode.ByIndex://顺序播放
                        NextSong();
                        break;
                    case playbackHeadMode.List_Cycling://列表循环
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
                    case playbackHeadMode.Shuffle://TODO 随机
                        break;
                    case playbackHeadMode.Single: //单曲播放一次跟关掉playback有什么区别_(:3」∠)_
                        PlayBackActived = false;
                        break;
                    case playbackHeadMode.Single_Cycling://单曲循环
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
        //播放列表
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
        //回放标志位
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
        //回放模式
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
        //单实例模式需要，不能在外部访问这个构造器
        private Controller()
        {

        }

    }
}
