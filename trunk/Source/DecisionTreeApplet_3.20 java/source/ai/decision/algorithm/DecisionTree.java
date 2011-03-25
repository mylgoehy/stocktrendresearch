package ai.decision.algorithm;

import java.util.*;
import ai.common.*;

/**
 * Implementation of a decision tree data structure.
 *
 * The DecisionTree class supports TreeChangeListeners, which
 * are notified whenever the tree is modified.
 *
 * <p>
 * <b>Change History:</b>
 *
 * <p><pre>
 * Name:            Date:            Change:
 * =============================================================
 * J. Kelly         Aug-22-2000      Ground-up rewrite.
 * J. Kelly         Nov-10-2000      Added pruning backup ability.
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
public class DecisionTree
{
  // Debug data members

  boolean DEBUG_ON = true;    // Turn on/off debugging info.

  // Instance data members

  Vector     m_nodes;               // Ordered list of tree nodes.
  LinkedList m_treeChangeListeners; // Tree change listeners.

  int     m_internalNodes;      // Count of internal nodes.
  int     m_trainingCorrect;    // Count of training examples
                                // correctly classified so far.
  int     m_testingCorrect;     // Count of testing examples
                                // correctly classified so far.
  boolean m_complete;           // Indicates tree is complete
                                // (all branches descend to leaves)

  // Constructors

  /**
   * Creates an empty decision tree.
   */
  public DecisionTree()
  {
    m_nodes = new Vector();
    m_treeChangeListeners = new LinkedList();
  }

  // Public methods

  /**
   * Registers a TreeChangeListener.  Whenever the tree
   * modified (e.g. a node is added or removed, or a subtree
   * is pruned) all TreeChangeListeners are notified.
   *
   * @param l The TreeChangeListener to add.
   */
  public void addTreeChangeListener( TreeChangeListener l )
  {
    if( l == null || m_treeChangeListeners.contains( l ) ) return;
    m_treeChangeListeners.add( l );
  }

  /**
   * Removes a TreeChangeListener from the listener list.
   *
   * @param l The TreeChangeListener to be removed.  If the
   *          supplied TreeChangeListener is null, or is
   *          not a member of the current list, no action
   *          is taken.
   */
  public void removeTreeChangeListener( TreeChangeListener l )
  {
    m_treeChangeListeners.remove( l );
  }

  /**
   * Returns true if the tree is empty (no nodes) or false
   * otherwise.
   *
   * @return true if the tree is empty, or false if it
   *         contains at least one node.
   */
  public boolean isEmpty()
  {
    return m_nodes.size() == 0;
  }

  /**
   * Returns true if the tree is complete (all branches
   * decend to leaves), or false otherwise.
   *
   * @return true if the tree is complete, or false otherwise.
   */
  public boolean isComplete()
  {
    return m_complete;
  }

  /**
   * Returns a reference to the root of the tree, or null if
   * the tree is empty.
   *
   * @return A reference to the current root node, or
   *         null if no nodes have been added to the tree.
   */
  public DecisionTreeNode getRoot()
  {
    if( m_nodes.size() == 0 ) return null;

    return (DecisionTreeNode)m_nodes.elementAt( 0 );
  }

  /**
   * Returns the number of nodes in the tree.
   *
   * @return The number of internal and leaf nodes currently in the tree.
   */
  public int getNumNodes()
  {
    return m_nodes.size();
  }

  /**
   * Returns the number of internal nodes in the tree.
   *
   * @return The number of internal nodes <b>only</b> currently in the tree.
   */
  public int getNumInternalNodes()
  {
    return m_internalNodes;
  }

  /**
   * Returns the number of training examples from the
   * dataset that are correctly classified by
   * the current tree.
   *
   * @return The number of training examples from the
   *         dataset that are correctly classified by the
   *         tree (which is equivalent to the number of
   *         examples that reach leaf nodes).
   */
  public int getNumTrainingEgCorrectClass()
  {
    return m_trainingCorrect;
  }

  /**
   * Returns the number of testing examples from the
   * dataset that are correctly classified by
   * the current tree.
   *
   * @return The number of testing examples from the
   *         dataset that are correctly classified by the
   *         tree (which is equivalent to the number of
   *         examples that reach leaf nodes).
   */
  public int getNumTestingEgCorrectClass()
  {
    return m_testingCorrect;
  }

  /**
   * Attaches a new leaf node to the supplied node, along
   * the specified arc.
   *
   * @param parent The node in the current tree to attach
   *        the new leaf node to.  If the node is null, the
   *        new leaf node becomes the root of the tree.
   *
   * @param arcNum The arc number (or attribute value
   *        index) along which to attach the new node.
   *
   * @param label The label for the new node - this
   *        should correspond to the name of the
   *        target attribute value for this leaf.  The
   *        String is copied and stored in the leaf.
   *
   * @param mask The updated mask, with the additional
   *        arc to the new node and the target attribute
   *        value <i>already</i> masked off.
   *
   * @return A reference to the new leaf.
   */
  public DecisionTreeNode addLeafNode( DecisionTreeNode parent,
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
      int numTrainingEgsCorrectClassUsingBestTestingIndex )
  {
    // Create new leaf node.
    DecisionTreeNode leaf =
      new DecisionTreeNode( parent, new String( label ), null, mask );

    // Set the node statistics.
    leaf.setTrainingStats( numTrainingExamplesReachHere,
                           bestTrainingTargetIndex,
                           numTrainingEgsCorrectClassUsingBestTrainingIndex,
                           numTestingEgsCorrectClassUsingBestTrainingIndex );

    leaf.setTestingStats( numTestingExamplesReachHere,
                          bestTestingTargetIndex,
                          numTestingEgsCorrectClassUsingBestTestingIndex,
                          numTrainingEgsCorrectClassUsingBestTestingIndex );

    // Update the tree statistics.
    m_trainingCorrect += numTrainingEgsCorrectClassUsingBestTrainingIndex;
    m_testingCorrect  += numTestingEgsCorrectClassUsingBestTrainingIndex;

    // Now, attach the new leaf to the supplied node.
    if( parent != null )
      parent.setChild( arcNum, leaf );

    // Add a reference to the new node to the node list.
    m_nodes.add( leaf );

    //--------------------- Debug ---------------------
    if( DEBUG_ON ) {
      System.out.print( "DecisionTree::addLeafNode: " +
        "Added leaf node '" + label + "'" );

    if( parent == null )
      System.out.println( " as root of decision tree." );
    else
      System.out.println( " below internal node " +
        "'" + parent.getLabel() + "'." );
    }

    // Determine if the tree is complete.
    if( findIncompleteNode(
      (DecisionTreeNode)m_nodes.elementAt( 0 ), new int[1] ) == null )
      m_complete = true;

    // Inform any listeners that a node was added.
    Iterator i = m_treeChangeListeners.iterator();

    while( i.hasNext() )
      ((TreeChangeListener)i.next()).notifyNodeAdded( leaf );

    return leaf;
  }

  /**
   * Attaches a new internal node to the supplied node,
   * along the specified arc.
   *
   * @param parent The node in the current tree to attach
   *        the internal node to.  If the node is null, the
   *        new internal node becomes the root of the tree.
   *
   * @param arcNum The arc number (or attribute value
   *        index) along which to attach the new node.
   *
   * @param attributePosition The position of the
   *        attribute used to split at the new node, relative
   *        to the other attributes in the dataset.
   *
   * @param att The attribute used to split at the new
   *        node.
   *
   * @param mask The updated mask, with the additional
   *        arc to the new node <i>already</i> masked off.
   *
   * @return A reference to the new internal node.
   */
  public DecisionTreeNode addInternalNode( DecisionTreeNode parent,
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
      int numTrainingEgsCorrectClassUsingBestTestingIndex )
  {
    // Create a new internal node.
    DecisionTreeNode internal =
      new DecisionTreeNode( parent, new String( att.getName() ),
                            att.getValueNames().toArray(), mask );

    // Set the node statistics.
    internal.setTrainingStats( numTrainingExamplesReachHere,
                            bestTrainingTargetIndex,
                            numTrainingEgsCorrectClassUsingBestTrainingIndex,
                            numTestingEgsCorrectClassUsingBestTrainingIndex );

    internal.setTestingStats( numTestingExamplesReachHere,
                            bestTestingTargetIndex,
                            numTestingEgsCorrectClassUsingBestTestingIndex,
                            numTrainingEgsCorrectClassUsingBestTestingIndex );

    // Update the tree statistics.
    m_internalNodes++;

    // Now, attach the new internal node to the supplied node.
    if( parent != null )
      parent.setChild( arcNum, internal );

    // Add a reference to the new node to the node list.
    m_nodes.add( internal );

    //--------------------- Debug ---------------------
    if( DEBUG_ON ) {
      System.out.print( "DecisionTree::addInternalNode: " +
        "Added internal node '" + att.getName() + "'" );

      if( parent == null )
        System.out.println( " as root of decision tree." );
      else
        System.out.println( " below internal node " +
          "'" + parent.getLabel() + "'." );
    }

    // Inform any listeners that a node was added.
    Iterator i = m_treeChangeListeners.iterator();

    while( i.hasNext() )
      ((TreeChangeListener)i.next()).notifyNodeAdded( internal );

    return internal;
  }

  /**
   * Search through the current tree and return the first
   * node without a complete set of children.  The arc
   * number for the first missing child is returned in
   * position 0 of the arcNum array.
   *
   * @param node The node at which to begin the search.
   *
   * @param arcNum An integer array of size 1.  The arc
   *        number for the first missing child is
   *        returned in arcNum[0].
   *
   * @return A reference to the first incomplete node
   *         in the tree (farthest left).  The method
   *         returns null if the tree is already complete,
   *         or if the tree is empty.
   */
  public DecisionTreeNode
    findIncompleteNode( DecisionTreeNode node, int[] arcNum )
  {
    // The search is recursive - at some point, we
    // may want to change this to a non-recursive
    // algorithm (if we start dealing with extremely
    // large trees?)

    // Base cases.
    if( node == null || node.isLeaf() ) return null;

    // Recursive case. This node is not a leaf - so descend.
    for( int i = 0; i < node.getArcLabelCount(); i++ ) {
      DecisionTreeNode nextNode;

      if( (nextNode =
        findIncompleteNode( node.getChild(i), arcNum )) != null )
        return nextNode;
    }

    if( (arcNum[0] = node.getFirstMissingChild()) >= 0 ) {
      // We found a node with a missing child, which
      // is already stored in arcNum - so return this
      // node.

      //--------------------- Debug ---------------------
      if( DEBUG_ON ) {
        System.out.println( "DecisionTree::findIncompleteNode: " +
          "Found vacant branch (position " + (arcNum[0]+1) + ") at node '" +
          node.getLabel() + "'." );
        System.out.println();
      }

      return node;
    }

    // We searched all the subtrees attached to this
    // node, and didn't find anything, so return null.
    return null;
  }

  /**
   * Removes the most-recently added node from the tree.
   * Repeated calls to this method can be used to 'reverse'
   * the tree construction or pruning process.
   *
   * <p>
   * If the tree is empty, this method has no effect.
   */
  public void backup()
  {
    if( this.isEmpty() ) {
      //--------------------- Debug ---------------------
      if( DEBUG_ON ) {
        System.out.println( "DecisionTree:backup: Tree is " +
          "empty, can't backup any further." );
        System.out.println();
      }

      return;
    }

    pruneSubtree( (DecisionTreeNode)m_nodes.get( m_nodes.size() - 1 ) );
  }

  /**
   * Prunes off the subtree starting at the supplied root.
   *
   * @param pruneRoot The root of the subtree to remove.
   */
  public void pruneSubtree( DecisionTreeNode pruneRoot )
  {
    if( pruneRoot == null ) return;

    // Detach the root node of the subtree.
    pruneRoot.detach();

    // Once a node has been removed, the tree is no longer complete.
    m_complete = false;

    // Now, tell the tree to remove all the
    // node's children.
    recursiveRemoveSubtree( pruneRoot );
  }

  /**
   * Recursively descends through the tree, removing
   * the supplied node and any descendants from
   * the internal node list.
   *
   * @param node The root node of the subtree to remove.
   */
  public void recursiveRemoveSubtree( DecisionTreeNode node )
  {
    if( node == null ) return;

    // First, recursively remove all the node's children.
    // (This loop doesn't run if the node is a leaf node)
    for( int i = 0; i < node.getArcLabelCount(); i++ )
      if( node.getChild( i ) != null )
        recursiveRemoveSubtree( node.getChild( i ) );

    // Remove this node from the vector.
    m_nodes.remove( node );

    //--------------------- Debug ---------------------
    if( DEBUG_ON ) {
      System.out.println( "DecisionTree::recursiveRemoveSubtree: Removed " +
        "node '" + node.getLabel() + "' from tree." );
      System.out.println( "DecisionTree::recursiveRemoveSubtree: Tree now " +
        "contains " + m_nodes.size() + " nodes." );
      System.out.println();
    }

    // If the node was a leaf, then we have to update
    // the classification statistics.
    if( node.isLeaf() ) {
      m_trainingCorrect -=
        node.getTrainingEgsCorrectClassUsingBestTrainingIndex();
      m_testingCorrect  -=
        node.getTestingEgsCorrectClassUsingBestTrainingIndex();
    }
    else
      m_internalNodes--;

    // Inform any listeners that a node was removed.
    Iterator i = m_treeChangeListeners.iterator();

    while( i.hasNext() )
      ((TreeChangeListener)i.next()).notifyNodeRemoved( node );
  }

  /**
   * Sets the given node's flag value.  This method exist
   * here in order to allow the tree to notify
   * TreeChangeListeners that may be tracking the state of
   * the tree.
   */
  public void flagNode( DecisionTreeNode node, int flagValue )
  {
    node.setFlag( flagValue );

    // Inform any listeners that a node was modified.
    Iterator i = m_treeChangeListeners.iterator();

    while( i.hasNext() )
      ((TreeChangeListener)i.next()).notifyNodeModified( node );
  }
}
