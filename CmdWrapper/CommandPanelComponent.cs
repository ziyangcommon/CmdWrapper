using System;
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
            this.txtName.Text = this.Option.Name;
            this.txtCommand.Text = this.Option.Command;
            this.txtParameters.Text = this.Option.Parameters;
            this.txtWorkingDirectory.Text = this.Option.WorkingDirectory;
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