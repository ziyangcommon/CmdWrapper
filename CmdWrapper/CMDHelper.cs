using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace CmdWrapper
{
    public static class CmdHelper
    {
        public static readonly Dictionary<string,Process> WorkingProcesses = new Dictionary<string,Process>();
        public static void RunExternalExe(Option option)
        {
            if (WorkingProcesses.ContainsKey(option.Id))
            {
                WorkingProcesses[option.Id].Kill();
                WorkingProcesses.Remove(option.Id);
            }
            var process = new Process();
            WorkingProcesses.Add(option.Id,process);

            process.StartInfo.FileName = "cmd.exe";

            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.RedirectStandardOutput = true;
            var stdOutput = new StringBuilder();
            process.OutputDataReceived += (sender, args) =>
            {
                StdOutputReceiver.SendStdOutput(option,args.Data);
            };
            process.ErrorDataReceived+= (sender, args) =>
            {
                StdOutputReceiver.SendStdErrorReceived(option,args.Data);
            };
            process.Exited += (sender, args) =>
            {
                StdOutputReceiver.SendProcessExited(option,"exited");
            };

            string stdError = null;
            try
            {
                process.Start();
                process.StandardInput.WriteLine($"cd {option.WorkingDirectory}");
                process.StandardInput.WriteLine(option.Command);
                process.BeginOutputReadLine();
                stdError = process.StandardError.ReadToEnd();
                process.WaitForExit();
            }
            catch (Exception e)
            {
                StdOutputReceiver.SendStdOutput(option,e.Message);
            }

            if (process.ExitCode == 0)
            {
                StdOutputReceiver.SendStdOutput(option,"exited");
            }
            else
            {
                var message = new StringBuilder();

                if (!string.IsNullOrEmpty(stdError))
                {
                    message.AppendLine(stdError);
                }

                if (stdOutput.Length != 0)
                {
                    message.AppendLine("Std output:");
                    message.AppendLine(stdOutput.ToString());
                }

                StdOutputReceiver.SendStdOutput(option,$"finished with exit code = {process.ExitCode}: {message}");
            }
        }
    }
}