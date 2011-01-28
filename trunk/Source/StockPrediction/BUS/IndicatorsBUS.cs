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
        /// <param name="shortSMAs">đường trung bình trượt ngắn</param>
        /// <param name="longSMAs">đường trung bình trượt dài</param>
        /// <param name="index">chỉ số ngày hiện tại</param>
        /// <param name="refDayInShortSMA">số ngày tham khảo cho shortSMAs</param>
        /// <param name="refDayInLongSMA">số ngày tham khảo cho longSMAs</param>
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
        public double[] CalculateSMA(double[] closePrices, int numDaysPeriod)
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
        public double[] CalculateEMA(double[] closePrices, int numDaysPeriod)
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
        /// Tính chỉ số Aroon
        /// </summary>
        /// <param name="closingPrices">Giá đóng cửa</param>
        /// <param name="numDaysPeriod">Chu kỳ cho chỉ số Aroon</param>
        /// <param name="isUp">Xác định là AroonUp hay AroonDown</param>
        /// <returns>mảng chứa chỉ số Aroon tương ứng</returns>
        public double[] CalculateAroon(double[] closingPrices, int numDaysPeriod, bool isUp)
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
        /// Tìm số ngày tính từ ngày cao (thấp) nhất đến ngày end (dùng cho Aroon)
        /// </summary>
        private int FindNumDaysSinceOptimal(double[] closingPrices, int end, int numDaysPeriod, bool isMax)
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
        private double EMA(double[] closePrices, int index, double alpha, int baseIndex)
        {
            if (index == baseIndex)
            {
                return alpha * closePrices[baseIndex];
            }
            else
            {
                return alpha * closePrices[index] + (1 - alpha) * EMA(closePrices, index - 1, alpha, baseIndex);
            }
        }

        #endregion
    }
}
