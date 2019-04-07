using System;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using System.Text;
using System.Configuration;

namespace ZI
{
	public partial class DbUtil
	{

		#region "ADO Base"
		private static SqlConnection conn;
        public static string nowProject = "";//当前项目代码
        public static string nowProjectAlias = "";//当前项目别名
        public static int nowClassNum = 0;//当前项目的级数
        public static int nowIndex = 0;//当前项目的指数类型
        public static string sort = "";//指数发布周期

        public static string ConnectionString = ConfigurationManager.ConnectionStrings["ZI.Properties.Settings.ZI_DataBaseConnectionString"].ConnectionString.ToString();
		
        /// <summary>
		/// 执行查询，返回第一条记录
		/// </summary>
		/// <param name="sql"></param>
		/// <returns></returns>
		public static object ExecuteScalar(String sql)
		{
			SqlConnection conn = getConnection();
			IDbCommand command = conn.CreateCommand();
			command.CommandText = sql;
			return command.ExecuteScalar();
		}

		/// <summary>
		/// 执行查询，返回全部记录
		/// </summary>
		/// <param name="sql">要执行的SQL语句</param>
		/// <param name="paramsHash">参数列表</param>
		/// <returns></returns>
		public static void ExecuteReader(out  SqlDataReader reader, string sql, Hashtable paramsHash)
		{

			SqlCommand command = getConnection().CreateCommand();
			command.CommandText = sql;
			foreach (string key in paramsHash.Keys)
			{
				command.Parameters.AddWithValue(key, paramsHash[key]);
			}
			reader = command.ExecuteReader();
		}
		/// <summary>
		/// 执行查询，返回全部记录
		/// </summary>
		/// <param name="sql">要执行的SQL语句</param>
		/// <returns>SqlDataReader，记得使用完后要手动关闭Reader</returns>
		public static void ExecuteReader(out SqlDataReader reader, string sql)
		{
			ExecuteReader(out reader, sql, new Hashtable());
		}

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
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
			return conn;
		}

		/// <summary>
		/// 获取一个全新的数据库连接，使用完后应手动关闭
		/// </summary>
		/// <returns></returns>
		public static SqlConnection getNewConnection()
		{
			SqlConnection conn = new SqlConnection();
			conn.ConnectionString = ConnectionString;
			conn.Open();
			return conn;
		}
#endregion
        /// <summary>
        /// 判断项目表死否存在，并创建该表
        /// </summary>
        /// <returns></returns>
        /// <summary>
       /// <returns></returns>
        public static void checkProject()
        {
            String sql = "if not exists ( select *  from  sysobjects where name = 'project'and type = 'U') create table  project (projectID [bigint] IDENTITY(1,1) NOT NULL, projectCode varchar(50) not null,projectName varchar(50) not null,projectClassNum int not null,projectIndex int not null)";
            SqlCommand command = new SqlCommand(sql, getConnection());
            command.ExecuteNonQuery();
        }

