using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace MiniPlayerClassic
{
    public class c_VolumeBar
    {
        //常量
        const int s_font = 10;
        const float fft_data_zoom = 16f;
        const int fft_fall_step = 4;
        const int peak_fall_step = 2;
        const int level_fall_step = 4;

        //画布
        private Bitmap buffer;
        private Bitmap canvas;

        public Graphics draw;
        public Graphics bitmap_enter;
        //画笔
        private Pen p_frame;
        public Pen p_fft;
        //画刷
        SolidBrush b_back;
        SolidBrush b_fore;
        SolidBrush b_text;
        SolidBrush b_level;
        //字体
        Font font_label;
        //recs
        Rectangle rect_pb;
        Rectangle rect_fore;
        //变量
        public string pb_text;//进度条上的文字标签
        public int pb_value;//进度条的进度值
        public int pb_maxvalue;//进度条的最大值
        private int x_label = 0;//标签的x坐标

        public Single[] fft_data = new Single[512];//绘制频谱的元数据的存储的地方
        private int level_left = 0;//频谱柱相关变量
        private int level_right = 0;
        private int temp_left = 0;
        private int temp_right = 0;
        private short[] temp_fft = new short[256];
        private short[] temp_peak = new short[256];
        //尺寸
        public int height, width;
        private int pb_f_long = 0;//保存进度条实际绘图区域长度
        Point point_label;

        public c_VolumeBar(int w,int h)
        {
            height = h - 1;//
            width = w - 1;

            //画布的初始化
            buffer = new Bitmap(w, h);//缓冲
            canvas = new Bitmap(w - 2, h - 2);//主要绘图区
            draw = Graphics.FromImage(canvas);//把Graphic对象指向绘图区以便能够使用它们画图
            bitmap_enter = Graphics.FromImage(buffer);
            //笔的初始化
            p_frame = new Pen(Color.Black, 1);//边框
            p_fft = new Pen(Color.Green, 1);//频谱
            //刷子初始化
            b_back = new SolidBrush(Color.WhiteSmoke);
            b_fore = new SolidBrush(Color.LightBlue);
            b_text = new SolidBrush(Color.Black);
            b_level = new SolidBrush(Color.LightGray);
            
            //矩形的尺寸
            rect_pb = new Rectangle(0, 0, width, height);
            rect_fore = new Rectangle(0, 0, 0, height);
            //fonts
            font_label = new Font("MS YaHei UI",s_font);
            point_label = new Point(0, 0);
        }

        public void tellitlevel(int left, int right)//外部程序在此设置响度
        { 
            level_left = (int)((float)width * ((float)left) / ((float)Int16.MaxValue));
            level_right = (int)((float)width * ((float)right) / ((float)Int16.MaxValue)); 
        }

        public void DrawBar(Graphics e)//绘图
        {
            const int thumb_text_offset = 4;//文本的x偏移

            //对进度条的值进行限定
            if (pb_value >= pb_maxvalue) { pb_value = pb_maxvalue; }
            if (pb_value <= 0) { pb_value = 0; }
            //获得进度条的实际绘图区域长度
            pb_f_long = (int)((float)width * ((float)pb_value / (float)pb_maxvalue));

            rect_pb.Width = width;
            rect_pb.Height = height;
            rect_fore.Width = pb_f_long;
            //获得文本标签的长度
            Size info_text = TextRenderer.MeasureText(pb_text, font_label);

            //定义文本标签的坐标
            point_label.X = 0; point_label.Y = 0;

            //响度显示的高度的限定以及顶端的飘落计算
            if (temp_left < level_left) { temp_left = level_left; }
            if (temp_right < level_right) { temp_right = level_right; }
            if (temp_left > level_left) { temp_left -= level_fall_step; }
            if (temp_right > level_right) { temp_right -= level_fall_step; }
            //标签文本位置的计算
            byte temp1 = (byte)(info_text.Width - thumb_text_offset);
            if (pb_f_long < temp1) { x_label = 0; }
            if (pb_f_long >= temp1) { x_label = pb_f_long - temp1; }

            point_label.X = x_label;
            point_label.Y = (height + 4 - info_text.Height) / 2;

            //填充颜色
            bitmap_enter.FillRectangle(b_back, rect_pb);
            //绘制响度条
            height++;
            //设置灰色
            b_level.Color = Color.LightGray;
            //计算并用矩形填充响度条（背景）的区域
            bitmap_enter.FillRectangle(b_level, 0, 0, temp_left, height / 2);
            bitmap_enter.FillRectangle(b_level, 0, height / 2, temp_right, height / 2);
            bitmap_enter.FillRectangle(b_fore, rect_fore);
            //设置响度条（前景）的长度规则
            int t_t_l_left = temp_left;
            if (t_t_l_left > pb_f_long) { t_t_l_left = pb_f_long; }
            int t_t_l_right = temp_right;
            if (t_t_l_right > pb_f_long) { t_t_l_right = pb_f_long; }
            //设置莱姆色，并绘制响度条
            b_level.Color = Color.Lime;
            bitmap_enter.FillRectangle(b_level, 0, 0, t_t_l_left, height / 2);
            bitmap_enter.FillRectangle(b_level, 0, height / 2, t_t_l_right, height / 2);
            //频谱绘制
            for (int i = 0; i <= 128; i++)//处理元数据
            {
                short di = Math.Abs((short)((float)height * fft_data[i * 2] * fft_data_zoom));
                if (di > height) { di = (byte)height; }
                if (di < 1) { di = 1; }
                if (di > temp_fft[i]) { temp_fft[i] = di; }
                if (temp_peak[i] < di) { temp_peak[i] = di; }
                if (di <= temp_fft[i]) { temp_fft[i] -= fft_fall_step; }
                if (temp_peak[i] > di) { temp_peak[i] -= peak_fall_step; }
            }
            for (int i = 0; i <= 100; i++)//画频谱柱
            {
                bitmap_enter.DrawLine(p_fft, i * 2, height - 1, i * 2, height - 1 - temp_fft[i]);
                bitmap_enter.FillRectangle(new SolidBrush(p_fft.Color), i * 2, height - 1 - temp_peak[i], 1, 1);
            }
            height--;
            //label
            bitmap_enter.DrawString(pb_text, font_label, b_text, point_label);//画文本标签到画布
            
            //Draw the Frame
            bitmap_enter.DrawRectangle(p_frame, rect_fore);
            bitmap_enter.DrawRectangle(p_frame, rect_pb);

            //Draw on the graphic "e"
            e.DrawImage(buffer,0,0);
        }
    }
}
