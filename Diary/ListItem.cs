using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

namespace Diary
{
    public partial class ListItem : UserControl
    {
        public ListItem()
        {
            InitializeComponent();
        }

        private void ListItem_Load(object sender, EventArgs e)
        {

        }

        #region Properties

        private string _title, _content, _time, _category;

        
        public PictureBox _pictureBox1
        {
            get
            {
                return this.pictureBox1;
            }
        }
        public PictureBox _pictureBox2
        {
            get
            {
                return this.pictureBox2;
            }
        }

        [Category("Custom prop")]
        public string title {
            get
            {
                return this._title;
            }
            set
            {
                this._title = value;
                this.lblTitle.Text = value;
            }
        }

        [Category("Custom prop")]
        public string content
        {
            get
            {
                return this._content;
            }
            set
            {
                this._content = value;
                this.lblContent.Text = value;
            }
        }

        [Category("Custom prop")]
        public string time
        {
            get
            {
                return this._time;
            }
            set
            {
                this._time = value;
                this.lblDate.Text = value;
            }
        }

        [Category("Custom prop")]
        public string category
        {
            get
            {
                return this._category;
            }
            set
            {
                this._category = value;
                this.lblCat.Text = value;
            }
        }
        #endregion

    }
}