        /// 初始化数据库
        /// </summary>
        /// <returns></returns>
        public static void initDataBase() 
        {
            //周价格定级
          /*  String sql = "insert into SJQ_Price_Index_Class_Third_Week(CreatedDate,Class_Third_Code,Class_Third_Index) select '2011-06-01',Class_Third_Code,100 from SJQ_Class_Third";
            SqlCommand command = new SqlCommand(sql, getConnection());
            command.ExecuteNonQuery();
            sql = "insert into SJQ_Price_Index_Class_Second_Week(CreatedDate,Class_Second_Code,Class_Second_Index) select '2011-06-01',Class_Second_Code,100 from SJQ_Class_Second";
            command.CommandText = sql;
            command.ExecuteNonQuery();
            sql = "insert into SJQ_Price_Index_Class_First_Week(CreatedDate,Class_First_Code,Class_First_Index) select '2011-06-01',ID,100 from SJQ_Class_First";
            command.CommandText = sql;
            command.ExecuteNonQuery();
            sql = "insert into SJQ_Price_Index_Week(CreatedDate,PriceIndex) select top 1 '2011-06-01',100 from SJQ_Class_First";
            command.CommandText = sql;
            command.ExecuteNonQuery();
            //月价格定级
            sql = "insert into SJQ_Price_Index_Class_Third_Month(CreatedDate,Class_Third_Code,Class_Third_Index) select '2011-06-01',Class_Third_Code,100 from SJQ_Class_Third";
            command.CommandText = sql;
            command.ExecuteNonQuery();
            sql = "insert into SJQ_Price_Index_Class_Second_Month(CreatedDate,Class_Second_Code,Class_Second_Index) select '2011-06-01',Class_Second_Code,100 from SJQ_Class_Second";
            command.CommandText = sql;
            command.ExecuteNonQuery();
            sql = "insert into SJQ_Price_Index_Class_First_Month(CreatedDate,Class_First_Code,Class_First_Index) select '2011-06-01',ID,100 from SJQ_Class_First";
            command.CommandText = sql;
            command.ExecuteNonQuery();
            sql = "insert into SJQ_Price_Index_Month(CreatedDate,PriceIndex) select top 1 '2011-06-01',100 from SJQ_Class_First";
            command.CommandText = sql;
            command.ExecuteNonQuery();
            //景气月定级
            sql = "insert into SJQ_Boom_Index_Class_Third_Month(CreatedDate,Class_Third_Code,Fash_Index,Scale_Index,Confidence_Index,Boom_Index) select '2011-06-01',Class_Third_Code,1000,1000,1000,1000 from SJQ_Class_Third";
            command.CommandText = sql;
            command.ExecuteNonQuery();
            sql = "insert into SJQ_Boom_Index_Class_Second_Month(CreatedDate,Class_Second_Code,Fash_Index,Scale_Index,Confidence_Index,Boom_Index) select '2011-06-01',Class_Second_Code,1000,1000,1000,1000 from SJQ_Class_Second";
            command.CommandText = sql;
            command.ExecuteNonQuery();
            sql = "insert into SJQ_Boom_Index_Class_First_Month(CreatedDate,Class_First_Code,Fash_Index,Scale_Index,Confidence_Index,Boom_Index) select '2011-06-01',ID,1000,1000,1000,1000 from SJQ_Class_First";
            command.CommandText = sql;
            command.ExecuteNonQuery();
            sql = "insert into SJQ_Boom_Index_Month(CreatedDate,Fash_Index,Scale_Index,Confidence_Index,Boom_Index) select top 1 '2011-06-01',1000,1000,1000,1000 from SJQ_Class_First";
            command.CommandText = sql;
            command.ExecuteNonQuery();*/

        }
		#region 分类指数计算
		/// <summary>
		/// 汇总日价格表，增量更新
		/// </summary>
		/// <returns></returns>
        public static int totalDayPrice()
		{

			String sql = @"select top 1 createddate from "+nowProject+"_Price_Etl_Day order by createddate desc";

			SqlCommand command = new SqlCommand(sql, getConnection());
			Object createdDate = command.ExecuteScalar();
			createdDate = createdDate == null ? "2011-06-01" : createdDate;

			//汇总原始数据到日平均数据
            sql = @"INSERT INTO " + nowProject + "_Price_Etl_Day( CreatedDate , Class_Code , AveragePrice ) SELECT  CreatedDate, Class_Code, AVG(Price) as AveragePrice FROM " + nowProject + "_Price_Pick  where CreatedDate>@CreatedDate  and Approvaled=1 and price<>0 group by CreatedDate,Class_Code ";
			command.CommandText = sql;
			command.Parameters.AddWithValue("@CreatedDate", createdDate);
			int rows = command.ExecuteNonQuery();
            if (nowIndex == 1)
            {
                //如果是第一次导入某代表品，将之存入基期，作为基期价格
                sql = @"insert into " + nowProject + "_Price_Ods_Base(CreatedDate,Class_Code,AveragePrice) select t.CreatedDate,t.Class_Code,t.AveragePrice/0.94 from( Select rank() over(partition by Class_Code order by CreatedDate ) as id, AveragePrice,Class_Code,CreatedDate from " + nowProject + "_Price_Etl_Day )t where t.id=1 and t.Class_Code not in(select Class_Code from " + nowProject + "_Price_Ods_Base)";
                command.CommandText = sql;
                command.Parameters.Clear();
                int delrows = command.ExecuteNonQuery();
            }
                    //先计算本次汇总期数
            sql = @"select count(distinct CreatedDate) from " + nowProject + "_Price_Etl_Day where CreatedDate>@CreatedDate";
                    command.CommandText = sql;
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@CreatedDate", createdDate);
                    int count =(int) command.ExecuteScalar();
                    //把该日缺价格的代表品用上期价格代替 
                    sql = @"INSERT INTO " + nowProject + "_Price_Etl_Day( CreatedDate , Class_Code , AveragePrice ) select aa.CreatedDate,w.Class_Code,w.AveragePrice from " + nowProject + "_Price_Etl_Day w,(SELECT  distinct tb.CreatedDate lastCreatedDate,ta.CreatedDate FROM  " + nowProject + "_Price_Etl_Day ta,  " + nowProject + "_Price_Etl_Day tb WHERE  ta.CreatedDate =(select top 1 CreatedDate from " + nowProject + "_Price_Etl_Day tt where tt.CreatedDate>tb.CreatedDate order by tt.CreatedDate))aa  where w.CreatedDate=aa.lastCreatedDate and Class_Code not in (select Class_Code from " + nowProject + "_Price_Etl_Day  where CreatedDate=aa.CreatedDate)";
                    command.CommandText = sql;
                    command.Parameters.Clear();
                    int addlrows = 0;
                    for (int i = 0; i <count; i++)
                    {
                        addlrows+= command.ExecuteNonQuery();
                    }
                    string messString = "日价格表增加了" + (rows + addlrows) + "条数据";
                    //计算日价格变动
                    sql = @"UPDATE " + nowProject + "_Price_Etl_Day SET RingMove = t1.AveragePrice - t1.LastAveragePrice, RingMoveRate = (t1.AveragePrice - t1.LastAveragePrice) * 100 / t1.LastAveragePrice FROM " + nowProject + "_Price_Etl_Day t,(SELECT rank() OVER (partition BY ta.CreatedDate, ta.Class_Code ORDER BY tb.CreatedDate DESC) AS Date_Rank, tb.CreatedDate lastCreatedDate,  tb.AveragePrice lastAveragePrice, ta.*  FROM  " + nowProject + "_Price_Etl_Day ta,  " + nowProject + "_Price_Etl_Day tb WHERE ta.Class_Code = tb.Class_Code AND ta.CreatedDate =(select top 1 CreatedDate from " + nowProject + "_Price_Etl_Day tt where tt.CreatedDate>tb.CreatedDate and tt.Class_Code=tb.Class_Code order by tt.CreatedDate )) t1 WHERE  t.Class_Code = t1.Class_Code AND t.CreatedDate = t1.CreatedDate and  t.CreatedDate>@CreatedDate";

			command.CommandText = sql;
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@CreatedDate", createdDate);
			command.ExecuteNonQuery();
			MessageBox.Show(messString);
			return rows;
		}
        /// <summary>
        /// 汇总周价格表，增量更新
        /// </summary>
        /// <returns></returns>
        public static int totalWeekPrice()
        {
            String sql = @"select top 1 createddate from " + nowProject + "_Price_Etl_Week order by createddate desc";

            SqlCommand command = new SqlCommand(sql, getConnection());
            Object createdDate = command.ExecuteScalar();
            createdDate = createdDate == null ? "2011-06-01" : createdDate;

            //汇总原始数据到周平均数据
            if (nowIndex == 2)
            {
                sql = @"INSERT INTO " + nowProject + "_Price_Etl_Week( CreatedDate , Class_Code , AveragePrice ) SELECT  CreatedWeekend, Class_Code, AVG(Price) as AveragePrice FROM " + nowProject + "_Price_Pick  where CreatedWeekend>@CreatedDate  and Approvaled=1 and price<>0 group by CreatedWeekend,Class_Code ";
            }
            else 
            {
                sql = @"INSERT INTO " + nowProject + "_Price_Etl_Week( CreatedDate , Class_Code , AveragePrice )SELECT  CreatedWeekend, Class_Code, AVG(Price) as AveragePrice FROM " + nowProject + "_Price_Pick  where CreatedWeekend>@CreatedDate  and Approvaled=1 and price<>0 and CreatedWeekend<(select top 1 CreatedWeekend from " + nowProject + "_Price_Pick order by CreatedWeekend desc) group by CreatedWeekend,Class_Code ";
            }
            
            command.CommandText = sql;
            command.Parameters.AddWithValue("@CreatedDate", createdDate);
            int rows = command.ExecuteNonQuery();
           if (nowIndex == 2)
            {
                //如果是第一次导入某代表品，将之存入基期，作为基期价格
                sql = @"insert into " + nowProject + "_Price_Ods_Base(CreatedDate,Class_Code,AveragePrice) select t.CreatedDate,t.Class_Code,t.AveragePrice/0.94 from( Select rank() over(partition by Class_Code order by CreatedDate ) as id, AveragePrice,Class_Code,CreatedDate from " + nowProject + "_Price_Etl_Week )t where t.id=1 and t.Class_Code not in(select Class_Code from " + nowProject + "_Price_Ods_Base)";
                command.CommandText = sql;
                command.Parameters.Clear();
                int delrows = command.ExecuteNonQuery();
            }
            
            //先计算本次汇总期数
            sql = @"select count(distinct CreatedDate) from " + nowProject + "_Price_Etl_Week where CreatedDate>@CreatedDate";
            command.CommandText = sql;
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@CreatedDate", createdDate);
            int count = (int)command.ExecuteScalar();
            //把本周缺价格的代表品用上期价格代替 
            sql = @"INSERT INTO " + nowProject + "_Price_Etl_Week( CreatedDate , Class_Code , AveragePrice ) select aa.CreatedDate,w.Class_Code,w.AveragePrice from " + nowProject + "_Price_Etl_Week w,(SELECT  distinct tb.CreatedDate lastCreatedDate,ta.CreatedDate FROM  " + nowProject + "_Price_Etl_Week ta,  " + nowProject + "_Price_Etl_Week tb WHERE  ta.CreatedDate =(select top 1 CreatedDate from " + nowProject + "_Price_Etl_Week tt where tt.CreatedDate>tb.CreatedDate order by tt.CreatedDate))aa  where w.CreatedDate=aa.lastCreatedDate and Class_Code not in (select Class_Code from " + nowProject + "_Price_Etl_Week  where CreatedDate=aa.CreatedDate)";
            command.CommandText = sql;
            command.Parameters.Clear();
            int addlrows = 0;
            for (int i = 0; i < count; i++)
            {
                addlrows += command.ExecuteNonQuery();
            }
            string messString = "周价格表增加了" + (rows + addlrows) + "条数据";
            //计算周价格变动
            sql = @"UPDATE " + nowProject + "_Price_Etl_Week SET RingMove = t1.AveragePrice - t1.LastAveragePrice, RingMoveRate = (t1.AveragePrice - t1.LastAveragePrice) * 100 / t1.LastAveragePrice FROM " + nowProject + "_Price_Etl_Week t,(SELECT rank() OVER (partition BY ta.CreatedDate, ta.Class_Code ORDER BY tb.CreatedDate DESC) AS Date_Rank, tb.CreatedDate lastCreatedDate,  tb.AveragePrice lastAveragePrice, ta.*  FROM  " + nowProject + "_Price_Etl_Week ta,  " + nowProject + "_Price_Etl_Week tb WHERE ta.Class_Code = tb.Class_Code AND ta.CreatedDate =(select top 1 CreatedDate from " + nowProject + "_Price_Etl_Week tt where tt.CreatedDate>tb.CreatedDate and tt.Class_Code=tb.Class_Code order by tt.CreatedDate )) t1 WHERE  t.Class_Code = t1.Class_Code AND t.CreatedDate = t1.CreatedDate and  t.CreatedDate>@CreatedDate";

            command.CommandText = sql;
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@CreatedDate", createdDate);
            command.ExecuteNonQuery();
            MessageBox.Show(messString);
            return rows;
        }
        /// <summary>
        /// 汇总月价格表，增量更新
        /// </summary>
        /// <returns></returns>
        public static int totalMonthPrice()
		{
            String sql = @"select top 1 createddate from " + nowProject + "_Price_Etl_Month order by createddate desc";

			SqlCommand command = new SqlCommand(sql, getConnection());
			Object createdDate = command.ExecuteScalar();
			createdDate = createdDate == null ? "2011-06-01" : createdDate;

			//汇总原始数据到月平均数据
            if (nowIndex == 3)
            {
                sql = @"INSERT INTO " + nowProject + "_Price_Etl_Month ( CreatedDate , Class_Code , AveragePrice )SELECT  CreatedMonth, Class_Code, AVG(Price) as AveragePrice FROM " + nowProject + "_Price_Pick  where CreatedMonth>@CreatedDate and Price<>0 and Approvaled=1 group by CreatedMonth,Class_Code";
            }
            else
            {
                sql = @"INSERT INTO " + nowProject + "_Price_Etl_Month ( CreatedDate , Class_Code , AveragePrice )SELECT  CreatedMonth, Class_Code, AVG(Price) as AveragePrice FROM " + nowProject + "_Price_Pick  where CreatedMonth>@CreatedDate and Price<>0 and CreatedMonth<(select top 1 CreatedMonth from " + nowProject + "_Price_Pick order by CreatedMonth desc) and Approvaled=1 group by CreatedMonth,Class_Code";
            }
            command.CommandText = sql;
			command.Parameters.AddWithValue("@CreatedDate", createdDate);
			int rows = command.ExecuteNonQuery();

            if (nowIndex == 3)
            {
                //如果是第一次导入某代表品，将之存入基期，作为基期价格
                sql = @"insert into " + nowProject + "_Price_Ods_Base(CreatedDate,Class_Code,AveragePrice) select t.CreatedDate,t.Class_Code,t.AveragePrice/0.94 from( Select rank() over(partition by Class_Code order by CreatedDate ) as id, AveragePrice,Class_Code,CreatedDate from " + nowProject + "_Price_Etl_Month )t where t.id=1 and t.Class_Code not in(select Class_Code from " + nowProject + "_Price_Ods_Base)";
                command.CommandText = sql;
                command.Parameters.Clear();
                int delrows = command.ExecuteNonQuery();
            }

           //先计算本次汇总期数
            sql = @"select count(distinct CreatedDate) from " + nowProject + "_Price_Etl_Month where CreatedDate>@CreatedDate";
            command.CommandText = sql;
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@CreatedDate", createdDate);
            int count = (int)command.ExecuteScalar();
            //把本月缺价格的代表品用上月价格代替 
            sql = @"INSERT INTO " + nowProject + "_Price_Etl_Month( CreatedDate , Class_Code , AveragePrice ) select aa.CreatedDate,w.Class_Code,w.AveragePrice from " + nowProject + "_Price_Etl_Month w,(SELECT  distinct tb.CreatedDate lastCreatedDate,ta.CreatedDate FROM  " + nowProject + "_Price_Etl_Month ta,  " + nowProject + "_Price_Etl_Month tb WHERE  ta.CreatedDate =(select top 1 CreatedDate from " + nowProject + "_Price_Etl_Month tt where tt.CreatedDate>tb.CreatedDate order by tt.CreatedDate))aa  where w.CreatedDate=aa.lastCreatedDate and Class_Code not in (select Class_Code from " + nowProject + "_Price_Etl_Month  where CreatedDate=aa.CreatedDate)";
            command.CommandText = sql;
            command.Parameters.Clear();
            int addlrows = 0;
            for (int i = 0; i < count; i++)
            {
                addlrows += command.ExecuteNonQuery();
            }
			string messString = "月价格表增加了" + (rows+addlrows)+"条数据";
            //计算月价格变动
            sql = @"UPDATE  " + nowProject + "_Price_Etl_Month SET RingMove = t1.AveragePrice - t1.LastAveragePrice, RingMoveRate = (t1.AveragePrice - t1.LastAveragePrice) * 100 / t1.LastAveragePrice FROM " + nowProject + "_Price_Etl_Month t,(SELECT rank() OVER (partition BY ta.CreatedDate, ta.Class_Code ORDER BY tb.CreatedDate DESC) AS Date_Rank, tb.CreatedDate lastCreatedDate,  tb.AveragePrice lastAveragePrice, ta.*  FROM  " + nowProject + "_Price_Etl_Month ta,  " + nowProject + "_Price_Etl_Month tb WHERE ta.Class_Code = tb.Class_Code AND ta.CreatedDate =(select top 1 CreatedDate from " + nowProject + "_Price_Etl_Month tt where tt.CreatedDate>tb.CreatedDate and tt.Class_Code=tb.Class_Code order by tt.CreatedDate )) t1 WHERE  t.Class_Code = t1.Class_Code AND t.CreatedDate = t1.CreatedDate and  t.CreatedDate>@CreatedDate";
			command.CommandText = sql;
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@CreatedDate", createdDate);
			command.ExecuteNonQuery();
			MessageBox.Show(messString);
			return rows;
		}
        /// <summary>
        /// 计算日价格指数，增量更新
        /// </summary>
        public static void computePriceIndexDay()
        {
            String sql = @"select top 1 CreatedDate from " + nowProject + "_Price_Index_Class_" + nowClassNum + "_Day order by CreatedDate desc";

            SqlCommand command = new SqlCommand(sql, getConnection());
            //command.CommandText = sql;
            Object createdDate = command.ExecuteScalar();
            createdDate = createdDate == null ? "2011-06-01" : createdDate;
            //日三级指数表
            sql = @"insert into " + nowProject + "_Price_Index_Class_" + nowClassNum + "_Day(CreatedDate,Class_Code,AveragePrice,BasePrice,Class_Index)select wp.CreatedDate,bp.Class_Code,wp.AveragePrice,bp.AveragePrice BasePrice,wp.AveragePrice*100/bp.AveragePrice ProductIndex from " + nowProject + "_Price_Ods_Base bp," + nowProject + "_Price_Etl_Day wp where bp.Class_Code = wp.Class_Code and wp.AveragePrice<>0 and wp.createddate>@createdDate";

            command.CommandText = sql;
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@createdDate", createdDate);

            int rows = command.ExecuteNonQuery();
            //---------------------------------------------------------//
            //先计算本次期数
            sql = @"select count(distinct CreatedDate) from " + nowProject + "_Price_Index_Class_" + nowClassNum + "_Day where CreatedDate>@CreatedDate";
            command.CommandText = sql;
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@CreatedDate", createdDate);
            int count = (int)command.ExecuteScalar();
            //把该缺价格的代表品用上期价格代替 
            sql = @"INSERT INTO " + nowProject + "_Price_Index_Class_" + nowClassNum + "_Day ( CreatedDate , Class_Code , AveragePrice,BasePrice,Class_Index ) select aa.CreatedDate,w.Class_Code,w.AveragePrice,w.BasePrice,w.Class_Index from " + nowProject + "_Price_Index_Class_" + nowClassNum + "_Day w,(SELECT  distinct tb.CreatedDate lastCreatedDate,ta.CreatedDate FROM  " + nowProject + "_Price_Index_Class_" + nowClassNum + "_Day ta,  " + nowProject + "_Price_Index_Class_" + nowClassNum + "_Day tb WHERE  ta.CreatedDate =(select top 1 CreatedDate from " + nowProject + "_Price_Index_Class_" + nowClassNum + "_Day tt where tt.CreatedDate>tb.CreatedDate order by tt.CreatedDate))aa  where w.CreatedDate=aa.lastCreatedDate and Class_Code not in (select Class_Code from " + nowProject + "_Price_Index_Class_" + nowClassNum + "_Day  where CreatedDate=aa.CreatedDate)";
            command.CommandText = sql;
            command.Parameters.Clear();
            int addlrows = 0;
            for (int i = 0; i < count; i++)
            {
                addlrows += command.ExecuteNonQuery();
            }
            //--------------------------------------------------------//
            string smess = "日"+nowClassNum+"级指数表增加了" + (rows + addlrows) + "条数据";

            sql = @"UPDATE  " + nowProject + "_Price_Index_Class_" + nowClassNum + "_Day SET RingMove = t1.Class_Index- t1.lastClass_Index, RingMoveRate = case t1.lastClass_Index when 0 then 0 else (t1.Class_Index - t1.lastClass_Index) * 100 / t1.lastClass_Index end FROM " + nowProject + "_Price_Index_Class_" + nowClassNum + "_Day t,(SELECT rank() OVER (partition BY ta.CreatedDate, ta.Class_Code ORDER BY tb.CreatedDate DESC) AS Date_Rank, tb.CreatedDate lastCreatedDate,  tb.Class_Index lastClass_Index, ta.*  FROM  " + nowProject + "_Price_Index_Class_" + nowClassNum + "_Day ta,  " + nowProject + "_Price_Index_Class_" + nowClassNum + "_Day tb WHERE ta.Class_Code = tb.Class_Code AND ta.CreatedDate =(select top 1 CreatedDate from " + nowProject + "_Price_Index_Class_"+nowClassNum+"_Day tt where tt.CreatedDate>tb.CreatedDate and tt.Class_Code=tb.Class_Code order by tt.CreatedDate )) t1 WHERE  t.Class_Code = t1.Class_Code AND t.CreatedDate = t1.CreatedDate and t.CreatedDate> @createdDate";

            command.CommandText = sql;
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@createdDate", createdDate);
            command.ExecuteNonQuery();
            for (int k = nowClassNum-1; k > 0; k--)
            {
                //日二级指数表
                command.CommandText = "select top 1 CreatedDate from " + nowProject + "_Price_Index_Class_" + k+ "_Day order by CreatedDate desc";
                createdDate = command.ExecuteScalar();
                createdDate = createdDate == null ? "2011-06-01" : createdDate;

                sql = @"insert into " + nowProject + "_Price_Index_Class_" + k+ "_Day(CreatedDate,Class_Code,Class_Index) select t1.CreatedDate,t1.Class_"+k+"_Code, case SUM(WeightValue) when 0 then 0 else SUM(t1.Class_Index * WeightValue) / SUM(WeightValue) end from " + nowProject + "_Price_Index_Class_" + nowClassNum + "_Day t1," + nowProject + "_Class_" + nowClassNum + "_Weight t2 where t1.Class_Code = t2.Class_Code and t1.createddate >@createdDate and t2.state = 1 and t1.Class_Index<>0	group by t1.CreatedDate,t1.Class_"+k+"_Code";

                command.CommandText = sql;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@createdDate", createdDate);
                rows = command.ExecuteNonQuery();
                smess += "\n日"+k+"级类别指数表增加了" + rows + "条数据";

                sql = @"UPDATE  " + nowProject + "_Price_Index_Class_" + k + "_Day SET RingMove = t1.Class_Index- t1.lastClass_Index, RingMoveRate = case t1.lastClass_Index when 0 then 0 else (t1.Class_Index - t1.lastClass_Index) * 100 / t1.lastClass_Index end FROM " + nowProject + "_Price_Index_Class_" + k + "_Day t,(SELECT rank() OVER (partition BY ta.CreatedDate, ta.Class_Code ORDER BY tb.CreatedDate DESC) AS Date_Rank, tb.CreatedDate lastCreatedDate,  tb.Class_Index lastClass_Index, ta.*  FROM  " + nowProject + "_Price_Index_Class_" + k + "_Day ta,  " + nowProject + "_Price_Index_Class_" + k + "_Day tb WHERE ta.Class_Code = tb.Class_Code AND ta.CreatedDate =(select top 1 CreatedDate from " + nowProject + "_Price_Index_Class_" + k + "_Day tt where tt.CreatedDate>tb.CreatedDate and tt.Class_Code=tb.Class_Code order by tt.CreatedDate )) t1 WHERE  t.Class_Code = t1.Class_Code AND t.CreatedDate = t1.CreatedDate and t.CreatedDate> @createdDate";

                command.CommandText = sql;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@createdDate", createdDate);
                command.ExecuteNonQuery();
                
            }
            //日价格总指数表
            command.CommandText = "select top 1 CreatedDate from " + nowProject + "_Price_Index_Day order by CreatedDate desc";
            createdDate = command.ExecuteScalar();
            createdDate = createdDate == null ? "2011-06-01" : createdDate;

            sql = @"insert into " + nowProject + "_Price_Index_Day(CreatedDate,PriceIndex)select t1.CreatedDate,case SUM(WeightValue) when 0 then 0 else  sum(t1.Class_Index * t2.WeightValue)/ SUM(WeightValue)  end PriceIndex from " + nowProject + "_Price_Index_Class_1_Day t1," + nowProject + "_Class_1_Weight t2 where t1.Class_Code = t2.Class_Code and t1.createddate >@createdDate and t2.state = 1 and t1.Class_Index<>0 group by t1.CreatedDate	";

            command.CommandText = sql;
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@createdDate", createdDate);
            rows = command.ExecuteNonQuery();
            smess += "\n日价格总指数表增加了" + rows + "条数据";

            sql = @"update " + nowProject + "_Price_Index_Day set RingMove=t1.PriceIndex-t1.LastPriceIndex,RingMoveRate= case t1.LastPriceIndex  when 0 then 0 else (t1.PriceIndex-t1.LastPriceIndex)*100/t1.LastPriceIndex end from " + nowProject + "_Price_Index_Day t,(select rank() over (partition by t1.CreatedDate order by t2.CreatedDate desc) as Date_Rank,t2.CreatedDate lastCreatedDate,t2.PriceIndex lastPriceIndex,t1.* from " + nowProject + "_Price_Index_Day t1," + nowProject + "_Price_Index_Day t2	where t1.CreatedDate=(select top 1 CreatedDate from " + nowProject + "_Price_Index_Day  tt where tt.CreatedDate>t2.CreatedDate order by tt.CreatedDate)) t1 where t.CreatedDate = t1.CreatedDate  and t.CreatedDate>@createdDate";
            command.CommandText = sql;
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@createdDate", createdDate);
            command.ExecuteNonQuery();
            MessageBox.Show(smess);
        }
		/// <summary>
		/// 计算周价格指数，增量更新
		/// </summary>
		public static void computePriceIndexWeek()
		{
            String sql = @"select top 1 CreatedDate from " + nowProject + "_Price_Index_Class_" + nowClassNum + "_Week order by CreatedDate desc";

            SqlCommand command = new SqlCommand(sql, getConnection());
            //command.CommandText = sql;
            Object createdDate = command.ExecuteScalar();
            createdDate = createdDate == null ? "2011-06-01" : createdDate;
            //周三级指数表
            sql = @"insert into " + nowProject + "_Price_Index_Class_" + nowClassNum + "_Week(CreatedDate,Class_Code,AveragePrice,BasePrice,Class_Index)select wp.CreatedDate,bp.Class_Code,wp.AveragePrice,bp.AveragePrice BasePrice,wp.AveragePrice*100/bp.AveragePrice ProductIndex from " + nowProject + "_Price_Ods_Base bp," + nowProject + "_Price_Etl_Week wp where bp.Class_Code = wp.Class_Code and wp.AveragePrice<>0 and wp.createddate>@createdDate";

            command.CommandText = sql;
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@createdDate", createdDate);

            int rows = command.ExecuteNonQuery();
            //---------------------------------------------------------//
            //先计算本次期数
            sql = @"select count(distinct CreatedDate) from " + nowProject + "_Price_Index_Class_" + nowClassNum + "_Week where CreatedDate>@CreatedDate";
            command.CommandText = sql;
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@CreatedDate", createdDate);
            int count = (int)command.ExecuteScalar();
            //把本周缺价格的代表品用上期价格代替 
            sql = @"INSERT INTO " + nowProject + "_Price_Index_Class_" + nowClassNum + "_Week ( CreatedDate , Class_Code , AveragePrice,BasePrice,Class_Index ) select aa.CreatedDate,w.Class_Code,w.AveragePrice,w.BasePrice,w.Class_Index from " + nowProject + "_Price_Index_Class_" + nowClassNum + "_Week w,(SELECT  distinct tb.CreatedDate lastCreatedDate,ta.CreatedDate FROM  " + nowProject + "_Price_Index_Class_" + nowClassNum + "_Week ta,  " + nowProject + "_Price_Index_Class_" + nowClassNum + "_Week tb WHERE  ta.CreatedDate =(select top 1 CreatedDate from " + nowProject + "_Price_Index_Class_" + nowClassNum + "_Week tt where tt.CreatedDate>tb.CreatedDate order by tt.CreatedDate))aa  where w.CreatedDate=aa.lastCreatedDate and Class_Code not in (select Class_Code from " + nowProject + "_Price_Index_Class_" + nowClassNum + "_Week  where CreatedDate=aa.CreatedDate)";
            command.CommandText = sql;
            command.Parameters.Clear();
            int addlrows = 0;
            for (int i = 0; i < count; i++)
            {
                addlrows += command.ExecuteNonQuery();
            }
            //--------------------------------------------------------//
            string smess = "周" + nowClassNum + "级指数表增加了" + (rows + addlrows) + "条数据";

            sql = @"UPDATE  " + nowProject + "_Price_Index_Class_" + nowClassNum + "_Week SET RingMove = t1.Class_Index- t1.lastClass_Index, RingMoveRate = case t1.lastClass_Index when 0 then 0 else (t1.Class_Index - t1.lastClass_Index) * 100 / t1.lastClass_Index end FROM " + nowProject + "_Price_Index_Class_" + nowClassNum + "_Week t,(SELECT rank() OVER (partition BY ta.CreatedDate, ta.Class_Code ORDER BY tb.CreatedDate DESC) AS Date_Rank, tb.CreatedDate lastCreatedDate,  tb.Class_Index lastClass_Index, ta.*  FROM  " + nowProject + "_Price_Index_Class_" + nowClassNum + "_Week ta,  " + nowProject + "_Price_Index_Class_" + nowClassNum + "_Week tb WHERE ta.Class_Code = tb.Class_Code AND ta.CreatedDate =(select top 1 CreatedDate from " + nowProject + "_Price_Index_Class_" + nowClassNum + "_Week tt where tt.CreatedDate>tb.CreatedDate and tt.Class_Code=tb.Class_Code order by tt.CreatedDate )) t1 WHERE  t.Class_Code = t1.Class_Code AND t.CreatedDate = t1.CreatedDate and t.CreatedDate> @createdDate";

            command.CommandText = sql;
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@createdDate", createdDate);
            command.ExecuteNonQuery();
            for (int k = nowClassNum - 1; k > 0; k--)
            {
                //周二级指数表
                command.CommandText = "select top 1 CreatedDate from " + nowProject + "_Price_Index_Class_" + k + "_Week order by CreatedDate desc";
                createdDate = command.ExecuteScalar();
                createdDate = createdDate == null ? "2011-06-01" : createdDate;

                sql = @"insert into " + nowProject + "_Price_Index_Class_" + k + "_Week(CreatedDate,Class_Code,Class_Index) select t1.CreatedDate,t1.Class_" + k + "_Code, case SUM(WeightValue) when 0 then 0 else SUM(t1.Class_Index * WeightValue) / SUM(WeightValue) end from " + nowProject + "_Price_Index_Class_" + nowClassNum + "_Week t1," + nowProject + "_Class_" + nowClassNum + "_Weight t2 where t1.Class_Code = t2.Class_Code and t1.createddate >@createdDate and t2.state = 1 and t1.Class_Index<>0	group by t1.CreatedDate,t1.Class_" + k + "_Code";

                command.CommandText = sql;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@createdDate", createdDate);
                rows = command.ExecuteNonQuery();
                smess += "\n周" + k + "级类别指数表增加了" + rows + "条数据";

                sql = @"UPDATE  " + nowProject + "_Price_Index_Class_" + k + "_Week SET RingMove = t1.Class_Index- t1.lastClass_Index, RingMoveRate = case t1.lastClass_Index when 0 then 0 else (t1.Class_Index - t1.lastClass_Index) * 100 / t1.lastClass_Index end FROM " + nowProject + "_Price_Index_Class_" + k + "_Week t,(SELECT rank() OVER (partition BY ta.CreatedDate, ta.Class_Code ORDER BY tb.CreatedDate DESC) AS Date_Rank, tb.CreatedDate lastCreatedDate,  tb.Class_Index lastClass_Index, ta.*  FROM  " + nowProject + "_Price_Index_Class_" + k + "_Week ta,  " + nowProject + "_Price_Index_Class_" + k + "_Week tb WHERE ta.Class_Code = tb.Class_Code AND ta.CreatedDate =(select top 1 CreatedDate from " + nowProject + "_Price_Index_Class_" + k + "_Week tt where tt.CreatedDate>tb.CreatedDate and tt.Class_Code=tb.Class_Code order by tt.CreatedDate )) t1 WHERE  t.Class_Code = t1.Class_Code AND t.CreatedDate = t1.CreatedDate and t.CreatedDate> @createdDate";

                command.CommandText = sql;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@createdDate", createdDate);
                command.ExecuteNonQuery();

            }
            //周价格总指数表
            command.CommandText = "select top 1 CreatedDate from " + nowProject + "_Price_Index_Week order by CreatedDate desc";
            createdDate = command.ExecuteScalar();
            createdDate = createdDate == null ? "2011-06-01" : createdDate;

            sql = @"insert into " + nowProject + "_Price_Index_Week(CreatedDate,PriceIndex)select t1.CreatedDate,case SUM(WeightValue) when 0 then 0 else  sum(t1.Class_Index * t2.WeightValue)/ SUM(WeightValue)  end PriceIndex from " + nowProject + "_Price_Index_Class_1_Week t1," + nowProject + "_Class_1_Weight t2 where t1.Class_Code = t2.Class_Code and t1.createddate >@createdDate and t2.state = 1 and t1.Class_Index<>0 group by t1.CreatedDate	";

            command.CommandText = sql;
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@createdDate", createdDate);
            rows = command.ExecuteNonQuery();
            smess += "\n周价格总指数表增加了" + rows + "条数据";

            sql = @"update " + nowProject + "_Price_Index_Week set RingMove=t1.PriceIndex-t1.LastPriceIndex,RingMoveRate= case t1.LastPriceIndex  when 0 then 0 else (t1.PriceIndex-t1.LastPriceIndex)*100/t1.LastPriceIndex end from " + nowProject + "_Price_Index_Week t,(select rank() over (partition by t1.CreatedDate order by t2.CreatedDate desc) as Date_Rank,t2.CreatedDate lastCreatedDate,t2.PriceIndex lastPriceIndex,t1.* from " + nowProject + "_Price_Index_Week t1," + nowProject + "_Price_Index_Week t2	where t1.CreatedDate=(select top 1 CreatedDate from " + nowProject + "_Price_Index_Week  tt where tt.CreatedDate>t2.CreatedDate order by tt.CreatedDate)) t1 where t.CreatedDate = t1.CreatedDate  and t.CreatedDate>@createdDate";
            command.CommandText = sql;
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@createdDate", createdDate);
            command.ExecuteNonQuery();
            MessageBox.Show(smess);
        }

