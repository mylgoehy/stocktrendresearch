﻿using System;
using System.Collections.Generic;

namespace BUS.DecisionTree
{
    public class DecisionTree
    {
        #region Attribute
        private List<DecisionTreeNode> _nodes;     // Ordered list of tree nodes.
        private int _internalNodes;      // Count of internal nodes.
        private int _trainingCorrect;    // Count of training examples correctly classified so far.
        private int _testingCorrect;     // Count of testing examples correctly classified so far.
        private bool _complete;           // Indicates tree is complete(all branches descend to leaves)
        #endregion

        #region Properties
        public List<DecisionTreeNode> Nodes
        {
            get { return _nodes; }
            set { _nodes = value; }
        }
        public int InternalNodes
        {
            get { return _internalNodes; }
            set { _internalNodes = value; }
        }
        public int TrainingCorrect
        {
            get { return _trainingCorrect; }
            set { _trainingCorrect = value; }
        }
        public int TestingCorrect
        {
            get { return _testingCorrect; }
            set { _testingCorrect = value; }
        }
        public bool Complete
        {
            get { return _complete; }
            set { _complete = value; }
        }
        #endregion

        #region Constructors
        public DecisionTree()
        {
            Nodes = new List<DecisionTreeNode>();
        }
        #endregion

        #region Methods        
        /// <summary>
        /// Attaches a new internal node to the supplied node,
        /// along the specified arc.
        /// </summary>
        /// <param name="parent">The node in the current tree to attach
        /// the internal node to.  If the node is null, the
        /// new internal node becomes the root of the tree.</param>
        /// <param name="arcNum">The arc number (or attribute value
        /// index) along which to attach the new node.</param>
        /// <param name="attributePosition">The position of the
        /// attribute used to split at the new node, relative
        /// to the other attributes in the dataset.</param>
        /// <param name="att">The attribute used to split at the new
        /// node.</param>       
        /// <returns>A reference to the new internal node.</returns>
        public DecisionTreeNode addInternalNode(DecisionTreeNode parent,
            int arcNum,
            int attributePosition,
            Attribute att,
            AttributeMask mask,
            int numTrainingExamplesReachHere,
            int bestTrainingTargetIndex,
            int numTrainingEgsCorrectClassUsingBestTrainingIndex,
            int numTestingEgsCorrectClassUsingBestTrainingIndex,
            int numTestingExamplesReachHere,
            int bestTestingTargetIndex,
            int numTestingEgsCorrectClassUsingBestTestingIndex,
            int numTrainingEgsCorrectClassUsingBestTestingIndex)
        {
            // Create a new internal node.
            DecisionTreeNode internalNode = new DecisionTreeNode(parent, att.getName(), att.getValueNames(), mask);

            // Set the node statistics.
            internalNode.setTrainingStats(numTrainingExamplesReachHere,
                                    bestTrainingTargetIndex,
                                    numTrainingEgsCorrectClassUsingBestTrainingIndex,
                                    numTestingEgsCorrectClassUsingBestTrainingIndex);

            internalNode.setTestingStats(numTestingExamplesReachHere,
                                    bestTestingTargetIndex,
                                    numTestingEgsCorrectClassUsingBestTestingIndex,
                                    numTrainingEgsCorrectClassUsingBestTestingIndex);

            // Update the tree statistics.
            InternalNodes++;

            // Now, attach the new internal node to the supplied node.
            if (parent != null)
                parent.setChild(arcNum, internalNode);

            // Add a reference to the new node to the node list.
            Nodes.Add(internalNode);
           
            return internalNode;
        }        
        /// <summary>
        /// Attaches a new leaf node to the supplied node, along
        /// the specified arc.
        /// </summary>        
        public DecisionTreeNode addLeafNode(DecisionTreeNode parent,
            int arcNum,
            String label,
            AttributeMask mask,
            int numTrainingExamplesReachHere,
            int bestTrainingTargetIndex,
            int numTrainingEgsCorrectClassUsingBestTrainingIndex,
            int numTestingEgsCorrectClassUsingBestTrainingIndex,
            int numTestingExamplesReachHere,
            int bestTestingTargetIndex,
            int numTestingEgsCorrectClassUsingBestTestingIndex,
            int numTrainingEgsCorrectClassUsingBestTestingIndex)
        {
            // Create new leaf node.
            DecisionTreeNode leaf =
              new DecisionTreeNode(parent, label, null, mask);

            // Set the node statistics.
            leaf.setTrainingStats(numTrainingExamplesReachHere,
                                   bestTrainingTargetIndex,
                                   numTrainingEgsCorrectClassUsingBestTrainingIndex,
                                   numTestingEgsCorrectClassUsingBestTrainingIndex);

            leaf.setTestingStats(numTestingExamplesReachHere,
                                  bestTestingTargetIndex,
                                  numTestingEgsCorrectClassUsingBestTestingIndex,
                                  numTrainingEgsCorrectClassUsingBestTestingIndex);

            // Update the tree statistics.
            TrainingCorrect += numTrainingEgsCorrectClassUsingBestTrainingIndex;
            TestingCorrect += numTestingEgsCorrectClassUsingBestTrainingIndex;

            // Now, attach the new leaf to the supplied node.
            if (parent != null)
                parent.setChild(arcNum, leaf);

            // Add a reference to the new node to the node list.
            Nodes.Add(leaf);

            // Determine if the tree is complete.
            if (findIncompleteNode((DecisionTreeNode)Nodes[0], new int[1]) == null)
            {
                Complete = true;
            }

            return leaf;
        }        
        /// <summary>
        /// Search through the current tree and return the first
        /// node without a complete set of children.  The arc
        /// number for the first missing child is returned in
        /// position 0 of the arcNum array.
        /// </summary>
        /// <param name="node">The node at which to begin the search.</param>
        /// <param name="arcNum">An integer array of size 1.  The arc
        /// number for the first missing child is
        /// returned in arcNum[0].</param>
        /// <returns>A reference to the first incomplete node
        /// in the tree (farthest left).  The method
        /// returns null if the tree is already complete,
        /// or if the tree is empty.</returns>
        public DecisionTreeNode findIncompleteNode(DecisionTreeNode node, int[] arcNum)
        {
            // The search is recursive - at some point, we
            // may want to change this to a non-recursive
            // algorithm (if we start dealing with extremely
            // large trees?)

            // Base cases.
            if (node == null || node.isLeaf())
            {
                return null;
            }

            // Recursive case. This node is not a leaf - so descend.
            for (int i = 0; i < node.getArcLabelCount(); i++)
            {
                DecisionTreeNode nextNode;

                if ((nextNode = findIncompleteNode(node.getChild(i), arcNum)) != null)
                {
                    return nextNode;
                }
            }

            if ((arcNum[0] = node.getFirstMissingChild()) >= 0)
            {
                // We found a node with a missing child, which
                // is already stored in arcNum - so return this
                // node.
                return node;
            }

            // We searched all the subtrees attached to this
            // node, and didn't find anything, so return null.
            return null;
        }        
        /// <summary>
        /// Sets the given node's flag value.  This method exist
        /// here in order to allow the tree to notify
        /// TreeChangeListeners that may be tracking the state of
        /// the tree.        
        public void flagNode(DecisionTreeNode node, int flagValue)
        {
            node.setFlag(flagValue);
        }     
        /// <summary>
        /// Returns the number of internal nodes in the tree.
        /// </summary>        
        public int getNumInternalNodes()
        {
            return InternalNodes;
        }  
        /// <summary>
        /// Returns the number of nodes in the tree.
        /// </summary>        
        public int getNumNodes()
        {
            return Nodes.Count;
        }
        /// <summary>
        /// Returns the number of testing examples from the
        /// dataset that are correctly classified by
        /// the current tree.
        /// </summary>        
        public int getNumTestingEgCorrectClass()
        {
            return TestingCorrect;
        }       
        /// <summary>
        /// Returns the number of training examples from the
        /// dataset that are correctly classified by
        /// the current tree.
        /// </summary>        
        public int getNumTrainingEgCorrectClass()
        {
            return TrainingCorrect;
        }       
        /// <summary>
        /// Returns a reference to the root of the tree, or null if
        /// the tree is empty.
        /// </summary>        
        public DecisionTreeNode getRoot()
        {
            if (Nodes.Count == 0)
            {
                return null;
            }

            return (DecisionTreeNode)Nodes[0];
        }        
        /// <summary>
        /// Returns true if the tree is complete (all branches
        /// decend to leaves), or false otherwise. 
        /// </summary>
        public bool isComplete()
        {
            return Complete;
        }
        /**
         * Returns true if the tree is empty (no nodes) or false
         * otherwise.
         *
         * @return true if the tree is empty, or false if it
         *         contains at least one node.
         */
        public bool isEmpty()
        {
            return (Nodes.Count == 0);
        }
        /**
         * Prunes off the subtree starting at the supplied root.
         *
         * @param pruneRoot The root of the subtree to remove.
         */
        public void pruneSubtree(DecisionTreeNode pruneRoot)
        {
            if (pruneRoot == null)
            {
                return;
            }

            // Detach the root node of the subtree.
            pruneRoot.detach();

            // Once a node has been removed, the tree is no longer complete.
            Complete = false;

            // Now, tell the tree to remove all the
            // node's children.
            recursiveRemoveSubtree(pruneRoot);
        }
        /**
       * Recursively descends through the tree, removing
       * the supplied node and any descendants from
       * the internal node list.
       *
       * @param node The root node of the subtree to remove.
       */
        public void recursiveRemoveSubtree(DecisionTreeNode node)
        {
            if (node == null) return;

            // First, recursively remove all the node's children.
            // (This loop doesn't run if the node is a leaf node)
            for (int i = 0; i < node.getArcLabelCount(); i++)
                if (node.getChild(i) != null)
                    recursiveRemoveSubtree(node.getChild(i));

            // Remove this node from the vector.
            Nodes.Remove(node);

            // If the node was a leaf, then we have to update
            // the classification statistics.
            if (node.isLeaf())
            {
                TrainingCorrect -=
                  node.getTrainingEgsCorrectClassUsingBestTrainingIndex();
                TestingCorrect -=
                  node.getTestingEgsCorrectClassUsingBestTrainingIndex();
            }
            else
                InternalNodes--;
        }
        #endregion
    }
}
