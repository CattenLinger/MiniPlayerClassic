using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MiniPlayerClassic
{
    public class c_ProgressBar
    {
        //Canvas
        private Bitmap buffer;
        private Bitmap canvas;

        public Graphics draw;
        public Graphics bitmap_enter;
        //pens
        private Pen p_frame;
        //brushs
        SolidBrush b_back;
        SolidBrush b_fore;
        //recs
        Rectangle rect_pb;
        Rectangle rect_fore;
        //values
        public string pb_text;//text on the bar
        public int pb_value;//Progress Bar's value
        public int pb_maxvalue;//Progress Bar's max value
        //sizes
        public int height, width;
        private int s_h_middle;
        private int pb_f_long;

        public void RefreshSettings()
        { 
        
        }

        public c_ProgressBar(int w,int h)
        {
            height = h - 1;
            width = w - 1;
            //graphics
            buffer = new Bitmap(w, h);
            canvas = new Bitmap(w, h);
            draw = Graphics.FromImage(canvas);
            bitmap_enter = Graphics.FromImage(buffer);
            //pens
            p_frame = new Pen(Color.Black, 1);
            //brush
            b_back = new SolidBrush(Color.Gray);
            b_fore = new SolidBrush(Color.Yellow);
            //rects
            rect_pb = new Rectangle(0, 0, width, height);
            rect_fore = new Rectangle(0, 0, 0, height);
        }
        public void DrawBar(Graphics e)
        {
            //sizes
            s_h_middle = height / 2;
            pb_f_long = (int)((float)width * ((float)pb_value / (float)pb_maxvalue));
            
            rect_pb.Width = width;
            rect_pb.Height = height;
            rect_fore.Width = pb_f_long;
            //Draw
            bitmap_enter.FillRectangle(b_back, rect_pb);
            bitmap_enter.FillRectangle(b_fore, rect_fore);
            bitmap_enter.DrawRectangle(p_frame, rect_fore);
            bitmap_enter.DrawRectangle(p_frame, rect_pb);
            //Draw on the graphic "e"
            e.DrawImage(buffer,0,0);
        }
    }
}
