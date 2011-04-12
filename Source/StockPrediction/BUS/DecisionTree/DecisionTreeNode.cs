using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecisionTree
{
    public class DecisionTreeNode
    {
        #region Attributes
        private String _nodeLabel;             // Attribute or value for this node.
        private DecisionTreeNode _parent;      // Parent of this node.
        private Object[] _arcLabels;           // Attribute value names.
        private DecisionTreeNode[] _children;  // Children of this node.
        private AttributeMask _attMask;        // Mask of attributes that lie on the path to this node.

        private int _nodeFlag;                 // Multi-purpose node flag.
        private int _trainEgsReachHere;        // Number of training examples
        // that reach node.
        private int _testEgsReachHere;         // Number of testing examples
        // that reach node.

        private int _trainBestTargetIndex;     // Target value index for *best*
        // (most common) training value.
        private int _testBestTargetIndex;      // Target value index for *best*
        // (most common) testing value.

        private int _trainTrainingCorrectClass;    // Number of training examples
        // correctly classified, if
        // this were a leaf node w/ most
        // common training target value.
        private int _trainTestingCorrectClass;     // Number of testing examples
        // correctly classified, if
        // this were a leaf node w/ most
        // common training target value.
        private int _testTrainingCorrectClass;     // Number of training examples
        // correctly classified, if
        // this were a leaf node w/ most
        // common testing target value.
        private int _testTestingCorrectClass;      // Number of testing examples
        // correctly classified, if
        // this were a leaf node w/ most
        // common testing target value.
        #endregion

        #region Properties
        public String NodeLabel
        {
            get { return _nodeLabel; }
            set { _nodeLabel = value; }
        }
        public DecisionTreeNode Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }
        public Object[] ArcLabels
        {
            get { return _arcLabels; }
            set { _arcLabels = value; }
        }
        public DecisionTreeNode[] Children
        {
            get { return _children; }
            set { _children = value; }
        }
        public AttributeMask AttMask
        {
            get { return _attMask; }
            set { _attMask = value; }
        }

        public int NodeFlag
        {
            get { return _nodeFlag; }
            set { _nodeFlag = value; }
        }
        public int TrainEgsReachHere
        {
            get { return _trainEgsReachHere; }
            set { _trainEgsReachHere = value; }
        }
        public int TestEgsReachHere
        {
            get { return _testEgsReachHere; }
            set { _testEgsReachHere = value; }
        }
        public int TrainBestTargetIndex
        {
            get { return _trainBestTargetIndex; }
            set { _trainBestTargetIndex = value; }
        }
        public int TestBestTargetIndex
        {
            get { return _testBestTargetIndex; }
            set { _testBestTargetIndex = value; }
        }
        public int TrainTrainingCorrectClass
        {
            get { return _trainTrainingCorrectClass; }
            set { _trainTrainingCorrectClass = value; }
        }
        public int TrainTestingCorrectClass
        {
            get { return _trainTestingCorrectClass; }
            set { _trainTestingCorrectClass = value; }
        }
        public int TestTrainingCorrectClass
        {
            get { return _testTrainingCorrectClass; }
            set { _testTrainingCorrectClass = value; }
        }
        public int TestTestingCorrectClass
        {
            get { return _testTestingCorrectClass; }
            set { _testTestingCorrectClass = value; }
        }
        #endregion

        #region Constructors
        public DecisionTreeNode(DecisionTreeNode parent,
                           String label,
                           Object[] arcLabels,
                           AttributeMask mask)
        {
            if (label == null || mask == null)
                throw new Exception("Label or attribute mask is null.");

            Parent = parent;
            NodeLabel = label;
            ArcLabels = arcLabels;
            AttMask = mask;

            // The number of possible children for the node is
            // equal to the number of values for this attribute.

            // If the arc label array was null, then this is
            // a leaf node that has no children.
            if (arcLabels != null)
                Children = new DecisionTreeNode[ArcLabels.Length];

            // The node is initially unflagged.
            NodeFlag = -2;
        }
        #endregion

        #region Methods
        /**
       * Returns a particular arc label (attribute value name)
       * for this node.
       *
       * @return The arc label for the specified arc.  If this
       *         node is a leaf node, the method returns null.
       *
       * @throws IndexOutOfBoundsException If the supplied
       *         label number is less than 0 or greater than
       *         the number of arc labels (attribute values)
       *         at this node.
       */
        public String getArcLabel(int labelNum)
        {
            if (isLeaf()) return null;

            if (labelNum < 0 || labelNum > (ArcLabels.Length - 1))
                throw new Exception("No arc label exists at position " + labelNum + ".");

            return (String)ArcLabels[labelNum];
        }
        /**
       * Returns the number of arc labels (attribute values) for this node.
       *
       * @return The number of arc labels (attribute values)
       *         or paths leaving this node (descending to
       *         subtrees).  If this is a leaf node, the method
       *         returns 0.
       */
        public int getArcLabelCount()
        {
            if (isLeaf()) return 0;

            return ArcLabels.Length;
        }
        /**
         * Returns a particular child node (subtree).
         *
         * @return The subtree rooted along the specified
         *         path.  This value can be null, if no
         *         subtree currently exists along an arc, or
         *         if this node is a leaf node.
         *
         * @throws IndexOutOfBoundsException If the supplied
         *         child number is less than 0 or greater
         *         than the number of children (which is
         *         equivalent to the number of attribute
         *         values) at this node.
         */
        public DecisionTreeNode getChild(int childNum)
        {
            if (this.isLeaf()) return null;

            if (childNum < 0 || childNum > (Children.Length - 1))
                throw new Exception("No child exists at position " + childNum + ".");

            return Children[childNum];
        }
        /**
        * Returns the position of the supplied child beneath
        * the parent node.  If this node is a leaf, or the
        * supplied node is not a child of this node, the method
        * returns -1.
        *
        * @param child The 'potential' child of this node.
        *
        * @return The position of the supplied child beneath
        *         this node, or -1 if this is a leaf node
        *         or the node is not a parent of the
        *         child.
        */
        public int getChildPosition(DecisionTreeNode child)
        {
            if (this.isLeaf()) return -1;

            for (int i = 0; i < Children.Length; i++)
                if (Children[i] == child) return i;

            return -1;
        }

        /**
         * Returns the position of the first missing child
         * for this node (i.e. the first empty arc).
         *
         * @return The position of the first missing child
         *         for this node, or -1 if the node is
         *         already complete or is a leaf node.
         */
        public int getFirstMissingChild()
        {
            if (isLeaf()) return -1;

            for (int i = 0; i < Children.Length; i++)
                if (Children[i] == null) return i;

            return -1;
        }
        /**
        * Returns the current node flag value or a number less
        * than zero if the node is unflagged. The number '-1'
        * is reserved as a special flag value.
        *
        * @return The current node flag value, or a number < 0
        *         if the node is unflagged.
        */
        public int getFlag()
        {
            return NodeFlag;
        }
        /**
         * Returns the label (name) for the attribute or value at this node.
         *
         * @return The label String for this node.
         */
        public String getLabel()
        {
            return NodeLabel;
        }

        /**
         * Returns a copy of the attribute mask for this node.
         *
         * @return The attribute mask for this node, indicating
         *         the attributes used along the path through the
         *         tree that reaches this position.
         */
        public AttributeMask getMask()
        {
            return new AttributeMask(AttMask);
        }
        /**
         * Returns the parent of this node.
         *
         * @return The parent node of this node, or null
         *         if this node has no parent (i.e. is the root
         *         node).
         */
        public DecisionTreeNode getParent()
        {
            return Parent;
        }
        /**
         * Returns the best (most common) testing set
         * target attribute value index.
         */
        public int getTestingBestTarget()
        {
            return TestBestTargetIndex;
        }
        /**
         * Returns the number of testing examples that reach
         * the node.
         *
         * @return The number of examples from the dataset that reach the node.
         */
        public int getTestingEgsAtNode()
        {
            return TestEgsReachHere;
        }
        /**
         * Returns the number of testing examples that would
         * be correctly classified at this node, if it was
         * a leaf node with the most common testing target
         * attribute value.
         */
        public int getTestingEgsCorrectClassUsingBestTestingIndex()
        {
            return TestTestingCorrectClass;
        }
        /**
         * Returns the number of testing examples that would
         * be correctly classified at this node, if it was
         * a leaf node with the most common training target
         * attribute value.
         */
        public int getTestingEgsCorrectClassUsingBestTrainingIndex()
        {
            return TrainTestingCorrectClass;
        }
        /**
         * Returns the best (most common) training set
         * target attribute value index.
         */
        public int getTrainingBestTarget()
        {
            return TrainBestTargetIndex;
        }
        /**
         * Returns the number of training examples that reach
         * the node.
         *
         * @return The number of examples from the dataset
         *         that reach the node.
         */
        public int getTrainingEgsAtNode()
        {
            return TrainEgsReachHere;
        }
        /**
         * Returns the number of training examples that would
         * be correctly classified at this node, if it was
         * a leaf node with the most common testing target
         * attribute value.
         */
        public int getTrainingEgsCorrectClassUsingBestTestingIndex()
        {
            return TestTrainingCorrectClass;
        }
        /**
         * Returns the number of training examples that would
         * be correctly classified at this node, if it was
         * a leaf node with the most common training target
         * attribute value.
         */
        public int getTrainingEgsCorrectClassUsingBestTrainingIndex()
        {
            return TrainTrainingCorrectClass;
        }
        /**
         * Identifies if a node is a leaf node or an internal node.
         *
         * @return true if the node is a leaf node, or false otherwise.
         */
        public bool isLeaf()
        {
            return ArcLabels == null;
        }
        /**
         * Searches through the current node's list of children
         * and attempts to locate a child that matches the
         * supplied child.  If a match is found, the reference
         * to the child is removed, leaving a vacant arc.
         */
        public void removeChild(DecisionTreeNode child)
        {
            // Search through the list of children, looking for a match.
            for (int i = 0; i < Children.Length; i++)
            {
                if (Children[i] == child)
                {
                    Children[i] = null;
                    return;
                }
            }
        }
        /**
         * Attaches the supplied node at specified child position.
         * The method does not check to see if a node already
         * exists at the insertion position.
         * If this node is a leaf node, the method has no effect.
         *
         * @throws IndexOutOfBoundsException If the supplied
         *         child number is less than 0 or greater
         *         than the number of possible children (which
         *         is equivalent to the number of attribute
         *         values) at this node.
         */
        public void setChild(int childNum, DecisionTreeNode node)
        {
            if (isLeaf()) return;

            if (childNum < 0 || childNum > (Children.Length - 1))
                throw new Exception("Cannot add child at position " + childNum + ".");

            Children[childNum] = node;
        }
        /**
         * Flags or unflags the node.  Usually, an algorithm
         * will set the flag to indicate that the node is
         * currently being considered, modified, or used in
         * some other way.
         *
         * @param flagValue The integer value to set the flag to.
         *        If the number is less than zero, the node is
         *        considered unflagged.
         */
        public void setFlag(int flagValue)
        {
            NodeFlag = flagValue;
        }
        /**
         * Sets the internal testing statistics for the node.
         *
         * @param numTestingExamplesReachHere The number of testing
         *        examples that reach this node.
         *
         * @param bestTestingTargetIndex The index for the best
         *        (most common) target attribute value in the
         *        testing set.
         *
         * @param numTestingExamplesCorrectClass The number of testing
         *        examples that <i>would be</i> correctly
         *        classified, if this node was a leaf node
         *        (with the most common testing target attribute value
         *         in the available examples).
         *
         * @param numTrainingExamplesCorrectClass The number of training
         *        examples that <i>would be</i> correctly
         *        classified, if this node was a leaf node
         *        (with the most common training target attribute value
         *         in the available examples).
         */
        public void setTestingStats(int numTestingExamplesReachHere,
                           int bestTestingTargetIndex,
                           int numTestingExamplesCorrectClass,
                           int numTrainingExamplesCorrectClass)
        {
            TestEgsReachHere = numTestingExamplesReachHere;
            TestBestTargetIndex = bestTestingTargetIndex;
            TestTestingCorrectClass = numTestingExamplesCorrectClass;
            TestTrainingCorrectClass = numTrainingExamplesCorrectClass;
        }
        /**
         * Sets the internal training statistics for the node.
         *
         * @param numTrainingExamplesReachHere The number of training
         *        examples that reach this node.
         *
         * @param bestTrainingTargetIndex The index for the best
         *        (most common) target attribute value in the
         *        training set.
         *
         * @param numTrainingExamplesCorrectClass The number of training
         *        examples that <i>would be</i> correctly
         *        classified, if this node was a leaf node
         *        (with the most common training target attribute value
         *         in the available examples).
         *
         * @param numTestingExamplesCorrectClass The number of testing
         *        examples that <i>would be</i> correctly
         *        classified, if this node was a leaf node
         *        (with the most common training target attribute value
         *         in the available examples).
         */
        public void setTrainingStats(int numTrainingExamplesReachHere,
                            int bestTrainingTargetIndex,
                            int numTrainingExamplesCorrectClass,
                            int numTestingExamplesCorrectClass)
        {
            TrainEgsReachHere = numTrainingExamplesReachHere;
            TrainBestTargetIndex = bestTrainingTargetIndex;
            TrainTrainingCorrectClass = numTrainingExamplesCorrectClass;
            TrainTestingCorrectClass = numTestingExamplesCorrectClass;
        }
        /**
         * A utility method that detaches this node from
         * its parent node.  Once this method is called,
         * the node will no longer be attached to the tree.
         */
        public void detach()
        {
            if (Parent != null)
            {
                Parent.removeChild(this);
            }
        }
        #endregion
    }
}
