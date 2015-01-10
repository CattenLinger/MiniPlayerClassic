using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace MiniPlayerClassic
{
    public class c_ProgressBar
    {
        //常量
        const int s_font = 10;

        //画布
        private Bitmap buffer;
        private Bitmap canvas;

        public Graphics draw;
        public Graphics bitmap_enter;
        //画笔
        private Pen p_frame;
        private Pen p_middleline;
        //笔刷
        SolidBrush b_back;
        SolidBrush b_fore;
        SolidBrush b_text;
        //字体
        Font font_labels;
        //矩形尺寸
        Rectangle rect_pb;
        Rectangle rect_fore;
        //变量
        public String pb_text;//在进度条上方的文本
        public String pb_text2;//在进度条下方的文本

        private StringBuilder pb_title;
        private StringBuilder pb_subtitle;

        public int pb_value;//进度条的进度值
        public int pb_maxvalue;//进度条的最大值
        private int x_label1 = 0;//上方文本的x坐标
        private int x_label2 = 0;//下方文本的x坐标
        //尺寸
        public int height, width;
        private int s_h_middle = 0;//中间分界条的位置
        private int pb_f_long = 0;//储存进度条的实际绘图长度

        Point point_label1;
        Point point_label2;

        int width_title = 0;
        int width_subtitle = 0;

        public c_ProgressBar(int w,int h)
        {
            height = h - 1;
            width = w - 1;
            x_label1 = 0;
            //画布
            buffer = new Bitmap(w, h);
            canvas = new Bitmap(w, h);
            //把Graphic链接到画布以便画图
            draw = Graphics.FromImage(canvas);
            bitmap_enter = Graphics.FromImage(buffer);
            //笔刷
            p_frame = new Pen(Color.Black, 1);
            p_middleline = new Pen(Color.Black,1);
            p_middleline.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            //画刷
            b_back = new SolidBrush(Color.WhiteSmoke);
            b_fore = new SolidBrush(Color.Yellow);
            b_text = new SolidBrush(Color.Black);
            
            //矩形
            rect_pb = new Rectangle(0, 0, width, height);
            rect_fore = new Rectangle(0, 0, 0, height);
            //字体
            font_labels = new Font("MS YaHei UI",s_font);

            point_label1 = new Point(0, 0);
            point_label2 = new Point(0, 0);

            pb_title = new StringBuilder();
            pb_subtitle = new StringBuilder();

            
        }

        public void ChangeTitle(string s)//改变标题文字
        {
            pb_title.Clear();
            pb_title.Append(s);
            width_title = TextRenderer.MeasureText(pb_text.ToString(), font_labels).Width;
        }

        public void DrawBar(Graphics e)//画图函数
        {
            const int thumb_text_offset = 4;//文本的偏移值
            const int label_text_interval = 50;
            const int label_text_movestep = 2;

            bool rollflage = false;//上方文本的滚动标志
            //进度条进度的规定
            if (pb_value >= pb_maxvalue) { pb_value = pb_maxvalue; }
            if (pb_value <= 0) { pb_value = 0; }
            s_h_middle = height / 2;
            //实际绘图区域的计算
            pb_f_long = (int)((float)width * ((float)pb_value / (float)pb_maxvalue));

            rect_pb.Width = width;
            rect_pb.Height = height;
            rect_fore.Width = pb_f_long;

            //获取文本的长度
            Size info_text1 = TextRenderer.MeasureText(pb_text, font_labels);
            Size info_text2 = TextRenderer.MeasureText(pb_text2, font_labels);

            //设置文本的坐标
            point_label1.X = 0; point_label1.Y = 0;
            point_label2.X = 0; point_label2.Y = 0;
            //文本滚动的设定
            if (info_text1.Width > (width - 2))//如果文本长度越界
            { x_label1 = x_label1 - label_text_movestep; rollflage = true; }//滚动
            else { x_label1 = 0; rollflage = false; }
            if (x_label1 <= (-info_text1.Width - label_text_interval)) { x_label1 = 0; }
            //下方标签文字的位置计算
            byte temp1 = (byte)(info_text2.Width / 2 - thumb_text_offset);
            if (pb_f_long < temp1) { x_label2 = 0; }
            if (pb_f_long >= temp1) { x_label2 = pb_f_long - temp1; }
            if ((pb_f_long + temp1) >= width) { x_label2 = width - temp1*2; }

            point_label1.X = x_label1;
            point_label1.Y = (s_h_middle + 4 - info_text1.Height) / 2;
            point_label2.X = x_label2;
            point_label2.Y = s_h_middle + (s_h_middle + 4 - info_text2.Height) / 2;

            //给矩形上色
            bitmap_enter.FillRectangle(b_back, rect_pb);
            bitmap_enter.FillRectangle(b_fore, rect_fore);

            //labels
            if (rollflage)
            {
                bitmap_enter.DrawString(pb_text.ToString(), font_labels, b_text, point_label1);
                point_label1.X = point_label1.X + info_text1.Width + label_text_interval ;
                bitmap_enter.DrawString(pb_text.ToString(), font_labels, b_text, point_label1);
            } 
            else 
            {
                bitmap_enter.DrawString(pb_text, font_labels, b_text, point_label1);
            }

            bitmap_enter.DrawString(pb_text2, font_labels, b_text, point_label2);
            
            //Draw the Frame, with a middle line
            bitmap_enter.DrawRectangle(p_frame, rect_fore);
            bitmap_enter.DrawLine(p_middleline, 0, s_h_middle, width, s_h_middle);
            bitmap_enter.DrawRectangle(p_frame, rect_pb);
            if (pb_f_long > 0 ){ bitmap_enter.DrawLine(p_frame,pb_f_long - 1,0,pb_f_long - 1,height);}

            //Draw on the graphic "e"
            e.DrawImage(buffer,0,0);
        }
    }
}
