using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Collections;
using System.IO;
using System.Xml;
using System.Configuration;

namespace ZI.Utils
{
    class XMLUtil
    {
        /// <summary>
        /// 导出分类指数信息(含分类名字)
        /// </summary>
        private static void genClassAndNameIndexXML(string sql, string file)
        {
            XmlDocument productDoc = new XmlDocument();
            XmlDeclaration xml = (XmlDeclaration)productDoc.CreateNode(XmlNodeType.XmlDeclaration, "", "");
            xml.Encoding = "utf-8";
            productDoc.AppendChild(xml);
            XmlElement strucElem = productDoc.CreateElement("STRUCTURE");
            XmlElement dataElem = null;
            string code = "";
            SqlDataReader reader;
            DbUtil.ExecuteReader(out reader, sql);
            while (reader.Read())
            {
                if (dataElem != null)
                {
                    strucElem.AppendChild(dataElem);
                }
                dataElem = productDoc.CreateElement("dataElem");
                code = reader[4].ToString();
                dataElem.SetAttribute("code", code);
                string name = "";
                name = reader[0].ToString().Trim();
                dataElem.SetAttribute("Name", name.Trim());
                dataElem.SetAttribute("index", string.IsNullOrEmpty(reader[1].ToString()) ? "0" : Math.Round(decimal.Parse(reader[1].ToString()), 2).ToString());
                dataElem.SetAttribute("index_RingMove", string.IsNullOrEmpty(reader[2].ToString()) ? "0" : Math.Round(decimal.Parse(reader[2].ToString()), 2).ToString());
                dataElem.SetAttribute("index_RingMoveRate", string.IsNullOrEmpty(reader[3].ToString()) ? "0" : Math.Round(decimal.Parse(reader[3].ToString()), 2).ToString());
            }
            if (dataElem != null)
            {
                strucElem.AppendChild(dataElem);
            }
            productDoc.AppendChild(strucElem);
            productDoc.Save(file);
            reader.Close();
        }
        /// <summary>
        /// 导出分类指数信息
        /// </summary>
        private static void genClassIndexXML(string sql,string file)
        {
            XmlDocument productDoc = new XmlDocument();
            XmlDeclaration xml = (XmlDeclaration)productDoc.CreateNode(XmlNodeType.XmlDeclaration, "", "");
            xml.Encoding = "utf-8";
            productDoc.AppendChild(xml);
            XmlElement strucElem = productDoc.CreateElement("STRUCTURE");
            XmlElement dataElem = null;
            string code = "";
            SqlDataReader reader;
            DbUtil.ExecuteReader(out reader, sql);
            while (reader.Read())
            {
                    if (dataElem != null)
                    {
                        strucElem.AppendChild(dataElem);
                    }
                    dataElem = productDoc.CreateElement("dataElem");
                    code = reader[4].ToString();
                    dataElem.SetAttribute("code", code);
                    string phase = "";
                    phase = reader[0].ToString().Trim().Split(' ')[0];
                    dataElem.SetAttribute("Phase", phase.Trim());
                    dataElem.SetAttribute("index", string.IsNullOrEmpty(reader[1].ToString()) ? "0" : Math.Round(decimal.Parse(reader[1].ToString()), 2).ToString());
                    dataElem.SetAttribute("index_RingMove", string.IsNullOrEmpty(reader[2].ToString()) ? "0" : Math.Round(decimal.Parse(reader[2].ToString()), 2).ToString());
                    dataElem.SetAttribute("index_RingMoveRate", string.IsNullOrEmpty(reader[3].ToString()) ? "0" : Math.Round(decimal.Parse(reader[3].ToString()), 2).ToString());
            }
            if (dataElem != null)
            {
                strucElem.AppendChild(dataElem);
            }
            productDoc.AppendChild(strucElem);
            productDoc.Save(file);
            reader.Close();
        }
        /// <summary>
        /// 导出总指数信息
        /// </summary>
        private static void genALLIndexXML(string sql, string file)
        {
            XmlDocument productDoc = new XmlDocument();
            XmlDeclaration xml = (XmlDeclaration)productDoc.CreateNode(XmlNodeType.XmlDeclaration, "", "");
            xml.Encoding = "utf-8";
            productDoc.AppendChild(xml);
            XmlElement strucElem = productDoc.CreateElement("STRUCTURE");
            XmlElement dataElem = null;
            SqlDataReader reader;
            DbUtil.ExecuteReader(out reader, sql);
            while (reader.Read())
            {      
                    if (dataElem != null)
                    {
                        strucElem.AppendChild(dataElem);
                    }
                    dataElem = productDoc.CreateElement("dataElem");
                    string phase = "";
                    phase = reader[0].ToString().Trim().Split(' ')[0];
                    dataElem.SetAttribute("Phase", phase.Trim());
                    dataElem.SetAttribute("index", string.IsNullOrEmpty(reader[1].ToString()) ? "0" : Math.Round(decimal.Parse(reader[1].ToString()), 2).ToString());
                    dataElem.SetAttribute("index_RingMove", string.IsNullOrEmpty(reader[2].ToString()) ? "0" : Math.Round(decimal.Parse(reader[2].ToString()), 2).ToString());
                    dataElem.SetAttribute("index_RingMoveRate", string.IsNullOrEmpty(reader[3].ToString()) ? "0" : Math.Round(decimal.Parse(reader[3].ToString()), 2).ToString());
                
            }
            if (dataElem != null)
            {
                strucElem.AppendChild(dataElem);
            }
            productDoc.AppendChild(strucElem);
            productDoc.Save(file);
            reader.Close();
        }   
        /// <summary>
        /// 导出分类信息
        /// </summary>
        public static void genClassesXML(){
            if (!Directory.Exists("XML"))
                Directory.CreateDirectory("XML");
            //生成分类列表           

            string sql = @"select p.Class_First_Code,p.Class_First_Name,p.Class_Second_Code,p.Class_Second_Name,p.Class_Third_Code,p.Class_Third_Name from SJQ_Product_WithFullName p";

            XmlDocument productDoc = new XmlDocument();
            XmlDeclaration xml = (XmlDeclaration)productDoc.CreateNode(XmlNodeType.XmlDeclaration, "", "");
            xml.Encoding = "utf-8";
            productDoc.AppendChild(xml);
            XmlElement strucElem = productDoc.CreateElement("STRUCTURE");
            XmlElement Class_FirstElem = null;
            XmlElement Class_SecondElem = null;
            XmlElement Class_ThirdElem = null;

            string Class_First_Code = "", Class_Second_Code = "", Class_Third_Code = "";
            SqlDataReader reader;
            DbUtil.ExecuteReader(out reader, sql);
            while (reader.Read())
            {
               
                //新的Class_ThirdElem
                if (!reader[4].Equals(Class_Third_Code))
                {
                    //把上个子类先添加进大类，再新建一个子类
                    if (Class_ThirdElem != null)
                    {
                        Class_SecondElem.AppendChild(Class_ThirdElem);
                    }

                    Class_ThirdElem = productDoc.CreateElement("Class_Third");
                    Class_ThirdElem.SetAttribute("name", reader[5].ToString());
                    Class_Third_Code = reader[4].ToString();
                    Class_ThirdElem.SetAttribute("Class_Third_Code", Class_Third_Code);
                }
                if (!reader[2].Equals(Class_Second_Code))
                {
                    //把上个子类先添加进大类，再新建一个子类
                    if (Class_SecondElem != null)
                    {
                        Class_FirstElem.AppendChild(Class_SecondElem);
                    }

                    Class_SecondElem = productDoc.CreateElement("Class_Second");
                    Class_SecondElem.SetAttribute("name", reader[3].ToString());
                    Class_Second_Code = reader[2].ToString();
                    Class_SecondElem.SetAttribute("Class_Second_Code", Class_Second_Code);
                }

                //新的一个大类开始
                if (!reader[0].Equals(Class_First_Code))
                {
                    if (Class_FirstElem != null)
                    {
                        strucElem.AppendChild(Class_FirstElem);
                    }
                    Class_FirstElem = productDoc.CreateElement("Class_First");
                    Class_FirstElem.SetAttribute("name", reader[1].ToString());
                    Class_First_Code = reader[0].ToString();
                    Class_FirstElem.SetAttribute("Class_First_Code", Class_First_Code);
                }
            }
            Class_SecondElem.AppendChild(Class_ThirdElem);
            Class_FirstElem.AppendChild(Class_SecondElem);
            strucElem.AppendChild(Class_FirstElem);

            productDoc.AppendChild(strucElem);
            productDoc.Save("XML/ProductListing.xml");
            reader.Close();
        }
        /// <summary>
        /// 导出价格指数信息
        /// </summary>
        public static void genPriceXML()
        {
            if (!Directory.Exists("PriceXML"))
                Directory.CreateDirectory("PriceXML");
            string sql = "";
            //生成三级定基分类指数列表  
            sql = @"select w.CreatedDate,w.Class_Third_Index,w.RingMove,w.RingMoveRate,w.Class_Third_Code from SJQ_Price_Index_Class_Third_Week w order by w.Class_Third_Code,w.CreatedDate desc";
            genClassIndexXML(sql, "PriceXML/Price_Class_Third_Week_Index.xml");
            //生成二级定基分类指数列表  
            sql = @"select w.CreatedDate,w.Class_Second_Index,w.RingMove,w.RingMoveRate,w.Class_Second_Code from SJQ_Price_Index_Class_Second_Week w order by w.Class_Second_Code,w.CreatedDate desc";
            genClassIndexXML(sql, "PriceXML/Price_Class_Second_Week_Index.xml");
            //生成一级定基分类指数列表  
            sql = @"select w.CreatedDate,w.Class_First_Index,w.RingMove,w.RingMoveRate,w.Class_First_Code from SJQ_Price_Index_Class_First_Week w order by w.Class_First_Code,w.CreatedDate desc";
            genClassIndexXML(sql, "PriceXML/Price_Class_First_Week_Index.xml");
            //生成总定基分类指数列表  
            sql = @"select w.CreatedDate,w.PriceIndex,w.RingMove,w.RingMoveRate from SJQ_Price_Index_Week w order by w.CreatedDate desc";
            genALLIndexXML(sql, "PriceXML/Price_All_Week_Index.xml");
            //------------------月指数--------------------------//
            //生成三级定基分类指数列表  
            sql = @"select w.CreatedDate,w.Class_Third_Index,w.RingMove,w.RingMoveRate,w.Class_Third_Code from SJQ_Price_Index_Class_Third_Month w order by w.Class_Third_Code,w.CreatedDate desc";
            genClassIndexXML(sql, "PriceXML/Price_Class_Third_Month_Index.xml");
            //生成二级定基分类指数列表  
            sql = @"select w.CreatedDate,w.Class_Second_Index,w.RingMove,w.RingMoveRate,w.Class_Second_Code from SJQ_Price_Index_Class_Second_Month w order by w.Class_Second_Code,w.CreatedDate desc";
            genClassIndexXML(sql, "PriceXML/Price_Class_Second_Month_Index.xml");
            //生成一级定基分类指数列表  
            sql = @"select w.CreatedDate,w.Class_First_Index,w.RingMove,w.RingMoveRate,w.Class_First_Code from SJQ_Price_Index_Class_First_Month w order by w.Class_First_Code,w.CreatedDate desc";
            genClassIndexXML(sql, "PriceXML/Price_Class_First_Month_Index.xml");
            //生成总定基分类指数列表  
            sql = @"select w.CreatedDate,w.PriceIndex,w.RingMove,w.RingMoveRate from SJQ_Price_Index_Month w order by w.CreatedDate desc";
            genALLIndexXML(sql, "PriceXML/Price_All_Month_Index.xml");
        //////--------------环比-------------------------------////
            //生成三级环比分类指数列表  
            sql = @"select w.CreatedDate,w.Class_Third_Index,w.RingMove,w.RingMoveRate,w.Class_Third_Code from SJQ_PriceRing_Index_Class_Third_Week w order by w.Class_Third_Code,w.CreatedDate desc";
            genClassIndexXML(sql, "PriceXML/PriceRing_Class_Third_Week_Index.xml");
            //生成二级环比分类指数列表  
            sql = @"select w.CreatedDate,w.Class_Second_Index,w.RingMove,w.RingMoveRate,w.Class_Second_Code from SJQ_PriceRing_Index_Class_Second_Week w order by w.Class_Second_Code,w.CreatedDate desc";
            genClassIndexXML(sql, "PriceXML/PriceRing_Class_Second_Week_Index.xml");
            //生成一级环比分类指数列表  
            sql = @"select w.CreatedDate,w.Class_First_Index,w.RingMove,w.RingMoveRate,w.Class_First_Code from SJQ_PriceRing_Index_Class_First_Week w order by w.Class_First_Code,w.CreatedDate desc";
            genClassIndexXML(sql, "PriceXML/PriceRing_Class_First_Week_Index.xml");
            //生成总环比分类指数列表  
            sql = @"select w.CreatedDate,w.PriceIndex,w.RingMove,w.RingMoveRate from SJQ_PriceRing_Index_Week w order by w.CreatedDate desc";
            genALLIndexXML(sql, "PriceXML/PriceRing_All_Week_Index.xml");
            //-------------月环比-----------------//
            //生成三级环比分类指数列表  
            sql = @"select w.CreatedDate,w.Class_Third_Index,w.RingMove,w.RingMoveRate,w.Class_Third_Code from SJQ_PriceRing_Index_Class_Third_Month w order by w.Class_Third_Code,w.CreatedDate desc";
            genClassIndexXML(sql, "PriceXML/PriceRing_Class_Third_Month_Index.xml");
            //生成二级环比分类指数列表  
            sql = @"select w.CreatedDate,w.Class_Second_Index,w.RingMove,w.RingMoveRate,w.Class_Second_Code from SJQ_PriceRing_Index_Class_Second_Month w order by w.Class_Second_Code,w.CreatedDate desc";
            genClassIndexXML(sql, "PriceXML/PriceRing_Class_Second_Month_Index.xml");
            //生成一级环比分类指数列表  
            sql = @"select w.CreatedDate,w.Class_First_Index,w.RingMove,w.RingMoveRate,w.Class_First_Code from SJQ_PriceRing_Index_Class_First_Month w order by w.Class_First_Code,w.CreatedDate desc";
            genClassIndexXML(sql, "PriceXML/PriceRing_Class_First_Month_Index.xml");
            //生成总环比分类指数列表  
            sql = @"select w.CreatedDate,w.PriceIndex,w.RingMove,w.RingMoveRate from SJQ_PriceRing_Index_Month w order by w.CreatedDate desc";
            genALLIndexXML(sql, "PriceXML/PriceRing_All_Month_Index.xml");
            //-----三级涨跌--------//
            sql = @"select Top 10 CreatedDate,Class_Third_Index,RingMove,RingMoveRate,Class_Third_Code from SJQ_Price_Index_Class_Third_Week  where CreatedDate=(select top 1 CreatedDate from SJQ_Price_Index_Class_Third_Week order by CreatedDate desc) and RingMove is not null order by RingMove desc";
            genClassIndexXML(sql, "PriceXML/PriceRankUp_Class_Third_Week_Index.xml");
            sql = @"select Top 10 CreatedDate,Class_Third_Index,RingMove,RingMoveRate,Class_Third_Code from SJQ_Price_Index_Class_Third_Week  where CreatedDate=(select top 1 CreatedDate from SJQ_Price_Index_Class_Third_Week order by CreatedDate desc) and RingMove is not null order by RingMove asc";
            genClassIndexXML(sql, "PriceXML/PriceRankDown_Class_Third_Week_Index.xml");
            genPriceForPicXML();
        }
        /// <summary>
        /// 导出图片对应价格信息
        /// </summary>
        public static void genPriceForPicXML()
        {
            if (!Directory.Exists("PriceXML"))
                Directory.CreateDirectory("PriceXML");
            string sql = "";
            //生成三级定基分类指数列表  
            sql = @"select * from
(select w.CreatedDate,'0' as code,w.PriceIndex,w.RingMove,w.RingMoveRate from SJQ_Price_Index_Week w where CreatedDate=(select top 1 CreatedDate from SJQ_Price_Index_Week order by CreatedDate desc)) w1 left join
 (select '0' as code,w.PriceIndex,w.RingMove,w.RingMoveRate from SJQ_PriceRing_Index_Week w where CreatedDate=(select top 1 CreatedDate from SJQ_PriceRing_Index_Week order by CreatedDate desc))w2 on w1.code=w2.code left join
 (select '0' as code,w.PriceIndex,w.RingMove,w.RingMoveRate from SJQ_Price_Index_Month w where CreatedDate=(select top 1 CreatedDate from SJQ_Price_Index_Month order by CreatedDate desc)) m1 on w2.code=m1.code left join
 (select '0' as code,w.PriceIndex,w.RingMove,w.RingMoveRate from SJQ_PriceRing_Index_Month w where CreatedDate=(select top 1 CreatedDate from SJQ_PriceRing_Index_Month order by CreatedDate desc)) m2 on m1.code=m2.code";
            XmlDocument productDoc = new XmlDocument();
            XmlDeclaration xml = (XmlDeclaration)productDoc.CreateNode(XmlNodeType.XmlDeclaration, "", "");
            xml.Encoding = "utf-8";
            productDoc.AppendChild(xml);
            XmlElement strucElem = productDoc.CreateElement("STRUCTURE");
            XmlElement priceElem = productDoc.CreateElement("PriceIndex");
            SqlDataReader reader;
            DbUtil.ExecuteReader(out reader, sql);
            while (reader.Read())
            {
                string phase = "";
                phase = reader[0].ToString().Trim().Split(' ')[0];
                priceElem.SetAttribute("Date", phase);
                XmlElement itemsElem = productDoc.CreateElement("Items");
                itemsElem.SetAttribute("Code","0");
                XmlElement itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name","本周价格指数");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[2].ToString()) ? "0" : Math.Round(decimal.Parse(reader[2].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Week");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本周价格涨跌值");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[3].ToString()) ? "0" : Math.Round(decimal.Parse(reader[3].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Week");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本周价格涨跌幅");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[4].ToString()) ? "0" : Math.Round(decimal.Parse(reader[4].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Week");
                itemsElem.AppendChild(itemElem);

                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本周环比价格指数");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[6].ToString()) ? "0" : Math.Round(decimal.Parse(reader[6].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "WeekRing");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本周环比价格涨跌值");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[7].ToString()) ? "0" : Math.Round(decimal.Parse(reader[7].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "WeekRing");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本周环比价格涨跌幅");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[8].ToString()) ? "0" : Math.Round(decimal.Parse(reader[8].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "WeekRing");
                itemsElem.AppendChild(itemElem);

                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月价格指数");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[10].ToString()) ? "0" : Math.Round(decimal.Parse(reader[10].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月价格涨跌值");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[11].ToString()) ? "0" : Math.Round(decimal.Parse(reader[11].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月价格涨跌幅");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[12].ToString()) ? "0" : Math.Round(decimal.Parse(reader[12].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);

                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月环比价格指数");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[14].ToString()) ? "0" : Math.Round(decimal.Parse(reader[14].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "MonthRing");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月环比价格涨跌值");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[15].ToString()) ? "0" : Math.Round(decimal.Parse(reader[15].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "MonthRing");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月环比价格涨跌幅");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[16].ToString()) ? "0" : Math.Round(decimal.Parse(reader[16].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "MonthRing");
                itemsElem.AppendChild(itemElem);
                priceElem.AppendChild(itemsElem);
            }
            reader.Close();
            sql = @"select * from 
            (select Class_First_Code,w.Class_First_Index,w.RingMove,w.RingMoveRate from SJQ_Price_Index_Class_First_Week w where CreatedDate=(select top 1 CreatedDate from  SJQ_Price_Index_Class_First_Week order by CreatedDate desc)) w1 
            left join (select Class_First_Code,w.Class_First_Index,w.RingMove,w.RingMoveRate from SJQ_PriceRing_Index_Class_First_Week w where CreatedDate=(select top 1 CreatedDate from  SJQ_Price_Index_Class_First_Week order by CreatedDate desc)) w2 on w1.Class_First_Code=w2.Class_First_Code 
            left join (select Class_First_Code,w.Class_First_Index,w.RingMove,w.RingMoveRate from SJQ_Price_Index_Class_First_Month w where CreatedDate=(select top 1 CreatedDate from  SJQ_Price_Index_Class_First_Month order by CreatedDate desc)) m1 on w2.Class_First_Code=m1.Class_First_Code 
            left join(select Class_First_Code,w.Class_First_Index,w.RingMove,w.RingMoveRate from SJQ_PriceRing_Index_Class_First_Month w where CreatedDate=(select top 1 CreatedDate from  SJQ_PriceRing_Index_Class_First_Month order by CreatedDate desc))m2 on m1.Class_First_Code=m2.Class_First_Code";
            DbUtil.ExecuteReader(out reader, sql);
            while (reader.Read())
            {
                XmlElement itemsElem = productDoc.CreateElement("Items");
                itemsElem.SetAttribute("Code", reader[0].ToString());
                XmlElement itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本周价格指数");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[1].ToString()) ? "0" : Math.Round(decimal.Parse(reader[1].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Week");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本周价格涨跌值");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[2].ToString()) ? "0" : Math.Round(decimal.Parse(reader[2].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Week");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本周价格涨跌幅");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[3].ToString()) ? "0" : Math.Round(decimal.Parse(reader[3].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Week");
                itemsElem.AppendChild(itemElem);

                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本周环比价格指数");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[5].ToString()) ? "0" : Math.Round(decimal.Parse(reader[5].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "WeekRing");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本周环比价格涨跌值");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[6].ToString()) ? "0" : Math.Round(decimal.Parse(reader[6].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "WeekRing");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本周环比价格涨跌幅");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[7].ToString()) ? "0" : Math.Round(decimal.Parse(reader[7].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "WeekRing");
                itemsElem.AppendChild(itemElem);

                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月价格指数");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[9].ToString()) ? "0" : Math.Round(decimal.Parse(reader[9].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月价格涨跌值");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[10].ToString()) ? "0" : Math.Round(decimal.Parse(reader[10].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月价格涨跌幅");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[11].ToString()) ? "0" : Math.Round(decimal.Parse(reader[11].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);

                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月环比价格指数");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[13].ToString()) ? "0" : Math.Round(decimal.Parse(reader[13].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "MonthRing");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月环比价格涨跌值");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[14].ToString()) ? "0" : Math.Round(decimal.Parse(reader[14].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "MonthRing");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月环比价格涨跌幅");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[15].ToString()) ? "0" : Math.Round(decimal.Parse(reader[15].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "MonthRing");
                itemsElem.AppendChild(itemElem);
                priceElem.AppendChild(itemsElem);
            }
            reader.Close();
            sql = @"select * from 
            (select Class_Second_Code,w.Class_Second_Index,w.RingMove,w.RingMoveRate from SJQ_Price_Index_Class_Second_Week w where CreatedDate=(select top 1 CreatedDate from  SJQ_Price_Index_Class_Second_Week order by CreatedDate desc)) w1 
            left join (select Class_Second_Code,w.Class_Second_Index,w.RingMove,w.RingMoveRate from SJQ_PriceRing_Index_Class_Second_Week w where CreatedDate=(select top 1 CreatedDate from  SJQ_Price_Index_Class_Second_Week order by CreatedDate desc)) w2 on w1.Class_Second_Code=w2.Class_Second_Code 
            left join (select Class_Second_Code,w.Class_Second_Index,w.RingMove,w.RingMoveRate from SJQ_Price_Index_Class_Second_Month w where CreatedDate=(select top 1 CreatedDate from  SJQ_Price_Index_Class_Second_Month order by CreatedDate desc)) m1 on w2.Class_Second_Code=m1.Class_Second_Code 
            left join(select Class_Second_Code,w.Class_Second_Index,w.RingMove,w.RingMoveRate from SJQ_PriceRing_Index_Class_Second_Month w where CreatedDate=(select top 1 CreatedDate from  SJQ_PriceRing_Index_Class_Second_Month order by CreatedDate desc))m2 on m1.Class_Second_Code=m2.Class_Second_Code";
            DbUtil.ExecuteReader(out reader, sql);
            while (reader.Read())
            {
                XmlElement itemsElem = productDoc.CreateElement("Items");
                itemsElem.SetAttribute("Code", reader[0].ToString());
                XmlElement itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本周价格指数");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[1].ToString()) ? "0" : Math.Round(decimal.Parse(reader[1].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Week");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本周价格涨跌值");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[2].ToString()) ? "0" : Math.Round(decimal.Parse(reader[2].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Week");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本周价格涨跌幅");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[3].ToString()) ? "0" : Math.Round(decimal.Parse(reader[3].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Week");
                itemsElem.AppendChild(itemElem);

                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本周环比价格指数");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[5].ToString()) ? "0" : Math.Round(decimal.Parse(reader[5].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "WeekRing");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本周环比价格涨跌值");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[6].ToString()) ? "0" : Math.Round(decimal.Parse(reader[6].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "WeekRing");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本周环比价格涨跌幅");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[7].ToString()) ? "0" : Math.Round(decimal.Parse(reader[7].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "WeekRing");
                itemsElem.AppendChild(itemElem);

                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月价格指数");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[9].ToString()) ? "0" : Math.Round(decimal.Parse(reader[9].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月价格涨跌值");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[10].ToString()) ? "0" : Math.Round(decimal.Parse(reader[10].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月价格涨跌幅");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[11].ToString()) ? "0" : Math.Round(decimal.Parse(reader[11].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);

                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月环比价格指数");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[13].ToString()) ? "0" : Math.Round(decimal.Parse(reader[13].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "MonthRing");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月环比价格涨跌值");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[14].ToString()) ? "0" : Math.Round(decimal.Parse(reader[14].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "MonthRing");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月环比价格涨跌幅");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[15].ToString()) ? "0" : Math.Round(decimal.Parse(reader[15].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "MonthRing");
                itemsElem.AppendChild(itemElem);
                priceElem.AppendChild(itemsElem);
            }
            reader.Close();
            sql = @"select * from 
            (select Class_Third_Code,w.Class_Third_Index,w.RingMove,w.RingMoveRate from SJQ_Price_Index_Class_Third_Week w where CreatedDate=(select top 1 CreatedDate from  SJQ_Price_Index_Class_Third_Week order by CreatedDate desc)) w1 
            left join (select Class_Third_Code,w.Class_Third_Index,w.RingMove,w.RingMoveRate from SJQ_PriceRing_Index_Class_Third_Week w where CreatedDate=(select top 1 CreatedDate from  SJQ_Price_Index_Class_Third_Week order by CreatedDate desc)) w2 on w1.Class_Third_Code=w2.Class_Third_Code 
            left join (select Class_Third_Code,w.Class_Third_Index,w.RingMove,w.RingMoveRate from SJQ_Price_Index_Class_Third_Month w where CreatedDate=(select top 1 CreatedDate from  SJQ_Price_Index_Class_Third_Month order by CreatedDate desc)) m1 on w2.Class_Third_Code=m1.Class_Third_Code 
            left join(select Class_Third_Code,w.Class_Third_Index,w.RingMove,w.RingMoveRate from SJQ_PriceRing_Index_Class_Third_Month w where CreatedDate=(select top 1 CreatedDate from  SJQ_PriceRing_Index_Class_Third_Month order by CreatedDate desc))m2 on m1.Class_Third_Code=m2.Class_Third_Code";
            DbUtil.ExecuteReader(out reader, sql);
            while (reader.Read())
            {
                XmlElement itemsElem = productDoc.CreateElement("Items");
                itemsElem.SetAttribute("Code", reader[0].ToString());
                XmlElement itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本周价格指数");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[1].ToString()) ? "0" : Math.Round(decimal.Parse(reader[1].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Week");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本周价格涨跌值");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[2].ToString()) ? "0" : Math.Round(decimal.Parse(reader[2].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Week");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本周价格涨跌幅");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[3].ToString()) ? "0" : Math.Round(decimal.Parse(reader[3].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Week");
                itemsElem.AppendChild(itemElem);

                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本周环比价格指数");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[5].ToString()) ? "0" : Math.Round(decimal.Parse(reader[5].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "WeekRing");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本周环比价格涨跌值");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[6].ToString()) ? "0" : Math.Round(decimal.Parse(reader[6].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "WeekRing");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本周环比价格涨跌幅");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[7].ToString()) ? "0" : Math.Round(decimal.Parse(reader[7].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "WeekRing");
                itemsElem.AppendChild(itemElem);

                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月价格指数");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[9].ToString()) ? "0" : Math.Round(decimal.Parse(reader[9].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月价格涨跌值");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[10].ToString()) ? "0" : Math.Round(decimal.Parse(reader[10].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月价格涨跌幅");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[11].ToString()) ? "0" : Math.Round(decimal.Parse(reader[11].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);

                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月环比价格指数");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[13].ToString()) ? "0" : Math.Round(decimal.Parse(reader[13].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "MonthRing");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月环比价格涨跌值");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[14].ToString()) ? "0" : Math.Round(decimal.Parse(reader[14].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "MonthRing");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月环比价格涨跌幅");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[15].ToString()) ? "0" : Math.Round(decimal.Parse(reader[15].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "MonthRing");
                itemsElem.AppendChild(itemElem);
                priceElem.AppendChild(itemsElem);
            }
            reader.Close();
            if (priceElem != null)
            {
                strucElem.AppendChild(priceElem);
            }
            productDoc.AppendChild(strucElem);
            productDoc.Save("PriceXML/PriceIndexItemListing.xml");
        }
        /// <summary>
        /// 导出景气信息
        /// </summary>
        public static void genBoomXML()
        {
            if (!Directory.Exists("BoomXML"))
                Directory.CreateDirectory("BoomXML");
            string sql = "";
            //生成三级时尚指数列表  
            sql = @"select w.CreatedDate,w.Fash_Index,w.Fash_RingMove,w.Fash_RingMoveRate,w.Class_Third_Code from SJQ_Boom_Index_Class_Third_Month w order by w.Class_Third_Code,w.CreatedDate desc";
            genClassIndexXML(sql, "BoomXML/Fash_Class_Third_Index.xml");
            //生成三级规模指数列表  
            sql = @"select w.CreatedDate,w.Scale_Index,w.Scale_RingMove,w.Scale_RingMoveRate,w.Class_Third_Code from SJQ_Boom_Index_Class_Third_Month w order by w.Class_Third_Code,w.CreatedDate desc";
            genClassIndexXML(sql, "BoomXML/Scale_Class_Third_Index.xml");
            //生成三级信心指数列表  
            sql = @"select w.CreatedDate,w.Confidence_Index,w.Confidence_RingMove,w.Confidence_RingMoveRate,w.Class_Third_Code from SJQ_Boom_Index_Class_Third_Month w order by w.Class_Third_Code,w.CreatedDate desc";
            genClassIndexXML(sql, "BoomXML/Confidence_Class_Third_Index.xml");
            //生成三级景气指数列表  
            sql = @"select w.CreatedDate,w.Boom_Index,w.Boom_RingMove,w.Boom_RingMoveRate,w.Class_Third_Code from SJQ_Boom_Index_Class_Third_Month w order by w.Class_Third_Code,w.CreatedDate desc";
            genClassIndexXML(sql, "BoomXML/Boom_Class_Third_Index.xml");
            //生成二级时尚指数指数列表  
            sql = @"select w.CreatedDate,w.Fash_Index,w.Fash_RingMove,w.Fash_RingMoveRate,w.Class_Second_Code from SJQ_Boom_Index_Class_Second_Month w order by w.Class_Second_Code,w.CreatedDate desc";
            genClassIndexXML(sql, "BoomXML/Fash_Class_Second_Index.xml");
            //生成二级规模指数指数列表  
            sql = @"select w.CreatedDate,w.Scale_Index,w.Scale_RingMove,w.Scale_RingMoveRate,w.Class_Second_Code from SJQ_Boom_Index_Class_Second_Month w order by w.Class_Second_Code,w.CreatedDate desc";
            genClassIndexXML(sql, "BoomXML/Scale_Class_Second_Index.xml");
            //生成二级信心指数指数列表  
            sql = @"select w.CreatedDate,w.Confidence_Index,w.Confidence_RingMove,w.Confidence_RingMoveRate,w.Class_Second_Code from SJQ_Boom_Index_Class_Second_Month w order by w.Class_Second_Code,w.CreatedDate desc";
            genClassIndexXML(sql, "BoomXML/Confidence_Class_Second_Index.xml");
            //生成二级景气指数指数列表  
            sql = @"select w.CreatedDate,w.Boom_Index,w.Boom_RingMove,w.Boom_RingMoveRate,w.Class_Second_Code from SJQ_Boom_Index_Class_Second_Month w order by w.Class_Second_Code,w.CreatedDate desc";
            genClassIndexXML(sql, "BoomXML/Boom_Class_Second_Index.xml");
            //生成一级时尚指数指数列表  
            sql = @"select w.CreatedDate,w.Fash_Index,w.Fash_RingMove,w.Fash_RingMoveRate,w.Class_First_Code from SJQ_Boom_Index_Class_First_Month w order by w.Class_First_Code,w.CreatedDate desc";
            genClassIndexXML(sql, "BoomXML/Fash_Class_First_Index.xml");
            //生成一级规模指数指数列表  
            sql = @"select w.CreatedDate,w.Scale_Index,w.Scale_RingMove,w.Scale_RingMoveRate,w.Class_First_Code from SJQ_Boom_Index_Class_First_Month w order by w.Class_First_Code,w.CreatedDate desc";
            genClassIndexXML(sql, "BoomXML/Scale_Class_First_Index.xml");
            //生成一级信心指数指数列表  
            sql = @"select w.CreatedDate,w.Confidence_Index,w.Confidence_RingMove,w.Confidence_RingMoveRate,w.Class_First_Code from SJQ_Boom_Index_Class_First_Month w order by w.Class_First_Code,w.CreatedDate desc";
            genClassIndexXML(sql, "BoomXML/Confidence_Class_First_Index.xml");
            //生成一级景气指数指数列表  
            sql = @"select w.CreatedDate,w.Boom_Index,w.Boom_RingMove,w.Boom_RingMoveRate,w.Class_First_Code from SJQ_Boom_Index_Class_First_Month w order by w.Class_First_Code,w.CreatedDate desc";
            genClassIndexXML(sql, "BoomXML/Boom_Class_First_Index.xml");
            //生成总时尚指数指数列表  
            sql = @"select w.CreatedDate,w.Fash_Index,w.Fash_RingMove,w.Fash_RingMoveRate from SJQ_Boom_Index_Month w order by w.CreatedDate desc";
            genALLIndexXML(sql, "BoomXML/Fash_All_Index.xml");
            //生成总规模指数指数列表  
            sql = @"select w.CreatedDate,w.Scale_Index,w.Scale_RingMove,w.Scale_RingMoveRate from SJQ_Boom_Index_Month w order by w.CreatedDate desc";
            genALLIndexXML(sql, "BoomXML/Scale_All_Index.xml");
            //生成总信心指数指数列表  
            sql = @"select w.CreatedDate,w.Confidence_Index,w.Confidence_RingMove,w.Confidence_RingMoveRate from SJQ_Boom_Index_Month w order by w.CreatedDate desc";
            genALLIndexXML(sql, "BoomXML/Confidence_All_Index.xml");
            //生成总景气指数指数列表  
            sql = @"select w.CreatedDate,w.Boom_Index,w.Boom_RingMove,w.Boom_RingMoveRate from SJQ_Boom_Index_Month w order by w.CreatedDate desc";
            genALLIndexXML(sql, "BoomXML/Boom_All_Index.xml");
            //-----景气涨跌前十------//
            sql = @"SELECT   TOP (10) t.DisplayName, m.Boom_Index, m.Boom_RingMove, m.Boom_RingMoveRate, m.Class_Third_Code
FROM      SJQ_Boom_Index_Class_Third_Month AS m INNER JOIN
                SJQ_Class_Third AS t ON m.Class_Third_Code = t.Class_Third_Code
WHERE   (m.CreatedDate =
                    (SELECT   TOP (1) CreatedDate
                     FROM      SJQ_Boom_Index_Class_Third_Month
                     ORDER BY CreatedDate DESC)) AND (m.Boom_RingMove IS NOT NULL) AND (m.Class_First_Code = 1)
ORDER BY m.Boom_RingMove DESC";
            genClassAndNameIndexXML(sql, "BoomXML/BoomRankUp_Class_Third_Index.xml");
            sql = @"SELECT   TOP (10) t.DisplayName, m.Boom_Index, m.Boom_RingMove, m.Boom_RingMoveRate, m.Class_Third_Code
FROM      SJQ_Boom_Index_Class_Third_Month AS m INNER JOIN
                SJQ_Class_Third AS t ON m.Class_Third_Code = t.Class_Third_Code
WHERE   (m.CreatedDate =
                    (SELECT   TOP (1) CreatedDate
                     FROM      SJQ_Boom_Index_Class_Third_Month
                     ORDER BY CreatedDate DESC)) AND (m.Boom_RingMove IS NOT NULL) AND (m.Class_First_Code = 1)
ORDER BY m.Boom_RingMove ASC";
            genClassAndNameIndexXML(sql, "BoomXML/BoomRankDown_Class_Third_Index.xml");
            //-----时尚排名前四------//
           /* sql = @"select Top 4 CreatedDate,Fash_Index,Fash_RingMove,Fash_RingMoveRate,Class_Third_Code from SJQ_Boom_Index_Class_Third_Month  where Class_First_Code=1 and CreatedDate=(select top 1 CreatedDate from SJQ_Boom_Index_Class_Third_Month order by CreatedDate desc) and Fash_Index is not null order by Fash_Index desc";
            genClassIndexXML(sql, "BoomXML/FashHot01_Class_Third_Index.xml");
            sql = @"select Top 4 CreatedDate,Fash_Index,Fash_RingMove,Fash_RingMoveRate,Class_Third_Code from SJQ_Boom_Index_Class_Third_Month  where  Class_First_Code=2 and CreatedDate=(select top 1 CreatedDate from SJQ_Boom_Index_Class_Third_Month order by CreatedDate desc) and Fash_Index is not null order by Fash_Index desc";
            genClassIndexXML(sql, "BoomXML/FashHot02_Class_Third_Index.xml");
            sql = @"select Top 4 CreatedDate,Fash_Index,Fash_RingMove,Fash_RingMoveRate,Class_Third_Code from SJQ_Boom_Index_Class_Third_Month  where  Class_First_Code=3 and CreatedDate=(select top 1 CreatedDate from SJQ_Boom_Index_Class_Third_Month order by CreatedDate desc) and Fash_Index is not null order by Fash_Index desc";
            genClassIndexXML(sql, "BoomXML/FashHot03_Class_Third_Index.xml");
            sql = @"select Top 4 CreatedDate,Fash_Index,Fash_RingMove,Fash_RingMoveRate,Class_Third_Code from SJQ_Boom_Index_Class_Third_Month  where  Class_First_Code=4 and CreatedDate=(select top 1 CreatedDate from SJQ_Boom_Index_Class_Third_Month order by CreatedDate desc) and Fash_Index is not null order by Fash_Index desc";
            genClassIndexXML(sql, "BoomXML/FashHot04_Class_Third_Index.xml");*/
            genClassFashIndexXML("BoomXML/FashHot_Class_Third_Index.xml");
            genBoomForPicXML();
        }
        /// <summary>
        /// 导出图片对应景气信息
        /// </summary>
        public static void genBoomForPicXML() {
            if (!Directory.Exists("BoomXML"))
                Directory.CreateDirectory("BoomXML");
            string sql = "";
            //生成三级定基分类指数列表  
            sql = @"select * from
(select CreatedDate,'0' as code,Fash_Index,Fash_RingMove,Fash_RingMoveRate,Scale_Index,Scale_RingMove,Scale_RingMoveRate,Confidence_Index,Confidence_RingMove,Confidence_RingMoveRate,Boom_Index,Boom_RingMove,Boom_RingMoveRate from SJQ_Boom_Index_Month where CreatedDate=(select top 1 CreatedDate from SJQ_Boom_Index_Month order by CreatedDate desc)) m1 left join
(select '0' as code,Fash_Index,Fash_RingMove,Fash_RingMoveRate,Scale_Index,Scale_RingMove,Scale_RingMoveRate,Confidence_Index,Confidence_RingMove,Confidence_RingMoveRate,Boom_Index,Boom_RingMove,Boom_RingMoveRate from SJQ_BoomRing_Index_Month where CreatedDate=(select top 1 CreatedDate from SJQ_BoomRing_Index_Month order by CreatedDate desc)) m2 on m1.code=m2.code";
            XmlDocument productDoc = new XmlDocument();
            XmlDeclaration xml = (XmlDeclaration)productDoc.CreateNode(XmlNodeType.XmlDeclaration, "", "");
            xml.Encoding = "utf-8";
            productDoc.AppendChild(xml);
            XmlElement strucElem = productDoc.CreateElement("STRUCTURE");
            XmlElement priceElem = productDoc.CreateElement("PriceIndex");
            SqlDataReader reader;
            DbUtil.ExecuteReader(out reader, sql);
            while (reader.Read())
            {
                string phase = "";
                phase = reader[0].ToString().Trim().Split(' ')[0];
                priceElem.SetAttribute("Date", phase);
                XmlElement itemsElem = productDoc.CreateElement("Items");
                itemsElem.SetAttribute("Code", "0");
                XmlElement itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月时尚指数");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[2].ToString()) ? "0" : Math.Round(decimal.Parse(reader[2].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月时尚指数涨跌值");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[3].ToString()) ? "0" : Math.Round(decimal.Parse(reader[3].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月时尚指数涨跌幅");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[4].ToString()) ? "0" : Math.Round(decimal.Parse(reader[4].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);

                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月规模指数");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[5].ToString()) ? "0" : Math.Round(decimal.Parse(reader[5].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月规模涨跌值");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[6].ToString()) ? "0" : Math.Round(decimal.Parse(reader[6].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月规模涨跌幅");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[7].ToString()) ? "0" : Math.Round(decimal.Parse(reader[7].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);

                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月信心指数");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[8].ToString()) ? "0" : Math.Round(decimal.Parse(reader[8].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月信心涨跌值");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[9].ToString()) ? "0" : Math.Round(decimal.Parse(reader[9].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月信心涨跌幅");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[10].ToString()) ? "0" : Math.Round(decimal.Parse(reader[10].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);

                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月景气指数");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[11].ToString()) ? "0" : Math.Round(decimal.Parse(reader[11].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月景气涨跌值");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[12].ToString()) ? "0" : Math.Round(decimal.Parse(reader[12].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月景气涨跌幅");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[13].ToString()) ? "0" : Math.Round(decimal.Parse(reader[13].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);

                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月环比时尚指数");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[15].ToString()) ? "0" : Math.Round(decimal.Parse(reader[15].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月环比时尚指数涨跌值");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[16].ToString()) ? "0" : Math.Round(decimal.Parse(reader[16].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月环比时尚指数涨跌幅");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[17].ToString()) ? "0" : Math.Round(decimal.Parse(reader[17].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);

                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月环比规模指数");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[18].ToString()) ? "0" : Math.Round(decimal.Parse(reader[18].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月环比规模涨跌值");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[19].ToString()) ? "0" : Math.Round(decimal.Parse(reader[19].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月环比规模涨跌幅");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[20].ToString()) ? "0" : Math.Round(decimal.Parse(reader[20].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);

                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月环比信心指数");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[21].ToString()) ? "0" : Math.Round(decimal.Parse(reader[21].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月环比信心涨跌值");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[22].ToString()) ? "0" : Math.Round(decimal.Parse(reader[22].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月环比信心涨跌幅");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[23].ToString()) ? "0" : Math.Round(decimal.Parse(reader[23].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);

                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月环比景气指数");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[24].ToString()) ? "0" : Math.Round(decimal.Parse(reader[24].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月环比景气涨跌值");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[25].ToString()) ? "0" : Math.Round(decimal.Parse(reader[25].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月环比景气涨跌幅");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[26].ToString()) ? "0" : Math.Round(decimal.Parse(reader[26].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);

                priceElem.AppendChild(itemsElem);
            }
            reader.Close();

            sql = @"select * from 
(select Class_First_Code,Fash_Index,Fash_RingMove,Fash_RingMoveRate,Scale_Index,Scale_RingMove,Scale_RingMoveRate,Confidence_Index,Confidence_RingMove,Confidence_RingMoveRate,Boom_Index,Boom_RingMove,Boom_RingMoveRate from SJQ_Boom_Index_Class_First_Month where CreatedDate=(select top 1 CreatedDate from SJQ_Boom_Index_Class_First_Month order by CreatedDate desc)) m1 left join
(select Class_First_Code,Fash_Index,Fash_RingMove,Fash_RingMoveRate,Scale_Index,Scale_RingMove,Scale_RingMoveRate,Confidence_Index,Confidence_RingMove,Confidence_RingMoveRate,Boom_Index,Boom_RingMove,Boom_RingMoveRate from SJQ_BoomRing_Index_Class_First_Month where CreatedDate=(select top 1 CreatedDate from SJQ_BoomRing_Index_Class_First_Month order by CreatedDate desc)) m2 on m1.Class_First_Code=m2.Class_First_Code";
            DbUtil.ExecuteReader(out reader, sql);
            while (reader.Read())
            {
                XmlElement itemsElem = productDoc.CreateElement("Items");
                itemsElem.SetAttribute("Code", reader[0].ToString());
                XmlElement itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月时尚指数");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[1].ToString()) ? "0" : Math.Round(decimal.Parse(reader[1].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月时尚指数涨跌值");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[2].ToString()) ? "0" : Math.Round(decimal.Parse(reader[2].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月时尚指数涨跌幅");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[3].ToString()) ? "0" : Math.Round(decimal.Parse(reader[3].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);

                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月规模指数");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[4].ToString()) ? "0" : Math.Round(decimal.Parse(reader[4].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月规模涨跌值");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[5].ToString()) ? "0" : Math.Round(decimal.Parse(reader[5].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月规模涨跌幅");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[6].ToString()) ? "0" : Math.Round(decimal.Parse(reader[6].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);

                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月信心指数");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[7].ToString()) ? "0" : Math.Round(decimal.Parse(reader[7].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月信心涨跌值");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[8].ToString()) ? "0" : Math.Round(decimal.Parse(reader[8].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月信心涨跌幅");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[9].ToString()) ? "0" : Math.Round(decimal.Parse(reader[9].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);

                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月景气指数");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[10].ToString()) ? "0" : Math.Round(decimal.Parse(reader[10].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月景气涨跌值");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[11].ToString()) ? "0" : Math.Round(decimal.Parse(reader[11].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月景气涨跌幅");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[12].ToString()) ? "0" : Math.Round(decimal.Parse(reader[12].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);

                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月环比时尚指数");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[14].ToString()) ? "0" : Math.Round(decimal.Parse(reader[14].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月环比时尚指数涨跌值");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[15].ToString()) ? "0" : Math.Round(decimal.Parse(reader[15].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月环比时尚指数涨跌幅");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[16].ToString()) ? "0" : Math.Round(decimal.Parse(reader[16].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);

                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月环比规模指数");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[17].ToString()) ? "0" : Math.Round(decimal.Parse(reader[17].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月环比规模涨跌值");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[18].ToString()) ? "0" : Math.Round(decimal.Parse(reader[18].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月环比规模涨跌幅");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[19].ToString()) ? "0" : Math.Round(decimal.Parse(reader[19].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);

                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月环比信心指数");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[20].ToString()) ? "0" : Math.Round(decimal.Parse(reader[20].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月环比信心涨跌值");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[21].ToString()) ? "0" : Math.Round(decimal.Parse(reader[21].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月环比信心涨跌幅");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[22].ToString()) ? "0" : Math.Round(decimal.Parse(reader[22].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);

                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月环比景气指数");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[23].ToString()) ? "0" : Math.Round(decimal.Parse(reader[23].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月环比景气涨跌值");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[24].ToString()) ? "0" : Math.Round(decimal.Parse(reader[24].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月环比景气涨跌幅");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[25].ToString()) ? "0" : Math.Round(decimal.Parse(reader[25].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);

                priceElem.AppendChild(itemsElem);
            }
            reader.Close();

            sql = @"select * from 
(select Class_Second_Code,Fash_Index,Fash_RingMove,Fash_RingMoveRate,Scale_Index,Scale_RingMove,Scale_RingMoveRate,Confidence_Index,Confidence_RingMove,Confidence_RingMoveRate,Boom_Index,Boom_RingMove,Boom_RingMoveRate from SJQ_Boom_Index_Class_Second_Month where CreatedDate=(select top 1 CreatedDate from SJQ_Boom_Index_Class_Second_Month order by CreatedDate desc)) m1 left join
(select Class_Second_Code,Fash_Index,Fash_RingMove,Fash_RingMoveRate,Scale_Index,Scale_RingMove,Scale_RingMoveRate,Confidence_Index,Confidence_RingMove,Confidence_RingMoveRate,Boom_Index,Boom_RingMove,Boom_RingMoveRate from SJQ_BoomRing_Index_Class_Second_Month where CreatedDate=(select top 1 CreatedDate from SJQ_BoomRing_Index_Class_Second_Month order by CreatedDate desc)) m2 on m1.Class_Second_Code=m2.Class_Second_Code";
            DbUtil.ExecuteReader(out reader, sql);
            while (reader.Read())
            {
                XmlElement itemsElem = productDoc.CreateElement("Items");
                itemsElem.SetAttribute("Code", reader[0].ToString());
                XmlElement itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月时尚指数");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[1].ToString()) ? "0" : Math.Round(decimal.Parse(reader[1].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月时尚指数涨跌值");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[2].ToString()) ? "0" : Math.Round(decimal.Parse(reader[2].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月时尚指数涨跌幅");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[3].ToString()) ? "0" : Math.Round(decimal.Parse(reader[3].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);

                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月规模指数");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[4].ToString()) ? "0" : Math.Round(decimal.Parse(reader[4].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月规模涨跌值");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[5].ToString()) ? "0" : Math.Round(decimal.Parse(reader[5].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月规模涨跌幅");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[6].ToString()) ? "0" : Math.Round(decimal.Parse(reader[6].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);

                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月信心指数");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[7].ToString()) ? "0" : Math.Round(decimal.Parse(reader[7].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月信心涨跌值");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[8].ToString()) ? "0" : Math.Round(decimal.Parse(reader[8].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月信心涨跌幅");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[9].ToString()) ? "0" : Math.Round(decimal.Parse(reader[9].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);

                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月景气指数");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[10].ToString()) ? "0" : Math.Round(decimal.Parse(reader[10].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月景气涨跌值");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[11].ToString()) ? "0" : Math.Round(decimal.Parse(reader[11].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月景气涨跌幅");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[12].ToString()) ? "0" : Math.Round(decimal.Parse(reader[12].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);

                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月环比时尚指数");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[14].ToString()) ? "0" : Math.Round(decimal.Parse(reader[14].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月环比时尚指数涨跌值");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[15].ToString()) ? "0" : Math.Round(decimal.Parse(reader[15].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月环比时尚指数涨跌幅");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[16].ToString()) ? "0" : Math.Round(decimal.Parse(reader[16].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);

                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月环比规模指数");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[17].ToString()) ? "0" : Math.Round(decimal.Parse(reader[17].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月环比规模涨跌值");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[18].ToString()) ? "0" : Math.Round(decimal.Parse(reader[18].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月环比规模涨跌幅");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[19].ToString()) ? "0" : Math.Round(decimal.Parse(reader[19].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);

                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月环比信心指数");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[20].ToString()) ? "0" : Math.Round(decimal.Parse(reader[20].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月环比信心涨跌值");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[21].ToString()) ? "0" : Math.Round(decimal.Parse(reader[21].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月环比信心涨跌幅");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[22].ToString()) ? "0" : Math.Round(decimal.Parse(reader[22].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);

                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月环比景气指数");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[23].ToString()) ? "0" : Math.Round(decimal.Parse(reader[23].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月环比景气涨跌值");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[24].ToString()) ? "0" : Math.Round(decimal.Parse(reader[24].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月环比景气涨跌幅");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[25].ToString()) ? "0" : Math.Round(decimal.Parse(reader[25].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);

                priceElem.AppendChild(itemsElem);
            }
            reader.Close();

            sql = @"select * from 
(select Class_Third_Code,Fash_Index,Fash_RingMove,Fash_RingMoveRate,Scale_Index,Scale_RingMove,Scale_RingMoveRate,Confidence_Index,Confidence_RingMove,Confidence_RingMoveRate,Boom_Index,Boom_RingMove,Boom_RingMoveRate from SJQ_Boom_Index_Class_Third_Month where CreatedDate=(select top 1 CreatedDate from SJQ_Boom_Index_Class_Third_Month order by CreatedDate desc)) m1 left join
(select Class_Third_Code,Fash_Index,Fash_RingMove,Fash_RingMoveRate,Scale_Index,Scale_RingMove,Scale_RingMoveRate,Confidence_Index,Confidence_RingMove,Confidence_RingMoveRate,Boom_Index,Boom_RingMove,Boom_RingMoveRate from SJQ_BoomRing_Index_Class_Third_Month where CreatedDate=(select top 1 CreatedDate from SJQ_BoomRing_Index_Class_Third_Month order by CreatedDate desc)) m2 on m1.Class_Third_Code=m2.Class_Third_Code";
            DbUtil.ExecuteReader(out reader, sql);
            while (reader.Read())
            {
                XmlElement itemsElem = productDoc.CreateElement("Items");
                itemsElem.SetAttribute("Code", reader[0].ToString());
                XmlElement itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月时尚指数");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[1].ToString()) ? "0" : Math.Round(decimal.Parse(reader[1].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月时尚指数涨跌值");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[2].ToString()) ? "0" : Math.Round(decimal.Parse(reader[2].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月时尚指数涨跌幅");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[3].ToString()) ? "0" : Math.Round(decimal.Parse(reader[3].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);

                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月规模指数");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[4].ToString()) ? "0" : Math.Round(decimal.Parse(reader[4].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月规模涨跌值");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[5].ToString()) ? "0" : Math.Round(decimal.Parse(reader[5].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月规模涨跌幅");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[6].ToString()) ? "0" : Math.Round(decimal.Parse(reader[6].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);

                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月信心指数");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[7].ToString()) ? "0" : Math.Round(decimal.Parse(reader[7].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月信心涨跌值");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[8].ToString()) ? "0" : Math.Round(decimal.Parse(reader[8].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月信心涨跌幅");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[9].ToString()) ? "0" : Math.Round(decimal.Parse(reader[9].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);

                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月景气指数");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[10].ToString()) ? "0" : Math.Round(decimal.Parse(reader[10].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月景气涨跌值");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[11].ToString()) ? "0" : Math.Round(decimal.Parse(reader[11].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月景气涨跌幅");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[12].ToString()) ? "0" : Math.Round(decimal.Parse(reader[12].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);

                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月环比时尚指数");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[14].ToString()) ? "0" : Math.Round(decimal.Parse(reader[14].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月环比时尚指数涨跌值");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[15].ToString()) ? "0" : Math.Round(decimal.Parse(reader[15].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月环比时尚指数涨跌幅");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[16].ToString()) ? "0" : Math.Round(decimal.Parse(reader[16].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);

                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月环比规模指数");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[17].ToString()) ? "0" : Math.Round(decimal.Parse(reader[17].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月环比规模涨跌值");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[18].ToString()) ? "0" : Math.Round(decimal.Parse(reader[18].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月环比规模涨跌幅");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[19].ToString()) ? "0" : Math.Round(decimal.Parse(reader[19].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);

                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月环比信心指数");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[20].ToString()) ? "0" : Math.Round(decimal.Parse(reader[20].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月环比信心涨跌值");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[21].ToString()) ? "0" : Math.Round(decimal.Parse(reader[21].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月环比信心涨跌幅");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[22].ToString()) ? "0" : Math.Round(decimal.Parse(reader[22].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);

                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月环比景气指数");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[23].ToString()) ? "0" : Math.Round(decimal.Parse(reader[23].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月环比景气涨跌值");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[24].ToString()) ? "0" : Math.Round(decimal.Parse(reader[24].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);
                itemElem = productDoc.CreateElement("Item");
                itemElem.SetAttribute("Name", "本月环比景气涨跌幅");
                itemElem.SetAttribute("Value", string.IsNullOrEmpty(reader[25].ToString()) ? "0" : Math.Round(decimal.Parse(reader[25].ToString()), 2).ToString());
                itemElem.SetAttribute("Period", "Month");
                itemsElem.AppendChild(itemElem);

                priceElem.AppendChild(itemsElem);
            }
            reader.Close();
            if (priceElem != null)
            {
                strucElem.AppendChild(priceElem);
            }
            productDoc.AppendChild(strucElem);
            productDoc.Save("BoomXML/BoomIndexItemListing.xml");
        
        }
        /// <summary>
        /// 导出分类时尚信息
        /// </summary>
        private static void genClassFashIndexXML(string file)
        {
            XmlDocument productDoc = new XmlDocument();
            XmlDeclaration xml = (XmlDeclaration)productDoc.CreateNode(XmlNodeType.XmlDeclaration, "", "");
            xml.Encoding = "utf-8";
            productDoc.AppendChild(xml);
            XmlElement strucElem = productDoc.CreateElement("STRUCTURE");
            XmlElement dataElem = null;
            string code = "";
            SqlDataReader reader;
            string sql = @"select Top 8 i.CreatedDate,i.Fash_Index,i.Fash_RingMove,i.Fash_RingMoveRate,i.Class_Third_Code,n.Class_First_Name,n.Class_Second_Name,n.Class_Third_Name from SJQ_Boom_Index_Class_Third_Month i left join SJQ_Product_WithFullName n on i.Class_Third_Code=n.Class_Third_Code where i.Class_First_Code=1 and i.CreatedDate=(select top 1 CreatedDate from SJQ_Boom_Index_Class_Third_Month order by CreatedDate desc) and i.Fash_Index is not null order by i.Fash_Index desc";   
            DbUtil.ExecuteReader(out reader, sql);
            while (reader.Read())
            {
                if (dataElem != null)
                {
                    strucElem.AppendChild(dataElem);
                }
                dataElem = productDoc.CreateElement("dataElem");
                code = reader[4].ToString().Trim();
                dataElem.SetAttribute("code", code);
                string phase = "";
                phase = reader[0].ToString().Trim().Split(' ')[0];
                dataElem.SetAttribute("Phase", phase.Trim());
                dataElem.SetAttribute("index", string.IsNullOrEmpty(reader[1].ToString()) ? "0" : Math.Round(decimal.Parse(reader[1].ToString()), 2).ToString());
                dataElem.SetAttribute("index_RingMove", string.IsNullOrEmpty(reader[2].ToString()) ? "0" : Math.Round(decimal.Parse(reader[2].ToString()), 2).ToString());
                dataElem.SetAttribute("index_RingMoveRate", string.IsNullOrEmpty(reader[3].ToString()) ? "0" : Math.Round(decimal.Parse(reader[3].ToString()), 2).ToString());
                dataElem.SetAttribute("class_first_name", reader[5].ToString().Trim());
                dataElem.SetAttribute("class_second_name", reader[6].ToString().Trim());
                dataElem.SetAttribute("class_third_name", reader[7].ToString().Trim());
            }
            reader.Close();
            if (dataElem != null)
            {
                strucElem.AppendChild(dataElem);
            }
          /*
            dataElem = null;
             sql = @"select Top 2 i.CreatedDate,i.Fash_Index,i.Fash_RingMove,i.Fash_RingMoveRate,i.Class_Third_Code,n.Class_First_Name,n.Class_Second_Name,n.Class_Third_Name from SJQ_Boom_Index_Class_Third_Month i left join SJQ_Product_WithFullName n on i.Class_Third_Code=n.Class_Third_Code where i.Class_First_Code=2 and i.CreatedDate=(select top 1 CreatedDate from SJQ_Boom_Index_Class_Third_Month order by CreatedDate desc) and i.Fash_Index is not null order by i.Fash_Index desc";
            DbUtil.ExecuteReader(out reader, sql);
            while (reader.Read())
            {
                if (dataElem != null)
                {
                    strucElem.AppendChild(dataElem);
                }
                dataElem = productDoc.CreateElement("dataElem");
                code = reader[4].ToString().Trim();
                dataElem.SetAttribute("code", code);
                string phase = "";
                phase = reader[0].ToString().Trim().Split(' ')[0];
                dataElem.SetAttribute("Phase", phase.Trim());
                dataElem.SetAttribute("index", string.IsNullOrEmpty(reader[1].ToString()) ? "0" : Math.Round(decimal.Parse(reader[1].ToString()), 2).ToString());
                dataElem.SetAttribute("index_RingMove", string.IsNullOrEmpty(reader[2].ToString()) ? "0" : Math.Round(decimal.Parse(reader[2].ToString()), 2).ToString());
                dataElem.SetAttribute("index_RingMoveRate", string.IsNullOrEmpty(reader[3].ToString()) ? "0" : Math.Round(decimal.Parse(reader[3].ToString()), 2).ToString());
                dataElem.SetAttribute("class_first_name", reader[5].ToString().Trim());
                dataElem.SetAttribute("class_second_name", reader[6].ToString().Trim());
                dataElem.SetAttribute("class_third_name", reader[7].ToString().Trim());
            }
            reader.Close();
            if (dataElem != null)
            {
                strucElem.AppendChild(dataElem);
            }
            dataElem = null;
            sql = @"select Top 2 i.CreatedDate,i.Fash_Index,i.Fash_RingMove,i.Fash_RingMoveRate,i.Class_Third_Code,n.Class_First_Name,n.Class_Second_Name,n.Class_Third_Name from SJQ_Boom_Index_Class_Third_Month i left join SJQ_Product_WithFullName n on i.Class_Third_Code=n.Class_Third_Code where i.Class_First_Code=3 and i.CreatedDate=(select top 1 CreatedDate from SJQ_Boom_Index_Class_Third_Month order by CreatedDate desc) and i.Fash_Index is not null order by i.Fash_Index desc";
            DbUtil.ExecuteReader(out reader, sql);
            while (reader.Read())
            {
                if (dataElem != null)
                {
                    strucElem.AppendChild(dataElem);
                }
                dataElem = productDoc.CreateElement("dataElem");
                code = reader[4].ToString().Trim();
                dataElem.SetAttribute("code", code);
                string phase = "";
                phase = reader[0].ToString().Trim().Split(' ')[0];
                dataElem.SetAttribute("Phase", phase.Trim());
                dataElem.SetAttribute("index", string.IsNullOrEmpty(reader[1].ToString()) ? "0" : Math.Round(decimal.Parse(reader[1].ToString()), 2).ToString());
                dataElem.SetAttribute("index_RingMove", string.IsNullOrEmpty(reader[2].ToString()) ? "0" : Math.Round(decimal.Parse(reader[2].ToString()), 2).ToString());
                dataElem.SetAttribute("index_RingMoveRate", string.IsNullOrEmpty(reader[3].ToString()) ? "0" : Math.Round(decimal.Parse(reader[3].ToString()), 2).ToString());
                dataElem.SetAttribute("class_first_name", reader[5].ToString().Trim());
                dataElem.SetAttribute("class_second_name", reader[6].ToString().Trim());
                dataElem.SetAttribute("class_third_name", reader[7].ToString().Trim());
            }
            reader.Close();
            if (dataElem != null)
            {
                strucElem.AppendChild(dataElem);
            }
            dataElem = null;
            sql = @"select Top 2 i.CreatedDate,i.Fash_Index,i.Fash_RingMove,i.Fash_RingMoveRate,i.Class_Third_Code,n.Class_First_Name,n.Class_Second_Name,n.Class_Third_Name from SJQ_Boom_Index_Class_Third_Month i left join SJQ_Product_WithFullName n on i.Class_Third_Code=n.Class_Third_Code where i.Class_First_Code=4 and i.CreatedDate=(select top 1 CreatedDate from SJQ_Boom_Index_Class_Third_Month order by CreatedDate desc) and i.Fash_Index is not null order by i.Fash_Index desc";
            DbUtil.ExecuteReader(out reader, sql);
            while (reader.Read())
            {
                if (dataElem != null)
                {
                    strucElem.AppendChild(dataElem);
                }
                dataElem = productDoc.CreateElement("dataElem");
                code = reader[4].ToString().Trim();
                dataElem.SetAttribute("code", code);
                string phase = "";
                phase = reader[0].ToString().Trim().Split(' ')[0];
                dataElem.SetAttribute("Phase", phase.Trim());
                dataElem.SetAttribute("index", string.IsNullOrEmpty(reader[1].ToString()) ? "0" : Math.Round(decimal.Parse(reader[1].ToString()), 2).ToString());
                dataElem.SetAttribute("index_RingMove", string.IsNullOrEmpty(reader[2].ToString()) ? "0" : Math.Round(decimal.Parse(reader[2].ToString()), 2).ToString());
                dataElem.SetAttribute("index_RingMoveRate", string.IsNullOrEmpty(reader[3].ToString()) ? "0" : Math.Round(decimal.Parse(reader[3].ToString()), 2).ToString());
                dataElem.SetAttribute("class_first_name", reader[5].ToString().Trim());
                dataElem.SetAttribute("class_second_name", reader[6].ToString().Trim());
                dataElem.SetAttribute("class_third_name", reader[7].ToString().Trim());
            }
            reader.Close();
            if (dataElem != null)
            {
                strucElem.AppendChild(dataElem);
            }*/
            productDoc.AppendChild(strucElem);
            productDoc.Save(file);
            reader.Close();
        }

        /// <summary>
        /// 导出景气汇总信息
        /// </summary>
        public static void genBoomEtlXML()
        {
            if (!Directory.Exists("BoomXML"))
                Directory.CreateDirectory("BoomXML");
            string sql = "";
            XmlDocument productDoc = new XmlDocument();
            XmlDeclaration xml = (XmlDeclaration)productDoc.CreateNode(XmlNodeType.XmlDeclaration, "", "");
            xml.Encoding = "utf-8";
            productDoc.AppendChild(xml);
            XmlElement strucElem = productDoc.CreateElement("STRUCTURE");
            XmlElement dataElem = null;
            string code = "";
            SqlDataReader reader;
            //生成三级时尚指数列表  
            sql = @"SELECT CreatedDate,Class_Third_Code,NewProductsCount,NewProductSaleRate,FashScore,SaleMoney,ClientCounts,BandConfidence,MarketConfidence FROM SJQ_Boom_Etl where CreatedDate in(select top 2 CreatedDate from SJQ_Boom_Etl group by CreatedDate order by CreatedDate desc ) order by Class_Third_Code ,CreatedDate desc";
            DbUtil.ExecuteReader(out reader, sql);
            while (reader.Read())
            {
                if (dataElem != null)
                {
                    strucElem.AppendChild(dataElem);
                }
                dataElem = productDoc.CreateElement("dataElem");
                code = reader[1].ToString().Trim();
                dataElem.SetAttribute("代码", code);
                string phase = "";
                phase = reader[0].ToString().Trim().Split(' ')[0];
                dataElem.SetAttribute("日期", phase.Trim());

                dataElem.SetAttribute("新品数", string.IsNullOrEmpty(reader[2].ToString()) ? "0" : Math.Round(decimal.Parse(reader[2].ToString()), 2).ToString());
                dataElem.SetAttribute("新品比", string.IsNullOrEmpty(reader[3].ToString()) ? "0" : Math.Round(decimal.Parse(reader[3].ToString()), 2).ToString());
                dataElem.SetAttribute("时尚分", string.IsNullOrEmpty(reader[4].ToString()) ? "0" : Math.Round(decimal.Parse(reader[4].ToString()), 2).ToString());
                dataElem.SetAttribute("销售额", string.IsNullOrEmpty(reader[5].ToString()) ? "0" : Math.Round(decimal.Parse(reader[5].ToString()), 2).ToString());
                dataElem.SetAttribute("客流量", string.IsNullOrEmpty(reader[6].ToString()) ? "0" : Math.Round(decimal.Parse(reader[6].ToString()), 2).ToString());
                dataElem.SetAttribute("品牌信心", string.IsNullOrEmpty(reader[7].ToString()) ? "0" : Math.Round(decimal.Parse(reader[7].ToString()), 2).ToString());
                dataElem.SetAttribute("市场信心", string.IsNullOrEmpty(reader[8].ToString()) ? "0" : Math.Round(decimal.Parse(reader[8].ToString()), 2).ToString());
            }
            reader.Close();
            if (dataElem != null)
            {
                strucElem.AppendChild(dataElem);
            }
            productDoc.AppendChild(strucElem);
            productDoc.Save("BoomXML/景气汇总.xml");
            reader.Close();
        }

        public static void ConfigSetConnectionString(string AppKey, string AppValue)
        {
            //XmlDocument xDoc = new XmlDocument();            

            //XmlNode xNode;
            //XmlElement xElem1;
            //XmlElement xElem2;
            //xDoc.Load(System.Windows.Forms.Application.ExecutablePath + ".config");
            //xNode = xDoc.SelectSingleNode("//connectionStrings");            
            //xElem1 = (XmlElement)xNode.SelectSingleNode("//add[@name='" + AppKey + "']");
            //if (xElem1 != null) xElem1.SetAttribute("connectionString", AppValue);
            //else
            //{
            //    xElem2 = xDoc.CreateElement("add");
            //    xElem2.SetAttribute("name", AppKey);
            //    xElem2.SetAttribute("connectionString", AppValue);
            //    xNode.AppendChild(xElem2);
            //}
            //xDoc.Save(System.Windows.Forms.Application.ExecutablePath + ".config");

            //DbUtil.ConnectionString = AppValue;
            //DbUtil.getConnection().Close(); 

            //记录该连接串是否已经存在      
            bool isModified = false;

            //如果要更改的连接串已经存在      
            if (ConfigurationManager.ConnectionStrings[AppKey] != null)
            {
                isModified = true;
            }

            //新建一个连接字符串实例      
            ConnectionStringSettings mySettings = new ConnectionStringSettings(AppKey, AppValue);

            // 打开可执行的配置文件*.exe.config      
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            // 如果连接串已存在，首先删除它     
            if (isModified)
            {
                config.ConnectionStrings.ConnectionStrings.Remove(AppKey);
            }

            // 将新的连接串添加到配置文件中.      
            config.ConnectionStrings.ConnectionStrings.Add(mySettings);

            // 保存对配置文件所作的更改      
            config.Save(ConfigurationSaveMode.Modified);

            // 强制重新载入配置文件的ConnectionStrings配置节     
            ConfigurationManager.RefreshSection("ConnectionStrings");
        }
  
    }
}
