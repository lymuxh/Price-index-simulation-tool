using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using ZedGraph;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ZI
{
    public partial class Test : Form
    {
        public Test()
        {
            InitializeComponent();
          //  ZedGraphControl zedGraphControl1 = new ZedGraphControl();
            createPane(zedGraphControl1);
        }

        private void Test_Load(object sender, EventArgs e)
        {

        }
        public void createPane(ZedGraphControl zgc)
        {
            GraphPane myPane = zgc.GraphPane;

            //设置图标标题和x、y轴标题
            myPane.Title.Text = "单肩包价格指数";
            myPane.XAxis.Title.Text = "发布日期";
            myPane.YAxis.Title.Text = "价格指数";

            //更改标题的字体
            FontSpec myFont = new FontSpec("Arial", 20, Color.Red, false, false, false);
            myPane.Title.FontSpec = myFont;
            myPane.XAxis.Title.FontSpec = myFont;
            myPane.YAxis.Title.FontSpec = myFont;

            // 造一些数据，PointPairList里有数据对x，y的数组
            Random y = new Random();
            PointPairList list1 = new PointPairList();
            for (int i = 0; i < 12; i++)
            {
                double x = i;
                //double y1 = 1.5 + Math.Sin((double)i * 0.2);
                double y1 = y.NextDouble() * 1000;
                list1.Add(x, y1); //添加一组数据
            }
            //清空绘制区 
            myPane.CurveList.Clear();
           // myPane.GraphItemList.Clear(); 

            // 用list1生产一条曲线，标注是“东航”
            LineItem myCurve = myPane.AddCurve("价格指数", list1, Color.Red, SymbolType.Circle);

            //填充图表颜色
            myPane.Fill = new Fill(Color.White, Color.FromArgb(200, 200, 255), 45.0f);

            //以上生成的图标X轴为数字，下面将转换为日期的文本
            string[] labels = new string[12];
            for (int i = 0; i < 12; i++)
            {
                labels[i] = System.DateTime.Now.AddDays(i).ToShortDateString();
            }
            myPane.XAxis.Scale.TextLabels = labels; //X轴文本取值
            myPane.XAxis.Type = AxisType.Text;   //X轴类型

            //画到zedGraphControl1控件中，此句必加
            zgc.AxisChange();

            //重绘控件
            Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            createPane(zedGraphControl1);
        }
    }
}
