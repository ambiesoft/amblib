namespace AmbLibTest
{
    partial class FormMain
    {
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "AAAAAAAAAAAAAAAA",
            "BBBBBBBBBBB"}, -1);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
            "xxxxxxxxxxxxxxx",
            "zzzzzzzzzzzzzz"}, -1);
            this.toolStrip1 = new Ambiesoft.ClickThroughToolStrip();
            this.tsbNew = new System.Windows.Forms.ToolStripButton();
            this.tsbOpen = new System.Windows.Forms.ToolStripButton();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.tsbPrint = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.tsbcMain = new System.Windows.Forms.ToolStripComboBox();
            this.tsbCut = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbHelp = new System.Windows.Forms.ToolStripButton();
            this.txtDNS = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPointInScreen = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1aaaaaaa = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnOpenAmbiesoft = new System.Windows.Forms.Button();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tpSimple = new System.Windows.Forms.TabPage();
            this.btnGetSaveFile = new System.Windows.Forms.Button();
            this.btnSelectApp = new System.Windows.Forms.Button();
            this.btnFormatSizeTest = new System.Windows.Forms.Button();
            this.cmbTripleClick = new System.Windows.Forms.ComboBox();
            this.btnI18NTest = new System.Windows.Forms.Button();
            this.btnShowTexts = new System.Windows.Forms.Button();
            this.btnShowText = new System.Windows.Forms.Button();
            this.btnSimpleTest = new System.Windows.Forms.Button();
            this.txtTripleClick = new System.Windows.Forms.TextBox();
            this.tbMisc = new System.Windows.Forms.TabPage();
            this.tpListView = new System.Windows.Forms.TabPage();
            this.lvForFontSize = new AmbLibTest.TestListView();
            this.ch1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tpGpuInfo = new System.Windows.Forms.TabPage();
            this.txtGpuInfo = new System.Windows.Forms.TextBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnFileToDir = new System.Windows.Forms.Button();
            this.btnCreateDir = new System.Windows.Forms.Button();
            this.btnSrcDstRelative = new System.Windows.Forms.Button();
            this.btnFileToFile = new System.Windows.Forms.Button();
            this.btnSrcDstFull = new System.Windows.Forms.Button();
            this.btnSrcDst = new System.Windows.Forms.Button();
            this.txtSrcDstResult = new System.Windows.Forms.TextBox();
            this.txtDst = new System.Windows.Forms.TextBox();
            this.txtSrc = new System.Windows.Forms.TextBox();
            this.btnGetFolder = new System.Windows.Forms.Button();
            this.toolStrip1.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tpSimple.SuspendLayout();
            this.tbMisc.SuspendLayout();
            this.tpListView.SuspendLayout();
            this.tpGpuInfo.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ClickThrough = true;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbNew,
            this.tsbOpen,
            this.tsbSave,
            this.tsbPrint,
            this.toolStripSeparator,
            this.tsbcMain,
            this.tsbCut,
            this.toolStripSeparator1,
            this.tsbHelp});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(548, 25);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "tsMain";
            // 
            // tsbNew
            // 
            this.tsbNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNew.Image = ((System.Drawing.Image)(resources.GetObject("tsbNew.Image")));
            this.tsbNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNew.Name = "tsbNew";
            this.tsbNew.Size = new System.Drawing.Size(23, 22);
            this.tsbNew.Text = "&New";
            // 
            // tsbOpen
            // 
            this.tsbOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbOpen.Image = ((System.Drawing.Image)(resources.GetObject("tsbOpen.Image")));
            this.tsbOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOpen.Name = "tsbOpen";
            this.tsbOpen.Size = new System.Drawing.Size(23, 22);
            this.tsbOpen.Text = "&Open";
            this.tsbOpen.Click += new System.EventHandler(this.tsbOpen_Click);
            // 
            // tsbSave
            // 
            this.tsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSave.Image = ((System.Drawing.Image)(resources.GetObject("tsbSave.Image")));
            this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Size = new System.Drawing.Size(23, 22);
            this.tsbSave.Text = "&Save";
            // 
            // tsbPrint
            // 
            this.tsbPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPrint.Image = ((System.Drawing.Image)(resources.GetObject("tsbPrint.Image")));
            this.tsbPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPrint.Name = "tsbPrint";
            this.tsbPrint.Size = new System.Drawing.Size(23, 22);
            this.tsbPrint.Text = "&Print";
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbcMain
            // 
            this.tsbcMain.Name = "tsbcMain";
            this.tsbcMain.Size = new System.Drawing.Size(121, 25);
            // 
            // tsbCut
            // 
            this.tsbCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCut.Image = ((System.Drawing.Image)(resources.GetObject("tsbCut.Image")));
            this.tsbCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCut.Name = "tsbCut";
            this.tsbCut.Size = new System.Drawing.Size(23, 22);
            this.tsbCut.Text = "C&ut";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbHelp
            // 
            this.tsbHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbHelp.Image = ((System.Drawing.Image)(resources.GetObject("tsbHelp.Image")));
            this.tsbHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbHelp.Name = "tsbHelp";
            this.tsbHelp.Size = new System.Drawing.Size(23, 22);
            this.tsbHelp.Text = "He&lp";
            // 
            // txtDNS
            // 
            this.txtDNS.Location = new System.Drawing.Point(47, 7);
            this.txtDNS.Name = "txtDNS";
            this.txtDNS.Size = new System.Drawing.Size(431, 19);
            this.txtDNS.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "&DNS:";
            // 
            // txtPointInScreen
            // 
            this.txtPointInScreen.Location = new System.Drawing.Point(88, 32);
            this.txtPointInScreen.Name = "txtPointInScreen";
            this.txtPointInScreen.Size = new System.Drawing.Size(309, 19);
            this.txtPointInScreen.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "PointInScreen";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(403, 32);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 18);
            this.button1.TabIndex = 5;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1aaaaaaa,
            this.columnHeader1});
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(9, 79);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(471, 119);
            this.listView1.TabIndex = 6;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // btnOpenAmbiesoft
            // 
            this.btnOpenAmbiesoft.Location = new System.Drawing.Point(9, 204);
            this.btnOpenAmbiesoft.Name = "btnOpenAmbiesoft";
            this.btnOpenAmbiesoft.Size = new System.Drawing.Size(189, 21);
            this.btnOpenAmbiesoft.TabIndex = 7;
            this.btnOpenAmbiesoft.Text = "Open ambiesoft site";
            this.btnOpenAmbiesoft.UseVisualStyleBackColor = true;
            this.btnOpenAmbiesoft.Click += new System.EventHandler(this.btnOpenAmbiesoft_Click);
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tpSimple);
            this.tabMain.Controls.Add(this.tbMisc);
            this.tabMain.Controls.Add(this.tpListView);
            this.tabMain.Controls.Add(this.tpGpuInfo);
            this.tabMain.Controls.Add(this.tabPage1);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 25);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(548, 293);
            this.tabMain.TabIndex = 8;
            // 
            // tpSimple
            // 
            this.tpSimple.Controls.Add(this.btnGetFolder);
            this.tpSimple.Controls.Add(this.btnGetSaveFile);
            this.tpSimple.Controls.Add(this.btnSelectApp);
            this.tpSimple.Controls.Add(this.btnFormatSizeTest);
            this.tpSimple.Controls.Add(this.cmbTripleClick);
            this.tpSimple.Controls.Add(this.btnI18NTest);
            this.tpSimple.Controls.Add(this.btnShowTexts);
            this.tpSimple.Controls.Add(this.btnShowText);
            this.tpSimple.Controls.Add(this.btnSimpleTest);
            this.tpSimple.Controls.Add(this.txtTripleClick);
            this.tpSimple.Location = new System.Drawing.Point(4, 22);
            this.tpSimple.Name = "tpSimple";
            this.tpSimple.Padding = new System.Windows.Forms.Padding(3);
            this.tpSimple.Size = new System.Drawing.Size(540, 267);
            this.tpSimple.TabIndex = 0;
            this.tpSimple.Text = "Simple";
            this.tpSimple.UseVisualStyleBackColor = true;
            // 
            // btnGetSaveFile
            // 
            this.btnGetSaveFile.Location = new System.Drawing.Point(284, 183);
            this.btnGetSaveFile.Name = "btnGetSaveFile";
            this.btnGetSaveFile.Size = new System.Drawing.Size(75, 21);
            this.btnGetSaveFile.TabIndex = 9;
            this.btnGetSaveFile.Text = "GetSaveFile";
            this.btnGetSaveFile.UseVisualStyleBackColor = true;
            this.btnGetSaveFile.Click += new System.EventHandler(this.btnGetSaveFile_Click);
            // 
            // btnSelectApp
            // 
            this.btnSelectApp.Location = new System.Drawing.Point(203, 183);
            this.btnSelectApp.Name = "btnSelectApp";
            this.btnSelectApp.Size = new System.Drawing.Size(75, 21);
            this.btnSelectApp.TabIndex = 9;
            this.btnSelectApp.Text = "Select App";
            this.btnSelectApp.UseVisualStyleBackColor = true;
            this.btnSelectApp.Click += new System.EventHandler(this.btnSelectApp_Click);
            // 
            // btnFormatSizeTest
            // 
            this.btnFormatSizeTest.Location = new System.Drawing.Point(89, 183);
            this.btnFormatSizeTest.Name = "btnFormatSizeTest";
            this.btnFormatSizeTest.Size = new System.Drawing.Size(108, 21);
            this.btnFormatSizeTest.TabIndex = 9;
            this.btnFormatSizeTest.Text = "FormatSize Test";
            this.btnFormatSizeTest.UseVisualStyleBackColor = true;
            this.btnFormatSizeTest.Click += new System.EventHandler(this.btnFormatSizeTest_Click);
            // 
            // cmbTripleClick
            // 
            this.cmbTripleClick.FormattingEnabled = true;
            this.cmbTripleClick.Items.AddRange(new object[] {
            "Triple Click Test"});
            this.cmbTripleClick.Location = new System.Drawing.Point(6, 30);
            this.cmbTripleClick.Name = "cmbTripleClick";
            this.cmbTripleClick.Size = new System.Drawing.Size(470, 20);
            this.cmbTripleClick.TabIndex = 10;
            this.cmbTripleClick.Text = "Triple Click Test";
            this.cmbTripleClick.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.cmbTripleClick_MouseDoubleClick);
            // 
            // btnI18NTest
            // 
            this.btnI18NTest.Location = new System.Drawing.Point(8, 183);
            this.btnI18NTest.Name = "btnI18NTest";
            this.btnI18NTest.Size = new System.Drawing.Size(75, 21);
            this.btnI18NTest.TabIndex = 9;
            this.btnI18NTest.Text = "I18N TEST";
            this.btnI18NTest.UseVisualStyleBackColor = true;
            this.btnI18NTest.Click += new System.EventHandler(this.btnI18NTest_Click);
            // 
            // btnShowTexts
            // 
            this.btnShowTexts.Location = new System.Drawing.Point(89, 135);
            this.btnShowTexts.Name = "btnShowTexts";
            this.btnShowTexts.Size = new System.Drawing.Size(75, 21);
            this.btnShowTexts.TabIndex = 9;
            this.btnShowTexts.Text = "Show Texts";
            this.btnShowTexts.UseVisualStyleBackColor = true;
            this.btnShowTexts.Click += new System.EventHandler(this.btnShowTexts_Click);
            // 
            // btnShowText
            // 
            this.btnShowText.Location = new System.Drawing.Point(8, 135);
            this.btnShowText.Name = "btnShowText";
            this.btnShowText.Size = new System.Drawing.Size(75, 21);
            this.btnShowText.TabIndex = 2;
            this.btnShowText.Text = "Show Text";
            this.btnShowText.UseVisualStyleBackColor = true;
            this.btnShowText.Click += new System.EventHandler(this.btnShowText_Click);
            // 
            // btnSimpleTest
            // 
            this.btnSimpleTest.Location = new System.Drawing.Point(8, 88);
            this.btnSimpleTest.Name = "btnSimpleTest";
            this.btnSimpleTest.Size = new System.Drawing.Size(75, 21);
            this.btnSimpleTest.TabIndex = 1;
            this.btnSimpleTest.Text = "SimpleTest";
            this.btnSimpleTest.UseVisualStyleBackColor = true;
            this.btnSimpleTest.Click += new System.EventHandler(this.btnSimpleTest_Click);
            // 
            // txtTripleClick
            // 
            this.txtTripleClick.Location = new System.Drawing.Point(6, 6);
            this.txtTripleClick.Name = "txtTripleClick";
            this.txtTripleClick.Size = new System.Drawing.Size(470, 19);
            this.txtTripleClick.TabIndex = 0;
            this.txtTripleClick.Text = "Triple Click Test";
            // 
            // tbMisc
            // 
            this.tbMisc.Controls.Add(this.btnOpenAmbiesoft);
            this.tbMisc.Controls.Add(this.listView1);
            this.tbMisc.Controls.Add(this.txtDNS);
            this.tbMisc.Controls.Add(this.label1);
            this.tbMisc.Controls.Add(this.button1);
            this.tbMisc.Controls.Add(this.label2);
            this.tbMisc.Controls.Add(this.txtPointInScreen);
            this.tbMisc.Location = new System.Drawing.Point(4, 22);
            this.tbMisc.Name = "tbMisc";
            this.tbMisc.Padding = new System.Windows.Forms.Padding(3);
            this.tbMisc.Size = new System.Drawing.Size(540, 269);
            this.tbMisc.TabIndex = 1;
            this.tbMisc.Text = "Misc";
            this.tbMisc.UseVisualStyleBackColor = true;
            // 
            // tpListView
            // 
            this.tpListView.Controls.Add(this.lvForFontSize);
            this.tpListView.Location = new System.Drawing.Point(4, 22);
            this.tpListView.Name = "tpListView";
            this.tpListView.Padding = new System.Windows.Forms.Padding(3);
            this.tpListView.Size = new System.Drawing.Size(540, 269);
            this.tpListView.TabIndex = 2;
            this.tpListView.Text = "ListView";
            this.tpListView.UseVisualStyleBackColor = true;
            // 
            // lvForFontSize
            // 
            this.lvForFontSize.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ch1,
            this.ch2});
            this.lvForFontSize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvForFontSize.HideSelection = false;
            this.lvForFontSize.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
            this.lvForFontSize.Location = new System.Drawing.Point(3, 3);
            this.lvForFontSize.Name = "lvForFontSize";
            this.lvForFontSize.Size = new System.Drawing.Size(534, 263);
            this.lvForFontSize.TabIndex = 11;
            this.lvForFontSize.UseCompatibleStateImageBehavior = false;
            this.lvForFontSize.View = System.Windows.Forms.View.Details;
            // 
            // ch1
            // 
            this.ch1.Width = 200;
            // 
            // ch2
            // 
            this.ch2.Width = 170;
            // 
            // tpGpuInfo
            // 
            this.tpGpuInfo.Controls.Add(this.txtGpuInfo);
            this.tpGpuInfo.Location = new System.Drawing.Point(4, 22);
            this.tpGpuInfo.Name = "tpGpuInfo";
            this.tpGpuInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tpGpuInfo.Size = new System.Drawing.Size(540, 269);
            this.tpGpuInfo.TabIndex = 3;
            this.tpGpuInfo.Text = "GPU info";
            this.tpGpuInfo.UseVisualStyleBackColor = true;
            // 
            // txtGpuInfo
            // 
            this.txtGpuInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtGpuInfo.Location = new System.Drawing.Point(3, 3);
            this.txtGpuInfo.Multiline = true;
            this.txtGpuInfo.Name = "txtGpuInfo";
            this.txtGpuInfo.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtGpuInfo.Size = new System.Drawing.Size(534, 263);
            this.txtGpuInfo.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnFileToDir);
            this.tabPage1.Controls.Add(this.btnCreateDir);
            this.tabPage1.Controls.Add(this.btnSrcDstRelative);
            this.tabPage1.Controls.Add(this.btnFileToFile);
            this.tabPage1.Controls.Add(this.btnSrcDstFull);
            this.tabPage1.Controls.Add(this.btnSrcDst);
            this.tabPage1.Controls.Add(this.txtSrcDstResult);
            this.tabPage1.Controls.Add(this.txtDst);
            this.tabPage1.Controls.Add(this.txtSrc);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(540, 269);
            this.tabPage1.TabIndex = 4;
            this.tabPage1.Text = "Get Src and Dst";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnFileToDir
            // 
            this.btnFileToDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFileToDir.Location = new System.Drawing.Point(206, 247);
            this.btnFileToDir.Name = "btnFileToDir";
            this.btnFileToDir.Size = new System.Drawing.Size(75, 21);
            this.btnFileToDir.TabIndex = 3;
            this.btnFileToDir.Text = "File2Dir";
            this.btnFileToDir.UseVisualStyleBackColor = true;
            this.btnFileToDir.Click += new System.EventHandler(this.btnFileToDir_Click);
            // 
            // btnCreateDir
            // 
            this.btnCreateDir.Location = new System.Drawing.Point(462, 48);
            this.btnCreateDir.Name = "btnCreateDir";
            this.btnCreateDir.Size = new System.Drawing.Size(75, 21);
            this.btnCreateDir.TabIndex = 2;
            this.btnCreateDir.Text = "mkdir";
            this.btnCreateDir.UseVisualStyleBackColor = true;
            this.btnCreateDir.Click += new System.EventHandler(this.btnCreateDir_Click);
            // 
            // btnSrcDstRelative
            // 
            this.btnSrcDstRelative.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSrcDstRelative.Location = new System.Drawing.Point(411, 245);
            this.btnSrcDstRelative.Name = "btnSrcDstRelative";
            this.btnSrcDstRelative.Size = new System.Drawing.Size(116, 21);
            this.btnSrcDstRelative.TabIndex = 1;
            this.btnSrcDstRelative.Text = "Relative";
            this.btnSrcDstRelative.UseVisualStyleBackColor = true;
            this.btnSrcDstRelative.Click += new System.EventHandler(this.btnSrcDstRelative_Click);
            // 
            // btnFileToFile
            // 
            this.btnFileToFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFileToFile.Location = new System.Drawing.Point(125, 247);
            this.btnFileToFile.Name = "btnFileToFile";
            this.btnFileToFile.Size = new System.Drawing.Size(75, 21);
            this.btnFileToFile.TabIndex = 1;
            this.btnFileToFile.Text = "File2File";
            this.btnFileToFile.UseVisualStyleBackColor = true;
            this.btnFileToFile.Click += new System.EventHandler(this.btnFileToFile_Click);
            // 
            // btnSrcDstFull
            // 
            this.btnSrcDstFull.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSrcDstFull.Location = new System.Drawing.Point(289, 245);
            this.btnSrcDstFull.Name = "btnSrcDstFull";
            this.btnSrcDstFull.Size = new System.Drawing.Size(116, 21);
            this.btnSrcDstFull.TabIndex = 1;
            this.btnSrcDstFull.Text = "Full";
            this.btnSrcDstFull.UseVisualStyleBackColor = true;
            this.btnSrcDstFull.Click += new System.EventHandler(this.btnSrcDstFull_Click);
            // 
            // btnSrcDst
            // 
            this.btnSrcDst.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSrcDst.Location = new System.Drawing.Point(3, 248);
            this.btnSrcDst.Name = "btnSrcDst";
            this.btnSrcDst.Size = new System.Drawing.Size(116, 21);
            this.btnSrcDst.TabIndex = 1;
            this.btnSrcDst.Text = "Get Src and Dst";
            this.btnSrcDst.UseVisualStyleBackColor = true;
            this.btnSrcDst.Click += new System.EventHandler(this.btnSrcDst_Click);
            // 
            // txtSrcDstResult
            // 
            this.txtSrcDstResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSrcDstResult.Location = new System.Drawing.Point(3, 72);
            this.txtSrcDstResult.Multiline = true;
            this.txtSrcDstResult.Name = "txtSrcDstResult";
            this.txtSrcDstResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSrcDstResult.Size = new System.Drawing.Size(524, 156);
            this.txtSrcDstResult.TabIndex = 0;
            this.txtSrcDstResult.WordWrap = false;
            // 
            // txtDst
            // 
            this.txtDst.Location = new System.Drawing.Point(6, 48);
            this.txtDst.Name = "txtDst";
            this.txtDst.Size = new System.Drawing.Size(451, 19);
            this.txtDst.TabIndex = 0;
            // 
            // txtSrc
            // 
            this.txtSrc.Location = new System.Drawing.Point(8, 24);
            this.txtSrc.Name = "txtSrc";
            this.txtSrc.Size = new System.Drawing.Size(470, 19);
            this.txtSrc.TabIndex = 0;
            // 
            // btnGetFolder
            // 
            this.btnGetFolder.Location = new System.Drawing.Point(365, 181);
            this.btnGetFolder.Name = "btnGetFolder";
            this.btnGetFolder.Size = new System.Drawing.Size(75, 23);
            this.btnGetFolder.TabIndex = 9;
            this.btnGetFolder.Text = "Get Folder";
            this.btnGetFolder.UseVisualStyleBackColor = true;
            this.btnGetFolder.Click += new System.EventHandler(this.btnGetFolder_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(548, 318);
            this.Controls.Add(this.tabMain);
            this.Controls.Add(this.toolStrip1);
            this.Name = "FormMain";
            this.Text = "FormMain";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.SizeChanged += new System.EventHandler(this.FormMain_SizeChanged);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabMain.ResumeLayout(false);
            this.tpSimple.ResumeLayout(false);
            this.tpSimple.PerformLayout();
            this.tbMisc.ResumeLayout(false);
            this.tbMisc.PerformLayout();
            this.tpListView.ResumeLayout(false);
            this.tpGpuInfo.ResumeLayout(false);
            this.tpGpuInfo.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Ambiesoft.ClickThroughToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbNew;
        private System.Windows.Forms.ToolStripButton tsbOpen;
        private System.Windows.Forms.ToolStripButton tsbSave;
        private System.Windows.Forms.ToolStripButton tsbPrint;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripComboBox tsbcMain;
        private System.Windows.Forms.ToolStripButton tsbCut;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbHelp;
        private System.Windows.Forms.TextBox txtDNS;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPointInScreen;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1aaaaaaa;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button btnOpenAmbiesoft;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tpSimple;
        private System.Windows.Forms.TabPage tbMisc;
        private System.Windows.Forms.TextBox txtTripleClick;
        private System.Windows.Forms.Button btnSimpleTest;
        private System.Windows.Forms.Button btnShowText;
        private System.Windows.Forms.Button btnShowTexts;
        private System.Windows.Forms.Button btnI18NTest;
        private System.Windows.Forms.ComboBox cmbTripleClick;
        private System.Windows.Forms.Button btnFormatSizeTest;
        private System.Windows.Forms.Button btnSelectApp;
        private System.Windows.Forms.TabPage tpListView;
        private TestListView lvForFontSize;
        private System.Windows.Forms.ColumnHeader ch1;
        private System.Windows.Forms.ColumnHeader ch2;
        private System.Windows.Forms.TabPage tpGpuInfo;
        private System.Windows.Forms.TextBox txtGpuInfo;
        private System.Windows.Forms.Button btnGetSaveFile;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btnSrcDst;
        private System.Windows.Forms.TextBox txtSrcDstResult;
        private System.Windows.Forms.TextBox txtDst;
        private System.Windows.Forms.TextBox txtSrc;
        private System.Windows.Forms.Button btnSrcDstRelative;
        private System.Windows.Forms.Button btnSrcDstFull;
        private System.Windows.Forms.Button btnCreateDir;
        private System.Windows.Forms.Button btnFileToFile;
        private System.Windows.Forms.Button btnFileToDir;
        private System.Windows.Forms.Button btnGetFolder;
    }
}