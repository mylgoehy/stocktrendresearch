using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BUS.DecisionTree
{
    public class Dataset
    {
        const int METAFILE = 0;
        const int DATAFILE = 1;

        #region Attributes
        private List<Attribute> _attributes;       // attributes for data set.
        private List<int[]> _trainingSet;      // training data storage.
        private List<int[]> _testingSet;       // testing data storage.
        private int[] _targetSums;       // number of examples in each target attribute class. 
        #endregion

        #region Properties
        public List<Attribute> Attributes
        {
            get { return _attributes; }
            set { _attributes = value; }
        }

        public List<int[]> TrainingSet
        {
            get { return _trainingSet; }
            set { _trainingSet = value; }
        }

        public List<int[]> TestingSet
        {
            get { return _testingSet; }
            set { _testingSet = value; }
        }

        public int[] TargetSums
        {
            get { return _targetSums; }
            set { _targetSums = value; }
        }
        #endregion

        #region Constructors
        /**
       * Builds a new Dataset.  Creates a FileParser to parse the metadata
       * and example files for the dataset.
       *
       * @param metaFile A file containing the metadata
       *        for this dataset and a pointer to the actual
       *        example file.
       *
       * @throws InvalidMetaFileException If the
       *         metadata file contains syntax errors.
       *
       * @throws InvalidDataFileException If the data
       *         (example) file contains syntax errors.
       *
       * @throws IOException If the configuration or data file
       *         cannot be read.
       */
        public Dataset(String metaFile, String dataFile)
        {
            Attributes = new List<Attribute>();
            TrainingSet = new List<int[]>();
            TestingSet = new List<int[]>();

            // First, we parse the configuration file and grab the various
            // attribute names etc.

            parseMetaFile(metaFile);                       

            // Initialize the target counts array.
            TargetSums = new int[getTargetAttribute().getNumValues()];

            parseDataFile(dataFile);
        }
        #endregion

        #region Methods
        /**
           * Creates a random testing dataset.  Calling this
           * method will destroy any previously built testing set.
           *
           * @param percentage Percentage of the entire dataset to
           *        use for testing.
           *
           * @param balanced <code>true</code> to create a balanced
           *        testing set, where the testing set and the
           *        remaining training set have the same proportion
           *        of each class.
           *
           * @throws IllegalArgumentException If the percentage value
           *         is < 0 or > 100.
           */
        public void createRndTestSet(int percentage, bool balanced)
        {
            if (percentage < 0 || percentage > 100)
                throw new Exception("Percentage value out of range.");

            // Move any examples that are part of the current testing
            // set back to the training set.
            for (int i = 0; i < TestingSet.Count; i++)
            {
                TrainingSet.Add((int[])TestingSet[i]);
            }
            TestingSet.Clear();

            // Calculate the number of examples that should be
            // in the testing set.
            int totalNumExamples = TrainingSet.Count;
            int numTestingExamples = (int)Math.Round(totalNumExamples * ((float)percentage) / 100.0f);
            Random rand = new Random();

            // If the set doesn't have to be balanced, then just
            // pick examples at random.
            if (!balanced)
            {
                for (int i = 0; i < numTestingExamples; i++)
                {
                    int temp = rand.Next(TrainingSet.Count);
                    TestingSet.Add((int[])TrainingSet[temp]);
                    TrainingSet.RemoveAt(temp);
                }
            }
            else
            {
                // We have the target value distribution for the dataset,
                // so reference it.
                for (int i = 0; i < TargetSums.Length; i++)
                {
                    int numExamplesToMove =
                     (int)Math.Round(TargetSums[i] / ((float)totalNumExamples) * numTestingExamples);

                    for (int j = 0; j < numExamplesToMove; j++)
                    {
                        // Attempt to randomly  pick examples from the
                        // dataset that have the required target classification.
                        int[] example = null;

                        while (true)
                        {
                            example = (int[])TrainingSet[rand.Next(TrainingSet.Count)];

                            if (example[0] == i) break;
                        }
                        int temp = TrainingSet.IndexOf(example);
                        TestingSet.Add((int[])TrainingSet[temp]);
                        TrainingSet.RemoveAt(temp);
                    }
                }
            }
        }
        /**
         * Finds and returns a particular attribute.
         *
         * @return An Attribute object with the specified
         *         name.
         *
         * @throws NonexistentAttributeException If
         *         the attribute does not exist in the dataset.
         */
        public Attribute getAttributeByName(String attName)
        {
            // Inefficient linear search of the vector
            for (int i = 0; i < Attributes.Count; i++)
            {
                Attribute att = (Attribute)Attributes.ElementAt(i);

                if (att.getName().Equals(attName))
                {
                    return att;
                }
            }

            throw new Exception("Attribute " + attName + " does not exist.");
        }
        /**
         * Finds and returns a particular attribute, using the attribute's
         * location in the internal attributes Vector.
         *
         * <p>
         * This method is primarily available to allow for iteration over all
         * attributes in the dataset.
         *
         * @return An Attribute object stored at the
         *         specified index in the attributes vector.
         *
         * @throws NonexistentAttributeException If
         *         the attribute does not exist in the dataset
         *         (i.e. the index is out of range).
         *
         */
        public Attribute getAttributeByNum(int attNum)
        {
            if (attNum < 0 || attNum >= Attributes.Count())
            {
                throw new Exception("Attribute at location " + attNum + " does not exist.");
            }
            return (Attribute)Attributes.ElementAt(attNum);
        }
        /**
          * Creates and returns a Vector that contains the names of
          * all attributes in this data set (including the target
          * attribute).
          *
          * @return A Vector containing Strings which are the
          *         names of all the attributes (in the order
          *         that they were parsed from the metadata file -
          *         target attribute first).
          */
        public List<String> getAttributeNames()
        {
            // Create and fill the vector of names
            List<String> names = new List<String>();

            for (int i = 0; i < Attributes.Count; i++)
                names.Add(((Attribute)Attributes.ElementAt(i)).getName());

            return names;
        }
        /**
         * Finds and returns the position of a particular attribute in the
         * Dataset's internal storage list.
         *
         * @param attName The name of the attribute to locate.
         *
         * @return The position of the attribute  in
         *         the Dataset's internal storage list.
         *
         * @throws NonexistentAttributeException If an
         *         attribute value with the supplied name
         *         does not exist.
         */
        public int getAttributePosition(String attName)
        {
            // Inefficient linear search of the vector
            for (int i = 0; i < Attributes.Count; i++)
            {
                String name = ((Attribute)Attributes.ElementAt(i)).getName();
                if (name.Equals(attName))
                {
                    return i;
                }
            }

            throw new Exception("Attribute named " + attName + " does not exist.");
        }
        /**
          * Returns the number of attributes (including the
          * target attribute) in this dataset.
          *
          * @return The total number of attributes in the
          *         dataset, including the target attribute.
          */
        public int getNumAttributes()
        {
            return Attributes.Count;
        }
        /**
         * Returns the number of testing examples currently
         * in this dataset.
         *
         * @return The total number of testing examples in
         *         the dataset.
         */
        public int getNumTestingExamples()
        {
            return TestingSet.Count;
        }
        /**
          * Returns the number of training examples <i>currently</i> in
          * this dataset.
          *
          * @return The total number of training examples in the dataset.
          */
        public int getNumTrainingExamples()
        {
            return TrainingSet.Count;
        }
        /**
          * Returns the target attribute for this data set.
          *
          * @return An Attribute object for the target attribute
          *         in this dataset.
          */
        public Attribute getTargetAttribute()
        {
            // The target attribute is always stored at
            // position 0 in the attributes vector.
            return (Attribute)Attributes.ElementAt(0);
        }
        /**
          * Returns a particular testing example from the examples in the
          * dataset.
          *
          * @return The selected example from the testing
          *         dataset as an integer array.
          *
          * @throws IndexOutOfBoundsException If the example
          *         number is less than zero, or greater than
          *         the number of testing examples in the
          *         dataset minus one.
          */
        public int[] getTestingExample(int exampleNum)
        {
            if (exampleNum < 0 || exampleNum >= TestingSet.Count)
                throw new Exception("Example number " + exampleNum + " does not exist.");

            return (int[])TestingSet.ElementAt(exampleNum);
        }
        /**
          * Returns a particular training example from the examples in the
          * dataset.
          *
          * @return The selected example from the training
          *         dataset as an integer array.
          *
          * @throws IndexOutOfBoundsException If the example
          *         number is less than zero, or greater than
          *         the number of training examples in the dataset
          *         minus one.
          */
        public int[] getTrainingExample(int exampleNum)
        {
            if (exampleNum < 0 || exampleNum >= TrainingSet.Count)
                throw new Exception("Example number " + exampleNum + " does not exist.");

            return (int[])TrainingSet.ElementAt(exampleNum);
        }
        /**
          * Returns an iterator over the training examples in the current dataset.
          *
          * @return An iterator over all the training examples.
          */
        public IEnumerator<int[]> getTrainingExamples()
        {
            return TrainingSet.GetEnumerator();
        }
        /**
         * Returns an iterator over the testing examples in the current dataset.
         *
         * @return An iterator over all the testing examples.
         */
        public IEnumerator<int[]> getTestingExamples()
        {
            return TestingSet.GetEnumerator();
        }
        /**
          * Moves an example from the training set to
          * the testing set.  If the supplied example number
          * is out of range no example is moved.
          *
          * @param exampleNum The example to transfer to the testing set.
          */
        public void moveToTestingSet(int exampleNum)
        {
            if (exampleNum < 0 || exampleNum > (TrainingSet.Count - 1))
            {
                return;
            }

            TestingSet.Add((int[])TrainingSet.ElementAt(exampleNum));
            TrainingSet.RemoveAt(exampleNum);
        }
        /**
          * Moves an example from the training set to
          * the testing set.  If the supplied example number
          * is out of range no example is moved.
          *
          * @param exampleNum The example to transfer to the training set.
          */
        public void moveToTrainingSet(int exampleNum)
        {
            if (exampleNum < 0 || exampleNum > (TestingSet.Count - 1))
            {
                return;
            }

            TrainingSet.Add((int[])TestingSet.ElementAt(exampleNum));
            TestingSet.RemoveAt(exampleNum);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataFile"></param>
        public void parseDataFile(string dataFile)
        {
            FileParsers parser = new FileParsers(dataFile, DATAFILE);

            int numSample = parser.DataLines.Count;
            
            String[] rawSample;
            for (int i = 0; i < numSample; i++)
            {
                rawSample = parser.extractDataSample(i);

                int[] dataSample = new int[Attributes.Count];

                try
                {
                    // 2.a. Deal with all the attributes.
                    for (int j = 0; j < rawSample.Length; j++)
                    {
                        // There should be a 1-to-1 ordering between
                        // the internal attributes vector and the
                        // raw sample vector.
                        Attribute currAtt = (Attribute)Attributes.ElementAt(j);

                        int attPos = currAtt.getAttributeValuePosition((String)rawSample.ElementAt(j));
                        dataSample[j] = attPos;

                        if (j == 0) TargetSums[attPos]++;
                    }
                }
                catch (Exception e)
                {
                }
                TrainingSet.Add(dataSample);
            }
        }
        /// <summary>
        /// Thực hiện đọc file MetaData để lấy thông tin
        /// </summary>
        /// <param name="metaFile">Đường dẫn file</param>
        public void parseMetaFile(string metaFile)
        {            
            FileParsers parser = new FileParsers(metaFile, METAFILE);
            String targetName = parser.getAttributeName(0);
            String[] targetValues ;
            int numTargetValues;
                        
            targetValues = parser.extractTargetAttributeValue(0);
            //// Build the target attribute and add it to the attributes vector.
            Attribute targetAttribute = new Attribute(targetName, targetValues, 1);

            numTargetValues = targetValues.Length;
            Attributes.Add(targetAttribute);

            List<Attribute> list = parser.extractAttributeFeatures();
            for (int i = 0; i < list.Count; i++)
            {
                Attributes.Add((Attribute)list[i]);
            }            
        }
        #endregion
    }
}
