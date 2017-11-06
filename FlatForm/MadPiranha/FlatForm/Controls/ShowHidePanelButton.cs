//Copyright © MadPiranha 2012-2013

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace MadPiranha.FlatForm.Controls
{
    public class ShowHidePanelButton : MyButton
    {

        private ShowHidePanel showHidePanel;

        public ShowHidePanel ShowHidePanel
        {
            get { return showHidePanel; }
            set 
            {
                if (showHidePanel == null && value!=null )
                {
                    showHidePanel = value;
                    showHidePanel.ParentChanged += new System.EventHandler(this.PanelParentChanged);
                }
            }
        }

        public ShowHidePanelButton(ShowHidePanel shPanel) : this()
        {
            this.ShowHidePanel = shPanel;
        }

        public ShowHidePanelButton()
        {
//            showHidePanel = new ShowHidePanel();
            this.Click += new System.EventHandler(this.buttonLoad_Click);
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            ToggleShowHidePanel();
        }

        public void ToggleShowHidePanel()
        {
            if (showHidePanel == null) return;

            if (showHidePanel.Visible)
            {
                showHidePanel.Visible = false;
            }
            else
            {
                ShowPanel();
            }
        }

        public void ShowPanel()
        {
            if (showHidePanel == null) return;

            SetPanelLocation();
            showHidePanel.Visible = true;
        }

        private void SetPanelLocation()
        {
            if (showHidePanel == null) return;

            Rectangle screenrect = this.RectangleToScreen(new Rectangle());
            showHidePanel.Location = showHidePanel.Parent.PointToClient(new Point(screenrect.X, screenrect.Y - showHidePanel.Bounds.Height));
            showHidePanel.BringToFront();
        }

        private void PlugsterWindow2_Resize(object sender, EventArgs e)
        {
            if (showHidePanel == null) return;

            if (showHidePanel.Visible)
            {
                SetPanelLocation();
            }
        }

        private void PanelParentChanged(object sender, EventArgs e)
        {
            if( showHidePanel !=null && showHidePanel.Parent!=null)
                showHidePanel.Parent.Resize += new System.EventHandler(this.PlugsterWindow2_Resize);
        }
    }
}
