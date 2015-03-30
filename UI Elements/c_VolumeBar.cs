using System;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace MiniPlayerClassic
{
    public class c_VolumeBar
    {
        #region 常量
        
        const int s_font = 10;
        const float fft_data_zoom = 17f;
        const int fft_fall_step = 5;
        const int peak_fall_step = 1;
        const int level_fall_step = 6;
        const int thumb_text_offset = 4;//文本的x偏移

        #endregion
        //颜色
        Color clFrame = Color.LightSlateGray,
              clThumb = Color.SkyBlue,
              clForce = Color.FromArgb(150, Color.SkyBlue),
              clBackGround = Color.LightGray,
              clText = Color.Black,
              clFFT = Color.MediumSeaGreen,
              clBackgroundLevel = Color.DeepSkyBlue;

        private Bitmap buffer, canvas;

        public Graphics draw, bitmap_enter;

        Pen pen;

        SolidBrush brush;
        public Font TextFont { get { return font_label; } set { font_label = value; } }
        Font font_label;

        Rectangle rect_pb, rect_fore;

        private StringBuilder bar_label;

        private int pb_value, pb_maxvalue;
        public int Value
        {
            get { return pb_value; }
            set
            {
                pb_value = value;
                if (pb_value > pb_maxvalue) { pb_value = pb_maxvalue; }
                if (pb_value <= 0) { pb_value = 0; }
            }
        }

        public int MaxValue { get { return pb_maxvalue; } set { pb_maxvalue = value; } }

        private int label_x = 0;//标签的x坐标

        public Single[] fft_data = new Single[512];//绘制频谱的元数据的存储的地方
        private int level_left = 0, level_right = 0;
        private int temp_level_left = 0, temp_level_right = 0;
        private short[] fft = new short[512];
        private short[] peak = new short[512];

        public int height, width;

        private int pb_f_long = 0;//保存进度条实际绘图区域长度

        Point point_label;

        Size size_label;

        public c_VolumeBar(int w,int h)
        {
            height = h - 1;
            width = w - 1;

            buffer = new Bitmap(w, h);//缓冲
            canvas = new Bitmap(w - 2, h - 2);//主要绘图区
            draw = Graphics.FromImage(canvas);//把Graphic对象指向绘图区以便能够使用它们画图
            bitmap_enter = Graphics.FromImage(buffer);

            pen = new Pen(clText);
            brush = new SolidBrush(clText);

            rect_pb = new Rectangle(0, 0, width, height);
            rect_fore = new Rectangle(0, 0, 0, height);

            font_label = new Font("MS YaHei UI",s_font);
            point_label = new Point(0, 0);
            size_label = new Size(0, 0);

            bar_label = new StringBuilder();
        }

        public void ChangeLabel(string s)
        {
            bar_label.Clear();
            bar_label.Append(s);
            size_label = TextRenderer.MeasureText(bar_label.ToString(), font_label);
        }

        public void tellitlevel(int left, int right)//外部程序在此设置响度
        { 
            level_left = (int)((float)width * ((float)left) / ((float)Int16.MaxValue));
            level_right = (int)((float)width * ((float)right) / ((float)Int16.MaxValue)); 
        }

        public void DrawBar(Graphics e)//绘图
        {
            int i = 0;
            //对进度条的值进行限定
            
            //获得进度条的实际绘图区域长度
            pb_f_long = (int)((float)width * ((float)pb_value / (float)pb_maxvalue));
            if (pb_f_long <= 1) pb_f_long = 1;
            rect_pb.Width = width; rect_pb.Height = height;
            rect_fore.Width = pb_f_long;

            //文本标签坐标
            point_label.X = 0; point_label.Y = 0;

            //响度显示的高度的限定以及顶端的飘落
            if (temp_level_left < level_left) { temp_level_left = level_left; }
            if (temp_level_right < level_right) { temp_level_right = level_right; }
            if (temp_level_left > level_left) { temp_level_left -= level_fall_step; }
            if (temp_level_right > level_right) { temp_level_right -= level_fall_step; }

            //文本标签位置
            byte temp1 = (byte)(size_label.Width - thumb_text_offset);
            if (pb_f_long < temp1) { label_x = 0; }
            if (pb_f_long >= temp1) { label_x = pb_f_long - temp1; }

            point_label.X = label_x;
            point_label.Y = (height + 4 - size_label.Height) / 2;

            brush.Color = clBackGround;
            bitmap_enter.FillRectangle(brush, rect_pb);
            //绘制响度条,

            //设置灰色
            brush.Color = clBackgroundLevel;
            //计算并用矩形填充响度条的区域
            bitmap_enter.FillRectangle(brush, 0, 0, temp_level_left, height / 2);
            bitmap_enter.FillRectangle(brush, 0, height / 2, temp_level_right, height / 2);

            //频谱绘制
            for (i = 0; i <= 100; i++)//处理元数据
            {
                short di = Math.Abs((short)((float)height * fft_data[i] * fft_data_zoom));
                if (di > height) { di = (byte)height; }
                if (di < 1) { di = 1; }
                if (di > fft[i]) { fft[i] = di; }
                if (peak[i] < fft[i]) { peak[i] = fft[i]; }
                if (di <= fft[i]) { fft[i] -= fft_fall_step; }
                if (peak[i] > fft[i]) { peak[i] -= peak_fall_step; }
            }
            pen.Color = clFFT;
            brush.Color = clFFT;
            for (i = 0; i <= 100; i++)//画频谱柱
            {
                bitmap_enter.DrawLine(pen, i * 2, height - 1, i * 2, height - fft[i]);
                bitmap_enter.FillRectangle(brush, i * 2, height - peak[i] + 2, 1, 1);
            }//*/

            brush.Color = clForce;
            bitmap_enter.FillRectangle(brush, rect_fore);
            //Draw the Frame
            pen.Color = clThumb;
            bitmap_enter.DrawLine(pen,rect_fore.Width,1,rect_fore.Width,height - 1);
            if (rect_fore.Width > 1) { bitmap_enter.DrawLine(pen, rect_fore.Width + 1, 1, rect_fore.Width + 1, height - 1); }

            pen.Color = clFrame;
            bitmap_enter.DrawRectangle(pen, rect_pb);
            //label
            brush.Color = clText;
            bitmap_enter.DrawString(bar_label.ToString(), font_label, brush, point_label);//画文本标签到画布

            //Draw on the graphic "e"
            e.DrawImage(buffer,0,0);
        }
    }
}
