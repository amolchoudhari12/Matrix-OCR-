using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vilani.Files.DB.Core
{
    public static class ProtoContractSerializer
    {
        public static void Serialize(object data, string fileName)
        {
            try
            {
                using (var fs = File.Create(fileName))
                {
             
                    Serializer.Serialize<object>(fs, data);
                   

                   
                }
            }
            catch (DirectoryNotFoundException ex)
            {
                var directory = Path.GetDirectoryName(fileName);
                Directory.CreateDirectory(directory);
                using (var fs = File.Create(fileName))
                {
                    Serializer.Serialize(fs, data);
                }
            }
        }
    }
}
