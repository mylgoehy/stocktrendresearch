using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DecisionTree;

namespace DecisionTree
{
    public class DecisionTreeAlgorithm
    {
        #region Constanst
        // Possible splitting criteria

        /**
         * Indicates that attributes for splitting are selected
         * at random.
         */
        public static String SPLIT_RANDOM = "Random";

        /**
         * Indicates that attributes for splitting are selected
         * based on maximum information gain.
         */
        public static String SPLIT_GAIN = "Gain";

        /**
         * Indicates that attributes for splitting are selected
         * based on maximum gain ratio.
         */
        public static String SPLIT_GAIN_RATIO = "Gain Ratio";

        /**
         * Indicates that attributes for splitting are selected
         * based on maxiumum GINI score.
         */
        public static String SPLIT_GINI = "GINI";

        /**
         * An array of the available splitting functions.
         */
        public static String[] SPLIT_FUNCTIONS = { SPLIT_RANDOM, SPLIT_GAIN, SPLIT_GAIN_RATIO, SPLIT_GINI };

        //-------------------------------------------------------

        /**
         * Indicates that the examples have mixed target
         * attribute values.
         */
        public static int DATASET_MIXED_CONCL = 0;

        /**
         * Indicates that all examples share one common target
         * attribute value.
         */
        public static int DATASET_IDENT_CONCL = 1;

        /**
         * Indicates that the set of training examples is empty.
         */
        public static int DATASET_EMPTY = 2;

        //-------------------------------------------------------

        // Possible pruning algorithms

        /**
         * Indicates that the decision tree should not be
         * pruned.
         */
        public static String PRUNING_NONE = "None";

        /**
         * Indicates that the decision tree should be
         * pruned using the reduced-error pruning
         * algorithm.
         */
        public static String PRUNING_REDUCED_ERROR = "Reduced-error";

        /**
         * Indicates that the decision tree should be
         * pruned using the pessimistic pruning
         * algorithm.
         */
        public static String PRUNING_PESSIMISTIC = "Pessimistic";

        /**
         * An array of the available pruning algorithms.
         */
        public static String[] PRUNING_ALGORITHMS = { PRUNING_NONE, PRUNING_REDUCED_ERROR, PRUNING_PESSIMISTIC };

        /**
         * Default pessimistic pruning z-score - 95% confidence
         * interval.
         */
        public static double DEFAULT_Z_SCORE = 1.96;

        #endregion

        #region Attributes

        Dataset _dataset;    // Data set used to build tree.
        DecisionTree _tree;       // Current decision tree.
        String _splitFun;   // Current splitting function.
        String _pruneAlg;   // Current pruning algorithm.
        Random _random;     // Random number generator.

        double _pessPruneZScore;   // Pessimistic pruning Z-score.

        // Tự khai báo thêm
        DecisionTreeNode _currentRoot;
        List<string> _listRules;

        #endregion

        #region Properties
        public Dataset DatasetUse
        {
            get { return _dataset; }
            set { _dataset = value; }
        }
        public DecisionTree Tree
        {
            get { return _tree; }
            set { _tree = value; }
        }
        public String SplitFun
        {
            get { return _splitFun; }
            set { _splitFun = value; }
        }
        public String PruneAlg
        {
            get { return _pruneAlg; }
            set { _pruneAlg = value; }
        }
        public Random RandomGenerator
        {
            get { return _random; }
            set { _random = value; }
        }
        public double PessPruneZScore
        {
            get { return _pessPruneZScore; }
            set { _pessPruneZScore = value; }
        }
        public DecisionTreeNode CurrentRoot
        {
            get { return _currentRoot; }
            set { _currentRoot = value; }
        }
        public List<string> ListRules
        {
            get { return _listRules; }
            set { _listRules = value; }
        }
        #endregion

        #region Constructors
        /**
           * Prepares to run the decision tree algorithm by
           * creating an empty decision tree.
           *
           * <p>
           * The default splitting function is set to &quot;Random&quot;.
           *
           * @param dataset A Dataset object that is already initialized.
           *
           * @throws NullPointerException If the supplied Dataset or
           *         ComponentManager is null.
           */
        public DecisionTreeAlgorithm(Dataset dataset)
        {
            //super();

            if (dataset == null)
                throw new Exception("Dataset is null.");

            //_manager = manager;

            DatasetUse = dataset;
            SplitFun = SPLIT_RANDOM;
            PruneAlg = PRUNING_NONE;
            RandomGenerator = new Random(2389);
            Tree = new DecisionTree();

            PessPruneZScore = DEFAULT_Z_SCORE;

            // Viết thêm

            ListRules = new List<string>();
        }
        #endregion

