using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MiniPlayerClassic
{
    public partial class MainFrom : Form
    {
        public MainFrom()
        {
            InitializeComponent();
        }

        private void MainFrom_Load(object sender, EventArgs e)
        {
            PlayerAgency Agency1 = new PlayerAgency(0, 0);
            if ( Agency1.ErrorCode == 1 )
            {
                MessageBox.Show(Agency1.AgencyInfo());
            }
        }
    }
}
