﻿namespace GUI
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
            this.label1 = new System.Windows.Forms.Label();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabExperiment = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnModelBrowser = new System.Windows.Forms.Button();
            this.tbxModelFilePath = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnTestBrowser = new System.Windows.Forms.Button();
            this.tbxTestFilePath = new System.Windows.Forms.TextBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnTrainBrowser = new System.Windows.Forms.Button();
            this.gbSVRSetting = new System.Windows.Forms.GroupBox();
            this.ckbProbEstimation = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxGamma = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxC = new System.Windows.Forms.TextBox();
            this.tbxNumFold = new System.Windows.Forms.TextBox();
            this.lblNumFold = new System.Windows.Forms.Label();
            this.cmbModelSelection = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbxTrainFilePath = new System.Windows.Forms.TextBox();
            this.btnTrain = new System.Windows.Forms.Button();
            this.gbAnnSetting = new System.Windows.Forms.GroupBox();
            this.cmbActivationFunc = new System.Windows.Forms.ComboBox();
            this.cmbTrainingMeasure = new System.Windows.Forms.ComboBox();
            this.lblTrainingMeasure = new System.Windows.Forms.Label();
            this.lblActivationFunc = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbxANNInputNode = new System.Windows.Forms.TextBox();
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
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnPreprocess = new System.Windows.Forms.Button();
            this.tbxCsvFilePath = new System.Windows.Forms.TextBox();
            this.btnBrowser = new System.Windows.Forms.Button();
            this.tbxTrainingRatio = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.btnStepTrainAndTest = new System.Windows.Forms.Button();
            this.tbxEndDate = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.tbxStartDate = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.tbxTrainingSize = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.GBModelChoice = new System.Windows.Forms.GroupBox();
            this.rdSOMSVM = new System.Windows.Forms.RadioButton();
            this.nmNumCluster = new System.Windows.Forms.NumericUpDown();
            this.cmbNumDaysPredicted = new System.Windows.Forms.ComboBox();
            this.lblNumCluster = new System.Windows.Forms.Label();
            this.rdSVM = new System.Windows.Forms.RadioButton();
            this.rdANN = new System.Windows.Forms.RadioButton();
            this.label10 = new System.Windows.Forms.Label();
            this.rdKSVMeans = new System.Windows.Forms.RadioButton();
            this.tabApplication = new System.Windows.Forms.TabPage();
            this.tbxNumDayTrendPredict = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.gbResult = new System.Windows.Forms.GroupBox();
            this.tbxSVMTrend = new System.Windows.Forms.TextBox();
            this.tbxANNPrice = new System.Windows.Forms.TextBox();
            this.lblSVMTrend = new System.Windows.Forms.Label();
            this.lblANNPrice = new System.Windows.Forms.Label();
            this.lblANNTrend = new System.Windows.Forms.Label();
            this.lblSVMPrice = new System.Windows.Forms.Label();
            this.tbxANNTrend = new System.Windows.Forms.TextBox();
            this.tbxSVMPrice = new System.Windows.Forms.TextBox();
            this.btnPredict = new System.Windows.Forms.Button();
            this.lblInputDay = new System.Windows.Forms.Label();
            this.dtpInputDay = new System.Windows.Forms.DateTimePicker();
            this.lblTo = new System.Windows.Forms.Label();
            this.lblFrom = new System.Windows.Forms.Label();
            this.lblStockID = new System.Windows.Forms.Label();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.cmbStockID = new System.Windows.Forms.ComboBox();
            this.zg1 = new ZedGraph.ZedGraphControl();
            this.gbBatchTrainTest = new System.Windows.Forms.GroupBox();
            this.cmbExperimentMode = new System.Windows.Forms.ComboBox();
            this.lblExperimentMode = new System.Windows.Forms.Label();
            this.lblInputFile = new System.Windows.Forms.Label();
            this.tabMain.SuspendLayout();
            this.tabExperiment.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.gbSVRSetting.SuspendLayout();
            this.gbAnnSetting.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.GBModelChoice.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmNumCluster)).BeginInit();
            this.tabApplication.SuspendLayout();
            this.gbResult.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1003, 43);
            this.label1.TabIndex = 8;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabExperiment);
            this.tabMain.Controls.Add(this.tabApplication);
            this.tabMain.Location = new System.Drawing.Point(0, 44);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(1003, 687);
            this.tabMain.TabIndex = 9;
            this.tabMain.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabOption_Selected);
            // 
            // tabExperiment
            // 
            this.tabExperiment.BackColor = System.Drawing.Color.Transparent;
            this.tabExperiment.Controls.Add(this.gbBatchTrainTest);
            this.tabExperiment.Controls.Add(this.groupBox3);
            this.tabExperiment.Controls.Add(this.groupBox2);
            this.tabExperiment.Controls.Add(this.groupBox5);
            this.tabExperiment.Controls.Add(this.groupBox9);
            this.tabExperiment.Controls.Add(this.GBModelChoice);
            this.tabExperiment.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabExperiment.Location = new System.Drawing.Point(4, 22);
            this.tabExperiment.Name = "tabExperiment";
            this.tabExperiment.Padding = new System.Windows.Forms.Padding(3);
            this.tabExperiment.Size = new System.Drawing.Size(995, 661);
            this.tabExperiment.TabIndex = 0;
            this.tabExperiment.Text = "EXPERIMENT";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.btnModelBrowser);
            this.groupBox3.Controls.Add(this.tbxModelFilePath);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.btnTestBrowser);
            this.groupBox3.Controls.Add(this.tbxTestFilePath);
            this.groupBox3.Controls.Add(this.btnTest);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.groupBox3.Location = new System.Drawing.Point(14, 304);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(356, 169);
            this.groupBox3.TabIndex = 24;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "3. TEST";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(6, 79);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(52, 13);
            this.label9.TabIndex = 34;
            this.label9.Text = "Model file";
            // 
            // btnModelBrowser
            // 
            this.btnModelBrowser.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModelBrowser.ForeColor = System.Drawing.Color.Black;
            this.btnModelBrowser.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnModelBrowser.Location = new System.Drawing.Point(259, 71);
            this.btnModelBrowser.Name = "btnModelBrowser";
            this.btnModelBrowser.Size = new System.Drawing.Size(83, 28);
            this.btnModelBrowser.TabIndex = 33;
            this.btnModelBrowser.Text = "Browse...";
            this.btnModelBrowser.UseVisualStyleBackColor = true;
            this.btnModelBrowser.Click += new System.EventHandler(this.btnModelBrowser_Click);
            // 
            // tbxModelFilePath
            // 
            this.tbxModelFilePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxModelFilePath.Location = new System.Drawing.Point(61, 75);
            this.tbxModelFilePath.Name = "tbxModelFilePath";
            this.tbxModelFilePath.Size = new System.Drawing.Size(192, 20);
            this.tbxModelFilePath.TabIndex = 32;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(6, 37);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 13);
            this.label8.TabIndex = 31;
            this.label8.Text = "Test file";
            // 
            // btnTestBrowser
            // 
            this.btnTestBrowser.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTestBrowser.ForeColor = System.Drawing.Color.Black;
            this.btnTestBrowser.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTestBrowser.Location = new System.Drawing.Point(259, 29);
            this.btnTestBrowser.Name = "btnTestBrowser";
            this.btnTestBrowser.Size = new System.Drawing.Size(83, 28);
            this.btnTestBrowser.TabIndex = 30;
            this.btnTestBrowser.Text = "Browse...";
            this.btnTestBrowser.UseVisualStyleBackColor = true;
            this.btnTestBrowser.Click += new System.EventHandler(this.btnTestBrowser_Click);
            // 
            // tbxTestFilePath
            // 
            this.tbxTestFilePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxTestFilePath.Location = new System.Drawing.Point(61, 34);
            this.tbxTestFilePath.Name = "tbxTestFilePath";
            this.tbxTestFilePath.Size = new System.Drawing.Size(192, 20);
            this.tbxTestFilePath.TabIndex = 29;
            // 
            // btnTest
            // 
            this.btnTest.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTest.ForeColor = System.Drawing.Color.Black;
            this.btnTest.Image = global::GUI.Properties.Resources.Test;
            this.btnTest.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTest.Location = new System.Drawing.Point(116, 117);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(124, 41);
            this.btnTest.TabIndex = 6;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.btnTrainBrowser);
            this.groupBox2.Controls.Add(this.gbSVRSetting);
            this.groupBox2.Controls.Add(this.tbxTrainFilePath);
            this.groupBox2.Controls.Add(this.btnTrain);
            this.groupBox2.Controls.Add(this.gbAnnSetting);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.groupBox2.Location = new System.Drawing.Point(392, 154);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(506, 321);
            this.groupBox2.TabIndex = 23;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "2. TRAINING";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(13, 35);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 13);
            this.label7.TabIndex = 28;
            this.label7.Text = "Training set file";
            // 
            // btnTrainBrowser
            // 
            this.btnTrainBrowser.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTrainBrowser.ForeColor = System.Drawing.Color.Black;
            this.btnTrainBrowser.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTrainBrowser.Location = new System.Drawing.Point(409, 27);
            this.btnTrainBrowser.Name = "btnTrainBrowser";
            this.btnTrainBrowser.Size = new System.Drawing.Size(83, 28);
            this.btnTrainBrowser.TabIndex = 27;
            this.btnTrainBrowser.Text = "Browse...";
            this.btnTrainBrowser.UseVisualStyleBackColor = true;
            this.btnTrainBrowser.Click += new System.EventHandler(this.btnTrainBrowser_Click);
            // 
            // gbSVRSetting
            // 
            this.gbSVRSetting.Controls.Add(this.ckbProbEstimation);
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
            this.gbSVRSetting.Location = new System.Drawing.Point(258, 67);
            this.gbSVRSetting.Name = "gbSVRSetting";
            this.gbSVRSetting.Size = new System.Drawing.Size(230, 159);
            this.gbSVRSetting.TabIndex = 44;
            this.gbSVRSetting.TabStop = false;
            this.gbSVRSetting.Text = "SVM SETTINGS";
            // 
            // ckbProbEstimation
            // 
            this.ckbProbEstimation.AutoSize = true;
            this.ckbProbEstimation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckbProbEstimation.ForeColor = System.Drawing.Color.Black;
            this.ckbProbEstimation.Location = new System.Drawing.Point(14, 25);
            this.ckbProbEstimation.Name = "ckbProbEstimation";
            this.ckbProbEstimation.Size = new System.Drawing.Size(125, 17);
            this.ckbProbEstimation.TabIndex = 47;
            this.ckbProbEstimation.Text = "Probability Estimation";
            this.ckbProbEstimation.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(10, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 46;
            this.label3.Text = "Gamma = 2^";
            // 
            // tbxGamma
            // 
            this.tbxGamma.Location = new System.Drawing.Point(100, 127);
            this.tbxGamma.Name = "tbxGamma";
            this.tbxGamma.Size = new System.Drawing.Size(115, 20);
            this.tbxGamma.TabIndex = 45;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(10, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 44;
            this.label2.Text = "C = 2^";
            // 
            // tbxC
            // 
            this.tbxC.Location = new System.Drawing.Point(100, 101);
            this.tbxC.Name = "tbxC";
            this.tbxC.Size = new System.Drawing.Size(115, 20);
            this.tbxC.TabIndex = 43;
            // 
            // tbxNumFold
            // 
            this.tbxNumFold.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxNumFold.ForeColor = System.Drawing.Color.Black;
            this.tbxNumFold.Location = new System.Drawing.Point(100, 75);
            this.tbxNumFold.Name = "tbxNumFold";
            this.tbxNumFold.Size = new System.Drawing.Size(115, 20);
            this.tbxNumFold.TabIndex = 38;
            this.tbxNumFold.Text = "5";
            // 
            // lblNumFold
            // 
            this.lblNumFold.AutoSize = true;
            this.lblNumFold.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumFold.ForeColor = System.Drawing.Color.Black;
            this.lblNumFold.Location = new System.Drawing.Point(10, 80);
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
            this.cmbModelSelection.Location = new System.Drawing.Point(100, 48);
            this.cmbModelSelection.Name = "cmbModelSelection";
            this.cmbModelSelection.Size = new System.Drawing.Size(115, 21);
            this.cmbModelSelection.TabIndex = 20;
            this.cmbModelSelection.SelectedIndexChanged += new System.EventHandler(this.cmbModelSelection_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(10, 54);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "Model selection";
            // 
            // tbxTrainFilePath
            // 
            this.tbxTrainFilePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxTrainFilePath.Location = new System.Drawing.Point(97, 32);
            this.tbxTrainFilePath.Name = "tbxTrainFilePath";
            this.tbxTrainFilePath.Size = new System.Drawing.Size(300, 20);
            this.tbxTrainFilePath.TabIndex = 26;
            // 
            // btnTrain
            // 
            this.btnTrain.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTrain.ForeColor = System.Drawing.Color.Black;
            this.btnTrain.Image = global::GUI.Properties.Resources.Train;
            this.btnTrain.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTrain.Location = new System.Drawing.Point(318, 259);
            this.btnTrain.Name = "btnTrain";
            this.btnTrain.Size = new System.Drawing.Size(124, 41);
            this.btnTrain.TabIndex = 6;
            this.btnTrain.Text = "Train";
            this.btnTrain.UseVisualStyleBackColor = true;
            this.btnTrain.Click += new System.EventHandler(this.btnTrain_Click);
            // 
            // gbAnnSetting
            // 
            this.gbAnnSetting.Controls.Add(this.cmbActivationFunc);
            this.gbAnnSetting.Controls.Add(this.cmbTrainingMeasure);
            this.gbAnnSetting.Controls.Add(this.lblTrainingMeasure);
            this.gbAnnSetting.Controls.Add(this.lblActivationFunc);
            this.gbAnnSetting.Controls.Add(this.label4);
            this.gbAnnSetting.Controls.Add(this.tbxANNInputNode);
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
            this.gbAnnSetting.Location = new System.Drawing.Point(11, 67);
            this.gbAnnSetting.Name = "gbAnnSetting";
            this.gbAnnSetting.Size = new System.Drawing.Size(230, 244);
            this.gbAnnSetting.TabIndex = 23;
            this.gbAnnSetting.TabStop = false;
            this.gbAnnSetting.Text = "ANN SETTINGS";
            // 
            // cmbActivationFunc
            // 
            this.cmbActivationFunc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbActivationFunc.FormattingEnabled = true;
            this.cmbActivationFunc.Items.AddRange(new object[] {
            "Sigmoid",
            "Tanh",
            "Logarithm",
            "Sine"});
            this.cmbActivationFunc.Location = new System.Drawing.Point(108, 23);
            this.cmbActivationFunc.Name = "cmbActivationFunc";
            this.cmbActivationFunc.Size = new System.Drawing.Size(111, 21);
            this.cmbActivationFunc.TabIndex = 41;
            // 
            // cmbTrainingMeasure
            // 
            this.cmbTrainingMeasure.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTrainingMeasure.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTrainingMeasure.ForeColor = System.Drawing.Color.Black;
            this.cmbTrainingMeasure.FormattingEnabled = true;
            this.cmbTrainingMeasure.Items.AddRange(new object[] {
            "MSE",
            "NMSE",
            "RMSE",
            "Sign"});
            this.cmbTrainingMeasure.Location = new System.Drawing.Point(108, 50);
            this.cmbTrainingMeasure.Name = "cmbTrainingMeasure";
            this.cmbTrainingMeasure.Size = new System.Drawing.Size(111, 21);
            this.cmbTrainingMeasure.TabIndex = 31;
            // 
            // lblTrainingMeasure
            // 
            this.lblTrainingMeasure.AutoSize = true;
            this.lblTrainingMeasure.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrainingMeasure.ForeColor = System.Drawing.Color.Black;
            this.lblTrainingMeasure.Location = new System.Drawing.Point(14, 55);
            this.lblTrainingMeasure.Name = "lblTrainingMeasure";
            this.lblTrainingMeasure.Size = new System.Drawing.Size(88, 13);
            this.lblTrainingMeasure.TabIndex = 32;
            this.lblTrainingMeasure.Text = "Training measure";
            // 
            // lblActivationFunc
            // 
            this.lblActivationFunc.AutoSize = true;
            this.lblActivationFunc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActivationFunc.ForeColor = System.Drawing.Color.Black;
            this.lblActivationFunc.Location = new System.Drawing.Point(14, 29);
            this.lblActivationFunc.Name = "lblActivationFunc";
            this.lblActivationFunc.Size = new System.Drawing.Size(81, 13);
            this.lblActivationFunc.TabIndex = 40;
            this.lblActivationFunc.Text = "Activation Func";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(14, 82);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 39;
            this.label4.Text = "Num input node";
            // 
            // tbxANNInputNode
            // 
            this.tbxANNInputNode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxANNInputNode.ForeColor = System.Drawing.Color.Black;
            this.tbxANNInputNode.Location = new System.Drawing.Point(108, 78);
            this.tbxANNInputNode.Name = "tbxANNInputNode";
            this.tbxANNInputNode.Size = new System.Drawing.Size(111, 20);
            this.tbxANNInputNode.TabIndex = 38;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.Black;
            this.label17.Location = new System.Drawing.Point(14, 109);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(91, 13);
            this.label17.TabIndex = 37;
            this.label17.Text = "Num hidden node";
            // 
            // tbxANNHiddenNode
            // 
            this.tbxANNHiddenNode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxANNHiddenNode.ForeColor = System.Drawing.Color.Black;
            this.tbxANNHiddenNode.Location = new System.Drawing.Point(108, 105);
            this.tbxANNHiddenNode.Name = "tbxANNHiddenNode";
            this.tbxANNHiddenNode.Size = new System.Drawing.Size(111, 20);
            this.tbxANNHiddenNode.TabIndex = 36;
            // 
            // tbxMomentum
            // 
            this.tbxMomentum.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxMomentum.ForeColor = System.Drawing.Color.Black;
            this.tbxMomentum.Location = new System.Drawing.Point(108, 213);
            this.tbxMomentum.Name = "tbxMomentum";
            this.tbxMomentum.Size = new System.Drawing.Size(111, 20);
            this.tbxMomentum.TabIndex = 33;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.Location = new System.Drawing.Point(14, 213);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(59, 13);
            this.label14.TabIndex = 32;
            this.label14.Text = "Momentum";
            // 
            // tbxMaxLoops
            // 
            this.tbxMaxLoops.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxMaxLoops.ForeColor = System.Drawing.Color.Black;
            this.tbxMaxLoops.Location = new System.Drawing.Point(108, 159);
            this.tbxMaxLoops.Name = "tbxMaxLoops";
            this.tbxMaxLoops.Size = new System.Drawing.Size(111, 20);
            this.tbxMaxLoops.TabIndex = 31;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(14, 161);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(55, 13);
            this.label11.TabIndex = 30;
            this.label11.Text = "Max loops";
            // 
            // tbxBias
            // 
            this.tbxBias.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxBias.ForeColor = System.Drawing.Color.Black;
            this.tbxBias.Location = new System.Drawing.Point(108, 186);
            this.tbxBias.Name = "tbxBias";
            this.tbxBias.Size = new System.Drawing.Size(111, 20);
            this.tbxBias.TabIndex = 29;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(14, 187);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(27, 13);
            this.label12.TabIndex = 28;
            this.label12.Text = "Bias";
            // 
            // tbxLearningRate
            // 
            this.tbxLearningRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxLearningRate.ForeColor = System.Drawing.Color.Black;
            this.tbxLearningRate.Location = new System.Drawing.Point(108, 132);
            this.tbxLearningRate.Name = "tbxLearningRate";
            this.tbxLearningRate.Size = new System.Drawing.Size(111, 20);
            this.tbxLearningRate.TabIndex = 27;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(14, 135);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(69, 13);
            this.label13.TabIndex = 26;
            this.label13.Text = "Learning rate";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.lblInputFile);
            this.groupBox5.Controls.Add(this.btnPreprocess);
            this.groupBox5.Controls.Add(this.tbxCsvFilePath);
            this.groupBox5.Controls.Add(this.btnBrowser);
            this.groupBox5.Controls.Add(this.tbxTrainingRatio);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.groupBox5.Location = new System.Drawing.Point(14, 154);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(357, 134);
            this.groupBox5.TabIndex = 25;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "1. DATA PREPROCESS";
            // 
            // btnPreprocess
            // 
            this.btnPreprocess.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPreprocess.ForeColor = System.Drawing.Color.Black;
            this.btnPreprocess.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPreprocess.Location = new System.Drawing.Point(115, 81);
            this.btnPreprocess.Name = "btnPreprocess";
            this.btnPreprocess.Size = new System.Drawing.Size(124, 41);
            this.btnPreprocess.TabIndex = 7;
            this.btnPreprocess.Text = "Preprocess";
            this.btnPreprocess.UseVisualStyleBackColor = true;
            this.btnPreprocess.Click += new System.EventHandler(this.btnPreprocess_Click);
            // 
            // tbxCsvFilePath
            // 
            this.tbxCsvFilePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxCsvFilePath.ForeColor = System.Drawing.Color.Black;
            this.tbxCsvFilePath.Location = new System.Drawing.Point(61, 52);
            this.tbxCsvFilePath.Name = "tbxCsvFilePath";
            this.tbxCsvFilePath.Size = new System.Drawing.Size(205, 20);
            this.tbxCsvFilePath.TabIndex = 18;
            // 
            // btnBrowser
            // 
            this.btnBrowser.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowser.ForeColor = System.Drawing.Color.Black;
            this.btnBrowser.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBrowser.Location = new System.Drawing.Point(268, 48);
            this.btnBrowser.Name = "btnBrowser";
            this.btnBrowser.Size = new System.Drawing.Size(83, 28);
            this.btnBrowser.TabIndex = 19;
            this.btnBrowser.Text = "Browse...";
            this.btnBrowser.UseVisualStyleBackColor = true;
            this.btnBrowser.Click += new System.EventHandler(this.btnBrowser_Click);
            // 
            // tbxTrainingRatio
            // 
            this.tbxTrainingRatio.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxTrainingRatio.Location = new System.Drawing.Point(125, 23);
            this.tbxTrainingRatio.Name = "tbxTrainingRatio";
            this.tbxTrainingRatio.Size = new System.Drawing.Size(141, 20);
            this.tbxTrainingRatio.TabIndex = 23;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(7, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "Training set ratio (%)";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.btnStepTrainAndTest);
            this.groupBox9.Controls.Add(this.tbxEndDate);
            this.groupBox9.Controls.Add(this.label20);
            this.groupBox9.Controls.Add(this.tbxStartDate);
            this.groupBox9.Controls.Add(this.label19);
            this.groupBox9.Controls.Add(this.tbxTrainingSize);
            this.groupBox9.Controls.Add(this.label16);
            this.groupBox9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox9.ForeColor = System.Drawing.Color.Green;
            this.groupBox9.Location = new System.Drawing.Point(14, 490);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(657, 128);
            this.groupBox9.TabIndex = 37;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "STEP TRAINING AND TEST";
            // 
            // btnStepTrainAndTest
            // 
            this.btnStepTrainAndTest.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStepTrainAndTest.ForeColor = System.Drawing.Color.Black;
            this.btnStepTrainAndTest.Image = global::GUI.Properties.Resources.Fast_Forward_32_h_p8;
            this.btnStepTrainAndTest.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStepTrainAndTest.Location = new System.Drawing.Point(194, 81);
            this.btnStepTrainAndTest.Name = "btnStepTrainAndTest";
            this.btnStepTrainAndTest.Size = new System.Drawing.Size(162, 41);
            this.btnStepTrainAndTest.TabIndex = 7;
            this.btnStepTrainAndTest.Text = "Step train and test";
            this.btnStepTrainAndTest.UseVisualStyleBackColor = true;
            this.btnStepTrainAndTest.Click += new System.EventHandler(this.btnStepTrainAndTest_Click);
            // 
            // tbxEndDate
            // 
            this.tbxEndDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxEndDate.ForeColor = System.Drawing.Color.Black;
            this.tbxEndDate.Location = new System.Drawing.Point(353, 50);
            this.tbxEndDate.Name = "tbxEndDate";
            this.tbxEndDate.Size = new System.Drawing.Size(139, 20);
            this.tbxEndDate.TabIndex = 5;
            this.tbxEndDate.Text = "WholeData";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.Black;
            this.label20.Location = new System.Drawing.Point(325, 53);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(20, 13);
            this.label20.TabIndex = 4;
            this.label20.Text = "To";
            // 
            // tbxStartDate
            // 
            this.tbxStartDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxStartDate.ForeColor = System.Drawing.Color.Black;
            this.tbxStartDate.Location = new System.Drawing.Point(169, 50);
            this.tbxStartDate.Name = "tbxStartDate";
            this.tbxStartDate.Size = new System.Drawing.Size(139, 20);
            this.tbxStartDate.TabIndex = 3;
            this.tbxStartDate.Text = "WholeData";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.Black;
            this.label19.Location = new System.Drawing.Point(58, 53);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(84, 13);
            this.label19.TabIndex = 2;
            this.label19.Text = "From (d/M/yyyy)";
            // 
            // tbxTrainingSize
            // 
            this.tbxTrainingSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxTrainingSize.ForeColor = System.Drawing.Color.Black;
            this.tbxTrainingSize.Location = new System.Drawing.Point(169, 14);
            this.tbxTrainingSize.Name = "tbxTrainingSize";
            this.tbxTrainingSize.Size = new System.Drawing.Size(139, 20);
            this.tbxTrainingSize.TabIndex = 1;
            this.tbxTrainingSize.Text = "30";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.Black;
            this.label16.Location = new System.Drawing.Point(58, 17);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(97, 13);
            this.label16.TabIndex = 0;
            this.label16.Text = "Training size (days)";
            // 
            // GBModelChoice
            // 
            this.GBModelChoice.Controls.Add(this.lblExperimentMode);
            this.GBModelChoice.Controls.Add(this.cmbExperimentMode);
            this.GBModelChoice.Controls.Add(this.rdSOMSVM);
            this.GBModelChoice.Controls.Add(this.nmNumCluster);
            this.GBModelChoice.Controls.Add(this.cmbNumDaysPredicted);
            this.GBModelChoice.Controls.Add(this.lblNumCluster);
            this.GBModelChoice.Controls.Add(this.rdSVM);
            this.GBModelChoice.Controls.Add(this.rdANN);
            this.GBModelChoice.Controls.Add(this.label10);
            this.GBModelChoice.Controls.Add(this.rdKSVMeans);
            this.GBModelChoice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GBModelChoice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.GBModelChoice.Location = new System.Drawing.Point(14, 16);
            this.GBModelChoice.Name = "GBModelChoice";
            this.GBModelChoice.Size = new System.Drawing.Size(356, 121);
            this.GBModelChoice.TabIndex = 19;
            this.GBModelChoice.TabStop = false;
            this.GBModelChoice.Text = "MODEL CHOICE";
            // 
            // rdSOMSVM
            // 
            this.rdSOMSVM.AutoSize = true;
            this.rdSOMSVM.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdSOMSVM.Location = new System.Drawing.Point(256, 26);
            this.rdSOMSVM.Name = "rdSOMSVM";
            this.rdSOMSVM.Size = new System.Drawing.Size(75, 17);
            this.rdSOMSVM.TabIndex = 31;
            this.rdSOMSVM.TabStop = true;
            this.rdSOMSVM.Text = "SOM-SVM";
            this.rdSOMSVM.UseVisualStyleBackColor = true;
            this.rdSOMSVM.CheckedChanged += new System.EventHandler(this.rdSOMSVM_CheckedChanged);
            // 
            // nmNumCluster
            // 
            this.nmNumCluster.Location = new System.Drawing.Point(289, 54);
            this.nmNumCluster.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nmNumCluster.Name = "nmNumCluster";
            this.nmNumCluster.Size = new System.Drawing.Size(56, 20);
            this.nmNumCluster.TabIndex = 33;
            this.nmNumCluster.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // cmbNumDaysPredicted
            // 
            this.cmbNumDaysPredicted.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNumDaysPredicted.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbNumDaysPredicted.FormattingEnabled = true;
            this.cmbNumDaysPredicted.Items.AddRange(new object[] {
            "1",
            "5",
            "10",
            "30"});
            this.cmbNumDaysPredicted.Location = new System.Drawing.Point(115, 54);
            this.cmbNumDaysPredicted.Name = "cmbNumDaysPredicted";
            this.cmbNumDaysPredicted.Size = new System.Drawing.Size(86, 21);
            this.cmbNumDaysPredicted.TabIndex = 46;
            // 
            // lblNumCluster
            // 
            this.lblNumCluster.AutoSize = true;
            this.lblNumCluster.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumCluster.ForeColor = System.Drawing.Color.Black;
            this.lblNumCluster.Location = new System.Drawing.Point(207, 58);
            this.lblNumCluster.Name = "lblNumCluster";
            this.lblNumCluster.Size = new System.Drawing.Size(76, 13);
            this.lblNumCluster.TabIndex = 32;
            this.lblNumCluster.Text = "Num of Cluster";
            // 
            // rdSVM
            // 
            this.rdSVM.AutoSize = true;
            this.rdSVM.Checked = true;
            this.rdSVM.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdSVM.Location = new System.Drawing.Point(85, 26);
            this.rdSVM.Name = "rdSVM";
            this.rdSVM.Size = new System.Drawing.Size(48, 17);
            this.rdSVM.TabIndex = 1;
            this.rdSVM.TabStop = true;
            this.rdSVM.Text = "SVM";
            this.rdSVM.UseVisualStyleBackColor = true;
            // 
            // rdANN
            // 
            this.rdANN.AutoSize = true;
            this.rdANN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdANN.Location = new System.Drawing.Point(16, 26);
            this.rdANN.Name = "rdANN";
            this.rdANN.Size = new System.Drawing.Size(48, 17);
            this.rdANN.TabIndex = 0;
            this.rdANN.Text = "ANN";
            this.rdANN.UseVisualStyleBackColor = true;
            this.rdANN.CheckedChanged += new System.EventHandler(this.rdANN_CheckedChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(13, 58);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(96, 13);
            this.label10.TabIndex = 40;
            this.label10.Text = "Num day predicted";
            // 
            // rdKSVMeans
            // 
            this.rdKSVMeans.AutoSize = true;
            this.rdKSVMeans.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdKSVMeans.Location = new System.Drawing.Point(154, 26);
            this.rdKSVMeans.Name = "rdKSVMeans";
            this.rdKSVMeans.Size = new System.Drawing.Size(81, 17);
            this.rdKSVMeans.TabIndex = 30;
            this.rdKSVMeans.TabStop = true;
            this.rdKSVMeans.Text = "K-SVMeans";
            this.rdKSVMeans.UseVisualStyleBackColor = true;
            this.rdKSVMeans.CheckedChanged += new System.EventHandler(this.rdKSVMeans_CheckedChanged);
            // 
            // tabApplication
            // 
            this.tabApplication.BackColor = System.Drawing.Color.Transparent;
            this.tabApplication.Controls.Add(this.tbxNumDayTrendPredict);
            this.tabApplication.Controls.Add(this.label22);
            this.tabApplication.Controls.Add(this.gbResult);
            this.tabApplication.Controls.Add(this.btnPredict);
            this.tabApplication.Controls.Add(this.lblInputDay);
            this.tabApplication.Controls.Add(this.dtpInputDay);
            this.tabApplication.Controls.Add(this.lblTo);
            this.tabApplication.Controls.Add(this.lblFrom);
            this.tabApplication.Controls.Add(this.lblStockID);
            this.tabApplication.Controls.Add(this.dtpTo);
            this.tabApplication.Controls.Add(this.dtpFrom);
            this.tabApplication.Controls.Add(this.cmbStockID);
            this.tabApplication.Controls.Add(this.zg1);
            this.tabApplication.Location = new System.Drawing.Point(4, 22);
            this.tabApplication.Name = "tabApplication";
            this.tabApplication.Padding = new System.Windows.Forms.Padding(3);
            this.tabApplication.Size = new System.Drawing.Size(995, 661);
            this.tabApplication.TabIndex = 1;
            this.tabApplication.Text = "APPLICATION";
            // 
            // tbxNumDayTrendPredict
            // 
            this.tbxNumDayTrendPredict.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxNumDayTrendPredict.ForeColor = System.Drawing.Color.Black;
            this.tbxNumDayTrendPredict.Location = new System.Drawing.Point(174, 545);
            this.tbxNumDayTrendPredict.Name = "tbxNumDayTrendPredict";
            this.tbxNumDayTrendPredict.Size = new System.Drawing.Size(79, 20);
            this.tbxNumDayTrendPredict.TabIndex = 43;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.ForeColor = System.Drawing.Color.Black;
            this.label22.Location = new System.Drawing.Point(41, 548);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(101, 13);
            this.label22.TabIndex = 42;
            this.label22.Text = "Num days predicted";
            // 
            // gbResult
            // 
            this.gbResult.Controls.Add(this.tbxSVMTrend);
            this.gbResult.Controls.Add(this.tbxANNPrice);
            this.gbResult.Controls.Add(this.lblSVMTrend);
            this.gbResult.Controls.Add(this.lblANNPrice);
            this.gbResult.Controls.Add(this.lblANNTrend);
            this.gbResult.Controls.Add(this.lblSVMPrice);
            this.gbResult.Controls.Add(this.tbxANNTrend);
            this.gbResult.Controls.Add(this.tbxSVMPrice);
            this.gbResult.Location = new System.Drawing.Point(414, 502);
            this.gbResult.Name = "gbResult";
            this.gbResult.Size = new System.Drawing.Size(484, 86);
            this.gbResult.TabIndex = 34;
            this.gbResult.TabStop = false;
            this.gbResult.Text = "Result";
            // 
            // tbxSVMTrend
            // 
            this.tbxSVMTrend.Location = new System.Drawing.Point(361, 52);
            this.tbxSVMTrend.Name = "tbxSVMTrend";
            this.tbxSVMTrend.ReadOnly = true;
            this.tbxSVMTrend.Size = new System.Drawing.Size(100, 20);
            this.tbxSVMTrend.TabIndex = 21;
            // 
            // tbxANNPrice
            // 
            this.tbxANNPrice.Location = new System.Drawing.Point(138, 18);
            this.tbxANNPrice.Name = "tbxANNPrice";
            this.tbxANNPrice.ReadOnly = true;
            this.tbxANNPrice.Size = new System.Drawing.Size(100, 20);
            this.tbxANNPrice.TabIndex = 14;
            // 
            // lblSVMTrend
            // 
            this.lblSVMTrend.AutoSize = true;
            this.lblSVMTrend.Location = new System.Drawing.Point(285, 55);
            this.lblSVMTrend.Name = "lblSVMTrend";
            this.lblSVMTrend.Size = new System.Drawing.Size(61, 13);
            this.lblSVMTrend.TabIndex = 20;
            this.lblSVMTrend.Text = "SVM Trend";
            // 
            // lblANNPrice
            // 
            this.lblANNPrice.AutoSize = true;
            this.lblANNPrice.Location = new System.Drawing.Point(62, 21);
            this.lblANNPrice.Name = "lblANNPrice";
            this.lblANNPrice.Size = new System.Drawing.Size(57, 13);
            this.lblANNPrice.TabIndex = 15;
            this.lblANNPrice.Text = "ANN Price";
            // 
            // lblANNTrend
            // 
            this.lblANNTrend.AutoSize = true;
            this.lblANNTrend.Location = new System.Drawing.Point(285, 21);
            this.lblANNTrend.Name = "lblANNTrend";
            this.lblANNTrend.Size = new System.Drawing.Size(61, 13);
            this.lblANNTrend.TabIndex = 19;
            this.lblANNTrend.Text = "ANN Trend";
            // 
            // lblSVMPrice
            // 
            this.lblSVMPrice.AutoSize = true;
            this.lblSVMPrice.Location = new System.Drawing.Point(62, 55);
            this.lblSVMPrice.Name = "lblSVMPrice";
            this.lblSVMPrice.Size = new System.Drawing.Size(57, 13);
            this.lblSVMPrice.TabIndex = 16;
            this.lblSVMPrice.Text = "SVM Price";
            // 
            // tbxANNTrend
            // 
            this.tbxANNTrend.Location = new System.Drawing.Point(361, 18);
            this.tbxANNTrend.Name = "tbxANNTrend";
            this.tbxANNTrend.ReadOnly = true;
            this.tbxANNTrend.Size = new System.Drawing.Size(100, 20);
            this.tbxANNTrend.TabIndex = 18;
            // 
            // tbxSVMPrice
            // 
            this.tbxSVMPrice.Location = new System.Drawing.Point(138, 52);
            this.tbxSVMPrice.Name = "tbxSVMPrice";
            this.tbxSVMPrice.ReadOnly = true;
            this.tbxSVMPrice.Size = new System.Drawing.Size(100, 20);
            this.tbxSVMPrice.TabIndex = 17;
            // 
            // btnPredict
            // 
            this.btnPredict.Location = new System.Drawing.Point(266, 523);
            this.btnPredict.Name = "btnPredict";
            this.btnPredict.Size = new System.Drawing.Size(129, 35);
            this.btnPredict.TabIndex = 33;
            this.btnPredict.Text = "Predict";
            this.btnPredict.UseVisualStyleBackColor = true;
            this.btnPredict.Click += new System.EventHandler(this.btnPredict_Click);
            // 
            // lblInputDay
            // 
            this.lblInputDay.AutoSize = true;
            this.lblInputDay.Location = new System.Drawing.Point(42, 519);
            this.lblInputDay.Name = "lblInputDay";
            this.lblInputDay.Size = new System.Drawing.Size(53, 13);
            this.lblInputDay.TabIndex = 32;
            this.lblInputDay.Text = "Input Day";
            // 
            // dtpInputDay
            // 
            this.dtpInputDay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpInputDay.Location = new System.Drawing.Point(174, 513);
            this.dtpInputDay.Name = "dtpInputDay";
            this.dtpInputDay.Size = new System.Drawing.Size(79, 20);
            this.dtpInputDay.TabIndex = 31;
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Location = new System.Drawing.Point(383, 31);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(20, 13);
            this.lblTo.TabIndex = 29;
            this.lblTo.Text = "To";
            // 
            // lblFrom
            // 
            this.lblFrom.AutoSize = true;
            this.lblFrom.Location = new System.Drawing.Point(239, 32);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(30, 13);
            this.lblFrom.TabIndex = 28;
            this.lblFrom.Text = "From";
            // 
            // lblStockID
            // 
            this.lblStockID.AutoSize = true;
            this.lblStockID.Location = new System.Drawing.Point(42, 33);
            this.lblStockID.Name = "lblStockID";
            this.lblStockID.Size = new System.Drawing.Size(49, 13);
            this.lblStockID.TabIndex = 27;
            this.lblStockID.Text = "Stock ID";
            // 
            // dtpTo
            // 
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTo.Location = new System.Drawing.Point(407, 29);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(81, 20);
            this.dtpTo.TabIndex = 25;
            this.dtpTo.ValueChanged += new System.EventHandler(this.dtpTo_ValueChanged);
            // 
            // dtpFrom
            // 
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Location = new System.Drawing.Point(275, 29);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(81, 20);
            this.dtpFrom.TabIndex = 24;
            this.dtpFrom.ValueChanged += new System.EventHandler(this.dtpFrom_ValueChanged);
            // 
            // cmbStockID
            // 
            this.cmbStockID.FormattingEnabled = true;
            this.cmbStockID.Location = new System.Drawing.Point(108, 29);
            this.cmbStockID.Name = "cmbStockID";
            this.cmbStockID.Size = new System.Drawing.Size(91, 21);
            this.cmbStockID.TabIndex = 23;
            this.cmbStockID.SelectedIndexChanged += new System.EventHandler(this.cmbStockID_SelectedIndexChanged);
            // 
            // zg1
            // 
            this.zg1.EditButtons = System.Windows.Forms.MouseButtons.Left;
            this.zg1.Location = new System.Drawing.Point(45, 70);
            this.zg1.Name = "zg1";
            this.zg1.PanModifierKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.None)));
            this.zg1.ScrollGrace = 0;
            this.zg1.ScrollMaxX = 0;
            this.zg1.ScrollMaxY = 0;
            this.zg1.ScrollMaxY2 = 0;
            this.zg1.ScrollMinX = 0;
            this.zg1.ScrollMinY = 0;
            this.zg1.ScrollMinY2 = 0;
            this.zg1.Size = new System.Drawing.Size(843, 414);
            this.zg1.TabIndex = 1;
            // 
            // gbBatchTrainTest
            // 
            this.gbBatchTrainTest.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbBatchTrainTest.ForeColor = System.Drawing.Color.Purple;
            this.gbBatchTrainTest.Location = new System.Drawing.Point(392, 16);
            this.gbBatchTrainTest.Name = "gbBatchTrainTest";
            this.gbBatchTrainTest.Size = new System.Drawing.Size(506, 121);
            this.gbBatchTrainTest.TabIndex = 38;
            this.gbBatchTrainTest.TabStop = false;
            this.gbBatchTrainTest.Text = "BATCH TRAINING AND TEST";
            // 
            // cmbExperimentMode
            // 
            this.cmbExperimentMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbExperimentMode.FormattingEnabled = true;
            this.cmbExperimentMode.Items.AddRange(new object[] {
            "Step-by-step",
            "Batch"});
            this.cmbExperimentMode.Location = new System.Drawing.Point(115, 91);
            this.cmbExperimentMode.Name = "cmbExperimentMode";
            this.cmbExperimentMode.Size = new System.Drawing.Size(86, 21);
            this.cmbExperimentMode.TabIndex = 47;
            // 
            // lblExperimentMode
            // 
            this.lblExperimentMode.AutoSize = true;
            this.lblExperimentMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExperimentMode.ForeColor = System.Drawing.Color.Black;
            this.lblExperimentMode.Location = new System.Drawing.Point(16, 94);
            this.lblExperimentMode.Name = "lblExperimentMode";
            this.lblExperimentMode.Size = new System.Drawing.Size(89, 13);
            this.lblExperimentMode.TabIndex = 48;
            this.lblExperimentMode.Text = "Experiment Mode";
            // 
            // lblInputFile
            // 
            this.lblInputFile.AutoSize = true;
            this.lblInputFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInputFile.ForeColor = System.Drawing.Color.Black;
            this.lblInputFile.Location = new System.Drawing.Point(10, 55);
            this.lblInputFile.Name = "lblInputFile";
            this.lblInputFile.Size = new System.Drawing.Size(47, 13);
            this.lblInputFile.TabIndex = 24;
            this.lblInputFile.Text = "Input file";
            // 
            // MainGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 696);
            this.Controls.Add(this.tabMain);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "MainGUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainGUI_Load);
            this.tabMain.ResumeLayout(false);
            this.tabExperiment.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.gbSVRSetting.ResumeLayout(false);
            this.gbSVRSetting.PerformLayout();
            this.gbAnnSetting.ResumeLayout(false);
            this.gbAnnSetting.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.GBModelChoice.ResumeLayout(false);
            this.GBModelChoice.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmNumCluster)).EndInit();
            this.tabApplication.ResumeLayout(false);
            this.tabApplication.PerformLayout();
            this.gbResult.ResumeLayout(false);
            this.gbResult.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabApplication;
        private System.Windows.Forms.TabPage tabExperiment;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Button btnStepTrainAndTest;
        private System.Windows.Forms.TextBox tbxEndDate;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox tbxStartDate;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox tbxTrainingSize;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.GroupBox gbSVRSetting;
        private System.Windows.Forms.TextBox tbxNumFold;
        private System.Windows.Forms.Label lblNumFold;
        private System.Windows.Forms.ComboBox cmbModelSelection;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnBrowser;
        private System.Windows.Forms.TextBox tbxCsvFilePath;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnPreprocess;
        private System.Windows.Forms.TextBox tbxTrainingRatio;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnModelBrowser;
        private System.Windows.Forms.TextBox tbxModelFilePath;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnTestBrowser;
        private System.Windows.Forms.TextBox tbxTestFilePath;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnTrainBrowser;
        private System.Windows.Forms.TextBox tbxTrainFilePath;
        private System.Windows.Forms.Button btnTrain;
        private System.Windows.Forms.GroupBox GBModelChoice;
        private System.Windows.Forms.RadioButton rdSVM;
        private System.Windows.Forms.RadioButton rdANN;
        private ZedGraph.ZedGraphControl zg1;
        private System.Windows.Forms.GroupBox gbResult;
        private System.Windows.Forms.TextBox tbxSVMTrend;
        private System.Windows.Forms.TextBox tbxANNPrice;
        private System.Windows.Forms.Label lblSVMTrend;
        private System.Windows.Forms.Label lblANNPrice;
        private System.Windows.Forms.Label lblANNTrend;
        private System.Windows.Forms.Label lblSVMPrice;
        private System.Windows.Forms.TextBox tbxANNTrend;
        private System.Windows.Forms.TextBox tbxSVMPrice;
        private System.Windows.Forms.Button btnPredict;
        private System.Windows.Forms.Label lblInputDay;
        private System.Windows.Forms.DateTimePicker dtpInputDay;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.Label lblStockID;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.ComboBox cmbStockID;
        private System.Windows.Forms.TextBox tbxNumDayTrendPredict;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.ComboBox cmbNumDaysPredicted;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxC;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxGamma;
        private System.Windows.Forms.RadioButton rdSOMSVM;
        private System.Windows.Forms.RadioButton rdKSVMeans;
        private System.Windows.Forms.NumericUpDown nmNumCluster;
        private System.Windows.Forms.Label lblNumCluster;
        private System.Windows.Forms.CheckBox ckbProbEstimation;
        private System.Windows.Forms.ComboBox cmbActivationFunc;
        private System.Windows.Forms.Label lblActivationFunc;
        private System.Windows.Forms.GroupBox gbAnnSetting;
        private System.Windows.Forms.ComboBox cmbTrainingMeasure;
        private System.Windows.Forms.Label lblTrainingMeasure;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbxANNInputNode;
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
        private System.Windows.Forms.ComboBox cmbExperimentMode;
        private System.Windows.Forms.Label lblExperimentMode;
        private System.Windows.Forms.Label lblInputFile;
    }
}

