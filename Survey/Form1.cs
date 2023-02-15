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
        List<Person> humans = new List<Person>();
        Person user = new Person();
        Person person = new Person();
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
            var newperson = new Person();
            var locationChange = Changebtn.Location;
            Changebtn.Location = Addbtn.Location;
            Addbtn.Location = locationChange;
            this.BackColor = Color.Green;

            newperson.Name = Nametxtb.Text;
            newperson.FileName = newperson.Name;
            newperson.Surname = Surnametxtb.Text;
            newperson.Email = Emailtxtb.Text;
            newperson.Phone = Phonemaskedtxtb.Text;
            newperson.BirthDate = BirthdatetimePicker.Text;


            UserListBox.DisplayMember = nameof(Person.Name);
            textBox3.Text = newperson.FileName;
            if (!UserListBox.Items.Equals(person.Id))
            {
                UserListBox.Items.Add(newperson);
            }

            if (!textBox3.Text.Contains(".json"))
            {
                textBox3.Text = newperson.FileName;
            }
        }
        private void Savebtn_Click(object sender, EventArgs e)
        {
            fileHelper.Write(person);
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
                if (textBox3.Text != "" && !textBox3.Text.Contains(".json"))
                {
                    textBox3.Text += ".json";
                }
                if (File.Exists(textBox3.Text))
                {
                    user = fileHelper.Read(textBox3.Text);
                    Nametxtb.Text = user.Name;
                    Surnametxtb.Text = user.Surname;
                    Emailtxtb.Text = user.Email;
                    Phonemaskedtxtb.Text = user.Phone;
                    BirthdatetimePicker.Text = user.BirthDate.ToString();
                }
            }
            catch
            {
            }
        }
        private void UserListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var human = UserListBox.SelectedItem as Person;
            textBox3.Text = human.FileName;
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
        private void Changebtn_Click(object sender, EventArgs e)
        {

            textBox3.Text = user.FileName + ".json";
            if (Nametxtb.Text != user.Name)
            {
                user.Name = Nametxtb.Text;
            }
            if (person.Name != user.Name)
            {
                person.FileName = user.FileName;
            }
            else
            {
                textBox3.Text = user.FileName + ".json";
            }
            if (Surnametxtb.Text != user.Surname)
            {
                user.Surname = Surnametxtb.Text;
            }
            if (Emailtxtb.Text != user.Email)
            {
                user.Email = Emailtxtb.Text;
            }
            if (Phonemaskedtxtb.Text != user.Phone)
            {
                user.Phone = Phonemaskedtxtb.Text;
            }
            if (BirthdatetimePicker.Text != user.BirthDate.ToString())
            {
                user.BirthDate = BirthdatetimePicker.Text;
            }
            person = user;
            fileHelper.Write(person);
        }
    }
    public class Person
    {
        public string Id { get; set; } = Guid.NewGuid().ToString().Substring(0, 8);
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string BirthDate { get; set; }
        public string Phone { get; set; }
        public string FileName { get; set; }
        public override string ToString()
        {
            return $"{Name}-{Surname}-{Email}-{Phone}-{BirthDate}";
        }
    }
    public class FileHelper
    {
        public void Write(Person newperson)
        {
            var serializer = new JsonSerializer();
            using (var sw = new StreamWriter(newperson.FileName + ".json"))
            {
                using (var jw = new JsonTextWriter(sw))
                {
                    jw.Formatting = Newtonsoft.Json.Formatting.Indented;
                    serializer.Serialize(jw, newperson);
                }
            }
        }
        public Person Read(string filename)
        {
            Person person = new Person();
            try
            {
                var context = File.ReadAllText(filename);
                person = JsonConvert.DeserializeObject<Person>(context);
            }
            catch (Exception)
            {

            }
            return person;
        }
        public void WriteListBox(ListBox.ObjectCollection humans)
        {
            var serializer = new JsonSerializer();
            using (var sw = new StreamWriter("humans.json"))
            {
                using (var jw = new JsonTextWriter(sw))
                {
                    jw.Formatting = Newtonsoft.Json.Formatting.Indented;
                    serializer.Serialize(jw, humans);
                }
            }
        }
    }
}