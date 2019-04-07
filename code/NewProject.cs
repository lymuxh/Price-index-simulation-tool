using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using ZI.Utils;
using System.Collections.Generic; 

namespace ZI
{
    public partial class NewProject : Form
    {
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["ZI.Properties.Settings.ZI_DataBaseConnectionString"].ConnectionString.ToString();
        private static SqlConnection conn;
        public NewProject()
        {
            InitializeComponent();
            this.Text = "新建项目";
        }


        //点击确定新建项目
        private void button1_Click(object sender, EventArgs e)
        {
            //MainForm.nowClassNum = 1;
            string codenamed = textBox1.Text; //得到项目代号
            string aliases = textBox2.Text; //得到项目别名
            int numberx = Int32.Parse(numericUpDown1.Value.ToString());//得到项目级别
            
            int predioid = 0;
            string pcheckbox = "";
            //List<string> pcheckbox = new List<string>();
            

            if (radioButton1.Checked)
            {
                predioid = 1;
            }
            else if (radioButton2.Checked)
            {
                predioid = 2;
            }
            else if (radioButton3.Checked)
            {
                predioid = 3;
            }
            if (codenamed != "" && aliases != "" && numberx > 0)
            {
                ZI.MainForm.nowProject = codenamed;
                ZI.MainForm.nowProjectAlias = aliases;
                ZI.MainForm.nowClassNum = numberx;
                ZI.MainForm.nowIndex = predioid;
                ZI.DbUtil.nowProject = codenamed;
                ZI.DbUtil.nowProjectAlias = aliases;
                ZI.DbUtil.nowClassNum = numberx;
                ZI.DbUtil.nowIndex = predioid;
                ZI.Utils.ImportExcel.nowProject = codenamed;
                ZI.Utils.ImportExcel.nowProjectAlias = aliases;
                ZI.Utils.ImportExcel.nowClassNum = numberx;
                ZI.Utils.ImportExcel.nowIndex = predioid;
                Boolean ds1 = ZI.DbUtil.isexist("indexproject");
                if (ds1 == false)
                {
                    string dssql = "CREATE TABLE [dbo].[indexproject]([projectid] [int] IDENTITY(1,1) NOT NULL,[projectname] [nvarchar](50) NOT NULL,	[projectaliasname] [nvarchar](50) NOT NULL,	[classnum] [int] NOT NULL,	[cycle] [int] NOT NULL,	[sort] [nvarchar](50) NOT NULL) ON [PRIMARY]";
                    SqlCommand comproject = new SqlCommand(dssql, getConnection());
                    comproject.ExecuteNonQuery();
                }
                String sql = "";
                string msql = "";
                string dsql = "";
                string csql = "";
                string bsql = "";
                string esql = "";
                string fullsql = "";
                String justone = "select count(*) from indexproject where projectname='" + codenamed + "'";
                SqlCommand comjust = new SqlCommand(justone, getConnection());
                SqlDataReader reader = comjust.ExecuteReader();
                int cnt = 0;
                while (reader.Read())
                {
                    cnt = int.Parse(reader[0].ToString());
                }
                conn.Close();
                if (cnt == 0)
                {
                    //if (this.checkBox1.Checked && this.checkBox2.Checked && this.checkBox3.Checked)
                    //{

                    for (int i = 1; i <= numberx; i++)
                    {
                        if (i < numberx)
                        {
                            msql += "[Class_" + i + "_Code]  AS (substring([Class_Code],(1),(" + i * 2 + "))),";
                        }
                        if (i > 1)
                        {
                            dsql += "[Class_" + (i - 1) + "_ID] [nvarchar](2) NOT NULL,";
                            if (i > 2)
                            {
                                bsql += "[Class_" + (i - 1) + "_ID]+";
                            }
                            esql = "[ID]+" + bsql + "[Class_1_ID]";
                            csql += "[Class_" + i + "_Code]  AS (" + esql + "),";
                        }
                        fullsql += "[Class_" + i + "_Code] [nvarchar](" + i * 2 + ") NOT NULL,[Class_" + i + "_Name] [nvarchar](80) NOT NULL,";
                        int a = i;
                        string asql = "";

                        for (int j = i - 1; j > 0; j--)
                        {
                            asql += "[Class_" + j + "_Code]  AS (substring([Class_Code],(1),(" + j * 2 + "))),";
                        }
                        //dsql += "[ID_" + i + "] [nvarchar](" + i * 2 + ") NOT NULL,[Name_" + i + "] [nvarchar](100) NOT NULL,[WeightValue"+i+"] [decimal](18, 5) NOT NULL,";
                        if (this.checkBox1.Checked)
                        {
                            //pcheckbox.Add("Day");
                            sql += "CREATE TABLE [dbo].[" + codenamed + "_Price_Index_Class_" + i + "_Day]([Index_ID] [bigint] IDENTITY(1,1) NOT NULL,	[CreatedDate] [datetime] NOT NULL,[Class_Code] [nvarchar](" + i * 2 + ") NOT NULL,[AveragePrice] [decimal](18, 5) NULL,[BasePrice] [decimal](18, 5) NULL,	[Class_Index] [decimal](18, 5) NULL,[RingMove] [decimal](18, 5) NULL,	[RingMoveRate] [decimal](18, 5) NULL,[state] [nvarchar](1) NULL,[CreateBy] [nvarchar](20) NULL,	[ApprovalDate] [datetime] NULL,	[ApprovalBy] [nvarchar](20) NULL,[Comments] [nvarchar](max) NULL," + asql + "PRIMARY KEY CLUSTERED (	[Index_ID] ASC)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]) ON [PRIMARY]";
                            sql += "CREATE TABLE [dbo].[" + codenamed + "_PriceRing_Index_Class_" + i + "_Day]([Index_ID] [bigint] IDENTITY(1,1) NOT NULL,[CreatedDate] [datetime] NOT NULL,[Class_Code] [nvarchar](" + i * 2 + ") NOT NULL,[Class_Index] [decimal](18, 5) NULL,[RingMove] [decimal](18, 5) NULL,[RingMoveRate] [decimal](18, 5) NULL,[state] [nvarchar](1) NULL,[CreateBy] [nvarchar](20) NULL,[ApprovalDate] [datetime] NULL,[ApprovalBy] [nvarchar](20) NULL,[Comments] [nvarchar](max) NULL," + asql + ") ON [PRIMARY]";
                        }
                        if (this.checkBox2.Checked)
                        {
                            // pcheckbox.Add("Week");
                            sql += "CREATE TABLE [dbo].[" + codenamed + "_Price_Index_Class_" + i + "_Week]([Index_ID] [bigint] IDENTITY(1,1) NOT NULL,	[CreatedDate] [datetime] NOT NULL,[Class_Code] [nvarchar](" + i * 2 + ") NOT NULL,[AveragePrice] [decimal](18, 5) NULL,[BasePrice] [decimal](18, 5) NULL,	[Class_Index] [decimal](18, 5) NULL,[RingMove] [decimal](18, 5) NULL,	[RingMoveRate] [decimal](18, 5) NULL,[state] [nvarchar](1) NULL,[CreateBy] [nvarchar](20) NULL,	[ApprovalDate] [datetime] NULL,	[ApprovalBy] [nvarchar](20) NULL,[Comments] [nvarchar](max) NULL," + asql + "PRIMARY KEY CLUSTERED (	[Index_ID] ASC)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]) ON [PRIMARY]";
                            sql += "CREATE TABLE [dbo].[" + codenamed + "_PriceRing_Index_Class_" + i + "_Week]([Index_ID] [bigint] IDENTITY(1,1) NOT NULL,[CreatedDate] [datetime] NOT NULL,[Class_Code] [nvarchar](" + i * 2 + ") NOT NULL,[Class_Index] [decimal](18, 5) NULL,[RingMove] [decimal](18, 5) NULL,[RingMoveRate] [decimal](18, 5) NULL,[state] [nvarchar](1) NULL,[CreateBy] [nvarchar](20) NULL,[ApprovalDate] [datetime] NULL,[ApprovalBy] [nvarchar](20) NULL,[Comments] [nvarchar](max) NULL," + asql + ") ON [PRIMARY]";
                        }
                        if (this.checkBox3.Checked)
                        {
                            sql += "CREATE TABLE [dbo].[" + codenamed + "_Price_Index_Class_" + i + "_Month]([Index_ID] [bigint] IDENTITY(1,1) NOT NULL,	[CreatedDate] [datetime] NOT NULL,[Class_Code] [nvarchar](" + i * 2 + ") NOT NULL,[AveragePrice] [decimal](18, 5) NULL,[BasePrice] [decimal](18, 5) NULL,	[Class_Index] [decimal](18, 5) NULL,[RingMove] [decimal](18, 5) NULL,	[RingMoveRate] [decimal](18, 5) NULL,[state] [nvarchar](1) NULL,[CreateBy] [nvarchar](20) NULL,	[ApprovalDate] [datetime] NULL,	[ApprovalBy] [nvarchar](20) NULL,[Comments] [nvarchar](max) NULL," + asql + "PRIMARY KEY CLUSTERED (	[Index_ID] ASC)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]) ON [PRIMARY]";
                            sql += "CREATE TABLE [dbo].[" + codenamed + "_PriceRing_Index_Class_" + i + "_Month]([Index_ID] [bigint] IDENTITY(1,1) NOT NULL,[CreatedDate] [datetime] NOT NULL,[Class_Code] [nvarchar](" + i * 2 + ") NOT NULL,[Class_Index] [decimal](18, 5) NULL,[RingMove] [decimal](18, 5) NULL,[RingMoveRate] [decimal](18, 5) NULL,[state] [nvarchar](1) NULL,[CreateBy] [nvarchar](20) NULL,[ApprovalDate] [datetime] NULL,[ApprovalBy] [nvarchar](20) NULL,[Comments] [nvarchar](max) NULL," + asql + ") ON [PRIMARY]";
                        }

                        sql += "CREATE TABLE [dbo].[" + codenamed + "_Class_" + i + "_Weight]([Index_ID] [bigint] IDENTITY(1,1) NOT NULL,[CreatedDate] [datetime] NULL,[Class_Code] [nvarchar](" + i * 2 + ") NOT NULL,[WeightValue] [decimal](18, 5) NOT NULL,[state] [nvarchar](1) NULL,[ApprovalDate] [datetime] NULL,[ApprovalBy] [nvarchar](20) NULL,[Comments] [nvarchar](max) NULL," + msql + "PRIMARY KEY CLUSTERED ([Index_ID] ASC)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]) ON [PRIMARY]";
                        sql += "CREATE TABLE [dbo].[" + codenamed + "_Class_" + i + "]([Index_ID] [bigint] IDENTITY(1,1) NOT NULL," + dsql + "[ID] [nvarchar](2) NOT NULL,[Name] [nvarchar](100) NOT NULL,[DisplayName] [nvarchar](100) NULL,[Alias] [nvarchar](100) NULL,[State] [nvarchar](1) NULL,[Comments] [nvarchar](max) NULL," + csql + " CONSTRAINT [PK_" + codenamed + "_Class_" + i + "] PRIMARY KEY CLUSTERED ([Index_ID] ASC)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]) ON [PRIMARY]";


                    }
                    if (this.checkBox1.Checked)
                    {
                        pcheckbox+="Day";
                        sql += "CREATE TABLE [dbo].[" + codenamed + "_Price_Etl_Day]([Index_ID] [bigint] IDENTITY(1,1) NOT NULL,	[CreatedDate] [datetime] NOT NULL,[Class_Code] [nvarchar](" + numberx * 2 + ") NOT NULL,[AveragePrice] [real] NULL,	[RingMove] [real] NULL,	[RingMoveRate] [real] NULL,	[state] [nvarchar](1) NULL,	[CreateBy] [nvarchar](20) NULL,	[ApprovalDate] [datetime] NULL,	[ApprovalBy] [nvarchar](20) NULL,[Comments] [nvarchar](max) NULL," + msql + "PRIMARY KEY CLUSTERED (	[Index_ID] ASC)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]) ON [PRIMARY]";
                        sql += "CREATE TABLE [dbo].[" + codenamed + "_Price_Index_Day](	[Index_ID] [bigint] IDENTITY(1,1) NOT NULL,	[CreatedDate] [datetime] NOT NULL,[PriceIndex] [decimal](18, 5) NULL,[RingMove] [decimal](18, 5) NULL,[RingMoveRate] [decimal](18, 5) NULL,[state] [nvarchar](1) NULL,[CreateBy] [nvarchar](20) NULL,[ApprovalDate] [datetime] NULL,[ApprovalBy] [nvarchar](20) NULL,[Comments] [nvarchar](max) NULL,PRIMARY KEY CLUSTERED ([Index_ID] ASC)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]) ON [PRIMARY]";
                        sql += "CREATE TABLE [dbo].[" + codenamed + "_PriceRing_Index_Day](	[Index_ID] [bigint] IDENTITY(1,1) NOT NULL,[CreatedDate] [datetime] NOT NULL,[PriceIndex] [decimal](18, 5) NULL,[RingMove] [decimal](18, 5) NULL,[RingMoveRate] [decimal](18, 5) NULL,[state] [nvarchar](1) NULL,[CreateBy] [nvarchar](20) NULL,[ApprovalDate] [datetime] NULL,[ApprovalBy] [nvarchar](20) NULL,[Comments] [nvarchar](max) NULL,PRIMARY KEY CLUSTERED (	[Index_ID] ASC)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]) ON [PRIMARY]";
                    }
                    if (this.checkBox2.Checked)
                    {
                        pcheckbox += "Week"; 
                        sql += "CREATE TABLE [dbo].[" + codenamed + "_Price_Etl_Week]([Index_ID] [bigint] IDENTITY(1,1) NOT NULL,	[CreatedDate] [datetime] NOT NULL,[Class_Code] [nvarchar](" + numberx * 2 + ") NOT NULL,[AveragePrice] [real] NULL,	[RingMove] [real] NULL,	[RingMoveRate] [real] NULL,	[state] [nvarchar](1) NULL,	[CreateBy] [nvarchar](20) NULL,	[ApprovalDate] [datetime] NULL,	[ApprovalBy] [nvarchar](20) NULL,[Comments] [nvarchar](max) NULL," + msql + "PRIMARY KEY CLUSTERED (	[Index_ID] ASC)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]) ON [PRIMARY]";
                        sql += "CREATE TABLE [dbo].[" + codenamed + "_Price_Index_Week](	[Index_ID] [bigint] IDENTITY(1,1) NOT NULL,	[CreatedDate] [datetime] NOT NULL,[PriceIndex] [decimal](18, 5) NULL,[RingMove] [decimal](18, 5) NULL,[RingMoveRate] [decimal](18, 5) NULL,[state] [nvarchar](1) NULL,[CreateBy] [nvarchar](20) NULL,[ApprovalDate] [datetime] NULL,[ApprovalBy] [nvarchar](20) NULL,[Comments] [nvarchar](max) NULL,PRIMARY KEY CLUSTERED ([Index_ID] ASC)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]) ON [PRIMARY]";
                        sql += "CREATE TABLE [dbo].[" + codenamed + "_PriceRing_Index_Week](	[Index_ID] [bigint] IDENTITY(1,1) NOT NULL,[CreatedDate] [datetime] NOT NULL,[PriceIndex] [decimal](18, 5) NULL,[RingMove] [decimal](18, 5) NULL,[RingMoveRate] [decimal](18, 5) NULL,[state] [nvarchar](1) NULL,[CreateBy] [nvarchar](20) NULL,[ApprovalDate] [datetime] NULL,[ApprovalBy] [nvarchar](20) NULL,[Comments] [nvarchar](max) NULL,PRIMARY KEY CLUSTERED (	[Index_ID] ASC)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]) ON [PRIMARY]";
                    }
                    if (this.checkBox3.Checked)
                    {
                        pcheckbox += "Month"; 
                        sql += "CREATE TABLE [dbo].[" + codenamed + "_Price_Etl_Month]([Index_ID] [bigint] IDENTITY(1,1) NOT NULL,[CreatedDate] [datetime] NOT NULL,[Class_Code] [nvarchar](" + numberx * 2 + ") NOT NULL,[AveragePrice] [real] NULL,[RingMove] [real] NULL,	[RingMoveRate] [real] NULL,	[state] [nvarchar](1) NULL,	[CreateBy] [nvarchar](20) NULL,	[ApprovalDate] [datetime] NULL,	[ApprovalBy] [nvarchar](20) NULL,[Comments] [nvarchar](max) NULL," + msql + "PRIMARY KEY CLUSTERED ([Index_ID] ASC)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]) ON [PRIMARY]";
                        sql += "CREATE TABLE [dbo].[" + codenamed + "_Price_Index_Month](	[Index_ID] [bigint] IDENTITY(1,1) NOT NULL,	[CreatedDate] [datetime] NOT NULL,[PriceIndex] [decimal](18, 5) NULL,[RingMove] [decimal](18, 5) NULL,[RingMoveRate] [decimal](18, 5) NULL,[state] [nvarchar](1) NULL,[CreateBy] [nvarchar](20) NULL,[ApprovalDate] [datetime] NULL,[ApprovalBy] [nvarchar](20) NULL,[Comments] [nvarchar](max) NULL,PRIMARY KEY CLUSTERED ([Index_ID] ASC)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]) ON [PRIMARY]";
                        sql += "CREATE TABLE [dbo].[" + codenamed + "_PriceRing_Index_Month](	[Index_ID] [bigint] IDENTITY(1,1) NOT NULL,[CreatedDate] [datetime] NOT NULL,[PriceIndex] [decimal](18, 5) NULL,[RingMove] [decimal](18, 5) NULL,[RingMoveRate] [decimal](18, 5) NULL,[state] [nvarchar](1) NULL,[CreateBy] [nvarchar](20) NULL,[ApprovalDate] [datetime] NULL,[ApprovalBy] [nvarchar](20) NULL,[Comments] [nvarchar](max) NULL,PRIMARY KEY CLUSTERED (	[Index_ID] ASC)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]) ON [PRIMARY]";
                    }

                        sql += "CREATE TABLE [dbo].[" + codenamed + "_Price_Pick]([Index_ID] [bigint] IDENTITY(1,1) NOT NULL,	[MarketName] [nvarchar](80) NULL,[ShopName] [nvarchar](80) NULL,[BandName] [nvarchar](80) NULL,	[Style] [nvarchar](80) NULL,[DisplayName] [nvarchar](80) NULL,[Class_Code] [nvarchar](" + numberx * 2 + ") NULL," + msql + "[Price] [real] NULL,[CreatedDate] [datetime] NULL,[CreatedWeekend] [datetime] NULL,	[CreatedMonth] [datetime] NULL,	[ApprovalDate] [datetime] NULL,	[ApprovalBy] [nvarchar](20) NULL,[Approvaled] [int] NULL, CONSTRAINT [PK_" + codenamed + "_PriceWeek_Pick] PRIMARY KEY CLUSTERED ([Index_ID] ASC)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]) ON [PRIMARY]";
                        sql += "CREATE TABLE [dbo].[" + codenamed + "_Price_Ods_Base]([Index_ID] [bigint] IDENTITY(1,1) NOT NULL,	[CreatedDate] [datetime] NOT NULL,[Class_Code] [nvarchar](" + numberx * 2 + ") NOT NULL,[AveragePrice] [decimal](18, 2) NOT NULL,[state] [nvarchar](1) NULL,[CreateBy] [nvarchar](20) NULL,[ApprovalDate] [datetime] NULL,[ApprovalBy] [nvarchar](20) NULL,[Comments] [nvarchar](max) NULL," + msql + "PRIMARY KEY CLUSTERED ([Index_ID] ASC)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]) ON [PRIMARY]";
                        sql += "CREATE TABLE [dbo].[" + codenamed + "_Product_WithFullName]([Index_ID] [bigint] IDENTITY(1,1) NOT NULL," + fullsql + "[DisplayName] [nvarchar](80) NULL,[Measure] [nvarchar](30) NULL,CONSTRAINT [PK__" + codenamed + "__Product_WithFullName] PRIMARY KEY CLUSTERED ([Index_ID] ASC)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]) ON [PRIMARY]";
                        sql += "insert into indexproject (projectname,projectaliasname,classnum,cycle,sort) values ('" + codenamed + "','" + aliases + "'," + numberx + "," + predioid + ",'" + pcheckbox + "')";
                   // }
                        ZI.MainForm.sort = pcheckbox;
                        ZI.DbUtil.sort = pcheckbox;
                    SqlCommand command = new SqlCommand(sql, getConnection());
                    int count = command.ExecuteNonQuery();
                    if (MessageBox.Show(this, "项目创建成功。现在导入分类权重表？", "确认导入", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2).Equals(DialogResult.Yes))
                    {
                        OpenFileDialog openFileDialog = new OpenFileDialog();
                        openFileDialog.InitialDirectory = "..\\";
                        openFileDialog.Filter = "Excel文件|*.xls";
                        openFileDialog.RestoreDirectory = true;
                        openFileDialog.FilterIndex = 1;
                        openFileDialog.Multiselect = false;
                        if (openFileDialog.ShowDialog(this) == DialogResult.OK)
                        {
                            String[] files = openFileDialog.FileNames;
                            foreach (string file in files)
                            {//导入分类权重
                                //this.mainStatusLabel.Text = "正在处理文件：" + file;
                                //this.statusStrip1.Update();
                                ImportExcel.importProductCodeExcel(file);
                                ImportExcel.importProductWeightExcel(file);
                            }
                            MessageBox.Show(this, "导入成功。", "确认", MessageBoxButtons.OK);
                            this.Dispose();
                        }
                        else
                        {
                            MessageBox.Show(this, "请到“基础数据维护菜单”中导入项目的分类权重。", "确认", MessageBoxButtons.OK);
                            this.Dispose();
                        }
                    }
                    else
                    {
                        MessageBox.Show(this, "请到“基础数据维护菜单”中导入项目的分类权重。", "确认", MessageBoxButtons.OK);
                        this.Dispose();
                    }
                }
                else
                {
                    MessageBox.Show("该用户名已经存在,请重新输入");
                }
            }
            else if (codenamed == "")
            {
                MessageBox.Show("请填写项目代号");
            }
            else if (aliases == "")
            {
                MessageBox.Show("请填写项目别名");
            }else if( numberx == 0){
                MessageBox.Show("请填写项目分类级数");
            }
            //else if (pcheckbox == "")
            //{
            //    MessageBox.Show("请选择一个指数类别");
            //}
        }


        private void NewProject_Load(object sender, EventArgs e)
        {

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

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1.CheckState = CheckState.Unchecked;
            checkBox1.Enabled = false;
            checkBox2.CheckState = CheckState.Checked;
            checkBox2.Enabled = true;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1.CheckState = CheckState.Checked;
            checkBox1.Enabled = true;
            checkBox2.CheckState = CheckState.Checked;
            checkBox2.Enabled = true;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1.CheckState = CheckState.Unchecked;
            checkBox1.Enabled = false;
            checkBox2.CheckState = CheckState.Unchecked;
            checkBox2.Enabled = false;
        }
    }
}
