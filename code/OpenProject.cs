using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace ZI
{
    public partial class OpenProject : Form
    {
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["ZI.Properties.Settings.ZI_DataBaseConnectionString"].ConnectionString.ToString();
        private static SqlConnection conn;
        public int tag = 0;// 1表示打开项目 2表示删除项目
        public OpenProject()
        {
            InitializeComponent();
            Boolean ds1 = ZI.DbUtil.isexist("indexproject");
            if (ds1 == true)
            {
                String sql = "select projectname,projectaliasname from indexproject order by projectname asc";
                SqlCommand command = new SqlCommand(sql, getConnection());
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    this.listBox1.Items.Add(reader["projectname"] + "-" + reader["projectaliasname"]);
                }
                conn.Close();
            }
            
        }

        private void OpenProject_Load(object sender, EventArgs e)
        {
            if (tag == 1)
            {
                this.Text = "打开项目";
            }
            else if (tag == 2)
            {
                this.Text = "删除项目";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string listtext = listBox1.SelectedItem.ToString();
                if (tag == 1)
                {

                    string[] listtext1 = listtext.Split('-');
                    String deletesql = "select * from indexproject where projectname='" + listtext1[0] + "'";
                    SqlCommand command = new SqlCommand(deletesql, getConnection());
                    SqlDataReader reader = command.ExecuteReader();
                    string sortstr = "";
                    while (reader.Read())
                    {
                        ZI.MainForm.nowProject = reader["projectname"].ToString();
                        ZI.MainForm.nowProjectAlias = reader["projectaliasname"].ToString();
                        ZI.MainForm.nowClassNum = int.Parse(reader["classnum"].ToString());
                        ZI.MainForm.nowIndex = int.Parse(reader["cycle"].ToString());
                        ZI.MainForm.sort = reader["sort"].ToString();
                        ZI.DbUtil.nowProject = reader["projectname"].ToString();
                        ZI.DbUtil.nowProjectAlias = reader["projectaliasname"].ToString();
                        ZI.DbUtil.nowClassNum = int.Parse(reader["classnum"].ToString());
                        ZI.DbUtil.nowIndex = int.Parse(reader["cycle"].ToString());
                        ZI.DbUtil.sort = reader["sort"].ToString();
                        ZI.Utils.ImportExcel.nowProject = reader["projectname"].ToString();
                        ZI.Utils.ImportExcel.nowProjectAlias = reader["projectaliasname"].ToString();
                        ZI.Utils.ImportExcel.nowClassNum = int.Parse(reader["classnum"].ToString());
                        ZI.Utils.ImportExcel.nowIndex = int.Parse(reader["cycle"].ToString());
                    }
                    conn.Close();
                    this.Close();
                }
                else if (tag == 2)
                {

                    string[] listtext1 = listtext.Split('-');
                    String deletetable = "declare @name varchar(50) ;declare cur cursor for select name from sysobjects where  xtype='U' and name like '" + listtext1[0] + "[_]%' ;open cur;while 1=1 begin fetch next from cur into @name;if (@@fetch_status = -1) break;exec ('drop table '+@name);print @name;end;close cur;deallocate cur;";
                    SqlCommand commanddelete = new SqlCommand(deletetable, getConnection());
                    commanddelete.ExecuteNonQuery();
                    String deletesqll = "delete from indexproject where projectname='" + listtext1[0] + "'";
                    SqlCommand commandd = new SqlCommand(deletesqll, getConnection());
                    commandd.ExecuteNonQuery();
                    conn.Close();
                    this.listBox1.Items.Remove(listBox1.SelectedItem);

                }
            }
            catch (Exception ex) {
                MessageBox.Show("请先选择一个项目");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        public static SqlConnection getConnection()
        {
            if (conn == null || !conn.State.Equals(ConnectionState.Open))
            {
                conn = new SqlConnection();
                conn.ConnectionString = ConnectionString;
                conn.Open();
            }
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            return conn;
        }
    }
}
