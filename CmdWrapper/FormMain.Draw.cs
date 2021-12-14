using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CmdWrapper
{
    public partial class FormMain
    {
        private void CreateTabPage(Option option)
        {
            var tabPage = new TabPage(option.Name);
            tabPage.Tag = option;
            tabPage.Text = option.Name;
            tabPage.Location = new System.Drawing.Point(4, 29);
            tabPage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tabPage.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tabPage.Size = new System.Drawing.Size(733, 699);
            tabPage.TabIndex = 0;
            tabPage.UseVisualStyleBackColor = true;
            
            var commandPanel = new CommandPanelComponent(option);
            commandPanel.Location = new System.Drawing.Point(3, 3);
            commandPanel.Margin = new System.Windows.Forms.Padding(2, 0, 0, 0);
            commandPanel.Size = new System.Drawing.Size(720, 520);
            commandPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            commandPanel.RemoveTabPageClick += (sender, args) =>
            {
                if (MessageBox.Show("Are you sure you want to remove this option?", "Remove Option",
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    AppConfig.Options.Remove(option);
                    if (this.tabControl.SelectedIndex > -1)
                    {
                        tabControl.TabPages.Remove(tabControl.SelectedTab);
                    }
                    AppConfig.SaveOption();
                }
            };
            tabPage.Controls.Add(commandPanel);

            tabControl.TabPages.Add(tabPage);
            tabControl.SelectedTab = tabPage;
        }
    }
}