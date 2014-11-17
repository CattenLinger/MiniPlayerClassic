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
        public String pb_text;//text on the bar
        public String pb_text2;

        public int pb_value;//Progress Bar's value
        public int pb_maxvalue;//Progress Bar's max value
        private int x_label1 = 0;
        private int x_label2 = 0;
        //sizes
        public int height, width;
        private int s_h_middle = 0;
        private int pb_f_long = 0;

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
            const int thumb_text_offset = 4;
            const int label_text_interval = 50;
            const int label_text_movestep = 2;

            bool rollflage = false;
            //sizes
            if (pb_value >= pb_maxvalue) { pb_value = pb_maxvalue; }
            if (pb_value <= 0) { pb_value = 0; }
            s_h_middle = height / 2;
            pb_f_long = (int)((float)width * ((float)pb_value / (float)pb_maxvalue));

            rect_pb.Width = width;
            rect_pb.Height = height;
            rect_fore.Width = pb_f_long;

            //set points
            Size info_text1 = TextRenderer.MeasureText(pb_text, font_labels);
            Size info_text2 = TextRenderer.MeasureText(pb_text2, font_labels);

            //point
            Point point_label1 = new Point(0, 0);
            Point point_label2 = new Point(0, 0);
            //rolling text
            if (info_text1.Width > (width - 2))
            { x_label1 = x_label1 - label_text_movestep; rollflage = true; }
            else { x_label1 = 0; rollflage = false; }
            if (x_label1 <= (-info_text1.Width - label_text_interval)) { x_label1 = 0; }
            //thumb text
            byte temp1 = (byte)(info_text2.Width / 2 - thumb_text_offset);
            if (pb_f_long < temp1) { x_label2 = 0; }
            if (pb_f_long >= temp1) { x_label2 = pb_f_long - temp1; }
            if ((pb_f_long + temp1) >= width) { x_label2 = width - temp1*2; }

            point_label1.X = x_label1;
            point_label1.Y = (s_h_middle + 4 - info_text1.Height) / 2;
            point_label2.X = x_label2;
            point_label2.Y = s_h_middle + (s_h_middle + 4 - info_text2.Height) / 2;

            //Fill colors
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
