namespace ZI
{
    partial class OptionForm
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("数据库设置");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("数据源", new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.optionTreeView = new System.Windows.Forms.TreeView();
            this.dataGroupBox = new System.Windows.Forms.GroupBox();
            this.testDbButton = new System.Windows.Forms.Button();
            this.dbPwdTextBox = new System.Windows.Forms.TextBox();
            this.dbUserTextBox = new System.Windows.Forms.TextBox();
            this.dbIpTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.bottomSplitContainer = new System.Windows.Forms.SplitContainer();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.dataGroupBox.SuspendLayout();
            this.bottomSplitContainer.Panel1.SuspendLayout();
            this.bottomSplitContainer.Panel2.SuspendLayout();
            this.bottomSplitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.optionTreeView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGroupBox);
            this.splitContainer1.Size = new System.Drawing.Size(394, 245);
            this.splitContainer1.SplitterDistance = 131;
            this.splitContainer1.TabIndex = 0;
            // 
            // optionTreeView
            // 
            this.optionTreeView.Location = new System.Drawing.Point(4, 4);
            this.optionTreeView.Name = "optionTreeView";
            treeNode1.Name = "节点1";
            treeNode1.Text = "数据库设置";
            treeNode2.Name = "data";
            treeNode2.Text = "数据源";
            this.optionTreeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2});
            this.optionTreeView.Size = new System.Drawing.Size(124, 240);
            this.optionTreeView.TabIndex = 0;
            // 
            // dataGroupBox
            // 
            this.dataGroupBox.Controls.Add(this.testDbButton);
            this.dataGroupBox.Controls.Add(this.dbPwdTextBox);
            this.dataGroupBox.Controls.Add(this.dbUserTextBox);
            this.dataGroupBox.Controls.Add(this.dbIpTextBox);
            this.dataGroupBox.Controls.Add(this.label3);
            this.dataGroupBox.Controls.Add(this.label2);
            this.dataGroupBox.Controls.Add(this.label1);
            this.dataGroupBox.Location = new System.Drawing.Point(3, 4);
            this.dataGroupBox.Name = "dataGroupBox";
            this.dataGroupBox.Size = new System.Drawing.Size(253, 240);
            this.dataGroupBox.TabIndex = 0;
            this.dataGroupBox.TabStop = false;
            this.dataGroupBox.Text = "SQL2008 数据库设置";
            this.dataGroupBox.Enter += new System.EventHandler(this.dataGroupBox_Enter);
            // 
            // testDbButton
            // 
            this.testDbButton.Location = new System.Drawing.Point(121, 169);
            this.testDbButton.Name = "testDbButton";
            this.testDbButton.Size = new System.Drawing.Size(75, 23);
            this.testDbButton.TabIndex = 6;
            this.testDbButton.Text = "测试连接";
            this.testDbButton.UseVisualStyleBackColor = true;
            this.testDbButton.Click += new System.EventHandler(this.testDbButton_Click);
            // 
            // dbPwdTextBox
            // 
            this.dbPwdTextBox.Location = new System.Drawing.Point(101, 103);
            this.dbPwdTextBox.Name = "dbPwdTextBox";
            this.dbPwdTextBox.Size = new System.Drawing.Size(123, 21);
            this.dbPwdTextBox.TabIndex = 5;
            this.dbPwdTextBox.UseSystemPasswordChar = true;
            // 
            // dbUserTextBox
            // 
            this.dbUserTextBox.Location = new System.Drawing.Point(101, 69);
            this.dbUserTextBox.Name = "dbUserTextBox";
            this.dbUserTextBox.Size = new System.Drawing.Size(123, 21);
            this.dbUserTextBox.TabIndex = 4;
            // 
            // dbIpTextBox
            // 
            this.dbIpTextBox.Location = new System.Drawing.Point(101, 30);
            this.dbIpTextBox.Name = "dbIpTextBox";
            this.dbIpTextBox.Size = new System.Drawing.Size(123, 21);
            this.dbIpTextBox.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(47, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "密  码";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(47, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "用户名";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "数据库IP";
            // 
            // bottomSplitContainer
            // 
            this.bottomSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bottomSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.bottomSplitContainer.Name = "bottomSplitContainer";
            this.bottomSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // bottomSplitContainer.Panel1
            // 
            this.bottomSplitContainer.Panel1.Controls.Add(this.splitContainer1);
            // 
            // bottomSplitContainer.Panel2
            // 
            this.bottomSplitContainer.Panel2.Controls.Add(this.cancelButton);
            this.bottomSplitContainer.Panel2.Controls.Add(this.okButton);
            this.bottomSplitContainer.Size = new System.Drawing.Size(394, 276);
            this.bottomSplitContainer.SplitterDistance = 245;
            this.bottomSplitContainer.TabIndex = 6;
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(306, 1);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "取消";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(219, 1);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "确定";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // OptionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 276);
            this.Controls.Add(this.bottomSplitContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "OptionForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设置选项";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.dataGroupBox.ResumeLayout(false);
            this.dataGroupBox.PerformLayout();
            this.bottomSplitContainer.Panel1.ResumeLayout(false);
            this.bottomSplitContainer.Panel2.ResumeLayout(false);
            this.bottomSplitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView optionTreeView;
        private System.Windows.Forms.GroupBox dataGroupBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox dbIpTextBox;
        private System.Windows.Forms.TextBox dbPwdTextBox;
        private System.Windows.Forms.TextBox dbUserTextBox;
        private System.Windows.Forms.SplitContainer bottomSplitContainer;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button testDbButton;
    }
}