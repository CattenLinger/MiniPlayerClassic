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
        //const
        public enum BarStyles
        { 
            middle_single = 0,
            updown_mutile = 1,
            up_1_down_2 = 2
        }
        public enum TextStyles
        { 
            Fixed = 0,
            Scroll = 1,
            Tag_L = 2,
            Tag_R = 3
        }

        //colors
        private Color cl_fore = Color.Yellow;
        private Color cl_back = Color.Gray;
        private Color cl_text = Color.Black;

        //values
        private int v_value = 0;
        private int v_maxvalue = 10;
        private string t_text1 = "";
        private string t_text2 = "";
        private string t_text3 = "";
        //styles
        private Boolean bar_separator = false;
        private BarStyles bar_style = BarStyles.middle_single;
        private TextStyles bar_textstyle = TextStyles.Fixed;

        #region Values

        [Browsable(true), Category("Styles")]
        public override Color ForeColor
        {
            get { return cl_fore; }
            set { cl_fore = value; }
        }

        [Browsable(true), Category("Styles")]
        public override Color BackColor
        {
            get { return cl_back; }
            set { cl_back = value; }
        }

        [Browsable(true), Category("Styles")]
        public Color TextColor
        {
            get { return cl_text; }
            set { cl_text = value; }
        }

        [Browsable(true), Category("Values")]
        public int Value
        {
            get { return v_value; }
            set { v_value = value; }
        }

        [Browsable(true), Category("Values")]
        public int MaxValue
        {
            get { return v_maxvalue; }
            set { v_maxvalue = value; }
        }

        [Browsable(true), Category("Styles")]
        public bool Separator
        {
            get { return bar_separator; }
            set { bar_separator = value; }
        }

        [Browsable(true), Category("Styles")]
        public BarStyles Style
        {
            get { return bar_style; }
            set { bar_style = value; }
        }

        [Browsable(true), Category("Styles")]
        public TextStyles TextStyle
        {
            get { return bar_textstyle; }
            set { bar_textstyle = value; }
        }
        
        [Browsable(true), Category("Values")]
        public string Label1
        {
            get { return t_text1; }
            set { t_text1 = value; }
        }
        
        [Browsable(true), Category("Values")]
        public string Label2
        {
            get { return t_text2; }
            set { t_text2 = value; }
        }

        [Browsable(true), Category("Values")]
        public string Label3
        {
            get { return t_text3; }
            set { t_text3 = value; }
        }

        #endregion

        public ClassicBar()
        {
            InitializeComponent();
        }

        private void ClassicBar_Load(object sender, EventArgs e)
        {

        }
    }
}
