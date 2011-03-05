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
            this.tabOption = new System.Windows.Forms.TabControl();
            this.tabExperiment = new System.Windows.Forms.TabPage();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.btnStepTrainAndTest = new System.Windows.Forms.Button();
            this.tbxEndDate = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.tbxStartDate = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.tbxTrainingSize = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.cmbNumDaysPredicted = new System.Windows.Forms.ComboBox();
            this.gbSVRSetting = new System.Windows.Forms.GroupBox();
            this.ckbProbEstimation = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxGamma = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxC = new System.Windows.Forms.TextBox();
            this.tbxNumFold = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.cmbModelSelection = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.gbAnnSetting = new System.Windows.Forms.GroupBox();
            this.rdSine = new System.Windows.Forms.RadioButton();
            this.rdTanh = new System.Windows.Forms.RadioButton();
            this.rdLogarithm = new System.Windows.Forms.RadioButton();
            this.rdSigmoid = new System.Windows.Forms.RadioButton();
            this.label15 = new System.Windows.Forms.Label();
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
            this.label18 = new System.Windows.Forms.Label();
            this.btnBrowser = new System.Windows.Forms.Button();
            this.tbxCsvFilePath = new System.Windows.Forms.TextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.tbxTrainingRatio = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnModelBrowser = new System.Windows.Forms.Button();
            this.tbxModelFilePath = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnTestBrowser = new System.Windows.Forms.Button();
            this.tbxTestFilePath = new System.Windows.Forms.TextBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.nmNumCluster = new System.Windows.Forms.NumericUpDown();
            this.lblNumCluster = new System.Windows.Forms.Label();
            this.rdSOM = new System.Windows.Forms.RadioButton();
            this.rdKmeans = new System.Windows.Forms.RadioButton();
            this.rdDefault = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.btnTrainBrowser = new System.Windows.Forms.Button();
            this.tbxTrainFilePath = new System.Windows.Forms.TextBox();
            this.btnTrain = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rdSVM = new System.Windows.Forms.RadioButton();
            this.rdANN = new System.Windows.Forms.RadioButton();
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
            this.lblTrainingMeasure = new System.Windows.Forms.Label();
            this.cmbTrainingMeasure = new System.Windows.Forms.ComboBox();
            this.tabOption.SuspendLayout();
            this.tabExperiment.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.gbSVRSetting.SuspendLayout();
            this.gbAnnSetting.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmNumCluster)).BeginInit();
            this.groupBox4.SuspendLayout();
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
            // tabOption
            // 
            this.tabOption.Controls.Add(this.tabExperiment);
            this.tabOption.Controls.Add(this.tabApplication);
            this.tabOption.Location = new System.Drawing.Point(0, 44);
            this.tabOption.Name = "tabOption";
            this.tabOption.SelectedIndex = 0;
            this.tabOption.Size = new System.Drawing.Size(1003, 687);
            this.tabOption.TabIndex = 9;
            this.tabOption.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabOption_Selected);
            // 
            // tabExperiment
            // 
            this.tabExperiment.BackColor = System.Drawing.Color.Transparent;
            this.tabExperiment.Controls.Add(this.groupBox9);
            this.tabExperiment.Controls.Add(this.groupBox7);
            this.tabExperiment.Controls.Add(this.groupBox6);
            this.tabExperiment.Controls.Add(this.groupBox4);
            this.tabExperiment.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabExperiment.Location = new System.Drawing.Point(4, 22);
            this.tabExperiment.Name = "tabExperiment";
            this.tabExperiment.Padding = new System.Windows.Forms.Padding(3);
            this.tabExperiment.Size = new System.Drawing.Size(995, 661);
            this.tabExperiment.TabIndex = 0;
            this.tabExperiment.Text = "EXPERIMENT";
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
            this.groupBox9.Location = new System.Drawing.Point(331, 461);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(657, 157);
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
            this.btnStepTrainAndTest.Location = new System.Drawing.Point(247, 112);
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
            this.tbxEndDate.Location = new System.Drawing.Point(406, 67);
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
            this.label20.Location = new System.Drawing.Point(378, 70);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(20, 13);
            this.label20.TabIndex = 4;
            this.label20.Text = "To";
            // 
            // tbxStartDate
            // 
            this.tbxStartDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxStartDate.ForeColor = System.Drawing.Color.Black;
            this.tbxStartDate.Location = new System.Drawing.Point(222, 67);
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
            this.label19.Location = new System.Drawing.Point(111, 70);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(84, 13);
            this.label19.TabIndex = 2;
            this.label19.Text = "From (d/M/yyyy)";
            // 
            // tbxTrainingSize
            // 
            this.tbxTrainingSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxTrainingSize.ForeColor = System.Drawing.Color.Black;
            this.tbxTrainingSize.Location = new System.Drawing.Point(222, 31);
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
            this.label16.Location = new System.Drawing.Point(111, 34);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(97, 13);
            this.label16.TabIndex = 0;
            this.label16.Text = "Training size (days)";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.cmbNumDaysPredicted);
            this.groupBox7.Controls.Add(this.cmbTrainingMeasure);
            this.groupBox7.Controls.Add(this.gbSVRSetting);
            this.groupBox7.Controls.Add(this.lblTrainingMeasure);
            this.groupBox7.Controls.Add(this.label10);
            this.groupBox7.Controls.Add(this.gbAnnSetting);
            this.groupBox7.Controls.Add(this.label18);
            this.groupBox7.Controls.Add(this.btnBrowser);
            this.groupBox7.Controls.Add(this.tbxCsvFilePath);
            this.groupBox7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.groupBox7.Location = new System.Drawing.Point(0, 11);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(314, 607);
            this.groupBox7.TabIndex = 36;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "SETTING";
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
            this.cmbNumDaysPredicted.Location = new System.Drawing.Point(116, 75);
            this.cmbNumDaysPredicted.Name = "cmbNumDaysPredicted";
            this.cmbNumDaysPredicted.Size = new System.Drawing.Size(141, 21);
            this.cmbNumDaysPredicted.TabIndex = 46;
            // 
            // gbSVRSetting
            // 
            this.gbSVRSetting.Controls.Add(this.ckbProbEstimation);
            this.gbSVRSetting.Controls.Add(this.label3);
            this.gbSVRSetting.Controls.Add(this.tbxGamma);
            this.gbSVRSetting.Controls.Add(this.label2);
            this.gbSVRSetting.Controls.Add(this.tbxC);
            this.gbSVRSetting.Controls.Add(this.tbxNumFold);
            this.gbSVRSetting.Controls.Add(this.label21);
            this.gbSVRSetting.Controls.Add(this.cmbModelSelection);
            this.gbSVRSetting.Controls.Add(this.label6);
            this.gbSVRSetting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbSVRSetting.ForeColor = System.Drawing.Color.Black;
            this.gbSVRSetting.Location = new System.Drawing.Point(10, 390);
            this.gbSVRSetting.Name = "gbSVRSetting";
            this.gbSVRSetting.Size = new System.Drawing.Size(299, 159);
            this.gbSVRSetting.TabIndex = 44;
            this.gbSVRSetting.TabStop = false;
            this.gbSVRSetting.Text = "SVR SETTING";
            // 
            // ckbProbEstimation
            // 
            this.ckbProbEstimation.AutoSize = true;
            this.ckbProbEstimation.Location = new System.Drawing.Point(9, 82);
            this.ckbProbEstimation.Name = "ckbProbEstimation";
            this.ckbProbEstimation.Size = new System.Drawing.Size(125, 17);
            this.ckbProbEstimation.TabIndex = 47;
            this.ckbProbEstimation.Text = "Probability Estimation";
            this.ckbProbEstimation.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 46;
            this.label3.Text = "Gamma = 2^";
            // 
            // tbxGamma
            // 
            this.tbxGamma.Location = new System.Drawing.Point(83, 131);
            this.tbxGamma.Name = "tbxGamma";
            this.tbxGamma.Size = new System.Drawing.Size(100, 20);
            this.tbxGamma.TabIndex = 45;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 44;
            this.label2.Text = "C = 2^";
            // 
            // tbxC
            // 
            this.tbxC.Location = new System.Drawing.Point(83, 105);
            this.tbxC.Name = "tbxC";
            this.tbxC.Size = new System.Drawing.Size(100, 20);
            this.tbxC.TabIndex = 43;
            // 
            // tbxNumFold
            // 
            this.tbxNumFold.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxNumFold.ForeColor = System.Drawing.Color.Black;
            this.tbxNumFold.Location = new System.Drawing.Point(156, 53);
            this.tbxNumFold.Name = "tbxNumFold";
            this.tbxNumFold.Size = new System.Drawing.Size(101, 20);
            this.tbxNumFold.TabIndex = 38;
            this.tbxNumFold.Text = "5";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.Black;
            this.label21.Location = new System.Drawing.Point(6, 56);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(135, 13);
            this.label21.TabIndex = 21;
            this.label21.Text = "Num fold (1: leave one out)";
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
            this.cmbModelSelection.Location = new System.Drawing.Point(100, 22);
            this.cmbModelSelection.Name = "cmbModelSelection";
            this.cmbModelSelection.Size = new System.Drawing.Size(157, 21);
            this.cmbModelSelection.TabIndex = 20;
            this.cmbModelSelection.SelectedIndexChanged += new System.EventHandler(this.cmbModelSelection_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(6, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "Model selection";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(8, 79);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(101, 13);
            this.label10.TabIndex = 40;
            this.label10.Text = "Num days predicted";
            // 
            // gbAnnSetting
            // 
            this.gbAnnSetting.Controls.Add(this.rdSine);
            this.gbAnnSetting.Controls.Add(this.rdTanh);
            this.gbAnnSetting.Controls.Add(this.rdLogarithm);
            this.gbAnnSetting.Controls.Add(this.rdSigmoid);
            this.gbAnnSetting.Controls.Add(this.label15);
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
            this.gbAnnSetting.Location = new System.Drawing.Point(10, 142);
            this.gbAnnSetting.Name = "gbAnnSetting";
            this.gbAnnSetting.Size = new System.Drawing.Size(299, 242);
            this.gbAnnSetting.TabIndex = 23;
            this.gbAnnSetting.TabStop = false;
            this.gbAnnSetting.Text = "ANN SETTING";
            // 
            // rdSine
            // 
            this.rdSine.AutoSize = true;
            this.rdSine.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdSine.ForeColor = System.Drawing.Color.Black;
            this.rdSine.Location = new System.Drawing.Point(227, 214);
            this.rdSine.Name = "rdSine";
            this.rdSine.Size = new System.Drawing.Size(46, 17);
            this.rdSine.TabIndex = 44;
            this.rdSine.TabStop = true;
            this.rdSine.Text = "Sine";
            this.rdSine.UseVisualStyleBackColor = true;
            // 
            // rdTanh
            // 
            this.rdTanh.AutoSize = true;
            this.rdTanh.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdTanh.ForeColor = System.Drawing.Color.Black;
            this.rdTanh.Location = new System.Drawing.Point(167, 214);
            this.rdTanh.Name = "rdTanh";
            this.rdTanh.Size = new System.Drawing.Size(50, 17);
            this.rdTanh.TabIndex = 43;
            this.rdTanh.TabStop = true;
            this.rdTanh.Text = "Tanh";
            this.rdTanh.UseVisualStyleBackColor = true;
            // 
            // rdLogarithm
            // 
            this.rdLogarithm.AutoSize = true;
            this.rdLogarithm.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdLogarithm.ForeColor = System.Drawing.Color.Black;
            this.rdLogarithm.Location = new System.Drawing.Point(84, 214);
            this.rdLogarithm.Name = "rdLogarithm";
            this.rdLogarithm.Size = new System.Drawing.Size(71, 17);
            this.rdLogarithm.TabIndex = 42;
            this.rdLogarithm.TabStop = true;
            this.rdLogarithm.Text = "Logarithm";
            this.rdLogarithm.UseVisualStyleBackColor = true;
            // 
            // rdSigmoid
            // 
            this.rdSigmoid.AutoSize = true;
            this.rdSigmoid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdSigmoid.ForeColor = System.Drawing.Color.Black;
            this.rdSigmoid.Location = new System.Drawing.Point(8, 214);
            this.rdSigmoid.Name = "rdSigmoid";
            this.rdSigmoid.Size = new System.Drawing.Size(62, 17);
            this.rdSigmoid.TabIndex = 41;
            this.rdSigmoid.TabStop = true;
            this.rdSigmoid.Text = "Sigmoid";
            this.rdSigmoid.UseVisualStyleBackColor = true;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.Black;
            this.label15.Location = new System.Drawing.Point(6, 193);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(98, 13);
            this.label15.TabIndex = 40;
            this.label15.Text = "Activation Function";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(7, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 39;
            this.label4.Text = "Num input node";
            // 
            // tbxANNInputNode
            // 
            this.tbxANNInputNode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxANNInputNode.ForeColor = System.Drawing.Color.Black;
            this.tbxANNInputNode.Location = new System.Drawing.Point(120, 20);
            this.tbxANNInputNode.Name = "tbxANNInputNode";
            this.tbxANNInputNode.Size = new System.Drawing.Size(157, 20);
            this.tbxANNInputNode.TabIndex = 38;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.Black;
            this.label17.Location = new System.Drawing.Point(7, 48);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(91, 13);
            this.label17.TabIndex = 37;
            this.label17.Text = "Num hidden node";
            // 
            // tbxANNHiddenNode
            // 
            this.tbxANNHiddenNode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxANNHiddenNode.ForeColor = System.Drawing.Color.Black;
            this.tbxANNHiddenNode.Location = new System.Drawing.Point(120, 47);
            this.tbxANNHiddenNode.Name = "tbxANNHiddenNode";
            this.tbxANNHiddenNode.Size = new System.Drawing.Size(157, 20);
            this.tbxANNHiddenNode.TabIndex = 36;
            // 
            // tbxMomentum
            // 
            this.tbxMomentum.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxMomentum.ForeColor = System.Drawing.Color.Black;
            this.tbxMomentum.Location = new System.Drawing.Point(120, 163);
            this.tbxMomentum.Name = "tbxMomentum";
            this.tbxMomentum.Size = new System.Drawing.Size(157, 20);
            this.tbxMomentum.TabIndex = 33;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.Location = new System.Drawing.Point(7, 164);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(59, 13);
            this.label14.TabIndex = 32;
            this.label14.Text = "Momentum";
            // 
            // tbxMaxLoops
            // 
            this.tbxMaxLoops.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxMaxLoops.ForeColor = System.Drawing.Color.Black;
            this.tbxMaxLoops.Location = new System.Drawing.Point(120, 105);
            this.tbxMaxLoops.Name = "tbxMaxLoops";
            this.tbxMaxLoops.Size = new System.Drawing.Size(157, 20);
            this.tbxMaxLoops.TabIndex = 31;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(7, 106);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(55, 13);
            this.label11.TabIndex = 30;
            this.label11.Text = "Max loops";
            // 
            // tbxBias
            // 
            this.tbxBias.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxBias.ForeColor = System.Drawing.Color.Black;
            this.tbxBias.Location = new System.Drawing.Point(120, 134);
            this.tbxBias.Name = "tbxBias";
            this.tbxBias.Size = new System.Drawing.Size(157, 20);
            this.tbxBias.TabIndex = 29;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(7, 138);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(27, 13);
            this.label12.TabIndex = 28;
            this.label12.Text = "Bias";
            // 
            // tbxLearningRate
            // 
            this.tbxLearningRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxLearningRate.ForeColor = System.Drawing.Color.Black;
            this.tbxLearningRate.Location = new System.Drawing.Point(120, 76);
            this.tbxLearningRate.Name = "tbxLearningRate";
            this.tbxLearningRate.Size = new System.Drawing.Size(157, 20);
            this.tbxLearningRate.TabIndex = 27;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(7, 77);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(69, 13);
            this.label13.TabIndex = 26;
            this.label13.Text = "Learning rate";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.Black;
            this.label18.Location = new System.Drawing.Point(8, 19);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(80, 13);
            this.label18.TabIndex = 35;
            this.label18.Text = "Input file (*.csv)";
            // 
            // btnBrowser
            // 
            this.btnBrowser.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowser.ForeColor = System.Drawing.Color.Black;
            this.btnBrowser.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBrowser.Location = new System.Drawing.Point(224, 30);
            this.btnBrowser.Name = "btnBrowser";
            this.btnBrowser.Size = new System.Drawing.Size(83, 34);
            this.btnBrowser.TabIndex = 19;
            this.btnBrowser.Text = "Browser...";
            this.btnBrowser.UseVisualStyleBackColor = true;
            this.btnBrowser.Click += new System.EventHandler(this.btnBrowser_Click);
            // 
            // tbxCsvFilePath
            // 
            this.tbxCsvFilePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxCsvFilePath.ForeColor = System.Drawing.Color.Black;
            this.tbxCsvFilePath.Location = new System.Drawing.Point(10, 38);
            this.tbxCsvFilePath.Name = "tbxCsvFilePath";
            this.tbxCsvFilePath.Size = new System.Drawing.Size(208, 20);
            this.tbxCsvFilePath.TabIndex = 18;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.groupBox5);
            this.groupBox6.Controls.Add(this.groupBox3);
            this.groupBox6.Controls.Add(this.groupBox2);
            this.groupBox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox6.ForeColor = System.Drawing.Color.Green;
            this.groupBox6.Location = new System.Drawing.Point(331, 122);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(657, 322);
            this.groupBox6.TabIndex = 35;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "TRADITIONAL TRAINING AND TEST";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnOK);
            this.groupBox5.Controls.Add(this.tbxTrainingRatio);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.ForeColor = System.Drawing.Color.Black;
            this.groupBox5.Location = new System.Drawing.Point(11, 22);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(312, 97);
            this.groupBox5.TabIndex = 25;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "1. DATA PREPARATION";
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(115, 50);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(83, 34);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // tbxTrainingRatio
            // 
            this.tbxTrainingRatio.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxTrainingRatio.Location = new System.Drawing.Point(116, 21);
            this.tbxTrainingRatio.Name = "tbxTrainingRatio";
            this.tbxTrainingRatio.Size = new System.Drawing.Size(186, 20);
            this.tbxTrainingRatio.TabIndex = 23;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(5, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "Training set ratio (%)";
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
            this.groupBox3.ForeColor = System.Drawing.Color.Black;
            this.groupBox3.Location = new System.Drawing.Point(334, 22);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(311, 285);
            this.groupBox3.TabIndex = 24;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "3. TEST";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(6, 82);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(52, 13);
            this.label9.TabIndex = 34;
            this.label9.Text = "Model file";
            // 
            // btnModelBrowser
            // 
            this.btnModelBrowser.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModelBrowser.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnModelBrowser.Location = new System.Drawing.Point(219, 94);
            this.btnModelBrowser.Name = "btnModelBrowser";
            this.btnModelBrowser.Size = new System.Drawing.Size(83, 34);
            this.btnModelBrowser.TabIndex = 33;
            this.btnModelBrowser.Text = "Browser...";
            this.btnModelBrowser.UseVisualStyleBackColor = true;
            this.btnModelBrowser.Click += new System.EventHandler(this.btnModelBrowser_Click);
            // 
            // tbxModelFilePath
            // 
            this.tbxModelFilePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxModelFilePath.Location = new System.Drawing.Point(9, 102);
            this.tbxModelFilePath.Name = "tbxModelFilePath";
            this.tbxModelFilePath.Size = new System.Drawing.Size(206, 20);
            this.tbxModelFilePath.TabIndex = 32;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(6, 32);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 13);
            this.label8.TabIndex = 31;
            this.label8.Text = "Test set file";
            // 
            // btnTestBrowser
            // 
            this.btnTestBrowser.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTestBrowser.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTestBrowser.Location = new System.Drawing.Point(219, 44);
            this.btnTestBrowser.Name = "btnTestBrowser";
            this.btnTestBrowser.Size = new System.Drawing.Size(83, 34);
            this.btnTestBrowser.TabIndex = 30;
            this.btnTestBrowser.Text = "Browser...";
            this.btnTestBrowser.UseVisualStyleBackColor = true;
            this.btnTestBrowser.Click += new System.EventHandler(this.btnTestBrowser_Click);
            // 
            // tbxTestFilePath
            // 
            this.tbxTestFilePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxTestFilePath.Location = new System.Drawing.Point(9, 52);
            this.tbxTestFilePath.Name = "tbxTestFilePath";
            this.tbxTestFilePath.Size = new System.Drawing.Size(206, 20);
            this.tbxTestFilePath.TabIndex = 29;
            // 
            // btnTest
            // 
            this.btnTest.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTest.Image = global::GUI.Properties.Resources.Test;
            this.btnTest.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTest.Location = new System.Drawing.Point(93, 155);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(124, 41);
            this.btnTest.TabIndex = 6;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.nmNumCluster);
            this.groupBox2.Controls.Add(this.lblNumCluster);
            this.groupBox2.Controls.Add(this.rdSOM);
            this.groupBox2.Controls.Add(this.rdKmeans);
            this.groupBox2.Controls.Add(this.rdDefault);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.btnTrainBrowser);
            this.groupBox2.Controls.Add(this.tbxTrainFilePath);
            this.groupBox2.Controls.Add(this.btnTrain);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.Black;
            this.groupBox2.Location = new System.Drawing.Point(11, 125);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(312, 182);
            this.groupBox2.TabIndex = 23;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "2. TRAINING";
            // 
            // nmNumCluster
            // 
            this.nmNumCluster.Location = new System.Drawing.Point(104, 99);
            this.nmNumCluster.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nmNumCluster.Name = "nmNumCluster";
            this.nmNumCluster.Size = new System.Drawing.Size(111, 20);
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
            this.lblNumCluster.Location = new System.Drawing.Point(9, 102);
            this.lblNumCluster.Name = "lblNumCluster";
            this.lblNumCluster.Size = new System.Drawing.Size(90, 13);
            this.lblNumCluster.TabIndex = 32;
            this.lblNumCluster.Text = "Num of Cluster";
            // 
            // rdSOM
            // 
            this.rdSOM.AutoSize = true;
            this.rdSOM.Location = new System.Drawing.Point(167, 76);
            this.rdSOM.Name = "rdSOM";
            this.rdSOM.Size = new System.Drawing.Size(52, 17);
            this.rdSOM.TabIndex = 31;
            this.rdSOM.TabStop = true;
            this.rdSOM.Text = "SOM";
            this.rdSOM.UseVisualStyleBackColor = true;
            // 
            // rdKmeans
            // 
            this.rdKmeans.AutoSize = true;
            this.rdKmeans.Location = new System.Drawing.Point(84, 76);
            this.rdKmeans.Name = "rdKmeans";
            this.rdKmeans.Size = new System.Drawing.Size(74, 17);
            this.rdKmeans.TabIndex = 30;
            this.rdKmeans.TabStop = true;
            this.rdKmeans.Text = "K-Means";
            this.rdKmeans.UseVisualStyleBackColor = true;
            // 
            // rdDefault
            // 
            this.rdDefault.AutoSize = true;
            this.rdDefault.Location = new System.Drawing.Point(8, 76);
            this.rdDefault.Name = "rdDefault";
            this.rdDefault.Size = new System.Drawing.Size(66, 17);
            this.rdDefault.TabIndex = 29;
            this.rdDefault.TabStop = true;
            this.rdDefault.Text = "Default";
            this.rdDefault.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(6, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 13);
            this.label7.TabIndex = 28;
            this.label7.Text = "Training set file";
            // 
            // btnTrainBrowser
            // 
            this.btnTrainBrowser.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTrainBrowser.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTrainBrowser.Location = new System.Drawing.Point(219, 32);
            this.btnTrainBrowser.Name = "btnTrainBrowser";
            this.btnTrainBrowser.Size = new System.Drawing.Size(83, 34);
            this.btnTrainBrowser.TabIndex = 27;
            this.btnTrainBrowser.Text = "Browser...";
            this.btnTrainBrowser.UseVisualStyleBackColor = true;
            this.btnTrainBrowser.Click += new System.EventHandler(this.btnTrainBrowser_Click);
            // 
            // tbxTrainFilePath
            // 
            this.tbxTrainFilePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxTrainFilePath.Location = new System.Drawing.Point(9, 40);
            this.tbxTrainFilePath.Name = "tbxTrainFilePath";
            this.tbxTrainFilePath.Size = new System.Drawing.Size(206, 20);
            this.tbxTrainFilePath.TabIndex = 26;
            // 
            // btnTrain
            // 
            this.btnTrain.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTrain.Image = global::GUI.Properties.Resources.Train;
            this.btnTrain.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTrain.Location = new System.Drawing.Point(95, 130);
            this.btnTrain.Name = "btnTrain";
            this.btnTrain.Size = new System.Drawing.Size(124, 41);
            this.btnTrain.TabIndex = 6;
            this.btnTrain.Text = "Train";
            this.btnTrain.UseVisualStyleBackColor = true;
            this.btnTrain.Click += new System.EventHandler(this.btnTrain_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rdSVM);
            this.groupBox4.Controls.Add(this.rdANN);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.groupBox4.Location = new System.Drawing.Point(331, 15);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(183, 92);
            this.groupBox4.TabIndex = 19;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "MODEL TYPE";
            // 
            // rdSVM
            // 
            this.rdSVM.AutoSize = true;
            this.rdSVM.Checked = true;
            this.rdSVM.Location = new System.Drawing.Point(38, 57);
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
            this.rdANN.Location = new System.Drawing.Point(38, 30);
            this.rdANN.Name = "rdANN";
            this.rdANN.Size = new System.Drawing.Size(48, 17);
            this.rdANN.TabIndex = 0;
            this.rdANN.Text = "ANN";
            this.rdANN.UseVisualStyleBackColor = true;
            this.rdANN.CheckedChanged += new System.EventHandler(this.rdANN_CheckedChanged);
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
            this.gbResult.Location = new System.Drawing.Point(427, 502);
            this.gbResult.Name = "gbResult";
            this.gbResult.Size = new System.Drawing.Size(522, 86);
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
            this.btnPredict.Location = new System.Drawing.Point(274, 523);
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
            this.zg1.Size = new System.Drawing.Size(904, 414);
            this.zg1.TabIndex = 1;
            // 
            // lblTrainingMeasure
            // 
            this.lblTrainingMeasure.AutoSize = true;
            this.lblTrainingMeasure.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrainingMeasure.ForeColor = System.Drawing.Color.Black;
            this.lblTrainingMeasure.Location = new System.Drawing.Point(8, 112);
            this.lblTrainingMeasure.Name = "lblTrainingMeasure";
            this.lblTrainingMeasure.Size = new System.Drawing.Size(89, 13);
            this.lblTrainingMeasure.TabIndex = 32;
            this.lblTrainingMeasure.Text = "Training Measure";
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
            this.cmbTrainingMeasure.Location = new System.Drawing.Point(117, 108);
            this.cmbTrainingMeasure.Name = "cmbTrainingMeasure";
            this.cmbTrainingMeasure.Size = new System.Drawing.Size(140, 21);
            this.cmbTrainingMeasure.TabIndex = 31;
            // 
            // MainGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1001, 696);
            this.Controls.Add(this.tabOption);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "MainGUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainGUI_Load);
            this.tabOption.ResumeLayout(false);
            this.tabExperiment.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.gbSVRSetting.ResumeLayout(false);
            this.gbSVRSetting.PerformLayout();
            this.gbAnnSetting.ResumeLayout(false);
            this.gbAnnSetting.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmNumCluster)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tabApplication.ResumeLayout(false);
            this.tabApplication.PerformLayout();
            this.gbResult.ResumeLayout(false);
            this.gbResult.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabOption;
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
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.GroupBox gbSVRSetting;
        private System.Windows.Forms.TextBox tbxNumFold;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ComboBox cmbModelSelection;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label10;
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
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button btnBrowser;
        private System.Windows.Forms.TextBox tbxCsvFilePath;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnOK;
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
        private System.Windows.Forms.GroupBox groupBox4;
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
        private System.Windows.Forms.RadioButton rdSOM;
        private System.Windows.Forms.RadioButton rdKmeans;
        private System.Windows.Forms.RadioButton rdDefault;
        private System.Windows.Forms.NumericUpDown nmNumCluster;
        private System.Windows.Forms.Label lblNumCluster;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbxANNInputNode;
        private System.Windows.Forms.CheckBox ckbProbEstimation;
        private System.Windows.Forms.RadioButton rdTanh;
        private System.Windows.Forms.RadioButton rdLogarithm;
        private System.Windows.Forms.RadioButton rdSigmoid;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.RadioButton rdSine;
        private System.Windows.Forms.ComboBox cmbTrainingMeasure;
        private System.Windows.Forms.Label lblTrainingMeasure;
    }
}
