using System.ComponentModel;

namespace CmdWrapper
{
    partial class CommandPanelComponent
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblCommand = new System.Windows.Forms.Label();
            this.txtCommand = new System.Windows.Forms.TextBox();
            this.lblParameters = new System.Windows.Forms.Label();
            this.txtParameters = new System.Windows.Forms.TextBox();
            this.lblWorkingDirectory = new System.Windows.Forms.Label();
            this.txtWorkingDirectory = new System.Windows.Forms.TextBox();
            this.btnRun = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.lblOutput = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.Location = new System.Drawing.Point(4, 6);
            this.lblName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(52, 23);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Name:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(63, 3);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(652, 26);
            this.txtName.TabIndex = 1;
            // 
            // lblCommand
            // 
            this.lblCommand.AutoSize = true;
            this.lblCommand.Location = new System.Drawing.Point(3, 42);
            this.lblCommand.Name = "lblCommand";
            this.lblCommand.Size = new System.Drawing.Size(82, 20);
            this.lblCommand.TabIndex = 2;
            this.lblCommand.Text = "Command:";
            // 
            // txtCommand
            // 
            this.txtCommand.Location = new System.Drawing.Point(94, 39);
            this.txtCommand.Name = "txtCommand";
            this.txtCommand.Size = new System.Drawing.Size(621, 26);
            this.txtCommand.TabIndex = 3;
            // 
            // lblParameters
            // 
            this.lblParameters.AutoSize = true;
            this.lblParameters.Location = new System.Drawing.Point(4, 79);
            this.lblParameters.Name = "lblParameters";
            this.lblParameters.Size = new System.Drawing.Size(87, 20);
            this.lblParameters.TabIndex = 4;
            this.lblParameters.Text = "Parameters:";
            // 
            // txtParameters
            // 
            this.txtParameters.Location = new System.Drawing.Point(94, 76);
            this.txtParameters.Name = "txtParameters";
            this.txtParameters.Size = new System.Drawing.Size(621, 26);
            this.txtParameters.TabIndex = 5;
            // 
            // lblWorkingDirectory
            // 
            this.lblWorkingDirectory.AutoSize = true;
            this.lblWorkingDirectory.Location = new System.Drawing.Point(6, 115);
            this.lblWorkingDirectory.Name = "lblWorkingDirectory";
            this.lblWorkingDirectory.Size = new System.Drawing.Size(131, 20);
            this.lblWorkingDirectory.TabIndex = 6;
            this.lblWorkingDirectory.Text = "WorkingDirectory:";
            // 
            // txtWorkingDirectory
            // 
            this.txtWorkingDirectory.Location = new System.Drawing.Point(140, 112);
            this.txtWorkingDirectory.Name = "txtWorkingDirectory";
            this.txtWorkingDirectory.Size = new System.Drawing.Size(575, 26);
            this.txtWorkingDirectory.TabIndex = 7;
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(10, 155);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(75, 28);
            this.btnRun.TabIndex = 8;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(91, 155);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 28);
            this.btnStop.TabIndex = 9;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(253, 155);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 28);
            this.btnRemove.TabIndex = 10;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(172, 155);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 28);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.Black;
            this.richTextBox1.ForeColor = System.Drawing.Color.White;
            this.richTextBox1.Location = new System.Drawing.Point(3, 225);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(712, 290);
            this.richTextBox1.TabIndex = 12;
            this.richTextBox1.Text = "";
            // 
            // lblOutput
            // 
            this.lblOutput.AutoSize = true;
            this.lblOutput.Location = new System.Drawing.Point(6, 202);
            this.lblOutput.Name = "lblOutput";
            this.lblOutput.Size = new System.Drawing.Size(60, 20);
            this.lblOutput.TabIndex = 13;
            this.lblOutput.Text = "Output:";
            // 
            // CommandPanelComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lblOutput);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.txtWorkingDirectory);
            this.Controls.Add(this.lblWorkingDirectory);
            this.Controls.Add(this.txtParameters);
            this.Controls.Add(this.lblParameters);
            this.Controls.Add(this.txtCommand);
            this.Controls.Add(this.lblCommand);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblName);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (134)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "CommandPanelComponent";
            this.Size = new System.Drawing.Size(718, 518);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblName;

        #endregion

        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblCommand;
        private System.Windows.Forms.TextBox txtCommand;
        private System.Windows.Forms.Label lblParameters;
        private System.Windows.Forms.TextBox txtParameters;
        private System.Windows.Forms.Label lblWorkingDirectory;
        private System.Windows.Forms.TextBox txtWorkingDirectory;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label lblOutput;
    }
}