namespace RemoteDesktopCenter
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tbDefault = new System.Windows.Forms.TableLayoutPanel();
            this.tbHeader = new System.Windows.Forms.TableLayoutPanel();
            this.tbFooter = new System.Windows.Forms.TableLayoutPanel();
            this.lblCredit = new System.Windows.Forms.Label();
            this.lblFooterDetail = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblHeader = new System.Windows.Forms.Label();
            this.lblSubHeader = new System.Windows.Forms.Label();
            this.tbMenu = new System.Windows.Forms.TableLayoutPanel();
            this.pbDefault = new System.Windows.Forms.PictureBox();
            this.btClose = new System.Windows.Forms.Button();
            this.btMinimize = new System.Windows.Forms.Button();
            this.ttDefault = new System.Windows.Forms.ToolTip(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.tbAccount = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.tmDefault = new System.Windows.Forms.Timer(this.components);
            this.tbContent = new System.Windows.Forms.TableLayoutPanel();
            this.lvSession = new System.Windows.Forms.ListView();
            this.clStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clUsername = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clIPAddress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clConnectTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clDisconnectTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clSessionID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clDomain = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.txtDebug = new System.Windows.Forms.TextBox();
            this.tbDefault.SuspendLayout();
            this.tbHeader.SuspendLayout();
            this.tbFooter.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tbMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbDefault)).BeginInit();
            this.tbAccount.SuspendLayout();
            this.tbContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbDefault
            // 
            this.tbDefault.ColumnCount = 1;
            this.tbDefault.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbDefault.Controls.Add(this.tbHeader, 0, 0);
            this.tbDefault.Controls.Add(this.tbFooter, 0, 3);
            this.tbDefault.Controls.Add(this.tbMenu, 0, 1);
            this.tbDefault.Controls.Add(this.tbContent, 0, 2);
            this.tbDefault.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbDefault.Location = new System.Drawing.Point(0, 0);
            this.tbDefault.Margin = new System.Windows.Forms.Padding(0);
            this.tbDefault.Name = "tbDefault";
            this.tbDefault.RowCount = 4;
            this.tbDefault.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tbDefault.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tbDefault.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbDefault.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tbDefault.Size = new System.Drawing.Size(558, 312);
            this.tbDefault.TabIndex = 0;
            // 
            // tbHeader
            // 
            this.tbHeader.AutoSize = true;
            this.tbHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(167)))), ((int)(((byte)(251)))));
            this.tbHeader.ColumnCount = 2;
            this.tbHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tbHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbHeader.Controls.Add(this.pbDefault, 0, 0);
            this.tbHeader.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tbHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbHeader.Location = new System.Drawing.Point(0, 0);
            this.tbHeader.Margin = new System.Windows.Forms.Padding(0);
            this.tbHeader.Name = "tbHeader";
            this.tbHeader.Padding = new System.Windows.Forms.Padding(5);
            this.tbHeader.RowCount = 1;
            this.tbHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbHeader.Size = new System.Drawing.Size(558, 80);
            this.tbHeader.TabIndex = 0;
            // 
            // tbFooter
            // 
            this.tbFooter.AutoSize = true;
            this.tbFooter.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbFooter.ColumnCount = 2;
            this.tbFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tbFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tbFooter.Controls.Add(this.lblCredit, 0, 0);
            this.tbFooter.Controls.Add(this.lblFooterDetail, 1, 0);
            this.tbFooter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbFooter.Location = new System.Drawing.Point(0, 289);
            this.tbFooter.Margin = new System.Windows.Forms.Padding(0);
            this.tbFooter.Name = "tbFooter";
            this.tbFooter.Padding = new System.Windows.Forms.Padding(5);
            this.tbFooter.RowCount = 1;
            this.tbFooter.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tbFooter.Size = new System.Drawing.Size(558, 23);
            this.tbFooter.TabIndex = 1;
            // 
            // lblCredit
            // 
            this.lblCredit.AutoSize = true;
            this.lblCredit.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblCredit.Location = new System.Drawing.Point(8, 5);
            this.lblCredit.Name = "lblCredit";
            this.lblCredit.Size = new System.Drawing.Size(352, 13);
            this.lblCredit.TabIndex = 0;
            this.lblCredit.Text = "©2015 Greenline Synergy All rights reserved. Power by nithi.re@glsict.com";
            // 
            // lblFooterDetail
            // 
            this.lblFooterDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFooterDetail.AutoSize = true;
            this.lblFooterDetail.BackColor = System.Drawing.Color.Transparent;
            this.lblFooterDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblFooterDetail.Location = new System.Drawing.Point(486, 5);
            this.lblFooterDetail.Name = "lblFooterDetail";
            this.lblFooterDetail.Size = new System.Drawing.Size(64, 13);
            this.lblFooterDetail.TabIndex = 1;
            this.lblFooterDetail.Text = "FooterDetail";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.lblHeader, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblSubHeader, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(78, 8);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(472, 64);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // lblHeader
            // 
            this.lblHeader.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblHeader.AutoSize = true;
            this.lblHeader.BackColor = System.Drawing.Color.Transparent;
            this.lblHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(0, 5);
            this.lblHeader.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(130, 31);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "lblHeader";
            // 
            // lblSubHeader
            // 
            this.lblSubHeader.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSubHeader.AutoSize = true;
            this.lblSubHeader.ForeColor = System.Drawing.Color.LightCyan;
            this.lblSubHeader.Location = new System.Drawing.Point(5, 43);
            this.lblSubHeader.Margin = new System.Windows.Forms.Padding(5, 0, 3, 0);
            this.lblSubHeader.Name = "lblSubHeader";
            this.lblSubHeader.Size = new System.Drawing.Size(71, 13);
            this.lblSubHeader.TabIndex = 1;
            this.lblSubHeader.Text = "lblSubHeader";
            // 
            // tbMenu
            // 
            this.tbMenu.AutoSize = true;
            this.tbMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(206)))), ((int)(((byte)(255)))));
            this.tbMenu.ColumnCount = 3;
            this.tbMenu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbMenu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tbMenu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tbMenu.Controls.Add(this.btClose, 2, 0);
            this.tbMenu.Controls.Add(this.btMinimize, 1, 0);
            this.tbMenu.Controls.Add(this.tbAccount, 0, 0);
            this.tbMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbMenu.Location = new System.Drawing.Point(0, 80);
            this.tbMenu.Margin = new System.Windows.Forms.Padding(0);
            this.tbMenu.Name = "tbMenu";
            this.tbMenu.RowCount = 1;
            this.tbMenu.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tbMenu.Size = new System.Drawing.Size(558, 33);
            this.tbMenu.TabIndex = 2;
            // 
            // pbDefault
            // 
            this.pbDefault.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pbDefault.BackColor = System.Drawing.Color.Transparent;
            this.pbDefault.Image = global::RemoteDesktopCenter.Properties.Resources.Desktop_icon_64;
            this.pbDefault.Location = new System.Drawing.Point(8, 8);
            this.pbDefault.Name = "pbDefault";
            this.pbDefault.Size = new System.Drawing.Size(64, 64);
            this.pbDefault.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbDefault.TabIndex = 0;
            this.pbDefault.TabStop = false;
            // 
            // btClose
            // 
            this.btClose.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btClose.BackColor = System.Drawing.Color.Transparent;
            this.btClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btClose.FlatAppearance.BorderSize = 0;
            this.btClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btClose.Image = global::RemoteDesktopCenter.Properties.Resources.icClose1;
            this.btClose.Location = new System.Drawing.Point(540, 8);
            this.btClose.Margin = new System.Windows.Forms.Padding(0, 2, 2, 2);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(16, 16);
            this.btClose.TabIndex = 0;
            this.btClose.UseVisualStyleBackColor = false;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // btMinimize
            // 
            this.btMinimize.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btMinimize.BackColor = System.Drawing.Color.Transparent;
            this.btMinimize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btMinimize.FlatAppearance.BorderSize = 0;
            this.btMinimize.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btMinimize.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btMinimize.Image = global::RemoteDesktopCenter.Properties.Resources.icMinimize;
            this.btMinimize.Location = new System.Drawing.Point(522, 8);
            this.btMinimize.Margin = new System.Windows.Forms.Padding(0, 2, 2, 2);
            this.btMinimize.Name = "btMinimize";
            this.btMinimize.Size = new System.Drawing.Size(16, 16);
            this.btMinimize.TabIndex = 0;
            this.btMinimize.UseVisualStyleBackColor = false;
            this.btMinimize.Click += new System.EventHandler(this.btMinimize_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(374, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(88, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "ConnectTest";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tbAccount
            // 
            this.tbAccount.AutoSize = true;
            this.tbAccount.ColumnCount = 5;
            this.tbAccount.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tbAccount.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tbAccount.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tbAccount.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tbAccount.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tbAccount.Controls.Add(this.label1, 0, 0);
            this.tbAccount.Controls.Add(this.button1, 4, 0);
            this.tbAccount.Controls.Add(this.label2, 2, 0);
            this.tbAccount.Controls.Add(this.txtUsername, 1, 0);
            this.tbAccount.Controls.Add(this.txtPassword, 3, 0);
            this.tbAccount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbAccount.Location = new System.Drawing.Point(0, 0);
            this.tbAccount.Margin = new System.Windows.Forms.Padding(0);
            this.tbAccount.Name = "tbAccount";
            this.tbAccount.Padding = new System.Windows.Forms.Padding(2);
            this.tbAccount.RowCount = 1;
            this.tbAccount.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tbAccount.Size = new System.Drawing.Size(522, 33);
            this.tbAccount.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "username";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(190, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "password";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(64, 5);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(120, 20);
            this.txtUsername.TabIndex = 2;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(248, 5);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(120, 20);
            this.txtPassword.TabIndex = 2;
            // 
            // tmDefault
            // 
            this.tmDefault.Interval = 1000;
            this.tmDefault.Tick += new System.EventHandler(this.tmDefault_Tick);
            // 
            // tbContent
            // 
            this.tbContent.AutoSize = true;
            this.tbContent.ColumnCount = 1;
            this.tbContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbContent.Controls.Add(this.lvSession, 0, 0);
            this.tbContent.Controls.Add(this.txtDebug, 0, 1);
            this.tbContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbContent.Location = new System.Drawing.Point(3, 116);
            this.tbContent.Name = "tbContent";
            this.tbContent.RowCount = 2;
            this.tbContent.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tbContent.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tbContent.Size = new System.Drawing.Size(552, 170);
            this.tbContent.TabIndex = 3;
            // 
            // lvSession
            // 
            this.lvSession.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clStatus,
            this.clDomain,
            this.clUsername,
            this.clIPAddress,
            this.clConnectTime,
            this.clDisconnectTime,
            this.clSessionID});
            this.lvSession.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvSession.Location = new System.Drawing.Point(3, 3);
            this.lvSession.Name = "lvSession";
            this.lvSession.Size = new System.Drawing.Size(546, 97);
            this.lvSession.TabIndex = 0;
            this.lvSession.UseCompatibleStateImageBehavior = false;
            this.lvSession.View = System.Windows.Forms.View.Details;
            // 
            // clStatus
            // 
            this.clStatus.Text = "Status";
            // 
            // clUsername
            // 
            this.clUsername.Text = "Username";
            // 
            // clIPAddress
            // 
            this.clIPAddress.Text = "IPAddress";
            // 
            // clConnectTime
            // 
            this.clConnectTime.Text = "ConnectTime";
            // 
            // clDisconnectTime
            // 
            this.clDisconnectTime.Text = "DisconnectTime";
            // 
            // clSessionID
            // 
            this.clSessionID.Text = "SessionID";
            // 
            // clDomain
            // 
            this.clDomain.Text = "Domain";
            // 
            // txtDebug
            // 
            this.txtDebug.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDebug.Location = new System.Drawing.Point(3, 106);
            this.txtDebug.Multiline = true;
            this.txtDebug.Name = "txtDebug";
            this.txtDebug.Size = new System.Drawing.Size(546, 61);
            this.txtDebug.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(558, 312);
            this.Controls.Add(this.tbDefault);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RemoteDesktopCenter";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tbDefault.ResumeLayout(false);
            this.tbDefault.PerformLayout();
            this.tbHeader.ResumeLayout(false);
            this.tbHeader.PerformLayout();
            this.tbFooter.ResumeLayout(false);
            this.tbFooter.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tbMenu.ResumeLayout(false);
            this.tbMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbDefault)).EndInit();
            this.tbAccount.ResumeLayout(false);
            this.tbAccount.PerformLayout();
            this.tbContent.ResumeLayout(false);
            this.tbContent.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tbDefault;
        private System.Windows.Forms.TableLayoutPanel tbHeader;
        private System.Windows.Forms.PictureBox pbDefault;
        private System.Windows.Forms.TableLayoutPanel tbFooter;
        private System.Windows.Forms.Label lblCredit;
        private System.Windows.Forms.Label lblFooterDetail;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblSubHeader;
        private System.Windows.Forms.TableLayoutPanel tbMenu;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Button btMinimize;
        private System.Windows.Forms.ToolTip ttDefault;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TableLayoutPanel tbAccount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Timer tmDefault;
        private System.Windows.Forms.TableLayoutPanel tbContent;
        private System.Windows.Forms.ListView lvSession;
        private System.Windows.Forms.ColumnHeader clStatus;
        private System.Windows.Forms.ColumnHeader clUsername;
        private System.Windows.Forms.ColumnHeader clIPAddress;
        private System.Windows.Forms.ColumnHeader clConnectTime;
        private System.Windows.Forms.ColumnHeader clDisconnectTime;
        private System.Windows.Forms.ColumnHeader clSessionID;
        private System.Windows.Forms.ColumnHeader clDomain;
        private System.Windows.Forms.TextBox txtDebug;
    }
}

