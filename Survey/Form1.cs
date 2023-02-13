using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

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
        public bool Check { get; set; } = false;
        private void Nametxtb_TextChanged(object sender, EventArgs e)
        {
            if (Nametxtb.Text.Length <= 0)
            {
                namechecklbl.Text = "Enter name!";
                namechecklbl.ForeColor = Color.Red;
            }
            else
            {
                namechecklbl.Text = "";
                Check = true;
            }
        }
        private void Surnametxtb_TextChanged(object sender, EventArgs e)
        {
            if (Surnametxtb.Text.Length <= 0)
            {
                surnamechecklbl.Text = "Enter surname!";
                surnamechecklbl.ForeColor = Color.Red;
            }
            else
            {
                surnamechecklbl.Text = "";
                Check = true;
            }
        }
        private void Emailtxtb_TextChanged(object sender, EventArgs e)
        {
            if (!Emailtxtb.Text.Contains("@gmail.com"))
            {
                emailchecklbl.Text = "Email is invalid!";
                emailchecklbl.ForeColor = Color.Red;
            }
            else
            {
                emailchecklbl.Text = "";
                Check = true;
            }
        }

        private void Addbtn_Click(object sender, EventArgs e)
        {

        }
    }
    public class Person
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string BirthDate { get; set; }
        public int MyProperty { get; set; }
    }
    public class FileHelper
    {
        public static void WriteJsonHuman(Person person)
        {
            var serializer = new JsonSerializer();
            using (var sw = new StreamWriter($"{person.Name}.json"))
            {
                using (var jw = new JsonTextWriter(sw))
                {
                    jw.Formatting = Newtonsoft.Json.Formatting.Indented;
                    serializer.Serialize(jw, person);
                }
            }
        }
    }
}
