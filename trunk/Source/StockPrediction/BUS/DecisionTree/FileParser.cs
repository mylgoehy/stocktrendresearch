using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace DecisionTree
{
    class FileParsers
    {
        const int METAFILE = 0;
        const int DATAFILE = 1;

        #region Atributes
        private List<string> _metaLines;
        private List<string> _dataLines;
        #endregion

        #region Properties
        public List<string> MetaLines
        {
            get { return _metaLines; }
            set { _metaLines = value; }
        }
        public List<string> DataLines
        {
            get { return _dataLines; }
            set { _dataLines = value; }
        }
        #endregion

        #region Contructors
        public FileParsers(string filePath, int typeFile)
        { 
            StreamReader reader = null;
            if (typeFile == METAFILE)
            {
                MetaLines = new List<string>();               
                // Đọc meta lines
                try
                {
                    reader = new StreamReader(filePath);
                    string strTemps = reader.ReadToEnd();
                    reader.Close();

                    string[] strLines = Regex.Split(strTemps, "\r\n");

                    for (int i = 0; i < strLines.Length; i++)
                    {
                        if (strLines[i] != string.Empty && strLines[i].IndexOf('\"') >= 0)
                        {
                            MetaLines.Add(strLines[i]);
                        }
                    }
                    reader = null;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                DataLines = new List<string>();  
                try
                {
                    reader = new StreamReader(filePath);
                    string strTemps = reader.ReadToEnd();
                    reader.Close();

                    string[] strLines = Regex.Split(strTemps, "\r\n");
                    for (int i = 0; i < strLines.Length; i++)
                    {
                        DataLines.Add(strLines[i]);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }  
        }
        #endregion

        #region Methods
        public string getAttributeName(int pos)
        {
            string result;

            string targetLine = (string) MetaLines[pos];

            int index = targetLine.IndexOf('\"');
            int index1 = index + 1;
            while(targetLine[index1] != '\"')
            {
                index1++;
            }
            result = targetLine.Substring(index+1, index1 - index -1);
            
            return result;
        }
        public String[] extractTargetAttributeValue(int pos)
        {
            String[] results;
            results = new string[2];
            string targetLine = (string)MetaLines[pos];
            string substring;

            List<String> tempList = new List<String>();
            while (true)
            {
                int index = targetLine.IndexOf('\'');
                if (index < 0)
                {
                    break;
                }
                int index1 = index + 1;
                while (targetLine[index1] != '\'')
                {
                    index1++;
                }
                substring = targetLine.Substring(index + 1, index1 - index - 1);
                targetLine = targetLine.Substring(index1 + 1, targetLine.Length - index1 -1);
                tempList.Add(substring);
            }
            results = new string[tempList.Count];

            for (int i = 0; i < tempList.Count; i++)
            {
                results[i] =(string) tempList[i]; 
            }
            
            return results;
        }
        public List<Attribute> extractAttributeFeatures()
        {
            List<Attribute> list = new List<Attribute>();
            string nameAttribute;
            string[] AttritbuteValues = null;

            for (int i = 1; i < MetaLines.Count; i++)
            {
                nameAttribute = getAttributeName(i);
                AttritbuteValues = extractTargetAttributeValue(i);
                Attribute att = new Attribute(nameAttribute, AttritbuteValues, AttritbuteValues.Length);
                list.Add(att);
            }
            
            return list;
        }
        public string[] extractDataSample(int index)
        {
            String temp =(string) DataLines[index];
            String[] raw = Regex.Split(temp,",");
            String[] results = new string[raw.Length - 1];
            for (int i = 0; i < results.Length; i++)
            {
                results[i] = (string)raw[i].Trim();                
            }
            return results;
        }
        #endregion
    }
}
