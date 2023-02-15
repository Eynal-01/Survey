using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        /// <summary>
        /// 
        /// </summary>
        public Form1()
        {
            InitializeComponent();                      
        }
        FileHelper fileHelper = new FileHelper();
        private void addBtn_Click(object sender, EventArgs e)
        {
            person = new Person();
            person.Name = Nametxtb.Text;
            person.Filename = person.Name;
            person.Surname = Surnametxtb.Text;
            person.Email = Emailtxtb.Text;
            person.Number = Phonemaskedtxtb.Text;
            person.Birthdate = BirthdatetimePicker.Value;
            UserListBox.DisplayMember = nameof(Person.Name);
            textBox3.Text = person.Filename + ".json";
            if (!UserListBox.Items.Equals(person.Id))
            {
                UserListBox.Items.Add(person);
            }

            if (!textBox3.Text.Contains(".json"))
            {
                textBox3.Text = person.Filename + ".json";
            }
        }
        private void saveBtn_Click(object sender, EventArgs e)
        {
            fileHelper.Write(person);
            var templocation1 = Changebtn.Location;
            Changebtn.Location = Addbtn.Location;
            Addbtn.Location = templocation1;
            Nametxtb.Text = "";
            Surnametxtb.Text = "";
            Emailtxtb.Text = "";
            Phonemaskedtxtb.Text = "";
            BirthdatetimePicker.Text = "";
            textBox3.Text = "";
        }
        private void loadBtn_Click(object sender, EventArgs e)
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
                    var templocation = Addbtn.Location;
                    Addbtn.Location = Changebtn.Location;
                    Changebtn.Location = templocation;
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
                    Phonemaskedtxtb.Text = user.Number;
                    BirthdatetimePicker.Text = user.Birthdate.ToString();
                }
            }
            catch
            {
            }
        }
        private void changeBtn_Click(object sender, EventArgs e)
        {
            textBox3.Text = user.Filename + ".json";
            if (Nametxtb.Text != user.Name)
            {
                user.Name = Nametxtb.Text;
            }
            if (person.Name != user.Name)
            {
                person.Filename = user.Filename;
            }
            else
            {
                textBox3.Text = user.Filename + ".json";
            }
            if (Surnametxtb.Text != user.Surname)
            {
                user.Surname = Surnametxtb.Text;
            }
            if (Emailtxtb.Text != user.Email)
            {
                user.Email = Emailtxtb.Text;
            }
            if (Phonemaskedtxtb.Text != user.Number)
            {
                user.Number = Phonemaskedtxtb.ToString();
            }
            if (BirthdatetimePicker.Text != user.Birthdate.ToString())
            {
                user.Birthdate = BirthdatetimePicker.Value;
            }
            person = user;
            fileHelper.Write(person);
        }
        private void exitBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void humanslistBox_DoubleClick(object sender, EventArgs e)
        {
            var human = UserListBox.SelectedItem as Person;
            textBox3.Text = human.Filename;
        }
        private void humanslistBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var human = UserListBox.SelectedItem as Person;
            textBox3.Text = human.Filename;

            Nametxtb.Text = human.Name;
            Surnametxtb.Text = human.Surname;
            Emailtxtb.Text = human.Email;
            Phonemaskedtxtb.Text = human.Number;
            BirthdatetimePicker.Text = human.Birthdate.ToString();
        }
        private void nameTxb_TextChanged(object sender, EventArgs e)
        {
            person.Filename = person.Name;
        }
        private void ExitBtn_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void ExitBtn_MouseEnter(object sender, EventArgs e)
        {
            ExitBtn.BackColor = Color.Red;
        }
        private void ExitBtn_MouseLeave(object sender, EventArgs e)
        {
            ExitBtn.BackColor = Color.Silver;
        }
    }
}