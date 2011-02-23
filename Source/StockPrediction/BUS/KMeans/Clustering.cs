using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NeuronDotNet.Core;
using System.IO;
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
        /// <summary>
        /// Khởi tạo cho thuật toán, dùng khi xây dựng cluster từ tập mẫu
        /// </summary>
        /// <param name="numCluster">Số cluster muốn tạo</param>
        /// <param name="samples">Các vector mẫu</param>
        /// <param name="distType">Loại khoảng cách</param>
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
        /// <summary>
        /// Khởi tạo dùng khi xác định cluster cho tập test, trường hợp đã có thông tin cluster
        /// </summary>
        /// <param name="samples">các mẫu test</param>
        /// <param name="distType">Loại khoảng cách</param>
        public Clustering(double[][] samples, DistanceType distType)
        {
            SampleData = new SampleSet(samples, distType);
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
        /// <summary>
        /// Thực hiện phân phối các mẫu vào cụm
        /// </summary>
        private void Distribute(bool testMode)
        {
            if (testMode)
            {
                for (int i = 0; i < SampleData.Samples.Length; i++)
                {
                    for (int j = 0; j < Clusters.Length; j++)
                    {
                        double dblDist = SampleData.CalculateDistance(SampleData.Samples[i], Clusters[j].MeanSample);
                        if (SampleData.DistanceToClusters[i] == -1 || dblDist < SampleData.DistanceToClusters[i])
                        {
                            SampleData.DistanceToClusters[i] = dblDist;
                            SampleData.ClusterIndices[i] = j;
                        }
                    }
                }
            }
            else
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
        }
        /// <summary>
        /// Ghi thông tin các cluster xuống file
        /// </summary>
        /// <param name="fileName">Tên file đích</param>
        private void WriteClusters(string fileName)
        {
            StreamWriter writer = new StreamWriter(fileName);
            writer.WriteLine(Clusters.Length);

            for (int i = 0; i < Clusters.Length; i++)
            {
                writer.WriteLine(Clusters[i].NumSample);
                for (int j = 0; j < Clusters[i].MeanSample.Length; j++)
                {
                    writer.Write(Clusters[i].MeanSample[j]);
                    if (j + 1 < Clusters[i].MeanSample.Length)
                    {
                        writer.Write(" ");
                    }
                    else
                    {
                        writer.Write("\n");
                    }
                }
            }
            writer.Close();
        }
        /// <summary>
        /// Đọc thông tin các cluster từ file
        /// </summary>
        /// <param name="fileName">Tên file đích</param>
        private void LoadClusters(string fileName)
        {
            StreamReader reader = new StreamReader(fileName);
            string buffer = null;
            buffer = reader.ReadLine();
            Clusters = new Cluster[Int32.Parse(buffer)];

            for (int i = 0; i < Clusters.Length; i++)
            {
                buffer = reader.ReadLine();
                Clusters[i].NumSample = Int32.Parse(buffer);
                buffer = reader.ReadLine();
                string[] parts = buffer.Split(' ');
                for (int j = 0; j < parts.Length; j++)
                {
                    Clusters[i].MeanSample[j] = Double.Parse(parts[j]);
                }

            }
            reader.Close();
        }

        public void Run(string fileName, bool testMode)
        {
            if (testMode)
            {
                LoadClusters(fileName);
                Distribute(testMode);
            }
            else
            {
                ChooseRandomMeanForClusters();
                Distribute(testMode);
                WriteClusters(fileName);
            }
        }
        #endregion
    }
}
