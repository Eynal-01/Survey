using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Survey
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle,
                                                                  Color.DimGray,
                                                                  Color.Black,
                                                                  90F))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void SurveygroupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
