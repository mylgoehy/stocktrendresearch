namespace GUI
{
    partial class MainGUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainGUI));
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabExperiment = new System.Windows.Forms.TabPage();
            this.tbxChoseFolder = new System.Windows.Forms.TextBox();
            this.btnChoseFolder = new System.Windows.Forms.Button();
            this.cbChoseData = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbExpStockID = new System.Windows.Forms.ComboBox();
            this.gbResults = new System.Windows.Forms.GroupBox();
            this.wbResults = new System.Windows.Forms.WebBrowser();
            this.lblExperimentMode = new System.Windows.Forms.Label();
            this.gbBatchTrainTest = new System.Windows.Forms.GroupBox();
            this.btnBatchTrainTest = new System.Windows.Forms.Button();
            this.lblBatchInputFile = new System.Windows.Forms.Label();
            this.tbxBatchInputFile = new System.Windows.Forms.TextBox();
            this.btnBatchBrowse = new System.Windows.Forms.Button();
            this.gbTest = new System.Windows.Forms.GroupBox();
            this.lblModelFile = new System.Windows.Forms.Label();
            this.btnModelBrowser = new System.Windows.Forms.Button();
            this.tbxModelFilePath = new System.Windows.Forms.TextBox();
            this.lblTestFile = new System.Windows.Forms.Label();
            this.btnTestBrowser = new System.Windows.Forms.Button();
            this.tbxTestFilePath = new System.Windows.Forms.TextBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.cmbExperimentMode = new System.Windows.Forms.ComboBox();
            this.gbTraining = new System.Windows.Forms.GroupBox();
            this.lblTrainingFile = new System.Windows.Forms.Label();
            this.btnTrainBrowser = new System.Windows.Forms.Button();
            this.tbxTrainFilePath = new System.Windows.Forms.TextBox();
            this.btnTrain = new System.Windows.Forms.Button();
            this.gbPreprocess = new System.Windows.Forms.GroupBox();
            this.lblInputFile = new System.Windows.Forms.Label();
            this.btnPreprocess = new System.Windows.Forms.Button();
            this.tbxCsvFilePath = new System.Windows.Forms.TextBox();
            this.btnPreprocessBrowser = new System.Windows.Forms.Button();
            this.gbModelChoice = new System.Windows.Forms.GroupBox();
            this.rdDTANN = new System.Windows.Forms.RadioButton();
            this.tabCtrlSettings = new System.Windows.Forms.TabControl();
            this.tabDT_ANN = new System.Windows.Forms.TabPage();
            this.gbAnnSetting = new System.Windows.Forms.GroupBox();
            this.cmbActivationFunc = new System.Windows.Forms.ComboBox();
            this.lblActivationFunc = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.tbxANNHiddenNode = new System.Windows.Forms.TextBox();
            this.tbxMomentum = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.tbxMaxLoops = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tbxBias = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tbxLearningRate = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.gbDTSetting = new System.Windows.Forms.GroupBox();
            this.cmbPruneFunc = new System.Windows.Forms.ComboBox();
            this.cmbSplitFunc = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tabSVM_KMeans = new System.Windows.Forms.TabPage();
            this.gbKmeansSetting = new System.Windows.Forms.GroupBox();
            this.cmbDistanceType = new System.Windows.Forms.ComboBox();
            this.lblDistanceType = new System.Windows.Forms.Label();
            this.nmNumCluster = new System.Windows.Forms.NumericUpDown();
            this.lblNumCluster = new System.Windows.Forms.Label();
            this.gbSVRSetting = new System.Windows.Forms.GroupBox();
            this.ckbProbEstimate = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxGamma = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxC = new System.Windows.Forms.TextBox();
            this.tbxNumFold = new System.Windows.Forms.TextBox();
            this.lblNumFold = new System.Windows.Forms.Label();
            this.cmbModelSelection = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbNumDaysPredicted = new System.Windows.Forms.ComboBox();
            this.tbxTrainingRatio = new System.Windows.Forms.TextBox();
            this.lblTrainingRatio = new System.Windows.Forms.Label();
            this.rdSVM = new System.Windows.Forms.RadioButton();
            this.rdANN = new System.Windows.Forms.RadioButton();
            this.label10 = new System.Windows.Forms.Label();
            this.rdKSVMeans = new System.Windows.Forms.RadioButton();
            this.tabApplication = new System.Windows.Forms.TabPage();
            this.wbSAResult = new System.Windows.Forms.WebBrowser();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnUpdateData = new System.Windows.Forms.Button();
            this.dtpChoseCurrentDate = new System.Windows.Forms.DateTimePicker();
            this.label15 = new System.Windows.Forms.Label();
            this.btnSAPredict = new System.Windows.Forms.Button();
            this.cbSAFiveDay = new System.Windows.Forms.CheckBox();
            this.cbSAOneDay = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbSAStockID = new System.Windows.Forms.ComboBox();
            this.gbStockChart = new System.Windows.Forms.GroupBox();
            this.zg1 = new ZedGraph.ZedGraphControl();
            this.lblTo = new System.Windows.Forms.Label();
            this.cmbStockID = new System.Windows.Forms.ComboBox();
            this.lblFrom = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.lblStockID = new System.Windows.Forms.Label();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.lblBackTitle = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.staMainProgress = new System.Windows.Forms.StatusStrip();
            this.tlsProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.tlsStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabMain.SuspendLayout();
            this.tabExperiment.SuspendLayout();
            this.gbResults.SuspendLayout();
            this.gbBatchTrainTest.SuspendLayout();
            this.gbTest.SuspendLayout();
            this.gbTraining.SuspendLayout();
            this.gbPreprocess.SuspendLayout();
            this.gbModelChoice.SuspendLayout();
            this.tabCtrlSettings.SuspendLayout();
            this.tabDT_ANN.SuspendLayout();
            this.gbAnnSetting.SuspendLayout();
            this.gbDTSetting.SuspendLayout();
            this.tabSVM_KMeans.SuspendLayout();
            this.gbKmeansSetting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmNumCluster)).BeginInit();
            this.gbSVRSetting.SuspendLayout();
            this.tabApplication.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gbStockChart.SuspendLayout();
            this.staMainProgress.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabExperiment);
            this.tabMain.Controls.Add(this.tabApplication);
            this.tabMain.Location = new System.Drawing.Point(0, 52);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(784, 570);
            this.tabMain.TabIndex = 9;
            this.tabMain.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabOption_Selected);
            // 
            // tabExperiment
            // 
            this.tabExperiment.BackColor = System.Drawing.Color.Transparent;
            this.tabExperiment.Controls.Add(this.tbxChoseFolder);
            this.tabExperiment.Controls.Add(this.btnChoseFolder);
            this.tabExperiment.Controls.Add(this.cbChoseData);
            this.tabExperiment.Controls.Add(this.label7);
            this.tabExperiment.Controls.Add(this.cmbExpStockID);
            this.tabExperiment.Controls.Add(this.gbResults);
            this.tabExperiment.Controls.Add(this.lblExperimentMode);
            this.tabExperiment.Controls.Add(this.gbBatchTrainTest);
            this.tabExperiment.Controls.Add(this.gbTest);
            this.tabExperiment.Controls.Add(this.cmbExperimentMode);
            this.tabExperiment.Controls.Add(this.gbTraining);
            this.tabExperiment.Controls.Add(this.gbPreprocess);
            this.tabExperiment.Controls.Add(this.gbModelChoice);
            this.tabExperiment.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabExperiment.ForeColor = System.Drawing.Color.Black;
            this.tabExperiment.Location = new System.Drawing.Point(4, 22);
            this.tabExperiment.Name = "tabExperiment";
            this.tabExperiment.Padding = new System.Windows.Forms.Padding(3);
            this.tabExperiment.Size = new System.Drawing.Size(776, 544);
            this.tabExperiment.TabIndex = 0;
            this.tabExperiment.Text = "EXPERIMENT";
            // 
            // tbxChoseFolder
            // 
            this.tbxChoseFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxChoseFolder.ForeColor = System.Drawing.Color.Black;
            this.tbxChoseFolder.Location = new System.Drawing.Point(491, 18);
            this.tbxChoseFolder.Name = "tbxChoseFolder";
            this.tbxChoseFolder.Size = new System.Drawing.Size(244, 20);
            this.tbxChoseFolder.TabIndex = 53;
            // 
            // btnChoseFolder
            // 
            this.btnChoseFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChoseFolder.ForeColor = System.Drawing.Color.Black;
            this.btnChoseFolder.Image = ((System.Drawing.Image)(resources.GetObject("btnChoseFolder.Image")));
            this.btnChoseFolder.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnChoseFolder.Location = new System.Drawing.Point(741, 15);
            this.btnChoseFolder.Name = "btnChoseFolder";
            this.btnChoseFolder.Size = new System.Drawing.Size(24, 24);
            this.btnChoseFolder.TabIndex = 54;
            this.btnChoseFolder.UseVisualStyleBackColor = true;
            this.btnChoseFolder.Click += new System.EventHandler(this.btnChoseFolder_Click);
            // 
            // cbChoseData
            // 
            this.cbChoseData.AutoSize = true;
            this.cbChoseData.Location = new System.Drawing.Point(405, 20);
            this.cbChoseData.Name = "cbChoseData";
            this.cbChoseData.Size = new System.Drawing.Size(80, 17);
            this.cbChoseData.TabIndex = 52;
            this.cbChoseData.Text = "Chose data";
            this.cbChoseData.UseVisualStyleBackColor = true;
            this.cbChoseData.CheckedChanged += new System.EventHandler(this.cbChoseData_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(236, 21);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 13);
            this.label7.TabIndex = 51;
            this.label7.Text = "Stock ID";
            // 
            // cmbExpStockID
            // 
            this.cmbExpStockID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbExpStockID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbExpStockID.FormattingEnabled = true;
            this.cmbExpStockID.Location = new System.Drawing.Point(296, 15);
            this.cmbExpStockID.Name = "cmbExpStockID";
            this.cmbExpStockID.Size = new System.Drawing.Size(88, 21);
            this.cmbExpStockID.TabIndex = 50;
            this.cmbExpStockID.SelectedIndexChanged += new System.EventHandler(this.cmbExpStockID_SelectedIndexChanged);
            // 
            // gbResults
            // 
            this.gbResults.Controls.Add(this.wbResults);
            this.gbResults.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbResults.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gbResults.Location = new System.Drawing.Point(403, 109);
            this.gbResults.Name = "gbResults";
            this.gbResults.Size = new System.Drawing.Size(365, 427);
            this.gbResults.TabIndex = 49;
            this.gbResults.TabStop = false;
            this.gbResults.Text = "RESULTS";
            // 
            // wbResults
            // 
            this.wbResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbResults.Location = new System.Drawing.Point(3, 16);
            this.wbResults.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbResults.Name = "wbResults";
            this.wbResults.Size = new System.Drawing.Size(359, 408);
            this.wbResults.TabIndex = 0;
            // 
            // lblExperimentMode
            // 
            this.lblExperimentMode.AutoSize = true;
            this.lblExperimentMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExperimentMode.ForeColor = System.Drawing.Color.Black;
            this.lblExperimentMode.Location = new System.Drawing.Point(16, 18);
            this.lblExperimentMode.Name = "lblExperimentMode";
            this.lblExperimentMode.Size = new System.Drawing.Size(104, 13);
            this.lblExperimentMode.TabIndex = 48;
            this.lblExperimentMode.Text = "Experiment Mode";
            // 
            // gbBatchTrainTest
            // 
            this.gbBatchTrainTest.Controls.Add(this.btnBatchTrainTest);
            this.gbBatchTrainTest.Controls.Add(this.lblBatchInputFile);
            this.gbBatchTrainTest.Controls.Add(this.tbxBatchInputFile);
            this.gbBatchTrainTest.Controls.Add(this.btnBatchBrowse);
            this.gbBatchTrainTest.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbBatchTrainTest.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gbBatchTrainTest.Location = new System.Drawing.Point(403, 46);
            this.gbBatchTrainTest.Name = "gbBatchTrainTest";
            this.gbBatchTrainTest.Size = new System.Drawing.Size(362, 57);
            this.gbBatchTrainTest.TabIndex = 38;
            this.gbBatchTrainTest.TabStop = false;
            this.gbBatchTrainTest.Text = "BATCH TRAINING AND TEST";
            // 
            // btnBatchTrainTest
            // 
            this.btnBatchTrainTest.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBatchTrainTest.ForeColor = System.Drawing.Color.Black;
            this.btnBatchTrainTest.Image = global::GUI.Properties.Resources.doit1;
            this.btnBatchTrainTest.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBatchTrainTest.Location = new System.Drawing.Point(275, 13);
            this.btnBatchTrainTest.Name = "btnBatchTrainTest";
            this.btnBatchTrainTest.Size = new System.Drawing.Size(80, 35);
            this.btnBatchTrainTest.TabIndex = 31;
            this.btnBatchTrainTest.Text = "Do it";
            this.btnBatchTrainTest.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBatchTrainTest.UseVisualStyleBackColor = true;
            this.btnBatchTrainTest.Click += new System.EventHandler(this.btnBatchTrainTest_Click);
            // 
            // lblBatchInputFile
            // 
            this.lblBatchInputFile.AutoSize = true;
            this.lblBatchInputFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBatchInputFile.ForeColor = System.Drawing.Color.Black;
            this.lblBatchInputFile.Location = new System.Drawing.Point(13, 26);
            this.lblBatchInputFile.Name = "lblBatchInputFile";
            this.lblBatchInputFile.Size = new System.Drawing.Size(47, 13);
            this.lblBatchInputFile.TabIndex = 29;
            this.lblBatchInputFile.Text = "Input file";
            // 
            // tbxBatchInputFile
            // 
            this.tbxBatchInputFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxBatchInputFile.ForeColor = System.Drawing.Color.Black;
            this.tbxBatchInputFile.Location = new System.Drawing.Point(66, 22);
            this.tbxBatchInputFile.Name = "tbxBatchInputFile";
            this.tbxBatchInputFile.Size = new System.Drawing.Size(169, 20);
            this.tbxBatchInputFile.TabIndex = 25;
            this.tbxBatchInputFile.Click += new System.EventHandler(this.btnBatchBrowse_Click);
            // 
            // btnBatchBrowse
            // 
            this.btnBatchBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBatchBrowse.ForeColor = System.Drawing.Color.Black;
            this.btnBatchBrowse.Image = ((System.Drawing.Image)(resources.GetObject("btnBatchBrowse.Image")));
            this.btnBatchBrowse.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBatchBrowse.Location = new System.Drawing.Point(241, 19);
            this.btnBatchBrowse.Name = "btnBatchBrowse";
            this.btnBatchBrowse.Size = new System.Drawing.Size(24, 24);
            this.btnBatchBrowse.TabIndex = 26;
            this.btnBatchBrowse.UseVisualStyleBackColor = true;
            this.btnBatchBrowse.Click += new System.EventHandler(this.btnBatchBrowse_Click);
            // 
            // gbTest
            // 
            this.gbTest.Controls.Add(this.lblModelFile);
            this.gbTest.Controls.Add(this.btnModelBrowser);
            this.gbTest.Controls.Add(this.tbxModelFilePath);
            this.gbTest.Controls.Add(this.lblTestFile);
            this.gbTest.Controls.Add(this.btnTestBrowser);
            this.gbTest.Controls.Add(this.tbxTestFilePath);
            this.gbTest.Controls.Add(this.btnTest);
            this.gbTest.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbTest.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gbTest.Location = new System.Drawing.Point(15, 466);
            this.gbTest.Name = "gbTest";
            this.gbTest.Size = new System.Drawing.Size(369, 70);
            this.gbTest.TabIndex = 24;
            this.gbTest.TabStop = false;
            this.gbTest.Text = "3. TEST";
            // 
            // lblModelFile
            // 
            this.lblModelFile.AutoSize = true;
            this.lblModelFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblModelFile.ForeColor = System.Drawing.Color.Black;
            this.lblModelFile.Location = new System.Drawing.Point(6, 46);
            this.lblModelFile.Name = "lblModelFile";
            this.lblModelFile.Size = new System.Drawing.Size(52, 13);
            this.lblModelFile.TabIndex = 34;
            this.lblModelFile.Text = "Model file";
            // 
            // btnModelBrowser
            // 
            this.btnModelBrowser.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModelBrowser.ForeColor = System.Drawing.Color.Black;
            this.btnModelBrowser.Image = ((System.Drawing.Image)(resources.GetObject("btnModelBrowser.Image")));
            this.btnModelBrowser.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnModelBrowser.Location = new System.Drawing.Point(244, 40);
            this.btnModelBrowser.Name = "btnModelBrowser";
            this.btnModelBrowser.Size = new System.Drawing.Size(24, 24);
            this.btnModelBrowser.TabIndex = 33;
            this.btnModelBrowser.UseVisualStyleBackColor = true;
            this.btnModelBrowser.Click += new System.EventHandler(this.btnModelBrowser_Click);
            // 
            // tbxModelFilePath
            // 
            this.tbxModelFilePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxModelFilePath.Location = new System.Drawing.Point(70, 42);
            this.tbxModelFilePath.Name = "tbxModelFilePath";
            this.tbxModelFilePath.Size = new System.Drawing.Size(169, 20);
            this.tbxModelFilePath.TabIndex = 32;
            // 
            // lblTestFile
            // 
            this.lblTestFile.AutoSize = true;
            this.lblTestFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTestFile.ForeColor = System.Drawing.Color.Black;
            this.lblTestFile.Location = new System.Drawing.Point(5, 19);
            this.lblTestFile.Name = "lblTestFile";
            this.lblTestFile.Size = new System.Drawing.Size(44, 13);
            this.lblTestFile.TabIndex = 31;
            this.lblTestFile.Text = "Test file";
            // 
            // btnTestBrowser
            // 
            this.btnTestBrowser.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTestBrowser.ForeColor = System.Drawing.Color.Black;
            this.btnTestBrowser.Image = ((System.Drawing.Image)(resources.GetObject("btnTestBrowser.Image")));
            this.btnTestBrowser.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTestBrowser.Location = new System.Drawing.Point(244, 13);
            this.btnTestBrowser.Name = "btnTestBrowser";
            this.btnTestBrowser.Size = new System.Drawing.Size(24, 24);
            this.btnTestBrowser.TabIndex = 30;
            this.btnTestBrowser.UseVisualStyleBackColor = true;
            this.btnTestBrowser.Click += new System.EventHandler(this.btnTestBrowser_Click);
            // 
            // tbxTestFilePath
            // 
            this.tbxTestFilePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxTestFilePath.Location = new System.Drawing.Point(70, 15);
            this.tbxTestFilePath.Name = "tbxTestFilePath";
            this.tbxTestFilePath.Size = new System.Drawing.Size(169, 20);
            this.tbxTestFilePath.TabIndex = 29;
            // 
            // btnTest
            // 
            this.btnTest.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTest.ForeColor = System.Drawing.Color.Black;
            this.btnTest.Image = global::GUI.Properties.Resources.Test;
            this.btnTest.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTest.Location = new System.Drawing.Point(281, 22);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(80, 35);
            this.btnTest.TabIndex = 6;
            this.btnTest.Text = "Test";
            this.btnTest.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // cmbExperimentMode
            // 
            this.cmbExperimentMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbExperimentMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbExperimentMode.FormattingEnabled = true;
            this.cmbExperimentMode.Items.AddRange(new object[] {
            "Step-by-step",
            "Batch"});
            this.cmbExperimentMode.Location = new System.Drawing.Point(125, 15);
            this.cmbExperimentMode.Name = "cmbExperimentMode";
            this.cmbExperimentMode.Size = new System.Drawing.Size(105, 21);
            this.cmbExperimentMode.TabIndex = 47;
            this.cmbExperimentMode.SelectedIndexChanged += new System.EventHandler(this.cmbExperimentMode_SelectedIndexChanged);
            // 
            // gbTraining
            // 
            this.gbTraining.Controls.Add(this.lblTrainingFile);
            this.gbTraining.Controls.Add(this.btnTrainBrowser);
            this.gbTraining.Controls.Add(this.tbxTrainFilePath);
            this.gbTraining.Controls.Add(this.btnTrain);
            this.gbTraining.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbTraining.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gbTraining.Location = new System.Drawing.Point(14, 396);
            this.gbTraining.Name = "gbTraining";
            this.gbTraining.Size = new System.Drawing.Size(370, 58);
            this.gbTraining.TabIndex = 23;
            this.gbTraining.TabStop = false;
            this.gbTraining.Text = "2. TRAINING";
            // 
            // lblTrainingFile
            // 
            this.lblTrainingFile.AutoSize = true;
            this.lblTrainingFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrainingFile.ForeColor = System.Drawing.Color.Black;
            this.lblTrainingFile.Location = new System.Drawing.Point(8, 26);
            this.lblTrainingFile.Name = "lblTrainingFile";
            this.lblTrainingFile.Size = new System.Drawing.Size(61, 13);
            this.lblTrainingFile.TabIndex = 28;
            this.lblTrainingFile.Text = "Training file";
            // 
            // btnTrainBrowser
            // 
            this.btnTrainBrowser.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTrainBrowser.ForeColor = System.Drawing.Color.Black;
            this.btnTrainBrowser.Image = ((System.Drawing.Image)(resources.GetObject("btnTrainBrowser.Image")));
            this.btnTrainBrowser.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTrainBrowser.Location = new System.Drawing.Point(244, 19);
            this.btnTrainBrowser.Name = "btnTrainBrowser";
            this.btnTrainBrowser.Size = new System.Drawing.Size(24, 24);
            this.btnTrainBrowser.TabIndex = 27;
            this.btnTrainBrowser.UseVisualStyleBackColor = true;
            this.btnTrainBrowser.Click += new System.EventHandler(this.btnTrainBrowser_Click);
            // 
            // tbxTrainFilePath
            // 
            this.tbxTrainFilePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxTrainFilePath.Location = new System.Drawing.Point(70, 22);
            this.tbxTrainFilePath.Name = "tbxTrainFilePath";
            this.tbxTrainFilePath.Size = new System.Drawing.Size(169, 20);
            this.tbxTrainFilePath.TabIndex = 26;
            // 
            // btnTrain
            // 
            this.btnTrain.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTrain.ForeColor = System.Drawing.Color.Black;
            this.btnTrain.Image = global::GUI.Properties.Resources.Train;
            this.btnTrain.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTrain.Location = new System.Drawing.Point(281, 15);
            this.btnTrain.Name = "btnTrain";
            this.btnTrain.Size = new System.Drawing.Size(80, 35);
            this.btnTrain.TabIndex = 6;
            this.btnTrain.Text = "Train";
            this.btnTrain.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnTrain.UseVisualStyleBackColor = true;
            this.btnTrain.Click += new System.EventHandler(this.btnTrain_Click);
            // 
            // gbPreprocess
            // 
            this.gbPreprocess.Controls.Add(this.lblInputFile);
            this.gbPreprocess.Controls.Add(this.btnPreprocess);
            this.gbPreprocess.Controls.Add(this.tbxCsvFilePath);
            this.gbPreprocess.Controls.Add(this.btnPreprocessBrowser);
            this.gbPreprocess.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbPreprocess.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gbPreprocess.Location = new System.Drawing.Point(14, 326);
            this.gbPreprocess.Name = "gbPreprocess";
            this.gbPreprocess.Size = new System.Drawing.Size(370, 58);
            this.gbPreprocess.TabIndex = 25;
            this.gbPreprocess.TabStop = false;
            this.gbPreprocess.Text = "1. DATA PREPROCESS";
            // 
            // lblInputFile
            // 
            this.lblInputFile.AutoSize = true;
            this.lblInputFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInputFile.ForeColor = System.Drawing.Color.Black;
            this.lblInputFile.Location = new System.Drawing.Point(11, 25);
            this.lblInputFile.Name = "lblInputFile";
            this.lblInputFile.Size = new System.Drawing.Size(47, 13);
            this.lblInputFile.TabIndex = 24;
            this.lblInputFile.Text = "Input file";
            // 
            // btnPreprocess
            // 
            this.btnPreprocess.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPreprocess.ForeColor = System.Drawing.Color.Black;
            this.btnPreprocess.Image = global::GUI.Properties.Resources.preprocess1;
            this.btnPreprocess.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPreprocess.Location = new System.Drawing.Point(281, 15);
            this.btnPreprocess.Name = "btnPreprocess";
            this.btnPreprocess.Size = new System.Drawing.Size(80, 35);
            this.btnPreprocess.TabIndex = 7;
            this.btnPreprocess.Text = "  Preprocess";
            this.btnPreprocess.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPreprocess.UseVisualStyleBackColor = true;
            this.btnPreprocess.Click += new System.EventHandler(this.btnPreprocess_Click);
            // 
            // tbxCsvFilePath
            // 
            this.tbxCsvFilePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxCsvFilePath.ForeColor = System.Drawing.Color.Black;
            this.tbxCsvFilePath.Location = new System.Drawing.Point(70, 22);
            this.tbxCsvFilePath.Name = "tbxCsvFilePath";
            this.tbxCsvFilePath.Size = new System.Drawing.Size(169, 20);
            this.tbxCsvFilePath.TabIndex = 18;
            this.tbxCsvFilePath.Click += new System.EventHandler(this.btnPreprocessBrowser_Click);
            // 
            // btnPreprocessBrowser
            // 
            this.btnPreprocessBrowser.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPreprocessBrowser.ForeColor = System.Drawing.Color.Black;
            this.btnPreprocessBrowser.Image = ((System.Drawing.Image)(resources.GetObject("btnPreprocessBrowser.Image")));
            this.btnPreprocessBrowser.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPreprocessBrowser.Location = new System.Drawing.Point(244, 20);
            this.btnPreprocessBrowser.Name = "btnPreprocessBrowser";
            this.btnPreprocessBrowser.Size = new System.Drawing.Size(24, 24);
            this.btnPreprocessBrowser.TabIndex = 19;
            this.btnPreprocessBrowser.UseVisualStyleBackColor = true;
            this.btnPreprocessBrowser.Click += new System.EventHandler(this.btnPreprocessBrowser_Click);
            // 
            // gbModelChoice
            // 
            this.gbModelChoice.Controls.Add(this.rdDTANN);
            this.gbModelChoice.Controls.Add(this.tabCtrlSettings);
            this.gbModelChoice.Controls.Add(this.cmbNumDaysPredicted);
            this.gbModelChoice.Controls.Add(this.tbxTrainingRatio);
            this.gbModelChoice.Controls.Add(this.lblTrainingRatio);
            this.gbModelChoice.Controls.Add(this.rdSVM);
            this.gbModelChoice.Controls.Add(this.rdANN);
            this.gbModelChoice.Controls.Add(this.label10);
            this.gbModelChoice.Controls.Add(this.rdKSVMeans);
            this.gbModelChoice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbModelChoice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.gbModelChoice.Location = new System.Drawing.Point(14, 47);
            this.gbModelChoice.Name = "gbModelChoice";
            this.gbModelChoice.Size = new System.Drawing.Size(370, 269);
            this.gbModelChoice.TabIndex = 19;
            this.gbModelChoice.TabStop = false;
            this.gbModelChoice.Text = "MODEL CHOICE";
            // 
            // rdDTANN
            // 
            this.rdDTANN.AutoSize = true;
            this.rdDTANN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdDTANN.ForeColor = System.Drawing.Color.Black;
            this.rdDTANN.Location = new System.Drawing.Point(86, 25);
            this.rdDTANN.Name = "rdDTANN";
            this.rdDTANN.Size = new System.Drawing.Size(80, 17);
            this.rdDTANN.TabIndex = 31;
            this.rdDTANN.Text = "DT - ANN";
            this.rdDTANN.UseVisualStyleBackColor = true;
            this.rdDTANN.CheckedChanged += new System.EventHandler(this.rdDT_ANN_CheckedChanged);
            // 
            // tabCtrlSettings
            // 
            this.tabCtrlSettings.Controls.Add(this.tabDT_ANN);
            this.tabCtrlSettings.Controls.Add(this.tabSVM_KMeans);
            this.tabCtrlSettings.Location = new System.Drawing.Point(7, 80);
            this.tabCtrlSettings.Name = "tabCtrlSettings";
            this.tabCtrlSettings.SelectedIndex = 0;
            this.tabCtrlSettings.Size = new System.Drawing.Size(355, 184);
            this.tabCtrlSettings.TabIndex = 49;
            // 
            // tabDT_ANN
            // 
            this.tabDT_ANN.BackColor = System.Drawing.SystemColors.Control;
            this.tabDT_ANN.Controls.Add(this.gbAnnSetting);
            this.tabDT_ANN.Controls.Add(this.gbDTSetting);
            this.tabDT_ANN.Location = new System.Drawing.Point(4, 22);
            this.tabDT_ANN.Name = "tabDT_ANN";
            this.tabDT_ANN.Padding = new System.Windows.Forms.Padding(3);
            this.tabDT_ANN.Size = new System.Drawing.Size(347, 158);
            this.tabDT_ANN.TabIndex = 0;
            this.tabDT_ANN.Text = "ANN";
            // 
            // gbAnnSetting
            // 
            this.gbAnnSetting.Controls.Add(this.cmbActivationFunc);
            this.gbAnnSetting.Controls.Add(this.lblActivationFunc);
            this.gbAnnSetting.Controls.Add(this.label17);
            this.gbAnnSetting.Controls.Add(this.tbxANNHiddenNode);
            this.gbAnnSetting.Controls.Add(this.tbxMomentum);
            this.gbAnnSetting.Controls.Add(this.label14);
            this.gbAnnSetting.Controls.Add(this.tbxMaxLoops);
            this.gbAnnSetting.Controls.Add(this.label11);
            this.gbAnnSetting.Controls.Add(this.tbxBias);
            this.gbAnnSetting.Controls.Add(this.label12);
            this.gbAnnSetting.Controls.Add(this.tbxLearningRate);
            this.gbAnnSetting.Controls.Add(this.label13);
            this.gbAnnSetting.ForeColor = System.Drawing.Color.Green;
            this.gbAnnSetting.Location = new System.Drawing.Point(6, 6);
            this.gbAnnSetting.Name = "gbAnnSetting";
            this.gbAnnSetting.Size = new System.Drawing.Size(335, 95);
            this.gbAnnSetting.TabIndex = 23;
            this.gbAnnSetting.TabStop = false;
            this.gbAnnSetting.Text = "ANN Settings";
            // 
            // cmbActivationFunc
            // 
            this.cmbActivationFunc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbActivationFunc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbActivationFunc.FormattingEnabled = true;
            this.cmbActivationFunc.Items.AddRange(new object[] {
            "Tanh",
            "Sigmoid",
            "Logarithm",
            "Sine"});
            this.cmbActivationFunc.Location = new System.Drawing.Point(100, 17);
            this.cmbActivationFunc.Name = "cmbActivationFunc";
            this.cmbActivationFunc.Size = new System.Drawing.Size(74, 21);
            this.cmbActivationFunc.TabIndex = 41;
            // 
            // lblActivationFunc
            // 
            this.lblActivationFunc.AutoSize = true;
            this.lblActivationFunc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActivationFunc.ForeColor = System.Drawing.Color.Black;
            this.lblActivationFunc.Location = new System.Drawing.Point(6, 21);
            this.lblActivationFunc.Name = "lblActivationFunc";
            this.lblActivationFunc.Size = new System.Drawing.Size(81, 13);
            this.lblActivationFunc.TabIndex = 40;
            this.lblActivationFunc.Text = "Activation Func";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.Black;
            this.label17.Location = new System.Drawing.Point(6, 46);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(91, 13);
            this.label17.TabIndex = 37;
            this.label17.Text = "Num hidden node";
            // 
            // tbxANNHiddenNode
            // 
            this.tbxANNHiddenNode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxANNHiddenNode.ForeColor = System.Drawing.Color.Black;
            this.tbxANNHiddenNode.Location = new System.Drawing.Point(100, 42);
            this.tbxANNHiddenNode.Name = "tbxANNHiddenNode";
            this.tbxANNHiddenNode.Size = new System.Drawing.Size(74, 20);
            this.tbxANNHiddenNode.TabIndex = 36;
            // 
            // tbxMomentum
            // 
            this.tbxMomentum.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxMomentum.ForeColor = System.Drawing.Color.Black;
            this.tbxMomentum.Location = new System.Drawing.Point(250, 67);
            this.tbxMomentum.Name = "tbxMomentum";
            this.tbxMomentum.Size = new System.Drawing.Size(74, 20);
            this.tbxMomentum.TabIndex = 33;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.Location = new System.Drawing.Point(186, 71);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(59, 13);
            this.label14.TabIndex = 32;
            this.label14.Text = "Momentum";
            // 
            // tbxMaxLoops
            // 
            this.tbxMaxLoops.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxMaxLoops.ForeColor = System.Drawing.Color.Black;
            this.tbxMaxLoops.Location = new System.Drawing.Point(250, 17);
            this.tbxMaxLoops.Name = "tbxMaxLoops";
            this.tbxMaxLoops.Size = new System.Drawing.Size(74, 20);
            this.tbxMaxLoops.TabIndex = 31;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(186, 21);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(55, 13);
            this.label11.TabIndex = 30;
            this.label11.Text = "Max loops";
            // 
            // tbxBias
            // 
            this.tbxBias.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxBias.ForeColor = System.Drawing.Color.Black;
            this.tbxBias.Location = new System.Drawing.Point(250, 42);
            this.tbxBias.Name = "tbxBias";
            this.tbxBias.Size = new System.Drawing.Size(74, 20);
            this.tbxBias.TabIndex = 29;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(186, 46);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(27, 13);
            this.label12.TabIndex = 28;
            this.label12.Text = "Bias";
            // 
            // tbxLearningRate
            // 
            this.tbxLearningRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxLearningRate.ForeColor = System.Drawing.Color.Black;
            this.tbxLearningRate.Location = new System.Drawing.Point(100, 67);
            this.tbxLearningRate.Name = "tbxLearningRate";
            this.tbxLearningRate.Size = new System.Drawing.Size(74, 20);
            this.tbxLearningRate.TabIndex = 27;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(6, 71);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(69, 13);
            this.label13.TabIndex = 26;
            this.label13.Text = "Learning rate";
            // 
            // gbDTSetting
            // 
            this.gbDTSetting.Controls.Add(this.cmbPruneFunc);
            this.gbDTSetting.Controls.Add(this.cmbSplitFunc);
            this.gbDTSetting.Controls.Add(this.label4);
            this.gbDTSetting.Controls.Add(this.label5);
            this.gbDTSetting.ForeColor = System.Drawing.Color.Green;
            this.gbDTSetting.Location = new System.Drawing.Point(6, 106);
            this.gbDTSetting.Name = "gbDTSetting";
            this.gbDTSetting.Size = new System.Drawing.Size(335, 47);
            this.gbDTSetting.TabIndex = 24;
            this.gbDTSetting.TabStop = false;
            this.gbDTSetting.Text = "Decision Tree Settings";
            // 
            // cmbPruneFunc
            // 
            this.cmbPruneFunc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPruneFunc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPruneFunc.FormattingEnabled = true;
            this.cmbPruneFunc.Items.AddRange(new object[] {
            "Reduced-error",
            "Pessimistic"});
            this.cmbPruneFunc.Location = new System.Drawing.Point(237, 18);
            this.cmbPruneFunc.Name = "cmbPruneFunc";
            this.cmbPruneFunc.Size = new System.Drawing.Size(92, 21);
            this.cmbPruneFunc.TabIndex = 42;
            // 
            // cmbSplitFunc
            // 
            this.cmbSplitFunc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSplitFunc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSplitFunc.FormattingEnabled = true;
            this.cmbSplitFunc.Items.AddRange(new object[] {
            "Gain Ratio",
            "Gain",
            "GINI",
            "Random"});
            this.cmbSplitFunc.Location = new System.Drawing.Point(76, 18);
            this.cmbSplitFunc.Name = "cmbSplitFunc";
            this.cmbSplitFunc.Size = new System.Drawing.Size(85, 21);
            this.cmbSplitFunc.TabIndex = 41;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(3, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 40;
            this.label4.Text = "Splitting Func";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(171, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 37;
            this.label5.Text = "Pruning Alg";
            // 
            // tabSVM_KMeans
            // 
            this.tabSVM_KMeans.BackColor = System.Drawing.SystemColors.Control;
            this.tabSVM_KMeans.Controls.Add(this.gbKmeansSetting);
            this.tabSVM_KMeans.Controls.Add(this.gbSVRSetting);
            this.tabSVM_KMeans.Location = new System.Drawing.Point(4, 22);
            this.tabSVM_KMeans.Name = "tabSVM_KMeans";
            this.tabSVM_KMeans.Padding = new System.Windows.Forms.Padding(3);
            this.tabSVM_KMeans.Size = new System.Drawing.Size(347, 158);
            this.tabSVM_KMeans.TabIndex = 1;
            this.tabSVM_KMeans.Text = "SVM";
            // 
            // gbKmeansSetting
            // 
            this.gbKmeansSetting.Controls.Add(this.cmbDistanceType);
            this.gbKmeansSetting.Controls.Add(this.lblDistanceType);
            this.gbKmeansSetting.Controls.Add(this.nmNumCluster);
            this.gbKmeansSetting.Controls.Add(this.lblNumCluster);
            this.gbKmeansSetting.ForeColor = System.Drawing.Color.Green;
            this.gbKmeansSetting.Location = new System.Drawing.Point(6, 106);
            this.gbKmeansSetting.Name = "gbKmeansSetting";
            this.gbKmeansSetting.Size = new System.Drawing.Size(335, 46);
            this.gbKmeansSetting.TabIndex = 32;
            this.gbKmeansSetting.TabStop = false;
            this.gbKmeansSetting.Text = "Kmeans Setting";
            // 
            // cmbDistanceType
            // 
            this.cmbDistanceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDistanceType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDistanceType.FormattingEnabled = true;
            this.cmbDistanceType.Items.AddRange(new object[] {
            "Manhattan",
            "Euclid"});
            this.cmbDistanceType.Location = new System.Drawing.Point(224, 17);
            this.cmbDistanceType.Name = "cmbDistanceType";
            this.cmbDistanceType.Size = new System.Drawing.Size(105, 21);
            this.cmbDistanceType.TabIndex = 41;
            // 
            // lblDistanceType
            // 
            this.lblDistanceType.AutoSize = true;
            this.lblDistanceType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDistanceType.ForeColor = System.Drawing.Color.Black;
            this.lblDistanceType.Location = new System.Drawing.Point(139, 22);
            this.lblDistanceType.Name = "lblDistanceType";
            this.lblDistanceType.Size = new System.Drawing.Size(76, 13);
            this.lblDistanceType.TabIndex = 40;
            this.lblDistanceType.Text = "Distance Type";
            // 
            // nmNumCluster
            // 
            this.nmNumCluster.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nmNumCluster.Location = new System.Drawing.Point(87, 17);
            this.nmNumCluster.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nmNumCluster.Name = "nmNumCluster";
            this.nmNumCluster.Size = new System.Drawing.Size(42, 20);
            this.nmNumCluster.TabIndex = 33;
            this.nmNumCluster.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // lblNumCluster
            // 
            this.lblNumCluster.AutoSize = true;
            this.lblNumCluster.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumCluster.ForeColor = System.Drawing.Color.Black;
            this.lblNumCluster.Location = new System.Drawing.Point(6, 22);
            this.lblNumCluster.Name = "lblNumCluster";
            this.lblNumCluster.Size = new System.Drawing.Size(76, 13);
            this.lblNumCluster.TabIndex = 32;
            this.lblNumCluster.Text = "Num of Cluster";
            // 
            // gbSVRSetting
            // 
            this.gbSVRSetting.BackColor = System.Drawing.SystemColors.Control;
            this.gbSVRSetting.Controls.Add(this.ckbProbEstimate);
            this.gbSVRSetting.Controls.Add(this.label3);
            this.gbSVRSetting.Controls.Add(this.tbxGamma);
            this.gbSVRSetting.Controls.Add(this.label2);
            this.gbSVRSetting.Controls.Add(this.tbxC);
            this.gbSVRSetting.Controls.Add(this.tbxNumFold);
            this.gbSVRSetting.Controls.Add(this.lblNumFold);
            this.gbSVRSetting.Controls.Add(this.cmbModelSelection);
            this.gbSVRSetting.Controls.Add(this.label6);
            this.gbSVRSetting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbSVRSetting.ForeColor = System.Drawing.Color.Green;
            this.gbSVRSetting.Location = new System.Drawing.Point(6, 6);
            this.gbSVRSetting.Name = "gbSVRSetting";
            this.gbSVRSetting.Size = new System.Drawing.Size(335, 94);
            this.gbSVRSetting.TabIndex = 44;
            this.gbSVRSetting.TabStop = false;
            this.gbSVRSetting.Text = "SVM Settings";
            // 
            // ckbProbEstimate
            // 
            this.ckbProbEstimate.AutoSize = true;
            this.ckbProbEstimate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckbProbEstimate.ForeColor = System.Drawing.Color.Black;
            this.ckbProbEstimate.Location = new System.Drawing.Point(9, 22);
            this.ckbProbEstimate.Name = "ckbProbEstimate";
            this.ckbProbEstimate.Size = new System.Drawing.Size(122, 17);
            this.ckbProbEstimate.TabIndex = 47;
            this.ckbProbEstimate.Text = "Probability Estimates";
            this.ckbProbEstimate.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(188, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 46;
            this.label3.Text = "Gamma = 2^";
            // 
            // tbxGamma
            // 
            this.tbxGamma.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxGamma.Location = new System.Drawing.Point(261, 67);
            this.tbxGamma.Name = "tbxGamma";
            this.tbxGamma.Size = new System.Drawing.Size(64, 20);
            this.tbxGamma.TabIndex = 45;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(188, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 44;
            this.label2.Text = "C = 2^";
            // 
            // tbxC
            // 
            this.tbxC.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxC.Location = new System.Drawing.Point(261, 41);
            this.tbxC.Name = "tbxC";
            this.tbxC.Size = new System.Drawing.Size(64, 20);
            this.tbxC.TabIndex = 43;
            // 
            // tbxNumFold
            // 
            this.tbxNumFold.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxNumFold.ForeColor = System.Drawing.Color.Black;
            this.tbxNumFold.Location = new System.Drawing.Point(87, 67);
            this.tbxNumFold.Name = "tbxNumFold";
            this.tbxNumFold.Size = new System.Drawing.Size(85, 20);
            this.tbxNumFold.TabIndex = 38;
            this.tbxNumFold.Text = "5";
            // 
            // lblNumFold
            // 
            this.lblNumFold.AutoSize = true;
            this.lblNumFold.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumFold.ForeColor = System.Drawing.Color.Black;
            this.lblNumFold.Location = new System.Drawing.Point(5, 71);
            this.lblNumFold.Name = "lblNumFold";
            this.lblNumFold.Size = new System.Drawing.Size(49, 13);
            this.lblNumFold.TabIndex = 21;
            this.lblNumFold.Text = "Num fold";
            // 
            // cmbModelSelection
            // 
            this.cmbModelSelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbModelSelection.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbModelSelection.ForeColor = System.Drawing.Color.Black;
            this.cmbModelSelection.FormattingEnabled = true;
            this.cmbModelSelection.Items.AddRange(new object[] {
            "Grid search",
            "Use default values"});
            this.cmbModelSelection.Location = new System.Drawing.Point(87, 41);
            this.cmbModelSelection.Name = "cmbModelSelection";
            this.cmbModelSelection.Size = new System.Drawing.Size(85, 21);
            this.cmbModelSelection.TabIndex = 20;
            this.cmbModelSelection.SelectedIndexChanged += new System.EventHandler(this.cmbModelSelection_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(5, 45);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "Param selection";
            // 
            // cmbNumDaysPredicted
            // 
            this.cmbNumDaysPredicted.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNumDaysPredicted.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbNumDaysPredicted.FormattingEnabled = true;
            this.cmbNumDaysPredicted.Items.AddRange(new object[] {
            "1",
            "5"});
            this.cmbNumDaysPredicted.Location = new System.Drawing.Point(113, 49);
            this.cmbNumDaysPredicted.Name = "cmbNumDaysPredicted";
            this.cmbNumDaysPredicted.Size = new System.Drawing.Size(37, 21);
            this.cmbNumDaysPredicted.TabIndex = 46;
            // 
            // tbxTrainingRatio
            // 
            this.tbxTrainingRatio.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxTrainingRatio.Location = new System.Drawing.Point(287, 49);
            this.tbxTrainingRatio.Name = "tbxTrainingRatio";
            this.tbxTrainingRatio.Size = new System.Drawing.Size(65, 20);
            this.tbxTrainingRatio.TabIndex = 23;
            // 
            // lblTrainingRatio
            // 
            this.lblTrainingRatio.AutoSize = true;
            this.lblTrainingRatio.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrainingRatio.ForeColor = System.Drawing.Color.Black;
            this.lblTrainingRatio.Location = new System.Drawing.Point(174, 53);
            this.lblTrainingRatio.Name = "lblTrainingRatio";
            this.lblTrainingRatio.Size = new System.Drawing.Size(102, 13);
            this.lblTrainingRatio.TabIndex = 22;
            this.lblTrainingRatio.Text = "Training set ratio (%)";
            // 
            // rdSVM
            // 
            this.rdSVM.AutoSize = true;
            this.rdSVM.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdSVM.ForeColor = System.Drawing.Color.Black;
            this.rdSVM.Location = new System.Drawing.Point(191, 25);
            this.rdSVM.Name = "rdSVM";
            this.rdSVM.Size = new System.Drawing.Size(51, 17);
            this.rdSVM.TabIndex = 1;
            this.rdSVM.Text = "SVM";
            this.rdSVM.UseVisualStyleBackColor = true;
            this.rdSVM.CheckedChanged += new System.EventHandler(this.rdSVM_CheckedChanged);
            // 
            // rdANN
            // 
            this.rdANN.AutoSize = true;
            this.rdANN.Checked = true;
            this.rdANN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdANN.ForeColor = System.Drawing.Color.Black;
            this.rdANN.Location = new System.Drawing.Point(10, 26);
            this.rdANN.Name = "rdANN";
            this.rdANN.Size = new System.Drawing.Size(51, 17);
            this.rdANN.TabIndex = 0;
            this.rdANN.TabStop = true;
            this.rdANN.Text = "ANN";
            this.rdANN.UseVisualStyleBackColor = true;
            this.rdANN.CheckedChanged += new System.EventHandler(this.rdANN_CheckedChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(9, 53);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(96, 13);
            this.label10.TabIndex = 40;
            this.label10.Text = "Num day predicted";
            // 
            // rdKSVMeans
            // 
            this.rdKSVMeans.AutoSize = true;
            this.rdKSVMeans.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdKSVMeans.ForeColor = System.Drawing.Color.Black;
            this.rdKSVMeans.Location = new System.Drawing.Point(267, 24);
            this.rdKSVMeans.Name = "rdKSVMeans";
            this.rdKSVMeans.Size = new System.Drawing.Size(90, 17);
            this.rdKSVMeans.TabIndex = 30;
            this.rdKSVMeans.Text = "K-SVMeans";
            this.rdKSVMeans.UseVisualStyleBackColor = true;
            this.rdKSVMeans.CheckedChanged += new System.EventHandler(this.rdKSVMeans_CheckedChanged);
            // 
            // tabApplication
            // 
            this.tabApplication.BackColor = System.Drawing.Color.Transparent;
            this.tabApplication.Controls.Add(this.wbSAResult);
            this.tabApplication.Controls.Add(this.groupBox1);
            this.tabApplication.Controls.Add(this.gbStockChart);
            this.tabApplication.Location = new System.Drawing.Point(4, 22);
            this.tabApplication.Name = "tabApplication";
            this.tabApplication.Padding = new System.Windows.Forms.Padding(3);
            this.tabApplication.Size = new System.Drawing.Size(776, 544);
            this.tabApplication.TabIndex = 1;
            this.tabApplication.Text = "APPLICATION";
            // 
            // wbSAResult
            // 
            this.wbSAResult.Location = new System.Drawing.Point(277, 403);
            this.wbSAResult.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbSAResult.Name = "wbSAResult";
            this.wbSAResult.Size = new System.Drawing.Size(485, 129);
            this.wbSAResult.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnUpdateData);
            this.groupBox1.Controls.Add(this.dtpChoseCurrentDate);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.btnSAPredict);
            this.groupBox1.Controls.Add(this.cbSAFiveDay);
            this.groupBox1.Controls.Add(this.cbSAOneDay);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.cmbSAStockID);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.groupBox1.Location = new System.Drawing.Point(3, 384);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(765, 154);
            this.groupBox1.TabIndex = 31;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Stock Advice";
            // 
            // btnUpdateData
            // 
            this.btnUpdateData.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateData.ForeColor = System.Drawing.SystemColors.Desktop;
            this.btnUpdateData.Location = new System.Drawing.Point(8, 125);
            this.btnUpdateData.Name = "btnUpdateData";
            this.btnUpdateData.Size = new System.Drawing.Size(111, 23);
            this.btnUpdateData.TabIndex = 30;
            this.btnUpdateData.Text = "Update stock data";
            this.btnUpdateData.UseVisualStyleBackColor = true;
            this.btnUpdateData.Click += new System.EventHandler(this.btnUpdateData_Click);
            // 
            // dtpChoseCurrentDate
            // 
            this.dtpChoseCurrentDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpChoseCurrentDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpChoseCurrentDate.Location = new System.Drawing.Point(138, 63);
            this.dtpChoseCurrentDate.Name = "dtpChoseCurrentDate";
            this.dtpChoseCurrentDate.Size = new System.Drawing.Size(114, 20);
            this.dtpChoseCurrentDate.TabIndex = 30;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label15.Location = new System.Drawing.Point(7, 67);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(97, 13);
            this.label15.TabIndex = 30;
            this.label15.Text = "Chose current date";
            // 
            // btnSAPredict
            // 
            this.btnSAPredict.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSAPredict.ForeColor = System.Drawing.SystemColors.Desktop;
            this.btnSAPredict.Location = new System.Drawing.Point(177, 125);
            this.btnSAPredict.Name = "btnSAPredict";
            this.btnSAPredict.Size = new System.Drawing.Size(75, 23);
            this.btnSAPredict.TabIndex = 5;
            this.btnSAPredict.Text = "Predict";
            this.btnSAPredict.UseVisualStyleBackColor = true;
            this.btnSAPredict.Click += new System.EventHandler(this.btnSAPredict_Click);
            // 
            // cbSAFiveDay
            // 
            this.cbSAFiveDay.AutoSize = true;
            this.cbSAFiveDay.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSAFiveDay.ForeColor = System.Drawing.SystemColors.Desktop;
            this.cbSAFiveDay.Location = new System.Drawing.Point(199, 95);
            this.cbSAFiveDay.Name = "cbSAFiveDay";
            this.cbSAFiveDay.Size = new System.Drawing.Size(59, 17);
            this.cbSAFiveDay.TabIndex = 4;
            this.cbSAFiveDay.Text = "5 Days";
            this.cbSAFiveDay.UseVisualStyleBackColor = true;
            // 
            // cbSAOneDay
            // 
            this.cbSAOneDay.AutoSize = true;
            this.cbSAOneDay.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSAOneDay.ForeColor = System.Drawing.SystemColors.Desktop;
            this.cbSAOneDay.Location = new System.Drawing.Point(138, 95);
            this.cbSAOneDay.Name = "cbSAOneDay";
            this.cbSAOneDay.Size = new System.Drawing.Size(54, 17);
            this.cbSAOneDay.TabIndex = 3;
            this.cbSAOneDay.Text = "1 Day";
            this.cbSAOneDay.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label9.Location = new System.Drawing.Point(7, 97);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(114, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "How it\'s trend will be in";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label8.Location = new System.Drawing.Point(7, 34);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(103, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "Chose your stock ID";
            // 
            // cmbSAStockID
            // 
            this.cmbSAStockID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSAStockID.ForeColor = System.Drawing.SystemColors.Desktop;
            this.cmbSAStockID.FormattingEnabled = true;
            this.cmbSAStockID.Items.AddRange(new object[] {
            "AGF",
            "BBC",
            "BMI",
            "BMP",
            "BT6",
            "CII",
            "CYC",
            "DCT",
            "DHA",
            "FMC",
            "FPT",
            "GIL",
            "GMD",
            "HAP",
            "HAS",
            "HTV",
            "ITA",
            "KDC",
            "KHA",
            "KHP",
            "LAF",
            "AGF",
            "BBC",
            "BMI",
            "BMP",
            "BT6",
            "CII",
            "CYC",
            "DCT",
            "DHA",
            "FMC",
            "FPT",
            "GIL",
            "GMD",
            "HAP",
            "HAS",
            "HTV",
            "ITA",
            "KDC",
            "KHA",
            "KHP",
            "LAF",
            "LBM",
            "MCV",
            "MHC",
            "NKD",
            "PGC",
            "PNC",
            "PPC",
            "PVD",
            "RAL",
            "REE",
            "SAM",
            "SAV",
            "SFC",
            "SJS",
            "SMC",
            "SSC",
            "STB",
            "TBC",
            "TDH",
            "TMS",
            "TNA",
            "TRI",
            "TS4",
            "TTP",
            "TYA",
            "VFC",
            "VIP",
            "VNM",
            "VSH",
            "All"});
            this.cmbSAStockID.Location = new System.Drawing.Point(138, 29);
            this.cmbSAStockID.Name = "cmbSAStockID";
            this.cmbSAStockID.Size = new System.Drawing.Size(114, 21);
            this.cmbSAStockID.TabIndex = 0;
            // 
            // gbStockChart
            // 
            this.gbStockChart.Controls.Add(this.zg1);
            this.gbStockChart.Controls.Add(this.lblTo);
            this.gbStockChart.Controls.Add(this.cmbStockID);
            this.gbStockChart.Controls.Add(this.lblFrom);
            this.gbStockChart.Controls.Add(this.dtpFrom);
            this.gbStockChart.Controls.Add(this.lblStockID);
            this.gbStockChart.Controls.Add(this.dtpTo);
            this.gbStockChart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.gbStockChart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gbStockChart.Location = new System.Drawing.Point(3, 6);
            this.gbStockChart.Name = "gbStockChart";
            this.gbStockChart.Size = new System.Drawing.Size(770, 372);
            this.gbStockChart.TabIndex = 30;
            this.gbStockChart.TabStop = false;
            this.gbStockChart.Text = "Stock chart";
            // 
            // zg1
            // 
            this.zg1.EditButtons = System.Windows.Forms.MouseButtons.Left;
            this.zg1.Location = new System.Drawing.Point(6, 47);
            this.zg1.Name = "zg1";
            this.zg1.PanModifierKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.None)));
            this.zg1.ScrollGrace = 0;
            this.zg1.ScrollMaxX = 0;
            this.zg1.ScrollMaxY = 0;
            this.zg1.ScrollMaxY2 = 0;
            this.zg1.ScrollMinX = 0;
            this.zg1.ScrollMinY = 0;
            this.zg1.ScrollMinY2 = 0;
            this.zg1.Size = new System.Drawing.Size(761, 318);
            this.zg1.TabIndex = 1;
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTo.ForeColor = System.Drawing.SystemColors.Desktop;
            this.lblTo.Location = new System.Drawing.Point(336, 22);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(20, 13);
            this.lblTo.TabIndex = 29;
            this.lblTo.Text = "To";
            // 
            // cmbStockID
            // 
            this.cmbStockID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbStockID.FormattingEnabled = true;
            this.cmbStockID.Location = new System.Drawing.Point(59, 15);
            this.cmbStockID.Name = "cmbStockID";
            this.cmbStockID.Size = new System.Drawing.Size(81, 21);
            this.cmbStockID.TabIndex = 23;
            this.cmbStockID.SelectedIndexChanged += new System.EventHandler(this.cmbStockID_SelectedIndexChanged);
            // 
            // lblFrom
            // 
            this.lblFrom.AutoSize = true;
            this.lblFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFrom.ForeColor = System.Drawing.SystemColors.Desktop;
            this.lblFrom.Location = new System.Drawing.Point(169, 22);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(30, 13);
            this.lblFrom.TabIndex = 28;
            this.lblFrom.Text = "From";
            // 
            // dtpFrom
            // 
            this.dtpFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Location = new System.Drawing.Point(224, 16);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(81, 20);
            this.dtpFrom.TabIndex = 24;
            this.dtpFrom.ValueChanged += new System.EventHandler(this.dtpFrom_ValueChanged);
            // 
            // lblStockID
            // 
            this.lblStockID.AutoSize = true;
            this.lblStockID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStockID.ForeColor = System.Drawing.SystemColors.Desktop;
            this.lblStockID.Location = new System.Drawing.Point(4, 22);
            this.lblStockID.Name = "lblStockID";
            this.lblStockID.Size = new System.Drawing.Size(49, 13);
            this.lblStockID.TabIndex = 27;
            this.lblStockID.Text = "Stock ID";
            // 
            // dtpTo
            // 
            this.dtpTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTo.Location = new System.Drawing.Point(391, 16);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(81, 20);
            this.dtpTo.TabIndex = 25;
            this.dtpTo.ValueChanged += new System.EventHandler(this.dtpTo_ValueChanged);
            // 
            // lblBackTitle
            // 
            this.lblBackTitle.BackColor = System.Drawing.Color.Green;
            this.lblBackTitle.ForeColor = System.Drawing.Color.Green;
            this.lblBackTitle.Location = new System.Drawing.Point(0, -2);
            this.lblBackTitle.Name = "lblBackTitle";
            this.lblBackTitle.Size = new System.Drawing.Size(800, 56);
            this.lblBackTitle.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Green;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Image = global::GUI.Properties.Resources.stock;
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(1, -2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(470, 56);
            this.label1.TabIndex = 8;
            this.label1.Text = "STOCK TREND PREDICTION";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // staMainProgress
            // 
            this.staMainProgress.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlsProgressBar,
            this.tlsStatus});
            this.staMainProgress.Location = new System.Drawing.Point(0, 625);
            this.staMainProgress.Name = "staMainProgress";
            this.staMainProgress.Size = new System.Drawing.Size(784, 22);
            this.staMainProgress.TabIndex = 11;
            this.staMainProgress.Text = "staMainProgress";
            // 
            // tlsProgressBar
            // 
            this.tlsProgressBar.Name = "tlsProgressBar";
            this.tlsProgressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // tlsStatus
            // 
            this.tlsStatus.Name = "tlsStatus";
            this.tlsStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // MainGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 647);
            this.Controls.Add(this.staMainProgress);
            this.Controls.Add(this.tabMain);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblBackTitle);
            this.MaximizeBox = false;
            this.Name = "MainGUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stock Trend Prediction";
            this.Load += new System.EventHandler(this.MainGUI_Load);
            this.tabMain.ResumeLayout(false);
            this.tabExperiment.ResumeLayout(false);
            this.tabExperiment.PerformLayout();
            this.gbResults.ResumeLayout(false);
            this.gbBatchTrainTest.ResumeLayout(false);
            this.gbBatchTrainTest.PerformLayout();
            this.gbTest.ResumeLayout(false);
            this.gbTest.PerformLayout();
            this.gbTraining.ResumeLayout(false);
            this.gbTraining.PerformLayout();
            this.gbPreprocess.ResumeLayout(false);
            this.gbPreprocess.PerformLayout();
            this.gbModelChoice.ResumeLayout(false);
            this.gbModelChoice.PerformLayout();
            this.tabCtrlSettings.ResumeLayout(false);
            this.tabDT_ANN.ResumeLayout(false);
            this.gbAnnSetting.ResumeLayout(false);
            this.gbAnnSetting.PerformLayout();
            this.gbDTSetting.ResumeLayout(false);
            this.gbDTSetting.PerformLayout();
            this.tabSVM_KMeans.ResumeLayout(false);
            this.gbKmeansSetting.ResumeLayout(false);
            this.gbKmeansSetting.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmNumCluster)).EndInit();
            this.gbSVRSetting.ResumeLayout(false);
            this.gbSVRSetting.PerformLayout();
            this.tabApplication.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbStockChart.ResumeLayout(false);
            this.gbStockChart.PerformLayout();
            this.staMainProgress.ResumeLayout(false);
            this.staMainProgress.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabApplication;
        private System.Windows.Forms.TabPage tabExperiment;
        private System.Windows.Forms.GroupBox gbSVRSetting;
        private System.Windows.Forms.TextBox tbxNumFold;
        private System.Windows.Forms.Label lblNumFold;
        private System.Windows.Forms.ComboBox cmbModelSelection;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnPreprocessBrowser;
        private System.Windows.Forms.TextBox tbxCsvFilePath;
        private System.Windows.Forms.GroupBox gbPreprocess;
        private System.Windows.Forms.Button btnPreprocess;
        private System.Windows.Forms.TextBox tbxTrainingRatio;
        private System.Windows.Forms.Label lblTrainingRatio;
        private System.Windows.Forms.GroupBox gbTest;
        private System.Windows.Forms.Label lblModelFile;
        private System.Windows.Forms.Button btnModelBrowser;
        private System.Windows.Forms.TextBox tbxModelFilePath;
        private System.Windows.Forms.Label lblTestFile;
        private System.Windows.Forms.Button btnTestBrowser;
        private System.Windows.Forms.TextBox tbxTestFilePath;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.GroupBox gbTraining;
        private System.Windows.Forms.Label lblTrainingFile;
        private System.Windows.Forms.Button btnTrainBrowser;
        private System.Windows.Forms.TextBox tbxTrainFilePath;
        private System.Windows.Forms.Button btnTrain;
        private System.Windows.Forms.GroupBox gbModelChoice;
        private System.Windows.Forms.RadioButton rdSVM;
        private System.Windows.Forms.RadioButton rdANN;
        private ZedGraph.ZedGraphControl zg1;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.Label lblStockID;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.ComboBox cmbStockID;
        private System.Windows.Forms.ComboBox cmbNumDaysPredicted;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxC;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxGamma;
        private System.Windows.Forms.RadioButton rdDTANN;
        private System.Windows.Forms.RadioButton rdKSVMeans;
        private System.Windows.Forms.NumericUpDown nmNumCluster;
        private System.Windows.Forms.Label lblNumCluster;
        private System.Windows.Forms.CheckBox ckbProbEstimate;
        private System.Windows.Forms.ComboBox cmbActivationFunc;
        private System.Windows.Forms.Label lblActivationFunc;
        private System.Windows.Forms.GroupBox gbAnnSetting;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox tbxANNHiddenNode;
        private System.Windows.Forms.TextBox tbxMomentum;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox tbxMaxLoops;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbxBias;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbxLearningRate;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox gbBatchTrainTest;
        private System.Windows.Forms.Label lblExperimentMode;
        private System.Windows.Forms.Label lblInputFile;
        private System.Windows.Forms.Label lblBatchInputFile;
        private System.Windows.Forms.TextBox tbxBatchInputFile;
        private System.Windows.Forms.Button btnBatchBrowse;
        private System.Windows.Forms.Button btnBatchTrainTest;
        private System.Windows.Forms.Label lblBackTitle;
        private System.Windows.Forms.StatusStrip staMainProgress;
        private System.Windows.Forms.ToolStripStatusLabel tlsStatus;
        private System.Windows.Forms.ToolStripProgressBar tlsProgressBar;
        private System.Windows.Forms.TabControl tabCtrlSettings;
        private System.Windows.Forms.TabPage tabDT_ANN;
        private System.Windows.Forms.TabPage tabSVM_KMeans;
        private System.Windows.Forms.GroupBox gbDTSetting;
        private System.Windows.Forms.ComboBox cmbSplitFunc;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbPruneFunc;
        private System.Windows.Forms.GroupBox gbResults;
        private System.Windows.Forms.WebBrowser wbResults;
        private System.Windows.Forms.ComboBox cmbExpStockID;
        private System.Windows.Forms.ComboBox cmbExperimentMode;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox gbKmeansSetting;
        private System.Windows.Forms.ComboBox cmbDistanceType;
        private System.Windows.Forms.Label lblDistanceType;
        private System.Windows.Forms.CheckBox cbChoseData;
        private System.Windows.Forms.TextBox tbxChoseFolder;
        private System.Windows.Forms.Button btnChoseFolder;
        private System.Windows.Forms.GroupBox gbStockChart;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.WebBrowser wbSAResult;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbSAStockID;
        private System.Windows.Forms.CheckBox cbSAFiveDay;
        private System.Windows.Forms.CheckBox cbSAOneDay;
        private System.Windows.Forms.Button btnSAPredict;
        private System.Windows.Forms.Button btnUpdateData;
        private System.Windows.Forms.DateTimePicker dtpChoseCurrentDate;
        private System.Windows.Forms.Label label15;
    }
}

