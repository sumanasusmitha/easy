using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.IO;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
using System.Security;
using System.Runtime.InteropServices;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace file_encrypt_des
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void DecryptFile(object sender, EventArgs e)
        {
            int key_id = 0;
            //Get the Input File Name and Extension
            string fileName = Path.GetFileNameWithoutExtension(upload.PostedFile.FileName);
            string fileExtension = Path.GetExtension(upload.PostedFile.FileName);
            string encrypt_key="";
            //Build the File Path for the original (input) and the decrypted (output) file
            string input = Server.MapPath("~/Files/") + fileName + fileExtension;
            string output = Server.MapPath("~/Files/") + fileName + "_dec" + fileExtension;
            string[] filename = fileName.Split('_');
            /////////////////////////////////////////////////////////////////////////////////
            MySqlConnection connect = new MySqlConnection();
            MySqlCommand cmd;
            connect.ConnectionString = "server=localhost;user id=root;password=A9866145009;port=8000;database=world";
            connect.Open();
            cmd = connect.CreateCommand();
            cmd.CommandText = "SELECT * FROM new_schema.file where filepath='"+ filename[0]+" '  ";
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                key_id = reader.GetInt32(2);
            }
            connect.Close();
            ////////////////////////////////////////////////////////////////////////////////

            /////////////////////////////////////////////////////////////////////////////////
            MySqlConnection con = new MySqlConnection();
            MySqlCommand command;
            con.ConnectionString = "server=localhost;user id=root;password=A9866145009;port=8000;database=new_schema";
            con.Open();
            command = con.CreateCommand();
            command.CommandText = "SELECT * FROM new_schema.key where key_id='" + key_id + "'   ";
            MySqlDataReader read = command.ExecuteReader();
            while(read.Read())
            {
                encrypt_key += read[3].ToString();
            }
            con.Close();
            //////////////////////////////////////////////////////////////////////////////////
            //Save the Input File, Decrypt it and save the decrypted file in output path.
            upload.SaveAs(@"C:\Users\Welcome\Desktop\easydoc\plain\" + "input");
            this.Decrypt(@"C:\Users\Welcome\Desktop\easydoc\plain\" + "input", @"C:\Users\Welcome\Desktop\easydoc\plain\" + "output",encrypt_key);
            
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

        private void Decrypt(string inputFilePath, string outputfilePath,string keyed)
        {
            string EncryptionKey = keyed;      
            
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
            int id = 0;
            string encrypt_key = "";
            /////////////////////////////////////////////////////////////////////////////////////////////
            string tyme = DateTime.Now.TimeOfDay.ToString();
            string[] array = tyme.Split(':');
            tyme = array[0] + array[1];
            int time = Convert.ToInt32(tyme);
            /////////////////////////////////////////////////////////////////////////////////////////////
            MySqlConnection connect = new MySqlConnection();
            MySqlCommand cmd;
            connect.ConnectionString = "server=localhost;user id=root;password=A9866145009;port=8000;database=world";
            connect.Open();
            cmd = connect.CreateCommand();
            cmd.CommandText = "SELECT * FROM new_schema.key where time1<"+time+" AND time2>"+time+"";
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                id = reader.GetInt32(0);
                encrypt_key += reader[3].ToString();
            }
            connect.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////
            //Get the Input File Name and Extension.
            string fileName = Path.GetFileNameWithoutExtension(upload.PostedFile.FileName);
            string fileExtension = Path.GetExtension(upload.PostedFile.FileName);

            //Build the File Path for the original (input) and the encrypted (output) file.
            string input = Server.MapPath("~/Files/") + fileName + fileExtension;
            string output = Server.MapPath("~/Files/") + fileName + "_enc" + fileExtension;

            //Save the Input File, Encrypt it and save the encrypted file in output path.
            upload.SaveAs(@"C:\Users\Welcome\Desktop\easydoc\plain\" + "input");
            this.Encrypt(@"C:\Users\Welcome\Desktop\easydoc\plain\" + "input", @"C:\Users\Welcome\Desktop\easydoc\plain\" + "output",encrypt_key);
            /////////////////////////////////////////////////////////////////////////////////////////////////
            //string fname = fileExtension + "_enc";
            //MySqlConnection connect2 = new MySqlConnection();
            ////MySqlCommand cmd2;
            //connect2.ConnectionString = "server=localhost;user id=root;password=A9866145009;port=8000;database=world";
            //connect2.Open();
            ////cmd2 = connect2.CreateCommand();
            //string inserted = "INSERT INTO `new_schema`.`file` (`filepath`, `enc_id`) VALUES ("+fileName+", "+id+")";
            //MySqlCommand cmd2 = new MySqlCommand(inserted, connect2);
            ////MySqlDataReader reader1 = cmd2.ExecuteReader();
            ////MySqlDataReader reader2 = cmd2.ExecuteReader();
            
            //connect2.Close();
            ////////////////////////////////////////////////////////////////////////////////////////////////
            string inserted = "INSERT INTO `new_schema`.`file` (`filepath`, `enc_id`) VALUES ('" + fileName +"', '" + id + "')";
            MySqlConnection connect1 = new MySqlConnection();
            connect1.ConnectionString = "server=localhost;user id=root;password=A9866145009;port=8000;database=new_schema";
            connect1.Open();

            MySqlCommand cmd2 = new MySqlCommand(inserted, connect1);
            MySqlDataReader reader1 = cmd2.ExecuteReader();

            connect1.Close();
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

        private void Encrypt(string inputFilePath, string outputfilePath,string key1)
        {
            string EncryptionKey = key1;
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