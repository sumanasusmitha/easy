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
        //protected void btn_click(object sender, EventArgs e)
        //{
        //    StringBuilder sb=new StringBuilder();
        //    if(uploading.HasFile)
        //    {
        //       uploading.SaveAs(@"c:\Users\Welcome\Documents\" + uploading.FileName);
        //    }
        //}
        //  Call this function to remove the key from memory after use for security
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
            //if (File.Exists(sOutputFilename))
            //{
            //    File.Delete(sOutputFilename);
            //}

            // Create the file. 
            //using (FileStream fs = File.Create(sOutputFilename))
            //{
            //    Byte[] info = new UTF8Encoding(true).GetBytes("This is some text in the file.");
            //    // Add some information to the file.
            //    fs.Write(info, 0, info.Length);
            //}

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
            // Must be 64 bits, 8 bytes.
            // Distribute this key to the user who will decrypt this file.
            if(uploading.HasFile)
            {
                uploading.SaveAs(@"C:\Users\Welcome\Desktop\easydoc\plain\" + uploading.FileName);
            }
            string sSecretKey;
            // string s = "bfvhgnxc";
            // Get the Key for the file to Encrypt.
            sSecretKey = GenerateKey();

            // For additional security Pin the key.
            GCHandle gch = GCHandle.Alloc(sSecretKey, GCHandleType.Pinned);

            // Encrypt the file.        
            EncryptFile(@"C:\Users\Welcome\Desktop\easydoc\plain\" + uploading.FileName,
               @"C:\Users\Welcome\Desktop\easydoc\encrypt\" + uploading.FileName,
               sSecretKey);
            //Console.WriteLine("sSecretKey: {0}", sSecretKey);

            // Decrypt the file.
            DecryptFile(@"C:\Users\Welcome\Desktop\easydoc\encrypt\" + uploading.FileName,
                @"C:\Users\Welcome\Desktop\easydoc\decrypt\" + uploading.FileName,
               sSecretKey);

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


namespace CSEncryptDecrypt
{
    class Class1
    {
        
    }
}