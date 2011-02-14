﻿using System;
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
		static int FAST_PERIOD = 25;
        static int LOW_PERIOD = 65; 
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

        public static void Convert(double[] closePrices, int numDaysPeriod, string destFileName, out int numLines)
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
                // Scale các chỉ số (ngoại trừ Aroon) về -1 1
                double dblMax = 0;
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
                }
                // Scale
                for (int i = 0; i < closePrices.Length; i++)
                {
                    closePrices[i] = closePrices[i] / dblMax;
                    dblFastSMAs[i] = dblFastSMAs[i] / dblMax;
                    dblLowSMAs[i] = dblLowSMAs[i] / dblMax;
                    dblMACD[i] = dblMACD[i] / dblMax;
                    dblMACDHist[i] = dblMACDHist[i] / dblMax;
                    dblBollingerUp[i] = dblBollingerUp[i] / dblMax;
                    dblBollingerMid[i] = dblBollingerMid[i] / dblMax;
                    dblBollingerLow[i] = dblBollingerLow[i] / dblMax;
                }

                TextWriter writer = new StreamWriter(destFileName);
                numLines = 0;
                for (int i = numDaysPeriod - 1; i < dblLabels.Length; i++)
                {
                    numLines++;
                    string strLine = dblLabels[i].ToString() + " ";
                    int j = 1;
                    int iPastIndex = LOW_PERIOD + i - numDaysPeriod;
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
                    
                    writer.WriteLine(strLine);
                }

                writer.Close();
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }
        #endregion

    }
}