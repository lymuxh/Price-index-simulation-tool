using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZI.Properties;
using System.Text.RegularExpressions;
using System.Configuration;
using ZI.Utils;
using System.Data.SqlClient;

namespace ZI
{
    public partial class OptionForm : Form
    {
        public OptionForm()
        {
            InitializeComponent();
            this.dataGroupBox.Select();            
        }

        private void dataGroupBox_Enter(object sender, EventArgs e)
        {            
            //string connString = Settings.Default.ZI_DataBaseConnectionString;
            string connString = ConfigurationManager.ConnectionStrings["ZI.Properties.Settings.ZI_DataBaseConnectionString"].ConnectionString.ToString(); 
            Regex reg = new Regex("Data Source=(.*);Initial Catalog=ZI_DataBase;Persist Security Info=True;User ID=(.*);Password=(.*)");
            Match result = reg.Match(connString);
            if (result.Success)
            {
                this.dbIpTextBox.Text = result.Groups[1].Value;
                this.dbUserTextBox.Text = result.Groups[2].Value;
                this.dbPwdTextBox.Text = result.Groups[3].Value;
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            string connString = "Data Source=" + this.dbIpTextBox.Text + ";Initial Catalog=ZI_DataBase;Persist Security Info=True;" +
               "User ID=" + this.dbUserTextBox.Text + ";Password=" + this.dbPwdTextBox.Text;
            XMLUtil.ConfigSetConnectionString("ZI.Properties.Settings.ZI_DataBaseConnectionString", connString);
            this.Dispose();
        }

        private void testDbButton_Click(object sender, EventArgs e)
        {
            string connString = "Data Source=" + this.dbIpTextBox.Text + ";Initial Catalog=ZI_DataBase;Persist Security Info=True;" +
                "User ID=" + this.dbUserTextBox.Text + ";Password=" + this.dbPwdTextBox.Text;
            
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = connString;
            try
            {
                conn.Open();
                MessageBox.Show("数据库连接成功！");
            }
            catch (SqlException se)
            {
                MessageBox.Show("数据库连接失败，请修改数据库连接信息后再重新尝试！");
            }
            conn.Close();
        }
    }
}
