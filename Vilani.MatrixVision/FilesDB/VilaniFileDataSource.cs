using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vilani.Files.DB.Core
{
    public class VilaniFileDataSource
    {

        public string FileDBName { get; set; }

        private string filePath;
        public string FileDBPath
        {
            get
            {
                if (string.IsNullOrEmpty(filePath))
                    filePath = Path.Combine(GetDataSourceDirectory(), FileConstants.DB_FOLDER_NAME, FileDBName);
                return filePath;

            }
            set
            {
                filePath = value;
            }
        }


        public static string GetDataSourceDirectory()
        {
            return Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), FileConstants.DATABASE_FOLDER);

        }



    }
}
