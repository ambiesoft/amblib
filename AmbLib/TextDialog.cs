using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ambiesoft
{
    internal partial class TextDialog : Form
    {
        public TextDialog()
        {
            InitializeComponent();

            AmbLib.MakeTripleClickTextBox(txtFind, 1000);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.F))
            {
                txtFind.Visible = !txtFind.Visible;
                if (txtFind.Visible)
                    txtFind.Focus();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        void FilterTextBox(TextBox tb, List<string> all, string filter)
        {
            StringBuilder sbResult = new StringBuilder();
            filter = filter.ToLower();
            foreach(string line in all)
            {
                if(line.ToLower().IndexOf(filter) >= 0)
                {
                    // found
                    sbResult.AppendLine(line);
                }
            }
            tb.Text = sbResult.ToString();
        }
        string filteredText_ = string.Empty;
        private void txtFind_TextChanged(object sender, EventArgs e)
        {
            if (filteredText_ == txtFind.Text)
                return;
            filteredText_ = txtFind.Text;
            DoFilter(filteredText_);
        }
        void DoFilter(string filter)
        {
            if (txtBody.Visible)
            {
                // One page view
                FilterTextBox(txtBody, origBody_, filter);
            }
            else
            {
                // Tab View
                TabControl tc = panelTabRoot.Controls[0] as TabControl;
                if (tc == null)
                    return;

                TabPage tp = tc.SelectedTab;
                if (tp == null)
                    return;
                TextBox tb = tp.Controls[0] as TextBox;
                if (tb == null)
                    return;
                FilterTextBox(tb, origBodies_[tc.SelectedIndex], filter);
            }
        }

        List<string> origBody_;
        List<List<string>> origBodies_;
        private void TextDialog_Shown(object sender, EventArgs e)
        {
            if(txtBody.Visible)
            {
                origBody_ = new List<string>(txtBody.Lines);
            }
            else
            {
                origBodies_ = new List<List<string>>();
                TabControl tc = panelTabRoot.Controls[0] as TabControl;
                if (tc == null)
                    return;
                tc.SelectedIndexChanged += tc_SelectedIndexChanged;
                foreach(TabPage tp in tc.TabPages)
                {
                    TextBox tb = tp.Controls[0] as TextBox;
                    if (tb == null)
                        return;
                    origBodies_.Add(new List<string>(tb.Lines));
                }
            }
        }

        void tc_SelectedIndexChanged(object sender, EventArgs e)
        {
            DoFilter(filteredText_);
        }
    }
}
