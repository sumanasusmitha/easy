using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
namespace file_encrypt_des
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        string write = "";
        public void decrypt(object sender, EventArgs e)
        {
            MySqlConnection decr = new MySqlConnection();
            MySqlCommand cmd_decrypt;
            decr.ConnectionString = "server=localhost;user id=root;password=A9866145009;port=8000;database=world";
            decr.Open();
            string retrieve = ret.Value;
            string roll="",name="";
            cmd_decrypt = decr.CreateCommand();
            cmd_decrypt.CommandText = "SELECT * FROM new_schema.student where idstudent=" + retrieve + " ";
            MySqlDataReader read_text = cmd_decrypt.ExecuteReader();
            while (read_text.Read())
            {
                int id = read_text.GetInt32(0);
                roll+=read_text[1].ToString();
                name += read_text[2].ToString();
                write += "<tr><td>" + id + "</td><td>" + roll + "</td><td>" + name + "</td></tr>";
            }
            //Text.InnerHtml = write;
            decr.Close();
            byte[] key_byte=null,IV_byte=null;
            byte[] encrypted,encrypted1;
            encrypted = System.Convert.FromBase64String(roll);
            encrypted1 = System.Convert.FromBase64String(name);
            MySqlConnection key = new MySqlConnection();
            MySqlCommand cmd_key;
            key.ConnectionString = "server=localhost;user id=root;password=A9866145009;port=8000;database=world";
            key.Open();
            cmd_key = key.CreateCommand();
            cmd_key.CommandText = "SELECT * FROM new_schema.keytable where idstudent=" + retrieve + " ";
            
            MySqlDataReader read_key = cmd_key.ExecuteReader();
            //Text.InnerHtml = "yes";
            while(read_key.Read())
            {
                key_byte = System.Convert.FromBase64String(read_key[1].ToString());
                IV_byte = System.Convert.FromBase64String(read_key[2].ToString());
            }
            string roundtrip = DecryptStringFromBytes(encrypted, key_byte, IV_byte);
            string roundtrip1= DecryptStringFromBytes(encrypted1, key_byte, IV_byte);
            na.InnerHtml = roundtrip;
            ro.InnerHtml = roundtrip1;
        }
        static string DecryptStringFromBytes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments. 
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold 
            // the decrypted text. 
            string plaintext = null;

            // Create an RijndaelManaged object 
            // with the specified key and IV. 
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for decryption. 
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream 
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }

            return plaintext;

        }
        static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

    }
}