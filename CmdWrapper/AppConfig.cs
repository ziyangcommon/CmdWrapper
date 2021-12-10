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
        
        public static bool DebugMode
        {
            get => bool.Parse(GetIniInfo().iniData["global"]["debug_mode"]);
        }

        private static List<Option> _options;

        public static List<Option> Options
        {
            get
            {
                var iniInfo = GetIniInfo();
                var iniData = iniInfo.iniData;
                if (_options != null) return _options;
                _options = new List<Option>();
                foreach (var section in iniData.Sections)
                {
                    const string prefix = "option";
                    if (!section.SectionName.StartsWith(prefix)) continue;
                    var option = new Option()
                    {
                        Name = iniData[section.SectionName]["name"],
                        Command = iniData[section.SectionName]["cmd"],
                        Parameters = iniData[section.SectionName]["parameters"],
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
            var removeKeys = new List<string>();
            foreach (var section in iniInfo.iniData.Sections)
            {
                const string prefix = "option";
                if (!section.SectionName.StartsWith(prefix)) continue;
                removeKeys.Add(section.SectionName);
            }

            foreach (var removeKey in removeKeys)
            {
                iniInfo.iniData.Sections.RemoveSection(removeKey);
            }
            for (var i = 1; i <= _options.Count; i++)
            {
                var option = _options[i - 1];
                var sectionName = $"option{i}";
                iniInfo.iniData[sectionName]["name"] = option.Name;
                iniInfo.iniData[sectionName]["cmd"] = option.Command;
                iniInfo.iniData[sectionName]["parameters"] = option.Parameters;
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
        public string Parameters { get; set; }
        public string WorkingDirectory { get; set; }
    }
}