        #region Methods
        /**
           * Choose an available attribute to split on,
           * based on the supplied attribute mask.  The
           * current splitting function is used to select
           * a particular attribute.
           *
           * @param atts A vector that contains all available (UNUSED)
           *        attributes.  The attributes in the vector have
           *        statistics attached that are used in the selection
           *        process.
           *
           * @param stats An array that contains classification
           *        results for a particular path through the
           *        tree, <i>before</i> splitting.
           *
           * @param results An initially empty vector that,
           *        depending on the splitting function, can be
           *        filled with splitting results for each of the
           *        available attributes.  Entries in the Vector
           *        are Double objects.  If a particular splitting
           *        function does not return numerical results
           *        (e.g. the 'Random' function), the Vector
           *        remains empty.
           *
           * @return The attribute that <i>best</i> classifies
           *         current examples (examples that are valid
           *         at the current position in the tree).
           */
        public Attribute chooseAttribute(List<Attribute> atts, int[] stats, List<Double> results)
        {
            // If the list of available attributes is empty,
            // return null.
            if (atts.Count == 0)
            {
                return null;
            }

            int pos = 0;

            // Select an attribute, based on the current
            // splitting function.
            if (SplitFun.Equals(SPLIT_RANDOM))
                // Choose one of the unmasked attributes
                // at random.  We leave the results vector
                // empty in this case.
                pos = RandomGenerator.Next(atts.Count);
            else
            {
                // Calculate a result value for each
                // attribute that is available.
                double val = 0.0;
                double temp;

                for (int i = 0; i < atts.Count; i++)
                {
                    if (SplitFun.Equals(SPLIT_GAIN))
                    {
                        temp = getGain(stats, (Attribute)atts[i]);

                        if (temp > val)
                        {
                            val = temp;
                            pos = i;
                        }
                        results.Add(temp);
                    }
                    else if (SplitFun.Equals(SPLIT_GAIN_RATIO))
                    {
                        temp = getGainRatio(stats, (Attribute)atts[i]);

                        if (temp > val)
                        {
                            val = temp;
                            pos = i;
                        }

                        results.Add(temp);
                    }
                    else if (SplitFun.Equals(SPLIT_GINI))
                    {
                        temp = getGINI(stats, (Attribute)atts[i]);

                        if (temp > val)
                        {
                            val = temp;
                            pos = i;
                        }
                    }
                }
            }

            return (Attribute)atts[pos];
        }
        /**
         * Classifies all examples in the current set of
         * examples, by target attribute value.  The
         * attribute mask determines which examples from the
         * dataset form the current example set.
         *
         * @param mask The current attribute mask that
         *        indicates which examples from the dataset
         *        should be considered.
         *
         * @param conclusion The method expects the parameter
         *        to be an array of size 8.  Positions in
         *        the array are filled with the following
         *        values.
         *
         *        <ul>
         *            <li><i>Position 0</i> - Records the
         *            index of the most common target attribute
         *            value from the training dataset.
         *
         *            <li><i>Position 1</i> - Records the number
         *            of training examples from the dataset that
         *            reach the current position.
         *
         *            <li><i>Position 2</i> - Records the number
         *            of training examples from the dataset
         *            that would be correcly classified
         *            <i>if a leaf with the most common training
         *            target classification</i> were added at the
         *            current position.
         *
         *            <li><i>Position 3</i> - Records the number
         *            if testing examples from the dataset
         *            that would be correctly classified
         *            <i>if a leaf with the most common training
         *            target classification</i> were added at the
         *            current position.
         *
         *            <li><i>Position 4</i> - Records the index
         *            of the most common target attribute
         *            value from the testing dataset.
         *
         *            <li><i>Position 5</i> - Records the number
         *            of testing examples from the dataset that
         *            reach the current position.
         *
         *            <li><i>Position 6</i> - Records the number
         *            of testing examples from the dataset
         *            that would be correcly classified
         *            <i>if a leaf with the most common testing
         *            target classification</i> were added at the
         *            current position.
         *
         *            <li><i>Position 7</i> - Records the number
         *            if training examples from the dataset
         *            that would be correctly classified
         *            <i>if a leaf with the most common testing
         *            target classification</i> were added at the
         *            current position.
         *        </ul>
         *
         * @param trainingCounts The method expects the parameter to be
         *        an array with a size equal to the number of
         *        target attribute values.  Each position in
         *        the array is filled with a corresponding count
         *        of the number of training examples that fall into
         *        that particular target class, at the current
         *        position in the tree.  This parameter can be null
         *        if training count data is not required.
         *
         * @param testingCounts The method expects the parameter to be
         *        an array with a size equal to the number of
         *        target attribute values.  Each position in the
         *        array is filled with a corresponding count of
         *        the number of testing examples that fall into
         *        that particular target class, at the current
         *        position in the tree.  This parameter can be null
         *        if testing count data is not required.
         *
         * @param examples The method expects the parameter to be
         *        an array with a size equal to the number of
         *        training examples in the dataset.  Each entry in
         *        the array is marked with true or false, depending
         *        on whether or not a particular example reaches
         *        the current position.
         *
         * @return DATASET_MIXED_CONCL if the examples have
         *         multiple, different target attribute values.
         *         DATASET_IDENT_CONCL if all the exmamples have
         *         the same target attribute value.
         *         DATASET_EMPTY if there are no examples in the
         *         current example set.
         *
         *         <p>
         *         If the result is DATASET_IDENT_CONCL, the
         *         index of the single target attribute value
         *         is returned in <code>conclusion[0]</code>.  If
         *         the result is DATASET_EMPTY, the index of the
         *         most common target attribute value is returned
         *         in <code>conclusion[0]</code>.
         */
        public int classifyExamples(AttributeMask mask,
                            int[] conclusion,
                            int[] trainingCounts,
                            int[] testingCounts,
                            bool[] examples)
        {
            if (mask == null || conclusion == null)
                throw new Exception("Mask or conclusion array is null.");

            // Determine the number of target attribute values
            // and create some storage space for our counts.
            int[] currTrainingCounts = null;
            int[] currTestingCounts = null;

            if (trainingCounts != null)
                currTrainingCounts = trainingCounts;
            else
                currTrainingCounts = new
                  int[DatasetUse.getTargetAttribute().getNumValues()];

            if (testingCounts != null)
                currTestingCounts = testingCounts;
            else
                currTestingCounts = new int[currTrainingCounts.Length];

            getExampleCounts(mask, DatasetUse.getTrainingExamples(), currTrainingCounts, examples);
            getExampleCounts(mask, DatasetUse.getTestingExamples(), currTestingCounts, null);

            // Init results.
            conclusion[0] = 0;   // Training target attribute value index.

            conclusion[1] = 0;   // Total number of training examples
            // reaching node.
            conclusion[2] = 0;   // Number of training examples correctly
            // classified if this node were a leaf
            // with most common training target value.
            conclusion[3] = 0;   // Number of testing examples correctly
            // classified if this node were a leaf
            // with most common training target value.

            conclusion[4] = 0;   // Testing target attribute value index.

            conclusion[5] = 0;   // Total number of testing examples
            // reaching node.
            conclusion[6] = 0;   // Number of testing examples correctly
            // classified if this node were a leaf
            // with most common testing target value.
            conclusion[7] = 0;   // Number of training examples correctly
            // classified if this node were a leaf
            // with most common testing target value.

            // Examine the results and determine the conclusion.
            int result = DATASET_EMPTY;

            for (int i = 0; i < currTrainingCounts.Length; i++)
            {
                // Increment # of examples that reach this position.
                conclusion[1] += currTrainingCounts[i];
                conclusion[5] += currTestingCounts[i];

                if (result == DATASET_EMPTY && currTrainingCounts[i] != 0)
                    result = DATASET_IDENT_CONCL;
                else if (result == DATASET_IDENT_CONCL && currTrainingCounts[i] != 0)
                    result = DATASET_MIXED_CONCL;

                if (currTrainingCounts[i] >= currTrainingCounts[conclusion[0]])
                {
                    // This target value is more common in the training set.
                    conclusion[0] = i;
                    conclusion[2] = currTrainingCounts[i];
                    conclusion[3] = currTestingCounts[i];
                }

                if (currTestingCounts[i] >= currTestingCounts[conclusion[4]])
                {
                    // This target value is more common in the testing set.
                    conclusion[4] = i;
                    conclusion[6] = currTestingCounts[i];
                    conclusion[7] = currTrainingCounts[i];
                }
            }
            return result;
        }

