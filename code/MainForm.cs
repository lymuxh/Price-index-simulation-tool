using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ZedGraph;
using ZI.Utils;
using System.Data.SqlClient;
using System.Collections.Generic; 

namespace ZI
{
   
    public partial class MainForm : Form
    {
        private Rectangle[] pointSquares = new Rectangle[14];
        private string tagMsg = "当前状态：【价格指数状态】";
        public static string nowProject = "";//当前项目代码
        public static string nowProjectAlias = "";//当前项目别名
        public static int nowClassNum = 3;//当前项目的级数
        public static int nowIndex = 1;//当前项目的导入周期
        public static string sort = "";//指数发布周期
        public static string class_code = ""; //系统发布后树所对应的code
        public static string class_name = "";//日周月环比的判断
        public static string index_price = "";//指数显示或者价格显示判断
        public static string price_type = "";//指数显示几级
        public static string type = "";//显示第几级
        public static string createdbegindate = "";//开始日期
        public static string createdenddate = "";//结束日期
        public static int rowindex = 1;//到第几页
        public static int pagenum = 0;//共几页
        
        public MainForm()
        {
            InitializeComponent();
            this.panel1.Visible = false;
            this.panel2.Visible = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.cheakDb();
            // TODO: 这行代码将数据加载到表“zI_DataBaseDataSet.ZI_Price_Ods_Day”中。您可以根据需要移动或移除它。
            DbUtil.checkProject();
            setupHotPoint();
            /* 对底部状态栏进行更新的方法*/
            this.mainStatusLabel.Text = "启动完毕";
            this.statusStrip1.Update();
        }

        void loginF_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(e.CloseReason==CloseReason.UserClosing)
                this.Dispose();
        }        

        /// <summary>
        /// 检查数据库连接状态，如果无法连接，则打开数据库设置页面
        /// </summary>
        private void cheakDb()
        {
            try
            {
                DbUtil.getConnection();
            }
            catch (SqlException e) {
                OptionForm optionF = new OptionForm();
                optionF.Visible = false;
                optionF.ShowDialog(this);
            }
        }

