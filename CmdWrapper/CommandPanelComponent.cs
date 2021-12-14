using System;
using System.Drawing;
using System.Windows.Forms;

namespace CmdWrapper
{
    public partial class CommandPanelComponent : UserControl
    {
        public CommandPanelComponent(Option option)
        {
            InitializeComponent();
            this.Option = option;
            this.InitData();
        }
        
        public delegate void RemoveTabPageEventHandler(Option sender, EventArgs e);

        public event RemoveTabPageEventHandler RemoveTabPageClick;

        private void SendRemoveTabPageClick(Option sender, EventArgs e)
        {
            if (RemoveTabPageClick != null)
            {
                this.RemoveTabPageClick(sender, e);
            }
        }

        private void InitData()
        {
            StdOutputReceiver.StdOutputReceived += StdOutputReceiverOnStdOutputReceived;
            StdOutputReceiver.StdErrorReceived += StdOutputReceiverOnStdErrorReceived;
            this.txtName.Text = this.Option.Name;
            this.txtCommand.Text = this.Option.Command;
            this.txtParameters.Text = this.Option.Parameters;
            this.txtWorkingDirectory.Text = this.Option.WorkingDirectory;
        }

        private void StdOutputReceiverOnStdErrorReceived(Option option, string output)
        {
            this.BeginInvoke(new Action(delegate
            {
                if (string.IsNullOrEmpty(output)) return;
                this.richTextBox.SelectionColor= Color.Red;
                this.richTextBox.SelectionBackColor = Color.Black;
                SetOutputText(output);
            }));
        }

        private void StdOutputReceiverOnStdOutputReceived(Option option, string output)
        {
            this.BeginInvoke(new Action(delegate
            {
                if (string.IsNullOrEmpty(output)) return;
                this.richTextBox.SelectionColor= Color.GreenYellow;
                this.richTextBox.SelectionBackColor = Color.Black;
                SetOutputText(output);
            }));
        }

        private void SetOutputText(string output)
        {
            //if output is not end with \n, add \n
            if (!output.EndsWith("\n"))
            {
                output += "\n";
            }

            this.richTextBox.AppendText(output);
            this.richTextBox.ScrollToCaret();
            this.richTextBox.ResumeLayout();
        }
        
        public Option Option { get; set; }

        private void btnRun_Click(object sender, EventArgs e)
        {
            var action = new Action(delegate
            {
                CmdHelper.RunExternalExe(Option);
            });
            action.BeginInvoke(delegate(IAsyncResult ar)
            {
            }, null);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (CmdHelper.WorkingProcesses.ContainsKey(Option.Id))
            {
                var process = CmdHelper.WorkingProcesses[Option.Id];
                process.Kill();
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            SendRemoveTabPageClick(this.Option, e);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Option.Name=this.txtName.Text;
            this.Option.Command = this.txtCommand.Text;
            this.Option.Parameters = this.txtParameters.Text;
            this.Option.WorkingDirectory = this.txtWorkingDirectory.Text;
            AppConfig.SaveOption();
            MessageBox.Show("Save Success", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}