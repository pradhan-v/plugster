//Copyright © MadPiranha 2012-2013

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Collections;

namespace MadPiranha.FlatForm.Controls
{
    //Filtering : http://stackoverflow.com/questions/3409464/realtime-filtering-of-listbox

    public partial class FilterListControl : UserControl
    {
        public delegate void ActionHandler(object selectedItem);
        public event ActionHandler ActionPerformed;

        public delegate void DeleteHandler(object deletedItem);
        public event DeleteHandler ItemDeleted;

        private BindingSource listBindingSource;
        private DataTable datatable;

        DataColumn keyColumn;
        DataColumn valueColumn;
        //private static string columnName = "Value";

        public bool DeleteAllowed { get; set; }
        public bool AddFromTextBox { get; set; }

        public ListBox ListBoxControl
        {
            get
            {
                return listBox1;
            }
        }
        public TextBox TextBoxControl
        {
            get
            {
                return textBox1.TheTextBox;
            }
        }

        public FilterListControl()
        {
            InitializeComponent();

            //this.listBox1.BackColor = UITheme.TextBackColor;
            //this.listBox1.BorderStyle = UITheme.BorderStyle;
            //this.textBox1.BackColor = UITheme.TextBackColor;
            //this.textBox1.BorderStyle = UITheme.BorderStyle;

            InitDataTable();
        }

        private void InitDataTable()
        {
            datatable = new DataTable();
            keyColumn = new DataColumn("Key");
            valueColumn = new DataColumn("Value");
            datatable.Columns.Add(keyColumn);
            datatable.Columns.Add(valueColumn);
            datatable.PrimaryKey = new DataColumn[] { keyColumn };

            listBindingSource = new BindingSource();
            listBindingSource.DataSource = datatable;

            this.listBox1.DisplayMember = valueColumn.ColumnName;
            this.listBox1.DataSource = listBindingSource;
        }

        public object GetItemAt(int pos)
        {
            return datatable.Rows[pos][keyColumn];
        }

        public object[] GetAllValueItems()
        {
            ArrayList all = new ArrayList();
            foreach(object item in listBox1.Items)
            {
                if (item is DataRowView)
                {
                    all.Add(((DataRowView)item).Row[keyColumn]);
                }
            }
            return all.ToArray();
        }

        public Array GetAllValueItems(Type type)
        {
            ArrayList all = new ArrayList();
            foreach (object item in listBox1.Items)
            {
                if (item is DataRowView)
                {
                    object row = ((DataRowView)item).Row[keyColumn];
                    if(type.Equals(row.GetType()))
                        all.Add(row);
                }
            }
            return all.ToArray(type);
        }

        public object GetSelectedItem()
        {
            if (listBox1.SelectedItem != null && listBox1.SelectedItem is DataRowView)
            {
                return ((DataRowView)listBox1.SelectedItem).Row[keyColumn];
            }
            return null;
        }

        public void SetListRows(object[] rows)
        {
            foreach (object obj in rows)
            {
                AddListItem(obj);
            }
        }

        public void AddListItem(object obj)
        {
            if (keyColumn.DataType != null)
                keyColumn.DataType = obj.GetType();

            //datatable.Rows.Add(str);

            DataRow row = datatable.NewRow();
            row[keyColumn] = obj;
            row[valueColumn] = obj.ToString();
            if(!datatable.Rows.Contains(obj))
                datatable.Rows.InsertAt(row, 0);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            listBindingSource.Filter = "Value LIKE '" + "%" + this.textBox1.Text + "%'";
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (AddFromTextBox && Keys.Enter == e.KeyCode && string.Empty != textBox1.Text.Trim() && !datatable.Rows.Contains(textBox1.Text))
            {
                AddListItem(textBox1.Text);
                textBox1.Text = string.Empty;
            }
        }

        private void listBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (DeleteAllowed && Keys.Delete == e.KeyCode && listBox1.SelectedIndex != -1)
            {
                object delObj = GetSelectedItem();
                datatable.Rows.RemoveAt(listBox1.SelectedIndex);
                if (ItemDeleted != null)
                    ItemDeleted(delObj);
            }
        }

        protected virtual void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (ActionPerformed != null)
                ActionPerformed(GetSelectedItem());
        }

        protected virtual void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keys.Enter == e.KeyCode)
            {
                if (ActionPerformed != null)
                    ActionPerformed(GetSelectedItem());
            }
        }

    }
}
