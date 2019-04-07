using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using ZI.Utils;
using ZedGraph;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace ZI.Utils
{
    class ChartUtil
    {
        public enum ChartType
        {

            Price_Index_Week,
            /// <summary>
            /// 周价格指数
            /// </summary>
            Price_Index_Week_Total,
            /// <summary>
            /// 周价格总指数
            /// </summary>
            PriceRing_Index_Week,
            /// <summary>
            /// 周环比指数
            /// </summary>
            PriceRing_Index_Week_Totle,
            /// <summary>
            /// 周环比总指数
            /// </summary>
            Price_Index_Month,
            /// <summary>
            /// 月价格指数
            /// </summary>
            Price_Index_Month_Total,
            /// <summary>
            /// 月价格总指数
            /// </summary>
            PriceRing_Index_Month,
            /// <summary>
            /// 月流行指数
            /// </summary>
            PriceRing_Index_Month_Total,
            /// <summary>
            /// 月环比总指数
            /// </summary>
            Fash_Index_Month,
            /// <summary>
            ///月景气指数
            /// </summary>
            Fash_Index_Month_Total,
            /// <summary>
            ///月流行总指数
            /// </summary>
            Scale_Index_Month,
            /// <summary>
            ///月规模指数
            /// </summary>
            Scale_Index_Month_Total,
            /// <summary>
            ///月规模总指数
            /// </summary>
            Confidence_Index_Month,
            /// <summary>
            ///月信心指数
            /// </summary>
            Confidence_Index_Month_Total,
            /// <summary>
            ///月信心总指数
            /// </summary>
            Boom_Index_Month,
            /// <summary>
            ///月景气指数
            /// </summary>
            Boom_Index_Month_Total,
            /// <summary>
            ///月景气总指数
            /// </summary>
        };
        /// <summary>
        /// Price生成图片之前，设置相应的sql语句，并调用方法
        /// </summary>
        public static void genPriceChart()
        {
            if (!Directory.Exists("chart"))
                Directory.CreateDirectory("chart");
            //生成周三级定级价格指数趋势图
           string sql = @"select w.CreatedDate,w.Class_Third_Code,w.Class_Third_Index,c.Name from SJQ_Price_Index_Class_Third_Week w left join
SJQ_Class_Third c on w.Class_Third_Code=c.Class_Third_Code where w.CreatedDate>=(select top 1 * from(
select top 20 CreatedDate from SJQ_Price_Index_Class_Third_Week group by CreatedDate order by CreatedDate desc )a  order by CreatedDate asc ) order by w.Class_Third_Code,CreatedDate desc";
            genChartBySql(sql, ChartType.Price_Index_Week);
            //生成周二级价格指数趋势图
            sql = @"select w.CreatedDate,w.Class_Second_Code,w.Class_Second_Index,c.Name from SJQ_Price_Index_Class_Second_Week w left join
SJQ_Class_Second c on w.Class_Second_Code=c.Class_Second_Code where w.CreatedDate>=(select top 1 * from(
select top 20 CreatedDate from SJQ_Price_Index_Class_Second_Week group by CreatedDate order by CreatedDate desc )a  order by CreatedDate asc ) order by w.Class_Second_Code,CreatedDate desc";
            genChartBySql(sql, ChartType.Price_Index_Week);
            //生成周一级价格指数趋势图
            sql = @"select w.CreatedDate,w.Class_First_Code,w.Class_First_Index,c.Name from SJQ_Price_Index_Class_First_Week w left join
SJQ_Class_First c on w.Class_First_Code=c.ID where w.CreatedDate>=(select top 1 * from(
select top 20 CreatedDate from SJQ_Price_Index_Class_First_Week group by CreatedDate order by CreatedDate desc )a  order by CreatedDate asc ) order by w.Class_First_Code,CreatedDate desc";
            genChartBySql(sql, ChartType.Price_Index_Week);
            //生成周总价格指数趋势图
            sql = @"select w.CreatedDate,'00',w.PriceIndex from SJQ_Price_Index_Week w 
 where w.CreatedDate>=(select top 1 * from(
select top 20 CreatedDate from SJQ_Price_Index_Week group by CreatedDate order by CreatedDate desc )a  order by CreatedDate asc ) order by CreatedDate desc";
            genChartBySql(sql, ChartType.Price_Index_Week_Total);
            //-------------------------月价格指数------------------------------------------//
            //生成月三级定级价格指数趋势图
            sql = @"select w.CreatedDate,w.Class_Third_Code,w.Class_Third_Index,c.Name from SJQ_Price_Index_Class_Third_Month w left join
SJQ_Class_Third c on w.Class_Third_Code=c.Class_Third_Code where w.CreatedDate>=(select top 1 * from(
select top 20 CreatedDate from SJQ_Price_Index_Class_Third_Month group by CreatedDate order by CreatedDate desc )a  order by CreatedDate asc ) order by w.Class_Third_Code,CreatedDate desc";
            genChartBySql(sql, ChartType.Price_Index_Month);
            //生成月二级价格指数趋势图
            sql = @"select w.CreatedDate,w.Class_Second_Code,w.Class_Second_Index,c.Name from SJQ_Price_Index_Class_Second_Month w left join
SJQ_Class_Second c on w.Class_Second_Code=c.Class_Second_Code where w.CreatedDate>=(select top 1 * from(
select top 20 CreatedDate from SJQ_Price_Index_Class_Second_Month group by CreatedDate order by CreatedDate desc )a  order by CreatedDate asc ) order by w.Class_Second_Code,CreatedDate desc";
            genChartBySql(sql, ChartType.Price_Index_Month);
            //生成月一级价格指数趋势图
            sql = @"select w.CreatedDate,w.Class_First_Code,w.Class_First_Index,c.Name from SJQ_Price_Index_Class_First_Month w left join
SJQ_Class_First c on w.Class_First_Code=c.ID where w.CreatedDate>=(select top 1 * from(
select top 20 CreatedDate from SJQ_Price_Index_Class_First_Month group by CreatedDate order by CreatedDate desc )a  order by CreatedDate asc ) order by w.Class_First_Code,CreatedDate desc";
            genChartBySql(sql, ChartType.Price_Index_Month);
            //生成月总价格指数趋势图
            sql = @"select w.CreatedDate,'00',w.PriceIndex from SJQ_Price_Index_Month w 
 where w.CreatedDate>=(select top 1 * from(
select top 20 CreatedDate from SJQ_Price_Index_Month group by CreatedDate order by CreatedDate desc )a  order by CreatedDate asc ) order by CreatedDate desc";
            genChartBySql(sql, ChartType.Price_Index_Month_Total);
            //==========================环比====================================//
            //生成周三级环比价格指数趋势图
            sql = @"select w.CreatedDate,w.Class_Third_Code,w.Class_Third_Index,c.Name from SJQ_PriceRing_Index_Class_Third_Week w left join
SJQ_Class_Third c on w.Class_Third_Code=c.Class_Third_Code where w.CreatedDate>=(select top 1 * from(
select top 20 CreatedDate from SJQ_PriceRing_Index_Class_Third_Week group by CreatedDate order by CreatedDate desc )a  order by CreatedDate asc ) order by w.Class_Third_Code,CreatedDate desc";
            genChartBySql(sql, ChartType.PriceRing_Index_Week);
            //生成周二级环比价格指数趋势图
            sql = @"select w.CreatedDate,w.Class_Second_Code,w.Class_Second_Index,c.Name from SJQ_PriceRing_Index_Class_Second_Week w left join
SJQ_Class_Second c on w.Class_Second_Code=c.Class_Second_Code where w.CreatedDate>=(select top 1 * from(
select top 20 CreatedDate from SJQ_PriceRing_Index_Class_Second_Week group by CreatedDate order by CreatedDate desc )a  order by CreatedDate asc ) order by w.Class_Second_Code,CreatedDate desc";
            genChartBySql(sql, ChartType.PriceRing_Index_Week);
            //生成周一级环比价格指数趋势图
            sql = @"select w.CreatedDate,w.Class_First_Code,w.Class_First_Index,c.Name from SJQ_PriceRing_Index_Class_First_Week w left join
SJQ_Class_First c on w.Class_First_Code=c.ID where w.CreatedDate>=(select top 1 * from(
select top 20 CreatedDate from SJQ_PriceRing_Index_Class_First_Week group by CreatedDate order by CreatedDate desc )a  order by CreatedDate asc ) order by w.Class_First_Code,CreatedDate desc";
            genChartBySql(sql, ChartType.PriceRing_Index_Week);
            //生成周总环比价格指数趋势图
            sql = @"select w.CreatedDate,'00',w.PriceIndex from SJQ_PriceRing_Index_Week w 
 where w.CreatedDate>=(select top 1 * from(
select top 20 CreatedDate from SJQ_PriceRing_Index_Week group by CreatedDate order by CreatedDate desc )a  order by CreatedDate asc ) order by CreatedDate desc";
            genChartBySql(sql, ChartType.PriceRing_Index_Week_Totle);
            //------------------------月环比----------------------------------------//
            //生成月三级环比价格指数趋势图
            sql = @"select w.CreatedDate,w.Class_Third_Code,w.Class_Third_Index,c.Name from SJQ_PriceRing_Index_Class_Third_Month w left join
SJQ_Class_Third c on w.Class_Third_Code=c.Class_Third_Code where w.CreatedDate>=(select top 1 * from(
select top 20 CreatedDate from SJQ_PriceRing_Index_Class_Third_Month group by CreatedDate order by CreatedDate desc )a  order by CreatedDate asc ) order by w.Class_Third_Code,CreatedDate desc";
            genChartBySql(sql, ChartType.PriceRing_Index_Month);
            //生成月二级环比价格指数趋势图
            sql = @"select w.CreatedDate,w.Class_Second_Code,w.Class_Second_Index,c.Name from SJQ_PriceRing_Index_Class_Second_Month w left join
SJQ_Class_Second c on w.Class_Second_Code=c.Class_Second_Code where w.CreatedDate>=(select top 1 * from(
select top 20 CreatedDate from SJQ_PriceRing_Index_Class_Second_Month group by CreatedDate order by CreatedDate desc )a  order by CreatedDate asc ) order by w.Class_Second_Code,CreatedDate desc";
            genChartBySql(sql, ChartType.PriceRing_Index_Month);
            //生成月一级环比价格指数趋势图
            sql = @"select w.CreatedDate,w.Class_First_Code,w.Class_First_Index,c.Name from SJQ_PriceRing_Index_Class_First_Month w left join
SJQ_Class_First c on w.Class_First_Code=c.ID where w.CreatedDate>=(select top 1 * from(
select top 20 CreatedDate from SJQ_PriceRing_Index_Class_First_Month group by CreatedDate order by CreatedDate desc )a  order by CreatedDate asc ) order by w.Class_First_Code,CreatedDate desc";
            genChartBySql(sql, ChartType.PriceRing_Index_Month);
            //生成月总环比价格指数趋势图
            sql = @"select w.CreatedDate,'00',w.PriceIndex from SJQ_PriceRing_Index_Month w 
 where w.CreatedDate>=(select top 1 * from(
select top 20 CreatedDate from SJQ_PriceRing_Index_Month group by CreatedDate order by CreatedDate desc )a  order by CreatedDate asc ) order by CreatedDate desc";
            genChartBySql(sql, ChartType.PriceRing_Index_Month_Total);

        }
        /// <summary>
        /// Boom生成图片之前，设置相应的sql语句，并调用方法
        /// </summary>
        public static void genBoomChart()
        {
            if (!Directory.Exists("chart"))
                Directory.CreateDirectory("chart");
            //生成月三级景气指数趋势图
           string sql = @"select w.CreatedDate,w.Class_Third_Code,w.Fash_Index,c.Name from SJQ_Boom_Index_Class_Third_Month w left join
SJQ_Class_Third c on w.Class_Third_Code=c.Class_Third_Code where w.CreatedDate>=(select top 1 * from(
select top 20 CreatedDate from SJQ_Boom_Index_Class_Third_Month group by CreatedDate order by CreatedDate desc )a  order by CreatedDate asc ) order by w.Class_Third_Code,CreatedDate desc";
            genChartBySql(sql, ChartType.Fash_Index_Month);
            sql = @"select w.CreatedDate,w.Class_Third_Code,w.Scale_Index,c.Name from SJQ_Boom_Index_Class_Third_Month w left join
SJQ_Class_Third c on w.Class_Third_Code=c.Class_Third_Code where w.CreatedDate>=(select top 1 * from(
select top 20 CreatedDate from SJQ_Boom_Index_Class_Third_Month group by CreatedDate order by CreatedDate desc )a  order by CreatedDate asc ) order by w.Class_Third_Code,CreatedDate desc";
            genChartBySql(sql, ChartType.Scale_Index_Month);
            sql = @"select w.CreatedDate,w.Class_Third_Code,w.Confidence_Index,c.Name from SJQ_Boom_Index_Class_Third_Month w left join
SJQ_Class_Third c on w.Class_Third_Code=c.Class_Third_Code where w.CreatedDate>=(select top 1 * from(
select top 20 CreatedDate from SJQ_Boom_Index_Class_Third_Month group by CreatedDate order by CreatedDate desc )a  order by CreatedDate asc ) order by w.Class_Third_Code,CreatedDate desc";
            genChartBySql(sql, ChartType.Confidence_Index_Month);
            sql = @"select w.CreatedDate,w.Class_Third_Code,w.Boom_Index,c.Name from SJQ_Boom_Index_Class_Third_Month w left join
SJQ_Class_Third c on w.Class_Third_Code=c.Class_Third_Code where w.CreatedDate>=(select top 1 * from(
select top 20 CreatedDate from SJQ_Boom_Index_Class_Third_Month group by CreatedDate order by CreatedDate desc )a  order by CreatedDate asc ) order by w.Class_Third_Code,CreatedDate desc";
            genChartBySql(sql, ChartType.Boom_Index_Month);
            //生成月二级景气趋势图
            sql = @"select w.CreatedDate,w.Class_Second_Code,w.Fash_Index,c.Name from SJQ_Boom_Index_Class_Second_Month w left join
SJQ_Class_Second c on w.Class_Second_Code=c.Class_Second_Code where w.CreatedDate>=(select top 1 * from(
select top 20 CreatedDate from SJQ_Boom_Index_Class_Second_Month group by CreatedDate order by CreatedDate desc )a  order by CreatedDate asc ) order by w.Class_Second_Code,CreatedDate desc";
            genChartBySql(sql, ChartType.Fash_Index_Month);
            sql = @"select w.CreatedDate,w.Class_Second_Code,w.Scale_Index,c.Name from SJQ_Boom_Index_Class_Second_Month w left join
SJQ_Class_Second c on w.Class_Second_Code=c.Class_Second_Code where w.CreatedDate>=(select top 1 * from(
select top 20 CreatedDate from SJQ_Boom_Index_Class_Second_Month group by CreatedDate order by CreatedDate desc )a  order by CreatedDate asc ) order by w.Class_Second_Code,CreatedDate desc";
            genChartBySql(sql, ChartType.Scale_Index_Month);
            sql = @"select w.CreatedDate,w.Class_Second_Code,w.Confidence_Index,c.Name from SJQ_Boom_Index_Class_Second_Month w left join
SJQ_Class_Second c on w.Class_Second_Code=c.Class_Second_Code where w.CreatedDate>=(select top 1 * from(
select top 20 CreatedDate from SJQ_Boom_Index_Class_Second_Month group by CreatedDate order by CreatedDate desc )a  order by CreatedDate asc ) order by w.Class_Second_Code,CreatedDate desc";
            genChartBySql(sql, ChartType.Confidence_Index_Month);
            sql = @"select w.CreatedDate,w.Class_Second_Code,w.Boom_Index,c.Name from SJQ_Boom_Index_Class_Second_Month w left join
SJQ_Class_Second c on w.Class_Second_Code=c.Class_Second_Code where w.CreatedDate>=(select top 1 * from(
select top 20 CreatedDate from SJQ_Boom_Index_Class_Second_Month group by CreatedDate order by CreatedDate desc )a  order by CreatedDate asc ) order by w.Class_Second_Code,CreatedDate desc";
            genChartBySql(sql, ChartType.Boom_Index_Month);
            //生成月一级景气趋势图
            sql = @"select w.CreatedDate,w.Class_First_Code,w.Fash_Index,c.Name from SJQ_Boom_Index_Class_First_Month w left join
SJQ_Class_First c on w.Class_First_Code=c.ID where w.CreatedDate>=(select top 1 * from(
select top 20 CreatedDate from SJQ_Boom_Index_Class_First_Month group by CreatedDate order by CreatedDate desc )a  order by CreatedDate asc ) order by w.Class_First_Code,CreatedDate desc";
            genChartBySql(sql, ChartType.Fash_Index_Month);
            sql = @"select w.CreatedDate,w.Class_First_Code,w.Scale_Index,c.Name from SJQ_Boom_Index_Class_First_Month w left join
SJQ_Class_First c on w.Class_First_Code=c.ID where w.CreatedDate>=(select top 1 * from(
select top 20 CreatedDate from SJQ_Boom_Index_Class_First_Month group by CreatedDate order by CreatedDate desc )a  order by CreatedDate asc ) order by w.Class_First_Code,CreatedDate desc";
            genChartBySql(sql, ChartType.Scale_Index_Month);
            sql = @"select w.CreatedDate,w.Class_First_Code,w.Confidence_Index,c.Name from SJQ_Boom_Index_Class_First_Month w left join
SJQ_Class_First c on w.Class_First_Code=c.ID where w.CreatedDate>=(select top 1 * from(
select top 20 CreatedDate from SJQ_Boom_Index_Class_First_Month group by CreatedDate order by CreatedDate desc )a  order by CreatedDate asc ) order by w.Class_First_Code,CreatedDate desc";
            genChartBySql(sql, ChartType.Confidence_Index_Month);
            sql = @"select w.CreatedDate,w.Class_First_Code,w.Boom_Index,c.Name from SJQ_Boom_Index_Class_First_Month w left join
SJQ_Class_First c on w.Class_First_Code=c.ID where w.CreatedDate>=(select top 1 * from(
select top 20 CreatedDate from SJQ_Boom_Index_Class_First_Month group by CreatedDate order by CreatedDate desc )a  order by CreatedDate asc ) order by w.Class_First_Code,CreatedDate desc";
            genChartBySql(sql, ChartType.Boom_Index_Month);
            //生成月总景气趋势图
            sql = @"select w.CreatedDate,'00',w.Fash_Index from SJQ_Boom_Index_Month w 
 where w.CreatedDate>=(select top 1 * from(
select top 20 CreatedDate from SJQ_Boom_Index_Month group by CreatedDate order by CreatedDate desc )a  order by CreatedDate asc ) order by CreatedDate desc";
            genChartBySql(sql, ChartType.Fash_Index_Month_Total);
            sql = @"select w.CreatedDate,'00',w.Scale_Index from SJQ_Boom_Index_Month w 
 where w.CreatedDate>=(select top 1 * from(
select top 20 CreatedDate from SJQ_Boom_Index_Month group by CreatedDate order by CreatedDate desc )a  order by CreatedDate asc ) order by CreatedDate desc";
            genChartBySql(sql, ChartType.Scale_Index_Month_Total);
            sql = @"select w.CreatedDate,'00',w.Confidence_Index from SJQ_Boom_Index_Month w 
 where w.CreatedDate>=(select top 1 * from(
select top 20 CreatedDate from SJQ_Boom_Index_Month group by CreatedDate order by CreatedDate desc )a  order by CreatedDate asc ) order by CreatedDate desc";
            genChartBySql(sql, ChartType.Confidence_Index_Month_Total);
            sql = @"select w.CreatedDate,'00',w.Boom_Index from SJQ_Boom_Index_Month w 
 where w.CreatedDate>=(select top 1 * from(
select top 20 CreatedDate from SJQ_Boom_Index_Month group by CreatedDate order by CreatedDate desc )a  order by CreatedDate asc ) order by CreatedDate desc";
            genChartBySql(sql, ChartType.Boom_Index_Month_Total);
        }

       
        static ZedGraphControl ct = null;

        public static ZedGraphControl getGraphControl()
        {
            if (ct != null)
            {
                try
                {
                    ct.Dispose();
                    ct = null;
                }
                catch
                {
                    ct = null;
                }
            }

            int n = 5;
            Exception ex = null;
            while (n > 0)
            {
                try
                {
                    n--;
                    ct = new ZedGraphControl();
                    return ct;
                    break;
                }
                catch (Exception exc)
                {
                    ex = exc;

                    if (ct != null)
                        ct = null;

                    System.Threading.Thread.Sleep(100);
                }
            }

            throw (ex);

        }

        public static void genChartBySql(string sql, ChartType type)
        {
            SqlDataReader reader;
            DbUtil.ExecuteReader(out reader, sql);
            if (!reader.HasRows)
            {
                reader.Close();
                return;

            }
            PointPairList list = new PointPairList();
            //计数器，取每个产品的12个值
            int i = 0;
            //结束符，标志该产品图已生成
            bool ok = false;
            string productCode = "";
            string title = "";
            string s = "";
            getGraphControl();
            while (reader.Read())
            {
                while (ok)
                {
                    if (reader.Read())
                    {

                        if (!productCode.Equals(reader[1].ToString()))
                        {
                            ok = false;
                            productCode = "";
                            break;
                        }
                    }
                    else
                    {
                        reader.Close();
                        return;
                    }

                }
                if (productCode.Equals(""))
                {
                    i = 0;
                    getGraphControl();
                    list = new PointPairList();
                    productCode = reader[1].ToString();
                    if (type.Equals(ChartType.Price_Index_Week))
                    {
                        title = reader[3].ToString() + "[周价格指数]";
                        s = "piw";
                    }
                    else if (type.Equals(ChartType.Price_Index_Week_Total))
                    {
                        title = "周价格总指数";
                        s = "piw";
                    }            
                    else if (type.Equals(ChartType.PriceRing_Index_Week))
                    {
                        title = reader[3].ToString() + "[周环比价格指数]";
                        s = "riw";
                    }
                    else if (type.Equals(ChartType.PriceRing_Index_Week_Totle))
                    {
                        title = "周环比价格总指数";
                        s = "riw";
                    }
                    else if (type.Equals(ChartType.Price_Index_Month))
                    {
                        title = reader[3].ToString() + "[月价格指数]";
                        s = "pim";
                    }
                    else if (type.Equals(ChartType.Price_Index_Month_Total))
                    {
                        title = "月价格总指数";
                        s = "pim";
                    }
                    else if (type.Equals(ChartType.PriceRing_Index_Month))
                    {
                        title = reader[3].ToString() + "[月环比价格指数]";
                        s = "rim";
                    }
                    else if (type.Equals(ChartType.PriceRing_Index_Month_Total))
                    {
                        title = "月环比价格总指数";
                        s = "rim";
                    }
                    else if (type.Equals(ChartType.Fash_Index_Month))
                    {
                        title = reader[3].ToString() + "[月流行指数]";
                        s = "fim";
                    }
                    else if (type.Equals(ChartType.Fash_Index_Month_Total))
                    {
                        title = "月流行总指数";
                        s = "fim";
                    }
                    else if (type.Equals(ChartType.Scale_Index_Month))
                    {
                        title = reader[3].ToString() + "[月规模指数]";
                        s = "sim";
                    }
                    else if (type.Equals(ChartType.Scale_Index_Month_Total))
                    {
                        title = "月规模总指数";
                        s = "sim";
                    }
                    else if (type.Equals(ChartType.Confidence_Index_Month))
                    {
                        title = reader[3].ToString() + "[月信心指数]";
                        s = "cim";
                    }
                    else if (type.Equals(ChartType.Confidence_Index_Month_Total))
                    {
                        title = "月信心总指数";
                        s = "cim";
                    }
                    else if(type.Equals(ChartType.Boom_Index_Month))
                    {
                        title = reader[3].ToString() + "[月景气指数]";
                        s = "bim";
                    }
                    else if (type.Equals(ChartType.Boom_Index_Month_Total))
                    {
                        title = "月景气总指数";
                        s = "bim";
                    }
                   
                }
                if (!productCode.Equals(reader[1].ToString()))
                {
                    //生成本产品图片
                    genChart(ct, productCode, s, title, list);
                    ok = false;

                    //下一个产品
                    i = 0;
                    getGraphControl();
                    list = new PointPairList();
                    productCode = reader[1].ToString();
                    if (type.Equals(ChartType.Price_Index_Week))
                    {
                        title = reader[3].ToString() + "[周价格指数]";
                        s = "piw";
                    }
                    else if (type.Equals(ChartType.Price_Index_Week_Total))
                    {
                        title = "周价格总指数";
                        s = "piw";
                    }
                    else if (type.Equals(ChartType.PriceRing_Index_Week))
                    {
                        title = reader[3].ToString() + "[周环比价格指数]";
                        s = "riw";
                    }
                    else if (type.Equals(ChartType.PriceRing_Index_Week_Totle))
                    {
                        title = "周环比价格总指数";
                        s = "riw";
                    }
                    else if (type.Equals(ChartType.Price_Index_Month))
                    {
                        title = reader[3].ToString() + "[月价格指数]";
                        s = "pim";
                    }
                    else if (type.Equals(ChartType.Price_Index_Month_Total))
                    {
                        title = "月价格总指数";
                        s = "pim";
                    }
                    else if (type.Equals(ChartType.PriceRing_Index_Month))
                    {
                        title = reader[3].ToString() + "[月环比价格指数]";
                        s = "rim";
                    }
                    else if (type.Equals(ChartType.PriceRing_Index_Month_Total))
                    {
                        title = "月环比价格总指数";
                        s = "rim";
                    }
                    else if (type.Equals(ChartType.Fash_Index_Month))
                    {
                        title = reader[3].ToString() + "[月流行指数]";
                        s = "fim";
                    }
                    else if (type.Equals(ChartType.Fash_Index_Month_Total))
                    {
                        title = "月流行总指数";
                        s = "fim";
                    }
                    else if (type.Equals(ChartType.Scale_Index_Month))
                    {
                        title = reader[3].ToString() + "[月规模指数]";
                        s = "sim";
                    }
                    else if (type.Equals(ChartType.Scale_Index_Month_Total))
                    {
                        title = "月规模总指数";
                        s = "sim";
                    }
                    else if (type.Equals(ChartType.Confidence_Index_Month))
                    {
                        title = reader[3].ToString() + "[月信心指数]";
                        s = "cim";
                    }
                    else if (type.Equals(ChartType.Confidence_Index_Month_Total))
                    {
                        title = "月信心总指数";
                        s = "cim";
                    }
                    else if (type.Equals(ChartType.Boom_Index_Month))
                    {
                        title = reader[3].ToString() + "[月景气指数]";
                        s = "bim";
                    }
                    else if (type.Equals(ChartType.Boom_Index_Month_Total))
                    {
                        title = "月景气总指数";
                        s = "bim";
                    }
                    
                }

                i++;
                double x = new XDate((DateTime)reader[0]); 
                double y = double.Parse(reader[2].ToString());
                list.Add(x, y);
                // Create a text label from the Y data value,当X轴为DateAsOrdinal类型时，将忽略掉X轴本来的值
                TextObj text = new TextObj(y.ToString("f2"), i, y, CoordType.AxisXYScale, AlignH.Left, AlignV.Center);
                text.ZOrder = ZOrder.A_InFront;
                // Hide the border and the fill
                text.FontSpec.Border.IsVisible = false;
                text.FontSpec.Fill.IsVisible = false;
                text.FontSpec.Size = 20;
                ct.GraphPane.GraphObjList.Add(text);

                if (i >= 12)
                {
                    //生成本产品图片

                    genChart(ct, productCode, s, title, list);
                    ok = true;
                }
            }
            //确保最后一张图片也生成 
            if (productCode.Equals("00") && (i >= 12))
            {
                reader.Close();
            }
            else
            {
                genChart(ct, productCode, s, title, list);
                reader.Close();
            }
            ct.Dispose();
            ct = null;
        }

        private static void genPriceChartBySql(string sql)
        {
            SqlDataReader reader;
            DbUtil.ExecuteReader(out reader, sql);

            PointPairList listAvg = new PointPairList();
            PointPairList listMin = new PointPairList();
            PointPairList listMax = new PointPairList();
            //计数器，取每个产品的12个值
            int i = 0;
            //结束符，标志该产品图已生成
            bool ok = false;
            string productCode = "";
            string title = "";
            getGraphControl();

            while (reader.Read())
            {
                while (ok)
                {

                    if (reader.Read())
                    {
                        if (!productCode.Equals(reader[1].ToString()))
                        {
                            ok = false;
                            productCode = "";
                            break;
                        }
                    }
                    else
                    {
                        reader.Close();
                        return;
                    }

                }
                if (productCode.Equals(""))
                {
                    i = 0;
                    productCode = reader[1].ToString();
                    title = reader[5].ToString() + "[日价格趋势] （元/件）";
                    getGraphControl();
                    listAvg = new PointPairList();
                    listMin = new PointPairList();
                    listMax = new PointPairList();
                }
                if (!productCode.Equals(reader[1].ToString()))
                {
                    //生成本产品图片
                    genChart(ct, productCode, "", title, listMin, listAvg, listMax);
                    ok = false;
                    i = 0;
                    productCode = reader[1].ToString();
                    title = reader[5].ToString() + "[日价格趋势] （元/件）";
                    getGraphControl();
                    listAvg = new PointPairList();
                    listMin = new PointPairList();
                    listMax = new PointPairList();
                }


                i++;
                double x = new XDate((DateTime)reader[0]);
                double yAvg = double.Parse(reader[2].ToString());
                double yMin = double.Parse((reader[3].ToString() == "" ? reader[2].ToString() : reader[3].ToString()));
                double yMax = double.Parse(reader[4].ToString());
                listAvg.Add(x, yAvg);
                listMin.Add(x, yMin);
                listMax.Add(x, yMax);
                // Create a text label from the Y data value

                //均价图示
                TextObj textAvg = new TextObj(yAvg.ToString("f2"), i, yAvg, CoordType.AxisXYScale, AlignH.Left, AlignV.Center);
                textAvg.ZOrder = ZOrder.A_InFront;
                // Hide the border and the fill
                textAvg.FontSpec.Border.IsVisible = false;
                textAvg.FontSpec.Fill.IsVisible = false;
                textAvg.FontSpec.Size = 20;
                ct.GraphPane.GraphObjList.Add(textAvg);


                //最低价图示
                TextObj textMin = new TextObj(yMin.ToString("f2"), i, yMin, CoordType.AxisXYScale, AlignH.Left, AlignV.Center);
                textMin.ZOrder = ZOrder.A_InFront;
                textMin.FontSpec.Border.IsVisible = false;
                textMin.FontSpec.Fill.IsVisible = false;
                textMin.FontSpec.Size = 20;
                ct.GraphPane.GraphObjList.Add(textMin);


                //最高价图示
                TextObj textMax = new TextObj(yMax.ToString("f2"), i, yMax, CoordType.AxisXYScale, AlignH.Left, AlignV.Center);
                textMax.ZOrder = ZOrder.A_InFront;
                textMax.FontSpec.Border.IsVisible = false;
                textMax.FontSpec.Fill.IsVisible = false;
                textMax.FontSpec.Size = 20;

                ct.GraphPane.GraphObjList.Add(textMax);

                if (i >= 12)
                {
                    //生成本产品图片
                    genChart(ct, productCode, "", title, listMin, listAvg, listMax);
                    ok = true;

                }

            }
            //确保最后一张图片也生成
            genChart(ct, productCode, "", title, listMin, listAvg, listMax);
            reader.Close();

            ct.Dispose();
            ct = null;
        }

        public static void genChart(ZedGraphControl ct, string productCode, string type, string title, params PointPairList[] lists)
        {
            GraphPane pane = ct.GraphPane;
            ct.IsAntiAlias = true;
            pane.Border.IsVisible = false;
            pane.Title.Text = title;
            pane.Title.FontSpec.Family = "宋体";
            pane.Title.FontSpec.Size = 26;
            pane.Title.FontSpec.IsAntiAlias = true;
            pane.YAxis.Title.IsVisible = false;
            pane.Legend.IsVisible = true;
            pane.Legend.FontSpec.Family = "宋体";
            pane.Legend.FontSpec.Size = 20;


            //倾斜度
            pane.XAxis.Scale.FontSpec.Angle = 30;

            pane.XAxis.Type = AxisType.DateAsOrdinal;
            if (type.Length == 0 || (type.IndexOf('i') >= 0 && type.IndexOf('m') < 0))
            {
                pane.XAxis.Scale.Format = "yy-MM-dd";
            }
            else
            {
                pane.XAxis.Scale.Format = "yy-MM-dd";
            }

            pane.XAxis.Scale.IsReverse = true;
            pane.XAxis.Scale.FontSpec.Family = "宋体";
            pane.XAxis.Scale.FontSpec.IsBold = true;
            pane.XAxis.Scale.FontSpec.IsAntiAlias = true;   //抗锯齿
            //pane.XAxis.Scale.FontSpec.IsDropShadow = true;    //看起来粗一点
            pane.XAxis.Scale.FontSpec.Size = 20;
            //在对面不显示出刻度
            pane.XAxis.MajorTic.IsOpposite = false;
            pane.XAxis.Title.IsVisible = false;

            pane.YAxis.Scale.FontSpec.Family = "宋体";
            pane.YAxis.Scale.FontSpec.Size = 20;
            pane.YAxis.MajorGrid.IsVisible = true;
            pane.YAxis.MajorGrid.PenWidth = 1;
            pane.YAxis.MajorGrid.DashOff = 2;  //虚线中孔间距
            pane.YAxis.MajorGrid.DashOn = 2;    //虚线单位长度
            pane.YAxis.MajorGrid.Color = Color.FromArgb(201, 201, 201);
            pane.YAxis.MajorTic.IsOpposite = false;

            //去掉小刻度
            pane.XAxis.MinorTic.IsAllTics = false;
            pane.XAxis.Scale.MajorStep = 1;
            pane.YAxis.MinorTic.IsAllTics = false;
            try
            {
                pane.YAxis.Scale.MajorStep = Math.Floor(lists[0].Max(list => list.Y) / 20);
            }
            catch
            {
                pane.XAxis.Scale.MajorStep = 1;
            }



            int i = 0;
            Color color = Color.Red;
            string legend = "价格指数";
            Regex dateRegex = new Regex(@".*日价格趋势.*");
            Match m = dateRegex.Match(title);
            if (m.Success)
            {
                legend = "均价";
            }
            foreach (PointPairList list in lists)
            {
                if (lists.Length > 1)
                {
                    switch (i)
                    {
                        case 0: color = Color.Orange;
                            legend = "最低价";
                            break;
                        case 1: color = Color.Red;
                            legend = "均价";
                            break;
                        case 2: color = Color.Green;
                            legend = "最高价";
                            break;
                    }
                }
                LineItem curve = pane.AddCurve(legend, list, color, SymbolType.Diamond);
                curve.Line.Width = 1.5F;
                curve.Symbol.Fill = new Fill(Color.White);
                curve.Symbol.Size = 5;
                curve.Line.IsAntiAlias = true;

                if (i != 1 && lists.Length > 1)
                {
                    curve.Line.Style = System.Drawing.Drawing2D.DashStyle.Dot;
                }
                i++;
            }

            // Leave some extra space on top for the labels to fit within the chart rect
            //pane.YAxis.Scale.MaxGrace = 0.2;

            //pane.XAxis.Scale.BaseTic = 50;

            //pane.Chart.Fill = new Fill(Color.White, Color.SteelBlue, 45.0F);
            pane.Chart.Border.IsVisible = false;

            //pane.Chart.Border.Color = Color.Red;

            //重新计算坐标刻度
            ct.AxisChange();
            Bitmap bmp = pane.GetImage(270, 200, 1000);

            if (productCode.Equals("00"))
                productCode = "";

            bmp.Save(("chart/" + type + "s" + productCode + ".png").ToUpper());

            pane.GetImage(330, 200, 1000).Save(("chart/" + type + "m" + productCode + ".png").ToUpper());
            pane.GetImage(675, 200, 1000).Save(("chart/" + type + "l" + productCode + ".png").ToUpper());
        }

    }
}
