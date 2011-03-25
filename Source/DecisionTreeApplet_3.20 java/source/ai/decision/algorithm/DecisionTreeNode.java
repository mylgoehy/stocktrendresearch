package ai.decision.algorithm;

/**
 * Nodes in a decision tree are represented by DecisionTreeNode
 * objects.
 *
 * Each  node has a label which identifies the attribute used to
 * 'split' at that node, or the target attribute value represented
 * by the node (in the case of a leaf node).  All nodes store
 * information about attributes that lie along the path from the
 * root of the decision tree to the particular node.
 *
 * <p>
 * Each node also stores a list of arc labels
 * (attribute values) for the paths that leave the node
 * (i.e. paths to subtrees).  Leaf nodes are easily identifiable,
 * as they have no arc labels and no children.
 *
 * <p>
 * <b>Change History:</b>
 *
 * <p><pre>
 * Name:            Date:            Change:
 * =============================================================
 * J. Kelly         May-10-2000      Created.
 * J. Kelly         May-31-2000      Added support for node
 *                                   statistics.
 * J. Kelly         Nov-19-2000      Added node flag support.
 * </pre>
 *
 * Copyright 2000 University of Alberta.
 *
 * <!--
 * This file is part of the Decision Tree Applet.
 *
 * The Decision Tree Applet is free software; you can redistribute it
 * and/or modify it under the terms of the GNU General Public License as
 * published by the Free Software Foundation; either version 2 of the
 * License, or (at your option) any later version.
 *
 * Foobar is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with the Decision Tree Applet; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
 * -->
 */
public class DecisionTreeNode
{
  // Instance data members

  String             m_nodeLabel;  // Attribute or value for
                                   // this node.
  DecisionTreeNode   m_parent;     // Parent of this node.
  Object[]           m_arcLabels;  // Attribute value names.
  DecisionTreeNode[] m_children;   // Children of this node.
  AttributeMask      m_attMask;    // Mask of attributes
                                   // that lie on the path to
                                   // this node.

  int m_nodeFlag;             // Multi-purpose node flag.

  int m_trainEgsReachHere;    // Number of training examples
                              // that reach node.
  int m_testEgsReachHere;     // Number of testing examples
                              // that reach node.

  int m_trainBestTargetIndex; // Target value index for *best*
                              // (most common) training value.
  int m_testBestTargetIndex;  // Target value index for *best*
                              // (most common) testing value.

  int m_trainTrainingCorrectClass;  // Number of training examples
                                    // correctly classified, if
                                    // this were a leaf node w/ most
                                    // common training target value.
  int m_trainTestingCorrectClass;   // Number of testing examples
                                    // correctly classified, if
                                    // this were a leaf node w/ most
                                    // common training target value.
  int m_testTrainingCorrectClass;   // Number of training examples
                                    // correctly classified, if
                                    // this were a leaf node w/ most
                                    // common testing target value.
  int m_testTestingCorrectClass;    // Number of testing examples
                                    // correctly classified, if
                                    // this were a leaf node w/ most
                                    // common testing target value.

  // Constructors

  /**
   * Builds a new decision tree node.
   *
   * @param parent The parent node for this node.  If
   *        the supplied parent is null, the node is
   *        the root node of the tree.
   *
   * @param label The label (name) for the attribute or
   *        value at this node.
   *
   * @param arcLabels An array of Strings that identify
   *        the possible values for the attribute
   *        stored at this node.  If this is a leaf node,
   *        set this to null.
   *
   * @param mask The attribute mask for this node. The
   *        mask should already be configured correctly
   *        (with the appropriate attributes masked off).
   *
   * @throws NullPointerException If the supplied
   *         label or attribute mask is null.
   */
  public DecisionTreeNode( DecisionTreeNode parent,
                           String label,
                           Object[] arcLabels,
                           AttributeMask mask )
  {
    if( label == null || mask == null )
      throw new NullPointerException( "Label or attribute mask is null." );

    m_parent    = parent;
    m_nodeLabel = label;
    m_arcLabels = arcLabels;
    m_attMask   = mask;

    // The number of possible children for the node is
    // equal to the number of values for this attribute.

    // If the arc label array was null, then this is
    // a leaf node that has no children.
    if( arcLabels != null )
      m_children = new DecisionTreeNode[ m_arcLabels.length ];

    // The node is initially unflagged.
    m_nodeFlag = -2;
  }

  // Public methods

  /**
   * Returns the label (name) for the attribute or value at this node.
   *
   * @return The label String for this node.
   */
  public String getLabel()
  {
    return m_nodeLabel;
  }

