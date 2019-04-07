namespace ZI
{
    partial class Test
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("单肩包");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("手提包");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("斜挎包");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("单肩斜挎两用包");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("手提斜挎两用包");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("双肩背包");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("坤包");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("化妆包");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("钱夹");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("钥匙包");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("时尚女包", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6,
            treeNode7,
            treeNode8,
            treeNode9,
            treeNode10});
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("时尚男包");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("时尚男鞋");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("时尚女鞋");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("女装腰带");
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("男装腰带");
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("总指数", new System.Windows.Forms.TreeNode[] {
            treeNode11,
            treeNode12,
            treeNode13,
            treeNode14,
            treeNode15,
            treeNode16});
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.treeView1 = new System.Windows.Forms.TreeView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.Location = new System.Drawing.Point(187, 1);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0D;
            this.zedGraphControl1.ScrollMaxX = 0D;
            this.zedGraphControl1.ScrollMaxY = 0D;
            this.zedGraphControl1.ScrollMaxY2 = 0D;
            this.zedGraphControl1.ScrollMinX = 0D;
            this.zedGraphControl1.ScrollMinY = 0D;
            this.zedGraphControl1.ScrollMinY2 = 0D;
            this.zedGraphControl1.Size = new System.Drawing.Size(542, 239);
            this.zedGraphControl1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(774, 54);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.dataGridView1.Location = new System.Drawing.Point(187, 264);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(542, 213);
            this.dataGridView1.TabIndex = 2;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "日期";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "价格指数";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "价格涨跌值";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "价格涨跌幅";
            this.Column4.Name = "Column4";
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(12, 13);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "节点8";
            treeNode1.Text = "单肩包";
            treeNode2.Name = "节点9";
            treeNode2.Text = "手提包";
            treeNode3.Name = "节点10";
            treeNode3.Text = "斜挎包";
            treeNode4.Name = "节点11";
            treeNode4.Text = "单肩斜挎两用包";
            treeNode5.Name = "节点12";
            treeNode5.Text = "手提斜挎两用包";
            treeNode6.Name = "节点13";
            treeNode6.Text = "双肩背包";
            treeNode7.Name = "节点14";
            treeNode7.Text = "坤包";
            treeNode8.Name = "节点15";
            treeNode8.Text = "化妆包";
            treeNode9.Name = "节点16";
            treeNode9.Text = "钱夹";
            treeNode10.Name = "节点17";
            treeNode10.Text = "钥匙包";
            treeNode11.Name = "节点1";
            treeNode11.Text = "时尚女包";
            treeNode12.Name = "节点2";
            treeNode12.Text = "时尚男包";
            treeNode13.Name = "节点3";
            treeNode13.Text = "时尚男鞋";
            treeNode14.Name = "节点4";
            treeNode14.Text = "时尚女鞋";
            treeNode15.Name = "节点5";
            treeNode15.Text = "女装腰带";
            treeNode16.Name = "节点7";
            treeNode16.Text = "男装腰带";
            treeNode17.Name = "节点0";
            treeNode17.Text = "总指数";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode17});
            this.treeView1.Size = new System.Drawing.Size(156, 470);
            this.treeView1.TabIndex = 3;
            // 
            // Test
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1121, 495);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.zedGraphControl1);
            this.Name = "Test";
            this.Text = "Test";
            this.Load += new System.EventHandler(this.Test_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ZedGraph.ZedGraphControl zedGraphControl1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.TreeView treeView1;

    }
}