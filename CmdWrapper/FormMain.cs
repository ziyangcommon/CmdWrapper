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
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            //subscribe to the std output received event
            StdOutputReceiver.StdOutputReceived+= StdOutputReceiverOnStdOutputReceived;
        }

        private void StdOutputReceiverOnStdOutputReceived(Option option,string output)
        {
            foreach (TabPage page in this.tabControl.TabPages)
            {
                var txt=page.Controls.OfType<RichTextBox>().FirstOrDefault(x => (x.Tag as Option)?.Id == option.Id);
                if (txt != null)
                {
                    this.BeginInvoke(new Action(delegate
                    {
                        txt.Text += output;
                    }));
                }
            }
        }
        
        private void TxtWorkDirOnTextChanged(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null)
            {
                var option = textBox.Tag as Option;
                if (option != null) option.WorkingDirectory = textBox.Text;
            }
        }

        private void TxtCommandOnTextChanged(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null)
            {
                var option = textBox.Tag as Option;
                if (option != null)
                {
                    option.Command = textBox.Text;
                }
            }
        }

        private void ButtonRemoveOnClick(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to remove this option?", "Remove Option", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var button = (Button)sender;
                var option = (Option)button.Tag;
                AppConfig.Options.Remove(option);
                if (this.tabControl.SelectedIndex > -1)
                {
                    tabControl.TabPages.Remove(tabControl.SelectedTab);
                }
            }
        }

        private void ButtonSaveOnClick(object sender, EventArgs e)
        {
            AppConfig.SaveOption();
            MessageBox.Show("Saved", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ButtonStopOnClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ButtonRunOnClick(object sender, EventArgs e)
        {
            var senderButton = (Button)sender;
            var option = (Option)senderButton.Tag;
            var action = new Action(delegate
            {
                CmdHelper.RunExternalExe(option);
            });
            action.BeginInvoke(delegate(IAsyncResult ar)
            {
            }, null);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            var options = AppConfig.Options;
            foreach (var option in options)
            {
                CreateTabPage(option);
            }
        }

        private void tabControl_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var option = new Option()
            {
                Name = "New Option",
            };
            AppConfig.Options.Add(option);
            CreateTabPage(option);
            AppConfig.SaveOption();
        }
    }
}