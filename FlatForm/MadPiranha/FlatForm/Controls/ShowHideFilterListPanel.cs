//Copyright © MadPiranha 2012-2013

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace MadPiranha.FlatForm.Controls
{
    public partial class ShowHideFilterListPanel : ShowHidePanel
    {
        public string Label
        {
            get
            {
                return labelHeader.Text;
            }
            set
            {
                labelHeader.Text = value;
            }
        }

        public FilterListControl FilterListBox
        {
            get
            {
                return filterList;
            }
            set
            {
                filterList = value;
            }
        }

        public ListBox ListBoxControl
        {
            get
            {
                return FilterListBox.ListBoxControl;
            }
        }

        public bool DeleteAllowed { get { return FilterListBox.DeleteAllowed; } set { FilterListBox.DeleteAllowed = value; } }
        public bool AddFromTextBox { get { return FilterListBox.AddFromTextBox; } set { FilterListBox.AddFromTextBox = value; } }

        public void AddListItem(object obj)
        {
            FilterListBox.AddListItem(obj);
        }

        public object GetItemAt(int i)
        {
            return FilterListBox.GetItemAt(i);
        }

        public ShowHideFilterListPanel()
        {
            InitializeComponent();
            this.filterList.TextBoxControl.KeyDown += new KeyEventHandler(this.EscapeKeyPress);
            this.filterList.ListBoxControl.KeyDown += new KeyEventHandler(this.EscapeKeyPress);
        }


        private void TestsListPanel_VisibleChanged(object sender, EventArgs e)
        {
            if(this.Visible)
                this.FilterListBox.TextBoxControl.Focus();
        }
    }
}
