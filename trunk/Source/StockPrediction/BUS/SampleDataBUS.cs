using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace BUS
{
    public class SampleDataBUS
    {
        #region Attributes
        /// <summary>
        /// Chứa các dòng dữ liệu mẫu cho training và test
        /// </summary>
        private string[] _dataLines;
        /// <summary>
        /// Các vector đầu vào, không bao gồm nhãn
        /// </summary>
        private double[][] _samples;
        #endregion

        #region Properties
        public string[] DataLines
        {
            get { return _dataLines; }
            set { _dataLines = value; }
        }
        
        public double[][] Samples
        {
            get { return _samples; }
            set { _samples = value; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Đọc file chứa các mẫu
        /// </summary>
        /// <param name="fileName">Tên file</param>
        public void Read(string fileName)
        {
            StreamReader stream = new StreamReader(fileName);
            string strTemp = stream.ReadToEnd();
            stream.Close();
            string[] strLines = Regex.Split(strTemp, "\n");
            int iNumLines = strLines.Length;
            if (strLines[0] == "")
            {
                iNumLines--;
            }
            if (strLines[strLines.Length - 1] == "")
            {
                iNumLines--;
            }
            _dataLines = new string[iNumLines];
            _samples = new double[iNumLines][];
            int iLineIndex = 0;
            for (int i = 0; i < strLines.Length; i++)
            {
                if (strLines[i] == "")
                {
                    continue;
                }
                _dataLines[iLineIndex] = strLines[i];
                string[] parts = strLines[i].Trim().Split();
                int iSampleDimension = parts.Length - 1;
                _samples[i] = new double[iSampleDimension];
                for (int j = 0; j < iSampleDimension; j++)
                {
                    string[] nodeParts = parts[j + 1].Split(':');
                    _samples[i][j] = double.Parse(nodeParts[1]);
                }
                iLineIndex++;
            }
        }
        /// <summary>
        /// Ghi các mẫu đã phân cụm ra từng file riêng
        /// </summary>
        /// <param name="fileNames">Mảng các tên file ứng với từng cụm</param>
        /// <param name="clusterIndices">Mảng chứa chỉ số cluster ứng từng sample</param>
        public void WriteIntoCluster(string[] fileNames, int[] clusterIndices)
        {
            StreamWriter[] writers = new StreamWriter[fileNames.Length];
            for (int i = 0; i < fileNames.Length; i++)
            {
                writers[i] = new StreamWriter(fileNames[i]);
            }
            for (int i = 0; i < _dataLines.Length; i++)
            {
                writers[clusterIndices[i]].Write(_dataLines[i]);
            }
            for (int i = 0; i < fileNames.Length; i++)
            {
                writers[i].Close();
            }
        }
        #endregion
    }
}
