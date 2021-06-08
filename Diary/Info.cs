using System;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;

namespace Diary
{
    public partial class Info : Form
    {
        private Form1 main;
        public Info(Form1 main)
        {
            InitializeComponent();
            this.main = main;
        }

        private void Info_Load(object sender, EventArgs e)
        {
            this.init();
        }

        private void Info_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.main.Enabled = true;
            this.main.startTimer();
            this.main.memory = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void init()
        {
            this.Text = this.main.memory.title;
            this.label3.Text = this.main.memory.title;
            this.label5.Text = this.main.memory.category;
            this.label7.Text = new PersianDateTime(this.main.memory.creationDate).ToString("dddd d MMMM yyyy ساعت hh:mm:ss tt");
            this.label9.Text = new PersianDateTime(this.main.memory.updateDate).ToString("dddd d MMMM yyyy ساعت hh:mm:ss tt");

            switch (this.label5.Text)
            {
                case "شخصی":
                    pictureBox1.Image = Image.FromFile("Images\\Person.png");
                    break;
                case "کاری":
                    pictureBox1.Image = Image.FromFile("Images\\bag.png");
                    break;
                case "مطالعه":
                    pictureBox1.Image = Image.FromFile("Images\\book.png");
                    break;
                case "خانوادگی":
                    pictureBox1.Image = Image.FromFile("Images\\home.jfif");
                    break;
                case "تعیین نشده":
                    pictureBox1.Image = Image.FromFile("Images\\category.png");
                    break;
            }
        }
    }
}
