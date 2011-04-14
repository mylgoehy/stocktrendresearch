using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BUS.DecisionTree
{
    public class Rule
    {
        #region Attributes
        private String _fullRule;      // Thể hiện một luật đầy đủ
        private List<String> _attributeConditions; // Danh sách các giá trị thuộc tinh điều kiện
        private List<String> _attributeConditionValues; // Danh sách các giá trị tương ứng
        private List<String> _attributeTargets; // Danh sách thuộc tính kết quả
        private List<String> _attributeTargetValues; // Danh sách các giá trị thuộc tính kết quả tương ứng
        
        #endregion

        #region Properties
        public String FullRule
        {
            get { return _fullRule; }
            set { _fullRule = value; }
        }

        public List<String> AttributeConditions
        {
            get { return _attributeConditions; }
            set { _attributeConditions = value; }
        }
        public List<String> AttributeConditionValues
        {
            get { return _attributeConditionValues; }
            set { _attributeConditionValues = value; }
        }
        public List<String> AttributeTargets
        {
            get { return _attributeTargets; }
            set { _attributeTargets = value; }
        }
        public List<String> AttributeTargetValues
        {
            get { return _attributeTargetValues; }
            set { _attributeTargetValues = value; }
        }        
        #endregion

        #region Constructors
        public Rule()
        {
            AttributeConditions = new List<string>();
            AttributeConditionValues = new List<string>();
            AttributeTargets = new List<string>();
            AttributeTargetValues = new List<string>();
        }
        #endregion

        #region Methods
        public void AddAttributeCondition(string attName, string attValue)
        {
            AttributeConditions.Add(attName);
            AttributeConditionValues.Add(attValue);
        }
        public void AddAttributeTarget(string attTName, string attTValue)
        {
            AttributeTargets.Add(attTName);
            AttributeTargetValues.Add(attTValue);
        }
        public void SetRule(string strRule)
        {
            FullRule = strRule;
        }
        #endregion
    }
}