  /**
   * Identifies if a node is a leaf node or an internal node.
   *
   * @return true if the node is a leaf node, or false otherwise.
   */
  public boolean isLeaf()
  {
    return m_arcLabels == null;
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
    return new AttributeMask( m_attMask );
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
    if( isLeaf() ) return 0;

    return m_arcLabels.length;
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
    if( isLeaf() ) return -1;

    for( int i = 0; i < m_children.length; i++ )
      if( m_children[i] == null ) return i;

    return -1;
  }

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
  public String getArcLabel( int labelNum )
  {
    if( isLeaf() ) return null;

    if( labelNum < 0 || labelNum > (m_arcLabels.length -  1) )
      throw new
        ArrayIndexOutOfBoundsException( "No arc label " +
          "exists at position " + labelNum + "." );

    return (String)m_arcLabels[ labelNum ];
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
  public DecisionTreeNode getChild( int childNum )
  {
    if( this.isLeaf() ) return null;

    if( childNum < 0 || childNum > (m_children.length - 1) )
      throw new
        ArrayIndexOutOfBoundsException( "No child " +
          "exists at position " + childNum + "." );

    return m_children[ childNum ];
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
  public int getChildPosition( DecisionTreeNode child )
  {
    if( this.isLeaf() ) return -1;

    for( int i = 0; i < m_children.length; i++ )
      if( m_children[i] == child ) return i;

    return -1;
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
    return m_parent;
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
  public void setChild( int childNum, DecisionTreeNode node )
  {
    if( isLeaf() ) return;

    if( childNum < 0 || childNum > (m_children.length - 1) )
      throw new
        ArrayIndexOutOfBoundsException( "Cannot add " +
          "child at position " + childNum + "." );

    m_children[ childNum ] = node;
  }

  /**
   * Searches through the current node's list of children
   * and attempts to locate a child that matches the
   * supplied child.  If a match is found, the reference
   * to the child is removed, leaving a vacant arc.
   */
  public void removeChild( DecisionTreeNode child )
  {
    // Search through the list of children, looking for a match.
    for( int i = 0; i < m_children.length; i++ ) {
      if( m_children[i] == child ) {
        m_children[i] = null;
        return;
      }
    }
  }

  /**
   * A utility method that detaches this node from
   * its parent node.  Once this method is called,
   * the node will no longer be attached to the tree.
   */
  public void detach()
  {
    if( m_parent != null ) m_parent.removeChild( this );
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
  public void
    setTrainingStats( int numTrainingExamplesReachHere,
                      int bestTrainingTargetIndex,
                      int numTrainingExamplesCorrectClass,
                      int numTestingExamplesCorrectClass )
  {
    m_trainEgsReachHere = numTrainingExamplesReachHere;
    m_trainBestTargetIndex = bestTrainingTargetIndex;
    m_trainTrainingCorrectClass = numTrainingExamplesCorrectClass;
    m_trainTestingCorrectClass  = numTestingExamplesCorrectClass;
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
    return m_trainEgsReachHere;
  }

  /**
   * Returns the number of training examples that would
   * be correctly classified at this node, if it was
   * a leaf node with the most common training target
   * attribute value.
   */
  public int getTrainingEgsCorrectClassUsingBestTrainingIndex()
  {
    return m_trainTrainingCorrectClass;
  }

  /**
   * Returns the number of testing examples that would
   * be correctly classified at this node, if it was
   * a leaf node with the most common training target
   * attribute value.
   */
  public int getTestingEgsCorrectClassUsingBestTrainingIndex()
  {
    return m_trainTestingCorrectClass;
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
  public void
    setTestingStats( int numTestingExamplesReachHere,
                     int bestTestingTargetIndex,
                     int numTestingExamplesCorrectClass,
                     int numTrainingExamplesCorrectClass )
  {
    m_testEgsReachHere = numTestingExamplesReachHere;
    m_testBestTargetIndex = bestTestingTargetIndex;
    m_testTestingCorrectClass  = numTestingExamplesCorrectClass;
    m_testTrainingCorrectClass = numTrainingExamplesCorrectClass;
  }

  /**
   * Returns the number of testing examples that reach
   * the node.
   *
   * @return The number of examples from the dataset that reach the node.
   */
  public int getTestingEgsAtNode()
  {
    return m_testEgsReachHere;
  }

  /**
   * Returns the number of training examples that would
   * be correctly classified at this node, if it was
   * a leaf node with the most common testing target
   * attribute value.
   */
  public int getTrainingEgsCorrectClassUsingBestTestingIndex()
  {
    return m_testTrainingCorrectClass;
  }

  /**
   * Returns the number of testing examples that would
   * be correctly classified at this node, if it was
   * a leaf node with the most common testing target
   * attribute value.
   */
  public int getTestingEgsCorrectClassUsingBestTestingIndex()
  {
    return m_testTestingCorrectClass;
  }

  /**
   * Returns the best (most common) testing set
   * target attribute value index.
   */
  public int getTestingBestTarget()
  {
    return m_testBestTargetIndex;
  }

  /**
   * Returns the best (most common) training set
   * target attribute value index.
   */
  public int getTrainingBestTarget()
  {
    return m_trainBestTargetIndex;
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
    return m_nodeFlag;
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
  public void setFlag( int flagValue )
  {
    m_nodeFlag = flagValue;
  }
}
