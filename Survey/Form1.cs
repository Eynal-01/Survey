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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace Survey
{
    public partial class Form1 : Form
    {
        Person person = new Person();
        List<Person> persons = new List<Person>();
        List<Person> GetAllPersons()
        {
            List<Person> persons = new List<Person>();
            var files = Directory.GetFiles(".");
            foreach (var item in files)
            {
                if (item.EndsWith(".json"))
                {
                    var obj = JsonConvert.DeserializeObject<Person>(File.ReadAllText(item));
                    persons.Add(obj);
                }
            }
            return persons;
        }
        public Form1()
        {
            InitializeComponent();
        }
        FileHelper fileHelper = new FileHelper();
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

        private void Addbtn_Click(object sender, EventArgs e)
        {
            var locationChange = Changebtn.Location;
            Changebtn.Location = Addbtn.Location;
            Addbtn.Location = locationChange;
            this.BackColor = Color.Green;
            Person newperson = new Person
            {
                Name = Nametxtb.Text,
                Surname = Surnametxtb.Text,
                Email = Emailtxtb.Text,
                Phone=Phonemaskedtxtb.Text,
                BirthDate = BirthdatetimePicker.Text
            };
            UserListBox.Items.Add(newperson);
            textBox3.Text = person.Name + ".json";
            if (!textBox3.Text.Contains(".json"))
            {
                textBox3.Text = person.Name + ".json";
            }
            UserListBox.DisplayMember = nameof(Person.Name);
            FileHelper.WriteJsonHuman(newperson);
        }
        private void Savebtn_Click(object sender, EventArgs e)
        {
            FileHelper.WriteJsonHuman(person);
            //fileHelper.WriteListBox(humanslistBox.Items.ToString());           
            Nametxtb.Text = "";
            Surnametxtb.Text = "";
            Emailtxtb.Text = "";
            Phonemaskedtxtb.Text = "";
            BirthdatetimePicker.Text = "";
        }

        private void Loadbtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox3.Text == "")
                {
                    var persons = GetAllPersons();
                    UserListBox.Items.Clear();
                    UserListBox.Items.AddRange(persons.ToArray());
                    UserListBox.DisplayMember = nameof(Person.Name);
                }
                var path = Directory.GetCurrentDirectory() + "\\" + textBox3.Text;
                if (File.Exists(path) || File.Exists(path + ".json"))
                {
                    var locationChange = Changebtn.Location;
                    Changebtn.Location = Addbtn.Location;
                    Addbtn.Location = locationChange;
                }
                //if (textBox3.Text != "" && !textBox3.Text.Contains(".json"))
                //{
                //    textBox3.Text += ".json";
                //}
                if (File.Exists(textBox3.Text))
                {
                    person = fileHelper.ReadJsonHuman(textBox3.Text);
                    Nametxtb.Text = person.Name;
                    Surnametxtb.Text = person.Surname;
                    Emailtxtb.Text = person.Email;
                    Phonemaskedtxtb.Text = person.Phone;
                    BirthdatetimePicker.Text = person.BirthDate.ToString();
                }
            }
            catch
            {
            }
        }
        private void fgvreagvregregrewwg()
        {

        }
        
        private void UserListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var human = UserListBox.SelectedItem as Person;
            Nametxtb.Text = human.Name;
            Surnametxtb.Text = human.Surname;
            Emailtxtb.Text = human.Email;
            Phonemaskedtxtb.Text = human.Phone;
            BirthdatetimePicker.Text = human.BirthDate;
        }
        private void UserListBox_DoubleClick(object sender, EventArgs e)
        {
            var human = UserListBox.SelectedItem as Person;
            textBox3.Text = human.Name + ".json";
        }
    }
    public class Person
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string BirthDate { get; set; }
        public string Phone { get; set; }
        //public override string ToString()
        //{
        //    return $"{Name}-{Surname}-{Email}-{Phone}-{BirthDate}";
        //}
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
        public Person ReadJsonHuman(string text)
        {
            Person employers = null;
            var serializer1 = new JsonSerializer();
            using (var sr1 = new StreamReader(".json"))
            {
                using (var jr1 = new JsonTextReader(sr1))
                {
                    employers = serializer1.Deserialize<Person>(jr1);
                }
            }
            return employers;
        }
    }
}