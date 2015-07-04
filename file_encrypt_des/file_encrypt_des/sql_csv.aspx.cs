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
   
    public partial class sql_csv : System.Web.UI.Page
    {

        MySqlConnection connect = new MySqlConnection();
        MySqlCommand cmd;
        protected string Page_Load()
        {
            
                connect.ConnectionString = "server=localhost;user id=root;password=A9866145009;port=8000;database=world";
                connect.Open();
            
            string htmlStr = "";
            File.Delete(@"C:\\Users\Welcome\Desktop\easydoc\test.csv");
            cmd = connect.CreateCommand();
            cmd.CommandText = "SELECT * FROM world.country where code='COD'";
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string id = reader[0].ToString();
                string roll = reader[1].ToString();
                string name = reader[2].ToString();
                htmlStr += "<tr><td>" + id + "</td><td>" + roll + "</td><td>" + name + "</td><td>"+reader[3].ToString()+"</td></tr>";
                string fileStr = id + "," + roll + "," + name + "," + reader[3].ToString() + "," + reader[4].ToString() + "," + reader[5].ToString() + "," + reader[6].ToString() + "," + reader[7].ToString() + "," + reader[8].ToString();
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\\Users\Welcome\Desktop\easydoc\test.csv", true))
                {
                    file.WriteLine(fileStr);
                }
            }
            // }
            connect.Close();

            return htmlStr;
        }
    }
}