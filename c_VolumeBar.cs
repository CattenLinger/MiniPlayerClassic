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
        //const
        const int s_font = 10;
        const int pick_fall_step = 3;

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
        SolidBrush b_text;
        SolidBrush b_level;
        //fonts
        Font font_label;
        //recs
        Rectangle rect_pb;
        Rectangle rect_fore;
        //values
        public string pb_text;//text on the bar

        public int pb_value;//Progress Bar's value
        public int pb_maxvalue;//Progress Bar's max value
        private int x_label = 0;

        private int level_left = 0;//level
        private int level_right = 0;
        int temp_left = 0;
        int temp_right = 0;
        //sizes
        public int height, width;
        private int pb_f_long = 0;

        public c_VolumeBar(int w,int h)
        {
            height = h - 1;
            width = w - 1;

            //graphics
            buffer = new Bitmap(w, h);
            canvas = new Bitmap(w - 2, h - 2);
            draw = Graphics.FromImage(canvas);
            bitmap_enter = Graphics.FromImage(buffer);
            //pens
            p_frame = new Pen(Color.Black, 1);
            //brush
            b_back = new SolidBrush(Color.WhiteSmoke);
            b_fore = new SolidBrush(Color.LightBlue);
            b_text = new SolidBrush(Color.Black);
            b_level = new SolidBrush(Color.LightGray);
            
            //rects
            rect_pb = new Rectangle(0, 0, width, height);
            rect_fore = new Rectangle(0, 0, 0, height);
            //fonts
            font_label = new Font("MS YaHei UI",s_font);

            
        }

        public void tellitlevel(int left, int right)
        { 
            level_left = (int)((float)width * ((float)left) / ((float)Int16.MaxValue));
            level_right = (int)((float)width * ((float)right) / ((float)Int16.MaxValue)); 
        }

        public void DrawBar(Graphics e)
        {
            const int thumb_text_offset = 4;

            //sizes
            if (pb_value >= pb_maxvalue) { pb_value = pb_maxvalue; }
            if (pb_value <= 0) { pb_value = 0; }
            //how long the progress show
            pb_f_long = (int)((float)width * ((float)pb_value / (float)pb_maxvalue));

            rect_pb.Width = width;
            rect_pb.Height = height;
            rect_fore.Width = pb_f_long;
            //get size of the label text
            Size info_text = TextRenderer.MeasureText(pb_text, font_label);

            //point
            Point point_label = new Point(0,0);

            //set points
            //level
            if (temp_left < level_left) { temp_left = level_left; }
            if (temp_right < level_right) { temp_right = level_right; }
            if (temp_left > level_left) { temp_left -= pick_fall_step; }
            if (temp_right > level_right) { temp_right -= pick_fall_step; }
            //thumb text
            byte temp1 = (byte)(info_text.Width - thumb_text_offset);
            if (pb_f_long < temp1) { x_label = 0; }
            if (pb_f_long >= temp1) { x_label = pb_f_long - temp1; }

            point_label.X = x_label;
            point_label.Y = (height + 4 - info_text.Height) / 2;

            //Fill colors
            bitmap_enter.FillRectangle(b_back, rect_pb);
            //level
            b_level.Color = Color.LightGray;
            bitmap_enter.FillRectangle(b_level, 0, 0, temp_left, height / 2);
            bitmap_enter.FillRectangle(b_level, 0, height / 2, temp_right, height / 2);
            bitmap_enter.FillRectangle(b_fore, rect_fore);
            int t_t_l_left = temp_left;
            if (t_t_l_left > pb_f_long) { t_t_l_left = pb_f_long; }
            int t_t_l_right = temp_right;
            if (t_t_l_right > pb_f_long) { t_t_l_right = pb_f_long; }
            b_level.Color = Color.Lime;
            bitmap_enter.FillRectangle(b_level, 0, 0, t_t_l_left, height / 2);
            bitmap_enter.FillRectangle(b_level, 0, height / 2, t_t_l_right, height / 2);
            //label
            bitmap_enter.DrawString(pb_text,font_label,b_text,point_label);
            
            //Draw the Frame
            bitmap_enter.DrawRectangle(p_frame, rect_fore);
            bitmap_enter.DrawRectangle(p_frame, rect_pb);

            //Draw on the graphic "e"
            e.DrawImage(buffer,0,0);
        }
    }
}
