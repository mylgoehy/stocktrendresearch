using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BUS
{
    public class PreprocessBUS
    {
        #region Attributes
        private double _min;
        private double _max;
        private double _mean;
        private double _variance;

        #endregion

        #region Constructors
        #endregion

        #region Properties
        public double Min
        {
            get { return _min; }
            set { _min = value; }
        }

        public double Max
        {
            get { return _max; }
            set { _max = value; }
        }

        public double Mean
        {
            get { return _mean; }
            set { _mean = value; }
        }

        public double Variance
        {
            get { return _variance; }
            set { _variance = value; }
        }
        #endregion

        #region Methods
 
        public void CalculateMean(double[] arr)
        {
            Mean = (arr[arr.Length - 1] - arr[0]) / (arr.Length - 1);
        }

        public void CalculateVariance(double[] arr)
        {
            Variance = 0;
            for (int i = 0; i < arr.Length - 1; i++)
            {
                Variance = Math.Pow((arr[i + 1] - arr[i] - Mean), 2);
            }
            Variance = Math.Sqrt(Variance / (arr.Length - 1));
        }

        public double[] DeNoiseByARIMA(double[] arr)
        {
            double[] result = new double[arr.Length];

            CalculateMean(arr);
            CalculateVariance(arr);

            for (int i = 0; i < arr.Length - 1; i++)
            {
                result[i] = NormalizeBySigmoid((arr[i + 1] - arr[i] - Mean) / Variance);
            }
            result[arr.Length - 1] = NormalizeBySigmoid(-Mean / Variance);

            return result;
        }

        public static double NormalizeBySigmoid(double value)
        {
            double result = 1 / (1 + Math.Exp(-value));
            return result;
        }

        public double PreprocessByMinMax(double value)
        {
            double result = (value - Min) / (Max - Min);
            return result;
        }

        public double[] PreprocessByMinMax(double[] arr)
        {
            //FindMinMax(arr);

            double[] result = new double[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                result[i] = PreprocessByMinMax(arr[i]);
            }

            return result;
        }

        public double[][] PreprocessByMinMax(double[][] arr)
        {
            //FindMinMax(arr);

            double[][] result = new double[arr.Length][];
            for (int i = 0; i < arr.Length; i++)
            {
                result[i] = new double[arr[i].Length];
                for (int j = 0; j < arr[i].Length; j++ )
                {
                    result[i][j] = PreprocessByMinMax(arr[i][j]);
                }
            }

            return result;
        }

        public double DenormalizeByMinMax(double value)
        {
            return (value * (Max - Min) + Min);
        }

        public double[] DenormalizeByMinMax(double[] arr)
        {
            double[] result = new double[arr.Length];
            
            for (int i = 0; i < arr.Length; i++)
            {
                result[i] = DenormalizeByMinMax(arr[i]);
            }

            return result;
        }

        public double[][] DenormalizeByMinMax(double[][] arr)
        {
            double[][] result = new double[arr.Length][];
            for (int i = 0; i < arr.Length; i++)
            {
                result[i] = new double[arr[i].Length];
                for (int j = 0; j < arr[i].Length; j++)
                {
                    result[i][j] = DenormalizeByMinMax(arr[i][j]);
                }
            }

            return result;
        }

        public void FindMinMax(double[] arr)
        {
            Min = double.MaxValue;
            Max = double.MinValue;
            for (int i = 0; i < arr.Length; i++)
            {
                if (Min > arr[i])
                {
                    Min = arr[i];
                }
                if (Max < arr[i])
                {
                    Max = arr[i];
                }
            }
        }

        public void FindMinMax(double[][] arr)
        {
            Min = double.MaxValue;
            Max = double.MinValue;
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < arr[i].Length; j++)
                {
                    if (Min > arr[i][j])
                    {
                        Min = arr[i][j];
                    }
                    if (Max < arr[i][j])
                    {
                        Max = arr[i][j];
                    }
                }
            }
        }

        public double[] Scale_SVR_Price(int n, double[] arr)
        {
            double[] result = new double[n];
            FindMinMax(arr);
            double dblDiff = this.Max - this.Min;
            for (int i = 0; i < n; i++)
            {
                result[i] = (arr[i] - this.Min) * 0.7 / dblDiff + 0.15;
            }
            return result;
        }

        public void FindMaxChange(double[] values)
        {
            Max = Math.Abs(values[0]);
            for (int i = 1; i < values.Length; i++)
            {
                if (Math.Abs(values[i]) > Max)
                {
                    Max = Math.Abs(values[i]);
                }
            }
        }

        ///<summary>Phương thức scale về dạng return [-1;1]</summary>
        ///<param>n: số giá trị đầu vào, arr: mảng giá trị, range: ngày giữa 2 giá trị return</param>
        ///<return>Mảng sau khi scale</return>
        public double[] Scale_SVR_Return(int n, double[] arr, int range, int type)
        {
            double[] dblResults = new double[n - range + 1];
            dblResults[0] = 0;
            switch (type)
            {
                case 1:
                    for (int i = range; i < n; i++)
                    {
                        dblResults[i - range + 1] = 100 * (Math.Log10(arr[i]) - Math.Log10(arr[i - range]));
                    }
                    break;
                case 2:
                    for (int i = range; i < n; i++)
                    {
                        dblResults[i - range + 1] = arr[i] - arr[i - range];
                    }
                    FindMaxChange(dblResults);
                    for (int i = 0; i < dblResults.Length; i++)
                    {
                        dblResults[i] = dblResults[i] / this.Max;
                    }
                    break;
            }
            
            //FindMaxChange(dblResults);
            //for (int i = 0; i < dblResults.Length; i++)
            //{
            //    dblResults[i] = dblResults[i]; / this.Max;
            //}
            return dblResults;
        }

        #endregion
    }
}
