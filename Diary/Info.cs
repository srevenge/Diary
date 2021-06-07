using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            this.main.memory = null;
            this.main.Enabled = true;
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
            this.label7.Text = this.main.memory.creationDate.ToLocalTime().ToLongTimeString();
            this.label9.Text = this.main.memory.updateDate.ToLocalTime().ToLongTimeString();

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
