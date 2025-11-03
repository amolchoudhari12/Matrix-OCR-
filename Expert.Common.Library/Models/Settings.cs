using System;
using System.Collections.Generic;
using System.Text;


namespace Expert.Common.Library
{
    public class Settings
    {

        public Settings()
        {

        }
        public Settings(string name, string value)
        {
            this.SettingName = name;
            this.SettingValue = value;
        }

        public int SettingID { get; set; }
        public string SettingName { get; set; }
        public object SettingValue { get; set; }

    }
}