        /// <summary>
        /// 日环比指数
        /// </summary>
        /// <returns></returns>
        public static void computeRingPriceIndexDay()
        {
            String sql = "";
            string returnstring = "";
            //===============================================计格指数===============================================
            for (int i = nowClassNum; i > 0; i--)
            {
                sql = @"SELECT TOP(1) CreatedDate FROM " + nowProject + "_PriceRing_Index_Class_" + i + "_Day ORDER BY CreatedDate DESC";
                SqlCommand command = new SqlCommand(sql, getConnection());
                //command.CommandText = sql;
                Object Date = command.ExecuteScalar();
                Date = Date == null ? "2011-06-01" : Date;

                
                command.CommandText = @"SELECT TOP(1) CreatedDate FROM " + nowProject + "_PriceRing_Index_Class_" + i + "_Day ORDER BY CreatedDate DESC";
                Date = command.ExecuteScalar();
                Date = Date == null ? "2011-06-01" : Date;

                sql = @"INSERT INTO " + nowProject + "_PriceRing_Index_Class_" + i + "_Day (CreatedDate,Class_Code,Class_Index)SELECT  t.CreatedDate, t.Class_Code,case t1.lastClass_Index when 0 then 0 else  t1.Class_Index * 100 / t1.lastClass_Index end AS RingPriceIndex FROM  " + nowProject + "_Price_Index_Class_" + i + "_Day t,(SELECT rank() OVER (partition BY ta.CreatedDate, ta.Class_Code ORDER BY tb.CreatedDate DESC) AS Date_Rank, tb.CreatedDate lastCreatedDate,  tb.Class_Index lastClass_Index, ta.*  FROM  " + nowProject + "_Price_Index_Class_" + i + "_Day ta,  " + nowProject + "_Price_Index_Class_" + i + "_Day tb WHERE ta.Class_Code = tb.Class_Code AND ta.CreatedDate =(select top 1 CreatedDate from " + nowProject + "_Price_Index_Class_" + i + "_Day tt where tt.CreatedDate>tb.CreatedDate and tt.Class_Code=tb.Class_Code order by tt.CreatedDate )) t1 WHERE   t .Class_Code = t1.Class_Code AND t .CreatedDate = t1.CreatedDate AND t.CreatedDate  > @Date";
                command.CommandText = sql;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@Date", Date);
                int rows = command.ExecuteNonQuery();
                returnstring = returnstring + "\n日" + i + "级类别环比指数表增加了" + rows + "条数据";


                sql = @"update " + nowProject + "_PriceRing_Index_Class_" + i + "_Day set RingMove=t1.Class_Index-t1.lastClass_Index,RingMoveRate= case t1.lastClass_Index when 0 then 0 else (t1.Class_Index-t1.lastClass_Index)*100/t1.lastClass_Index end FROM " + nowProject + "_PriceRing_Index_Class_" + i + "_Day t,(SELECT rank() OVER (partition BY ta.CreatedDate, ta.Class_Code ORDER BY tb.CreatedDate DESC) AS Date_Rank, tb.CreatedDate lastCreatedDate,  tb.Class_Index lastClass_Index, ta.*  FROM  " + nowProject + "_PriceRing_Index_Class_" + i + "_Day ta,  " + nowProject + "_PriceRing_Index_Class_" + i + "_Day tb WHERE ta.Class_Code = tb.Class_Code AND ta.CreatedDate =(select top 1 CreatedDate from " + nowProject + "_PriceRing_Index_Class_" + i + "_Day tt where tt.CreatedDate>tb.CreatedDate and tt.Class_Code=tb.Class_Code order by tt.CreatedDate )) t1 WHERE  t.Class_Code = t1.Class_Code AND t.CreatedDate = t1.CreatedDate and t.CreatedDate> @Date";
                command.CommandText = sql;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@Date", Date);
                rows = command.ExecuteNonQuery();
                command.ExecuteNonQuery();
            }
            //总价格环比表
            SqlCommand commandd = new SqlCommand(sql, getConnection());
            commandd.CommandText = @"SELECT TOP(1) CreatedDate FROM " + nowProject + "_PriceRing_Index_Day ORDER BY CreatedDate DESC";
            Object ADate = commandd.ExecuteScalar();
            ADate = ADate == null ? "2010-06-01" : ADate;

            sql = @"INSERT INTO " + nowProject + "_PriceRing_Index_Day(CreatedDate,PriceIndex) SELECT t.CreatedDate, case t1.lastPriceIndex when 0 then 0 else t1.PriceIndex * 100 / t1.lastPriceIndex end AS RingPriceIndex FROM  " + nowProject + "_Price_Index_Day t,(SELECT rank() OVER (partition BY ta.CreatedDate ORDER BY tb.CreatedDate DESC) AS Date_Rank, tb.CreatedDate lastCreatedDate,  tb.PriceIndex lastPriceIndex, ta.*  FROM  " + nowProject + "_Price_Index_Day ta,  " + nowProject + "_Price_Index_Day tb WHERE  ta.CreatedDate =(select top 1 CreatedDate from " + nowProject + "_Price_Index_Day tt where tt.CreatedDate>tb.CreatedDate  order by tt.CreatedDate )) t1 WHERE t .CreatedDate = t1.CreatedDate AND t.CreatedDate > @Date";
            commandd.CommandText = sql;
            commandd.Parameters.Clear();
            commandd.Parameters.AddWithValue("@Date", ADate);
            int rowss = commandd.ExecuteNonQuery();
            returnstring = returnstring + "\n日总类别环比指数表增加了" + rowss + "条数据";

            sql = @"update " + nowProject + "_PriceRing_Index_Day set RingMove=t1.PriceIndex-t1.lastPriceIndex,RingMoveRate=case t1.lastPriceIndex when 0 then 0 else (t1.PriceIndex-t1.lastPriceIndex)*100/t1.lastPriceIndex end FROM " + nowProject + "_PriceRing_Index_Day t,(SELECT rank() OVER (partition BY ta.CreatedDate ORDER BY tb.CreatedDate DESC) AS Date_Rank, tb.CreatedDate lastCreatedDate,  tb.PriceIndex lastPriceIndex, ta.*  FROM  " + nowProject + "_PriceRing_Index_Day ta,  " + nowProject + "_PriceRing_Index_Day tb WHERE  ta.CreatedDate =(select top 1 CreatedDate from " + nowProject + "_PriceRing_Index_Day tt where tt.CreatedDate>tb.CreatedDate  order by tt.CreatedDate )) t1 WHERE t.CreatedDate = t1.CreatedDate and t.CreatedDate>@Date";
            commandd.CommandText = sql;
            commandd.Parameters.Clear();
            commandd.Parameters.AddWithValue("@Date", ADate);
            commandd.ExecuteNonQuery();

            MessageBox.Show(returnstring);
        }