        /**
           * Computes entropy based on classification counts stored
           * in the stats array.
           *
           * @param stats An array of integers, where each value
           *        indicates the number of examples that fall into
           *        a particular target category.  The size of the
           *        array should be the same as the number of possible
           *        target attribute values.
           *
           * @param numExamples The total number of examples (sum of
           *        all the counts in the stats array).
           *
           * @return The entropy as a double value.
           */
        private double entropy(int[] stats, int numExamples)
        {
            double entropy = 0;

            if (numExamples == 0) return 0;

            for (int i = 0; i < stats.Length; i++)
            {
                if (stats[i] == 0) continue;

                // Unfortunately, the Java math class only
                // supports log{base e}...
                entropy -= ((double)stats[i]) / numExamples *
                  (Math.Log(((double)stats[i]) / numExamples)) / Math.Log(2.0);
            }

            return entropy;
        }
        /**
         * Returns an error bar value based on the current
         * confidence interval.
         *
         * @param mean The mean value used to calculate the error
         *        bar value.
         *
         * @param size The sample size.
         */
        private double errorBar(double mean, int size)
        {
            return Math.Sqrt(mean * (1 - mean) / size) * PessPruneZScore;
        }
        /**
         * Reduced error pruing error function.
         *
         * @param numExamplesReachParent The number of examples
         *        that reach the parent of a given position in the tree.
         *
         * @param numExamplesIncorrectClass The number of examples
         *        <i>in</i>correctly classified at the given position in
         *        the tree (this has meaning only for leaf nodes).
         */
        private double errorRE(int numExamplesReachParent,
                                int numExamplesIncorrectClass)
        {
            // Include Laplacian correction.
            int total = 1 + numExamplesIncorrectClass;

            return ((double)total) / (numExamplesReachParent + 2);
        }
        /**
          * Generates statistics (used for splitting) based on the
          * current position in the tree (as defined by an
          * attribute mask).
          *
          * @return A Vector that contains the available attributes.
          *         Each attribute's internal statistics array is
          *         populated with appropriate data.  The supplied
          *         stats array is filled with counts of the number of
          *         examples that fall into each of the target classes
          *         at the current position in the tree.
          */
        public List<Attribute> generateStats(AttributeMask mask, int[] stats)
        {
            // First, we fill the stats array - this is not the
            // most efficient approach, since we're looping through
            // the data several times.
            getExampleCounts(mask, DatasetUse.getTrainingExamples(), stats, null);

            // Now, we have to go through the attribute mask
            // and locate the attributes that are still available.
            List<Attribute> results = new List<Attribute>();

            // Create a new mask that we can modify.
            AttributeMask newMask = new AttributeMask(mask);

            // We don't use position 0, that's where the target attribute is.
            for (int i = 1; i < mask.getNumAttributes(); i++)
            {
                if (newMask.isMasked(i) == AttributeMask.UNUSED)
                {
                    // This attribute is available, so we calculate stats for it.
                    Attribute att = null;

                    try
                    {
                        att = DatasetUse.getAttributeByNum(i);
                    }
                    catch (Exception e)
                    {
                        // This can't happen!!          
                        return null;
                    }

                    int[][] attStats = att.getStatsArray();

                    // Modify the mask and fill in the arrays.
                    for (int j = 0; j < att.getNumValues(); j++)
                    {
                        newMask.mask(i, j);
                        getExampleCounts(newMask, DatasetUse.getTrainingExamples(), attStats[j], null);
                    }

                    // Reset the mask.
                    newMask.unmask(i);
                    results.Add(att);
                }
            }

            return results;
        }
        /**
          * Computes the information gain, which is the
          * &quot;expected reduction in entropy caused by
          * paritioning the examples according to [a certain]
          * attribute&quot;.
          *
          * @param stats An array of integers, where each value
          *        indicates the number of examples that fall into
          *        a particular target category.  The size of the
          *        array should be the same as the number of
          *        possible target attribute values.  This array
          *        holds the statistics associated with the number
          *        of examples that reach a certain node/arc in the
          *        tree.
          *
          * @param att An Attribute that can be split on at the
          *        current position in the tree.  The Attribute's
          *        internal storage space should already be
          *        populated with correct statistical information.
          *
          * @return The information gain value for the supplied
          *         attribute.
          */
        private double getGain(int[] stats, Attribute att)
        {
            // First, we calculate the entropy of the set
            // of examples before splitting (using the counts
            // in the stats array).
            int numExamples = 0;

            for (int i = 0; i < stats.Length; i++)
            {
                numExamples += stats[i];
            }

            if (numExamples == 0) return 0;

            double originalEntropy = entropy(stats, numExamples);

            // Now, we determine the entropy after splitting.
            double splitEntropy = 0;
            int[][] attStats = att.getStatsArray();

            // Loop over all possible values.
            for (int j = 0; j < attStats.Length; j++)
            {
                int numSubsetExamples = 0;

                // Determine number of examples along this path.
                for (int k = 0; k < attStats[j].Length; k++)
                {
                    numSubsetExamples += attStats[j][k];
                }
                splitEntropy +=
                  ((double)numSubsetExamples) / numExamples *
                  entropy(attStats[j], numSubsetExamples);
            }

            return originalEntropy - splitEntropy;
        }
        /**
         * Computes the gain ratio for a particular attribute.
         * The gain ratio calculation includes a term called
         * 'split information' that penalizes attributes that
         * split data broadly and uniformly.
         *
         * @param stats An array of integers, where each value
         *        indicates the number of examples that fall into
         *        a particular target category.  The size of the
         *        array should be the same as the number of
         *        possible target attribute values.  This array
         *        holds the statistics associated with the number
         *        of examples that reach a certain node/arc in the
         *        tree.
         *
         * @param att An Attribute that can be split on at the
         *        current position in the tree.  The Attribute's
         *        internal storage space should already be
         *        populated with correct statistical information.
         *
         * @return The gain ratio for the supplied attribute.
         */
        private double getGainRatio(int[] stats, Attribute att)
        {
            // We recompute some of the same quantities
            // calculated in the gain method here - it would
            // be more efficient to merge the gain calculation
            // here.
            int numExamples = 0;

            for (int i = 0; i < stats.Length; i++)
            {
                numExamples += stats[i];
            }

            if (numExamples == 0) return 0;

            // Compute the gain.
            double gain = getGain(stats, att);

            // Compute the SplitInformation term.
            // (which is the entropy of the examples with
            // respect to the attribute values of att).
            int[] splitInfoStats = new int[att.getNumValues()];

            int[][] attStats = att.getStatsArray();

            // Loop over all possible values.
            for (int j = 0; j < attStats.Length; j++)
            {
                // Determine number of examples along this path.
                for (int k = 0; k < attStats[j].Length; k++)
                {
                    splitInfoStats[j] += attStats[j][k];
                }
            }

            double splitInfo = entropy(splitInfoStats, numExamples);
            return gain / splitInfo;
        }
        /**
         * Computes the GINI score for a particular attribute.
         *
         * @param stats An array of integers, where each value
         *        indicates the number of examples that fall into
         *        a particular target category.  The size of the
         *        array should be the same as the number of
         *        possible target attribute values.  This array
         *        holds the statistics associated with the number
         *        of examples that reach a certain node/arc in the
         *        tree.
         *
         * @param att An Attribute that can be split on at the
         *        current position in the tree.  The Attribute's
         *        internal storage space should already be
         *        populated with correct statistical information.
         *
         * @return the GINI score for the supplied attribute.
         */
        private double getGINI(int[] stats, Attribute att)
        {
            // Determine the total number of examples.
            int numExamples = 0;

            for (int i = 0; i < stats.Length; i++)
            {
                numExamples += stats[i];
            }

            if (numExamples == 0) return 0;

            double giniScore = 0;
            int[][] attStats = att.getStatsArray();

            // Loop over all possible values.
            for (int j = 0; j < attStats.Length; j++)
            {
                int numSubsetExamples = 0;
                int sumOfSquares = 0;

                // Determine number of examples along this path.
                for (int k = 0; k < attStats[j].Length; k++)
                {
                    sumOfSquares += attStats[j][k] * attStats[j][k];
                    numSubsetExamples += attStats[j][k];
                }

                if (numSubsetExamples != 0)
                {
                    giniScore += ((double)sumOfSquares) / numSubsetExamples;
                }
            }

            // Now, compute the second term in the GINI score.
            for (int l = 0; l < stats.Length; l++)
            {
                giniScore -= ((double)stats[l]) * ((double)stats[l]) / numExamples;
            }
            // Finally, divide by the total number of examples.
            giniScore /= numExamples;
            return giniScore;
        }
        /**
         * Returns a reference to the dataset that the algorithm
         * is currently using.
         *
         * @return A reference to the current dataset.
         */
        public Dataset getDataset()
        {
            return DatasetUse;
        }
        /**
           * Fills the supplied array with the number of examples
           * from the current dataset that fall into each of the
           * target categories (based on the attribute mask).
           *
           * @param mask The mask that determines which examples
           *        reach the current position in the decision
           *        tree.
           *
           * @param examples An iteration over a series of
           *        examples from the current dataset.
           *
           * @param counts The method expects the parameter to be
           *        an array with a size equal to the number of
           *        target attribute values.  Each position in
           *        the array is filled with a corresponding count
           *        of the number of training examples that fall into
           *        that particular target class, at the current
           *        position in the decision tree.
           *
           * @param reachedHere The method expects the parameter
           *        to be an array with a size equal to the
           *        <i>total</i> number of examples being examined.
           *        Each cell in the array is set to true or
           *        false, depending on whether or not the
           *        corresponding example reaches the current
           *        position in the decision tree.
           */
        private void getExampleCounts(AttributeMask mask, IEnumerator<int[]> examples, int[] counts, bool[] reachedHere)
        {
            // Zero any values currently in stats.
            for (int i = 0; i < counts.Length; i++)
            {
                counts[i] = 0;
            }

            int j = 0;

            // Loop through and get totals.
            while (examples.MoveNext())
            {
                int[] example = (int[])examples.Current;

                if (mask.matchMask(example))
                {
                    counts[example[0]]++;   // Increment appropriate
                    // target count.
                    if (reachedHere != null && reachedHere.Length > j)
                        reachedHere[j] = true;
                }

                j++;
            }
        }
        /**
          * Returns the current pessimistic pruning Z-score.
          *
          * @return The current pessimistic pruning Z-score.
          */
        public double getPessimisticPruningZScore()
        {
            return PessPruneZScore;
        }
        /**
          * Returns the current pruning algorithm.
          *
          * @return The current pruning algorithm as a String.
          */
        public String getPruningAlgorithm()
        {
            return PruneAlg;
        }
        /**
          * Returns the current splitting function.
          *
          * @return The current splitting function as a String.
          */
        public String getSplittingFunction()
        {
            return SplitFun;
        }
        /**
          * Returns a reference to the decision tree data structure.
          */
        public DecisionTree getTree()
        {
            return Tree;
        }

