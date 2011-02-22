using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NeuronDotNet.Core;
namespace BUS.KMeans
{
    public class Clustering
    {
        #region Attributes
        /// <summary>
        /// Tập mẫu
        /// </summary>
        private SampleSet _sampleData;
        /// <summary>
        /// Tập các cluster
        /// </summary>
        private Cluster[] _clusters;
        /// <summary>
        /// Dùng để kiểm tra điều kiện dừng của thuật toán
        /// </summary>
        private bool _hasClusterChanged;
        #endregion

        #region Properties
        public SampleSet SampleData
        {
            get { return _sampleData; }
            set { _sampleData = value; }
        }
        public Cluster[] Clusters
        {
            get { return _clusters; }
            set { _clusters = value; }
        }
        public bool HasClusterChanged
        {
            get { return _hasClusterChanged; }
            set { _hasClusterChanged = value; }
        }
        #endregion

        #region Constructors
        public Clustering(int numCluster, double[][] samples, DistanceType distType)
        {
            SampleData = new SampleSet(samples, distType);
            Clusters = new Cluster[numCluster];
            for (int i = 0; i < numCluster; i++)
            {
                Clusters[i] = new Cluster(samples[0].Length);
            }
            HasClusterChanged = true;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Chọn các trung tâm cho từng cụm bằng cách ngẫu nhiên
        /// </summary>
        private void ChooseRandomMeanForClusters()
        {
            int[] randomOrders = Helper.GetRandomOrder(SampleData.Samples.Length);
            for (int i = 0; i < Clusters.Length; i++)
            {
                Clusters[i].MeanSample = SampleData.Samples[randomOrders[i]];
            }
        }
        private void Run()
        {
            while (HasClusterChanged)
            {
                HasClusterChanged = false;
                // Tính khoảng cách và phân cụm
                for (int i = 0; i < SampleData.Samples.Length; i++)
                {
                    for (int j = 0; j < Clusters.Length; j++)
                    {
                        double dblDist = SampleData.CalculateDistance(SampleData.Samples[i], Clusters[j].MeanSample);
                        if (SampleData.DistanceToClusters[i] == -1 || dblDist < SampleData.DistanceToClusters[i])
                        {
                            SampleData.DistanceToClusters[i] = dblDist;
                            int iCurrentClusterIndex = SampleData.ClusterIndices[i];    // Chỉ số cluster mà mẫu i hiện tại thuộc về
                            if (iCurrentClusterIndex != j)
                            {
                                HasClusterChanged = true;
                                if (iCurrentClusterIndex != -1)
                                {
                                    --Clusters[iCurrentClusterIndex].NumSample;
                                }
                                ++Clusters[j].NumSample;
                                SampleData.ClusterIndices[i] = j;
                            }
                        }
                    }
                    Clusters[SampleData.ClusterIndices[i]].AddToTemp(SampleData.Samples[i]);
                }
                for (int i = 0; i < Clusters.Length; i++)
                {
                    Clusters[i].ReCalculateMean();
                }
            }
        }
        #endregion
    }
}
