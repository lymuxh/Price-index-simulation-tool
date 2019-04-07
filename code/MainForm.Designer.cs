using ZI.Utils;
namespace ZI
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.mainStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.项目NToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新建项目NToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开项目OToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除项目DToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出系统QToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.基础数据维护IToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导入分类权重表IToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清空分类权重表WToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清空指数计算数据DToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清空指数汇总数据EToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清空指数采集数据CToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.价格指数CToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导入价格采集表IToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.汇总价格采集表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.价格采集表周汇总WToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.价格采集表月汇总MToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.价格采集表日定级价格指数计算DToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.价格采集表周定级价格指数计算BToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.价格采集表月定级价格指数计算NToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.价格采集表日环比价格指数计算OToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.价格采集表周环比价格指数计算RToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.价格采集表月环比价格指数计算RToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助HToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.showlabel = new System.Windows.Forms.Label();
            this.toDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.fromDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.AliasLabel1 = new System.Windows.Forms.Label();
            this.projectLabel1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.panel = new System.Windows.Forms.Panel();
            this.aliaslabel = new System.Windows.Forms.Label();
            this.projectlabel = new System.Windows.Forms.Label();
            this.helpLabel = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.zedGraphControl2 = new ZedGraph.ZedGraphControl();
            this.treeView2 = new System.Windows.Forms.TreeView();
            this.statusStrip1.SuspendLayout();
            this.menuStripMain.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 635);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(908, 22);
            this.statusStrip1.TabIndex = 18;
            // 
            // mainStatusLabel
            // 
            this.mainStatusLabel.Name = "mainStatusLabel";
            this.mainStatusLabel.Size = new System.Drawing.Size(32, 17);
            this.mainStatusLabel.Text = "就绪";
            // 
            // menuStripMain
            // 
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.项目NToolStripMenuItem,
            this.基础数据维护IToolStripMenuItem,
            this.价格指数CToolStripMenuItem,
            this.帮助HToolStripMenuItem});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(908, 25);
            this.menuStripMain.TabIndex = 15;
            this.menuStripMain.Text = "menuStripMain";
            // 
            // 项目NToolStripMenuItem
            // 
            this.项目NToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新建项目NToolStripMenuItem,
            this.打开项目OToolStripMenuItem,
            this.删除项目DToolStripMenuItem,
            this.退出系统QToolStripMenuItem});
            this.项目NToolStripMenuItem.Name = "项目NToolStripMenuItem";
            this.项目NToolStripMenuItem.Size = new System.Drawing.Size(59, 21);
            this.项目NToolStripMenuItem.Text = "项目(&P)";
            // 
            // 新建项目NToolStripMenuItem
            // 
            this.新建项目NToolStripMenuItem.Name = "新建项目NToolStripMenuItem";
            this.新建项目NToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.新建项目NToolStripMenuItem.Text = "新建项目(&N)";
            this.新建项目NToolStripMenuItem.Click += new System.EventHandler(this.新建项目NToolStripMenuItem_Click);
            // 
            // 打开项目OToolStripMenuItem
            // 
            this.打开项目OToolStripMenuItem.Name = "打开项目OToolStripMenuItem";
            this.打开项目OToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.打开项目OToolStripMenuItem.Text = "打开项目(&O)";
            this.打开项目OToolStripMenuItem.Click += new System.EventHandler(this.打开项目OToolStripMenuItem_Click);
            // 
            // 删除项目DToolStripMenuItem
            // 
            this.删除项目DToolStripMenuItem.Name = "删除项目DToolStripMenuItem";
            this.删除项目DToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.删除项目DToolStripMenuItem.Text = "删除项目(&D)";
            this.删除项目DToolStripMenuItem.Click += new System.EventHandler(this.删除项目DToolStripMenuItem_Click);
            // 
            // 退出系统QToolStripMenuItem
            // 
            this.退出系统QToolStripMenuItem.Name = "退出系统QToolStripMenuItem";
            this.退出系统QToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.退出系统QToolStripMenuItem.Text = "退出系统(&Q)";
            this.退出系统QToolStripMenuItem.Click += new System.EventHandler(this.退出系统QToolStripMenuItem_Click);
            // 
            // 基础数据维护IToolStripMenuItem
            // 
            this.基础数据维护IToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.导入分类权重表IToolStripMenuItem,
            this.清空分类权重表WToolStripMenuItem,
            this.清空指数计算数据DToolStripMenuItem,
            this.清空指数汇总数据EToolStripMenuItem,
            this.清空指数采集数据CToolStripMenuItem});
            this.基础数据维护IToolStripMenuItem.Name = "基础数据维护IToolStripMenuItem";
            this.基础数据维护IToolStripMenuItem.Size = new System.Drawing.Size(112, 21);
            this.基础数据维护IToolStripMenuItem.Text = "基础数据维护(&M)";
            // 
            // 导入分类权重表IToolStripMenuItem
            // 
            this.导入分类权重表IToolStripMenuItem.Name = "导入分类权重表IToolStripMenuItem";
            this.导入分类权重表IToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.导入分类权重表IToolStripMenuItem.Text = "导入分类权重表(&I)";
            this.导入分类权重表IToolStripMenuItem.Click += new System.EventHandler(this.导入分类权重表IToolStripMenuItem_Click);
            // 
            // 清空分类权重表WToolStripMenuItem
            // 
            this.清空分类权重表WToolStripMenuItem.Name = "清空分类权重表WToolStripMenuItem";
            this.清空分类权重表WToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.清空分类权重表WToolStripMenuItem.Text = "清空分类权重表(&W)";
            this.清空分类权重表WToolStripMenuItem.Click += new System.EventHandler(this.清空分类权重表WToolStripMenuItem_Click);
            // 
            // 清空指数计算数据DToolStripMenuItem
            // 
            this.清空指数计算数据DToolStripMenuItem.Name = "清空指数计算数据DToolStripMenuItem";
            this.清空指数计算数据DToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.清空指数计算数据DToolStripMenuItem.Text = "清空指数计算数据(&D)";
            this.清空指数计算数据DToolStripMenuItem.Click += new System.EventHandler(this.清空指数计算数据DToolStripMenuItem_Click);
            // 
            // 清空指数汇总数据EToolStripMenuItem
            // 
            this.清空指数汇总数据EToolStripMenuItem.Name = "清空指数汇总数据EToolStripMenuItem";
            this.清空指数汇总数据EToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.清空指数汇总数据EToolStripMenuItem.Text = "清空指数汇总数据(&E)";
            this.清空指数汇总数据EToolStripMenuItem.Click += new System.EventHandler(this.清空指数汇总数据EToolStripMenuItem_Click);
            // 
            // 清空指数采集数据CToolStripMenuItem
            // 
            this.清空指数采集数据CToolStripMenuItem.Name = "清空指数采集数据CToolStripMenuItem";
            this.清空指数采集数据CToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.清空指数采集数据CToolStripMenuItem.Text = "清空指数采集数据(&C)";
            this.清空指数采集数据CToolStripMenuItem.Click += new System.EventHandler(this.清空指数采集数据CToolStripMenuItem_Click);
            // 
            // 价格指数CToolStripMenuItem
            // 
            this.价格指数CToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.导入价格采集表IToolStripMenuItem,
            this.汇总价格采集表ToolStripMenuItem,
            this.价格采集表周汇总WToolStripMenuItem,
            this.价格采集表月汇总MToolStripMenuItem,
            this.价格采集表日定级价格指数计算DToolStripMenuItem,
            this.价格采集表周定级价格指数计算BToolStripMenuItem,
            this.价格采集表月定级价格指数计算NToolStripMenuItem,
            this.价格采集表日环比价格指数计算OToolStripMenuItem,
            this.价格采集表周环比价格指数计算RToolStripMenuItem,
            this.价格采集表月环比价格指数计算RToolStripMenuItem});
            this.价格指数CToolStripMenuItem.Name = "价格指数CToolStripMenuItem";
            this.价格指数CToolStripMenuItem.Size = new System.Drawing.Size(84, 21);
            this.价格指数CToolStripMenuItem.Text = "价格指数(&C)";
            // 
            // 导入价格采集表IToolStripMenuItem
            // 
            this.导入价格采集表IToolStripMenuItem.Name = "导入价格采集表IToolStripMenuItem";
            this.导入价格采集表IToolStripMenuItem.Size = new System.Drawing.Size(262, 22);
            this.导入价格采集表IToolStripMenuItem.Text = "导入价格采集表(&I)";
            this.导入价格采集表IToolStripMenuItem.Click += new System.EventHandler(this.导入价格采集表IToolStripMenuItem_Click);
            // 
            // 汇总价格采集表ToolStripMenuItem
            // 
            this.汇总价格采集表ToolStripMenuItem.Name = "汇总价格采集表ToolStripMenuItem";
            this.汇总价格采集表ToolStripMenuItem.Size = new System.Drawing.Size(262, 22);
            this.汇总价格采集表ToolStripMenuItem.Text = "价格采集表日汇总(&A)";
            this.汇总价格采集表ToolStripMenuItem.Click += new System.EventHandler(this.汇总价格采集表ToolStripMenuItem_Click);
            // 
            // 价格采集表周汇总WToolStripMenuItem
            // 
            this.价格采集表周汇总WToolStripMenuItem.Name = "价格采集表周汇总WToolStripMenuItem";
            this.价格采集表周汇总WToolStripMenuItem.Size = new System.Drawing.Size(262, 22);
            this.价格采集表周汇总WToolStripMenuItem.Text = "价格采集表周汇总(&W)";
            this.价格采集表周汇总WToolStripMenuItem.Click += new System.EventHandler(this.价格采集表周汇总WToolStripMenuItem_Click);
            // 
            // 价格采集表月汇总MToolStripMenuItem
            // 
            this.价格采集表月汇总MToolStripMenuItem.Name = "价格采集表月汇总MToolStripMenuItem";
            this.价格采集表月汇总MToolStripMenuItem.Size = new System.Drawing.Size(262, 22);
            this.价格采集表月汇总MToolStripMenuItem.Text = "价格采集表月汇总(&M)";
            this.价格采集表月汇总MToolStripMenuItem.Click += new System.EventHandler(this.价格采集表月汇总MToolStripMenuItem_Click);
            // 
            // 价格采集表日定级价格指数计算DToolStripMenuItem
            // 
            this.价格采集表日定级价格指数计算DToolStripMenuItem.Name = "价格采集表日定级价格指数计算DToolStripMenuItem";
            this.价格采集表日定级价格指数计算DToolStripMenuItem.Size = new System.Drawing.Size(262, 22);
            this.价格采集表日定级价格指数计算DToolStripMenuItem.Text = "价格采集表日定级价格指数计算(&D)";
            this.价格采集表日定级价格指数计算DToolStripMenuItem.Click += new System.EventHandler(this.价格采集表日定级价格指数计算DToolStripMenuItem_Click);
            // 
            // 价格采集表周定级价格指数计算BToolStripMenuItem
            // 
            this.价格采集表周定级价格指数计算BToolStripMenuItem.Name = "价格采集表周定级价格指数计算BToolStripMenuItem";
            this.价格采集表周定级价格指数计算BToolStripMenuItem.Size = new System.Drawing.Size(262, 22);
            this.价格采集表周定级价格指数计算BToolStripMenuItem.Text = "价格采集表周定级价格指数计算(&B)";
            this.价格采集表周定级价格指数计算BToolStripMenuItem.Click += new System.EventHandler(this.价格采集表周定级价格指数计算BToolStripMenuItem_Click);
            // 
            // 价格采集表月定级价格指数计算NToolStripMenuItem
            // 
            this.价格采集表月定级价格指数计算NToolStripMenuItem.Name = "价格采集表月定级价格指数计算NToolStripMenuItem";
            this.价格采集表月定级价格指数计算NToolStripMenuItem.Size = new System.Drawing.Size(262, 22);
            this.价格采集表月定级价格指数计算NToolStripMenuItem.Text = "价格采集表月定级价格指数计算(&N)";
            this.价格采集表月定级价格指数计算NToolStripMenuItem.Click += new System.EventHandler(this.价格采集表月定级价格指数计算NToolStripMenuItem_Click);
            // 
            // 价格采集表日环比价格指数计算OToolStripMenuItem
            // 
            this.价格采集表日环比价格指数计算OToolStripMenuItem.Name = "价格采集表日环比价格指数计算OToolStripMenuItem";
            this.价格采集表日环比价格指数计算OToolStripMenuItem.Size = new System.Drawing.Size(262, 22);
            this.价格采集表日环比价格指数计算OToolStripMenuItem.Text = "价格采集表日环比价格指数计算(&O)";
            this.价格采集表日环比价格指数计算OToolStripMenuItem.Click += new System.EventHandler(this.价格采集表日环比价格指数计算OToolStripMenuItem_Click);
            // 
            // 价格采集表周环比价格指数计算RToolStripMenuItem
            // 
            this.价格采集表周环比价格指数计算RToolStripMenuItem.Name = "价格采集表周环比价格指数计算RToolStripMenuItem";
            this.价格采集表周环比价格指数计算RToolStripMenuItem.Size = new System.Drawing.Size(262, 22);
            this.价格采集表周环比价格指数计算RToolStripMenuItem.Text = "价格采集表周环比价格指数计算(&R)";
            this.价格采集表周环比价格指数计算RToolStripMenuItem.Click += new System.EventHandler(this.价格采集表周环比价格指数计算RToolStripMenuItem_Click);
            // 
            // 价格采集表月环比价格指数计算RToolStripMenuItem
            // 
            this.价格采集表月环比价格指数计算RToolStripMenuItem.Name = "价格采集表月环比价格指数计算RToolStripMenuItem";
            this.价格采集表月环比价格指数计算RToolStripMenuItem.Size = new System.Drawing.Size(262, 22);
            this.价格采集表月环比价格指数计算RToolStripMenuItem.Text = "价格采集表月环比价格指数计算(&R)";
            this.价格采集表月环比价格指数计算RToolStripMenuItem.Click += new System.EventHandler(this.价格采集表月环比价格指数计算RToolStripMenuItem_Click);
            // 
            // 帮助HToolStripMenuItem
            // 
            this.帮助HToolStripMenuItem.Name = "帮助HToolStripMenuItem";
            this.帮助HToolStripMenuItem.Size = new System.Drawing.Size(61, 21);
            this.帮助HToolStripMenuItem.Text = "帮助(&H)";
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::ZI.Properties.Resources.P21;
            this.panel1.Controls.Add(this.showlabel);
            this.panel1.Controls.Add(this.toDateTimePicker);
            this.panel1.Controls.Add(this.fromDateTimePicker);
            this.panel1.Controls.Add(this.AliasLabel1);
            this.panel1.Controls.Add(this.projectLabel1);
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Controls.Add(this.zedGraphControl1);
            this.panel1.Controls.Add(this.treeView1);
            this.panel1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(905, 609);
            this.panel1.TabIndex = 1;
            this.panel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseClick);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            // 
            // showlabel
            // 
            this.showlabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.showlabel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.showlabel.Location = new System.Drawing.Point(459, 330);
            this.showlabel.Name = "showlabel";
            this.showlabel.Size = new System.Drawing.Size(137, 17);
            this.showlabel.TabIndex = 7;
            this.showlabel.Text = "共10条,1/2页";
            this.showlabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // toDateTimePicker
            // 
            this.toDateTimePicker.CalendarFont = new System.Drawing.Font("Arial Narrow", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toDateTimePicker.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.toDateTimePicker.Location = new System.Drawing.Point(328, 329);
            this.toDateTimePicker.Name = "toDateTimePicker";
            this.toDateTimePicker.Size = new System.Drawing.Size(78, 19);
            this.toDateTimePicker.TabIndex = 6;
            // 
            // fromDateTimePicker
            // 
            this.fromDateTimePicker.CalendarFont = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fromDateTimePicker.CustomFormat = "";
            this.fromDateTimePicker.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fromDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.fromDateTimePicker.Location = new System.Drawing.Point(228, 329);
            this.fromDateTimePicker.Name = "fromDateTimePicker";
            this.fromDateTimePicker.Size = new System.Drawing.Size(78, 19);
            this.fromDateTimePicker.TabIndex = 5;
            this.fromDateTimePicker.Value = new System.DateTime(2012, 7, 29, 0, 0, 0, 0);
            // 
            // AliasLabel1
            // 
            this.AliasLabel1.BackColor = System.Drawing.Color.Transparent;
            this.AliasLabel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.AliasLabel1.ForeColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.AliasLabel1.Location = new System.Drawing.Point(112, 27);
            this.AliasLabel1.Name = "AliasLabel1";
            this.AliasLabel1.Size = new System.Drawing.Size(400, 14);
            this.AliasLabel1.TabIndex = 4;
            // 
            // projectLabel1
            // 
            this.projectLabel1.BackColor = System.Drawing.Color.Transparent;
            this.projectLabel1.Font = new System.Drawing.Font("微软雅黑", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.projectLabel1.ForeColor = System.Drawing.Color.White;
            this.projectLabel1.Location = new System.Drawing.Point(110, 6);
            this.projectLabel1.Name = "projectLabel1";
            this.projectLabel1.Size = new System.Drawing.Size(400, 21);
            this.projectLabel1.TabIndex = 3;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(221, 356);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridView1.Size = new System.Drawing.Size(660, 230);
            this.dataGridView1.TabIndex = 2;
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.zedGraphControl1.ForeColor = System.Drawing.SystemColors.Control;
            this.zedGraphControl1.Location = new System.Drawing.Point(221, 65);
            this.zedGraphControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0D;
            this.zedGraphControl1.ScrollMaxX = 0D;
            this.zedGraphControl1.ScrollMaxY = 0D;
            this.zedGraphControl1.ScrollMaxY2 = 0D;
            this.zedGraphControl1.ScrollMinX = 0D;
            this.zedGraphControl1.ScrollMinY = 0D;
            this.zedGraphControl1.ScrollMinY2 = 0D;
            this.zedGraphControl1.Size = new System.Drawing.Size(660, 256);
            this.zedGraphControl1.TabIndex = 1;
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(23, 65);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(180, 521);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // panel
            // 
            this.panel.BackgroundImage = global::ZI.Properties.Resources.P11;
            this.panel.Controls.Add(this.panel1);
            this.panel.Controls.Add(this.aliaslabel);
            this.panel.Controls.Add(this.projectlabel);
            this.panel.Controls.Add(this.helpLabel);
            this.panel.Location = new System.Drawing.Point(1, 25);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(905, 609);
            this.panel.TabIndex = 17;
            this.panel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel_MouseClick);
            this.panel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel_MouseMove);
            // 
            // aliaslabel
            // 
            this.aliaslabel.BackColor = System.Drawing.Color.Transparent;
            this.aliaslabel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.aliaslabel.ForeColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.aliaslabel.Location = new System.Drawing.Point(112, 27);
            this.aliaslabel.Name = "aliaslabel";
            this.aliaslabel.Size = new System.Drawing.Size(400, 14);
            this.aliaslabel.TabIndex = 4;
            // 
            // projectlabel
            // 
            this.projectlabel.BackColor = System.Drawing.Color.Transparent;
            this.projectlabel.Font = new System.Drawing.Font("微软雅黑", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.projectlabel.ForeColor = System.Drawing.Color.White;
            this.projectlabel.Location = new System.Drawing.Point(110, 6);
            this.projectlabel.Name = "projectlabel";
            this.projectlabel.Size = new System.Drawing.Size(400, 21);
            this.projectlabel.TabIndex = 3;
            // 
            // helpLabel
            // 
            this.helpLabel.BackColor = System.Drawing.Color.Transparent;
            this.helpLabel.Location = new System.Drawing.Point(20, 549);
            this.helpLabel.Name = "helpLabel";
            this.helpLabel.Size = new System.Drawing.Size(862, 38);
            this.helpLabel.TabIndex = 0;
            this.helpLabel.Text = "helpLabel";
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = global::ZI.Properties.Resources.P3;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.dateTimePicker1);
            this.panel2.Controls.Add(this.dateTimePicker2);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.dataGridView2);
            this.panel2.Controls.Add(this.zedGraphControl2);
            this.panel2.Controls.Add(this.treeView2);
            this.panel2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panel2.Location = new System.Drawing.Point(0, 25);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(905, 609);
            this.panel2.TabIndex = 8;
            this.panel2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel2_MouseClick);
            this.panel2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel2_MouseMove);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(459, 330);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 17);
            this.label1.TabIndex = 7;
            this.label1.Text = "共10条,1/2页";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CalendarFont = new System.Drawing.Font("Arial Narrow", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(328, 329);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(78, 19);
            this.dateTimePicker1.TabIndex = 6;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.CalendarFont = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker2.CustomFormat = "";
            this.dateTimePicker2.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker2.Location = new System.Drawing.Point(228, 329);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(78, 19);
            this.dateTimePicker2.TabIndex = 5;
            this.dateTimePicker2.Value = new System.DateTime(2012, 7, 29, 0, 0, 0, 0);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.label2.Location = new System.Drawing.Point(112, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(400, 14);
            this.label2.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(110, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(400, 21);
            this.label3.TabIndex = 3;
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(221, 356);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowTemplate.Height = 23;
            this.dataGridView2.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridView2.Size = new System.Drawing.Size(660, 230);
            this.dataGridView2.TabIndex = 2;
            // 
            // zedGraphControl2
            // 
            this.zedGraphControl2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.zedGraphControl2.ForeColor = System.Drawing.SystemColors.Control;
            this.zedGraphControl2.Location = new System.Drawing.Point(221, 65);
            this.zedGraphControl2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.zedGraphControl2.Name = "zedGraphControl2";
            this.zedGraphControl2.ScrollGrace = 0D;
            this.zedGraphControl2.ScrollMaxX = 0D;
            this.zedGraphControl2.ScrollMaxY = 0D;
            this.zedGraphControl2.ScrollMaxY2 = 0D;
            this.zedGraphControl2.ScrollMinX = 0D;
            this.zedGraphControl2.ScrollMinY = 0D;
            this.zedGraphControl2.ScrollMinY2 = 0D;
            this.zedGraphControl2.Size = new System.Drawing.Size(660, 256);
            this.zedGraphControl2.TabIndex = 1;
            // 
            // treeView2
            // 
            this.treeView2.Location = new System.Drawing.Point(23, 65);
            this.treeView2.Name = "treeView2";
            this.treeView2.Size = new System.Drawing.Size(180, 521);
            this.treeView2.TabIndex = 0;
            this.treeView2.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView2_AfterSelect);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(908, 657);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.menuStripMain);
            this.Controls.Add(this.statusStrip1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "统计指数实验系统";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

       
        private System.Windows.Forms.StatusStrip statusStrip1;   
        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripStatusLabel mainStatusLabel;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Label helpLabel;
        private System.Windows.Forms.ToolStripMenuItem 项目NToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新建项目NToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开项目OToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除项目DToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出系统QToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 基础数据维护IToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导入分类权重表IToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 价格指数CToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 帮助HToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 清空分类权重表WToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 清空指数计算数据DToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 清空指数汇总数据EToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 清空指数采集数据CToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导入价格采集表IToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 汇总价格采集表ToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private ZedGraph.ZedGraphControl zedGraphControl1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ToolStripMenuItem 价格采集表周汇总WToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 价格采集表月汇总MToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 价格采集表日定级价格指数计算DToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 价格采集表周定级价格指数计算BToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 价格采集表月定级价格指数计算NToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 价格采集表日环比价格指数计算OToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 价格采集表周环比价格指数计算RToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 价格采集表月环比价格指数计算RToolStripMenuItem;
        private System.Windows.Forms.Label aliaslabel;
        private System.Windows.Forms.Label projectlabel;
        private System.Windows.Forms.Label AliasLabel1;
        private System.Windows.Forms.Label projectLabel1;
        private System.Windows.Forms.DateTimePicker toDateTimePicker;
        private System.Windows.Forms.Label showlabel;
        private System.Windows.Forms.DateTimePicker fromDateTimePicker;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dataGridView2;
        private ZedGraph.ZedGraphControl zedGraphControl2;
        private System.Windows.Forms.TreeView treeView2;
    }
}

