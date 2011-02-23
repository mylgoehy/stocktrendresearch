using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace DAO
{
    public class SampleDataDAO
    {
        private string[] _dataLines;
        private double[][] _samples;

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

        public void WriteIntoCluster(string[] fileNames, int[] clusterIndices)
        {
            StreamWriter[] writers = new StreamWriter[fileNames.Length];
            for (int i = 0; i < fileNames.Length; i++)
            {
                writers[i] = new StreamWriter(fileNames[i]);
            }
            for (int i = 0; i < _dataLines.Length; i++)
            {
                writers[clusterIndices[i]].WriteLine(_dataLines[i]);
            }
            for (int i = 0; i < fileNames.Length; i++)
            {
                writers[i].Close();
            }
        }
    }
}
