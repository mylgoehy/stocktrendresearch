using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using DTO;
using BUS;
using BUS.SVM;
using BUS.ANN;
using ZedGraph;
using BUS.ANN.Backpropagation;
using BUS.KMeans;
using System.Text.RegularExpressions;

namespace GUI
{
    public partial class MainGUI : Form
    {
        #region Attributes
        public const int MSE = 1;
        public const int NMSE = 2;
        public const int RMSE = 3;
        public const int SIGN = 4;

        public const int NUM_NODE = 10;

        private StockRecordDTO _stockRecordDTO;
        private StockRecordBUS _stockRecordBUS;
        private string strStockPath;
        private double pricePredict = 0;
        private double trendPredict = 0;
        #endregion

        public MainGUI()
        {
            InitializeComponent();
        }

        private void HandleMeasure(string fileName, double []actual, double []forecast)
        {
            StreamWriter writer = new StreamWriter(fileName);
            MeasureBUS measureBUS = new MeasureBUS();

            double Result = measureBUS.CorrectPredictRate(actual, forecast);
            writer.WriteLine("Correct Predicted:\t" + Result.ToString() + "%");

            writer.Close();
        }
        /// <summary>
        /// Phần train cho SVM
        /// </summary>
        private void TrainSVM()
        {
            int iPos = tbxTrainFilePath.Text.LastIndexOf('_');
            string strMutualPath = tbxTrainFilePath.Text.Remove(iPos + 1);
            string strModelFile = strMutualPath + "model.txt";
            Problem prob = Problem.Read(tbxTrainFilePath.Text);
            Parameter param = new Parameter();

            if (cmbModelSelection.SelectedItem.ToString() == "Grid search")
            {
                string strLogFile = strMutualPath + "Grid.txt";
                double dblC;
                double dblGamma;
                ParameterSelection paramSel = new ParameterSelection();
                paramSel.NFOLD = Int32.Parse(tbxNumFold.Text);
                paramSel.Grid(prob, param, strLogFile, out dblC, out dblGamma);
                param.C = dblC;
                param.Gamma = dblGamma;
                param.Probability = ckbProbEstimation.Checked;
                Model model = Training.Train(prob, param);
                Model.Write(strModelFile, model);
            }
            else if (cmbModelSelection.SelectedItem.ToString() == "Use default values")
            {
                if (tbxC.Text == "" || tbxGamma.Text == "")
                {
                    MessageBox.Show("Please fill in parameters!");
                    return;
                }
                param.C = double.Parse(tbxC.Text);
                param.Gamma = double.Parse(tbxGamma.Text);
                param.Probability = ckbProbEstimation.Checked;
                Model model = Training.Train(prob, param);
                Model.Write(strModelFile, model);
            }
        }
        /// <summary>
        /// Phần train cho K-SVMeans
        /// </summary>
        private void TrainKSVMeans()
        {
            int iNumCluster = (int)nmNumCluster.Value;
            int iPos = tbxTrainFilePath.Text.LastIndexOf('_');
            string strMutualPath = tbxTrainFilePath.Text.Remove(iPos + 1);
            string strClusterModelFile = strMutualPath + "_clusterModel.txt";
            string[] strClusterResultFiles = new string[iNumCluster];
            string[] strSVMModelFiles = new string[iNumCluster];

            for (int i = 0; i < iNumCluster; i++)
            {
                strClusterResultFiles[i] = strMutualPath + "cluster" + (i + 1).ToString() + ".txt";
                strSVMModelFiles[i] = strMutualPath + "model" + (i + 1).ToString() + ".txt";
            }
            // Thực hiện cluster
            SampleDataBUS samDataBUS = new SampleDataBUS();
            samDataBUS.Read(tbxTrainFilePath.Text);
            Clustering clustering = new Clustering(iNumCluster, samDataBUS.Samples, DistanceType.Manhattan);
            clustering.Run(strClusterModelFile, false);
            samDataBUS.WriteIntoCluster(strClusterResultFiles, clustering.SampleData.ClusterIndices);
            // Thực hiện train SVM
            for (int i = 0; i < iNumCluster; i++)
            {
                Problem prob = Problem.Read(strClusterResultFiles[i]);
                Parameter param = new Parameter();

                if (cmbModelSelection.SelectedItem.ToString() == "Grid search")
                {
                    string strLogFile = strMutualPath + "GridCluster" + (i + 1).ToString() + ".txt";
                    double dblC;
                    double dblGamma;
                    ParameterSelection paramSel = new ParameterSelection();
                    paramSel.NFOLD = Int32.Parse(tbxNumFold.Text);
                    paramSel.Grid(prob, param, strLogFile, out dblC, out dblGamma);
                    param.C = dblC;
                    param.Gamma = dblGamma;
                    param.Probability = ckbProbEstimation.Checked;
                    Model model = Training.Train(prob, param);
                    Model.Write(strSVMModelFiles[i], model);
                }
                else if (cmbModelSelection.SelectedItem.ToString() == "Use default values")
                {
                    if (tbxC.Text == "" || tbxGamma.Text == "")
                    {
                        MessageBox.Show("Please fill in parameters!");
                        return;
                    }
                    param.C = double.Parse(tbxC.Text);
                    param.Gamma = double.Parse(tbxGamma.Text);
                    param.Probability = ckbProbEstimation.Checked;
                    Model model = Training.Train(prob, param);
                    Model.Write(strSVMModelFiles[i], model);
                }
            }
        }
        /// <summary>
        /// Phần train cho ANN
        /// </summary>
        private void TrainANN()
        {
            int iPos = tbxTrainFilePath.Text.LastIndexOf('_');
            string strModelFile = tbxTrainFilePath.Text.Remove(iPos + 1) + "ANNmodel.txt";

            //khởi tạo các tham số cho mạng
            ANNParameterBUS.InputNode = int.Parse(tbxANNInputNode.Text);
            ANNParameterBUS.HiddenNode = int.Parse(tbxANNHiddenNode.Text);
            ANNParameterBUS.OutputNode = 3;
            ANNParameterBUS.MaxEpoch = int.Parse(tbxMaxLoops.Text);
            ANNParameterBUS.LearningRate = double.Parse(tbxLearningRate.Text);
            ANNParameterBUS.Momentum = double.Parse(tbxMomentum.Text);
            ANNParameterBUS.Bias = double.Parse(tbxBias.Text);

            //ANNParameterBUS.Accuracy = double.Parse(tbxAccuracy.Text);
            ANNParameterBUS.MeasureType = cmbTrainingMeasure.SelectedItem.ToString();

            //Tiến hành train
            //Tiến hành train
            BackpropagationNetwork bpNetwork;

            LinearLayer inputLayer = new LinearLayer(ANNParameterBUS.InputNode);
            ActivationLayer hidenLayer = null;
            ActivationLayer outputLayer = null;
            switch (cmbActivationFunc.SelectedItem.ToString())
            {
                case "Sigmoid":
                    hidenLayer = new SigmoidLayer(ANNParameterBUS.HiddenNode);
                    outputLayer = new SigmoidLayer(ANNParameterBUS.OutputNode);
                    break;
                case "Tanh":
                    hidenLayer = new TanhLayer(ANNParameterBUS.HiddenNode);
                    outputLayer = new TanhLayer(ANNParameterBUS.OutputNode);
                    break;
                case "Logarithm":
                    hidenLayer = new LogarithmLayer(ANNParameterBUS.HiddenNode);
                    outputLayer = new LogarithmLayer(ANNParameterBUS.OutputNode);
                    break;
                case "Sine":
                    hidenLayer = new SineLayer(ANNParameterBUS.HiddenNode);
                    outputLayer = new SineLayer(ANNParameterBUS.OutputNode);
                    break;
            }

            new BackpropagationConnector(inputLayer, hidenLayer);
            new BackpropagationConnector(hidenLayer, outputLayer);

            bpNetwork = new BackpropagationNetwork(inputLayer, outputLayer);

            bpNetwork.SetLearningRate(ANNParameterBUS.LearningRate);

            TrainingSet trainSet = new TrainingSet(ANNParameterBUS.InputNode, ANNParameterBUS.OutputNode);
            trainSet.CreateTrainingSet(tbxTrainFilePath.Text);

            bpNetwork.Learn(trainSet, ANNParameterBUS.MaxEpoch);

            // Lưu lại model
            Stream stream = File.Open(strModelFile, FileMode.Create);
            BinaryFormatter bformatter = new BinaryFormatter();
            bformatter.Serialize(stream, bpNetwork);
            stream.Close();

            //ANNModelBUS.AnnModelFile = strModelFile;
            //ANNTrainBUS annTrain = new ANNTrainBUS();
            //annTrain.LoadDataSet(tbxTrainFilePath.Text);
            //annTrain.Main(iMeasureType);
                
        }
        /// <summary>
        /// Phần test cho SVM
        /// </summary>
        private void TestSVM()
        {
            int iPos = tbxTestFilePath.Text.LastIndexOf('_');
            string strMutualPath = tbxTestFilePath.Text.Remove(iPos + 1);
            string strPredictedFile = strMutualPath + "predict.txt";
            string strStatisticFile = strMutualPath + "statistic.txt";
            Problem prob = Problem.Read(tbxTestFilePath.Text);
            Model model = Model.Read(tbxModelFilePath.Text);
            double dblPrecision = Prediction.Predict(prob, strPredictedFile, model, ckbProbEstimation.Checked);
            StatisticTrend2File(strPredictedFile, strStatisticFile);
            StreamWriter writer = new StreamWriter(strMutualPath + "performance.txt");
            writer.WriteLine(dblPrecision);
            writer.Close();
        }
        /// <summary>
        /// Phần test cho K-SVMeans
        /// </summary>
        private void TestKSVMeans()
        {
            int iPos = tbxTestFilePath.Text.LastIndexOf('_');
            string strMutualPath = tbxTestFilePath.Text.Remove(iPos + 1);
            int iNumCluster = (int)nmNumCluster.Value;
            string strClusterModelFile = strMutualPath + "_clusterModel.txt";
            string[] strClusterResultFiles = new string[iNumCluster];
            string[] strSVMModelFiles = new string[iNumCluster];
            string[] strPredictedFiles = new string[iNumCluster];
            string strStatisticFile = strMutualPath + "statistic.txt";
            for (int i = 0; i < iNumCluster; i++)
            {
                strClusterResultFiles[i] = strMutualPath + "testcluster" + (i + 1).ToString() + ".txt";
                strSVMModelFiles[i] = strMutualPath + "model" + (i + 1).ToString() + ".txt";
                strPredictedFiles[i] = strMutualPath + "predict" + (i + 1).ToString() + ".txt";
            }
            // Thực hiện cluster
            SampleDataBUS samDataBUS = new SampleDataBUS();
            samDataBUS.Read(tbxTestFilePath.Text);
            Clustering clustering = new Clustering(samDataBUS.Samples, DistanceType.Manhattan);
            clustering.Run(strClusterModelFile, true);
            samDataBUS.WriteIntoCluster(strClusterResultFiles, clustering.SampleData.ClusterIndices);
            // Thực hiện test SVM
            StreamWriter writer = new StreamWriter(strMutualPath + "performance.txt");
            double dblTotalPrecision = 0;
            for (int i = 0; i < iNumCluster; i++)
            {
                Problem prob = Problem.Read(strClusterResultFiles[i]);
                Model model = Model.Read(strSVMModelFiles[i]);
                double dblPrecision = Prediction.Predict(prob, strPredictedFiles[i], model, ckbProbEstimation.Checked);
                writer.WriteLine("Cluster " + (i + 1).ToString() + ": " + dblPrecision);
                if (clustering.Clusters[i].NumSample > 0)
                {
                    dblTotalPrecision += dblPrecision * clustering.Clusters[i].NumSample;
                }
            }
            StatisticTrend2File(strPredictedFiles, strStatisticFile);
            writer.WriteLine("All: " + dblTotalPrecision / samDataBUS.DataLines.Length);
            writer.Close();
        }
        /// <summary>
        /// Phần test cho ANN
        /// </summary>
        private void TestANN()
        {
            int iPos = tbxTestFilePath.Text.LastIndexOf('_');
            string strPredictedFile = tbxTestFilePath.Text.Remove(iPos + 1) + "predict.txt";
            string strStatisticFile = tbxTestFilePath.Text.Remove(iPos + 1) + "statistic.txt";

            // Load model lên
            BackpropagationNetwork bpNetwork;

            Stream stream = File.Open(tbxModelFilePath.Text, FileMode.Open);
            BinaryFormatter bformatter = new BinaryFormatter();
            bpNetwork = (BackpropagationNetwork)bformatter.Deserialize(stream);
            stream.Close();

            TrainingSet testSet = new TrainingSet(bpNetwork.InputLayer.NeuronCount, bpNetwork.OutputLayer.NeuronCount);
            testSet.CreateTrainingSet(tbxTestFilePath.Text);

            //Ma trận với dòng thứ 1 chứa các giá trị thực và dòng thứ 2 chứa các giá trị dự đoán.
            double[][] dblActual_Forecast = new double[2][];
            dblActual_Forecast[0] = new double[testSet.TrainingSampleCount];
            dblActual_Forecast[1] = new double[testSet.TrainingSampleCount];

            // Thực hiện test
            for (int i = 0; i < testSet.TrainingSampleCount; i++)
            {
                TrainingSample testSample = testSet[i];
                dblActual_Forecast[0][i] = ConverterBUS.Convert2Trend(testSample.OutputVector);

                double[] dblTemp = bpNetwork.Run(testSample.InputVector);
                dblActual_Forecast[1][i] = ConverterBUS.Convert2Trend(dblTemp);
            }

            bpNetwork.Write2PredictFile(dblActual_Forecast, strPredictedFile);
            StatisticTrend2File(strPredictedFile, strStatisticFile);
            HandleMeasure(tbxTestFilePath.Text.Remove(iPos + 1) + "PerformanceMeasure.txt", dblActual_Forecast[0], dblActual_Forecast[1]);
        }

