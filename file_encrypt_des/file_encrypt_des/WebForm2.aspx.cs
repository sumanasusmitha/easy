using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.IO;


namespace file_encrypt_des
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void DecryptFile(object sender, EventArgs e)
        {
            //Get the Input File Name and Extension
            string fileName = Path.GetFileNameWithoutExtension(upload.PostedFile.FileName);
            string fileExtension = Path.GetExtension(upload.PostedFile.FileName);

            //Build the File Path for the original (input) and the decrypted (output) file
            string input = Server.MapPath("~/Files/") + fileName + fileExtension;
            string output = Server.MapPath("~/Files/") + fileName + "_dec" + fileExtension;

            //Save the Input File, Decrypt it and save the decrypted file in output path.
            upload.SaveAs(@"C:\Users\Welcome\Desktop\easydoc\plain\" + "input");
            this.Decrypt(@"C:\Users\Welcome\Desktop\easydoc\plain\" + "input", @"C:\Users\Welcome\Desktop\easydoc\plain\" + "output");

            //Download the Decrypted File.
            Response.Clear();
            Response.ContentType = upload.PostedFile.ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(output));
            Response.WriteFile(@"C:\Users\Welcome\Desktop\easydoc\plain\" + "output");
            Response.Flush();

            //Delete the original (input) and the decrypted (output) file.
            File.Delete(@"C:\Users\Welcome\Desktop\easydoc\plain\" + "input");
            File.Delete(@"C:\Users\Welcome\Desktop\easydoc\plain\" + "output");

            Response.End();
        }

        private void Decrypt(string inputFilePath, string outputfilePath)
        {
            string EncryptionKey = "";      
            
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (FileStream fsInput = new FileStream(inputFilePath, FileMode.Open))
                {
                    using (CryptoStream cs = new CryptoStream(fsInput, encryptor.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        using (FileStream fsOutput = new FileStream(outputfilePath, FileMode.Create))
                        {
                            int data;
                            while ((data = cs.ReadByte()) != -1)
                            {
                                fsOutput.WriteByte((byte)data);
                            }
                        }
                    }
                }
            }
        }

        protected void EncryptFile(object sender, EventArgs e)
        {
            //Get the Input File Name and Extension.
            string fileName = Path.GetFileNameWithoutExtension(upload.PostedFile.FileName);
            string fileExtension = Path.GetExtension(upload.PostedFile.FileName);

            //Build the File Path for the original (input) and the encrypted (output) file.
            string input = Server.MapPath("~/Files/") + fileName + fileExtension;
            string output = Server.MapPath("~/Files/") + fileName + "_enc" + fileExtension;

            //Save the Input File, Encrypt it and save the encrypted file in output path.
            upload.SaveAs(@"C:\Users\Welcome\Desktop\easydoc\plain\" + "input");
            this.Encrypt(@"C:\Users\Welcome\Desktop\easydoc\plain\" + "input", @"C:\Users\Welcome\Desktop\easydoc\plain\" + "output");

            //Download the Encrypted File.
            Response.ContentType = upload.PostedFile.ContentType;
            Response.Clear();
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(output));
            Response.WriteFile(@"C:\Users\Welcome\Desktop\easydoc\plain\" + "output");
            Response.Flush();

            //Delete the original (input) and the encrypted (output) file.
            File.Delete(@"C:\Users\Welcome\Desktop\easydoc\plain\" + "input");
            File.Delete(@"C:\Users\Welcome\Desktop\easydoc\plain\" + "output");

            Response.End();
        }

        private void Encrypt(string inputFilePath, string outputfilePath)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (FileStream fsOutput = new FileStream(outputfilePath, FileMode.Create))
                {
                    using (CryptoStream cs = new CryptoStream(fsOutput, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        using (FileStream fsInput = new FileStream(inputFilePath, FileMode.Open))
                        {
                            int data;
                            while ((data = fsInput.ReadByte()) != -1)
                            {
                                cs.WriteByte((byte)data);
                            }
                        }
                    }
                }
            }
        }

    }
}