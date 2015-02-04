using System;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace MiniPlayerClassic
{
    public class c_ProgressBar
    {
        //常量
        const int s_font = 10;
        const int thumb_text_offset = 5;//文本的偏移值
        const int label_text_interval = 50;
        const int label_text_movestep = 2;
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

        private StringBuilder title;
        private StringBuilder subtitle;

        public int pb_value;//进度条的进度值
        public int pb_maxvalue;//进度条的最大值

        private int x_label1 = 0;//上方文本的x坐标
        private int x_label2 = 0;//下方文本的x坐标
        //尺寸
        public int height, width;
        private int s_h_middle = 0;//中间分界条的位置
        private int pb_f_long = 0;//储存进度条的实际绘图长度

        Point point_title;
        Point point_subtitle;

        Size size_title;
        Size size_subtitle;

        bool rollflage = false;//上方文本的滚动标志

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

            point_title = new Point(0, 0);
            point_subtitle = new Point(0, 0);

            title = new StringBuilder();
            subtitle = new StringBuilder();

            size_title = new Size(0, 0);
            size_subtitle = new Size(0, 0);
        }

        public void ChangeTitle(string s)//改变标题文字
        {
            title.Clear();
            title.Append(s);
            size_title = TextRenderer.MeasureText(title.ToString(), font_labels);
        }

        private bool title_too_width()
        {
            if (size_title.Width > (width - 2))
                rollflage = true;
            else
                rollflage = false;

            return rollflage;
        }

        private void subtitle_style1()
        {
            const int ms_per_hour = 1000 * 60 * 60;
            const int ms_per_minute = 1000 * 60;

            int left_hour = pb_value / ms_per_hour;
            bool show_hour = left_hour > 0;
            int all_hour = pb_maxvalue / ms_per_hour;

            int min,sec,min2,sec2;
            subtitle.Clear();
            min = pb_value % ms_per_hour / ms_per_minute;
            sec = pb_value % ms_per_minute / 1000;
            min2 = pb_maxvalue % ms_per_hour / ms_per_minute;
            sec2 = pb_maxvalue % ms_per_minute / 1000;

            if(show_hour)
                subtitle.AppendFormat("{0:D2}:{1:D2}:{2:D2}|{3:D2}:{4:D2}:{5:D2}",left_hour,min,sec,all_hour,min2,sec2);
            else
                subtitle.AppendFormat("{0:D2}:{1:D2}|{2:D2}:{3:D2}",min, sec, min2, sec2);

        }

        public void DrawBar(Graphics e)//画图函数
        {
            
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
            size_subtitle = TextRenderer.MeasureText(subtitle.ToString(), font_labels);

            //设置文本的坐标
            point_title.X = 0; point_title.Y = 0;
            point_subtitle.X = 0; point_subtitle.Y = 0;
            //文本滚动的设定
            if (title_too_width())//如果文本长度越界
                x_label1 = x_label1 - label_text_movestep;//滚动
            else 
                x_label1 = 0;
            if (x_label1 <= (-size_title.Width - label_text_interval)) { x_label1 = 0; }
            //下方标签文字的位置计算
            subtitle_style1();
            byte temp1 = (byte)(size_subtitle.Width / 2 - thumb_text_offset);
            if (pb_f_long < temp1) { x_label2 = 0; }
            if (pb_f_long >= temp1) { x_label2 = pb_f_long - temp1; }
            if ((pb_f_long + temp1) >= width) { x_label2 = width - temp1*2; }

            point_title.X = x_label1;
            point_title.Y = (s_h_middle + 4 - size_title.Height) / 2;
            point_subtitle.X = x_label2;
            point_subtitle.Y = s_h_middle + (s_h_middle + 4 - size_subtitle.Height) / 2;

            //给矩形上色
            bitmap_enter.FillRectangle(b_back, rect_pb);
            bitmap_enter.FillRectangle(b_fore, rect_fore);

            //labels
            if (rollflage)
            {
                bitmap_enter.DrawString(title.ToString(), font_labels, b_text, point_title);
                point_title.X = point_title.X + size_title.Width + label_text_interval ;
                bitmap_enter.DrawString(title.ToString(), font_labels, b_text, point_title);
            } 
            else 
            {
                bitmap_enter.DrawString(title.ToString(), font_labels, b_text, point_title);
            }

            bitmap_enter.DrawString(subtitle.ToString(), font_labels, b_text, point_subtitle);
            
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
