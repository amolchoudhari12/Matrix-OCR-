using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Vilani.MatrixVision.Core;


namespace Vilani.MatrixVision.Common
{
    /// <summary>
    /// 
    /// </summary>
    public static class DirectorFileHelper
    {
        public static void CreateReferenceImageFolder()
        {
            var refPathName = Path.Combine(Application.StartupPath, VisionConstants.REFERENCE_IMG_FOLDER_NAME);
            if (!Directory.Exists(refPathName))
            {
                Directory.CreateDirectory(refPathName);
            }
        }

        private static string GetTodaysDate()
        {
            return DateTime.Now.ToString("dd-MMM-yyyy");
        }


        public static void CreateCaputredImageFolder()
        {
            var capPathName = Path.Combine(Application.StartupPath, VisionConstants.CAPTURED_IMG_FOLDER_NAME);
            var todaysDateFolder = Path.Combine(capPathName, GetTodaysDate());
            if (!Directory.Exists(todaysDateFolder))
            {
                Directory.CreateDirectory(todaysDateFolder);
            }
        }

        public static void CreateProcessedImageFolder()
        {
            var processedPathName = Path.Combine(Application.StartupPath, VisionConstants.PROCESS_IMG_FOLDER_NAME);
            var todaysDateFolder = Path.Combine(processedPathName, GetTodaysDate());
            if (!Directory.Exists(todaysDateFolder))
            {
                Directory.CreateDirectory(todaysDateFolder);
            }
        }

        public static string GetReferenceImageFolderPath
        {
            get
            {
                var path = Path.Combine(Application.StartupPath, VisionConstants.REFERENCE_IMG_FOLDER_NAME);
                return path;
            }
        }

        public static string GetCapturedImageFolderPath
        {
            get
            {
                var processedPathName = Path.Combine(Application.StartupPath, VisionConstants.CAPTURED_IMG_FOLDER_NAME);
                var todaysDateFolder = Path.Combine(processedPathName, GetTodaysDate());
                if (!Directory.Exists(todaysDateFolder))
                {
                    Directory.CreateDirectory(todaysDateFolder);
                }
                return todaysDateFolder;
            }
        }

        public static string GetProcessedImageFolderPath
        {
            get
            {
                var processedPathName = Path.Combine(Application.StartupPath, VisionConstants.PROCESS_IMG_FOLDER_NAME);
                var todaysDateFolder = Path.Combine(processedPathName, GetTodaysDate());
                if (!Directory.Exists(todaysDateFolder))
                {
                    Directory.CreateDirectory(todaysDateFolder);
                }
                return todaysDateFolder;
            }
        }

        public static string GetCapturedImageFilePath(string fileName)
        {
            return Path.Combine(GetCapturedImageFolderPath, fileName);
        }

        public static string GetReferenceImageFilePath(string fileName)
        {
            return Path.Combine(GetReferenceImageFolderPath, fileName);
        }

        public static string GetPorcessedImageFilePath(string fileName)
        {
            return Path.Combine(GetProcessedImageFolderPath, fileName);
        }

        public static void SaveReferenceImage(Image imageToSave, string fileName)
        {
            var pathToSave = Path.Combine(GetReferenceImageFolderPath, fileName);
            imageToSave.Save(pathToSave, ImageFormat.Jpeg);
        }

        public static void SaveCapturedImage(Image imageToSave, string fileName)
        {
            var pathToSave = Path.Combine(GetCapturedImageFolderPath, fileName);
            imageToSave.Save(pathToSave, ImageFormat.Jpeg);
        }

        public static void SaveProcessedImage(Image imageToSave, string fileName)
        {
            var pathToSave = Path.Combine(GetProcessedImageFolderPath, fileName);
            imageToSave.Save(pathToSave, ImageFormat.Jpeg);
        }

        public static void SaveReferenceImage(Image imageToSave, string imageName, string areaOfInterest)
        {
            var fileName = string.Format("{0}_{1}.jpg", imageName, areaOfInterest.Replace(',', '.'));
            var pathToSave = Path.Combine(GetReferenceImageFolderPath, fileName);
            imageToSave.Save(pathToSave, ImageFormat.Jpeg);
        }


        public static string GetContentFilePath(string fileName)
        {
            return Path.Combine(Path.Combine(Application.StartupPath, VisionConstants.CONTENT_FOLDER_NAME), fileName);
        }


    }
}
