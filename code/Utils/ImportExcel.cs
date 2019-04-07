using System;
using System.Data.OleDb;
using System.IO;
using System.Text.RegularExpressions;
using ADODB;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Windows.Forms;
using System.Collections.Generic;

namespace ZI.Utils
{
    public partial class ImportExcel
    {
        #region SqlBase
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["ZI.Properties.Settings.ZI_DataBaseConnectionString"].ConnectionString.ToString();
        private static Connection myConnect = new ADODB.Connection();
        private static ADOX.Catalog myADOX = new ADOX.Catalog();
        public static string nowProject = "";//当前项目代码
        public static string nowProjectAlias = "";//当前项目别名
        public static int nowClassNum = 0;//当前项目的级数
        public static int nowIndex = 0;//当前项目的指数类型

       // private static string sort = ZI.MainForm.sort;//指数发布周期

        //public static string sqls = "";
        //public static string sqls1="";

        private static SqlConnection conn;
        /// <summary>
        /// 返回本项目的一个数据库连接
        /// </summary>
        /// <returns></returns>
        public static SqlConnection getConnection()
        {
            if (conn == null || !conn.State.Equals(ConnectionState.Open))
            {
                conn = new SqlConnection();
                conn.ConnectionString = ConnectionString;
                conn.Open();
            }
            return conn;
        }
        public static void TruncateTable(string table)
        {

            if ((table != null) && (table != ""))
            {
                string sqlTruncate = @"truncate table " + table;
                sqlTruncate = sqlTruncate + ";\r\n";
                SqlCommand commandTruncate = DbUtil.getConnection().CreateCommand();
                commandTruncate.CommandText = sqlTruncate;
                commandTruncate.ExecuteNonQuery();
            }
        }
        public static object ExecuteScalar(String sql)
        {
            SqlConnection conn = getConnection();
            IDbCommand command = conn.CreateCommand();
            command.CommandText = sql;
            return command.ExecuteScalar();
        }
        #endregion
        /// <summary>
        /// 月周字符转换
        /// </summary>
        /// <param name="string"></param>
        /// <returns></returns>
        public static string transString(string getTime)
        {
            getTime = getTime.Replace("一", "1");
            getTime = getTime.Replace("二", "2");
            getTime = getTime.Replace("三", "3");
            getTime = getTime.Replace("四", "4");
            getTime = getTime.Replace("五", "5");
            getTime = getTime.Replace("六", "6");
            getTime = getTime.Replace("七", "7");
            getTime = getTime.Replace("八", "8");
            getTime = getTime.Replace("九", "9");
            getTime = getTime.Replace("十", "10");
            getTime = getTime.Replace("十一", "11");
            getTime = getTime.Replace("十二", "12");
            getTime = getTime.Replace("元月", "1月");
            return getTime;
        }

