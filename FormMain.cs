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
    delegate void SetTextCallback(string text);
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
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
            
            // 
            // button run
            // 
            var buttonRun = new Button();
            buttonRun.Location = new System.Drawing.Point(25, 107);
            buttonRun.Size = new System.Drawing.Size(75, 32);
            buttonRun.TabIndex = 6;
            buttonRun.Text = "Run";
            buttonRun.UseVisualStyleBackColor = true;
            buttonRun.Tag = option;
            buttonRun.Click += ButtonRunOnClick;
            tabPage.Controls.Add(buttonRun);
            
            // 
            // button stop
            // 
            var buttonStop = new Button();
            buttonStop.Location = new System.Drawing.Point(106, 107);
            buttonStop.Size = new System.Drawing.Size(75, 32);
            buttonStop.TabIndex = 7;
            buttonStop.Text = "Stop";
            buttonStop.UseVisualStyleBackColor = true;
            buttonStop.Tag = option;
            buttonStop.Click += ButtonStopOnClick;
            tabPage.Controls.Add(buttonStop);
            
            // 
            // button save
            // 
            var buttonSave = new Button();
            buttonSave.Location = new System.Drawing.Point(187, 107);
            buttonSave.Size = new System.Drawing.Size(75, 32);
            buttonSave.TabIndex = 8;
            buttonSave.Text = "Save";
            buttonSave.UseVisualStyleBackColor = true;
            buttonSave.Tag = option;
            buttonSave.Click += ButtonSaveOnClick;
            tabPage.Controls.Add(buttonSave);
            
            // 
            // button remove
            // 
            var buttonRemove = new Button();
            buttonRemove.Location = new System.Drawing.Point(268, 107);
            buttonRemove.Size = new System.Drawing.Size(75, 32);
            buttonRemove.TabIndex = 8;
            buttonRemove.Text = "Remove";
            buttonRemove.UseVisualStyleBackColor = true;
            buttonRemove.Tag = option;
            buttonRemove.Click += ButtonRemoveOnClick;
            tabPage.Controls.Add(buttonRemove);

            // 
            // label command
            // 
            var label = new Label();
            label.Location = new System.Drawing.Point(23, 25);
            label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label.Size = new System.Drawing.Size(83, 21);
            label.TabIndex = 0;
            label.Text = "Command:";
            tabPage.Controls.Add(label);
            // 
            // txtCommand
            // 
            var txtCommand = new TextBox();
            txtCommand.Location = new System.Drawing.Point(106, 20);
            txtCommand.Size = new System.Drawing.Size(604, 26);
            txtCommand.TabIndex = 1;
            txtCommand.Text = option.Command;
            txtCommand.Tag= option;
            txtCommand.TextChanged += TxtCommandOnTextChanged;
            tabPage.Controls.Add(txtCommand);
            
            // 
            // labelWorkDir
            // 
            var labelWorkDir = new Label();
            labelWorkDir.Location = new System.Drawing.Point(23, 68);
            labelWorkDir.Size = new System.Drawing.Size(140, 23);
            labelWorkDir.TabIndex = 2;
            labelWorkDir.Text = "Working Directory:";
            tabPage.Controls.Add(labelWorkDir);
            // 
            // txtWorkDir
            // 
            var txtWorkDir = new TextBox();
            txtWorkDir.Location = new System.Drawing.Point(169, 65);
            txtWorkDir.Size = new System.Drawing.Size(541, 26);
            txtWorkDir.TabIndex = 3;
            txtWorkDir.Text = option.WorkingDirectory;
            txtWorkDir.Tag = option;
            txtWorkDir.TextChanged += TxtWorkDirOnTextChanged;
            tabPage.Controls.Add(txtWorkDir);
            
            // 
            // label output
            // 
            var labelOutput = new Label();
            labelOutput.Location = new System.Drawing.Point(23, 158);
            labelOutput.Size = new System.Drawing.Size(77, 23);
            labelOutput.TabIndex = 5;
            labelOutput.Text = "Output:";
            tabPage.Controls.Add(labelOutput);
            
            // 
            // richTextBox output
            // 
            var richTextBox = new RichTextBox();
            richTextBox.BackColor = System.Drawing.Color.Black;
            richTextBox.ForeColor = System.Drawing.Color.White;
            richTextBox.Location = new System.Drawing.Point(23, 184);
            richTextBox.Size = new System.Drawing.Size(687, 331);
            richTextBox.TabIndex = 4;
            richTextBox.Tag = option;
            tabPage.Controls.Add(richTextBox);
            
            tabControl.TabPages.Add(tabPage);
            tabControl.SelectedTab = tabPage;
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
            var action = new Action(delegate
            {
                var senderButton = (Button)sender;
                var option = (Option)senderButton.Tag;
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
        }
    }
}