		/// <summary>
		/// 周环比指数
		/// </summary>
		/// <returns></returns>
		public static void computeRingPriceIndexWeek()
		{
            String sql = "";
            string returnstring = "";
			//===============================================计格指数===============================================
            for (int i = nowClassNum; i > 0; i--)
            {
                sql = @"SELECT TOP(1) CreatedDate FROM " + nowProject + "_PriceRing_Index_Class_"+i+"_Week ORDER BY CreatedDate DESC";
                SqlCommand command = new SqlCommand(sql, getConnection());
                //command.CommandText = sql;
                Object Date = command.ExecuteScalar();
                Date = Date == null ? "2011-06-01" : Date;

                //周产品指数表
                command.CommandText = @"SELECT TOP(1) CreatedDate FROM " + nowProject + "_PriceRing_Index_Class_" + i + "_Week ORDER BY CreatedDate DESC";
                Date = command.ExecuteScalar();
                Date = Date == null ? "2011-06-01" : Date;

                sql = @"INSERT INTO " + nowProject + "_PriceRing_Index_Class_" + i + "_Week (CreatedDate,Class_Code,Class_Index)SELECT  t.CreatedDate, t.Class_Code,case t1.lastClass_Index when 0 then 0 else  t1.Class_Index * 100 / t1.lastClass_Index end AS RingPriceIndex FROM  " + nowProject + "_Price_Index_Class_"+i+"_Week t,(SELECT rank() OVER (partition BY ta.CreatedDate, ta.Class_Code ORDER BY tb.CreatedDate DESC) AS Date_Rank, tb.CreatedDate lastCreatedDate,  tb.Class_Index lastClass_Index, ta.*  FROM  " + nowProject + "_Price_Index_Class_"+i+"_Week ta,  " + nowProject + "_Price_Index_Class_"+i+"_Week tb WHERE ta.Class_Code = tb.Class_Code AND ta.CreatedDate =(select top 1 CreatedDate from " + nowProject + "_Price_Index_Class_"+i+"_Week tt where tt.CreatedDate>tb.CreatedDate and tt.Class_Code=tb.Class_Code order by tt.CreatedDate )) t1 WHERE   t .Class_Code = t1.Class_Code AND t .CreatedDate = t1.CreatedDate AND t.CreatedDate  > @Date";
                command.CommandText = sql;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@Date", Date);
                int rows = command.ExecuteNonQuery();
                returnstring = returnstring + "\n周"+i+"级类别环比指数表增加了" + rows + "条数据";


                sql = @"update " + nowProject + "_PriceRing_Index_Class_"+i+"_Week set RingMove=t1.Class_Index-t1.lastClass_Index,RingMoveRate= case t1.lastClass_Index when 0 then 0 else (t1.Class_Index-t1.lastClass_Index)*100/t1.lastClass_Index end FROM " + nowProject + "_PriceRing_Index_Class_"+i+"_Week t,(SELECT rank() OVER (partition BY ta.CreatedDate, ta.Class_Code ORDER BY tb.CreatedDate DESC) AS Date_Rank, tb.CreatedDate lastCreatedDate,  tb.Class_Index lastClass_Index, ta.*  FROM  " + nowProject + "_PriceRing_Index_Class_"+i+"_Week ta,  " + nowProject + "_PriceRing_Index_Class_"+i+"_Week tb WHERE ta.Class_Code = tb.Class_Code AND ta.CreatedDate =(select top 1 CreatedDate from " + nowProject + "_PriceRing_Index_Class_"+i+"_Week tt where tt.CreatedDate>tb.CreatedDate and tt.Class_Code=tb.Class_Code order by tt.CreatedDate )) t1 WHERE  t.Class_Code = t1.Class_Code AND t.CreatedDate = t1.CreatedDate and t.CreatedDate> @Date";
                command.CommandText = sql;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@Date", Date);
                rows = command.ExecuteNonQuery();
                command.ExecuteNonQuery();
            }
            //总价格环比表
            SqlCommand commandd = new SqlCommand(sql, getConnection());
            commandd.CommandText = @"SELECT TOP(1) CreatedDate FROM " + nowProject + "_PriceRing_Index_Week ORDER BY CreatedDate DESC";
            Object ADate = commandd.ExecuteScalar();
            ADate = ADate == null ? "2010-06-01" : ADate;

            sql = @"INSERT INTO " + nowProject + "_PriceRing_Index_Week(CreatedDate,PriceIndex) SELECT t.CreatedDate, case t1.lastPriceIndex when 0 then 0 else t1.PriceIndex * 100 / t1.lastPriceIndex end AS RingPriceIndex FROM  " + nowProject + "_Price_Index_Week t,(SELECT rank() OVER (partition BY ta.CreatedDate ORDER BY tb.CreatedDate DESC) AS Date_Rank, tb.CreatedDate lastCreatedDate,  tb.PriceIndex lastPriceIndex, ta.*  FROM  " + nowProject + "_Price_Index_Week ta,  " + nowProject + "_Price_Index_Week tb WHERE  ta.CreatedDate =(select top 1 CreatedDate from " + nowProject + "_Price_Index_Week tt where tt.CreatedDate>tb.CreatedDate  order by tt.CreatedDate )) t1 WHERE t .CreatedDate = t1.CreatedDate AND t.CreatedDate > @Date";
            commandd.CommandText = sql;
            commandd.Parameters.Clear();
            commandd.Parameters.AddWithValue("@Date", ADate);
            int rowss = commandd.ExecuteNonQuery();
            returnstring = returnstring + "\n周总类别环比指数表增加了" + rowss + "条数据";

            sql = @"update " + nowProject + "_PriceRing_Index_Week set RingMove=t1.PriceIndex-t1.lastPriceIndex,RingMoveRate=case t1.lastPriceIndex when 0 then 0 else (t1.PriceIndex-t1.lastPriceIndex)*100/t1.lastPriceIndex end FROM " + nowProject + "_PriceRing_Index_Week t,(SELECT rank() OVER (partition BY ta.CreatedDate ORDER BY tb.CreatedDate DESC) AS Date_Rank, tb.CreatedDate lastCreatedDate,  tb.PriceIndex lastPriceIndex, ta.*  FROM  " + nowProject + "_PriceRing_Index_Week ta,  " + nowProject + "_PriceRing_Index_Week tb WHERE  ta.CreatedDate =(select top 1 CreatedDate from " + nowProject + "_PriceRing_Index_Week tt where tt.CreatedDate>tb.CreatedDate  order by tt.CreatedDate )) t1 WHERE t.CreatedDate = t1.CreatedDate and t.CreatedDate>@Date";
            commandd.CommandText = sql;
            commandd.Parameters.Clear();
            commandd.Parameters.AddWithValue("@Date", ADate);
            commandd.ExecuteNonQuery();

            MessageBox.Show(returnstring);
        }
        /// <summary>
        /// 计算月价格指数，增量更新
        /// </summary>
        public static void computePriceIndexMonth()
        {
            String sql = "";
            string smess ="";
            for (int j = nowClassNum; j > 0; j--)
            {
                sql = @"select top 1 CreatedDate from " + nowProject + "_Price_Index_Class_"+j+"_Month order by CreatedDate desc";

                SqlCommand command = new SqlCommand(sql, getConnection());
                //command.CommandText = sql;
                Object createdDate = command.ExecuteScalar();
                createdDate = createdDate == null ? "2011-06-01" : createdDate;
                string classcode = "";
                //月指数表
                if (j != nowClassNum)
                {
                    classcode = "Class_" + j + "_Code";
                }
                else
                {
                    classcode = "Class_Code";
                }
                sql = @"insert into " + nowProject + "_Price_Index_Class_"+j+"_Month(CreatedDate,Class_Code,AveragePrice,BasePrice,Class_Index) select wp.CreatedDate,bp."+classcode+",wp.AveragePrice,bp.AveragePrice BasePrice,wp.AveragePrice*100/bp.AveragePrice ProductIndex from " + nowProject + "_Price_Ods_Base bp," + nowProject + "_Price_Etl_Month wp where bp.Class_Code = wp.Class_Code and wp.AveragePrice<>0 and wp.createddate>@createdDate";

                command.CommandText = sql;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@createdDate", createdDate);
                int rows = command.ExecuteNonQuery();
                //----------------------------------------------------------------//
                sql = @"select count(distinct CreatedDate) from " + nowProject + "_Price_Index_Class_"+j+"_Month where CreatedDate>@CreatedDate";
                command.CommandText = sql;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@CreatedDate", createdDate);
                int count = (int)command.ExecuteScalar();
                //把本月缺价格的代表品用上月价格代替 
                sql = @"INSERT INTO " + nowProject + "_Price_Index_Class_"+j+"_Month( CreatedDate , Class_Code , AveragePrice,BasePrice,Class_Index )select aa.CreatedDate,w.Class_Code,w.AveragePrice,w.BasePrice,w.Class_Index from " + nowProject + "_Price_Index_Class_"+j+"_Month w,(SELECT  distinct tb.CreatedDate lastCreatedDate,ta.CreatedDate FROM  " + nowProject + "_Price_Index_Class_"+j+"_Month ta,  " + nowProject + "_Price_Index_Class_"+j+"_Month tb WHERE  ta.CreatedDate =(select top 1 CreatedDate from " + nowProject + "_Price_Index_Class_"+j+"_Month tt where tt.CreatedDate>tb.CreatedDate order by tt.CreatedDate))aa  where w.CreatedDate=aa.lastCreatedDate and Class_Code not in (select Class_Code from " + nowProject + "_Price_Index_Class_"+j+"_Month  where CreatedDate=aa.CreatedDate)";
                command.CommandText = sql;
                command.Parameters.Clear();
                int addlrows = 0;
                for (int i = 0; i < count; i++)
                {
                    addlrows += command.ExecuteNonQuery();
                }
                //---------------------------------------------------------------//

                smess = smess + "\n月" + j + "级指数表增加了" + (rows + addlrows )+ "条数据";

                sql = @"UPDATE  " + nowProject + "_Price_Index_Class_"+j+"_Month SET RingMove = t1.Class_Index- t1.lastClass_Index, RingMoveRate = case t1.lastClass_Index when 0 then 0 else (t1.Class_Index - t1.lastClass_Index) * 100 / t1.lastClass_Index end FROM " + nowProject + "_Price_Index_Class_"+j+"_Month t,(SELECT rank() OVER (partition BY ta.CreatedDate, ta.Class_Code ORDER BY tb.CreatedDate DESC) AS Date_Rank, tb.CreatedDate lastCreatedDate,  tb.Class_Index lastClass_Index, ta.*  FROM  " + nowProject + "_Price_Index_Class_"+j+"_Month ta,  " + nowProject + "_Price_Index_Class_"+j+"_Month tb WHERE ta.Class_Code = tb.Class_Code AND ta.CreatedDate =(select top 1 CreatedDate from " + nowProject + "_Price_Index_Class_"+j+"_Month tt where tt.CreatedDate>tb.CreatedDate and tt.Class_Code=tb.Class_Code order by tt.CreatedDate )) t1 WHERE  t.Class_Code = t1.Class_Code AND t.CreatedDate = t1.CreatedDate and t.CreatedDate> @createdDate";

                command.CommandText = sql;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@createdDate", createdDate);
                command.ExecuteNonQuery();
            }
            //月价格总指数表
            SqlCommand commandd = new SqlCommand(sql, getConnection());
            commandd.CommandText = "select top 1 CreatedDate from " + nowProject + "_Price_Index_Month order by CreatedDate desc";
            Object creatDate = commandd.ExecuteScalar();
            creatDate = creatDate == null ? "2011-06-01" : creatDate;

            sql = @"insert into " + nowProject + "_Price_Index_Month(CreatedDate,PriceIndex)select t1.CreatedDate,case SUM(WeightValue) when 0 then 0 else  sum(t1.Class_Index * t2.WeightValue)/ SUM(WeightValue)  end PriceIndex from " + nowProject + "_Price_Index_Class_1_Month t1," + nowProject + "_Class_1_Weight t2 where t1.Class_Code = t2.Class_Code and t1.createddate >@createdDate and t2.state = 1 and t1.Class_Index<>0 group by t1.CreatedDate	";

            commandd.CommandText = sql;
            commandd.Parameters.Clear();
            commandd.Parameters.AddWithValue("@createdDate", creatDate);
            int rowss = commandd.ExecuteNonQuery();
            smess += "\n月价格总指数表增加了" + rowss + "条数据";

            sql = @"update " + nowProject + "_Price_Index_Month set RingMove=t1.PriceIndex-t1.LastPriceIndex,RingMoveRate= case t1.LastPriceIndex  when 0 then 0 else (t1.PriceIndex-t1.LastPriceIndex)*100/t1.LastPriceIndex end from " + nowProject + "_Price_Index_Month t,(select rank() over (partition by t1.CreatedDate order by t2.CreatedDate desc) as Date_Rank,t2.CreatedDate lastCreatedDate,t2.PriceIndex lastPriceIndex,t1.* from " + nowProject + "_Price_Index_Month t1," + nowProject + "_Price_Index_Month t2	where t1.CreatedDate=(select top 1 CreatedDate from " + nowProject + "_Price_Index_Month  tt where tt.CreatedDate>t2.CreatedDate order by tt.CreatedDate)) t1 where t.CreatedDate = t1.CreatedDate  and t.CreatedDate>@createdDate";

            commandd.CommandText = sql;
            commandd.Parameters.Clear();
            commandd.Parameters.AddWithValue("@createdDate", creatDate);
            commandd.ExecuteNonQuery();
            MessageBox.Show(smess);

        }

