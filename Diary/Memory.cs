using System;

namespace Model
{
    [Serializable]
    public class Memory
    {
        private string _title, _content, _category;
        private DateTime _creationDate, _updateDate;

        public Memory()
        {
            this._creationDate = DateTime.Now;
            this.updateDate = this.creationDate;
            this.category = "";
        }

        public Memory(string title, string content, string category)
        {
            this.title = title;
            this.content = content;
            this._creationDate = DateTime.Now;
            this.updateDate = this.creationDate;
            this.category = category;
        }
        public Memory(string title, string content, string category, DateTime updateDate)
        {
            this.title = title;
            this.content = content;
            this._creationDate = DateTime.Now;
            this.updateDate = updateDate;
            this.category = category;
        }
        public string title
        {
            get
            {
                return this._title;
            }
            set
            {
                this._title = (value.Trim() == "") ? "تعیین نشده" : value;
            }
        }

        public string content
        {
            get
            {
                return this._content;
            }
            set
            {
                this._content = (value.Trim() == "") ? "محتوایی وجود ندارد" : value;
            }
        }

        public string category
        {
            get
            {
                return this._category;
            }
            set
            {
                this._category = (value.Trim() == "") ? "تعیین نشده" : value;
            }
        }

        public DateTime creationDate
        {
            get
            {
                return this._creationDate;
            }
        }

        public DateTime updateDate
        {
            get
            {
                return this._updateDate;
            }

            set
            {
                this._updateDate = value;
            }
        }
    }
}
