using Expert.Common.Library.Enumrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Vilani.MatrixVision.Common;
using System.Windows.Forms;
using Vilani.MatrixVision.Core;

namespace Expert.Common.Library.Common
{
    public class ImageDeleteProcessor
    {
        public ImageDeleteProcessor()
        {

        }

        public static void DeleteImagesAfterExpiredDuration(int inDuration)
        {
            List<string> folderNames = new List<string>();
            DeleteDuration duration = (DeleteDuration)inDuration;

            switch (duration)
            {
                case DeleteDuration.Never:
                    break;
                case DeleteDuration.After1Day:
                case DeleteDuration.After2Day:
                case DeleteDuration.After3Day:
                case DeleteDuration.After4Day:
                case DeleteDuration.After5Day:
                case DeleteDuration.After6Day:
                case DeleteDuration.After7Day:
                    folderNames = GetFolderNames(inDuration);
                    break;
                case DeleteDuration.After1Week:
                    folderNames = GetFolderNames(8);
                    break;
                case DeleteDuration.After2Week:
                    folderNames = GetFolderNames(14);
                    break;
                case DeleteDuration.After3Week:
                    folderNames = GetFolderNames(21);
                    break;
                case DeleteDuration.After1Month:
                    folderNames = GetFolderNames(30);
                    break;
                case DeleteDuration.After2Month:
                    folderNames = GetFolderNames(60);
                    break;
                case DeleteDuration.After3Month:
                    folderNames = GetFolderNames(90);
                    break;
                case DeleteDuration.After4Month:
                    folderNames = GetFolderNames(120);
                    break;
                case DeleteDuration.After5Month:
                    folderNames = GetFolderNames(150);
                    break;
                case DeleteDuration.After6Month:
                    folderNames = GetFolderNames(180);
                    break;
                case DeleteDuration.After9Month:
                    folderNames = GetFolderNames(270);
                    break;
                case DeleteDuration.After1Year:
                    folderNames = GetFolderNames(365);
                    break;
                default:
                    break;
            }

            DeleteFolders(folderNames);
        }

        private static void DeleteFolders(List<string> folderNames)
        {
            foreach (var item in folderNames)
            {
                var foldToDelete = Path.Combine(Path.Combine(Application.StartupPath, VisionConstants.CAPTURED_IMG_FOLDER_NAME), item);
                var foldToDelete2 = Path.Combine(Path.Combine(Application.StartupPath, VisionConstants.PROCESS_IMG_FOLDER_NAME), item);
                if (Directory.Exists(foldToDelete))
                    System.IO.Directory.Delete(foldToDelete, true);
                if (Directory.Exists(foldToDelete2))
                    System.IO.Directory.Delete(foldToDelete2, true);
            }
        }

        private static List<string> GetFolderNames(int days)
        {
            List<string> folderNames = new List<string>();
            for (int i = 1; i <= days; i++)
            {
                folderNames.Add(System.DateTime.Now.AddDays(-i).ToString("dd-MMM-yyyy"));
            }
            return folderNames;
        }

    }
}
