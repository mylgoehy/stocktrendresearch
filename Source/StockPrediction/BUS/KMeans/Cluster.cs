using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BUS.KMeans
{
    public class Cluster
    {
        #region Attributes
        /// <summary>
        /// Vector mẫu trung bình của cluster
        /// </summary>
        double[] _meanSample;
        /// <summary>
        /// Vector dùng để tính toán vector trung bình
        /// </summary>
        double[] _tempSample;
        /// <summary>
        /// Số mẫu thuộc về cluster
        /// </summary>
        int _numSample;
        #endregion

        #region Properties
        public double[] MeanSample
        {
            get { return _meanSample; }
            set { _meanSample = value; }
        }
        public double[] TempSample
        {
            get { return _tempSample; }
            set { _tempSample = value; }
        }
        public int NumSample
        {
            get { return _numSample; }
            set { _numSample = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Khởi tạo 
        /// </summary>
        /// <param name="dimension">Số chiều của vector</param>
        public Cluster(int dimension)
        {
            NumSample = 0;
            TempSample = new double[dimension];
            ClearTempSample();
        }
        /// <summary>
        /// Khởi tạo
        /// </summary>
        public Cluster()
        {
            NumSample = 0;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Cộng dồn vào TempSample
        /// </summary>
        /// <param name="sample">mẫu</param>
        public void AddToTemp(double[] sample)
        {
            for (int i = 0; i < sample.Length; i++)
            {
                TempSample[i] += sample[i];
            }
        }
        /// <summary>
        /// Khởi tạo lại TempSample
        /// </summary>
        private void ClearTempSample()
        {
            for (int i = 0; i < TempSample.Length; i++)
            {
                TempSample[i] = 0;
            }
        }
        /// <summary>
        /// Tính lại vector trung tâm
        /// </summary>
        public void ReCalculateMean()
        {
            for (int i = 0; i < MeanSample.Length; i++)
            {
                MeanSample[i] = TempSample[i] / NumSample;
            }
            ClearTempSample();
        }
        #endregion
    }
}
