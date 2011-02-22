using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BUS.KMeans
{
    public enum DistanceType
    {
        Manhattan = 0,
        Euclid = 1
    }

    public class SampleSet
    {
        #region Attributes
        /// <summary>
        /// Loại khoảng cách
        /// </summary>
        private DistanceType _distType;
        /// <summary>
        /// Tập các giá trị sample, mỗi sample là 1 vector nhiều chiều
        /// </summary>
        private double[][] _samples; 
        /// <summary>
        /// Khoảng cách tới cluster mà sample thuộc về
        /// </summary>
        private double[] _distanceToClusters;
        /// <summary>
        /// Chỉ số cluster mà sample thuộc về
        /// </summary>
        private int[] _clusterIndices;
        #endregion

        #region Properties
        
        public DistanceType DistType
        {
          get { return _distType; }
          set { _distType = value; }
        }
        public double[][] Samples
        {
            get { return _samples; }
            set { _samples = value; }
        }
        public double[] DistanceToClusters
        {
            get { return _distanceToClusters; }
            set { _distanceToClusters = value; }
        }
        public int[] ClusterIndices
        {
            get { return _clusterIndices; }
            set { _clusterIndices = value; }
        }
        #endregion

        #region Constructors
        public SampleSet(double[][] input, DistanceType type)
        {
            Samples = input;
            DistanceToClusters = new double[input.Length];
            ClusterIndices = new int[input.Length];
            for(int i = 0;i < DistanceToClusters.Length;i++)
            {
                DistanceToClusters[i] = -1;
                ClusterIndices[i] = -1;
            }
            DistType = type;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Tính khoảng cách giữa 2 vector
        /// </summary>
        /// <param name="vectorA"> vector A</param>
        /// <param name="vectorB">vector B</param>
        /// <returns>Giá trị khoảng cách</returns>
        public double CalculateDistance(double[] vectorA, double[] vectorB)
        {
            double dblDistance = 0;
            if (DistType == DistanceType.Manhattan)
            {
                for (int i = 0; i < vectorA.Length; i++)
                {
                    dblDistance += Math.Abs(vectorA[i] - vectorB[i]);
                }
            }
            else
            {
                for (int i = 0; i < vectorA.Length; i++)
                {
                    dblDistance += Math.Pow(vectorA[i] - vectorB[i], 2);
                }
            }
            return dblDistance;
        }

        #endregion
    }
}
