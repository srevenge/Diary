using System;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using Model;

namespace Diary
{
    public partial class Add : Form
    {
        private Form1 main;
        private Memory memory;
        private string cat;
        public Add(Form1 main)
        {
            InitializeComponent();
            this.main = main;
        }

        private void Add_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.main.Enabled = true;
            this.main.startTimer();
            this.main.memory = null;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (this.main.memory == null)
                this.SaveMemory();
            else
                this.UpdateMemory();
        }

        private void Add_Load(object sender, EventArgs e)
        {
            if (this.main.memory == null)
                this.initAdd();
            else
                this.initUpdate();

            this.addMemorySubItems();

        }

        private void handleCategoriesClick(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox) sender;
            this.resetBackColors();
            p.BackColor = Color.Aqua;
            if (p == pictureBox2)
                cat = "شخصی";
            else if(p == pictureBox3)
                cat = "مطالعه";
            else if (p == pictureBox4)
                cat = "خانوادگی";
            else if (p == pictureBox5)
                cat = "کاری";
            else
                cat = "";


        }

        private void handleMenuItemsClick(object sender, EventArgs e)
        {
            ToolStripMenuItem mI = (ToolStripMenuItem)sender;
            switch (mI.Text)
            {
                case "بستن":
                    this.Close();
                    break;
                case "ذخیره":
                    if (this.main.memory == null)
                        this.SaveMemory();
                    else
                        this.UpdateMemory();
                    break;
                case "جدید":
                    this.resetBackColors();
                    pictureBox6.BackColor = Color.Aqua;
                    richTextBox1.Clear();
                    placeHolderTextBox1.Clear();
                    this.cat = "";
                    break;
                case "حذف":
                    
                    if(this.main.memories.Count == 1)
                    {
                        File.Delete("dData\\dm.bin");
                        this.main.MainFlow.Controls.Clear();
                        this.main.txtSearch.ReadOnly = true;
                        this.main.memories = null;
                    }
                        
                    else
                    {
                        this.main.memories.Remove(this.main.memory);
                        FileStreams.SerializeAll("dData\\dm.bin", this.main.memories);
                        this.main.setMemories();
                    }
                    
                    this.Close();
                    break;
            }
        }

        private void handleOpenAnotherMemoryClick(object sender, EventArgs e)
        {
            ToolStripMenuItem mI = (ToolStripMenuItem)sender;
            bool flag = true;
            foreach (Memory m in this.main.memories)
                if (mI.Text.Contains(m.title) || (m.title.Length> 10 && mI.Text.Contains(m.title.Substring(0,10))))
                {
                    this.Close();
                    this.main.memory = m;
                    new Add(this.main).Show();
                    this.main.Enabled = false;
                    flag = false;
                    break;
                }
            if(flag)
                MessageBox.Show("احتمالا پیدا نشد");
        }


        private void resetBackColors()
        {
            this.pictureBox2.BackColor = Color.FloralWhite;
            this.pictureBox3.BackColor = Color.FloralWhite;
            this.pictureBox4.BackColor = Color.FloralWhite;
            this.pictureBox5.BackColor = Color.FloralWhite;
            this.pictureBox6.BackColor = Color.FloralWhite;
        }

        private void addListItemToMainPage()
        {
            ListItem l = new ListItem();
            l.title = this.memory.title;
            l.content = this.memory.content;
            l.time = DateConverter.Convert(new PersianDateTime(this.memory.creationDate));
            l.category = this.memory.category;
            l.Click += this.main.handleListItemClick;
            l._pictureBox1.Click += this.main.handleBtnDetail;
            l._pictureBox2.Click += this.main.handleBtnDel;

            this.main.MainFlow.Controls.Add(l);
            this.main.txtSearch.ReadOnly = false;
        }

        private void SaveMemory()
        {
            if (richTextBox1.Text.Trim().Equals("") || placeHolderTextBox1.Text.Trim().Equals(""))
                MessageBox.Show("لطفا اطلاعات خود را وارد کنید");
            else
            {
                this.memory = new Memory(placeHolderTextBox1.Text, richTextBox1.Text, this.cat);
                FileStreams.Serialize("dData\\dm.bin", this.memory);
                this.main.Enabled = true;

                if (this.main.memories == null)
                    this.main.memories = new List<Memory>();
                    

                this.main.memories.Add(this.memory);
                this.addListItemToMainPage();
                this.Close();
            }
            
        }

        private void addMemorySubItems()
        {
            if(main.memories != null)
            {
                string txt = "";
                this.main.memories.Reverse();
                for (int i = 0; i < this.main.memories.Count && i < 10; i++)
                {
                    txt = this.main.memories[i].title + " ";
                    txt += (this.main.memories[i].content.Length< 5)? this.main.memories[i].content : this.main.memories[i].content.Substring(0, 5);
                    txt = (txt.Length> 15)? txt.Substring(0,10) + " ..." : txt;
                    ToolStripMenuItem mI = new ToolStripMenuItem(txt);
                    mI.Click += handleOpenAnotherMemoryClick;
                    this.بازکردنToolStripMenuItem.DropDownItems.Add(mI);
                }
                this.main.memories.Reverse();
            }else
            {
                this.بازکردنToolStripMenuItem.Enabled = false;
            }
            
        }

        private void initAdd()
        {
            if (this.main.memories == null)
                this.بازکردنToolStripMenuItem.Enabled = false;
            cat = "";
            pictureBox6.BackColor = Color.Aqua;
        }
        private void initUpdate()
        {
            this.Text = this.main.memory.title;
            this.label1.Text = "تغییر خاطره";
            this.placeHolderTextBox1.Text = this.main.memory.title;
            this.richTextBox1.Text = this.main.memory.content;

            //addng menuItem delete
            ToolStripMenuItem mI = new ToolStripMenuItem("حذف");
            mI.Image = Image.FromFile("Images\\delete.png");
            mI.Click += this.handleMenuItemsClick;
            mI.ShowShortcutKeys = true;
            mI.ShortcutKeys = Keys.Control | Keys.D;

            this.فایلToolStripMenuItem.DropDownItems.Add(mI);

            cat = this.main.memory.category;
            switch (cat)
            {
                case "شخصی":
                    pictureBox2.BackColor = Color.Aqua;
                    break;
                case "مطالعه":
                    pictureBox3.BackColor = Color.Aqua;
                    break;
                case "خانوادگی":
                    pictureBox4.BackColor = Color.Aqua;
                    break;
                case "کاری":
                    pictureBox5.BackColor = Color.Aqua;
                    break;
                case "تعیین نشده":
                    pictureBox6.BackColor = Color.Aqua;
                    break;
            }
        }

        private void UpdateMemory()
        {
            if (richTextBox1.Text.Trim().Equals("") || placeHolderTextBox1.Text.Trim().Equals(""))
                MessageBox.Show("لطفا اطلاعات خود را وارد کنید");
            else
            {
                this.memory = this.main.memory;
                this.memory.title = this.placeHolderTextBox1.Text;
                this.memory.content = this.richTextBox1.Text;
                this.memory.category = this.cat;
                this.memory.updateDate = DateTime.Now;
                this.main.memories.Remove(this.main.memory);
                this.main.memories.Add(this.memory);
                FileStreams.SerializeAll("dData\\dm.bin", this.main.memories);
                this.main.setMemories();

                this.Close();
            }
            
        }
    }
}
