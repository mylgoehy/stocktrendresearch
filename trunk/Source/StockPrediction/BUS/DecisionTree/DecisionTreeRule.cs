using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BUS.DecisionTree
{
    public class DecisionTreeRule
    {        
        #region Attritbutes
        private List<Rule> _listRules; 

        #endregion

        #region Properties
        public List<Rule> ListRules
        {
            get { return _listRules; }
            set { _listRules = value; }
        }
        #endregion

        #region Contructors
        public DecisionTreeRule()
        {
            ListRules = new List<Rule>();
        }
        #endregion


        #region Methods
        public void SaveModel2File(string fileName)
        {
            StreamWriter writer = new StreamWriter(fileName);
            writer.WriteLine(ListRules.Count);

            for (int i = 0; i < ListRules.Count; i++)
            {                
                writer.WriteLine(ListRules[i].FullRule);
                writer.WriteLine(ListRules[i].AttributeConditions.Count);
                // Ghi gia tri thuoc tinh lam dieu kien xuong
                for (int j = 0; j < ListRules[i].AttributeConditions.Count; j++)
                {
                    writer.WriteLine(ListRules[i].AttributeConditions[j]);
                    writer.WriteLine(ListRules[i].AttributeConditionValues[j]);
                }
                // Ghi gia tri thuoc tinh dich xuong
                writer.WriteLine(ListRules[i].AttributeTargets.Count);
                for (int j = 0; j < ListRules[i].AttributeTargets.Count; j++)
                {
                    writer.WriteLine(ListRules[i].AttributeTargets[j]);
                    writer.WriteLine(ListRules[i].AttributeTargetValues[j]);
                }
            }
            writer.Close();
        }

        public void LoadModelFromFile(string fileName)
        {
            StreamReader reader = new StreamReader(fileName);
            int numRule = int.Parse(reader.ReadLine());
            for (int i = 0; i < numRule; i++)
            {
                // Đọc chuỗi luật
                Rule rule = new Rule();
                rule.SetRule(reader.ReadLine());

                // Đọc sô thuộc tính điều kiện
                int numAttributeCondition = int.Parse(reader.ReadLine());
                // Đọc gia tri thuoc tinh lam dieu kien xuong
                for (int j = 0; j < numAttributeCondition; j++)
                {
                    rule.AttributeConditions.Add(reader.ReadLine());
                    rule.AttributeConditionValues.Add(reader.ReadLine());
                }
                // Đọc sô thuộc tính đích
                int numAttributeTarget = int.Parse(reader.ReadLine());
                for (int j = 0; j < numAttributeTarget; j++)
                {
                    rule.AttributeTargets.Add(reader.ReadLine());
                    rule.AttributeTargetValues.Add(reader.ReadLine());
                }
                ListRules.Add(rule);
            }
            reader.Close();
        }
        public int ClassifyTest(Dataset testData)
        {
            int NumExampleCorrect = 0;
            int[] example;
            for (int i = 0; i < testData.TrainingSet.Count; i++)
            {
                example = (int[])testData.getTrainingExample(i);
                for(int j = 0 ; j < ListRules.Count; j ++)
                {
                    bool b = isClassified(example, ListRules[j], testData);// true classified, false not classified
                    if (b == true)
                    {
                        NumExampleCorrect++;
                        break;
                    }                    
                }                
            }
            return NumExampleCorrect;
        }

        private bool isClassified(int[] example, Rule rule, Dataset testData)
        {            
            for (int i = 0; i < rule.AttributeConditions.Count; i++)
            {
                string attributeCondition = (string)rule.AttributeConditions[i];
                string ConditionValue = (string)rule.AttributeConditionValues[i];
                                
                int attributePosition = testData.getAttributePosition(attributeCondition);
                int valuesAtPositionExample = example[attributePosition];
                
                string exampleAttributeValuesAtPosition = testData.Attributes[attributePosition].getAttributeValueByNum(valuesAtPositionExample);

                if (ConditionValue != exampleAttributeValuesAtPosition)
                {                    
                    return false;
                }
            }
            return true;
        }
        #endregion





    }
}
