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
    public partial class testing : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string tyme = DateTime.Now.TimeOfDay.ToString();
            string[] array=tyme.Split(':');
            tyme = array[0] + array[1];
            int time= Convert.ToInt32(tyme);
            time1.InnerHtml = tyme;
            MySqlConnection connect = new MySqlConnection();
            MySqlCommand cmd;
            connect.ConnectionString = "server=localhost;user id=root;password=A9866145009;port=8000;database=world";
            connect.Open();
            cmd = connect.CreateCommand();
            cmd.CommandText = "SELECT * FROM new_schema.key where time1<" + time + " AND time2>" + time + "";
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int d = reader.GetInt32(0);
            }
            connect.Close();

        }
    }
}