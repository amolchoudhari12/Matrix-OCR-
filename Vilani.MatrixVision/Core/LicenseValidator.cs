using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Vilani.MatrixVision.Core
{
    public class SerialKeyGenrator
    {
        public string Architecture { get; set; }
        public string Caption { get; set; }
        public string Family { get; set; }
        public string ProcessorID { get; set; }
        public string VolumeSerialNumber { get; set; }


        public SerialKeyGenrator()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor");


            foreach (ManagementObject queryObj in searcher.Get())
            {

                this.Architecture = Convert.ToString(queryObj["Architecture"]);
                this.Caption = Convert.ToString(queryObj["Caption"]);
                this.Family = Convert.ToString(queryObj["Family"]);
                this.ProcessorID = Convert.ToString(queryObj["ProcessorId"]);

            }


            ManagementClass managementClass = new ManagementClass("win32_processor");
            ManagementObjectCollection instances = managementClass.GetInstances();
            using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = instances.GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    ManagementObject managementObject = (ManagementObject)enumerator.Current;
                    this.ProcessorID = managementObject.Properties["processorID"].Value.ToString();
                }
            }
            ManagementObject managementObject2 = new ManagementObject("win32_logicaldisk.deviceid=\"C:\"");
            managementObject2.Get();
            this.VolumeSerialNumber = managementObject2["VolumeSerialNumber"].ToString();

        }


        public string GetSerialKey()
        {
            var activateKey = string.Format("{0}#{1}#{2}#{3}#{4}",
               LicenseValidator.Encrypt(Architecture, LicenseValidator.PublicActivateKey),
                 LicenseValidator.Encrypt(Caption, LicenseValidator.PublicActivateKey),
                 LicenseValidator.Encrypt(Family, LicenseValidator.PublicActivateKey),
                  LicenseValidator.Encrypt(ProcessorID, LicenseValidator.PublicActivateKey),
                 LicenseValidator.Encrypt(VolumeSerialNumber, LicenseValidator.PublicActivateKey));

            return activateKey;
        }

        public bool IsSerialKeyValid()
        {


            try
            {
                string readText = File.ReadAllText(Path.Combine(Application.StartupPath, "serialkey"));

                string[] array = readText.Split(new char[]
			{
				'#'
			});

                var architecture = LicenseValidator.Decrypt(array[0], LicenseValidator.PublicActivateKey);
                var caption = LicenseValidator.Decrypt(array[1], LicenseValidator.PublicActivateKey);
                var family = LicenseValidator.Decrypt(array[2], LicenseValidator.PublicActivateKey);
                var processorID = LicenseValidator.Decrypt(array[3], LicenseValidator.PublicActivateKey);
                var volumeSerialNumber = LicenseValidator.Decrypt(array[4], LicenseValidator.PublicActivateKey);

                if (this.Architecture == architecture && this.Caption == caption
                    && this.Family == family && this.ProcessorID == processorID && this.VolumeSerialNumber == volumeSerialNumber)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (CryptographicException ex)
            {
                return false;
            }

            catch (Exception ex)
            {
                return false;
            }

            return false;
        }

    }
    public class LicenseValidator
    {
        public static string PublicActivateKey = "9009BEHERENOWTHISISITAWESOME40";

        public static string Encrypt(string toEncrypt, string key)
        {
            bool useHashing = true;
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            //System.Windows.Forms.MessageBox.Show(key);
            //If hashing use get hashcode regards to your key
            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //Always release the resources and flush data
                // of the Cryptographic service provide. Best Practice

                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes.
            //We choose ECB(Electronic code Book)
            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)

            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            //transform the specified region of bytes array to resultArray
            byte[] resultArray =
              cTransform.TransformFinalBlock(toEncryptArray, 0,
              toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor
            tdes.Clear();
            //Return the encrypted data into unreadable string format
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public static string Decrypt(string cipherString, string key)
        {
            bool useHashing = true;
            byte[] keyArray;
            //get the byte code of the string

            byte[] toEncryptArray = Convert.FromBase64String(cipherString);


            if (useHashing)
            {
                //if hashing was used get the hash code with regards to your key
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //release any resource held by the MD5CryptoServiceProvider

                hashmd5.Clear();
            }
            else
            {
                //if hashing was not implemented get the byte code of the key
                keyArray = UTF8Encoding.UTF8.GetBytes(key);
            }

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes. 
            //We choose ECB(Electronic code Book)

            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(
                                 toEncryptArray, 0, toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor                
            tdes.Clear();
            //return the Clear decrypted TEXT
            return UTF8Encoding.UTF8.GetString(resultArray);
        }


        public static int ValidateLicenseKey(string cipherString, string encryptionKey, ref int id, ref int activationNumber)
        {
            id = -1;
            var thisKey = Decrypt(encryptionKey, PublicActivateKey);
            string[] array = cipherString.Split(new char[]
			{
				'#'
			});

            try
            {
                var result1 = Decrypt(array[0], thisKey);
                var result2 = Decrypt(array[1], thisKey);
                var result3 = Decrypt(array[2], thisKey);
                var result4 = Decrypt(array[3], thisKey);
                var result5 = Decrypt(array[4], thisKey);
                var result6 = array[5];

                var result7 = Convert.ToDateTime(result4);
                var result8 = Decrypt(array[6], thisKey);
                if (result7.Date.ToString("dd/MMM/yyyy") == System.DateTime.Now.ToString("dd/MMM/yyyy"))
                {
                    id = Convert.ToInt32(result5);
                    activationNumber = Convert.ToInt32(result6);
                }
                if (result8 != "Vilani.Vision.Inspection.System")
                {
                    id = -1;
                    throw new Exception();
                }

               
            }
            catch (CryptographicException ex)
            {
                return -1;
            }

            catch (Exception ex)
            {
                return -1;
            }
            return -1;

        }


    }
}
