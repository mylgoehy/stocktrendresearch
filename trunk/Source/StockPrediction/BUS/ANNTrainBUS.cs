using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using DAO;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;

namespace BUS
{
    public class ANNTrainBUS
    {


        #region Attributes
        private double[][] _arrActuals;
        private double[][] _arrPredicts;

        private double[] _arrTempActuals;
        private double[] _arrTempPredicts;
        private double[] _arrDesiredValue;

        private ANNModelBUS _annModel;

        private int _numPattern;
        private double[][] _arrPattern;

        private double _error;

        public const int MSE = 1;
        public const int NMSE = 2;
        public const int RMSE = 3;
        public const int SIGN = 4;

        public const int UPTREND = 1;
        public const int NOTREND = 0;
        public const int DOWNTREND = -1;

        #endregion

        #region Constructors
        public ANNTrainBUS()
        {
        }
        #endregion

        #region Properties
        public double[][] ArrActuals
        {
            get { return _arrActuals; }
            set { _arrActuals = value; }
        }

        public double[][] ArrPredicts
        {
            get { return _arrPredicts; }
            set { _arrPredicts = value; }
        }

        public double[] ArrTempActuals
        {
            get { return _arrTempActuals; }
            set { _arrTempActuals = value; }
        }

        public double[] ArrTempPredicts
        {
            get { return _arrTempPredicts; }
            set { _arrTempPredicts = value; }
        }

        public double[] ArrDesiredValue
        {
            get { return _arrDesiredValue; }
            set { _arrDesiredValue = value; }
        }

        public ANNModelBUS ANNModel
        {
            get { return _annModel; }
            set { _annModel = value; }
        }

        public int NumPattern
        {
            get { return _numPattern; }
            set { _numPattern = value; }
        }

        public double[][] ArrPattern
        {
            get { return _arrPattern; }
            set { _arrPattern = value; }
        }

        public double Error
        {
            get { return _error; }
            set { _error = value; }
        }
        #endregion