        private void 选项ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OptionForm optionF = new OptionForm();
            optionF.ShowDialog(this);
        }

        #region 界面
        //捕捉鼠标移动事件，在热点显示手形指针
        private void panel_MouseMove(object sender, MouseEventArgs e)
        {

            projectlabel.Text = nowProject;
            aliaslabel.Text = nowProjectAlias;
            if (nowProjectAlias != "")
            {
                this.Text = "统计指数实验系统 - " + nowProjectAlias;
            }
            if (pointSquares[0].Contains(e.Location))
            {
                ((System.Windows.Forms.Panel)sender).Cursor = Cursors.Hand;
                this.helpLabel.Text = "切换到计算平台";
            }
            else if (pointSquares[1].Contains(e.Location))
            {
                ((System.Windows.Forms.Panel)sender).Cursor = Cursors.Hand;
                this.helpLabel.Text = "切换到发布平台";
            }
            else if (pointSquares[2].Contains(e.Location))
            {
                ((System.Windows.Forms.Panel)sender).Cursor = Cursors.Hand;
                this.helpLabel.Text = "导入价格采集表";
            }
            else if (pointSquares[3].Contains(e.Location))
            {
                ((System.Windows.Forms.Panel)sender).Cursor = Cursors.Hand;
                this.helpLabel.Text = "将导入的价格采集表进行汇总";
            }
            else if (pointSquares[4].Contains(e.Location))
            {
                ((System.Windows.Forms.Panel)sender).Cursor = Cursors.Hand;
                this.helpLabel.Text = "对汇总结果进行指数计算";
            }
            else if (pointSquares[5].Contains(e.Location))
            {
                ((System.Windows.Forms.Panel)sender).Cursor = Cursors.Hand;
                this.helpLabel.Text = "进行指数发布";
            }
            else if (pointSquares[6].Contains(e.Location))
            {
                ((System.Windows.Forms.Panel)sender).Cursor = Cursors.Hand;
                this.helpLabel.Text = "将删除指数计算数据和中间结果";
            }
            else if (pointSquares[7].Contains(e.Location))
            {
                ((System.Windows.Forms.Panel)sender).Cursor = Cursors.Hand;
                this.helpLabel.Text = "将删除导入的采样数据分类权重等数据，此操作不可逆，应谨慎使用";
            }
            else
            {
                try
                {
                    ((System.Windows.Forms.Panel)sender).Cursor = Cursors.Default;
                      this.helpLabel.Text = tagMsg + "鼠标移动到相应区域显示提示";
                }
                catch (Exception ex)
                {
                    ((System.Windows.Forms.Form)sender).Cursor = Cursors.Default;
                      this.helpLabel.Text = tagMsg + "鼠标移动到相应区域显示提示";
                }
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            projectLabel1.Text = nowProject;
            AliasLabel1.Text = nowProjectAlias;
            if (nowProjectAlias != "")
            {
                this.Text = "统计指数实验系统 - " + nowProjectAlias;
            }
            if (pointSquares[0].Contains(e.Location))//切换计算平台
            {
                ((System.Windows.Forms.Panel)sender).Cursor = Cursors.Hand;
            }
            else if (pointSquares[1].Contains(e.Location))//切换发布平台
            {
                ((System.Windows.Forms.Panel)sender).Cursor = Cursors.Hand;
            }
            else if (pointSquares[8].Contains(e.Location))//查询
            {
                ((System.Windows.Forms.Panel)sender).Cursor = Cursors.Hand;
            }
            else if (pointSquares[9].Contains(e.Location))//第一页
            {
                ((System.Windows.Forms.Panel)sender).Cursor = Cursors.Hand;
            }
            else if (pointSquares[10].Contains(e.Location))//上一页
            {
                ((System.Windows.Forms.Panel)sender).Cursor = Cursors.Hand;
            }
            else if (pointSquares[11].Contains(e.Location))//下一页
            {
                ((System.Windows.Forms.Panel)sender).Cursor = Cursors.Hand;
            }
            else if (pointSquares[12].Contains(e.Location))//最后一页
            {
                ((System.Windows.Forms.Panel)sender).Cursor = Cursors.Hand;
            }
            else
            {
                try
                {
                    ((System.Windows.Forms.Panel)sender).Cursor = Cursors.Default;
                }
                catch (Exception ex)
                {
                    ((System.Windows.Forms.Form)sender).Cursor = Cursors.Default;
                }
            }
        }
        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            projectLabel1.Text = nowProject;
            AliasLabel1.Text = nowProjectAlias;
            if (nowProjectAlias != "")
            {
                this.Text = "统计指数实验系统 - " + nowProjectAlias;
            }
            if (pointSquares[0].Contains(e.Location))//切换计算平台
            {
                ((System.Windows.Forms.Panel)sender).Cursor = Cursors.Hand;
            }
            else if (pointSquares[1].Contains(e.Location))//切换发布平台
            {
                ((System.Windows.Forms.Panel)sender).Cursor = Cursors.Hand;
            }
            else if (pointSquares[8].Contains(e.Location))//查询
            {
                ((System.Windows.Forms.Panel)sender).Cursor = Cursors.Hand;
            }
            else if (pointSquares[9].Contains(e.Location))//第一页
            {
                ((System.Windows.Forms.Panel)sender).Cursor = Cursors.Hand;
            }
            else if (pointSquares[10].Contains(e.Location))//上一页
            {
                ((System.Windows.Forms.Panel)sender).Cursor = Cursors.Hand;
            }
            else if (pointSquares[11].Contains(e.Location))//下一页
            {
                ((System.Windows.Forms.Panel)sender).Cursor = Cursors.Hand;
            }
            else if (pointSquares[12].Contains(e.Location))//最后一页
            {
                ((System.Windows.Forms.Panel)sender).Cursor = Cursors.Hand;
            }
            else
            {
                try
                {
                    ((System.Windows.Forms.Panel)sender).Cursor = Cursors.Default;
                }
                catch (Exception ex)
                {
                    ((System.Windows.Forms.Form)sender).Cursor = Cursors.Default;
                }
            }
        }


        /*计算平台*/
        private void panel_MouseClick(object sender, MouseEventArgs e)
        {
            if (pointSquares[0].Contains(e.Location))//计算平台
            {
               this.panel2.Visible = false;
               this.panel1.Visible=false;
            }
            else if (pointSquares[1].Contains(e.Location))//指数发布平台
            {
                if (nowProject != "")
                {
                    this.panel2.Visible = false;
                    this.panel1.Visible = true;
                    //createPane(this.zedGraphControl1);
                    treeView1.Nodes.Clear();
                    if (sort.Length == 12)
                    {
                        loadTreeViewDay();
                        loadTreeViewWeek();
                        loadTreeViewMonth();
                        loadTreeViewRingDay();
                        loadTreeViewRingWeek();
                        loadTreeViewRingMonth();
                        DataSet ds1 = DbUtil.GetDayIndexData("", "1");
                        int count = DbUtil.GetIndexDatacount("", "1");
                        class_name = "1";
                        dataGridView1.DataSource = ds1.Tables[0];
                        createPane(this.zedGraphControl1);
                        pagenum = count / 9 + (count % 9 == 0 ? 0 : 1);
                        showlabel.Text = "共" + count.ToString() + "条   1 /" + pagenum + "页";
                    }
                    if (sort.Length == 9)
                    {
                        loadTreeViewWeek();
                        loadTreeViewMonth();
                        loadTreeViewRingWeek();
                        loadTreeViewRingMonth();
                        DataSet ds1 = DbUtil.GetDayIndexData("", "2");
                        int count = DbUtil.GetIndexDatacount("", "2");
                        class_name = "2";
                        dataGridView1.DataSource = ds1.Tables[0];
                        createPane(this.zedGraphControl1);
                        pagenum = count / 9 + (count % 9 == 0 ? 0 : 1);
                        showlabel.Text = "共" + count.ToString() + "条   1 /" + pagenum + "页";
                    }
                    if (sort.Length == 5)
                    {
                        loadTreeViewMonth();
                        loadTreeViewRingMonth();
                        DataSet ds1 = DbUtil.GetDayIndexData("", "3");
                        int count = DbUtil.GetIndexDatacount("", "3");
                        class_name = "3";
                        dataGridView1.DataSource = ds1.Tables[0];
                        createPane(this.zedGraphControl1);
                        pagenum = count / 9 + (count % 9 == 0 ? 0 : 1);
                        showlabel.Text = "共" + count.ToString() + "条   1 /" + pagenum + "页";
                    }
                    
                    
                }
                else
                {
                    MessageBox.Show("请打开或者创建项目");
                }
            }
            else if (pointSquares[13].Contains(e.Location))//价格发布平台
            {
                if (nowProject != "")
                {
                    this.panel2.Visible = true;
                    //createPane(this.zedGraphControl1);
                    treeView2.Nodes.Clear();
                    loadTreePriceViewDay();
                    DataSet ds1 = DbUtil.GetDayPriceData("", "1");
                    int count = DbUtil.GetPriceDatacount("", "1");
                    class_name = "1";
                    index_price = "price";
                    dataGridView2.DataSource = ds1.Tables[0];
                    createPane(this.zedGraphControl2);
                    pagenum = count / 9 + (count % 9 == 0 ? 0 : 1);
                    showlabel.Text = "共" + count.ToString() + "条   1 /" + pagenum + "页";
                   
                }
                else
                {
                    MessageBox.Show("请打开或者创建项目");
                }
            }
            else if (pointSquares[2].Contains(e.Location))//价格采集
            {
                tagMsg = "当前状态：【价格指数状态】";
                if (nowProject != "")
                {
                    this.导入价格采集表IToolStripMenuItem.PerformClick();
                }
                else
                {
                    MessageBox.Show("请先打开或者创建项目");
                }
            }
            else if (pointSquares[3].Contains(e.Location))//数据汇总
            {
                if (nowProject != "")
                {
                    if (sort.Length==12)
                    {
                        DbUtil.totalDayPrice();
                        DbUtil.totalWeekPrice();
                        DbUtil.totalMonthPrice();
                    }
                    if (sort.Length == 9)
                    {
                        DbUtil.totalWeekPrice();
                        DbUtil.totalMonthPrice();
                    }
                    if (sort.Length == 5)
                    {
                        DbUtil.totalMonthPrice();
                    }
                    
                }
                else
                {
                    MessageBox.Show("请先打开或者创建项目");
                }
            }
            else if (pointSquares[4].Contains(e.Location))//指数计算
            {
                if (nowProject != "")
                {
                    if (sort.Length==12)
                    {
                        DbUtil.computePriceIndexDay();
                        DbUtil.computeRingPriceIndexDay();
                        DbUtil.computePriceIndexWeek();
                        DbUtil.computeRingPriceIndexWeek();
                        DbUtil.computePriceIndexMonth();
                        DbUtil.computeRingPriceIndexMonth();
                    }
                    if (sort.Length == 9)
                    {
                        DbUtil.computePriceIndexWeek();
                        DbUtil.computeRingPriceIndexWeek();
                        DbUtil.computePriceIndexMonth();
                        DbUtil.computeRingPriceIndexMonth();
                    }
                    if (sort.Length == 5)
                    {
                        DbUtil.computePriceIndexMonth();
                        DbUtil.computeRingPriceIndexMonth();
                    }
                }
                else
                {
                    MessageBox.Show("请先打开或者创建项目");
                }
            }
            else if (pointSquares[5].Contains(e.Location))//指数发布
            {
                if (nowProject != "")
                {
                    this.panel1.Visible = true;
                    //createPane(this.zedGraphControl1);
                    treeView1.Nodes.Clear();
                    if (sort.Length == 12)
                    {
                        loadTreeViewDay();
                        loadTreeViewWeek();
                        loadTreeViewMonth();
                        loadTreeViewRingDay();
                        loadTreeViewRingWeek();
                        loadTreeViewRingMonth();
                        DataSet ds1 = DbUtil.GetDayIndexData("", "1");
                        int count = DbUtil.GetIndexDatacount("", "1");
                        class_name = "1";
                        dataGridView1.DataSource = ds1.Tables[0];
                        createPane(this.zedGraphControl1);
                        pagenum = count / 9 + (count % 9 == 0 ? 0 : 1);
                        showlabel.Text = "共" + count.ToString() + "条   1 /" + pagenum + "页";
                    }
                    if (sort.Length == 9)
                    {
                        loadTreeViewWeek();
                        loadTreeViewMonth();
                        loadTreeViewRingWeek();
                        loadTreeViewRingMonth();
                        DataSet ds1 = DbUtil.GetDayIndexData("", "2");
                        int count = DbUtil.GetIndexDatacount("", "2");
                        class_name = "2";
                        dataGridView1.DataSource = ds1.Tables[0];
                        createPane(this.zedGraphControl1);
                        pagenum = count / 9 + (count % 9 == 0 ? 0 : 1);
                        showlabel.Text = "共" + count.ToString() + "条   1 /" + pagenum + "页";
                    }
                    if (sort.Length == 5)
                    {
                        loadTreeViewMonth();
                        loadTreeViewRingMonth();
                        DataSet ds1 = DbUtil.GetDayIndexData("", "3");
                        int count = DbUtil.GetIndexDatacount("", "3");
                        class_name = "3";
                        dataGridView1.DataSource = ds1.Tables[0];
                        createPane(this.zedGraphControl1);
                        pagenum = count / 9 + (count % 9 == 0 ? 0 : 1);
                        showlabel.Text = "共" + count.ToString() + "条   1 /" + pagenum + "页";
                    }
                    
                    
                }
                else
                {
                    MessageBox.Show("请打开或者创建项目");
                }
            }
            else if (pointSquares[6].Contains(e.Location))//清空计算数据
            {
                if (nowProject != "")
                {
                    if (MessageBox.Show(this, "该过程不可逆，确定要清空所有计算数据表吗？", "请确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2).Equals(DialogResult.Yes))
                    {
                        DbUtil.truncateComputedTable();
                        MessageBox.Show("已清空计算数据表");
                    }
                }
                else
                {
                    MessageBox.Show("请先打开或者创建项目");
                }
            }
            else if (pointSquares[7].Contains(e.Location))//清空原始数据
            {
                if (nowProject != "")
                {
                    if (MessageBox.Show(this, "该过程不可逆，确定要所有清空采集数据表吗？", "请确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2).Equals(DialogResult.Yes))
                    {
                        DbUtil.truncatePickTable();
                        MessageBox.Show("已清空采集数据表");
                    }
                }
                else
                {
                    MessageBox.Show("请先打开或者创建项目");
                }
            }
        }
        /*指数发布平台*/
        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (pointSquares[0].Contains(e.Location))//计算平台
            {
                this.panel2.Visible = false;
                this.panel1.Visible=false;
            }
            else if (pointSquares[1].Contains(e.Location))//指数发布平台
            {
                if (nowProject != "")
                {
                    this.panel2.Visible = false;
                    this.panel1.Visible = true;
                    //createPane(this.zedGraphControl1);
                    treeView1.Nodes.Clear();
                    if (sort.Length == 12)
                    {
                        loadTreeViewDay();
                        loadTreeViewWeek();
                        loadTreeViewMonth();
                        loadTreeViewRingDay();
                        loadTreeViewRingWeek();
                        loadTreeViewRingMonth();
                        DataSet ds1 = DbUtil.GetDayIndexData("", "1");
                        int count = DbUtil.GetIndexDatacount("", "1");
                        class_name = "1";
                        dataGridView1.DataSource = ds1.Tables[0];
                        createPane(this.zedGraphControl1);
                        pagenum = count / 9 + (count % 9 == 0 ? 0 : 1);
                        showlabel.Text = "共" + count.ToString() + "条   1 /" + pagenum + "页";
                    }
                    if (sort.Length == 9)
                    {
                        loadTreeViewWeek();
                        loadTreeViewMonth();
                        loadTreeViewRingWeek();
                        loadTreeViewRingMonth();
                        DataSet ds1 = DbUtil.GetDayIndexData("", "2");
                        int count = DbUtil.GetIndexDatacount("", "2");
                        class_name = "2";
                        dataGridView1.DataSource = ds1.Tables[0];
                        createPane(this.zedGraphControl1);
                        pagenum = count / 9 + (count % 9 == 0 ? 0 : 1);
                        showlabel.Text = "共" + count.ToString() + "条   1 /" + pagenum + "页";
                    }
                    if (sort.Length == 5)
                    {
                        loadTreeViewMonth();
                        loadTreeViewRingMonth();
                        DataSet ds1 = DbUtil.GetDayIndexData("", "3");
                        int count = DbUtil.GetIndexDatacount("", "3");
                        class_name = "3";
                        dataGridView1.DataSource = ds1.Tables[0];
                        createPane(this.zedGraphControl1);
                        pagenum = count / 9 + (count % 9 == 0 ? 0 : 1);
                        showlabel.Text = "共" + count.ToString() + "条   1 /" + pagenum + "页";
                    }


                }
                else
                {
                    MessageBox.Show("请打开或者创建项目");
                }
            }
            else if (pointSquares[13].Contains(e.Location))//价格发布平台
            {
                if (nowProject != "")
                {
                    this.panel2.Visible = true;
                    //createPane(this.zedGraphControl1);
                    treeView2.Nodes.Clear();
                    loadTreePriceViewDay();
                    DataSet ds1 = DbUtil.GetDayPriceData("", "1");
                    int count = DbUtil.GetPriceDatacount("", "1");
                    class_name = "1";
                    index_price = "price";
                    dataGridView2.DataSource = ds1.Tables[0];
                    createPane(this.zedGraphControl2);
                    pagenum = count / 9 + (count % 9 == 0 ? 0 : 1);
                    showlabel.Text = "共" + count.ToString() + "条   1 /" + pagenum + "页";

                }
                else
                {
                    MessageBox.Show("请打开或者创建项目");
                }
            }
            else if (pointSquares[8].Contains(e.Location))//查询
            {
                createdbegindate = fromDateTimePicker.Value.ToString();
                createdenddate = toDateTimePicker.Value.ToString();
                DataSet ds1 = DbUtil.GetDayIndexData(class_code, class_name);
                int count = DbUtil.GetIndexDatacount(class_code, class_name);
                type = (class_code.Length / 2).ToString();
                dataGridView1.DataSource = ds1.Tables[0];
                createPane(this.zedGraphControl1);
                int pagenum = count / 9 + (count % 9 == 0 ? 0 : 1);
                showlabel.Text = "共" + count.ToString() + "条   " + rowindex + " /" + pagenum + "页";
            }
            else if (pointSquares[9].Contains(e.Location))//第一页
            {
                if (rowindex != 1)
                {
                    rowindex = 1;
                    DataSet ds1 = DbUtil.GetDayIndexData(class_code, class_name);
                    int count = DbUtil.GetIndexDatacount(class_code, class_name);
                    type = (class_code.Length / 2).ToString();
                    dataGridView1.DataSource = ds1.Tables[0];
                    createPane(this.zedGraphControl1);
                    int pagenum = count / 9 + (count % 9 == 0 ? 0 : 1);
                    showlabel.Text = "共" + count.ToString() + "条   " + rowindex + " /" + pagenum + "页";
                }

            }
            else if (pointSquares[10].Contains(e.Location))//上一页
            {
                if (rowindex != 1)
                {
                    rowindex = rowindex - 1;
                    DataSet ds1 = DbUtil.GetDayIndexData(class_code, class_name);
                    int count = DbUtil.GetIndexDatacount(class_code, class_name);
                    type = (class_code.Length / 2).ToString();
                    dataGridView1.DataSource = ds1.Tables[0];
                    createPane(this.zedGraphControl1);
                    //pagenum = count / 9 + (count % 9 == 0 ? 0 : 1);
                    showlabel.Text = "共" + count.ToString() + "条   " + rowindex + " /" + pagenum + "页";
                }

            }
            else if (pointSquares[11].Contains(e.Location))//下一页
            {
                if (rowindex != pagenum)
                {
                    rowindex = rowindex + 1;
                    DataSet ds1 = DbUtil.GetDayIndexData(class_code, class_name);
                    int count = DbUtil.GetIndexDatacount(class_code, class_name);
                    type = (class_code.Length / 2).ToString();
                    dataGridView1.DataSource = ds1.Tables[0];
                    createPane(this.zedGraphControl1);
                    //pagenum = count / 9 + (count % 9 == 0 ? 0 : 1);
                    showlabel.Text = "共" + count.ToString() + "条   " + rowindex + " /" + pagenum + "页";
                }
            }
            else if (pointSquares[12].Contains(e.Location))//最后一页
            {
                if (rowindex != pagenum)
                {
                    rowindex = pagenum;
                    DataSet ds1 = DbUtil.GetDayIndexData(class_code, class_name);
                    int count = DbUtil.GetIndexDatacount(class_code, class_name);
                    type = (class_code.Length / 2).ToString();
                    dataGridView1.DataSource = ds1.Tables[0];
                    createPane(this.zedGraphControl1);
                    //pagenum = count / 9 + (count % 9 == 0 ? 0 : 1);
                    showlabel.Text = "共" + count.ToString() + "条   " + rowindex + " /" + pagenum + "页";
                }
            }
        }
        //价格发布平台
        private void panel2_MouseClick(object sender, MouseEventArgs e)
        {
            if (pointSquares[0].Contains(e.Location))//计算平台
            {
                this.panel2.Visible = false;
                this.panel1.Visible = false;
            }
            else if (pointSquares[1].Contains(e.Location))//指数发布平台
            {
                if (nowProject != "")
                {
                    this.panel2.Visible = false;
                    this.panel1.Visible = true;
                    //createPane(this.zedGraphControl1);
                    treeView1.Nodes.Clear();
                    if (sort.Length == 12)
                    {
                        loadTreeViewDay();
                        loadTreeViewWeek();
                        loadTreeViewMonth();
                        loadTreeViewRingDay();
                        loadTreeViewRingWeek();
                        loadTreeViewRingMonth();
                        DataSet ds1 = DbUtil.GetDayIndexData("", "1");
                        int count = DbUtil.GetIndexDatacount("", "1");
                        class_name = "1";
                        index_price = "index";
                        dataGridView1.DataSource = ds1.Tables[0];
                        createPane(this.zedGraphControl1);
                        pagenum = count / 9 + (count % 9 == 0 ? 0 : 1);
                        showlabel.Text = "共" + count.ToString() + "条   1 /" + pagenum + "页";
                    }
                    if (sort.Length == 9)
                    {
                        loadTreeViewWeek();
                        loadTreeViewMonth();
                        loadTreeViewRingWeek();
                        loadTreeViewRingMonth();
                        DataSet ds1 = DbUtil.GetDayIndexData("", "2");
                        int count = DbUtil.GetIndexDatacount("", "2");
                        class_name = "2";
                        dataGridView1.DataSource = ds1.Tables[0];
                        createPane(this.zedGraphControl1);
                        pagenum = count / 9 + (count % 9 == 0 ? 0 : 1);
                        showlabel.Text = "共" + count.ToString() + "条   1 /" + pagenum + "页";
                    }
                    if (sort.Length == 5)
                    {
                        loadTreeViewMonth();
                        loadTreeViewRingMonth();
                        DataSet ds1 = DbUtil.GetDayIndexData("", "3");
                        int count = DbUtil.GetIndexDatacount("", "3");
                        class_name = "3";
                        dataGridView1.DataSource = ds1.Tables[0];
                        createPane(this.zedGraphControl1);
                        pagenum = count / 9 + (count % 9 == 0 ? 0 : 1);
                        showlabel.Text = "共" + count.ToString() + "条   1 /" + pagenum + "页";
                    }


                }
                else
                {
                    MessageBox.Show("请打开或者创建项目");
                }
            }
            else if (pointSquares[13].Contains(e.Location))//价格发布平台
            {
                if (nowProject != "")
                {
                    this.panel2.Visible = true;
                    //createPane(this.zedGraphControl1);
                    treeView2.Nodes.Clear();
                    loadTreePriceViewDay();
                    DataSet ds1 = DbUtil.GetDayPriceData("", "1");
                    int count = DbUtil.GetPriceDatacount("", "1");
                    class_name = "1";
                    index_price = "price";
                    dataGridView2.DataSource = ds1.Tables[0];
                    createPane(this.zedGraphControl2);
                    pagenum = count / 9 + (count % 9 == 0 ? 0 : 1);
                    label1.Text = "共" + count.ToString() + "条   1 /" + pagenum + "页";
                }
                else
                {
                    MessageBox.Show("请打开或者创建项目");
                }
            }
            else if (pointSquares[8].Contains(e.Location))//查询
            {
                createdbegindate = fromDateTimePicker.Value.ToString();
                createdenddate = toDateTimePicker.Value.ToString();
                DataSet ds1 = DbUtil.GetDayPriceData(class_code, class_name);
                int count = DbUtil.GetPriceDatacount(class_code, class_name);
                type = (class_code.Length / 2).ToString();
                dataGridView2.DataSource = ds1.Tables[0];
                createPane(this.zedGraphControl2);
                //int pagenum = count / 9 + (count % 9 == 0 ? 0 : 1);
                label1.Text = "共" + count.ToString() + "条   " + rowindex + " /" + pagenum + "页";
            }
            else if (pointSquares[9].Contains(e.Location))//第一页
            {
                if (rowindex != 1)
                {
                    rowindex = 1;
                    DataSet ds1 = DbUtil.GetDayPriceData(class_code, class_name);
                    int count = DbUtil.GetPriceDatacount(class_code, class_name);
                    type = (class_code.Length / 2).ToString();
                    dataGridView2.DataSource = ds1.Tables[0];
                    createPane(this.zedGraphControl2);
                    //int pagenum = count / 9 + (count % 9 == 0 ? 0 : 1);
                    label1.Text = "共" + count.ToString() + "条   " + rowindex + " /" + pagenum + "页";
                }

            }
            else if (pointSquares[10].Contains(e.Location))//上一页
            {
                if (rowindex != 1)
                {
                    rowindex = rowindex - 1;
                    DataSet ds1 = DbUtil.GetDayPriceData(class_code, class_name);
                    int count = DbUtil.GetPriceDatacount(class_code, class_name);
                    type = (class_code.Length / 2).ToString();
                    dataGridView2.DataSource = ds1.Tables[0];
                    createPane(this.zedGraphControl2);
                    //pagenum = count / 9 + (count % 9 == 0 ? 0 : 1);
                    label1.Text = "共" + count.ToString() + "条   " + rowindex + " /" + pagenum + "页";
                }

            }
            else if (pointSquares[11].Contains(e.Location))//下一页
            {
                if (rowindex != pagenum)
                {
                    rowindex = rowindex + 1;
                    DataSet ds1 = DbUtil.GetDayPriceData(class_code, class_name);
                    int  count = DbUtil.GetPriceDatacount(class_code, class_name);
                    type = (class_code.Length / 2).ToString();
                    dataGridView2.DataSource = ds1.Tables[0];
                    createPane(this.zedGraphControl2);
                    //pagenum = count / 9 + (count % 9 == 0 ? 0 : 1);
                    label1.Text = "共" + count.ToString() + "条   " + rowindex + " /" + pagenum + "页";
                }
            }
            else if (pointSquares[12].Contains(e.Location))//最后一页
            {
                if (rowindex != pagenum)
                {
                    rowindex = pagenum;
                    DataSet ds1 = DbUtil.GetDayPriceData(class_code, class_name);
                    int count = DbUtil.GetPriceDatacount(class_code, class_name);
                    //
                    type = (class_code.Length / 2).ToString();
                    dataGridView2.DataSource = ds1.Tables[0];
                    createPane(this.zedGraphControl2);
                    label1.Text = "共" + count.ToString() + "条   " + rowindex + " /" + pagenum + "页";
                }
            }
        }


        /// <summary>
        /// 设置图片的热点
        /// </summary>
        private void setupHotPoint()
        {
            /*计算平台*/
            pointSquares[0] = new Rectangle(480, 14, 130, 35);
            /*发布平台*/
            pointSquares[1] = new Rectangle(620, 14, 130, 35);

            /*价格采集*/
            pointSquares[2] = new Rectangle(75, 103, 205, 119);
            /*数据汇总*/
            pointSquares[3] = new Rectangle(221, 332, 161, 94);
            /*指数计算*/
            pointSquares[4] = new Rectangle(440, 136, 181, 105);
            /*指数发布*/
            pointSquares[5] = new Rectangle(615, 359, 218, 127);

            /*清空计算数据*/
            pointSquares[6] = new Rectangle(19, 484, 149, 30);
            /*清空原始数据*/
            pointSquares[7] = new Rectangle(179, 484, 149, 30);

            /*指数查询*/
            pointSquares[8] = new Rectangle(412, 323, 41, 30);
            /*第一页*/
            pointSquares[9] = new Rectangle(601, 323, 66, 30);
            /*上一页*/
            pointSquares[10] = new Rectangle(670, 323, 66, 30);
            /*下一页*/
            pointSquares[11] = new Rectangle(739, 323, 66, 30);
            /*最后一页*/
            pointSquares[12] = new Rectangle(808, 323, 66, 30);
            /*价格发布平台*/
            pointSquares[13] = new Rectangle(761, 14, 130, 35);
        }


        #endregion    
        public void createPane(ZedGraphControl zgc)
        {
            string sql = "";
            if (class_name == "")
            {
                class_name = "1";
            }
            GraphPane myPane = zgc.GraphPane;

            //设置图标标题和x、y轴标题
          //  myPane.Title.Text = "西装套装价格指数";
          //  myPane.XAxis.Title.Text = "发布日期";
          //  myPane.YAxis.Title.Text = "价格指数";
            myPane.Title.IsVisible = false;
            myPane.YAxis.Title.IsVisible = false;
            myPane.XAxis.Title.IsVisible = false;

            //更改标题的字体
            FontSpec myFont = new FontSpec("Arial", 20, Color.Red, false, false, false);
            myPane.Title.FontSpec = myFont;
            myPane.XAxis.Title.FontSpec = myFont;
            myPane.YAxis.Title.FontSpec = myFont;

            // 造一些数据，PointPairList里有数据对x，y的数组
            Random y = new Random();
            PointPairList list1 = new PointPairList();
           /* for (int i = 0; i < 12; i++)
            {
                double x = i;
                //double y1 = 1.5 + Math.Sin((double)i * 0.2);
                double y1 = y.NextDouble() * 1000;
                list1.Add(x, y1); //添加一组数据
            }*/
            string pricetype = "", pricedate = "";
            if (class_name == "1")
            {
                pricetype = "Price";
                pricedate = "day";
            }
            else if (class_name == "2")
            {
                pricetype = "Price";
                pricedate = "Week";
            }
            else if (class_name == "3")
            {
                pricetype = "Price";
                pricedate = "Month";
            }
            else if (class_name == "4")
            {
                pricetype = "PriceRing";
                pricedate = "Day";
            }
            else if (class_name == "5")
            {
                pricetype = "PriceRing";
                pricedate = "Week";
            }
            else if (class_name == "6")
            {
                pricetype = "PriceRing";
                pricedate = "Month";
            }
            int pagerow = rowindex - 1;
            string code_type = "";
            if (index_price == "price")
            {
                if (class_code == "")
                {
                    if (createdbegindate == "")
                    {
                        createdbegindate = "1900-01-01";
                        createdenddate = DateTime.Now.ToString("yyyy-MM-dd");
                    }
                    sql = @"select createddate AS '期数',price AS '平均价格' from (select ROW_NUMBER() over (order by createddate desc) as row,  t.createddate,avg(t.price) as price from " + nowProject + "_" + pricetype + "_Pick t  where CreatedDate>='" + MainForm.createdbegindate + "' and CreatedDate<='" + MainForm.createdenddate + "' group by CreatedDate) tt where tt.row between 9*" + pagerow + " and 9*" + (pagerow + 1);
                }
                else
                {
                    if (class_code.Length == nowClassNum * 2)
                    {
                        code_type = "";
                    }
                    else
                    {
                        code_type = "_" + class_code.Length / 2;
                    }
                    sql = @"select  createddate AS '期数',price AS '平均价格' from (select ROW_NUMBER() over (order by createddate desc) as row,  t.createddate,avg(t.price) as price from " + nowProject + "_" + pricetype + "_Pick t  where Class" + code_type + "_Code='" + class_code + "' and CreatedDate>='" + MainForm.createdbegindate + "' and CreatedDate<='" + MainForm.createdenddate + "' group by CreatedDate) tt where tt.row between 9*" + pagerow + " and 9*" + (pagerow + 1);
                }
            }
            else
            {
                if (class_code == "")
                {
                    if (createdbegindate == "")
                    {
                        createdbegindate = "1900-01-01";
                        createdenddate = DateTime.Now.ToString("yyyy-MM-dd");
                    }
                    sql = @"select top 9 CreatedDate AS '期数',PriceIndex AS '指数' , RingMove AS '指数涨跌值', RingMoveRate AS '指数涨跌幅' from ( select ROW_NUMBER()OVER(ORDER BY CreatedDate desc) AS RowNumber,* from " + nowProject + "_" + pricetype + "_Index_" + pricedate + " where CreatedDate>='" + createdbegindate + "' and CreatedDate<='" + createdenddate + "') A where RowNumber>9*" + pagerow;
                }
                else
                {
                    sql = @"select top 9 CreatedDate AS '期数',Class_Index AS '指数' , RingMove AS '指数涨跌值', RingMoveRate AS '指数涨跌幅' from ( select ROW_NUMBER()OVER(ORDER BY CreatedDate desc) AS RowNumber,* from " + nowProject + "_" + pricetype + "_Index_Class_" + type + "_" + pricedate + " where Class_Code='" + class_code + "' and CreatedDate>='" + createdbegindate + "' and CreatedDate<='" + createdenddate + "') A where RowNumber>9*" + pagerow;
                }
            }
            SqlDataReader reader;
            DbUtil.ExecuteReader(out reader, sql);
            string[] labels = new string[10];
            List<string> listArr = new List<string>();
            List<string> list2 = new List<string>();
            while (reader.Read())
            {
                list2.Add(reader[1].ToString().Trim());
                listArr.Add(reader[0].ToString().Trim());
            }
            for (int i = 0; i <listArr.Count ; i++)
            {
                int j = listArr.Count-1 - i;
                list1.Add(0, double.Parse(list2[j]));
                labels[i] = listArr[j].Substring(0, 9);
            }
            myPane.XAxis.Scale.TextLabels = labels; //X轴文本取值
            myPane.XAxis.Type = AxisType.Text;   //X轴类型

            //画到zedGraphControl1控件中，此句必加
            zgc.AxisChange();

            //重绘控件
            Refresh();
            reader.Close();
            //清空绘制区 
            myPane.CurveList.Clear();
            // myPane.GraphItemList.Clear(); 

            // 用list1生产一条曲线，标注是“东航”
            if (index_price == "price")
            {
                LineItem myCurve = myPane.AddCurve("平均价格", list1, Color.Red, SymbolType.Circle);
            }
            else
            {
                LineItem myCurve = myPane.AddCurve("价格指数", list1, Color.Red, SymbolType.Circle);
            }

           

            //以上生成的图标X轴为数字，下面将转换为日期的文本
           // string[] labels = new string[10];
            //for (int i = 0; i < 10; i++)
            //{
            //    labels[i] = System.DateTime.Now.AddDays(i).ToShortDateString();
            //}
            //myPane.XAxis.Scale.TextLabels = labels; //X轴文本取值
            //myPane.XAxis.Type = AxisType.Text;   //X轴类型

            ////画到zedGraphControl1控件中，此句必加
            zgc.AxisChange();

            ////重绘控件
            Refresh();
        }

        private void 新建项目NToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewProject project = new NewProject();
            project.Show(this);

        }

        private void 打开项目OToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenProject project = new OpenProject();
            project.tag = 1;
            project.Show(this);
        }

        private void 删除项目DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenProject project = new OpenProject();
            project.tag = 2;
            project.Show(this);
        }

        private void 退出系统QToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }


        private void 数据库初始化BToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.mainStatusLabel.Text = "开始初始化...";
            this.statusStrip1.Update();
            DbUtil.initDataBase();
            this.mainStatusLabel.Text = "初始化完成";
            this.statusStrip1.Update();
        }

        private void 清空分类权重表WToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (nowProject != "")
            {
                if (MessageBox.Show(this, "该过程不可逆，确定要清空分类权重吗？", "请确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2).Equals(DialogResult.Yes))
                {
                    DbUtil.truncateOrgTable();
                    MessageBox.Show("已清空分类权重数据表");
                }
            }
            else
            {
                MessageBox.Show("请先打开或者创建项目");
            }
            
        }

        private void 清空指数计算数据DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (nowProject != "")
            {
                if (MessageBox.Show(this, "该过程不可逆，确定要清空所有计算数据表吗？", "请确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2).Equals(DialogResult.Yes))
                {
                    DbUtil.truncateComputedTable();
                    MessageBox.Show("已清空计算数据表");
                }
            }
            else
            {
                MessageBox.Show("请先打开或者创建项目");
            }
            
        }



        private void 导入分类权重表IToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (nowProject != "")
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
                }
            }
            else
            {
                MessageBox.Show("请先打开或者创建项目");
            }
            
        }

        private void 导入价格采集表IToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (nowProject != "")
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.InitialDirectory = "..\\";
                openFileDialog.Filter = "Excel文件|*.xls";
                openFileDialog.RestoreDirectory = true;
                openFileDialog.FilterIndex = 1;
                openFileDialog.Multiselect = true;
                if (openFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    String[] files = openFileDialog.FileNames;
                    DateTime d1 = DateTime.Now;
                    int productrecord = 0;
                    foreach (string file in files)
                    {
                        this.mainStatusLabel.Text = "正在处理文件：" + file;
                        this.statusStrip1.Update();
                        productrecord += ImportExcel.importExcelDaily(file, DialogResult.ToString());
                    }
                    this.mainStatusLabel.Text = "导入完成";
                    this.statusStrip1.Update();
                    MessageBox.Show("本次操作共耗时 " + (new TimeSpan(DateTime.Now.Ticks) - new TimeSpan(d1.Ticks)).TotalSeconds.ToString() + "秒\n" + "本次共导入 " + productrecord + "条数据");
                }
            }
            else
            {
                MessageBox.Show("请先打开或者创建项目");
            }
            
        }

        



        //指数汇总
        private void 汇总价格采集表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (nowProject != "")
            {
                if (sort.Length == 12)
                {
                    DbUtil.totalDayPrice();
                }
            }
            else
            {
                MessageBox.Show("请先打开或者创建项目");
            }
        }
        private void 价格采集表周汇总WToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (nowProject != "")
            {
                if (sort.Length == 12 || sort.Length == 9)
                {
                    DbUtil.totalWeekPrice();
                }
            }
            else
            {
                MessageBox.Show("请先打开或者创建项目");
            }
        }
        private void 价格采集表月汇总MToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (nowProject != "")
            {
                if (sort.Length == 12 || sort.Length == 9||sort.Length==5)
                {
                    DbUtil.totalMonthPrice();
                }
            }
            else
            {
                MessageBox.Show("请先打开或者创建项目");
            }
        }

        

        private void 价格采集表日定级价格指数计算DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            //判断是否打开项目
            if (nowProject != "")
            {

                if (sort.Length == 12)
                {
                      DbUtil.computePriceIndexDay();
                }
                else
                {
                    MessageBox.Show("请先打开或者创建项目");
                }
            }
            else
            {
                MessageBox.Show("请先打开或者创建项目");
            }
        }

        private void 价格采集表周定级价格指数计算BToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //判断是否打开项目
            if (nowProject != "")
            {
                if (sort.Length == 12 || sort.Length == 9)
                {
                    DbUtil.computePriceIndexWeek();
                }
            }
            else
            {
                MessageBox.Show("请先打开或者创建项目");
            }
           
        }

        private void 价格采集表月定级价格指数计算NToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (nowProject != "")
            {
                if (sort.Length == 12 || sort.Length == 9 || sort.Length == 5)
                {
                    DbUtil.computePriceIndexMonth();
                }
            }
            else
            {
                MessageBox.Show("请先打开或者创建项目");
            }
        }

        private void 价格采集表周环比价格指数计算RToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (nowProject != "")
            {
                if (sort.Length == 12 || sort.Length == 9)
                {
                    DbUtil.computeRingPriceIndexWeek();
                }
            }
            else
            {
                MessageBox.Show("请先打开或者创建项目");
            }
           
        }

        private void 价格采集表日环比价格指数计算OToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (nowProject != "")
            {
                if (sort.Length == 12)
                {
                    DbUtil.computeRingPriceIndexDay();
                }
            }
            else
            {
                MessageBox.Show("请先打开或者创建项目");
            }
        }

        private void 价格采集表月环比价格指数计算RToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (nowProject != "")
            {
                if (sort.Length == 12 || sort.Length == 9 || sort.Length == 5)
                {
                    DbUtil.computeRingPriceIndexMonth();
                }
            }
            else
            {
                MessageBox.Show("请先打开或者创建项目");
            }
        }
        private void loadTreeViewDay()
        {
            TreeNode dirnode = new TreeNode("日价格指数");
            dirnode.Tag = "";
            dirnode.Name = "1";
            treeView1.Nodes.Add(dirnode);
            TreeNode Class_FirstElem = null;
            TreeNode Class_SecondElem = null;
            TreeNode Class_ThirdElem = null;
            string Class_First_Code = "", Class_Second_Code = "", Class_Third_Code = "", Class_Fourth_Code = "";
            string codename = "";
            for (int i = 1; i < nowClassNum; i++)
            {
                codename += "p.Class_" + i + "_Code,p.Class_" + i + "_Name,";
            }
            string sql = @"select " + codename + "p.Class_" + nowClassNum + "_Code,p.Class_" + nowClassNum + "_Name from " + nowProject + "_Product_WithFullName p";
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
                        Class_SecondElem.Nodes.Add(Class_ThirdElem);
                    }
                    Class_ThirdElem = new TreeNode(reader[5].ToString().Trim());
                    Class_ThirdElem.Tag = reader[4].ToString().Trim();
                    Class_ThirdElem.Name = "1";
                    Class_Third_Code = reader[4].ToString().Trim();
                }
                if (!reader[2].Equals(Class_Second_Code))
                {
                    //把上个子类先添加进大类，再新建一个子类
                    if (Class_SecondElem != null)
                    {
                        Class_FirstElem.Nodes.Add(Class_SecondElem);
                    }

                    Class_SecondElem = new TreeNode(reader[3].ToString().Trim());
                    Class_SecondElem.Tag = reader[2].ToString().Trim();
                    Class_SecondElem.Name = "1";
                    Class_Second_Code = reader[2].ToString().Trim();
                }

                //新的一个大类开始
                if (!reader[0].Equals(Class_First_Code))
                {
                    if (Class_FirstElem != null)
                    {
                        dirnode.Nodes.Add(Class_FirstElem);
                    }
                    Class_FirstElem = new TreeNode(reader[1].ToString().Trim());
                    Class_FirstElem.Tag = reader[0].ToString().Trim();
                    Class_FirstElem.Name = "1";
                    Class_First_Code = reader[0].ToString().Trim();
                }
            }
            if (Class_Third_Code != "" && Class_Second_Code != "")
            {
                Class_SecondElem.Nodes.Add(Class_ThirdElem);
                Class_FirstElem.Nodes.Add(Class_SecondElem);
                dirnode.Nodes.Add(Class_FirstElem);
            }
            reader.Close();
        }
        
        private void loadTreeViewWeek()
        {
            TreeNode dirnode = new TreeNode("周价格指数");
            dirnode.Tag = "";
            dirnode.Name = "2";
            treeView1.Nodes.Add(dirnode);
            TreeNode Class_FirstElem = null;
            TreeNode Class_SecondElem = null;
            TreeNode Class_ThirdElem = null;
            string Class_First_Code = "", Class_Second_Code = "", Class_Third_Code = "", Class_Fourth_Code = "";
            string codename = "";
            for (int i = 1; i < nowClassNum; i++)
            {
                codename += "p.Class_" + i + "_Code,p.Class_" + i + "_Name,";
            }
            string sql = @"select " + codename + "p.Class_" + nowClassNum + "_Code,p.Class_" + nowClassNum + "_Name from " + nowProject + "_Product_WithFullName p";
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
                        Class_SecondElem.Nodes.Add(Class_ThirdElem);
                    }
                    Class_ThirdElem = new TreeNode(reader[5].ToString().Trim());
                    Class_ThirdElem.Tag = reader[4].ToString().Trim();
                    Class_ThirdElem.Name = "2";
                    Class_Third_Code = reader[4].ToString().Trim();
                }
                if (!reader[2].Equals(Class_Second_Code))
                {
                    //把上个子类先添加进大类，再新建一个子类
                    if (Class_SecondElem != null)
                    {
                        Class_FirstElem.Nodes.Add(Class_SecondElem);
                    }

                    Class_SecondElem = new TreeNode(reader[3].ToString().Trim());
                    Class_SecondElem.Tag = reader[2].ToString().Trim();
                    Class_SecondElem.Name = "2";
                    Class_Second_Code = reader[2].ToString().Trim();
                }

                //新的一个大类开始
                if (!reader[0].Equals(Class_First_Code))
                {
                    if (Class_FirstElem != null)
                    {
                        dirnode.Nodes.Add(Class_FirstElem);
                    }
                    Class_FirstElem = new TreeNode(reader[1].ToString().Trim());
                    Class_FirstElem.Tag = reader[0].ToString().Trim();
                    Class_FirstElem.Name = "2";
                    Class_First_Code = reader[0].ToString().Trim();
                }
            }
            if (Class_Third_Code != ""&&Class_Second_Code!="")
            {
                Class_SecondElem.Nodes.Add(Class_ThirdElem);
                Class_FirstElem.Nodes.Add(Class_SecondElem);
                dirnode.Nodes.Add(Class_FirstElem);
            }
            reader.Close();
        }
        
        private void loadTreeViewMonth()
        {
            TreeNode dirnode = new TreeNode("月价格指数");
            dirnode.Tag = "";
            dirnode.Name = "3";
            treeView1.Nodes.Add(dirnode);
            TreeNode Class_FirstElem = null;
            TreeNode Class_SecondElem = null;
            TreeNode Class_ThirdElem = null;
            string Class_First_Code = "", Class_Second_Code = "", Class_Third_Code = "", Class_Fourth_Code = "";
            string codename = "";
            for (int i = 1; i < nowClassNum; i++)
            {
                codename += "p.Class_" + i + "_Code,p.Class_" + i + "_Name,";
            }
            string sql = @"select " + codename + "p.Class_" + nowClassNum + "_Code,p.Class_" + nowClassNum + "_Name from " + nowProject + "_Product_WithFullName p";
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
                        Class_SecondElem.Nodes.Add(Class_ThirdElem);
                    }
                    Class_ThirdElem = new TreeNode(reader[5].ToString().Trim());
                    Class_ThirdElem.Tag = reader[4].ToString().Trim();
                    Class_ThirdElem.Name = "3";
                    Class_Third_Code = reader[4].ToString().Trim();
                }
                if (!reader[2].Equals(Class_Second_Code))
                {
                    //把上个子类先添加进大类，再新建一个子类
                    if (Class_SecondElem != null)
                    {
                        Class_FirstElem.Nodes.Add(Class_SecondElem);
                    }

                    Class_SecondElem = new TreeNode(reader[3].ToString().Trim());
                    Class_SecondElem.Tag = reader[2].ToString().Trim();
                    Class_SecondElem.Name = "3";
                    Class_Second_Code = reader[2].ToString().Trim();
                }

                //新的一个大类开始
                if (!reader[0].Equals(Class_First_Code))
                {
                    if (Class_FirstElem != null)
                    {
                        dirnode.Nodes.Add(Class_FirstElem);
                    }
                    Class_FirstElem = new TreeNode(reader[1].ToString().Trim());
                    Class_FirstElem.Tag = reader[0].ToString().Trim();
                    Class_FirstElem.Name = "3";
                    Class_First_Code = reader[0].ToString().Trim();
                }
            }
            if (Class_Second_Code != "" && Class_Third_Code != "")
            {
                Class_SecondElem.Nodes.Add(Class_ThirdElem);
                Class_FirstElem.Nodes.Add(Class_SecondElem);
                dirnode.Nodes.Add(Class_FirstElem);
            }
            reader.Close();
        }
        
        private void loadTreeViewRingDay()
        {
            TreeNode dirnode = new TreeNode("日价格环比指数");
            dirnode.Tag = "";
            dirnode.Name = "4";
            treeView1.Nodes.Add(dirnode);
            TreeNode Class_FirstElem = null;
            TreeNode Class_SecondElem = null;
            TreeNode Class_ThirdElem = null;
            string Class_First_Code = "", Class_Second_Code = "", Class_Third_Code = "", Class_Fourth_Code = "";
            string codename = "";
            for (int i = 1; i < nowClassNum; i++)
            {
                codename += "p.Class_" + i + "_Code,p.Class_" + i + "_Name,";
            }
            string sql = @"select " + codename + "p.Class_" + nowClassNum + "_Code,p.Class_" + nowClassNum + "_Name from " + nowProject + "_Product_WithFullName p";
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
                        Class_SecondElem.Nodes.Add(Class_ThirdElem);
                    }
                    Class_ThirdElem = new TreeNode(reader[5].ToString().Trim());
                    Class_ThirdElem.Tag = reader[4].ToString().Trim();
                    Class_ThirdElem.Name = "4";
                    Class_Third_Code = reader[4].ToString().Trim();
                }
                if (!reader[2].Equals(Class_Second_Code))
                {
                    //把上个子类先添加进大类，再新建一个子类
                    if (Class_SecondElem != null)
                    {
                        Class_FirstElem.Nodes.Add(Class_SecondElem);
                    }

                    Class_SecondElem = new TreeNode(reader[3].ToString().Trim());
                    Class_SecondElem.Tag = reader[2].ToString().Trim();
                    Class_SecondElem.Name = "4";
                    Class_Second_Code = reader[2].ToString().Trim();
                }

                //新的一个大类开始
                if (!reader[0].Equals(Class_First_Code))
                {
                    if (Class_FirstElem != null)
                    {
                        dirnode.Nodes.Add(Class_FirstElem);
                    }
                    Class_FirstElem = new TreeNode(reader[1].ToString().Trim());
                    Class_FirstElem.Tag = reader[0].ToString().Trim();
                    Class_FirstElem.Name = "4";
                    Class_First_Code = reader[0].ToString().Trim();
                }
            }
            if (Class_Second_Code != "" && Class_Third_Code != "")
            {
                Class_SecondElem.Nodes.Add(Class_ThirdElem);
                Class_FirstElem.Nodes.Add(Class_SecondElem);
                dirnode.Nodes.Add(Class_FirstElem);
            }
            reader.Close();
        }
        private void loadTreeViewRingWeek()
        {
            TreeNode dirnode = new TreeNode("周价格环比指数");
            dirnode.Tag = "";
            dirnode.Name = "5";
            treeView1.Nodes.Add(dirnode);
            TreeNode Class_FirstElem = null;
            TreeNode Class_SecondElem = null;
            TreeNode Class_ThirdElem = null;
            string Class_First_Code = "", Class_Second_Code = "", Class_Third_Code = "", Class_Fourth_Code = "";
            string codename = "";
            for (int i = 1; i < nowClassNum; i++)
            {
                codename += "p.Class_" + i + "_Code,p.Class_" + i + "_Name,";
            }
            string sql = @"select " + codename + "p.Class_" + nowClassNum + "_Code,p.Class_" + nowClassNum + "_Name from " + nowProject + "_Product_WithFullName p";
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
                        Class_SecondElem.Nodes.Add(Class_ThirdElem);
                    }
                    Class_ThirdElem = new TreeNode(reader[5].ToString().Trim());
                    Class_ThirdElem.Tag = reader[4].ToString().Trim();
                    Class_ThirdElem.Name = "5";
                    Class_Third_Code = reader[4].ToString().Trim();
                }
                if (!reader[2].Equals(Class_Second_Code))
                {
                    //把上个子类先添加进大类，再新建一个子类
                    if (Class_SecondElem != null)
                    {
                        Class_FirstElem.Nodes.Add(Class_SecondElem);
                    }

                    Class_SecondElem = new TreeNode(reader[3].ToString().Trim());
                    Class_SecondElem.Tag = reader[2].ToString().Trim();
                    Class_SecondElem.Name = "5";
                    Class_Second_Code = reader[2].ToString().Trim();
                }

                //新的一个大类开始
                if (!reader[0].Equals(Class_First_Code))
                {
                    if (Class_FirstElem != null)
                    {
                        dirnode.Nodes.Add(Class_FirstElem);
                    }
                    Class_FirstElem = new TreeNode(reader[1].ToString().Trim());
                    Class_FirstElem.Tag = reader[0].ToString().Trim();
                    Class_FirstElem.Name = "5";
                    Class_First_Code = reader[0].ToString().Trim();
                }
            }
            if (Class_Second_Code != "" && Class_Third_Code != "")
            {
                Class_SecondElem.Nodes.Add(Class_ThirdElem);
                Class_FirstElem.Nodes.Add(Class_SecondElem);
                dirnode.Nodes.Add(Class_FirstElem);
            }
            reader.Close();
        }
        private void loadTreeViewRingMonth()
        {
            TreeNode dirnode = new TreeNode("月价格环比指数");
            dirnode.Tag = "";
            dirnode.Name = "6";
            treeView1.Nodes.Add(dirnode);
            TreeNode Class_FirstElem = null;
            TreeNode Class_SecondElem = null;
            TreeNode Class_ThirdElem = null;
            string Class_First_Code = "", Class_Second_Code = "", Class_Third_Code = "", Class_Fourth_Code = "";
            string codename = "";
            for (int i = 1; i < nowClassNum; i++)
            {
                codename += "p.Class_" + i + "_Code,p.Class_" + i + "_Name,";
            }
            string sql = @"select " + codename + "p.Class_" + nowClassNum + "_Code,p.Class_" + nowClassNum + "_Name from " + nowProject + "_Product_WithFullName p";
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
                        Class_SecondElem.Nodes.Add(Class_ThirdElem);
                    }
                    Class_ThirdElem = new TreeNode(reader[5].ToString().Trim());
                    Class_ThirdElem.Tag = reader[4].ToString().Trim();
                    Class_ThirdElem.Name = "6";
                    Class_Third_Code = reader[4].ToString().Trim();
                }
                if (!reader[2].Equals(Class_Second_Code))
                {
                    //把上个子类先添加进大类，再新建一个子类
                    if (Class_SecondElem != null)
                    {
                        Class_FirstElem.Nodes.Add(Class_SecondElem);
                    }

                    Class_SecondElem = new TreeNode(reader[3].ToString().Trim());
                    Class_SecondElem.Tag = reader[2].ToString().Trim();
                    Class_SecondElem.Name = "6";
                    Class_Second_Code = reader[2].ToString().Trim();
                }

                //新的一个大类开始
                if (!reader[0].Equals(Class_First_Code))
                {
                    if (Class_FirstElem != null)
                    {
                        dirnode.Nodes.Add(Class_FirstElem);
                    }
                    Class_FirstElem = new TreeNode(reader[1].ToString().Trim());
                    Class_FirstElem.Tag = reader[0].ToString().Trim();
                    Class_FirstElem.Name = "6";
                    Class_First_Code = reader[0].ToString().Trim();
                }
            }
            if (Class_Second_Code != "" && Class_Third_Code != "")
            {
                Class_SecondElem.Nodes.Add(Class_ThirdElem);
                Class_FirstElem.Nodes.Add(Class_SecondElem);
                dirnode.Nodes.Add(Class_FirstElem);
            }
            reader.Close();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            rowindex = 1;
            DataSet ds1 = DbUtil.GetDayIndexData(e.Node.Tag.ToString(), e.Node.Name.ToString());
            //MessageBox.Show(e.Node.Name.ToString());
            createdbegindate = "1900-01-01";
            int count = DbUtil.GetIndexDatacount(e.Node.Tag.ToString(),e.Node.Name.ToString());
            class_code = e.Node.Tag.ToString();
            class_name = e.Node.Name.ToString();
            type = (class_code.Length / 2).ToString();
            dataGridView1.DataSource = ds1.Tables[0];
            createPane(this.zedGraphControl1);
            int pagenum = count / 9 + (count % 9 == 0 ? 0 : 1);
            showlabel.Text = "共" + count.ToString() + "条   "+1+" /" + pagenum + "页";
        }





        /*---------价格平台--begin---------*/
        private void loadTreePriceViewDay()
        {
            TreeNode dirnode = new TreeNode("价格平均");
            dirnode.Tag = "";
            dirnode.Name = "1";
            treeView2.Nodes.Add(dirnode);
            TreeNode Class_FirstElem = null;
            TreeNode Class_SecondElem = null;
            TreeNode Class_ThirdElem = null;
            string Class_First_Code = "", Class_Second_Code = "", Class_Third_Code = "", Class_Fourth_Code = "";
            string codename = "";
            for (int i = 1; i < nowClassNum; i++)
            {
                codename += "p.Class_" + i + "_Code,p.Class_" + i + "_Name,";
            }
            string sql = @"select " + codename + "p.Class_" + nowClassNum + "_Code,p.Class_" + nowClassNum + "_Name from " + nowProject + "_Product_WithFullName p";
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
                        Class_SecondElem.Nodes.Add(Class_ThirdElem);
                    }
                    Class_ThirdElem = new TreeNode(reader[5].ToString().Trim());
                    Class_ThirdElem.Tag = reader[4].ToString().Trim();
                    Class_ThirdElem.Name = "1";
                    Class_Third_Code = reader[4].ToString().Trim();
                }
                if (!reader[2].Equals(Class_Second_Code))
                {
                    //把上个子类先添加进大类，再新建一个子类
                    if (Class_SecondElem != null)
                    {
                        Class_FirstElem.Nodes.Add(Class_SecondElem);
                    }

                    Class_SecondElem = new TreeNode(reader[3].ToString().Trim());
                    Class_SecondElem.Tag = reader[2].ToString().Trim();
                    Class_SecondElem.Name = "1";
                    Class_Second_Code = reader[2].ToString().Trim();
                }

                //新的一个大类开始
                if (!reader[0].Equals(Class_First_Code))
                {
                    if (Class_FirstElem != null)
                    {
                        dirnode.Nodes.Add(Class_FirstElem);
                    }
                    Class_FirstElem = new TreeNode(reader[1].ToString().Trim());
                    Class_FirstElem.Tag = reader[0].ToString().Trim();
                    Class_FirstElem.Name = "1";
                    Class_First_Code = reader[0].ToString().Trim();
                }
            }
            if (Class_Third_Code != "" && Class_Second_Code != "")
            {
                Class_SecondElem.Nodes.Add(Class_ThirdElem);
                Class_FirstElem.Nodes.Add(Class_SecondElem);
                dirnode.Nodes.Add(Class_FirstElem);
            }
            reader.Close();
        }
        private void loadTreePriceViewWeek()
        {
            TreeNode dirnode = new TreeNode("周价格平均");
            dirnode.Tag = "";
            dirnode.Name = "2";
            treeView2.Nodes.Add(dirnode);
            TreeNode Class_FirstElem = null;
            TreeNode Class_SecondElem = null;
            TreeNode Class_ThirdElem = null;
            string Class_First_Code = "", Class_Second_Code = "", Class_Third_Code = "", Class_Fourth_Code = "";
            string codename = "";
            for (int i = 1; i < nowClassNum; i++)
            {
                codename += "p.Class_" + i + "_Code,p.Class_" + i + "_Name,";
            }
            string sql = @"select " + codename + "p.Class_" + nowClassNum + "_Code,p.Class_" + nowClassNum + "_Name from " + nowProject + "_Product_WithFullName p";
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
                        Class_SecondElem.Nodes.Add(Class_ThirdElem);
                    }
                    Class_ThirdElem = new TreeNode(reader[5].ToString().Trim());
                    Class_ThirdElem.Tag = reader[4].ToString().Trim();
                    Class_ThirdElem.Name = "2";
                    Class_Third_Code = reader[4].ToString().Trim();
                }
                if (!reader[2].Equals(Class_Second_Code))
                {
                    //把上个子类先添加进大类，再新建一个子类
                    if (Class_SecondElem != null)
                    {
                        Class_FirstElem.Nodes.Add(Class_SecondElem);
                    }

                    Class_SecondElem = new TreeNode(reader[3].ToString().Trim());
                    Class_SecondElem.Tag = reader[2].ToString().Trim();
                    Class_SecondElem.Name = "2";
                    Class_Second_Code = reader[2].ToString().Trim();
                }

                //新的一个大类开始
                if (!reader[0].Equals(Class_First_Code))
                {
                    if (Class_FirstElem != null)
                    {
                        dirnode.Nodes.Add(Class_FirstElem);
                    }
                    Class_FirstElem = new TreeNode(reader[1].ToString().Trim());
                    Class_FirstElem.Tag = reader[0].ToString().Trim();
                    Class_FirstElem.Name = "2";
                    Class_First_Code = reader[0].ToString().Trim();
                }
            }
            if (Class_Third_Code != "" && Class_Second_Code != "")
            {
                Class_SecondElem.Nodes.Add(Class_ThirdElem);
                Class_FirstElem.Nodes.Add(Class_SecondElem);
                dirnode.Nodes.Add(Class_FirstElem);
            }
            reader.Close();
        }
        private void loadTreePriceViewMonth()
        {
            TreeNode dirnode = new TreeNode("月价格平均");
            dirnode.Tag = "";
            dirnode.Name = "3";
            treeView2.Nodes.Add(dirnode);
            TreeNode Class_FirstElem = null;
            TreeNode Class_SecondElem = null;
            TreeNode Class_ThirdElem = null;
            string Class_First_Code = "", Class_Second_Code = "", Class_Third_Code = "", Class_Fourth_Code = "";
            string codename = "";
            for (int i = 1; i < nowClassNum; i++)
            {
                codename += "p.Class_" + i + "_Code,p.Class_" + i + "_Name,";
            }
            string sql = @"select " + codename + "p.Class_" + nowClassNum + "_Code,p.Class_" + nowClassNum + "_Name from " + nowProject + "_Product_WithFullName p";
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
                        Class_SecondElem.Nodes.Add(Class_ThirdElem);
                    }
                    Class_ThirdElem = new TreeNode(reader[5].ToString().Trim());
                    Class_ThirdElem.Tag = reader[4].ToString().Trim();
                    Class_ThirdElem.Name = "3";
                    Class_Third_Code = reader[4].ToString().Trim();
                }
                if (!reader[2].Equals(Class_Second_Code))
                {
                    //把上个子类先添加进大类，再新建一个子类
                    if (Class_SecondElem != null)
                    {
                        Class_FirstElem.Nodes.Add(Class_SecondElem);
                    }

                    Class_SecondElem = new TreeNode(reader[3].ToString().Trim());
                    Class_SecondElem.Tag = reader[2].ToString().Trim();
                    Class_SecondElem.Name = "3";
                    Class_Second_Code = reader[2].ToString().Trim();
                }

                //新的一个大类开始
                if (!reader[0].Equals(Class_First_Code))
                {
                    if (Class_FirstElem != null)
                    {
                        dirnode.Nodes.Add(Class_FirstElem);
                    }
                    Class_FirstElem = new TreeNode(reader[1].ToString().Trim());
                    Class_FirstElem.Tag = reader[0].ToString().Trim();
                    Class_FirstElem.Name = "3";
                    Class_First_Code = reader[0].ToString().Trim();
                }
            }
            if (Class_Second_Code != "" && Class_Third_Code != "")
            {
                Class_SecondElem.Nodes.Add(Class_ThirdElem);
                Class_FirstElem.Nodes.Add(Class_SecondElem);
                dirnode.Nodes.Add(Class_FirstElem);
            }
            reader.Close();
        }
        private void treeView2_AfterSelect(object sender, TreeViewEventArgs e)
        {
            rowindex = 1;
            DataSet ds1 = DbUtil.GetDayPriceData(e.Node.Tag.ToString(), e.Node.Name.ToString());
            //MessageBox.Show(e.Node.Name.ToString());
            createdbegindate = "1900-01-01";
            int count = DbUtil.GetPriceDatacount(e.Node.Tag.ToString(), e.Node.Name.ToString());
            class_code = e.Node.Tag.ToString();
            class_name = e.Node.Name.ToString();
            type = (class_code.Length / 2).ToString();
            dataGridView2.DataSource = ds1.Tables[0];
            createPane(this.zedGraphControl2);
            pagenum = count / 9 + (count % 9 == 0 ? 0 : 1);
            label1.Text = "共" + count.ToString() + "条   " + 1 + " /" + pagenum + "页";
        }
        /*---------价格平台--end---------*/
        private void 清空指数汇总数据EToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (nowProject != "")
            {
                if (MessageBox.Show(this, "该过程不可逆，确定要清空所有汇总数据表吗？", "请确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2).Equals(DialogResult.Yes))
                {
                    DbUtil.truncateEtlTable();
                    MessageBox.Show("已清空汇总数据表");
                }
            }
            else
            {
                MessageBox.Show("请先打开或者创建项目");
            }
               
        }

        private void 清空指数采集数据CToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (nowProject != "")
            {
                if (MessageBox.Show(this, "该过程不可逆，确定要所有清空采集数据表吗？", "请确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2).Equals(DialogResult.Yes))
                {
                    DbUtil.truncatePickTable();
                    MessageBox.Show("已清空采集数据表");
                }
            }
            else
            {
                MessageBox.Show("请先打开或者创建项目");
            }
            
        }

        

        
    }
}