        /**
         * An implementation of the recursive decision tree
         * learning algorithm.  Given a parent node and an arc
         * number, the method will attach a new decision 'sub'-tree
         * below the parent node.
         *
         * @param parent The parent node for the new decision tree.
         *
         * @param arcNum The arc number (or path) along which the
         *        new subtree will be attached.
         *
         * @return true if an entire subtree was successfully added,
         *         false otherwise.
         */
        public bool learnDT(DecisionTreeNode parent, int arcNum)
        {
            AttributeMask mask;

            if (parent == null)
            {
                // We have to add at the root.
                mask = new AttributeMask(DatasetUse.getNumAttributes());
            }
            else
            {
                mask = new AttributeMask(parent.getMask());

                // Mask off the specified arc number.
                try
                {
                    mask.mask(DatasetUse.getAttributePosition(parent.getLabel()), arcNum);
                }
                catch (Exception e)
                {
                    //e.printStackTrace();
                    return false;
                }
            }

            // Now, classify the examples at the current position.
            int[] conclusion = new int[8];
            int result = classifyExamples(mask, conclusion, null, null, null);

            Attribute target = DatasetUse.getTargetAttribute();
            int numTargetVals = target.getNumValues();
            String label;

            if (result == DATASET_EMPTY)
            {
                // If no examples reach our current position
                // we add a leaf with the most common target
                // classfication for the parent node.

                // Save testing results.
                int numTestingExamplesReachHere = conclusion[5];
                int bestTestingTargetIndex = conclusion[4];
                int numTestingExamplesCorrectClass = conclusion[6];
                int numTrainingExamplesCorrectClass = conclusion[7];

                classifyExamples(parent.getMask(), conclusion, null, null, null);

                try
                {
                    label = target.getAttributeValueByNum(conclusion[0]);
                }
                catch (Exception e)
                {
                    return false;
                }

                // We have to grab the counts again for the testing data...
                int[] currTestingCounts = new int[target.getNumValues()];
                getExampleCounts(mask, DatasetUse.getTestingExamples(), currTestingCounts, null);

                // Mask target value and add a leaf to the tree.
                mask.mask(0, conclusion[0]);

                DecisionTreeNode node = Tree.addLeafNode(parent,
                                      arcNum,
                                      label,
                                      mask,
                                      0,
                                      conclusion[0],
                                      0,
                                      currTestingCounts[conclusion[0]],
                                      numTestingExamplesReachHere,
                                      bestTestingTargetIndex,
                                      numTestingExamplesCorrectClass,
                                      numTrainingExamplesCorrectClass);

                return true;
            }

            if (result == DATASET_IDENT_CONCL)
            {
                // Pure result - we can add a leaf node with the
                // correct target attribute value.
                try
                {
                    label = target.getAttributeValueByNum(conclusion[0]);
                }
                catch (Exception e)
                {
                    //e.printStackTrace();
                    return false;
                }

                // Mask target value and add a leaf to the tree.
                mask.mask(0, conclusion[0]);

                DecisionTreeNode node = Tree.addLeafNode(parent,
                                      arcNum,
                                      label,
                                      mask,
                                      conclusion[1],
                                      conclusion[0],
                                      conclusion[2],
                                      conclusion[3],
                                      conclusion[5],
                                      conclusion[4],
                                      conclusion[6],
                                      conclusion[7]);

                return true;
            }

            // Mixed conclusion - so we have to select
            // an attribute to split on, and then build a
            // new internal node with that attribute.

            // First, generate statistics - this may take awhile.
            int[] nodeStats = new int[numTargetVals];
            List<Attribute> availableAtts = generateStats(mask, nodeStats);

            if (availableAtts.Count == 0)
            {
                // No attributes left to split on - so use
                // the most common target value at the current position.
                try
                {
                    label = target.getAttributeValueByNum(conclusion[0]);
                }
                catch (Exception e)
                {
                    //e.printStackTrace();
                    return false;
                }

                mask.mask(0, conclusion[0]);

                DecisionTreeNode node = Tree.addLeafNode(parent,
                                      arcNum,
                                      label,
                                      mask,
                                      conclusion[1],
                                      conclusion[0],
                                      conclusion[2],
                                      conclusion[3],
                                      conclusion[5],
                                      conclusion[4],
                                      conclusion[6],
                                      conclusion[7]);

                return true;
            }

            // Choose an attribute, based on the set of
            // available attributes.
            List<double> results = new List<double>();
            Attribute att = chooseAttribute(availableAtts, nodeStats, results);

            int attPos;

            try
            {
                attPos = DatasetUse.getAttributePosition(att.getName());
            }
            catch (Exception e)
            {
                //e.printStackTrace();
                return false;
            }

            DecisionTreeNode newParent = Tree.addInternalNode(parent,
                                      arcNum,
                                      attPos,
                                      att,
                                      mask,
                                      conclusion[1],
                                      conclusion[0],
                                      conclusion[2],
                                      conclusion[3],
                                      conclusion[5],
                                      conclusion[4],
                                      conclusion[6],
                                      conclusion[7]);

            // Now, recursively decend along each branch of the new node.
            for (int j = 0; j < newParent.getArcLabelCount(); j++)
            {
                // Recursive call.
                if (!learnDT(newParent, j)) return false;
            }

            return true;
        }
        /**
          * An implementation of the recursive decision tree
          * pessimistic pruning algorithm.  Given a parent
          * node, the method will prune all the branches
          * below the node.
          *
          * @param node The root node of the tree to prune.
          *
          * @param error A <code>double</code> array of size 1. The
          *        array is used to store the current error value.
          *
          * @return <code>true</code> if an entire subtree was successfully
          *         pruned, or <code>false</code> otherwise.
          */
        public bool prunePessimisticDT(DecisionTreeNode node, double[] error)
        {
            // Post-order walk through the tree, marking
            // our path as we go along.
            if (node.isLeaf())
            {
                if (node.getTrainingEgsAtNode() == 0)
                {
                    error[0] = 0;
                    return true;
                }
                else
                {
                    // We do the error calculation in two steps -
                    // Here we multiply the error value by the number
                    // of examples that reach the node.  When the method
                    // is called recursively, this value will be divided
                    // by the number of examples that reach the parent
                    // node (thus weighting the error from each child).
                    int errors1 = (int)node.getTrainingEgsAtNode() - node.getTrainingEgsCorrectClassUsingBestTrainingIndex();
                    double p1 = (double)(errors1 + 1.0) / (node.getTrainingEgsAtNode() + 2.0);

                    error[0] = node.getTrainingEgsAtNode() * errorBar(p1, node.getTrainingEgsAtNode()) + errors1;

                    return true;
                }
            }

            // We're at an internal node, so compute the error
            // of the children and use the result to determine
            // if we prune or not.
            double errorSum = 0;

            for (int i = 0; i < node.getArcLabelCount(); i++)
            {
                // Mark our current path.
                Tree.flagNode(node, i);

                if (!prunePessimisticDT(node.getChild(i), error))
                {
                    Tree.flagNode(node, -2);
                    return false;
                }

                errorSum += error[0];
            }

            // Mark the node as our current target.
            Tree.flagNode(node, -1);

            // Get the worst-case performance of this node.
            double errorWorst;

            if (node.getTrainingEgsAtNode() == 0)
            {
                error[0] = 0;
                return true;
            }

            int errors = (int)node.getTrainingEgsAtNode() - node.getTrainingEgsCorrectClassUsingBestTrainingIndex();
            double p = (double)(errors + 1.0) / (node.getTrainingEgsAtNode() + 2.0);

            errorWorst = (double)node.getTrainingEgsAtNode() * errorBar(p, node.getTrainingEgsAtNode()) + errors;

            DecisionTreeNode newNode = node;

            if (errorWorst < errorSum)
            {
                // We need to "prune" this node to a leaf.
                DecisionTreeNode parent = node.getParent();
                int arcNum = -1;

                if (parent != null)
                {
                    arcNum = parent.getChildPosition(node);
                }

                Tree.pruneSubtree(node);

                // Figure out the label for the new leaf.
                String label = null;

                try
                {
                    label = DatasetUse.getTargetAttribute().getAttributeValueByNum(node.getTrainingBestTarget());
                }
                catch (Exception e)
                {
                    // Should never happen.
                    //e.printStackTrace();
                }

                node.getMask().mask(0, node.getTrainingBestTarget());

                newNode =
                  Tree.addLeafNode(parent, arcNum, label,
                    node.getMask(),
                    node.getTrainingEgsAtNode(),
                    node.getTrainingBestTarget(),
                    node.getTrainingEgsCorrectClassUsingBestTrainingIndex(),
                    node.getTestingEgsCorrectClassUsingBestTrainingIndex(),
                    node.getTestingEgsAtNode(),
                    node.getTestingBestTarget(),
                    node.getTestingEgsCorrectClassUsingBestTestingIndex(),
                    node.getTrainingEgsCorrectClassUsingBestTestingIndex());
            }

            // Update the count.
            if (newNode.isLeaf())
            {
                error[0] = errorWorst;
            }
            else
            {
                error[0] = errorSum;
            }

            // All finished, unmark the node if it still exists.
            Tree.flagNode(node, -2);

            return true;
        }