        #region Methods
        public void LoadDataSet(string strDataFile)
        {
            StreamReader reader = null;

            try
            {
                reader = new StreamReader(strDataFile);
                string strTemps = reader.ReadToEnd();
                reader.Close();

                string[] strLines = Regex.Split(strTemps, "\r\n");

                NumPattern = strLines.Length - 1;
                ArrActuals = new double[NumPattern][];
                ArrPredicts = new double[NumPattern][];
                ArrPattern = new double[NumPattern][];

                for (int i = 0; i < NumPattern; i++)
                {
                    string[] strValue = strLines[i].Split(' ');

                    int iTempActual = int.Parse(strValue[0]);
                    ArrActuals[i] = new double[3];
                    switch (iTempActual)
                    {
                        case DOWNTREND:
                            ArrActuals[i][0] = 1;
                            ArrActuals[i][1] = 0;
                            ArrActuals[i][2] = 0;
                            break;
                        case NOTREND:
                            ArrActuals[i][0] = 0;
                            ArrActuals[i][1] = 1;
                            ArrActuals[i][2] = 0;
                            break;
                        case UPTREND:
                            ArrActuals[i][0] = 0;
                            ArrActuals[i][1] = 0;
                            ArrActuals[i][2] = 1;
                            break;
                    }

                    ArrPattern[i] = new double[ANNParameterBUS.InputNode];
                    for (int j = 0; j < ANNParameterBUS.InputNode; j++)
                    {
                        ArrPattern[i][j] = double.Parse(strValue[j + 1].Split(':')[1]);
                    }
                }

                ANNModel = new ANNModelBUS();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //public void LoadDataSet(double[][] trainingSet)
        //{
        //    NumPattern = trainingSet.Length;
        //    ArrActual = new double[NumPattern][];
        //    ArrPredict = new double[NumPattern][];
        //    ArrPattern = new double[NumPattern][];

        //    for (int i = 0; i < NumPattern; i++)
        //    {
        //        ArrActual[i] = trainingSet[i][ANNParameterBUS.InputNode];

        //        ArrPattern[i] = new double[ANNParameterBUS.InputNode];
        //        for (int j = 0; j < ANNParameterBUS.InputNode; j++)
        //        {
        //            ArrPattern[i][j] = trainingSet[i][j];
        //        }
        //    }

        //    ANNModel = new ANNModelBUS();
        //}

        //public void Main()
        //{
        //    //Khởi tạo bộ trọng số ngẫu nhiên cho mô hình
        //    ANNModel.InitialWeight();
        //    TSCFDCostFunction();

        //    int loop = 1;
        //    bool bContinue = true;

        //    MeasureBUS measureBUS = new MeasureBUS();

        //    while (bContinue)
        //    {
        //        for (int i = 0; i < NumPattern; i++)
        //        {
        //            ANNModel.OutInputLayer = ArrPattern[i];
        //            ArrPredict[i] = ANNModel.FeedforwardTraining();
        //        }

        //        Error = measureBUS.NMSE(ArrActual, ArrPredict);

        //        if (Error <= 0.01 || loop >= ANNParameterBUS.MaxEpoch)
        //        {
        //            bContinue = false;
        //            ANNModel.SaveModelFile();
        //            //ghi nhận độ lỗi và số vòng lặp của quá trình train
        //            ANNModel.SaveError_MaxLoop(Error, loop);
        //        }
        //        else
        //        {
        //            for (int i = 0; i < NumPattern; i++)
        //            {
        //                ANNModel.OutInputLayer = ArrPattern[i];
        //                ANNModel.FeedforwardTraining();
        //                ANNModel.ErrorBackpropagationTraining(ArrDesiredValue[i]);
        //            }
        //        }

        //        loop++;
        //    }
        //}

        public void Main(int measureType)
        {
            //Khởi tạo bộ trọng số ngẫu nhiên cho mô hình
            ANNModel.InitialWeight();
            //TSCFDCostFunction();

            int loop = 1;
            bool bContinue = true;

            MeasureBUS measureBUS = new MeasureBUS();

            while (bContinue)
            {
                for (int i = 0; i < NumPattern; i++)
                {
                    ANNModel.OutInputLayer = ArrPattern[i];
                    ArrPredicts[i] = ANNModel.FeedforwardTraining();
                }

                switch (measureType)
                {
                    case MSE:
                        Convert2CaculateErrorMeasure();
                        Error = measureBUS.MSE(ArrTempActuals, ArrTempPredicts);
                        break;
                    case NMSE:
                        Convert2CaculateErrorMeasure();
                        Error = measureBUS.NMSE(ArrTempActuals, ArrTempPredicts);
                        break;
                    case RMSE:
                        Convert2CaculateErrorMeasure();
                        Error = measureBUS.RMSE(ArrTempActuals, ArrTempPredicts);
                        break;
                    case SIGN:
                        Convert2CaculateErrorMeasure();
                        Error = measureBUS.Sign(ArrTempActuals, ArrTempPredicts);
                        break;
                }

                bool bResult = CompareError_Accurancy(Error, ANNParameterBUS.Accuracy, measureType);
                if (bResult == true || loop >= ANNParameterBUS.MaxEpoch)
                {
                    bContinue = false;
                    ANNModel.SaveModelFile();
                    //ghi nhận độ lỗi và số vòng lặp của quá trình train
                    ANNModel.SaveError_MaxLoop(Error, loop);
                }
                else
                {
                    for (int i = 0; i < NumPattern; i++)
                    {
                        ANNModel.OutInputLayer = ArrPattern[i];
                        ANNModel.FeedforwardTraining();
                        ANNModel.ErrorBackpropagationTraining(ArrActuals[i]);
                    }
                }

                loop++;
            }
        }

        //public void TSCFDCostFunction()
        //{
        //    ArrDesiredValue = ArrActual;

        //    if (ArrActual.Length > 2)
        //    {
        //        double prev = 0;
        //        double cur = ArrActual[0];
        //        double next = ArrActual[1];
        //        for (int i = 1; i < ANNParameterBUS.TrainingSize - 1; i++)
        //        {
        //            prev = cur;
        //            cur = next;
        //            next = ArrActual[i + 1];

        //            if ((cur - prev) * (next - cur) > 0)
        //            {
        //                //ArrDesiredValue[i] = next;
        //            }
        //        }              
        //    }
        //}

        public bool CompareError_Accurancy(double error, double accuracy, int measureType)
        {
            bool bResult = false;
            switch (measureType)
            {
                case MSE:
                    error = error * 100;
                    if (error <= 100 - accuracy)
                    {
                        bResult = true;
                    }
                    else
                    {
                        bResult = false;
                    }
                    break;
                case NMSE:
                    error = error * 100;
                    if (error <= 100 - accuracy)
                    {
                        bResult = true;
                    }
                    else
                    {
                        bResult = false;
                    }
                    break;
                case RMSE:

                    break;
                case SIGN:
                    if (error >= accuracy)
                    {
                        bResult = true;
                    }
                    else
                    {
                        bResult = false;
                    }
                    break;
            }
            return bResult;
        }

        public void Convert2CaculateErrorMeasure()
        {
            ArrTempActuals = new double[ArrActuals.Length * 3];
            ArrTempPredicts = new double[ArrPredicts.Length * 3];

            for (int i = 0; i < ArrActuals.Length; i++)
            {
                ArrTempActuals[i * 3 + 0] = ArrActuals[i][0];
                ArrTempActuals[i * 3 + 1] = ArrActuals[i][1];
                ArrTempActuals[i * 3 + 2] = ArrActuals[i][2];

                ArrTempPredicts[i * 3 + 0] = ArrPredicts[i][0];
                ArrTempPredicts[i * 3 + 1] = ArrPredicts[i][1];
                ArrTempPredicts[i * 3 + 2] = ArrPredicts[i][2];
            }
        }
        #endregion
    }
}
