using System;
using System.Drawing;

namespace MiniPlayerClassic.Core
{
    /// <summary>
    /// 频谱图像产生器接口
    /// </summary>
    public interface IVisualEffects
    {
        void GetWaveForm(int width, int height);
        void getData(ref float[] data);
        void GetLevel(ref int Left, ref int Right);

        Bitmap WaveForm { get; }

        event EventHandler WaveFormFinished;
    }
}