        /**
         * An implementation of the recursive decision tree
         * reduced error pruning algorithm.  Given a parent
         * node, the method will prune all the branches
         * below the node.
         *
         * @param node The root node of the tree to prune.
         *
         * @param error A <code>double</code> array of size 1. The
         *        array is used to store the current error value.
         *
         * @return <code>true</code> if an entire subtree was successfully
         *         pruned, or <code>false</code> otherwise.
         */
        public bool pruneReducedErrorDT(DecisionTreeNode node, double[] error)
        {
            if (node.isLeaf())
            {
                error[0] = node.getTestingEgsAtNode() - node.getTestingEgsCorrectClassUsingBestTrainingIndex();
                return true;
            }

            // We're at an internal node, so compute the error
            // of the children and use the result to determine
            // if we prune or not.
            double errorSum = 0;

            for (int i = 0; i < node.getArcLabelCount(); i++)
            {
                // Mark our current path.
                Tree.flagNode(node, i);

                if (!pruneReducedErrorDT(node.getChild(i), error))
                {
                    Tree.flagNode(node, -2);
                    return false;
                }

                errorSum += error[0];
            }

            // Mark the node as our current target.
            Tree.flagNode(node, -1);

            // Get the best-case performance of this node.
            double errorBest = node.getTestingEgsAtNode() - node.getTestingEgsCorrectClassUsingBestTestingIndex();

            DecisionTreeNode newNode = node;

            if (errorBest < errorSum)
            {
                // We need to "prune" this node to a leaf.
                DecisionTreeNode parent = node.getParent();
                int arcNum = -1;

                if (parent != null)
                {
                    arcNum = parent.getChildPosition(node);
                }

                Tree.pruneSubtree(node);

                // Figure out the label for the new leaf.
                String label = null;

                try
                {
                    label = DatasetUse.getTargetAttribute().getAttributeValueByNum(node.getTestingBestTarget());
                }
                catch (Exception e)
                {
                    // Should never happen.
                    //e.printStackTrace();
                }

                node.getMask().mask(0, node.getTestingBestTarget());

                newNode = Tree.addLeafNode(parent, arcNum, label,
                    node.getMask(),
                    node.getTrainingEgsAtNode(),
                    node.getTestingBestTarget(),
                    node.getTrainingEgsCorrectClassUsingBestTestingIndex(),
                    node.getTestingEgsCorrectClassUsingBestTestingIndex(),
                    node.getTestingEgsAtNode(),
                    node.getTestingBestTarget(),
                    node.getTestingEgsCorrectClassUsingBestTestingIndex(),
                    node.getTrainingEgsCorrectClassUsingBestTestingIndex());
            }

            // Update the count.
            if (newNode.isLeaf())
            {
                error[0] = errorBest;
            }
            else
            {
                error[0] = errorSum;
            }

            // All finished, unmark the node if it still exists.
            Tree.flagNode(node, -2);

            return true;
        }
        /**
          * Resets the algorithm, destroying the current
          * tree.  The dataset used to build the tree
          * remains unchanged.
          */
        public void reset()
        {
            Tree = new DecisionTree();
        }
        /**
         * Runs the complete decision tree algorithm.
         * The algorithm starts with the current state of
         * the tree, and builds from there.
         *
         * <p>
         * This method allows the algorithm to be run in a
         * separate thread.
         */
        public void BuildDTTree()
        {
            DecisionTreeNode parent;
            int[] arcNum = new int[1];

            // Build the tree, looping until it's complete.
            while (true)
            {
                // Determine where to start building
                // (leftmost available position).
                if (Tree.isEmpty())
                {
                    parent = null;
                }
                else if ((parent = Tree.findIncompleteNode(Tree.getRoot(), arcNum)) == null)
                {
                    break;
                }
                // Build the tree - the call to learnDT()
                // will fill in everything below the current branch.
                if (!learnDT(parent, arcNum[0])) break;
            }

            // Now check - if the tree is complete, we can start the
            // pruning process.  Otherwise, we've stopped with some
            // nodes still missing.
            if (!Tree.isComplete())
            {
                return;
            }


            //------------------ PruneDT ------------------
            if (PruneAlg.Equals(PRUNING_REDUCED_ERROR))
            {
                // pruneReducedErrorDT will return false if it's
                // interrupted and is unable to finish pruning the tree.
                if (!pruneReducedErrorDT(Tree.getRoot(), new double[1]))
                {
                    return;
                }
            }
            else if (PruneAlg.Equals(PRUNING_PESSIMISTIC))
            {
                // prunePessimisticDT will return false if it's
                // interrupted and is unable to finish pruning the tree.
                if (!prunePessimisticDT(Tree.getRoot(), new double[1]))
                {
                    return;
                }
            }
            if (!Tree.isEmpty())
            {
                CurrentRoot = Tree.Nodes[0];
            }
        }
        public void ExtractRules()
        {
            ExtractRulesFromDTTree(this.Tree.Nodes[0]);

        }