        private void btnTrain_Click(object sender, EventArgs e)
        {
            if (tbxTrainFilePath.Text == "")
            {
                MessageBox.Show("Error: You must fill all required inputs!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (rdSVM.Checked)
            {
                TrainSVM();
            }
            else if (rdKSVMeans.Checked)
            {
                TrainKSVMeans();
            }
            else if(rdANN.Checked)//Mô hình ANN
            {
                TrainANN();
            }
            MessageBox.Show("Finish!");
        }

        private void btnBrowser_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDlg = new OpenFileDialog();
            openFileDlg.Filter = "(*.csv)|*.csv";
            if (openFileDlg.ShowDialog() == DialogResult.OK)
            {
                tbxCsvFilePath.Text = openFileDlg.FileName;
                tbxTrainFilePath.Text = openFileDlg.FileName.Replace(".csv", "") + "_" + cmbNumDaysPredicted.Text + "_train.txt";
                tbxTestFilePath.Text = openFileDlg.FileName.Replace(".csv", "") + "_" + cmbNumDaysPredicted.Text + "_test.txt";
                tbxModelFilePath.Text = openFileDlg.FileName.Replace(".csv", "") + "_" + cmbNumDaysPredicted.Text + "_model.txt";
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            if (tbxTestFilePath.Text == "" || tbxModelFilePath.Text == "")
            {
                MessageBox.Show("Error: You must fill all required inputs!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (rdSVM.Checked)
            {
                TestSVM();
            }
            else if (rdKSVMeans.Checked)
            {
                TestKSVMeans();
            }
            else if(rdANN.Checked)
            {
                TestANN();
            }

            MessageBox.Show("Finish!");           
        }

        private void btnPreprocess_Click(object sender, EventArgs e)
        {
            if (tbxCsvFilePath.Text == "" || tbxTrainingRatio.Text == "")
            {
                MessageBox.Show("Error: You must fill all required inputs!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //1. Đọc dữ liệu từ file .csv vào mảng và tiền xử lý
            StockRecordBUS stockRecordBUS = new StockRecordBUS();
            StockRecordDTO stockRecordDTO = stockRecordBUS.LoadData(tbxCsvFilePath.Text);
            double[] dblClosePrices = new double[stockRecordDTO.Entries.Count];
            double[] dblVolumes = new double[stockRecordDTO.Entries.Count];
            int i = 0;
            foreach (EntryDTO entryDTO in stockRecordDTO.Entries)
            {
                dblClosePrices[i] = entryDTO.ClosePrice;
                dblVolumes[i] = entryDTO.Volume;
                i++;
            }
            
            //2. Chuyển sang định dạng của LibSVM (dựa vào số node đầu vào)
            ConverterBUS converter = new ConverterBUS();
            int iPos = tbxCsvFilePath.Text.LastIndexOf('\\');
            string strFolderPath = tbxCsvFilePath.Text.Remove(iPos+1);
            string strTotalFile = strFolderPath + stockRecordDTO.ID + ".txt";
            int numDaysPredicted = Int32.Parse(cmbNumDaysPredicted.Text);
            int iNumLine = 0;

            ConverterBUS.Convert(dblClosePrices, dblVolumes, numDaysPredicted, strTotalFile, out iNumLine);

            //3. Từ file chứa toàn bộ dữ liệu ta phân phối vào 2 file train và test (dựa vào tỉ lệ bộ train)
            string strTrainFile = strFolderPath + stockRecordDTO.ID + "_" + numDaysPredicted + "_train.txt";
            string strTestFile = strFolderPath + stockRecordDTO.ID + "_" + numDaysPredicted + "_test.txt";
            StreamReader reader = new StreamReader(strTotalFile);
            StreamWriter trainWriter = new StreamWriter(strTrainFile);
            StreamWriter testWriter = new StreamWriter(strTestFile);

            double dblTrainingSetRatio = Convert.ToDouble(tbxTrainingRatio.Text);
            //int iBound = numDaysPredicted > iNumInputNode ? 2 * numDaysPredicted : numDaysPredicted + iNumInputNode;
            //int iNumLine = dblSource.Length - iBound + 1;
            int iDivideLine = (int)(dblTrainingSetRatio * iNumLine/100);
            for (i = 0; i < iDivideLine; i++)
            {
                string strLine = reader.ReadLine();
                trainWriter.WriteLine(strLine);
            }
            for (; i < iNumLine; i++)
            {
                string strLine = reader.ReadLine();
                testWriter.WriteLine(strLine);
            }

            testWriter.Close();
            trainWriter.Close();
            reader.Close();

            MessageBox.Show("Finish!");
        }

        private void MainGUI_Load(object sender, EventArgs e)
        {
            //Khởi gán
            cmbNumDaysPredicted.SelectedIndex = 0;
            tbxTrainingRatio.Text = "80";
            cmbModelSelection.SelectedIndex = 0;
            cmbTrainingMeasure.SelectedIndex = 0;
            cmbActivationFunc.SelectedIndex = 0;
            lblNumCluster.Enabled = false;
            nmNumCluster.Enabled = false;
            cmbExperimentMode.SelectedIndex = 0;

            //Khởi gán tham số ANN
            tbxANNInputNode.Text = 10.ToString();
            tbxANNHiddenNode.Text = 4.ToString();
            tbxLearningRate.Text = 0.3.ToString();
            tbxMaxLoops.Text = 2000.ToString();
            tbxBias.Text = 0.ToString();
            tbxMomentum.Text = 0.01.ToString();
            //tbxAccuracy.Text = 90.ToString();

            if (rdANN.Checked == true)
            {
                gbAnnSetting.Enabled = true;
            }
            else
            {
                gbAnnSetting.Enabled = false;
            }

            _stockRecordBUS = new StockRecordBUS();
            _stockRecordDTO = null;
            strStockPath = "";
        }

        private void CreateGraph(ZedGraphControl zgc)
        {
            GraphPane myPane = zgc.GraphPane;
            myPane.CurveList.Clear();
            
            // Set the titles and axis labels
            myPane.Title.Text = "Stock Chart";
            myPane.XAxis.Title.Text = "Time";
            myPane.YAxis.Title.Text = "Price";

            // Make up some data points from data
            //1. Đọc dữ liệu từ file .csv vào mảng và tiền xử lý
            PointPairList list = new PointPairList();
            DateTime from = dtpFrom.Value;
            DateTime to = dtpTo.Value;

            foreach (EntryDTO entryDTO in _stockRecordDTO.Entries)
            {
                if (entryDTO.Date.Subtract(from).Days >= 0 && entryDTO.Date.Subtract(to).Days <= 0)
                {
                    double x = (double)new XDate(entryDTO.Date);
                    list.Add(x, entryDTO.ClosePrice);
                }
            }
            
            // Generate a blue curve with circle symbols, and "My Curve 2" in the legend
            LineItem myCurve = myPane.AddCurve("Close Price", list, Color.Blue, SymbolType.None);

            // Fill the axis background with a color gradient
            myPane.Chart.Fill = new Fill(Color.White);

            // Fill the pane background with a color gradient
            myPane.Fill = new Fill(Color.White);

            // Set the XAxis to date type
            myPane.XAxis.Type = AxisType.DateAsOrdinal;
            
            // Calculate the Axis Scale Ranges
            zgc.AxisChange();
            zgc.Invalidate();
        }

        private void btnTrainBrowser_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDlg = new OpenFileDialog();
            openFileDlg.Filter = "(*.txt)|*.txt";
            if (openFileDlg.ShowDialog() == DialogResult.OK)
            {
                tbxTrainFilePath.Text = openFileDlg.FileName;
            }
        }

        private void btnTestBrowser_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDlg = new OpenFileDialog();
            openFileDlg.Filter = "(*.txt)|*.txt";
            if (openFileDlg.ShowDialog() == DialogResult.OK)
            {
                tbxTestFilePath.Text = openFileDlg.FileName;
            }
        }

        private void btnModelBrowser_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDlg = new OpenFileDialog();
            openFileDlg.Filter = "(*.txt)|*.txt";
            if (openFileDlg.ShowDialog() == DialogResult.OK)
            {
                tbxModelFilePath.Text = openFileDlg.FileName;
            }
        }


        private void rdANN_CheckedChanged(object sender, EventArgs e)
        {
            gbAnnSetting.Enabled = rdANN.Checked;
            gbSVRSetting.Enabled = !rdANN.Checked;
        }

        private void btnStepTrainAndTest_Click(object sender, EventArgs e)
        {
            try
            {
                bool isSVR = true;
                if (rdANN.Checked == true)
                {
                    isSVR = false;
                    ANNParameterBUS.HiddenNode = Convert.ToInt16(tbxANNHiddenNode.Text);
                    ANNParameterBUS.OutputNode = 1;
                    ANNParameterBUS.MaxEpoch = Convert.ToInt16(tbxMaxLoops.Text);
                    ANNParameterBUS.LearningRate = Convert.ToDouble(tbxLearningRate.Text);
                    ANNParameterBUS.Momentum = Convert.ToDouble(tbxMomentum.Text);
                    ANNParameterBUS.Bias = Convert.ToDouble(tbxBias.Text);
                }

                double[][] dblActual_Forecast = new double[2][];
                StepTrainingBUS stepTrainingBUS = new StepTrainingBUS();
                stepTrainingBUS.NumInputNode = Convert.ToInt16(NUM_NODE);
                stepTrainingBUS.TrainingSize = Convert.ToInt16(tbxTrainingSize.Text);
                //stepTrainingBUS.Preprocess = cmbPreprocess.SelectedItem.ToString();
                stepTrainingBUS.ModelSelection = cmbModelSelection.SelectedItem.ToString();
                stepTrainingBUS.NumFold = tbxNumFold.Text;
                stepTrainingBUS.ImprovedDirection = true;

                switch (cmbTrainingMeasure.SelectedItem.ToString())
                {
                    case "MSE":
                        stepTrainingBUS.TrainingMeasure = MSE;
                        break;
                    case "NMSE":
                        stepTrainingBUS.TrainingMeasure = NMSE;
                        break;
                    case "RMSE":
                        stepTrainingBUS.TrainingMeasure = RMSE;
                        break;
                    case "Sign":
                        stepTrainingBUS.TrainingMeasure = SIGN;
                        break;
                }
                stepTrainingBUS.LoadWholdeData(tbxCsvFilePath.Text);

                if (tbxStartDate.Text == "WholeData")
                {
                    stepTrainingBUS.CurrentDateIndex = stepTrainingBUS.TrainingSize - 1;
                }
                else
                {
                    stepTrainingBUS.CurrentDateIndex =
                        stepTrainingBUS.FindIndex(DateTime.ParseExact(tbxStartDate.Text, "d/M/yyyy", null));
                }
                int iEndIndex;
                if (tbxEndDate.Text == "WholeData")
                {
                    iEndIndex = stepTrainingBUS.WholeData.Entries.Count-2;
                }
                else
                {
                    iEndIndex =
                        stepTrainingBUS.FindIndex(DateTime.ParseExact(tbxEndDate.Text, "d/M/yyyy", null));
                }
                if (iEndIndex == -1 || iEndIndex > stepTrainingBUS.WholeData.Entries.Count - 2 || iEndIndex < stepTrainingBUS.CurrentDateIndex)
                {
                    MessageBox.Show("Error: Invalid input!");
                    return;
                }

                int iLen = iEndIndex + 1 - stepTrainingBUS.CurrentDateIndex;
                dblActual_Forecast[0] = new double[iLen];
                dblActual_Forecast[1] = new double[iLen];
                for (int i = 0; i < iLen; i++)
                {
                    EntryDTO entryDTO = (EntryDTO)stepTrainingBUS.WholeData.Entries[stepTrainingBUS.CurrentDateIndex + 1];
                    dblActual_Forecast[0][i] = entryDTO.ClosePrice;
                    //Tạm thời bỏ dòng này chưa sử dụng step training cho ANN
                    //dblActual_Forecast[1][i] = stepTrainingBUS.TrainAndPredict(isSVR);
                    stepTrainingBUS.CurrentDateIndex++;
                }

                //Ghi kết quả cuối cùng ra file
                StreamWriter finalResult = new StreamWriter("StepPredict.txt");
                for (int i = 0; i < iLen; i++)
                {
                    finalResult.WriteLine(dblActual_Forecast[0][i].ToString() + "\t" + dblActual_Forecast[1][i].ToString());
                }
                finalResult.Close();

                HandleMeasure("StepMeasure.txt", dblActual_Forecast[0], dblActual_Forecast[1]);
                MessageBox.Show("Finish!");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void cmbStockID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbStockID.SelectedItem != null)
            {
                strStockPath = "Excel" + "\\" + cmbStockID.SelectedItem.ToString() + ".csv";
                _stockRecordDTO = _stockRecordBUS.LoadData(strStockPath);
                dtpFrom.Value = ((EntryDTO)_stockRecordDTO.Entries[0]).Date;
                dtpTo.Value = ((EntryDTO)_stockRecordDTO.Entries[_stockRecordDTO.Entries.Count - 1]).Date;
                dtpInputDay.Value = ((EntryDTO)_stockRecordDTO.Entries[_stockRecordDTO.Entries.Count - 1]).Date;
                CreateGraph(zg1);
            }            
        }

        private void btnPredict_Click(object sender, EventArgs e)
        {
            //Ma trận với dòng thứ 1 chứa các giá trị thực và dòng thứ 2 chứa các giá trị dự đoán.
            double[][] dblActual_Forecast = new double[2][];
            dblActual_Forecast[0] = null;
            dblActual_Forecast[1] = null;

            #region Price
            #region Ghi File Test
            //Dùng tạm số node input = 5, sau này bổ sung hàm đọc file số node input
            int numInputNode = 5;

            //Tìm ngày cần dự đoán
            DateTime inputDay = dtpInputDay.Value;
            DateTime Today = ((EntryDTO)_stockRecordDTO.Entries[_stockRecordDTO.Entries.Count - 1]).Date;
            int i;
            for (i = 0; i < _stockRecordDTO.Entries.Count - numInputNode; i++)
            {
                EntryDTO curEntry = (EntryDTO)_stockRecordDTO.Entries[i + numInputNode];
                if (inputDay.Subtract(curEntry.Date).Days <= 0 && inputDay.Subtract(Today).Days <= 0)
                {
                    break;
                }
            }

            //Ghi bộ input đầu vào cho ngày dự đoán
            double[] dblSource = new double[numInputNode + 1];
            if (i < _stockRecordDTO.Entries.Count - numInputNode)
            {
                //1. Đọc dữ liệu vào mảng
                double[] dblTemp = new double[_stockRecordDTO.Entries.Count];
                for (int j = 0; j < _stockRecordDTO.Entries.Count; j++)
                {
                    EntryDTO curEntry = (EntryDTO)_stockRecordDTO.Entries[j];
                    dblTemp[j] = curEntry.ClosePrice;
                }

                for (int j = 0; j < numInputNode; j++)
                {
                    EntryDTO curEntry = (EntryDTO)_stockRecordDTO.Entries[i + j];
                    dblSource[j] = curEntry.ClosePrice;
                }

                PreprocessBUS preprocessBUS = new PreprocessBUS();
                preprocessBUS.FindMinMax(dblTemp);
                dblSource = preprocessBUS.PreprocessByMinMax(dblSource);

                //2. Tạo file test. File test này chỉ có 2 dòng:
                //+Dòng 1: Chứa thông tin tiền xử lý
                //+Dòng 2: Giống như 1 dòng của file training, nhưng giá trị đích không biết (để bằng 0)
                StreamWriter writer = new StreamWriter(@"TestPrice.txt");
                writer.WriteLine("ScaleByMinMax " + preprocessBUS.Min.ToString() + " " + preprocessBUS.Max.ToString());

                string strLine = "0 ";
                for (int j = 0; j < numInputNode; j++)
                {
                    strLine += (j + 1).ToString() + ":" + dblSource[j].ToString() + " ";
                }
                writer.WriteLine(strLine);

                writer.Close();
            }
            else
            {
                MessageBox.Show("Error: Invalid input!");
            }
            #endregion
            #region Dự đoán ANN
            ANNModelBUS.AnnModelFile = @"AppModel\ANNPrice\" + cmbStockID.SelectedItem.ToString() + "_1_model.txt";

            ANNParameterBUS.LoadParameter();

            ANNPredictBUS annPredict = new ANNPredictBUS();
            annPredict.LoadDataSet(@"TestPrice.txt");
            //Tạm thời bỏ dòng này chưa sử dụng trong predict
            //dblActual_Forecast = annPredict.MainProcessTrend();
            pricePredict = dblActual_Forecast[1][0];
            tbxANNPrice.Text = Math.Round(dblActual_Forecast[1][0], 2).ToString();
            #endregion
            string[] strArgs = new string[3];
            strArgs[0] = @"TestPrice.txt";
            strArgs[1] = @"AppModel\SVRPrice\" + cmbStockID.SelectedItem.ToString() + "_1_model.txt";
            strArgs[2] = "price_predicted.txt";
            //dblActual_Forecast = svm_predict.MainProcess(strArgs);
            tbxSVMPrice.Text = Math.Round(dblActual_Forecast[1][0], 2).ToString();
            #endregion
            #region Trend
            //Dùng tạm số node input = 5, sau này bổ sung hàm đọc file số node input
            numInputNode = 5;

            //Tìm ngày cần dự đoán
            inputDay = dtpInputDay.Value;
            Today = ((EntryDTO)_stockRecordDTO.Entries[_stockRecordDTO.Entries.Count - 1]).Date;
            for (i = 0; i < _stockRecordDTO.Entries.Count; i++)
            {
                EntryDTO curEntry = (EntryDTO)_stockRecordDTO.Entries[i];
                if (inputDay.Subtract(curEntry.Date).Days <= 0 && inputDay.Subtract(Today).Days <= 0)
                {
                    break;
                }
            }
            int numDaysPredicted = 1; 
            if (!int.TryParse(tbxNumDayTrendPredict.Text, out numDaysPredicted))
            {
                MessageBox.Show("Please enter a number");
                return;
            } 

            //Ghi bộ input đầu vào cho ngày dự đoán
            dblSource = new double[numInputNode * numDaysPredicted + numDaysPredicted + 1];
            if (i < _stockRecordDTO.Entries.Count && i > numInputNode * numDaysPredicted)
            {
                for (int k = 0; k < dblSource.Length; k++)
                {
                    dblSource[k] = 0;
                }
                //1. Đọc dữ liệu vào mảng
                for (int j = 0; j < numInputNode * numDaysPredicted; j++)
                {
                    EntryDTO curEntry = (EntryDTO)_stockRecordDTO.Entries[i - j];
                    dblSource[j] = curEntry.ClosePrice;
                }
                for (int k = 0; k < dblSource.Length; k++)
                {
                    if (dblSource[k] == 0)
                    {
                        dblSource[k] = dblSource[0];
                    }
                }
                PreprocessBUS preprocessBUS = new PreprocessBUS();
                dblSource = preprocessBUS.Scale_SVR_Return(dblSource.Length, dblSource, 1, 1);

                //2. Chuyển sang định dạng của LibSVM (dựa vào số node đầu vào)
                ConverterBUS converter = new ConverterBUS();
                  
                int iNumLine = 0;

                converter.ConvertForTrend(numDaysPredicted, numInputNode, dblSource, "TestTrend.txt", out iNumLine, 2, true);

                #region Dự đoán ANN

                if (numDaysPredicted >= 1 && numDaysPredicted < 5)
                {
                    ANNModelBUS.AnnModelFile = @"AppModel\ANNTrend\" + cmbStockID.SelectedItem.ToString() + "_1_model.txt";
                    strArgs[1] = @"AppModel\SVRTrend\" + cmbStockID.SelectedItem.ToString() + "_1_model.txt";
                }
                else if (numDaysPredicted >= 5 && numDaysPredicted < 10)
                {
                    ANNModelBUS.AnnModelFile = @"AppModel\ANNTrend\" + cmbStockID.SelectedItem.ToString() + "_5_model.txt";
                    strArgs[1] = @"AppModel\SVRTrend\" + cmbStockID.SelectedItem.ToString() + "_5_model.txt";
                }
                else if (numDaysPredicted >= 10 && numDaysPredicted < 30)
                {
                    ANNModelBUS.AnnModelFile = @"AppModel\ANNTrend\" + cmbStockID.SelectedItem.ToString() + "_10_model.txt";
                    strArgs[1] = @"AppModel\SVRTrend\" + cmbStockID.SelectedItem.ToString() + "_10_model.txt";
                }
                else
                {
                    ANNModelBUS.AnnModelFile = @"AppModel\ANNTrend\" + cmbStockID.SelectedItem.ToString() + "_30_model.txt";
                    strArgs[1] = @"AppModel\SVRTrend\" + cmbStockID.SelectedItem.ToString() + "_30_model.txt";
                }

                ANNParameterBUS.LoadParameter();

                ANNPredictBUS annPredictTrend = new ANNPredictBUS();
                annPredictTrend.LoadDataSet(@"TestTrend.txt");
                dblActual_Forecast = annPredictTrend.MainProcessTrend();
                trendPredict = dblActual_Forecast[1][0];
                tbxANNTrend.Text = dblActual_Forecast[1][0] > 0 ? "Tăng" : "Giảm";

                strArgs[0] = @"TestTrend.txt";
                strArgs[2] = "trend_predicted.txt";
                //dblActual_Forecast = svm_predict.MainProcess(strArgs);
                tbxSVMTrend.Text = dblActual_Forecast[1][0] > 0 ? "Tăng" : "Giảm";

                #endregion
                
 
              
            }
            else
            {
                MessageBox.Show("Error: Invalid input!");
            }
            
            
            #endregion            
            
        }

        private void tabOption_Selected(object sender, TabControlEventArgs e)
        {
            //Load tất cả mã chứng khoán lên combobox
            string[] fileNames = Directory.GetFiles("Excel");
            cmbStockID.Items.Clear();
            for (int i = 0; i < fileNames.Length; i++)
            {
                string strTemp = fileNames[i].Substring(fileNames[i].LastIndexOf('\\') + 1);
                strTemp = strTemp.Remove(strTemp.IndexOf('.')).ToUpper();

                cmbStockID.Items.Add(strTemp);
            }

            if (cmbStockID.Items.Count > 0)
            {
                cmbStockID.SelectedIndex = 0;
            }
        }

        private void dtpFrom_ValueChanged(object sender, EventArgs e)
        {
            CreateGraph(zg1);
        }

        private void dtpTo_ValueChanged(object sender, EventArgs e)
        {
            CreateGraph(zg1);
        }

        private void cmbModelSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbModelSelection.Text == "Use default values")
            {
                tbxC.ReadOnly = false;
                tbxGamma.ReadOnly = false;
            }
            else
            {
                tbxC.ReadOnly = true;
                tbxGamma.ReadOnly = true;
            }
        }

        private void StatisticTrend2File(string predictedFile, string statisticFile)
        {
            string strTemp;
            double[][] actual_Forecasts = new double[2][];            

            // Đọc từ file predict lên
            StreamReader reader = new StreamReader(predictedFile);
            StreamWriter writer = new StreamWriter(statisticFile);

            reader.ReadLine();// bỏ dòng đầu

            strTemp = reader.ReadToEnd();
            reader.Close();
            string[] strActual_Forecasts = Regex.Split(strTemp,"\n");
            int iLen = 0;
            for (int i = 0; i < strActual_Forecasts.Length; i++)
            {
                if(strActual_Forecasts[i] != "")
                {
                    iLen++;
                }
            }
            actual_Forecasts[0] = new double[iLen];
            actual_Forecasts[1] = new double[iLen];

            for (int i = 0; i < strActual_Forecasts.Length; i++)
            {
                if (strActual_Forecasts[i] != "")
                {
                    actual_Forecasts[0][i] = double.Parse(Regex.Split(strActual_Forecasts[i], " ")[0]);
                    actual_Forecasts[1][i] = double.Parse(Regex.Split(strActual_Forecasts[i], " ")[1]);
                }
            }

            //thông kê kết quả dự đoán so với thực tế
            strTemp = "Predicted Trend\t Actual Trend\n\tUptrend\tNotrend\tDowntrend";
            writer.WriteLine(strTemp);

            int[][] tables;
            tables = new int[3][];
            for (int i = 0; i < tables.Length; i++)
            {
                tables[i] = new int[3];
            }
            for (int i = 0; i < actual_Forecasts[0].Length; i++)
            {
                int iTemp = (int)actual_Forecasts[0][i];
                switch (iTemp)
                {
                    case 1:
                        if (actual_Forecasts[1][i] == 1)
                        {
                            tables[0][0]++;
                        }
                        else if (actual_Forecasts[1][i] == 0)
                        {
                            tables[0][1]++;
                        }
                        else
                        {
                            tables[0][2]++;
                        }
                        break;
                    case 0:
                        if (actual_Forecasts[1][i] == 1)
                        {
                            tables[1][0]++;
                        }
                        else if (actual_Forecasts[1][i] == 0)
                        {
                            tables[1][1]++;
                        }
                        else
                        {
                            tables[1][2]++;
                        }
                        break;
                    case -1:
                        if (actual_Forecasts[1][i] == 1)
                        {
                            tables[2][0]++;
                        }
                        else if (actual_Forecasts[1][i] == 0)
                        {
                            tables[2][1]++;
                        }
                        else
                        {
                            tables[2][2]++;
                        }
                        break;
                }
            }
            string[] strLables = { "Uptrend", "Notrend", "Downtrend" };
            for (int i = 0; i < tables.Length; i++)
            {
                strTemp = "";
                strTemp += strLables[i] + "\t";
                for (int j = 0; j < tables.Length; j++)
                {
                    strTemp += tables[i][j].ToString() + "\t";
                }
                writer.WriteLine(strTemp);
            }
            writer.Close();
        }

        private void StatisticTrend2File(string[] predictedFiles, string statisticFile)
        {
            string strTemp = "";
            double[][] actual_Forecasts = new double[2][];

            // Đọc từ file predict lên
            for (int i = 0; i < predictedFiles.Length; i++)
            {
                StreamReader reader = new StreamReader(predictedFiles[i]);
                reader.ReadLine();
                strTemp += reader.ReadToEnd();
                reader.Close();
            }

            StreamWriter writer = new StreamWriter(statisticFile);
            string[] strActual_Forecasts = Regex.Split(strTemp, "\n");
            int iLen = 0;
            for (int i = 0; i < strActual_Forecasts.Length; i++)
            {
                if (strActual_Forecasts[i] != "")
                {
                    iLen++;
                }
            }
            actual_Forecasts[0] = new double[iLen];
            actual_Forecasts[1] = new double[iLen];

            for (int i = 0; i < strActual_Forecasts.Length; i++)
            {
                if (strActual_Forecasts[i] != "")
                {
                    actual_Forecasts[0][i] = double.Parse(Regex.Split(strActual_Forecasts[i], " ")[0]);
                    actual_Forecasts[1][i] = double.Parse(Regex.Split(strActual_Forecasts[i], " ")[1]);
                }
            }

            //thông kê kết quả dự đoán so với thực tế
            strTemp = "Predicted Trend\t Actual Trend\n\tUptrend\tNotrend\tDowntrend";
            writer.WriteLine(strTemp);

            int[][] tables;
            tables = new int[3][];
            for (int i = 0; i < tables.Length; i++)
            {
                tables[i] = new int[3];
            }
            for (int i = 0; i < actual_Forecasts[0].Length; i++)
            {
                int iTemp = (int)actual_Forecasts[0][i];
                switch (iTemp)
                {
                    case 1:
                        if (actual_Forecasts[1][i] == 1)
                        {
                            tables[0][0]++;
                        }
                        else if (actual_Forecasts[1][i] == 0)
                        {
                            tables[0][1]++;
                        }
                        else
                        {
                            tables[0][2]++;
                        }
                        break;
                    case 0:
                        if (actual_Forecasts[1][i] == 1)
                        {
                            tables[1][0]++;
                        }
                        else if (actual_Forecasts[1][i] == 0)
                        {
                            tables[1][1]++;
                        }
                        else
                        {
                            tables[1][2]++;
                        }
                        break;
                    case -1:
                        if (actual_Forecasts[1][i] == 1)
                        {
                            tables[2][0]++;
                        }
                        else if (actual_Forecasts[1][i] == 0)
                        {
                            tables[2][1]++;
                        }
                        else
                        {
                            tables[2][2]++;
                        }
                        break;
                }
            }
            string[] strLables = { "Uptrend", "Notrend", "Downtrend" };
            for (int i = 0; i < tables.Length; i++)
            {
                strTemp = "";
                strTemp += strLables[i] + "\t";
                for (int j = 0; j < tables.Length; j++)
                {
                    strTemp += tables[i][j].ToString() + "\t";
                }
                writer.WriteLine(strTemp);
            }
            writer.Close();
        }

        private void rdKSVMeans_CheckedChanged(object sender, EventArgs e)
        {
            lblNumCluster.Enabled = rdKSVMeans.Checked;
            nmNumCluster.Enabled = rdKSVMeans.Checked;
        }

        private void rdSOMSVM_CheckedChanged(object sender, EventArgs e)
        {
            lblNumCluster.Enabled = rdSOMSVM.Checked;
            nmNumCluster.Enabled = rdSOMSVM.Checked;
        }

        private void EnableStepByStepTrainAndTest(bool isEnable)
        {
            btnPreprocess.Enabled = isEnable;
            btnTrain.Enabled = isEnable;
            btnTest.Enabled = isEnable;

            btnBrowser.Enabled = isEnable; ;
            btnTestBrowser.Enabled = isEnable;
            btnTrainBrowser.Enabled = isEnable;
            btnModelBrowser.Enabled = isEnable;

            tbxCsvFilePath.Enabled = isEnable;
            tbxTrainFilePath.Enabled = isEnable;
            tbxTestFilePath.Enabled = isEnable;
            tbxModelFilePath.Enabled = isEnable;

            lblInputFile.Enabled = isEnable;
            lblTestFile.Enabled = isEnable;
            lblModelFile.Enabled = isEnable;
            lblTrainingFile.Enabled = isEnable;
        }

        private void cmbExperimentMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbExperimentMode.SelectedItem.ToString() == "Batch")
            {
                gbBatchTrainTest.Enabled = true;
                EnableStepByStepTrainAndTest(false);
            }
            else
            {
                gbBatchTrainTest.Enabled = false;
                EnableStepByStepTrainAndTest(true);
            }
        }
    }
}