        /// <summary>
        /// 导入价格采样表
        /// </summary>
        /// <param name="file"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int importExcelDaily(string file, string date)
        {
            if (file.Contains("'"))
            {
                file = file.Replace("'", "''");
            }
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source='" + file + "';Extended Properties='Excel 8.0;HDR=No;IMEX=1'";
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            DataTable schemeDataTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            int count =schemeDataTable.Rows.Count;
            string sheetName="";
            int ProductNum = 0;
            //foreach (DataRow row in schemeDataTable.Rows) {

            for(int j=0;j<count;j++){
                sheetName = schemeDataTable.Rows[j][2].ToString().Trim();
               // sheetName = row["TABLE_NAME"].ToString();
                if(sheetName.Contains("表2") && !sheetName.Contains("Print_Titles")){
                    string strExcel = "select * from [" + sheetName + "]";
                    OleDbDataAdapter command = new OleDbDataAdapter(strExcel, strConn);
                    DataSet ds = new DataSet();
                    command.Fill(ds, "ParkDaily");
                    DataTable dt = ds.Tables["ParkDaily"];

                    int rowCount = dt.Rows.Count;
                    int columnCount = dt.Columns.Count;
                    
                    string sqlInsert = null;
                    Regex merchantRegex = new Regex("^\\d{"+nowClassNum*2+"}$", RegexOptions.IgnoreCase);
                    Regex merchantRegex2 = new Regex("^\\d{2,}|\\d{2,}[\u4e00-\u9fa5]$", RegexOptions.IgnoreCase);
                    string MarketName = "";
                    string ShopName = "";
                    string BandName = "";
                    string Style = "";
                    string DisplayName = "";
                    string Class_Code = "";
                    string Price = "";
                    string CreatedDate = "";
                    //读取表单数据
                    for (int i = 1; i < rowCount; i++)
                    {
                        dt.Rows[i][6] = dt.Rows[i][6].ToString().Replace(".", "").Trim();
                        int abc = dt.Rows[i][6].ToString().Length;
                       
                        if (dt.Rows[i][6].ToString().Length <4)
                        {
                            continue;
                        }
                        Match m = merchantRegex.Match(dt.Rows[i][6].ToString().Trim());
                       
                        //导入Product数据--
                        if (m.Success)
                        {
                            MarketName = string.IsNullOrEmpty(dt.Rows[i][1].ToString().Trim()) ? MarketName : dt.Rows[i][1].ToString().Trim();
                            ShopName = string.IsNullOrEmpty(dt.Rows[i][2].ToString().Trim()) ? ShopName : dt.Rows[i][2].ToString().Trim();
                             BandName = dt.Rows[i][3].ToString().Trim();
                             Style = dt.Rows[i][4].ToString().Trim();
                             DisplayName = dt.Rows[i][5].ToString().Trim();
                             Class_Code = dt.Rows[i][6].ToString();
                             Price = dt.Rows[i][7].ToString().Trim().Replace("特","");
                             Match m2 = merchantRegex2.Match(Price);
                             CreatedDate=dt.Rows[i][8].ToString().Replace(".","-").Trim();              
                            if (string.IsNullOrEmpty(Price)||string.IsNullOrEmpty(CreatedDate) || m2.Success!=true)
                            {
                                continue;
                            }
                            int year = int.Parse(CreatedDate.Split('-')[0]);
                            int month = int.Parse(CreatedDate.Split('-')[1]);
                            int day = int.Parse(CreatedDate.Split('-')[2]);
                            DateTime weekTime = new DateTime(year, month, day);
                            DateTime startWeek;
                            if (Convert.ToInt32(weekTime.DayOfWeek.ToString("d")) == 0)
                            {
                                startWeek = weekTime.AddDays(1 - Convert.ToInt32(weekTime.DayOfWeek.ToString("d")));
                            }
                            else
                            {
                                startWeek = weekTime.AddDays(8 - Convert.ToInt32(weekTime.DayOfWeek.ToString("d")));
                            }
                            string CreatedWeekend = startWeek.Year + "-" + startWeek.Month + "-" + startWeek.Day;
                           
                            string CreatedMonth = startWeek.AddMonths(1).Year + "-" + startWeek.AddMonths(1).Month + "-1";
                            sqlInsert = sqlInsert + @"INSERT  INTO "+nowProject+"_Price_Pick(MarketName ,ShopName,BandName,Style,DisplayName,Class_Code,Price ,CreatedDate,CreatedWeekend,CreatedMonth,Approvaled)VALUES  ('@MarketName','@ShopName','@BandName','@Style','@DisplayName','@Class_Code','@Price','@CreatedDate','@CreatedWeekend','@CreatedMonth',1)";
                            sqlInsert = sqlInsert.Replace("@MarketName", MarketName);
                            sqlInsert = sqlInsert.Replace("@ShopName", ShopName);
                            sqlInsert = sqlInsert.Replace("@BandName", BandName);
                            sqlInsert = sqlInsert.Replace("@Style", Style);
                            sqlInsert = sqlInsert.Replace("@DisplayName", DisplayName);
                            sqlInsert = sqlInsert.Replace("@Class_Code", Class_Code);
                            sqlInsert = sqlInsert.Replace("@Price", Price);
                            sqlInsert = sqlInsert.Replace("@CreatedDate", CreatedDate);
                            sqlInsert = sqlInsert.Replace("@CreatedWeekend", CreatedWeekend);
                            sqlInsert = sqlInsert.Replace("@CreatedMonth", CreatedMonth);
                            sqlInsert = sqlInsert + ";\r\n";


                        }
                    }
                    SqlCommand commandInsert = DbUtil.getConnection().CreateCommand();
                    commandInsert.CommandText = sqlInsert;
                    if (sqlInsert != null)
                    {
                        ProductNum = commandInsert.ExecuteNonQuery();
                    }
                }
            }
                   
            //把不在分类体系中的数据删除
            string sqlDelete = "delete from " + nowProject + "_Price_Pick where Class_Code not in(select Class_Code from " + nowProject + "_Class_"+MainForm.nowClassNum+")";
            SqlCommand commandDelete = DbUtil.getConnection().CreateCommand();
            commandDelete.CommandText = sqlDelete;
            ProductNum = ProductNum - commandDelete.ExecuteNonQuery();
            conn.Close();
            return ProductNum;
        }

