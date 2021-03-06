using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace MiniJIRAClientApplication
{
    public partial class Form1 : Form
    {
        MiniJIRA.MainMiniJIRA proxy = new MiniJIRA.MainMiniJIRA();
        HttpClient client = new HttpClient();

        public Form1()
        {
            InitializeComponent();
        }

        private void WebServicesSettings()
        {
            client.BaseAddress = new Uri("https://localhost:44394/MainMiniJIRA.asmx/");
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            //string usersJson = proxy.UsersTable();
            //DataTable dtUsers = JsonConvert.DeserializeObject<DataTable>(usersJson);
            //dataGridView1.DataSource = dtUsers;

            WebServicesSettings();
        }

        private DataTable stringSplit(string userJson)
        {
            string[] json = userJson.Split('>');
            string[] finalJson = json[2].Split('<');

            DataTable dt = JsonConvert.DeserializeObject<DataTable>(finalJson[0]);
            return dt;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            // Nie uzywaj dodatkowych spacji!!!
            HttpResponseMessage message = client.GetAsync("UsersTableForUsers?id=" + textBoxId.Text + "").Result;
            string userJson = message.Content.ReadAsStringAsync().Result;
            dataGridView1.DataSource = stringSplit(userJson);
        }
    }
}
