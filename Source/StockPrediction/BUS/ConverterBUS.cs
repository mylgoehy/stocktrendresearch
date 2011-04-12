using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using DAO;
using System.Collections;
using System.IO;

namespace BUS
{
    public class ConverterBUS
    {

        #region Attributes
        static int NUM_DISTINC_VAL = 10;
		static int FAST_PERIOD = 25;
        static int LOW_PERIOD = 65;
        /// <summary>
        /// Mảng các cận trên và dưới của từng thuộc tính
        /// </summary>
        static private double[][] _attBounds;
	    #endregion

        #region Methods
        public void Convert(int numNode, double[] sourceArr, string destinationFile, out int numLines)//n là số phần tử của mảng sourceArr
        {
            try
            {
                TextWriter writer = new StreamWriter(destinationFile);
                numLines = 0;
                for (int i = 0; i < sourceArr.Length - numNode; i++)
                {
                    numLines++;
                    string strLine = sourceArr[i + numNode].ToString() + " ";
                    for (int j = 0; j < numNode; j++)
                    {
                        strLine += (j + 1).ToString() + ":" + sourceArr[i + j].ToString() + " ";
                    }
                    writer.WriteLine(strLine);
                }

                writer.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConvertWImprovedDirection(int numNode, double[] sourceArr, string destinationFile, double trainPercent, out int numLines)//n là số phần tử của mảng sourceArr
        {
            try
            {
                TextWriter writer = new StreamWriter(destinationFile);
                numLines = 0;
                int iDivideLine = (int)(trainPercent * (sourceArr.Length - numNode)/100);
                int i;
                string strLine;
                //Bộ train
                for (i = 0; i < iDivideLine - 1; i++)
                {
                    numLines++;
                    if ((sourceArr[i + numNode + 1] - sourceArr[i + numNode]) * (sourceArr[i + numNode] - sourceArr[i + numNode - 1]) > 0)
                    {
                        strLine = sourceArr[i + numNode + 1].ToString() + " ";
                    }
                    else
                    {
                        strLine = sourceArr[i + numNode].ToString() + " ";
                    }
                    for (int j = 0; j < numNode; j++)
                    {
                        strLine += (j + 1).ToString() + ":" + sourceArr[i + j].ToString() + " ";
                    }
                    writer.WriteLine(strLine);
                }
                numLines++;
                strLine = sourceArr[i + numNode].ToString() + " ";
                for (int j = 0; j < numNode; j++)
                {
                    strLine += (j + 1).ToString() + ":" + sourceArr[i + j].ToString() + " ";
                }
                writer.WriteLine(strLine);
                i++;
                //Bộ test
                for (; i < sourceArr.Length - numNode; i++)
                {
                    numLines++;
                    //double dblTarget = sourceArr[i + numNode] - sourceArr[i + numNode - 1];
                    //string strLine = dblTarget.ToString() + " ";
                    strLine = sourceArr[i + numNode].ToString() + " ";
                    for (int j = 0; j < numNode; j++)
                    {
                        strLine += (j + 1).ToString() + ":" + sourceArr[i + j].ToString() + " ";
                    }
                    writer.WriteLine(strLine);
                }
                writer.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConvertForTrend(int numDaysPredicted, int numNode, double[] sourceArr, string destinationFile, out int numLines, int type, bool app)//n là số phần tử của mảng sourceArr
        {
            try
            {
                numLines = 0;
                if (numDaysPredicted == 1)
                {
                    TextWriter writer = new StreamWriter(destinationFile);
                    if (app)
                    {
                        writer.WriteLine("Return[-1;1] 0 0");
                    }
                    for (int i = 0; i < sourceArr.Length - numNode; i++)
                    {
                        numLines++;
                        string strLine = sourceArr[i + numNode].ToString() + " ";
                        //double dblTemp = 0;
                        for (int j = 0; j < numNode; j++)
                        {
                            //dblTemp += sourceArr[i + j];
                            strLine += (j + 1).ToString() + ":" + sourceArr[i + j].ToString() + " ";
                        }
                        writer.WriteLine(strLine);
                    }

                    writer.Close();
                }
                else
                {
                    TextWriter writer = new StreamWriter(destinationFile);
                    if (app)
                    {
                        writer.WriteLine("Return[-1;1] 0 0");
                    }
                    switch (type)
                    {
                        case 1:
                            int iBound = numDaysPredicted > numNode ? 2 * numDaysPredicted - 1 : numDaysPredicted + numNode - 1;
                            for (int i = 0; i < sourceArr.Length - iBound; i++)
                            {
                                double dblTar = 0;
                                for (int k = numDaysPredicted - 1; k >= 0; k--)
                                {
                                    dblTar += sourceArr[i - k + iBound];
                                }

                                numLines++;
                                string strLine = dblTar.ToString() + " ";
                                for (int j = 0; j < numNode; j++)
                                {
                                    double dblTemp = 0;
                                    if (numDaysPredicted / numNode <= 1)
                                    {
                                        dblTemp = sourceArr[i + j];
                                    }
                                    else
                                    {
                                        int iRange = numDaysPredicted / numNode;
                                        for (int l = 0; l < iRange; l++)
                                        {
                                            dblTemp += sourceArr[i + j * iRange + l];
                                        }
                                    }
                                    strLine += (j + 1).ToString() + ":" + dblTemp.ToString() + " ";
                                }
                                writer.WriteLine(strLine);
                            }
                            break;
                        case 2:
                            for (int i = 0; i < sourceArr.Length - numNode * numDaysPredicted - numDaysPredicted; i++)
                            {
                                double dblTar = 0;
                                for (int k = 0; k < numDaysPredicted; k++)
                                {
                                    dblTar += sourceArr[i + numNode * numDaysPredicted + k];
                                }

                                numLines++;
                                string strLine = dblTar.ToString() + " ";
                                for (int j = 0; j < numNode; j++)
                                {
                                    double dblTemp = 0;
                                    for (int l = 0; l < numDaysPredicted; l++)
                                    {
                                        dblTemp += sourceArr[i + j * numDaysPredicted + l];
                                    }
                                    strLine += (j + 1).ToString() + ":" + dblTemp.ToString() + " ";
                                }
                                writer.WriteLine(strLine);
                            }
                            break;
                        case 3:
                            for (int i = 0; i < sourceArr.Length - numNode * numDaysPredicted - numDaysPredicted; i++)
                            {
                                double dblTar = 0;
                                for (int k = 0; k < numDaysPredicted; k++)
                                {
                                    dblTar += sourceArr[i + numNode * numDaysPredicted + k];
                                }

                                numLines++;
                                string strLine = dblTar.ToString() + " ";
                                for (int j = 0; j < numNode; j++)
                                {
                                    double dblTemp = 0;
                                    for (int l = 0; l < numDaysPredicted; l++)
                                    {
                                        dblTemp += sourceArr[i + j * numDaysPredicted + l];
                                    }
                                    strLine += (j + 1).ToString() + ":" + dblTemp.ToString() + " ";
                                }

                                for (int j = 0; j < numNode; j++)
                                {
                                    double dblTemp = 0;
                                    if (numDaysPredicted / numNode <= 1)
                                    {
                                        dblTemp = sourceArr[i + j];
                                    }
                                    else
                                    {
                                        int iRange = numDaysPredicted / numNode;
                                        for (int l = 0; l < iRange; l++)
                                        {
                                            dblTemp += sourceArr[i + j * iRange + l];
                                        }
                                    }
                                    strLine += (j + 1 + numNode).ToString() + ":" + dblTemp.ToString() + " ";
                                }
                                writer.WriteLine(strLine);
                            }
                            break;
                    }
                            
                    writer.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void Convert(double[] closePrices, double[] volumes, int numDaysPeriod, string destFileName, out int numLines)
        {
            try
            {
                double[] dblFastSMAs = IndicatorsBUS.CalculateSMA(closePrices, FAST_PERIOD);
                double[] dblLowSMAs = IndicatorsBUS.CalculateSMA(closePrices, LOW_PERIOD);
                // Chỉ có thể đánh nhãn khi giá trị tại đó xác định được SMA low
                int[] dblLabels = new int[closePrices.Length - LOW_PERIOD];

                for (int i = 0; i < dblLabels.Length; i++)
                {
                    dblLabels[i] = IndicatorsBUS.DetermineTrend(closePrices, dblFastSMAs, dblLowSMAs, i + LOW_PERIOD, 5, 1);
                }
                // Tính chỉ số aroon với period bằng 2 lần số ngày cần dự đoán, nếu dự đoán 1 ngày thì period = 5
                int iAroonPeriod = (numDaysPeriod < 5) ? 5 : numDaysPeriod * 2;
                double[] dblAroonUps = IndicatorsBUS.CalculateAroon(closePrices, iAroonPeriod, true);
                double[] dblAroonDowns = IndicatorsBUS.CalculateAroon(closePrices, iAroonPeriod, false);

                double[] dblMACD = IndicatorsBUS.CalculateMACDHist(closePrices, 12, 26, 1);
                double[] dblMACDHist = IndicatorsBUS.CalculateMACDHist(closePrices, 12, 26, 9);
                double[] dblBollingerUp = IndicatorsBUS.CalculateBollingerband(closePrices, 20, 2, true);
                double[] dblBollingerMid = IndicatorsBUS.CalculateSMA(closePrices, 20);
                double[] dblBollingerLow = IndicatorsBUS.CalculateBollingerband(closePrices, 20, 2, false);
                double[] dblRSI = IndicatorsBUS.CalculateRSI(closePrices, iAroonPeriod);
                // Scale các chỉ số (ngoại trừ Aroon) về -1 1
                double dblMax = 0;
                double dblMaxVol = 0;
                // Tìm trị tuyệt đối lớn nhất. Nhận xét, ta chỉ cần tìm trên closePrices và BollingerUp là đủ
                for (int i = 0; i < closePrices.Length; i++)
                {
                    if (Math.Abs(dblBollingerUp[i]) > dblMax)
                    {
                        dblMax = Math.Abs(dblBollingerUp[i]);
                    }
                    if (Math.Abs(closePrices[i]) > dblMax)
                    {
                        dblMax = Math.Abs(closePrices[i]);
                    }
                    if (volumes[i] > dblMaxVol)
                    {
                        dblMaxVol = volumes[i];
                    }
                }
                _attBounds = new double[10][]; // 10 là số thuộc tính - số chiều, 2 là cận trên và dưới
                for (int i = 0; i < _attBounds.Length; i++)
                {
                    _attBounds[i] = new double[2];
                    _attBounds[i][0] = -1;  // khởi gán cận trên
                    _attBounds[i][1] = 1;   // khởi gán cận dưới
                }
                _attBounds[8][0] = _attBounds[9][0] = 1;    // cận trên cho Aroonup và AroonDown
                _attBounds[8][1] = _attBounds[9][1] = 0;    // cận dưới cho AroonUp và AroonDown
                // Scale và tìm cận trên và dưới
                for (int i = 0; i < closePrices.Length; i++)
                {
                    //volumes[i] = volumes[i] / dblMaxVol;
                    closePrices[i] = closePrices[i] / dblMax;
                    dblFastSMAs[i] = dblFastSMAs[i] / dblMax;
                    dblLowSMAs[i] = dblLowSMAs[i] / dblMax;
                    dblMACD[i] = dblMACD[i] / dblMax;
                    dblMACDHist[i] = dblMACDHist[i] / dblMax;
                    dblBollingerUp[i] = dblBollingerUp[i] / dblMax;
                    dblBollingerMid[i] = dblBollingerMid[i] / dblMax;
                    dblBollingerLow[i] = dblBollingerLow[i] / dblMax;

                    if(_attBounds[0][0] < closePrices[i])
                    {
                        _attBounds[0][0] = closePrices[i];
                    }
                    if(_attBounds[0][1] > closePrices[i])
                    {
                        _attBounds[0][1] = closePrices[i];
                    }

                    if (_attBounds[1][0] < dblFastSMAs[i])
                    {
                        _attBounds[1][0] = dblFastSMAs[i];
                    }
                    if (_attBounds[1][1] > dblFastSMAs[i])
                    {
                        _attBounds[1][1] = dblFastSMAs[i];
                    }

                    if (_attBounds[2][0] < dblLowSMAs[i])
                    {
                        _attBounds[2][0] = dblLowSMAs[i];
                    }
                    if (_attBounds[2][1] > dblLowSMAs[i])
                    {
                        _attBounds[2][1] = dblLowSMAs[i];
                    }

                    if (_attBounds[3][0] < dblMACD[i])
                    {
                        _attBounds[3][0] = dblMACD[i];
                    }
                    if (_attBounds[3][1] > dblMACD[i])
                    {
                        _attBounds[3][1] = dblMACD[i];
                    }

                    if (_attBounds[4][0] < dblMACDHist[i])
                    {
                        _attBounds[4][0] = dblMACDHist[i];
                    }
                    if (_attBounds[4][1] > dblMACDHist[i])
                    {
                        _attBounds[4][1] = dblMACDHist[i];
                    }

                    if (_attBounds[5][0] < dblBollingerUp[i])
                    {
                        _attBounds[5][0] = dblBollingerUp[i];
                    }
                    if (_attBounds[5][1] > dblBollingerUp[i])
                    {
                        _attBounds[5][1] = dblBollingerUp[i];
                    }

                    if (_attBounds[6][0] < dblBollingerMid[i])
                    {
                        _attBounds[6][0] = dblBollingerMid[i];
                    }
                    if (_attBounds[6][1] > dblBollingerMid[i])
                    {
                        _attBounds[6][1] = dblBollingerMid[i];
                    }

                    if (_attBounds[7][0] < dblBollingerLow[i])
                    {
                        _attBounds[7][0] = dblBollingerLow[i];
                    }
                    if (_attBounds[7][1] > dblBollingerLow[i])
                    {
                        _attBounds[7][1] = dblBollingerLow[i];
                    }

                }

                WriteMetaForDT(destFileName + ".meta");
                TextWriter commonWriter = new StreamWriter(destFileName);
                TextWriter dataDTWriter = new StreamWriter(destFileName + ".data");
                numLines = 0;
                for (int i = numDaysPeriod - 1; i < dblLabels.Length; i++)
                {
                    numLines++;
                    // phần ghi dữ liệu thông thường: dùng làm đầu vào cho ANN, SVM
                    string strLine = dblLabels[i].ToString() + " ";
                    int j = 1;
                    int iPastIndex = LOW_PERIOD + i - numDaysPeriod;
                    //strLine += (j++).ToString() + ":" + volumes[iPastIndex].ToString() + " ";
                    strLine += (j++).ToString() + ":" + closePrices[iPastIndex].ToString() + " ";
                    strLine += (j++).ToString() + ":" + dblFastSMAs[iPastIndex].ToString() + " ";
                    strLine += (j++).ToString() + ":" + dblLowSMAs[iPastIndex].ToString() + " ";
                    strLine += (j++).ToString() + ":" + dblMACD[iPastIndex].ToString() + " ";
                    strLine += (j++).ToString() + ":" + dblMACDHist[iPastIndex].ToString() + " ";
                    strLine += (j++).ToString() + ":" + dblBollingerUp[iPastIndex].ToString() + " ";
                    strLine += (j++).ToString() + ":" + dblBollingerMid[iPastIndex].ToString() + " ";
                    strLine += (j++).ToString() + ":" + dblBollingerLow[iPastIndex].ToString() + " ";
                    strLine += (j++).ToString() + ":" + dblAroonUps[iPastIndex].ToString() + " ";
                    strLine += (j++).ToString() + ":" + dblAroonDowns[iPastIndex].ToString() + " ";
                    //strLine += (j++).ToString() + ":" + dblRSI[iPastIndex].ToString();
                    commonWriter.WriteLine(strLine);
                    // phần ghi dữ liệu cho decision tree
                    strLine = dblLabels[i].ToString() + ", ";
                    strLine += DetermineDistincValue(closePrices[iPastIndex], _attBounds[0][0], _attBounds[0][1]) + ", ";
                    strLine += DetermineDistincValue(dblFastSMAs[iPastIndex], _attBounds[1][0], _attBounds[1][1]) + ", ";
                    strLine += DetermineDistincValue(dblLowSMAs[iPastIndex], _attBounds[2][0], _attBounds[2][1]) + ", ";
                    strLine += DetermineDistincValue(dblMACD[iPastIndex], _attBounds[3][0], _attBounds[3][1]) + ", ";
                    strLine += DetermineDistincValue(dblMACDHist[iPastIndex], _attBounds[4][0], _attBounds[4][1]) + ", ";
                    strLine += DetermineDistincValue(dblBollingerUp[iPastIndex], _attBounds[5][0], _attBounds[5][1]) + ", ";
                    strLine += DetermineDistincValue(dblBollingerMid[iPastIndex], _attBounds[6][0], _attBounds[6][1]) + ", ";
                    strLine += DetermineDistincValue(dblBollingerLow[iPastIndex], _attBounds[7][0], _attBounds[7][1]) + ", ";
                    strLine += DetermineDistincValue(dblAroonUps[iPastIndex], _attBounds[8][0], _attBounds[8][1]) + ", ";
                    strLine += DetermineDistincValue(dblAroonDowns[iPastIndex], _attBounds[9][0], _attBounds[9][1]) + ", ";
                    dataDTWriter.WriteLine(strLine);
                }
                commonWriter.Close();
                dataDTWriter.Close();

                

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static int Convert2Trend(double[] outputValues)        
        {
            if (outputValues[0] > outputValues[1] && outputValues[0] > outputValues[2])
            {
                return  -1;// DOWNTREND
            }
            else if ((outputValues[1] > outputValues[2] && outputValues[1] > outputValues[0]))
            {
                return 0;// NOTREND
            }
            return 1;
        }

        private static int DetermineDistincValue(double val, double max, double min)
        {
            double dblRange = (max - min) / NUM_DISTINC_VAL;
            double dblLowerBound = min;
            double dblUpperBound = min + dblRange;
            for (int i = 0; i < NUM_DISTINC_VAL; i++)
            {
                if (dblLowerBound <= val && val < dblUpperBound)
                {
                    return i;
                }
                dblLowerBound = dblUpperBound;
                dblUpperBound += dblRange;
            }
            return NUM_DISTINC_VAL - 1;
        }

        private static void WriteMetaForDT(string metaFile)
        {
            TextWriter metaWriter = new StreamWriter(metaFile);
            string[] strFeatureNames = { "ClosePrice", "FastSMA", "LowSMA", "MACD", "MACDHist", "BollingerUp", "BollingerMid", "BollingerLow", "AroonUp", "AroonDown" };
            metaWriter.WriteLine("CONCLUSION =");
            metaWriter.WriteLine();
            metaWriter.WriteLine("\"OutCome\" = { '-1', '0', '1' }");
            metaWriter.WriteLine();
            metaWriter.WriteLine("FEATURES =");
            metaWriter.WriteLine();
            for (int j = 0; j < 10; j++)
            {
                metaWriter.Write("\"" + strFeatureNames[j] + "\" = { ");
                for (int i = 0; i < NUM_DISTINC_VAL; i++)
                {
                    
                    metaWriter.Write("'" + i + "'");
                    if (i != NUM_DISTINC_VAL - 1)
                    {
                        metaWriter.Write(", ");
                    }
                }
                if (j != 9)
                {
                    metaWriter.Write(" },\n");
                }
                else
                {
                    metaWriter.Write("}\n");
                }
            }
            //metaWriter.WriteLine("TRAINDATA = \"" + dataFile + "\"");
            metaWriter.Close();
        }
        #endregion

    }
}