        /// <summary>
        /// 月环比指数
        /// </summary>
        /// <returns></returns>
        public static void computeRingPriceIndexMonth()
        {
           
            //===============================================计格指数===============================================
            string returnstring = "";
            String sql = "";
            for(int j=nowClassNum;j>0;j--){
                sql = @"SELECT TOP(1) CreatedDate FROM " + nowProject + "_PriceRing_Index_Class_"+j+"_Month ORDER BY CreatedDate DESC";
                SqlCommand command = new SqlCommand(sql, getConnection());
                //command.CommandText = sql;
                Object Date = command.ExecuteScalar();
                Date = Date == null ? "2011-06-01" : Date;

                //月产品三级指数表
                sql = @"INSERT INTO " + nowProject + "_PriceRing_Index_Class_"+j+"_Month (CreatedDate,Class_Code,Class_Index)SELECT  t.CreatedDate, t.Class_Code,case t1.lastClass_Index when 0 then 0 else  t1.Class_Index * 100 / t1.lastClass_Index end AS RingPriceIndex FROM  " + nowProject + "_Price_Index_Class_"+j+"_Month t,(SELECT rank() OVER (partition BY ta.CreatedDate, ta.Class_Code ORDER BY tb.CreatedDate DESC) AS Date_Rank, tb.CreatedDate lastCreatedDate,  tb.Class_Index lastClass_Index, ta.*  FROM  " + nowProject + "_Price_Index_Class_"+j+"_Month ta,  " + nowProject + "_Price_Index_Class_"+j+"_Month tb WHERE ta.Class_Code = tb.Class_Code AND ta.CreatedDate =(select top 1 CreatedDate from " + nowProject + "_Price_Index_Class_"+j+"_Month tt where tt.CreatedDate>tb.CreatedDate and tt.Class_Code=tb.Class_Code order by tt.CreatedDate )) t1 WHERE   t .Class_Code = t1.Class_Code AND t .CreatedDate = t1.CreatedDate AND t.CreatedDate> @Date";

                command.CommandText = sql;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@Date", Date);
               int rows = command.ExecuteNonQuery();
                returnstring = returnstring + "\n月"+j+"级类别环比指数表增加了" + rows + "条数据";


                sql = @"update " + nowProject + "_PriceRing_Index_Class_"+j+"_Month set RingMove=t1.Class_Index-t1.lastClass_Index,RingMoveRate= case t1.lastClass_Index when 0 then 0 else (t1.Class_Index-t1.lastClass_Index)*100/t1.lastClass_Index end FROM " + nowProject + "_PriceRing_Index_Class_"+j+"_Month t,(SELECT rank() OVER (partition BY ta.CreatedDate, ta.Class_Code ORDER BY tb.CreatedDate DESC) AS Date_Rank, tb.CreatedDate lastCreatedDate,  tb.Class_Index lastClass_Index, ta.*  FROM  " + nowProject + "_PriceRing_Index_Class_"+j+"_Month ta,  " + nowProject + "_PriceRing_Index_Class_"+j+"_Month tb WHERE ta.Class_Code = tb.Class_Code AND ta.CreatedDate =(select top 1 CreatedDate from " + nowProject + "_PriceRing_Index_Class_"+j+"_Month tt where tt.CreatedDate>tb.CreatedDate and tt.Class_Code=tb.Class_Code order by tt.CreatedDate )) t1 WHERE  t.Class_Code = t1.Class_Code AND t.CreatedDate = t1.CreatedDate and t.CreatedDate> @Date";

                command.CommandText = sql;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@Date", Date);
                rows = command.ExecuteNonQuery();
                command.ExecuteNonQuery();
            }

            //总价格环比表
            SqlCommand commandd = new SqlCommand(sql, getConnection());
            commandd.CommandText = @"SELECT TOP(1) CreatedDate FROM " + nowProject + "_PriceRing_Index_Month ORDER BY CreatedDate DESC";
            Object ADate = commandd.ExecuteScalar();
            ADate = ADate == null ? "2010-06-01" : ADate;

            sql = @"INSERT INTO " + nowProject + "_PriceRing_Index_Month(CreatedDate,PriceIndex) SELECT t.CreatedDate, case t1.lastPriceIndex when 0 then 0 else t1.PriceIndex * 100 / t1.lastPriceIndex end AS RingPriceIndex FROM  " + nowProject + "_Price_Index_Month t,(SELECT rank() OVER (partition BY ta.CreatedDate ORDER BY tb.CreatedDate DESC) AS Date_Rank, tb.CreatedDate lastCreatedDate,  tb.PriceIndex lastPriceIndex, ta.*  FROM  " + nowProject + "_Price_Index_Month ta,  " + nowProject + "_Price_Index_Month tb WHERE  ta.CreatedDate =(select top 1 CreatedDate from " + nowProject + "_Price_Index_Month tt where tt.CreatedDate>tb.CreatedDate  order by tt.CreatedDate )) t1 WHERE t .CreatedDate = t1.CreatedDate AND t.CreatedDate > @Date";
            commandd.CommandText = sql;
            commandd.Parameters.Clear();
            commandd.Parameters.AddWithValue("@Date", ADate);
            int rowss = commandd.ExecuteNonQuery();
            returnstring = returnstring + "\n月总类别环比指数表增加了" + rowss + "条数据";

            sql = @"update " + nowProject + "_PriceRing_Index_Month set RingMove=t1.PriceIndex-t1.lastPriceIndex,RingMoveRate=case t1.lastPriceIndex when 0 then 0 else (t1.PriceIndex-t1.lastPriceIndex)*100/t1.lastPriceIndex end FROM " + nowProject + "_PriceRing_Index_Month t,(SELECT rank() OVER (partition BY ta.CreatedDate ORDER BY tb.CreatedDate DESC) AS Date_Rank, tb.CreatedDate lastCreatedDate,  tb.PriceIndex lastPriceIndex, ta.*  FROM  " + nowProject + "_PriceRing_Index_Month ta,  " + nowProject + "_PriceRing_Index_Month tb WHERE  ta.CreatedDate =(select top 1 CreatedDate from " + nowProject + "_PriceRing_Index_Month tt where tt.CreatedDate>tb.CreatedDate  order by tt.CreatedDate )) t1 WHERE t.CreatedDate = t1.CreatedDate and t.CreatedDate>@Date";

            commandd.CommandText = sql;
            commandd.Parameters.Clear();
            commandd.Parameters.AddWithValue("@Date", ADate);
            commandd.ExecuteNonQuery();

            MessageBox.Show(returnstring);
        }
		#endregion
        
       
		#region 数据操作
		/// <summary>
		/// 清空分类采集表
		/// </summary>
		public static void truncateOrgTable()
		{
            ArrayList truncateTables = new ArrayList();
            for (int i = 1; i <= nowClassNum; i++)
            {
                truncateTables.Add(nowProject + "_Class_" + i);
                truncateTables.Add(nowProject + "_Class_" + i+"_Weight");
            }
            truncateTables.Add(nowProject + "_Product_WithFullName");

			foreach (string s in truncateTables)
			{
				String sql = "truncate table " + s;
				SqlCommand command = new SqlCommand(sql, getConnection());
				command.ExecuteNonQuery();
			}
		}

