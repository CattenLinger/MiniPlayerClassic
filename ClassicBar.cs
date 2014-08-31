using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MiniPlayerClassic
{
    public partial class ClassicBar : UserControl
    {
        Graphics BarFrame;
        Graphics BarBG;
        Graphics BarInfo;

        Pen p_Frame;

        int c_hight;
        int c_width;

        public ClassicBar()
        {
            InitializeComponent();
        }

        public void ClassicBar_Load(object sender, EventArgs e)
        {
            p_Frame = new Pen(Color.Black, 1);
            c_hight = this.Size.Height;
            c_width = this.Size.Width;

        }

        private void ClassicBar_Paint(object sender, PaintEventArgs e)
        {
            BarBG = e.Graphics;
            
        }
    }
}