        /// <summary>
        /// <summary>
        /// 导入分类 
        /// </summary>
        /// <param name="file">Excle文件</param>
        public static void importProductCodeExcel(string file)
        {
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source='" + file + "';Extended Properties='Excel 8.0;HDR=No;IMEX=1'";
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            // myADOX.ActiveConnection = myConnect;
            string strExcel = "select * from [sheet1$]";
            OleDbDataAdapter command = new OleDbDataAdapter(strExcel, strConn);
            DataSet ds = new DataSet();
            command.Fill(ds, "ProductCode");
            DataTable dt = ds.Tables["ProductCode"];
            int rowCount = dt.Rows.Count;
            int columnCount = dt.Columns.Count;

            string Class_ID="00";
            string Class_Name="00";
            int CategoryNum = 0, SubCategoryNum = 0, BrandNum = 0, ProductNum = 0,LastNum=0;
            string sqlInsert = null;
            Regex merchantRegex = new Regex("^\\d{"+nowClassNum*2+"}$", RegexOptions.IgnoreCase);

            //读取表单数据
            string codel1 = "", codel2 = "", codel3 = "", codel4 = "", codel5 = "";
            for (int i = 1; i < rowCount; i++)
            {
                if (dt.Rows[i][0].ToString().Trim().Length < 6)
                {
                   // dt.Rows[i][0] = "0" + dt.Rows[i][0].ToString().Trim();
                    continue;
                }
                dt.Rows[i][0] = dt.Rows[i][0].ToString().Trim().Replace(".","");
                Match m = merchantRegex.Match(dt.Rows[i][0].ToString().Trim());

                for (int j = nowClassNum; j > 0; j--)
                {
                    if (m.Success && dt.Rows[i][j * 2].ToString().Trim() != "")
                    {
                        string classid = "",valueclassid="";
                        for (int k = j - 1; k > 0; k--)
                        {
                            classid += "Class_" + k + "_ID,";
                            valueclassid += "'@Class_" + k + "_ID',  ";
                        }
                        Class_ID = dt.Rows[i][0].ToString().Trim().Substring((j-1)*2, 2);
                        Class_Name = dt.Rows[i][j*2-1].ToString().Trim();
                        sqlInsert = sqlInsert + @"insert into " + nowProject + "_Class_"+j+"("+classid+"ID,Name,State) values("+valueclassid+"'@ID', '@Name', 1)";
                        for (int l = j - 1; l > 0; l--)
                        {
                            sqlInsert = sqlInsert.Replace("@Class_" + l + "_ID", dt.Rows[i][0].ToString().Trim().Substring((l - 1) * 2, 2));
                        }
                        sqlInsert = sqlInsert.Replace("@ID", Class_ID);
                        sqlInsert = sqlInsert.Replace("@Name", Class_Name);
                        //sqlInsert = sqlInsert.Replace("@DisplayName", Class_Second_Name + Class_Third_Name);
                        sqlInsert = sqlInsert + ";\r\n";

                        




                        if (j == 1)
                        {
                            ++CategoryNum;
                        }
                        else if (j == 2)
                        {
                            ++SubCategoryNum;
                        }
                        else if (j == 3)
                        {
                            ++BrandNum;
                        }
                        else if (j == 4)
                        {
                            ++ProductNum;
                        }
                        else if (j == 5)
                        {
                            ++LastNum;
                        }
                    }
                }
                 //导入Product数据--
                if (m.Success)
                {
                    string Measure = "件";
                    string withcode = "", withname = "";
                    for (int k = nowClassNum - 1; k > 0; k--)
                    {
                        withcode += "Class_" + k + "_Code,Class_" + k + "_Name,";
                        withname += "'@Class_" + k + "_Code','@Class_" + k + "_Name',  ";
                    }
                    sqlInsert = sqlInsert + @"INSERT  INTO " + nowProject + "_Product_WithFullName( Class_" + nowClassNum + "_Code ,Class_" + nowClassNum + "_Name ," + withcode + "Measure)VALUES  ('@Class_" + nowClassNum + "_Code','@Class_" + nowClassNum + "_Name'," + withname + "'@Measure') ";
                    
                    for (int l = nowClassNum; l > 0; l--)
                    {
                        string hcode = "";
                        if (l == 3)
                        {
                            if (dt.Rows[i][5].ToString().Trim() != "")
                            {
                                codel3 = dt.Rows[i][5].ToString().Trim();
                            }
                            else
                            {
                                hcode += codel3;
                            }
                            hcode = codel3;
                        } else if (l == 2)
                        {
                            if (dt.Rows[i][3].ToString().Trim() != "")
                            {
                                codel2 = dt.Rows[i][3].ToString().Trim();
                            }
                            else
                            {
                                hcode += codel2;
                            }
                            hcode = codel2;
                        }
                        else if (l == 1)
                        {
                            if (dt.Rows[i][1].ToString().Trim() != "")
                            {
                                codel1 = dt.Rows[i][1].ToString().Trim();
                            }
                            else
                            {
                                hcode += codel1;
                            }
                            hcode = codel1;
                        }
                        else if (l == 4)
                        {
                            if (dt.Rows[i][7].ToString().Trim() != "")
                            {
                                codel4 = dt.Rows[i][7].ToString().Trim();
                            }
                            else
                            {
                                hcode += codel4;
                            }
                            hcode = codel4;
                        }
                        else if (l == 5)
                        {
                            if (dt.Rows[i][9].ToString().Trim() != "")
                            {
                                codel5 = dt.Rows[i][9].ToString().Trim();
                            }
                            else
                            {
                                hcode += codel5;
                            }
                            hcode = codel5;
                        }
                        sqlInsert = sqlInsert.Replace("@Class_" + l + "_Code", dt.Rows[i][0].ToString().Trim().Substring(0, l * 2));
                        sqlInsert = sqlInsert.Replace("@Class_" + l + "_Name", hcode);

                    }
                    //sqlInsert = sqlInsert.Replace("@DisplayName", DisplayName);
                    sqlInsert = sqlInsert.Replace("@Measure", Measure);
                    sqlInsert = sqlInsert + ";\r\n";
                    ++ProductNum;
                }
                
            }
            string classtype = "";
            if (nowClassNum == 1)
            {
                classtype = "一级类别数为:" + CategoryNum.ToString() + "\n";
            }
            else if (nowClassNum == 2)
            {
                classtype = "一级类别数为:" + CategoryNum.ToString() + "\n" +  "二级类别数为:" + SubCategoryNum.ToString() + "\n" ;
            }
            else if (nowClassNum == 3)
            {
                classtype = "一级类别数为:" + CategoryNum.ToString() + "\n" + "二级类别数为:" + SubCategoryNum.ToString() + "\n" + "三级类别数为:" + BrandNum.ToString() + "\n";
            }
            else if (nowClassNum == 4)
            {
                classtype = "一级类别数为:" + CategoryNum.ToString() + "\n" + "二级类别数为:" + SubCategoryNum.ToString() + "\n" + "三级类别数为:" + BrandNum.ToString() + "\n" + "四级类别数为:" + ProductNum.ToString() + "\n";
            }
            else if (nowClassNum == 5)
            {
                classtype = "一级类别数为:" + CategoryNum.ToString() + "\n" + "二级类别数为:" + SubCategoryNum.ToString() + "\n" + "三级类别数为:" + BrandNum.ToString() + "\n" + "四级类别数为:" + ProductNum.ToString() + "\n" + "五级类别数为:" + LastNum.ToString() + "\n";
            }

        //    DialogResult response = MessageBox.Show(classtype +                                                   
        //"您确定要导入分类表，覆盖当前分类表吗？", "请确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        //    if (response == System.Windows.Forms.DialogResult.Yes)
        //    {
                for (int k = nowClassNum; k > 0; k--)
                {
                    TruncateTable(nowProject + "_Class_"+k);
                }
                TruncateTable(nowProject + "_Product_WithFullName");

                SqlCommand commandInsert = DbUtil.getConnection().CreateCommand();
                commandInsert.CommandText = sqlInsert;
                commandInsert.ExecuteNonQuery();
            //}
            conn.Close();

        }
        /// <summary>
        /// 导入分类权重
        /// </summary>
        /// <param name="file">Excle文件</param>
        public static void importProductWeightExcel(string file)
        {
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source='" + file + "';Extended Properties='Excel 8.0;HDR=No;IMEX=1'";
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            // myADOX.ActiveConnection = myConnect;
            string strExcel = "select * from [sheet1$]";
            OleDbDataAdapter command = new OleDbDataAdapter(strExcel, strConn);
            DataSet ds = new DataSet();
            command.Fill(ds, "ProductWeight");
            DataTable dt = ds.Tables["ProductWeight"];
            int rowCount = dt.Rows.Count;
            int columnCount = dt.Columns.Count;
            string Class_Code = "0000000000";
            double Class_WeightValue = 0;
            //double CategoryWeightSum = 0, SubCategoryWeightSum = 0;
            int Class_1Num = 0, Class_2Num = 0, Class_3Num = 0, Class_4Num = 0, Class_5Num = 0;
            string sqlInsert = null;

            Regex merchantRegex = new Regex("^\\d{"+nowClassNum*2+"}$", RegexOptions.IgnoreCase);
            for (int i = 1; i < rowCount; i++)
            {
                if (dt.Rows[i][0].ToString().Trim().Length < 6)
                {
                    //dt.Rows[i][0] = "0" + dt.Rows[i][0].ToString().Trim();
                    continue;
                }
                dt.Rows[i][0] = dt.Rows[i][0].ToString().Trim().Replace(".", "");
                Match m = merchantRegex.Match(dt.Rows[i][0].ToString().Trim());
                for (int j = nowClassNum; j > 0; j--)
                {
                    if (m.Success && dt.Rows[i][j*2].ToString().Trim() != "")
                    {
                        Class_Code = dt.Rows[i][0].ToString().Trim().Substring(0, j*2);
                        Class_WeightValue = Convert.ToDouble(dt.Rows[i][j * 2]);

                        sqlInsert = sqlInsert + @"insert into " + nowProject + "_Class_"+j+"_Weight(Class_Code,WeightValue,State) values( '@Class_"+j+"_Code',@WeightValue, 1)";
                        sqlInsert = sqlInsert.Replace("@Class_"+j+"_Code", Class_Code);
                        sqlInsert = sqlInsert.Replace("@WeightValue", Class_WeightValue.ToString("0.00000"));
                        sqlInsert = sqlInsert + ";\r\n";
                        if (j == 1)
                        {
                            ++Class_1Num;
                        }
                        else if (j == 2)
                        {
                            ++Class_2Num;
                        }
                        else if (j == 3)
                        {
                            ++Class_3Num;
                        }
                        else if (j == 4)
                        {
                            ++Class_4Num;
                        }
                        else if (j == 5)
                        {
                            ++Class_5Num;
                        }
                    }
                }
            }
            string classtype = "";
            if (nowClassNum == 1)
            {
                classtype = "一级类别数为:" + Class_1Num.ToString() + "\n";
            }
            else if (nowClassNum == 2)
            {
                classtype = "一级类别数为:" + Class_1Num.ToString() + "\n" + "二级类别数为:" + Class_2Num.ToString() + "\n";
            }
            else if (nowClassNum == 3)
            {
                classtype = "一级类别数为:" + Class_1Num.ToString() + "\n" + "二级类别数为:" + Class_2Num.ToString() + "\n" + "三级类别数为:" + Class_3Num.ToString() + "\n";
            }
            else if (nowClassNum == 4)
            {
                classtype = "一级类别数为:" + Class_1Num.ToString() + "\n" + "二级类别数为:" + Class_2Num.ToString() + "\n" + "三级类别数为:" + Class_3Num.ToString() + "\n" + "四级类别数为:" + Class_4Num.ToString() + "\n";
            }
            else if (nowClassNum == 5)
            {
                classtype = "一级类别数为:" + Class_1Num.ToString() + "\n" + "二级类别数为:" + Class_2Num.ToString() + "\n" + "三级类别数为:" + Class_3Num.ToString() + "\n" + "四级类别数为:" + Class_4Num.ToString() + "\n" + "五级类别数为:" + Class_5Num.ToString() + "\n";
            }
            //DialogResult response = MessageBox.Show(
            //    classtype +
            //    "您确定要导入新权重表，覆盖当前权重表吗？", "请确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            //if (response == System.Windows.Forms.DialogResult.Yes)
            //{
                for (int k = nowClassNum; k > 0; k--)
                {
                    TruncateTable(nowProject + "_Class_3_Weight");
                }
                //TruncateTable(nowProject + "_Class_3_Weight");
                //TruncateTable(nowProject + "_Class_3_Weight");
                //TruncateTable(nowProject + "_Class_3_Weight");
                SqlCommand commandInsert = DbUtil.getConnection().CreateCommand();
                commandInsert.CommandText = sqlInsert;
                commandInsert.ExecuteNonQuery();
                conn.Close();
            //}
        }

    }

}
