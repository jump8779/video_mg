﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Video_mg
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void ToolStripButton_Lent_Click(object sender, EventArgs e)
        {
            LentMg lmg = new LentMg();
            lmg.MdiParent = this;
            lmg.Show();
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
