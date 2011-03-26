using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BUS
{
    public class IndicatorsBUS
    {
        #region Methods

        /// <summary>
        /// Xác định xu hướng dựa trên giá đóng cửa và 2 đường sma
        /// </summary>
        /// <param name="closingPrices">Giá đóng cửa</param>
        /// <param name="shortSMAs">đường trung bình trượt ngắn 25</param>
        /// <param name="longSMAs">đường trung bình trượt dài 65</param>
        /// <param name="index">chỉ số ngày hiện tại</param>
        /// <param name="refDayInShortSMA">số ngày tham khảo cho shortSMAs 5</param>
        /// <param name="refDayInLongSMA">số ngày tham khảo cho longSMAs 1</param>
        /// <returns>-1 0 1</returns>
        public static int DetermineTrend(double[] closingPrices, double[] shortSMAs, double[] longSMAs, int index, int refDayInShortSMA, int refDayInLongSMA)
        {
            if (closingPrices[index] > shortSMAs[index])
            {
                if (shortSMAs[index] > longSMAs[index])
                {
                    if (shortSMAs[index] > shortSMAs[index - refDayInShortSMA])
                    {
                        if (longSMAs[index] > longSMAs[index - refDayInLongSMA])
                        {
                            return 1;
                        }
                    }
                }
            }
            else if (closingPrices[index] < shortSMAs[index])
            {
                if (shortSMAs[index] < longSMAs[index])
                {
                    if (shortSMAs[index] < shortSMAs[index - refDayInShortSMA])
                    {
                        if (longSMAs[index] < longSMAs[index - refDayInLongSMA])
                        {
                            return -1;
                        }
                    }
                }
            }

            return 0;

        }

        /// <summary>
        /// Tính chỉ số SMA
        /// </summary>
        /// <param name="closePrices">Giá đóng cửa</param>
        /// <param name="numDaysPeriod">chu kỳ</param>
        /// <returns>mảng chứa chỉ số SMA tương ứng</returns>
        public static double[] CalculateSMA(double[] closePrices, int numDaysPeriod)
        {
            double[] result = new double[closePrices.Length];

            int iTemp = numDaysPeriod < closePrices.Length ? numDaysPeriod : closePrices.Length;
            for (int i = 0; i < iTemp; i++)
            {
                double sum = 0;
                for (int j = 0; j <= i; j++)
                {
                    sum += closePrices[j];
                }
                result[i] = sum / (i + 1);
            }

            for (int i = numDaysPeriod - 1; i < closePrices.Length; i++)
            {
                double sum = 0;
                for (int j = i - numDaysPeriod + 1; j <= i; j++)
                {
                    sum += closePrices[j];
                }
                result[i] = sum / numDaysPeriod;
            }
            return result;
        }

        /// <summary>
        /// Tính chỉ số EMA
        /// </summary>
        /// <param name="closePrices">Giá đóng cửa</param>
        /// <param name="numDaysPeriod">chu kỳ</param>
        /// <returns>mảng chứa chỉ số EMA tương ứng</returns>
        public static double[] CalculateEMA(double[] closePrices, int numDaysPeriod)
        {
            double[] result = new double[closePrices.Length];
            double alpha = 2 * 1.0d / (numDaysPeriod + 1);

            int iTemp = numDaysPeriod < closePrices.Length ? numDaysPeriod : closePrices.Length;
            for (int i = 0; i < iTemp; i++)
            {
                int iBaseIndex = 0;
                result[i] = EMA(closePrices, i, alpha, iBaseIndex);
            }

            for (int i = numDaysPeriod - 1; i < closePrices.Length; i++)
            {
                int iBaseIndex = i - numDaysPeriod + 1;
                result[i] = EMA(closePrices, i, alpha, iBaseIndex);
            }

            return result;
        }

        /// <summary>
        /// Tính chỉ số MACD|MACD Histogram
        /// </summary>
        /// <param name="closePrices">Giá đóng cửa</param>
        /// <param name="fastPeriod">chu kỳ đường ema nhanh</param>
        /// <param name="lowPeriod"> chu kỳ đường ema chậm</param>
        /// <param name="signalPeriod">chu kỳ đường macd signal - hiệu 2 đường ema. Nếu bằng 1: kết quả là đường MACD</param>
        /// <returns>mảng chứa chỉ số MACD|MACD Histogram tương ứng</returns>
        public static double[] CalculateMACDHist(double[] closePrices, int fastPeriod, int lowPeriod, int signalPeriod)
        {
            double[] fastEMAs = CalculateEMA(closePrices, fastPeriod);
            double[] lowEMAs = CalculateEMA(closePrices, lowPeriod);
            double[] MACDHist = new double[closePrices.Length];
            for (int i = 0; i < closePrices.Length; i++)
            {
                MACDHist[i] = fastEMAs[i] - lowEMAs[i];
            }

            if (signalPeriod > 1)
            {
                double[] signalLine = CalculateEMA(MACDHist, signalPeriod);
                for (int i = 0; i < MACDHist.Length; i++)
                {
                    MACDHist[i] = MACDHist[i] - signalLine[i];
                }
            }

            return MACDHist;
        }

        /// <summary>
        /// Tính chỉ số Aroon
        /// </summary>
        /// <param name="closingPrices">Giá đóng cửa</param>
        /// <param name="numDaysPeriod">Chu kỳ cho chỉ số Aroon</param>
        /// <param name="isUp">Xác định là AroonUp hay AroonDown</param>
        /// <returns>mảng chứa chỉ số Aroon tương ứng</returns>
        public static double[] CalculateAroon(double[] closingPrices, int numDaysPeriod, bool isUp)
        {
            double[] results = new double[closingPrices.Length];

            int iTemp = numDaysPeriod < closingPrices.Length ? numDaysPeriod : closingPrices.Length;
            for (int i = 0; i < iTemp; i++)
            {
                results[i] = (double)((i + 1) - FindNumDaysSinceOptimal(closingPrices, i, i + 1, isUp)) / (i + 1);
            }

            for (int i = numDaysPeriod - 1; i < closingPrices.Length; i++)
            {
                results[i] = (double)(numDaysPeriod - FindNumDaysSinceOptimal(closingPrices, i, numDaysPeriod, isUp)) / numDaysPeriod;
            }

            return results;
        }

        /// <summary>
        /// Tính đường bollingerBand upper hoặc lower
        /// </summary>
        /// <param name="closingPrices">Giá đóng cửa</param>
        /// <param name="numDaysPeriod">Chu kỳ cho chỉ số (thường 20)</param>
        /// <param name="isUpper">Xác định là Upper hay lower bollinger band</param>
        /// <param name="width">Độ rộng của band(thường 2)</param>
        /// <returns>mảng chứa chỉ số bollinger band tương ứng</returns>
        public static double[] CalculateBollingerband(double[] closePrices, int numDaysPeriod, int width, bool isUpper)
        {
            double[] bands = CalculateSMA(closePrices, numDaysPeriod);
            if (isUpper)
            {
                for (int i = 0; i < bands.Length; i++)
                {
                    bands[i] = bands[i] + width * FindStandardDeviation(closePrices, i, numDaysPeriod);
                }
            }
            else
            {
                for (int i = 0; i < bands.Length; i++)
                {
                    bands[i] = bands[i] - width * FindStandardDeviation(closePrices, i, numDaysPeriod);
                }
            }
            return bands;
        }
        public static double[] CalculateRSI(double[] closePrices, int numDaysPeriod)
        {
            double[] results = new double[closePrices.Length];
            double[] Upwards = new double[closePrices.Length];
            double[] Downwards = new double[closePrices.Length];
            
            // Tính Upward changes and Downward changes
            Upwards[0] = 0;
            Downwards[0] = 0;
            for (int i = 1; i < closePrices.Length; i++)
            {
                double dblTemp = closePrices[i] - closePrices[i - 1];
                if (dblTemp > 0.0d)
                {
                    Upwards[i] = dblTemp;
                    Downwards[i] = 0.0d;
                }
                else if(dblTemp < 0.0d)
                {
                    Downwards[i] = Math.Abs(dblTemp);
                    Upwards[i] = 0.0d;
                }
                else // Giá đóng cửa ngày hiện tại không đổi so với ngày trước
                {
                    Upwards[i] = 0.0d;
                    Downwards[i] = 0.0d;
                }
            }
                        
            // Tinh chi so RSI
            double[] EMAUpwards = CalculateEMA(Upwards, numDaysPeriod);
            double[] EMADownwards = CalculateEMA(Downwards, numDaysPeriod);

            for (int i = 0; i < results.Length; i++)
            {
            
                if (EMADownwards[i] == 0.0d)
                {
                    results[i] = 1;
                }
                else
                {
                    double dblTemp = EMAUpwards[i] / EMADownwards[i];
                    results[i] = 1 - (1 / (1 + dblTemp));   // Công thức có thay đổi bằng cách chia 100 cho quy về đoạn [0,1]
                }
            }
            return results;
        }

        /// <summary>
        /// Tìm số ngày tính từ ngày cao (thấp) nhất đến ngày end (dùng cho Aroon)
        /// </summary>
        private static int FindNumDaysSinceOptimal(double[] closingPrices, int end, int numDaysPeriod, bool isMax)
        {
            int iStart = end - numDaysPeriod + 1;
            double dblOptimal = closingPrices[iStart];
            int iOptIndex = iStart;
            for (int i = iStart + 1; i <= end; i++)
            {
                if (isMax)
                {
                    if (closingPrices[i] > dblOptimal)
                    {
                        dblOptimal = closingPrices[i];
                        iOptIndex = i;
                    }
                }
                else
                {
                    if (closingPrices[i] < dblOptimal)
                    {
                        dblOptimal = closingPrices[i];
                        iOptIndex = i;
                    }
                }
            }
            return end - iOptIndex;
        }

        /// <summary>
        /// Tính giá trị EMA từ baseIndex đến index
        /// </summary>
        private static double EMA(double[] closePrices, int index, double alpha, int baseIndex)
        {
            if (index == baseIndex)
            {
                return closePrices[baseIndex];
            }
            else
            {
                return alpha * closePrices[index] + (1 - alpha) * EMA(closePrices, index - 1, alpha, baseIndex);
            }
        }

        /// <summary>
        /// Tính giá trị độ lệch chuẩn của numDaysPeriod từ ngày end về trước
        /// </summary>
        private static double FindStandardDeviation(double[] closePrices, int end, int numDaysPeriod)
        {
            int iStart = end - numDaysPeriod + 1;
            if (iStart < 0)
            {
                iStart = 0;
            }
            int iPeriod = end - iStart + 1;
            double dblAverage = 0;
            double dblVar = 0;

            for(int i = iStart;i <= end;i++)
            {
                dblAverage += closePrices[i];
            }
            dblAverage = dblAverage / iPeriod;

            for (int i = iStart; i <= end; i++)
            {
                dblVar += Math.Pow(closePrices[i] - dblAverage, 2);
            }
            dblVar = dblVar / (iPeriod - 1);

            return Math.Pow(dblVar, 0.5);
        }

        #endregion
    }
}