		public static void truncatePickTable()
		{
            String sql = @"truncate table " + nowProject + "_Price_Pick;";

			SqlCommand command = new SqlCommand(sql, getConnection());
			command.ExecuteNonQuery();
		}

		/// <summary>
		/// 清空计算数据表
		/// </summary>
		public static void truncateComputedTable()
		{
            ArrayList truncateTables = new ArrayList();
            for (int i = 1; i <= nowClassNum; i++)
            {
                if (sort.Length == 12)
                {
                    truncateTables.Add(nowProject + "_Price_Index_Class_" + i + "_Week");
                    truncateTables.Add(nowProject + "_Price_Index_Class_" + i + "_Day");
                    truncateTables.Add(nowProject + "_Price_Index_Class_" + i + "_Month");
                    truncateTables.Add(nowProject + "_PriceRing_Index_Class_" + i + "_Week");
                    truncateTables.Add(nowProject + "_PriceRing_Index_Class_" + i + "_Day");
                    truncateTables.Add(nowProject + "_PriceRing_Index_Class_" + i + "_Month");
                }
                else if (sort.Length == 9)
                {
                    truncateTables.Add(nowProject + "_Price_Index_Class_" + i + "_Week");
                    truncateTables.Add(nowProject + "_Price_Index_Class_" + i + "_Month");
                    truncateTables.Add(nowProject + "_PriceRing_Index_Class_" + i + "_Week");
                    truncateTables.Add(nowProject + "_PriceRing_Index_Class_" + i + "_Month");
                }
                else if (sort.Length == 5)
                {
                    truncateTables.Add(nowProject + "_Price_Index_Class_" + i + "_Month");
                    truncateTables.Add(nowProject + "_PriceRing_Index_Class_" + i + "_Month");
                }
                
										
            }
            if (sort.Length == 12)
            {
                truncateTables.Add(nowProject + "_PriceRing_Index_Week");
                truncateTables.Add(nowProject + "_PriceRing_Index_Day");
                truncateTables.Add(nowProject + "_PriceRing_Index_Month");
                truncateTables.Add(nowProject + "_Price_Index_Week");
                truncateTables.Add(nowProject + "_Price_Index_Day");
                truncateTables.Add(nowProject + "_Price_Index_Month");
            }
            else if (sort.Length == 9)
            {
                truncateTables.Add(nowProject + "_PriceRing_Index_Week");
                truncateTables.Add(nowProject + "_PriceRing_Index_Month");
                truncateTables.Add(nowProject + "_Price_Index_Week");
                truncateTables.Add(nowProject + "_Price_Index_Month");
            }
            else if (sort.Length == 5)
            {
                truncateTables.Add(nowProject + "_PriceRing_Index_Month");
                truncateTables.Add(nowProject + "_Price_Index_Month");
            }

                foreach (string s in truncateTables)
                {
                    String sql = "truncate table " + s;
                    SqlCommand command = new SqlCommand(sql, getConnection());
                    command.ExecuteNonQuery();
                }
		}
        /// <summary>
        /// 清空汇总数据表
        /// </summary>
        public static void truncateEtlTable()									
        {
            if (sort.Length == 12)
            {
                string[] truncateTables = { nowProject + "_Price_Etl_Day", nowProject + "_Price_Etl_Week", nowProject + "_Price_Etl_Month", nowProject + "_Price_Ods_Base" };
                foreach (string s in truncateTables)
                {
                    String sql = "truncate table " + s;
                    SqlCommand command = new SqlCommand(sql, getConnection());
                    command.ExecuteNonQuery();
                }
            }
            else if (sort.Length == 9)
            {
                string[] truncateTables = { nowProject + "_Price_Etl_Week", nowProject + "_Price_Etl_Month", nowProject + "_Price_Ods_Base" };
                foreach (string s in truncateTables)
                {
                    String sql = "truncate table " + s;
                    SqlCommand command = new SqlCommand(sql, getConnection());
                    command.ExecuteNonQuery();
                }
            }
            else if (sort.Length == 5)
            {
                string[] truncateTables = { nowProject + "_Price_Etl_Month", nowProject + "_Price_Ods_Base" };
                foreach (string s in truncateTables)
                {
                    String sql = "truncate table " + s;
                    SqlCommand command = new SqlCommand(sql, getConnection());
                    command.ExecuteNonQuery();
                }
            }
        }