        public void ExtractRulesFromDTTree(DecisionTreeNode currentRoot)
        {
            if (currentRoot.isLeaf())
            {
                BackTrackRule(currentRoot);
            }
            else 
            {
                DecisionTreeNode temp = currentRoot;
                for (int i = 0; i < temp.Children.Length; i++)
                {
                    currentRoot = temp.Children[i];
                    ExtractRulesFromDTTree(currentRoot);
                }
            }
        }

        private void BackTrackRule(DecisionTreeNode currentNode)
        {
            string rule;
            string[] Temp = {"IF", "AND","THEN"};

            DecisionTreeNode tempParentNode = currentNode;
            DecisionTreeNode tempCurrentNode = currentNode;

            // Xây dựng luật
            rule = Temp[2] + " \"" + DatasetUse.Attributes[0].Name + "\" = " + "\'"+ currentNode.NodeLabel +"\'"; 

            while (tempCurrentNode.Parent != null)
            {
                tempParentNode = tempCurrentNode.Parent;
                
                int i;
                for( i = 0 ; i < tempParentNode.Children.Length; i++)
                {
                    if(tempParentNode.Children[i].NodeLabel == tempCurrentNode.NodeLabel)
                    {
                        break;
                    }
                }

                rule = Temp[1] + " \"" + tempParentNode.NodeLabel 
                    + "\" = \'" + tempParentNode.ArcLabels[i] + "\' " + rule;

                tempCurrentNode = tempParentNode;
            }            
            rule = rule.Substring(rule.IndexOf(Temp[1]) + Temp[1].Length);
            rule = Temp[0] + rule;


            // Thêm một luật mới vào danh sách các luật rút ra
            ListRules.Add(rule);
        }
        #endregion
    }
}
