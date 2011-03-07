using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BUS
{
    public class ANNParameterBUS
    {
        #region Attributes
        private static int inputNode;
        private static int hiddenNode;
        private static int outputNode;

        private static double learningRate;
        private static double momentum;
        private static int maxEpoch;
        private static double bias;

        private static int trainingSize;

        private static double accuracy;
        private static string measureType;

        #endregion

        #region Properties
        public static int InputNode
        {
            get { return ANNParameterBUS.inputNode; }
            set { ANNParameterBUS.inputNode = value; }
        }

        public static int HiddenNode
        {
            get { return ANNParameterBUS.hiddenNode; }
            set { ANNParameterBUS.hiddenNode = value; }
        }

        public static int OutputNode
        {
            get { return ANNParameterBUS.outputNode; }
            set { ANNParameterBUS.outputNode = value; }
        }

        public static double LearningRate
        {
            get { return ANNParameterBUS.learningRate; }
            set { ANNParameterBUS.learningRate = value; }
        }

        public static double Momentum
        {
            get { return ANNParameterBUS.momentum; }
            set { ANNParameterBUS.momentum = value; }
        }

        public static int MaxEpoch
        {
            get { return ANNParameterBUS.maxEpoch; }
            set { ANNParameterBUS.maxEpoch = value; }
        }

        public static double Bias
        {
            get { return ANNParameterBUS.bias; }
            set { ANNParameterBUS.bias = value; }
        }

        public static int TrainingSize
        {
            get { return ANNParameterBUS.trainingSize; }
            set { ANNParameterBUS.trainingSize = value; }
        }

        public static double Accuracy
        {
            get { return ANNParameterBUS.accuracy; }
            set { ANNParameterBUS.accuracy = value; }
        }

        public static string MeasureType
        {
            get { return ANNParameterBUS.measureType; }
            set { ANNParameterBUS.measureType = value; }
        }
        #endregion

        #region Methods       
        #endregion
    }
}