        #endregion
        #region 当前数据库导入表、汇总表、计算表状态
        public static string getPriceState()
        {
            String sql = @"select top 1 CreatedDate from " + nowProject + "_Price_Pick order by CreatedDate desc";
            SqlCommand command = new SqlCommand(sql, getConnection());
            Object createdDateimport = command.ExecuteScalar();
            createdDateimport = createdDateimport == null ? "无导入数据" : createdDateimport;
            if (!createdDateimport.ToString().Trim().Equals("无导入数据"))
            {
               // createdDateimport = createdDateimport.ToString().Trim().Substring(0, 4) + "年" + createdDateimport.ToString().Trim().Substring(5, 2) + "月第" + createdDateimport.ToString().Trim().Substring(8, 2) + "期";
                createdDateimport = createdDateimport.ToString().Trim().Split(' ')[0];           
            }
            string returnstring = "已导入日期：" + createdDateimport.ToString().Trim() + ";";
            sql = @"select top 1 CreatedDate from " + nowProject + "_Price_Etl_Week order by CreatedDate desc ";
            command.CommandText = sql;
            Object createdDateWeekTotal = command.ExecuteScalar();
            createdDateWeekTotal = createdDateWeekTotal == null ? "无周汇总数据" : createdDateWeekTotal;
            if (!createdDateWeekTotal.ToString().Trim().Equals("无周汇总数据"))
            {
               // createdDateWeekTotal = createdDateWeekTotal.ToString().Trim().Substring(0, 4) + "年" + createdDateWeekTotal.ToString().Trim().Substring(5, 2) + "月第" + createdDateWeekTotal.ToString().Trim().Substring(8, 2) + "期";
                createdDateWeekTotal = createdDateWeekTotal.ToString().Trim().Split(' ')[0];
            }
            returnstring += "已汇总周：" + createdDateWeekTotal.ToString().Trim() + ";";
            sql = @"select top 1 CreatedDate from " + nowProject + "_Price_Index_Week order by CreatedDate desc ";
            command.CommandText = sql;
            Object createdDateComputeWeek = command.ExecuteScalar();
            createdDateComputeWeek = createdDateComputeWeek == null ? "无周计算数据" : createdDateComputeWeek;
            if (!createdDateComputeWeek.ToString().Trim().Equals("无周计算数据"))
            {
               // createdDateComputeWeek = createdDateComputeWeek.ToString().Trim().Substring(0, 4) + "年" + createdDateComputeWeek.ToString().Trim().Substring(5, 2) + "月第" + createdDateComputeWeek.ToString().Trim().Substring(8, 2) + "期";
                createdDateComputeWeek = createdDateComputeWeek.ToString().Trim().Split(' ')[0];           
            }
            returnstring += "已计算周：" + createdDateComputeWeek.ToString().Trim() + ";";
            sql = @"select top 1 CreatedDate from " + nowProject + "_Price_Etl_Month order by CreatedDate desc ";
            command.CommandText = sql;
            Object createdDateMonthTotal = command.ExecuteScalar();
            createdDateMonthTotal = createdDateMonthTotal == null ? "无月汇总数据" : createdDateMonthTotal;
            if (!createdDateMonthTotal.ToString().Trim().Equals("无月汇总数据"))
            {
                //createdDateMonthTotal = createdDateMonthTotal.ToString().Trim().Substring(0, 4) + "年第" + createdDateMonthTotal.ToString().Trim().Substring(5, 2) + "期";
                createdDateMonthTotal = createdDateMonthTotal.ToString().Trim().Split(' ')[0];
            }
            returnstring += "已汇总月：" + createdDateMonthTotal.ToString().Trim() + ";";
            sql = @"select top 1 CreatedDate from " + nowProject + "_Price_Index_Month order by CreatedDate desc ";
            command.CommandText = sql;
            Object createdDateComputeMonth = command.ExecuteScalar();
            createdDateComputeMonth = createdDateComputeMonth == null ? "无月计算数据" : createdDateComputeMonth;
            if (!createdDateComputeMonth.ToString().Trim().Equals("无月计算数据"))
            {
               // createdDateComputeMonth = createdDateComputeMonth.ToString().Trim().Substring(0, 4) + "年第" + createdDateComputeMonth.ToString().Trim().Substring(5, 2) + "期";
                createdDateComputeMonth = createdDateComputeMonth.ToString().Trim().Split(' ')[0];
            }
            returnstring += "已计算月：" + createdDateComputeMonth.ToString().Trim() + ";\n";
       
            return returnstring;
        }
        
