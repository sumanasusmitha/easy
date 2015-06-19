using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.Text;


namespace file_encrypt_des
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        
        [System.Runtime.InteropServices.DllImport("KERNEL32.DLL", EntryPoint = "RtlZeroMemory")]
        public static extern bool ZeroMemory(IntPtr Destination, int Length);

        // Function to Generate a 64 bits Key.
        static string GenerateKey()
        {
            // Create an instance of Symetric Algorithm. Key and IV is generated automatically.
            DESCryptoServiceProvider desCrypto = (DESCryptoServiceProvider)DESCryptoServiceProvider.Create();

            // Use the Automatically generated key for Encryption. 
            return ASCIIEncoding.ASCII.GetString(desCrypto.Key);
        }

        static void EncryptFile(string sInputFilename,
           string sOutputFilename,
           string sKey)
        {
            FileStream fsInput = new FileStream(sInputFilename,
               FileMode.Open,
               FileAccess.Read);

            FileStream fsEncrypted = new FileStream(sOutputFilename,
               FileMode.Create,
               FileAccess.Write);
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            ICryptoTransform desencrypt = DES.CreateEncryptor();
            CryptoStream cryptostream = new CryptoStream(fsEncrypted,
               desencrypt,
               CryptoStreamMode.Write);

            byte[] bytearrayinput = new byte[fsInput.Length];
            fsInput.Read(bytearrayinput, 0, bytearrayinput.Length);
            cryptostream.Write(bytearrayinput, 0, bytearrayinput.Length);
            cryptostream.Close();
            fsInput.Close();
            fsEncrypted.Close();
        }
        static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        static string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }
        static void DecryptFile(string sInputFilename,
           string sOutputFilename,
           string sKey)
        {
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            //A 64 bit key and IV is required for this provider.
            //Set secret key For DES algorithm.
            DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            //Set initialization vector.
            DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            //Console.WriteLine("iniVector:{0}", DES.Key);
            //Create a file stream to read the encrypted file back.
            FileStream fsread = new FileStream(sInputFilename,
               FileMode.Open,
               FileAccess.Read);
            //Create a DES decryptor from the DES instance.
            ICryptoTransform desdecrypt = DES.CreateDecryptor();
            //Create crypto stream set to read and do a 
            //DES decryption transform on incoming bytes.
            CryptoStream cryptostreamDecr = new CryptoStream(fsread,
               desdecrypt,
               CryptoStreamMode.Read);
            //Print the contents of the decrypted file.
            StreamWriter fsDecrypted = new StreamWriter(sOutputFilename);
            fsDecrypted.Write(new StreamReader(cryptostreamDecr).ReadToEnd());
            fsDecrypted.Flush();
            fsDecrypted.Close();
        }

        protected void btn_click(object sender, EventArgs e)
        {
            if (uploading.HasFile)
            {
                uploading.SaveAs(@"C:\Users\Welcome\Desktop\easydoc\plain\" + uploading.FileName);
            }
            using (StreamReader sr = new StreamReader(@"C:\Users\Welcome\Desktop\easydoc\plain\" + uploading.FileName))
            {
                string line = sr.ReadToEnd();
                byte[] bytes = GetBytes(line);
                  string x  = Convert.ToBase64String(bytes);
                  System.IO.File.WriteAllText(@"C:\Users\Welcome\Desktop\easydoc\plain1\" + uploading.FileName, x);
            }
           
            string sSecretKey;
            sSecretKey = GenerateKey();
            GCHandle gch = GCHandle.Alloc(sSecretKey, GCHandleType.Pinned);

            // Encrypt the file.        
            EncryptFile(@"C:\Users\Welcome\Desktop\easydoc\plain1\" + uploading.FileName,
               @"C:\Users\Welcome\Desktop\easydoc\encrypt\" + uploading.FileName,
               sSecretKey);
            // Decrypt the file.
            DecryptFile(@"C:\Users\Welcome\Desktop\easydoc\encrypt\" + uploading.FileName,
                @"C:\Users\Welcome\Desktop\easydoc\decrypt\" + uploading.FileName,
               sSecretKey);
            using (StreamReader o=new StreamReader(@"C:\Users\Welcome\Desktop\easydoc\decrypt\" + uploading.FileName))
            {

                byte[] binary=System.Convert.FromBase64String(o.ReadToEnd());
                string strin = GetString(binary);
                System.IO.File.WriteAllText(@"C:\Users\Welcome\Desktop\easydoc\decrypt1\" + uploading.FileName, strin);

            }
            // Remove the Key from memory. 
            ZeroMemory(gch.AddrOfPinnedObject(), sSecretKey.Length * 2);
            gch.Free();
            // Console.ReadKey();
        }
        private void EncryptFile(Stream stream, string p, string sSecretKey)
        {
            throw new NotImplementedException();
        }
    }
}
