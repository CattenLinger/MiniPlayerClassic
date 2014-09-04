using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MiniPlayerClassic
{
    public class c_ProgressBar
    {
        //const
        const int s_font = 10;
        //Canvas
        private Bitmap buffer;
        private Bitmap canvas;

        public Graphics draw;
        public Graphics bitmap_enter;
        //pens
        private Pen p_frame;
        private Pen p_middleline;
        //brushs
        SolidBrush b_back;
        SolidBrush b_fore;
        SolidBrush b_text;
        //fonts
        Font font_labels;
        //recs
        Rectangle rect_pb;
        Rectangle rect_fore;
        //values
        public string pb_text;//text on the bar
        
        public int pb_currenttime;//Time on the bar
        public int pb_alltime;//

        public int pb_value;//Progress Bar's value
        public int pb_maxvalue;//Progress Bar's max value
        private int x_label1 = 0;
        private int x_label2 = 0;
        //sizes
        public int height, width;
        private int s_h_middle = 0;
        private int pb_f_long = 0;

        public void trans_Time()
        { 
        
        }

        public c_ProgressBar(int w,int h)
        {
            height = h - 1;
            width = w - 1;
            x_label1 = 0;
            //graphics
            buffer = new Bitmap(w, h);
            canvas = new Bitmap(w, h);
            draw = Graphics.FromImage(canvas);
            bitmap_enter = Graphics.FromImage(buffer);
            //pens
            p_frame = new Pen(Color.Black, 1);
            p_middleline = new Pen(Color.Black,1);
            p_middleline.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            //brush
            b_back = new SolidBrush(Color.WhiteSmoke);
            b_fore = new SolidBrush(Color.Yellow);
            b_text = new SolidBrush(Color.Black);
            
            //rects
            rect_pb = new Rectangle(0, 0, width, height);
            rect_fore = new Rectangle(0, 0, 0, height);
            //fonts
            font_labels = new Font("MS YaHei UI",s_font);

            
        }
        public void DrawBar(Graphics e)
        {
            //sizes
            s_h_middle = height / 2;
            pb_f_long = (int)((float)width * ((float)pb_value / (float)pb_maxvalue));

            rect_pb.Width = width;
            rect_pb.Height = height;
            rect_fore.Width = pb_f_long;

            //point

            Point point_label1 = new Point(0,0);
            Point point_label2 = new Point(0,0);
            //set points
            point_label1.X = x_label1;
            point_label1.Y = (s_h_middle - s_font) / 2 - 1;
            point_label2.X = x_label2;
            point_label2.Y = s_h_middle + (point_label1.Y);
            //Fill colors
            bitmap_enter.FillRectangle(b_back, rect_pb);
            bitmap_enter.FillRectangle(b_fore, rect_fore);
            
            bitmap_enter.DrawString(pb_text,font_labels,b_text,point_label1);
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