        #endregion
        public static DataSet GetDayIndexData(String Code,String Name)
        {
            String sql = "";
            string pricetype = "",pricedate="";
            if (Name == "1")
            {
                pricetype = "Price";
                pricedate = "day";   
            }
            else if (Name == "2")
            {
                pricetype = "Price";
                pricedate = "Week"; 
            }
            else if (Name == "3")
            {
                pricetype = "Price";
                pricedate = "Month";
            }
            else if (Name == "4")
            {
                pricetype = "PriceRing";
                pricedate = "Day";
            }
            else if (Name == "5")
            {
                pricetype = "PriceRing";
                pricedate = "Week";
            }
            else if (Name == "6")
            {
                pricetype = "PriceRing";
                pricedate = "Month";
            }
            int pagerow = MainForm.rowindex - 1;
            if (MainForm.createdbegindate == "")
            {
                MainForm.createdbegindate = "1900-01-01";
                MainForm.createdenddate = DateTime.Now.ToString("yyyy-MM-dd");
            }
            if (Code == "")
            {
                sql = @"select top 9 CreatedDate AS '期数',PriceIndex AS '价格指数' , RingMove AS '涨跌值', RingMoveRate AS '涨跌幅' from ( select ROW_NUMBER()OVER(ORDER BY Index_ID desc) AS RowNumber,* from " + nowProject + "_" + pricetype + "_Index_" + pricedate + " where CreatedDate>='" + MainForm.createdbegindate + "' and CreatedDate<='" + MainForm.createdenddate + "') A where RowNumber>9*" + pagerow+"order by CreatedDate desc";
            }
            else
            {

                sql = @"select top 9 CreatedDate AS '期数',Class_Index AS '价格指数' , RingMove AS '涨跌值', RingMoveRate AS '涨跌幅' from ( select ROW_NUMBER()OVER(ORDER BY Index_ID desc) AS RowNumber,* from " + nowProject + "_" + pricetype + "_Index_Class_" + Code.Length / 2 + "_" + pricedate + " where Class_Code='" + Code + "' and CreatedDate>='" + MainForm.createdbegindate + "' and CreatedDate<='" + MainForm.createdenddate + "') A where RowNumber>9*" + pagerow + "order by CreatedDate desc";
            }
            DataSet ds = new DataSet();
            SqlDataAdapter ada = new SqlDataAdapter(sql, getConnection());
            ada.Fill(ds);
            return ds;
        }
        
        public static int GetIndexDatacount(String Code,String Name)
        {
            String sqlcount = "";
            string pricetype = "", pricedate = "";
            if (Name == "1")
            {
                pricetype = "Price";
                pricedate = "day";
            }
            else if (Name == "2")
            {
                pricetype = "Price";
                pricedate = "Week";
            }
            else if (Name == "3")
            {
                pricetype = "Price";
                pricedate = "Month";
            }
            else if (Name == "4")
            {
                pricetype = "PriceRing";
                pricedate = "Day";
            }
            else if (Name == "5")
            {
                pricetype = "PriceRing";
                pricedate = "Week";
            }
            else if (Name == "6")
            {
                pricetype = "PriceRing";
                pricedate = "Month";
            }
            if (MainForm.createdbegindate == "")
                {
                    MainForm.createdbegindate = "1900-01-01";
                    MainForm.createdenddate = DateTime.Now.ToString("yyyy-MM-dd");
                }
            if (Code == "")
            {
                sqlcount = @"select COUNT (*) as count from " + nowProject + "_"+pricetype+"_Index_"+pricedate+" where CreatedDate>='" + MainForm.createdbegindate + "' and CreatedDate<='" + MainForm.createdenddate + "'";
            }
            else
            {
                sqlcount = @"select COUNT (*) as count from " + nowProject + "_"+pricetype+"_Index_Class_" + Code.Length / 2 + "_"+pricedate+" where Class_Code='" + Code + "' and CreatedDate>='" + MainForm.createdbegindate + "' and CreatedDate<='" + MainForm.createdenddate + "'";
            }
            
            SqlCommand command = new SqlCommand(sqlcount, getConnection());
            command.CommandText = sqlcount;
            command.Parameters.Clear();
            int count = (int)command.ExecuteScalar();
            return count;
        }
        



        /*------------平均价格-------begin-------------*/
        public static DataSet GetDayPriceData(String Code, String Name)
        {
            String sql = "";
            string pricetype = "", pricedate = "",code_type="";
            if (Name == "1")
            {
                pricetype = "Price";
                pricedate = "Day";
            }
            else if (Name == "2")
            {
                pricetype = "Price";
                pricedate = "Week";
            }
            else if (Name == "3")
            {
                pricetype = "Price";
                pricedate = "Month";
            }
            int pagerow = MainForm.rowindex - 1;
            if (Code.Length != nowClassNum * 2)
            {
                code_type = "_" + Code.Length / 2;
            }
            if (MainForm.createdbegindate == "")
            {
                MainForm.createdbegindate = "1900-01-01";
                MainForm.createdenddate = DateTime.Now.ToString("yyyy-MM-dd");
            }
            if (Code == "")
            {
                sql = @"select createddate AS '期数',price AS '平均价格' from (select ROW_NUMBER() over (order by createddate desc) as row,  t.createddate,cast(avg(t.price) as decimal(15,5)) as price from " + nowProject + "_" + pricetype + "_Pick t  where CreatedDate>='" + MainForm.createdbegindate + "' and CreatedDate<='" + MainForm.createdenddate + "' group by CreatedDate) tt where tt.row between (9*" + pagerow + "+1) and 9*" + (pagerow + 1);
                //sql = @"select top 9 CreatedDate AS '期数',avg(Price) AS '平均价格' from ( select ROW_NUMBER()OVER(ORDER BY Index_ID desc) AS RowNumber,* from " + nowProject + "_" + pricetype + "_Pick where CreatedDate>='" + MainForm.createdbegindate + "' and CreatedDate<='" + MainForm.createdenddate + "') A where RowNumber>9*" + pagerow + " GROUP BY CreatedDate order by CreatedDate desc";
            }
            else
            {
                sql = @"select  createddate AS '期数',price AS '平均价格' from (select ROW_NUMBER() over (order by createddate desc) as row,  t.createddate,cast(avg(t.price) as decimal(15,5)) as price from " + nowProject + "_" + pricetype + "_Pick t  where Class" + code_type + "_Code='" + Code + "' and CreatedDate>='" + MainForm.createdbegindate + "' and CreatedDate<='" + MainForm.createdenddate + "' group by CreatedDate) tt where tt.row between (9*" + pagerow + "+1) and 9*" + (pagerow + 1);
                //sql = @"select top 9 CreatedDate AS '期数',avg(Price) AS '平均价格' from ( select ROW_NUMBER()OVER(ORDER BY Index_ID desc) AS RowNumber,* from " + nowProject + "_" + pricetype + "_Pick where Class" + code_type + "_Code='" + Code + "' and CreatedDate>='" + MainForm.createdbegindate + "' and CreatedDate<='" + MainForm.createdenddate + "') A where RowNumber>9*" + pagerow + " GROUP BY CreatedDate order by CreatedDate desc";
            }
            DataSet ds = new DataSet();
            SqlDataAdapter ada = new SqlDataAdapter(sql, getConnection());
            ada.Fill(ds);
            return ds;
        }
        public static int GetPriceDatacount(String Code, String Name)
        {
            String sqlcount = "";
            string pricetype = "", pricedate = "",code_type="";
            if (Name == "1")
            {
                pricetype = "Price";
                pricedate = "day";
            }
            else if (Name == "2")
            {
                pricetype = "Price";
                pricedate = "Week";
            }
            else if (Name == "3")
            {
                pricetype = "Price";
                pricedate = "Month";
            }
            if (MainForm.createdbegindate == "")
            {
                MainForm.createdbegindate = "1900-01-01";
                MainForm.createdenddate = DateTime.Now.ToString("yyyy-MM-dd");
            }
            if (Code == "")
            {
                
                sqlcount = @"select COUNT (counts) as count from(select count(CreatedDate) as counts from " + nowProject + "_" + pricetype + "_Pick t where CreatedDate>='" + MainForm.createdbegindate + "' and CreatedDate<='" + MainForm.createdenddate + "' group by CreatedDate)tt";
                //sqlcount = @"select COUNT (*) as count from " + nowProject + "_" + pricetype + "_Pick where CreatedDate>='" + MainForm.createdbegindate + "' and CreatedDate<='" + MainForm.createdenddate + "'";
            }
            else
            {
                if (Code.Length != nowClassNum * 2)
                {
                    code_type = "_" + Code.Length / 2;
                }
                sqlcount = @"select COUNT (counts) as count from(select count(CreatedDate) as counts from " + nowProject + "_" + pricetype + "_Pick t where Class" + code_type + "_Code='" + Code + "' and CreatedDate>='" + MainForm.createdbegindate + "' and CreatedDate<='" + MainForm.createdenddate + "' group by CreatedDate)tt";
                //sqlcount = @"select COUNT (*) as count from " + nowProject + "_" + pricetype + "_Pick where Class" + code_type + "_Code='" + Code + "' and CreatedDate>='" + MainForm.createdbegindate + "' and CreatedDate<='" + MainForm.createdenddate + "'";
            }

            SqlCommand command = new SqlCommand(sqlcount, getConnection());
            command.CommandText = sqlcount;
            command.Parameters.Clear();
            int count = (int)command.ExecuteScalar();
            return count;
        }
        /*------------平均价格-------end-------------*/

        public static bool isexist(string tab)
        {  //tab为表名,如果存在返回true,否则返回false
            string sql = "select * from sysobjects where type='U' and name='" + tab + "'";
            SqlDataAdapter sqlda = new SqlDataAdapter(sql, getConnection());
            DataSet myds = new DataSet();
            sqlda.Fill(myds);
            if (myds.Tables[0].Rows.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
