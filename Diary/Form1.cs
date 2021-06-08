using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

using Model;

namespace Diary
{
    public partial class Form1 : Form
    {
        private List<Memory> _memories;
        private Memory _memory;
        private string[] categories = { "شخصی", "خانوادگی", "مطالعه", "کاری", "تعیین نشده" };
        private bool flag;
        private Timer timer = new Timer();
        public Memory memory
        {
            get
            {
                return this._memory;
            }
            set
            {
                this._memory = value;
            }
        }

        public List<Memory> memories
        {
            get
            {
                return this._memories;
            }
            set
            {
                this._memories = value;
            }
        }
        public Form1()
        {
            InitializeComponent();
            this.memories = new List<Memory>();
            flag = false;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void showAddMemoryPage()
        {

            new Add(this).Show();
            this.Enabled = false;
            this.timer.Stop();
        }

        public void handleListItemClick(object sender, EventArgs e)
        {
            this.memory = this.findMemory((ListItem)sender);
            if (this.memory != null)
            {
                new Add(this).Show();
                this.Enabled = false;
                this.timer.Stop();
            }
            else
                MessageBox.Show("نمی‌تونه باز بشه");

        }

        public void handleBtnDetail(object sender, EventArgs e)
        {
            PictureBox p = sender as PictureBox;
            this.memory = this.findMemory(p.Parent as ListItem);
            if (this.memory != null)
            {
                this.Enabled = false;
                this.timer.Stop();
                new Info(this).Show();
            }
            else
                MessageBox.Show("این خاطره احتمالا وجود ندارد");
        }
        public void handleBtnDel(object sender, EventArgs e)
        {
            PictureBox p = sender as PictureBox;
            Memory m = this.findMemory(p.Parent as ListItem);

            if (this.memories.Count == 1)
            {
                File.Delete("dData\\dm.bin");
                this.memories.Remove(m);
            }
            else
            {
                this.memories.Remove(m);
                FileStreams.SerializeAll("dData\\dm.bin", this.memories);
            }
            this.flowLayoutPanel1.Controls.Remove(p.Parent as ListItem);


        }

        private void panel2_Click(object sender, EventArgs e)
        {
            this.showAddMemoryPage();
        }

        private void label2_Click_1(object sender, EventArgs e)
        {
            this.showAddMemoryPage();
        }

        private void refreshPage(object sender, EventArgs e)
        {
            this.flowLayoutPanel1.Controls.Clear();
            this.memories.Reverse();
            foreach (Memory m in this.memories)
            {
                ListItem l = new ListItem();
                l.title = m.title;
                l.content = m.content;


                l.time = DateConverter.Convert(new PersianDateTime(m.creationDate));
                l.category = m.category;
                l.Click += handleListItemClick;
                l._pictureBox1.Click += handleBtnDetail;
                l._pictureBox2.Click += handleBtnDel;

                this.flowLayoutPanel1.Controls.Add(l);
            }
            this.memories.Reverse();
            this.textBox1.ReadOnly = false;
        }
        public void setMemories()
        {
            this.MainFlow.Controls.Clear();

            this.memories = (List<Memory>) FileStreams.deSerialize("dData\\dm.bin");
            if(this.memories != null)
            {
                this.memories.Reverse();
                foreach (Memory m in this.memories)
                {
                    ListItem l = new ListItem();
                    l.title = m.title;
                    l.content = m.content;
                    

                    l.time = DateConverter.Convert(new PersianDateTime(m.creationDate));
                    l.category = m.category;
                    l.Click += handleListItemClick;
                    l._pictureBox1.Click += handleBtnDetail;
                    l._pictureBox2.Click += handleBtnDel;

                    this.flowLayoutPanel1.Controls.Add(l);
                }
                this.memories.Reverse();
                this.textBox1.ReadOnly = false;
            }
            else
                this.textBox1.ReadOnly = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.setMemories();
            this.setTimer();
            this.startTimer();
            this.setAutoCompleteSearchBar();
        }
        private Memory findMemory(ListItem l)
        {
            foreach (Memory m in this.memories)
                if (m.title == l.title && m.content == l.content && DateConverter.Convert(new PersianDateTime(m.creationDate)) == l.time && m.category == l.category)
                    return m;
            return null;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string txt = textBox1.Text;
            
            if (txt.Trim().Equals("") && flag == false)
            {
                this.setMemories();
                flag = true;
            }
            else
            {
                flag = false;
                foreach (ListItem l in this.flowLayoutPanel1.Controls)
                    if (!l.title.Contains(txt) && !l.category.Contains(txt))
                        this.flowLayoutPanel1.Controls.Remove(l);
            }
            
        }

        private void setAutoCompleteSearchBar()
        {
            AutoCompleteStringCollection acl = new AutoCompleteStringCollection();
            acl.AddRange(this.categories);
            if (this.memories != null)
                foreach (Memory m in this.memories)
                    acl.Add(m.title);


            textBox1.AutoCompleteCustomSource = acl;
        }

        private void setTimer()
        {
            timer.Tick += new EventHandler(refreshPage);
            timer.Interval = 30000;
        }
        public void startTimer()
        {
            timer.Start();
        }
    }
}
