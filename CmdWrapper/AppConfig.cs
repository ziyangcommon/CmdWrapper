using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using IniParser;
using IniParser.Model;

namespace CmdWrapper
{
    public static class AppConfig
    {
        private static readonly string ConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.ini");

        private static (IniData iniData, FileIniDataParser parser) GetIniInfo()
        {
            var parser = new FileIniDataParser();
            var ini = parser.ReadFile(ConfigPath, Encoding.UTF8);
            return (ini, parser);
        }

        public static bool ShowNotifyIcon
        {
            get => bool.Parse(GetIniInfo().iniData["global"]["show_notify_icon"]);
            set
            {
                var iniInfo = GetIniInfo();
                iniInfo.iniData["global"]["show_notify_icon"] = value.ToString();
                iniInfo.parser.WriteFile(ConfigPath, iniInfo.iniData, Encoding.UTF8);
            }
        }

        private static ObservableCollection<Option> _options;

        public static ObservableCollection<Option> Options
        {
            get
            {
                var iniInfo = GetIniInfo();
                var iniData = iniInfo.iniData;
                if (_options == null)
                {
                    _options = new ObservableCollection<Option>();
                    _options.CollectionChanged += (sender, args) =>
                    {
                        var ini = GetIniInfo();
                        for (var i = 1; i <= _options.Count; i++)
                        {
                            var option = _options[i - 1];
                            var sectionName = $"option{i}";
                            iniInfo.iniData[sectionName]["name"] = option.Name;
                            iniInfo.iniData[sectionName]["cmd"] = option.Command;
                            iniInfo.iniData[sectionName]["working_dir"] = option.WorkingDirectory;
                        }

                        iniInfo.parser.WriteFile(ConfigPath, iniInfo.iniData, Encoding.UTF8);
                    };
                }

                foreach (var section in iniData.Sections)
                {
                    const string prefix = "option";
                    if (!section.SectionName.StartsWith(prefix)) continue;
                    var option = new Option()
                    {
                        Name = iniData[section.SectionName]["name"],
                        Command = iniData[section.SectionName]["cmd"],
                        WorkingDirectory = iniData[section.SectionName]["working_dir"],
                    };
                    _options.Add(option);
                }

                return _options;
            }
        }

        public static void SaveOption()
        {
            var iniInfo = GetIniInfo();
            for (var i = 1; i <= _options.Count; i++)
            {
                var option = _options[i - 1];
                var sectionName = $"option{i}";
                iniInfo.iniData[sectionName]["name"] = option.Name;
                iniInfo.iniData[sectionName]["cmd"] = option.Command;
                iniInfo.iniData[sectionName]["working_dir"] = option.WorkingDirectory;
            }

            iniInfo.parser.WriteFile(ConfigPath, iniInfo.iniData, Encoding.UTF8);
        }
    }

    public class Option
    {
        public Option()
        {
        }
        public string Id { get; set; }= Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string Command { get; set; }
        public string WorkingDirectory { get; set; }
    }
}