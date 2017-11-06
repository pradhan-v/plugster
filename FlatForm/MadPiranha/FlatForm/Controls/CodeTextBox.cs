//Copyright © MadPiranha 2012-2013

using System;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Drawing;
using FastColoredTextBoxNS;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace MadPiranha.FlatForm.Controls
{
    public partial class CodeTextBox : FastColoredTextBox
    {
        string lang = "CSharp";

        //styles
        TextStyle BlueStyle = new TextStyle(Brushes.Blue, null, FontStyle.Regular);
        TextStyle BoldStyle = new TextStyle(null, null, FontStyle.Bold | FontStyle.Underline);
        TextStyle GrayStyle = new TextStyle(Brushes.Gray, null, FontStyle.Regular);
        TextStyle MagentaStyle = new TextStyle(Brushes.Magenta, null, FontStyle.Regular);
        TextStyle GreenStyle = new TextStyle(Brushes.Green, null, FontStyle.Italic);
        TextStyle BrownStyle = new TextStyle(Brushes.Brown, null, FontStyle.Italic);
        TextStyle MaroonStyle = new TextStyle(Brushes.Maroon, null, FontStyle.Regular);
        MarkerStyle SameWordsStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(40, Color.Gray)));

        public CodeTextBox()
        {
            InitializeComponent();
            InitStylesPriority();
            this.Text = "//MadPiranha: This text area is extended from... Fast Colored TextBox for Syntax Highlighting\n//https://github.com/PavelTorgashov/FastColoredTextBox";

            //            
            //move caret to start text
            this.Selection.Start = Place.Empty;
            this.DoCaretVisible();
            this.IsChanged = false;
            this.ClearUndo();
        }

        private void InitStylesPriority()
        {
            this.ClearStylesBuffer();
            
            //add this style explicitly for drawing under other styles
            this.AddStyle(SameWordsStyle);
        }

        #region Drag and drop

        protected override void OnDragEnter(DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                base.OnDragEnter(e);
        }

        protected override void OnDragDrop(DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                try
                {
                    Array data = (Array)e.Data.GetData(DataFormats.FileDrop);
                    if (data != null)
                    {
                        string filename = data.GetValue(0).ToString();
                        if (filename != null && !string.Empty.Equals(filename))
                        {
                            //TODO: thread this.
                            this.Text = File.ReadAllText(filename);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
            else
                base.OnDragDrop(e);
        }

        #endregion

        private void this_TextChanged(object sender, TextChangedEventArgs e)
        {
            switch(lang)
            {
                case "CSharp":
                    //For sample, we will highlight the syntax of C# manually, although could use built-in highlighter
                    CSharpSyntaxHighlight(e);//custom highlighting
                    break;
                default:
                    break;//for highlighting of other languages, we using built-in FastColoredTextBox highlighter
            }
        }

        private void CSharpSyntaxHighlight(TextChangedEventArgs e)
        {
            this.LeftBracket = '(';
            this.RightBracket = ')';
            this.LeftBracket2 = '\x0';
            this.RightBracket2 = '\x0';
            //clear style of changed range
            e.ChangedRange.ClearStyle(BlueStyle, BoldStyle, GrayStyle, MagentaStyle, GreenStyle, BrownStyle);

            //string highlighting
            e.ChangedRange.SetStyle(BrownStyle, @"""""|@""""|''|@"".*?""|(?<!@)(?<range>"".*?[^\\]"")|'.*?[^\\]'");
            //comment highlighting
            e.ChangedRange.SetStyle(GreenStyle, @"//.*$", RegexOptions.Multiline);
            e.ChangedRange.SetStyle(GreenStyle, @"(/\*.*?\*/)|(/\*.*)", RegexOptions.Singleline);
            e.ChangedRange.SetStyle(GreenStyle, @"(/\*.*?\*/)|(.*\*/)", RegexOptions.Singleline|RegexOptions.RightToLeft);
            //number highlighting
            e.ChangedRange.SetStyle(MagentaStyle, @"\b\d+[\.]?\d*([eE]\-?\d+)?[lLdDfF]?\b|\b0x[a-fA-F\d]+\b");
            //attribute highlighting
            e.ChangedRange.SetStyle(GrayStyle, @"^\s*(?<range>\[.+?\])\s*$", RegexOptions.Multiline);
            //class name highlighting
            e.ChangedRange.SetStyle(BoldStyle, @"\b(class|struct|enum|interface)\s+(?<range>\w+?)\b");
            //keyword highlighting
            e.ChangedRange.SetStyle(BlueStyle, @"\b(abstract|as|base|bool|break|byte|case|catch|char|checked|class|const|continue|decimal|default|delegate|do|double|else|enum|event|explicit|extern|false|finally|fixed|float|for|foreach|goto|if|implicit|in|int|interface|internal|is|lock|long|namespace|new|null|object|operator|out|override|params|private|protected|public|readonly|ref|return|sbyte|sealed|short|sizeof|stackalloc|static|string|struct|switch|this|throw|true|try|typeof|uint|ulong|unchecked|unsafe|ushort|using|virtual|void|volatile|while|add|alias|ascending|descending|dynamic|from|get|global|group|into|join|let|orderby|partial|remove|select|set|value|var|where|yield)\b|#region\b|#endregion\b");

            //clear folding markers
            e.ChangedRange.ClearFoldingMarkers();
            //set folding markers
            e.ChangedRange.SetFoldingMarkers("{", "}");//allow to collapse brackets block
            e.ChangedRange.SetFoldingMarkers(@"#region\b", @"#endregion\b");//allow to collapse #region blocks
            e.ChangedRange.SetFoldingMarkers(@"/\*", @"\*/");//allow to collapse comment block
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowFindDialog();
        }

        private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowReplaceDialog();
        }

        //private void miLanguage_DropDownOpening(object sender, EventArgs e)
        //{
        //    foreach (ToolStripMenuItem mi in miLanguage.DropDownItems)
        //        mi.Checked = mi.Text == lang;
        //}

        private void miCSharp_Click(object sender, EventArgs e)
        {
            //set language
            lang = (sender as ToolStripMenuItem).Text;
            this.ClearStylesBuffer();
            this.Range.ClearStyle(StyleIndex.All);
            InitStylesPriority();
            this.AutoIndentNeeded -= this_AutoIndentNeeded;
            //
            switch (lang)
            {
                //For example, we will highlight the syntax of C# manually, although could use built-in highlighter
                case "CSharp":
                    this.Language = Language.Custom;
                    this.CommentPrefix = "//";
                    this.AutoIndentNeeded += this_AutoIndentNeeded;
                    //call OnTextChanged for refresh syntax highlighting
                    this.OnTextChanged();
                    break;
                case "VB": this.Language = Language.VB; break;
                case "HTML": this.Language = Language.HTML; break;
                case "SQL": this.Language = Language.SQL; break;
                case "PHP": this.Language = Language.PHP; break;
                case "JS": this.Language = Language.JS; break;
            }
            this.OnSyntaxHighlight(new TextChangedEventArgs(this.Range));
        }

        private void collapseSelectedBlockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.CollapseBlock(this.Selection.Start.iLine, this.Selection.End.iLine);
        }

        private void collapseAllregionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this example shows how to collapse all #region blocks (C#)
            if (lang != "CSharp") return;
            for (int iLine = 0; iLine < this.LinesCount; iLine++)
            {
                if (this[iLine].FoldingStartMarker == @"#region\b")//marker @"#region\b" was used in SetFoldingMarkers()
                    this.CollapseFoldingBlock(iLine);
            }
        }

        private void exapndAllregionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this example shows how to expand all #region blocks (C#)
            if (lang != "CSharp") return;
            for (int iLine = 0; iLine < this.LinesCount; iLine++)
            {
                if (this[iLine].FoldingStartMarker == @"#region\b")//marker @"#region\b" was used in SetFoldingMarkers()
                    this.ExpandFoldedBlock(iLine);
            }
        }

        private void increaseIndentSiftTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.IncreaseIndent();
        }

        private void decreaseIndentTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.DecreaseIndent();
        }

        private void hTMLToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "HTML with <PRE> tag|*.html|HTML without <PRE> tag|*.html";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string html = "";

                if (sfd.FilterIndex == 1)
                {
                    html = this.Html;
                }
                if (sfd.FilterIndex == 2)
                {
                    
                    ExportToHTML exporter = new ExportToHTML();
                    exporter.UseBr = true;
                    exporter.UseNbsp = false;
                    exporter.UseForwardNbsp = true;
                    exporter.UseStyleTag = true;
                    html = exporter.GetHtml(this);
                }
                File.WriteAllText(sfd.FileName, html);
            }
        }

        private void this_SelectionChangedDelayed(object sender, EventArgs e)
        {
            this.VisibleRange.ClearStyle(SameWordsStyle);

            if (!this.Selection.IsEmpty)
                return;//user selected diapason

            //get fragment around caret
            var fragment = this.Selection.GetFragment(@"\w");
            string text = fragment.Text;
            if (text.Length == 0)
                return;
            //highlight same words
            //var ranges = this.VisibleRange.GetRanges("\\b" + text + "\\b").ToArray();
            //if(ranges.Length>1)
            //foreach(var r in ranges)
            //    r.SetStyle(SameWordsStyle);
            IEnumerator<Range> ranges = this.VisibleRange.GetRanges("\\b" + text + "\\b").GetEnumerator();
            while (ranges.MoveNext())
            {
                ranges.Current.SetStyle(SameWordsStyle);
            }
        }

        private void goForwardCtrlShiftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.NavigateForward();
        }

        private void goBackwardCtrlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.NavigateBackward();
        }

        private void autoIndentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.DoAutoIndent();
        }

        const int maxBracketSearchIterations = 2000;

        void GoLeftBracket(FastColoredTextBox tb, char leftBracket, char rightBracket)
        {
            Range range = tb.Selection.Clone();//need clone because we will move caret
            int counter = 0;
            int maxIterations = maxBracketSearchIterations;
            while (range.GoLeftThroughFolded())//move caret left
            {
                if (range.CharAfterStart == leftBracket) counter++;
                if (range.CharAfterStart == rightBracket) counter--;
                if (counter == 1)
                {
                    //found
                    tb.Selection.Start = range.Start;
                    tb.DoSelectionVisible();
                    break;
                }
                //
                maxIterations--;
                if (maxIterations <= 0) break;
            }
            tb.Invalidate();
        }

        void GoRightBracket(FastColoredTextBox tb, char leftBracket, char rightBracket)
        {
            var range = tb.Selection.Clone();//need clone because we will move caret
            int counter = 0;
            int maxIterations = maxBracketSearchIterations;
            do
            {
                if (range.CharAfterStart == leftBracket) counter++;
                if (range.CharAfterStart == rightBracket) counter--;
                if (counter == -1)
                {
                    //found
                    tb.Selection.Start = range.Start;
                    tb.Selection.GoRightThroughFolded();
                    tb.DoSelectionVisible();
                    break;
                }
                //
                maxIterations--;
                if (maxIterations <= 0) break;
            } while (range.GoRightThroughFolded());//move caret right

            tb.Invalidate();
        }

        private void goLeftBracketToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GoLeftBracket(this, '{', '}');
        }

        private void goRightBracketToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GoRightBracket(this, '{', '}');
        }

        private void this_AutoIndentNeeded(object sender, AutoIndentEventArgs args)
        {
            //block {}
            if (Regex.IsMatch(args.LineText, @"^[^""']*\{.*\}[^""']*$"))
                return;
            //start of block {}
            if (Regex.IsMatch(args.LineText, @"^[^""']*\{"))
            {
                args.ShiftNextLines = args.TabLength;
                return;
            }
            //end of block {}
            if (Regex.IsMatch(args.LineText, @"}[^""']*$"))
            {
                args.Shift = -args.TabLength;
                args.ShiftNextLines = -args.TabLength;
                return;
            }
            //label
            if (Regex.IsMatch(args.LineText, @"^\s*\w+\s*:\s*($|//)") &&
                !Regex.IsMatch(args.LineText, @"^\s*default\s*:"))
            {
                args.Shift = -args.TabLength;
                return;
            }
            //some statements: case, default
            if (Regex.IsMatch(args.LineText, @"^\s*(case|default)\b.*:\s*($|//)"))
            {
                args.Shift = -args.TabLength / 2;
                return;
            }
            //is unclosed operator in previous line ?
            if (Regex.IsMatch(args.PrevLineText, @"^\s*(if|for|foreach|while|[\}\s]*else)\b[^{]*$"))
                if (!Regex.IsMatch(args.PrevLineText, @"(;\s*$)|(;\s*//)"))//operator is unclosed
                {
                    args.Shift = args.TabLength;
                    return;
                }
        }

        private void CodeTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.S:
                    if (e.Modifiers == Keys.Control)
                        ShowSaveToDialog();
                    break;
                case Keys.I:
                    if (e.Modifiers == (Keys.Control | Keys.Shift))
                        DoAutoIndent();
                    break;
            }
        }

        SaveFileDialog savedialog;
        private void ShowSaveToDialog()
        {
            if(savedialog==null)
            {
                savedialog = new SaveFileDialog();
                savedialog.AddExtension=true;
                savedialog.DefaultExt="cs";
            }
            DialogResult res = savedialog.ShowDialog();
            if (res == DialogResult.OK)
                File.WriteAllText(savedialog.FileName, this.Text);
        }



    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   