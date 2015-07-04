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
//namespace mysql
//{
//    public partial class Form1 : Form
//    {
//        MySqlConnection connect = new MySqlConnection();
//        MySqlCommand cmd;
//        public Form1()
//        {
//            InitializeComponent();
//        }
//        private void page_load(object sender, EventArgs e)
//        {

//            connect.ConnectionString = "server=localhost;user id=root;password=A9866145009;port=8000;database=world";
//            connect.Open();
//            MessageBox.Show("conneted to database");
//        }
//    }
//}
namespace file_encrypt_des
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        MySqlConnection connect = new MySqlConnection();
        MySqlCommand cmd;
        protected string Page_Load()
        {
            
                connect.ConnectionString = "server=localhost;user id=root;password=A9866145009;port=8000;database=world";
                connect.Open();
            
            
            string htmlStr="";
            cmd = connect.CreateCommand();
            cmd.CommandText = "SELECT * FROM new_schema.student";
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string roll = reader[1].ToString();
                string name = reader[2].ToString();
                htmlStr += "<tr><td>" + id + "</td><td>" + roll + "</td><td>" + name + "</td></tr>";
                //ta.InnerHtml = reader[2].ToString();
                //span.InnerHtml = reader[0].ToString();
            }
                // }
            connect.Close();

            return htmlStr;
        }
        static byte[] EncryptStringToBytes(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments. 
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;
            // Create an RijndaelManaged object 
            // with the specified key and IV. 
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for encryption. 
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {

                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                        //String s = Encoding.ASCII.GetString(encrypted);
                        // Console.WriteLine(s);
                    }
                }
            }


            // Return the encrypted bytes from the memory stream. 
            return encrypted;

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
        string key1, iv1;
        public void btn(object sender,EventArgs e)
        {

            MySqlConnection connect1 = new MySqlConnection();
            string nam = roll1.Value;
            string rol = name1.Value;
            string s, s1;
            using (RijndaelManaged myRijndael = new RijndaelManaged())
            {

                myRijndael.GenerateKey();
                myRijndael.GenerateIV();
                // Encrypt the string to an array of bytes. 
                byte[] encrypted = EncryptStringToBytes(nam, myRijndael.Key, myRijndael.IV);
                byte[] encrypted1 = EncryptStringToBytes(rol, myRijndael.Key, myRijndael.IV);
                // Decrypt the bytes to a string. 
                //string roundtrip = DecryptStringFromBytes(encrypted, myRijndael.Key, myRijndael.IV);
                //string roundtrip1 = DecryptStringFromBytes(encrypted1, myRijndael.Key, myRijndael.IV);
                s = System.Convert.ToBase64String(encrypted);
                s1 = System.Convert.ToBase64String(encrypted1);
                key1 = System.Convert.ToBase64String(myRijndael.Key);
                iv1 = System.Convert.ToBase64String(myRijndael.IV);
            }
            string inserted = "INSERT INTO `new_schema`.`student` (`idstudent`, `rollno.`, `studentcol`) VALUES ('" + id1.Value + "','" + s + "', '" + s1 + "')";
          
               connect1.ConnectionString = "server=localhost;user id=root;password=A9866145009;port=8000;database=new_schema";
               connect1.Open();
          
           MySqlCommand cmd2 = new MySqlCommand(inserted, connect1);
           MySqlDataReader reader1 = cmd2.ExecuteReader();
           
           connect1.Close();

           Page_Load();
           MySqlConnection connect2 = new MySqlConnection();
           string stru = "INSERT INTO `new_schema`.`keytable` (`idstudent`, `key`, `IV`) VALUES ('" + id1.Value + "','" + key1 + "','" + iv1 + "')";
        
            connect2.ConnectionString = "server=localhost;user id=root;password=A9866145009;port=8000;database=world";
            connect2.Open();
       
        MySqlCommand command = new MySqlCommand(stru, connect2);
        MySqlDataReader reader2 = command.ExecuteReader();
        connect2.Close();
        }
        
    }
}