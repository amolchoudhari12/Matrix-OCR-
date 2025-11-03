using System;
using System.Collections.Generic;
using System.Text;
using Vilani.MatrixVision.Common;
using Vilani.MatrixVision.DataBase;
using Expert.Common.Library;

namespace Vilani.MatrixVision.Core
{
    public class StartupSettings
    {
        private static DataBaseAccess _dbAccess = new DataBaseAccess();


        public static List<Settings> LoadSettings()
        {
            var loadedSettings = _dbAccess.GetSettings();

            foreach (var item in loadedSettings)
            {
                switch (item.SettingName)
                {
                    case VisionConstants.SETTINGS_SHOW_REACTNGLES:
                        GlobalSettings.ShowReactanglesOnSourceImage = Convert.ToBoolean(item.SettingValue);
                        break;

                    case VisionConstants.SOURCE_IMAGES_TRIGGER_LOCATION:
                        GlobalSettings.ManulTriggerSourceImagesLocation = Convert.ToString(item.SettingValue);
                        GlobalSettings.IsManualImagesAreLoaded = false;
                        break;

                    case VisionConstants.SHOW_CPU_CYCLE_TIME:
                        GlobalSettings.IsCPUCycleTimeShow = Convert.ToBoolean(item.SettingValue);
                        break;

                    case VisionConstants.START_PORT_ON_STARTUP:
                        GlobalSettings.IsStartPortOnAppStart = Convert.ToBoolean(item.SettingValue);
                        break;

                    case VisionConstants.IMAGE_DELETE_DURATION:
                        GlobalSettings.ImageDeleteDuration = Convert.ToInt32(item.SettingValue);
                        break;

                    case VisionConstants.MEMORY_EXCEPTION_HANDLED_BY:
                        GlobalSettings.MemoryExceptionHandledBy = Convert.ToInt32(item.SettingValue);
                        break;

                    default:
                        break;
                }
            }

            return loadedSettings;

        }
    }
}
