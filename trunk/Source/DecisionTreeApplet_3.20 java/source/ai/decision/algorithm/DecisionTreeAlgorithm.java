package ai.decision.algorithm;

import java.util.*;
import ai.decision.gui.*;
import ai.common.*;

/**
 * An implementation of a decision tree learning algorithm.
 * (See Mitchell, <i>Machine Learning</i>, pg. 56;
 *  Russell and Norvig, <i>Artificial Intelligence:
 *  A Modern Approach</i>, pg. 537 )
 *
 * <p>
 * When building a decision tree, this class relies on a
 * Dataset instance to provide relevant statistics for
 * attribute selection.
 *
 * <p>
 * <b>Change History:</b>
 *
 * <p><pre>
 * Name:            Date:            Change:
 * =============================================================
 * J. Kelly         Sep-26-2000      Ground-up rewrite using
 *                                   AlgorithmFramework class.
 * J. Kelly         Oct-02-2000      Added reduced-error and
 *                                   pessimistic pruning.
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
public class DecisionTreeAlgorithm
  extends AlgorithmFramework
{
  // Class data members

  // Possible splitting criteria

  /**
   * Indicates that attributes for splitting are selected
   * at random.
   */
  public static final String SPLIT_RANDOM = "Random";

  /**
   * Indicates that attributes for splitting are selected
   * based on maximum information gain.
   */
  public static final String SPLIT_GAIN = "Gain";

  /**
   * Indicates that attributes for splitting are selected
   * based on maximum gain ratio.
   */
  public static final String SPLIT_GAIN_RATIO = "Gain Ratio";

  /**
   * Indicates that attributes for splitting are selected
   * based on maxiumum GINI score.
   */
  public static final String SPLIT_GINI = "GINI";

  /**
   * An array of the available splitting functions.
   */
  public static final String[] SPLIT_FUNCTIONS  =
    { SPLIT_RANDOM, SPLIT_GAIN, SPLIT_GAIN_RATIO, SPLIT_GINI };

  //-------------------------------------------------------

  /**
   * Indicates that the examples have mixed target
   * attribute values.
   */
  public static final int DATASET_MIXED_CONCL = 0;

  /**
   * Indicates that all examples share one common target
   * attribute value.
   */
  public static final int DATASET_IDENT_CONCL = 1;

  /**
   * Indicates that the set of training examples is empty.
   */
  public static final int DATASET_EMPTY       = 2;

  //-------------------------------------------------------

  // Possible pruning algorithms

  /**
   * Indicates that the decision tree should not be
   * pruned.
   */
  public static final String PRUNING_NONE = "None";

  /**
   * Indicates that the decision tree should be
   * pruned using the reduced-error pruning
   * algorithm.
   */
  public static final String PRUNING_REDUCED_ERROR = "Reduced-error";

  /**
   * Indicates that the decision tree should be
   * pruned using the pessimistic pruning
   * algorithm.
   */
  public static final String PRUNING_PESSIMISTIC = "Pessimistic";

  /**
   * An array of the available pruning algorithms.
   */
  public static final String[] PRUNING_ALGORITHMS =
    { PRUNING_NONE, PRUNING_REDUCED_ERROR, PRUNING_PESSIMISTIC };

  /**
   * Default pessimistic pruning z-score - 95% confidence
   * interval.
   */
  public static final double DEFAULT_Z_SCORE = 1.96;

  //-------------------------------------------------------

  // Instance data members

  Dataset       m_dataset;    // Data set used to build tree.
  DecisionTree  m_tree;       // Current decision tree.
  String        m_splitFun;   // Current splitting function.
  String        m_pruneAlg;   // Current pruning algorithm.
  Random        m_random;     // Random number generator.

  double m_pessPruneZScore;   // Pessimistic pruning Z-score.

  ComponentManager m_manager;

  // Constructors

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
  public DecisionTreeAlgorithm( Dataset dataset, ComponentManager manager )
  {
    super();

    if( dataset == null || manager == null )
      throw new
        NullPointerException( "Dataset or component manager is null." );

    m_manager = manager;

    m_dataset  = dataset;
    m_splitFun = SPLIT_RANDOM;
    m_pruneAlg = PRUNING_NONE;
    m_random   = new Random( 2389 );
    m_tree     = new DecisionTree();

    m_pessPruneZScore = DEFAULT_Z_SCORE;
  }

  // Public methods

  /**
   * Returns a reference to the dataset that the algorithm
   * is currently using.
   *
   * @return A reference to the current dataset.
   */
  public Dataset getDataset()
  {
    return m_dataset;
  }

  /**
   * Sets the current dataset.  Changing the dataset
   * automatically destroys the current tree.
   *
   * @param dataset The new dataset.
   *
   * @throws NullPointerException if the supplied dataset
   *         is null.
   */
  public void setDataset( Dataset dataset )
  {
    if( dataset == null )
      throw new
        NullPointerException( "Dataset is null." );

    m_dataset = dataset;
    m_tree    = new DecisionTree();

    // Re-register listeners with the new tree.
    if( m_manager.getVisualTreePanel() != null )
      m_tree.addTreeChangeListener( m_manager.getVisualTreePanel() );
  }

  /**
   * Resets the algorithm, destroying the current
   * tree.  The dataset used to build the tree
   * remains unchanged.
   */
  public void reset()
  {
    m_tree = new DecisionTree();

    // Re-register listeners with the new tree.
    if( m_manager.getVisualTreePanel() != null )
      m_tree.addTreeChangeListener( m_manager.getVisualTreePanel() );
  }

  /**
   * Returns a reference to the decision tree data structure.
   */
  public DecisionTree getTree()
  {
    return m_tree;
  }

  /**
   * Sets the splitting function used to build the
   * decision tree.  If the supplied function name
   * does not correspond to one of the known functions
   * the random 'function' is used by default.
   *
   * @param splitFun The new splitting function - this must
   *        be one of SPLIT_RANDOM, SPLIT_GAIN,
   *        SPLIT_GAIN_RATIO or SPLIT_GINI.
   */
  public synchronized void setSplittingFunction( String splitFun )
  {
    if( splitFun.equals( SPLIT_RANDOM )     ||
        splitFun.equals( SPLIT_GAIN )       ||
        splitFun.equals( SPLIT_GAIN_RATIO ) ||
        splitFun.equals( SPLIT_GINI ) )
        m_splitFun = splitFun;
    else
        m_splitFun = SPLIT_RANDOM;

    // Inform HighlightListeners that the splitting
    // function text may have changed.
    Iterator i = m_highlightListeners.iterator();

    while( i.hasNext() )
      ((HighlightListener)i.next())
        .setDynamicText( "LearnDT", "splitfun", m_splitFun );
  }

  /**
   * Sets the pruning algorithm.  If the supplied algorithm
   * name does not correspond to one of the known algorithms,
   * pruning is disabled.
   *
   * @param pruneAlg The new pruning algorithm - this must
   *        be one of PRUNING_NONE, PRUNING_REDUCED_ERROR or
   *        PRUNING_PESSIMISTIC.
   */
  public synchronized void setPruningAlgorithm( String pruneAlg )
  {
    if( pruneAlg.equals( PRUNING_NONE ) ||
        pruneAlg.equals( PRUNING_REDUCED_ERROR ) ||
        pruneAlg.equals( PRUNING_PESSIMISTIC ) )
      m_pruneAlg = pruneAlg;
    else
      m_pruneAlg = PRUNING_NONE;

    // Inform HighlightListeners that the splitting
    // function text may have changed.
    Iterator i = m_highlightListeners.iterator();

    while( i.hasNext() )
      ((HighlightListener)i.next())
        .setDynamicText( "BuildDT", "prunealg", m_pruneAlg );
  }

  /**
   * Returns the current splitting function.
   *
   * @return The current splitting function as a String.
   */
  public String getSplittingFunction()
  {
    return m_splitFun;
  }

  /**
   * Returns the current pruning algorithm.
   *
   * @return The current pruning algorithm as a String.
   */
  public String getPruningAlgorithm()
  {
    return m_pruneAlg;
  }

  /**
   * Returns the current pessimistic pruning Z-score.
   *
   * @return The current pessimistic pruning Z-score.
   */
  public double getPessimisticPruningZScore()
  {
    return m_pessPruneZScore;
  }

  /**
   * Sets the current pessimistic pruning Z-score.
   *
   * @param zScore The new Z-score to use when calculating
   *        error bars for the pessimistic pruning algorithm.
   *
   * @throws IllegalArgumentException If the supplied value
   *         is negative.
   */
  public void setPessimisticPruningZScore( double zScore )
  {
    if( zScore < 0 )
      throw new IllegalArgumentException( "Supplied Z-score < 0." );

    m_pessPruneZScore = zScore;
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
  public synchronized void run()
  {
    Iterator i;

    // Inform HighlightListeners that we're about to start
    // the main "BuildDT" routine.
    i = m_highlightListeners.iterator();

    while( i.hasNext() )
      ((HighlightListener)i.next()).displayFunction( "BuildDT" );

    //----------------- Breakpoint -----------------
    if( !handleBreakpoint( 1, null ) ) return;

    //----------------- Breakpoint -----------------
    if( !handleBreakpoint( 2, null ) ) return;

    DecisionTreeNode parent;
    int[] arcNum = new int[1];

    //------------------ LearnDT ------------------

    // Inform HighlightListeners that we're about to start
    // the "LearnDT" routine.
    i = m_highlightListeners.iterator();

    while( i.hasNext() )
      ((HighlightListener)i.next()).displayFunction( "LearnDT" );

    // Build the tree, looping until it's complete.
    while( true ) {
      // Determine where to start building
      // (leftmost available position).
      if( m_tree.isEmpty() )
        parent = null;
      else if( (parent =
        m_tree.findIncompleteNode( m_tree.getRoot(), arcNum )) == null )
        break;

      // Build the tree - the call to learnDT()
      // will fill in everything below the current branch.
      if( !learnDT( parent, arcNum[0] ) ) break;
    }

    // Now check - if the tree is complete, we can start the
    // pruning process.  Otherwise, we've stopped with some
    // nodes still missing.
    if( !m_tree.isComplete() ) return;

    // Inform HighlightListeners that we've finished "LearnDT",
    // and are now moving on to "PruneDT".
    i = m_highlightListeners.iterator();

    while( i.hasNext() )
      ((HighlightListener)i.next()).displayFunction( "BuildDT" );

    //----------------- Breakpoint -----------------
    if( !handleBreakpoint( 3, null ) ) return;

    //------------------ PruneDT ------------------

    if( m_pruneAlg.equals( PRUNING_REDUCED_ERROR ) ) {
      // Inform HighlightListeners that we're about to start
      // the "PruneReducedErrorDT" routine.
      i = m_highlightListeners.iterator();

      while( i.hasNext() )
        ((HighlightListener)i.next()).displayFunction( "PruneReducedErrorDT" );

      // pruneReducedErrorDT will return false if it's
      // interrupted and is unable to finish pruning the tree.
      if( !pruneReducedErrorDT( m_tree.getRoot(), new double[1] ) )
        return;
    }
    else if( m_pruneAlg.equals( PRUNING_PESSIMISTIC ) ) {
      // Inform HighlightListeners that we're about to start
      // the "PrunePessimisticDT" routine.
      i = m_highlightListeners.iterator();

      while( i.hasNext() )
        ((HighlightListener)i.next()).displayFunction( "PrunePessimisticDT" );

      // prunePessimisticDT will return false if it's
      // interrupted and is unable to finish pruning the tree.
      if( !prunePessimisticDT( m_tree.getRoot(), new double[1] ) )
        return;
    }

    // Reset the function display to "BuildDT"
    i = m_highlightListeners.iterator();

    while( i.hasNext() )
      ((HighlightListener)i.next()).displayFunction( "BuildDT" );

    // Tell anyone that might be tracking
    // the state of the algorithm that we've finished.
    i = m_algorithmListeners.iterator();

    while( i.hasNext() )
      ((AlgorithmListener)i.next()).notifyAlgorithmFinished();

    if( m_manager.getStatusBar() != null )
      m_manager.getStatusBar().postMessage( "Algorithm finished." );
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
  public boolean learnDT( DecisionTreeNode parent, int arcNum )
  {
    AttributeMask mask;
    Iterator i;

    if( parent == null )
      // We have to add at the root.
      mask = new AttributeMask( m_dataset.getNumAttributes() );
    else {
      mask = new AttributeMask( parent.getMask() );

      // Mask off the specified arc number.
      try {
        mask.mask(
          m_dataset.getAttributePosition( parent.getLabel() ), arcNum );
      }
      catch( NonexistentAttributeException e ) {
        e.printStackTrace();
        return false;
      }
    }

    //----------------- Breakpoint -----------------
    if( !handleBreakpoint( 1, null ) ) return false;

    // Now, classify the examples at the current position.
    int[] conclusion = new int[8];
    int   result     = classifyExamples( mask, conclusion, null, null, null );

    Attribute target  = m_dataset.getTargetAttribute();
    int numTargetVals = target.getNumValues();
    String label;

    //----------------- Breakpoint -----------------
    if( !handleBreakpoint( 2, null ) ) return false;

    if( result == DATASET_EMPTY ) {
      // If no examples reach our current position
      // we add a leaf with the most common target
      // classfication for the parent node.

      // Save testing results.
      int numTestingExamplesReachHere     =  conclusion[5];
      int bestTestingTargetIndex          =  conclusion[4];
      int numTestingExamplesCorrectClass  =  conclusion[6];
      int numTrainingExamplesCorrectClass =  conclusion[7];

      classifyExamples( parent.getMask(), conclusion, null, null, null );

      try {
        label = target.getAttributeValueByNum( conclusion[0] );
      }
      catch( NonexistentAttributeValueException e ) {
        e.printStackTrace();
        return false;
      }

      //--------------------- Debug ---------------------
      if( DEBUG_ON ) {
        System.out.println();
        System.out.println( "DecisionTreeAlgorithm::learnDT: " +
          "No examples reach the current position." );
      }

      // We have to grab the counts again for the testing data...
      int[] currTestingCounts = new int[ target.getNumValues() ];
      getExampleCounts( mask,
        m_dataset.getTestingExamples(), currTestingCounts, null );      

      // Mask target value and add a leaf to the tree.
      mask.mask( 0, conclusion[0] );

      DecisionTreeNode node =
        m_tree.addLeafNode( parent,
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
                            numTrainingExamplesCorrectClass );

      i = m_algorithmListeners.iterator();

      if( m_verboseFlag )
        while( i.hasNext() )
          ((AlgorithmListener)
            i.next()).notifyAlgorithmStepStart( createLeafMsg( 1, label ) );
      else
        while( i.hasNext() )
          ((AlgorithmListener)
            i.next()).notifyAlgorithmStepStart();

      return true;
    }

    //----------------- Breakpoint -----------------
    if( !handleBreakpoint( 3, null ) ) return false;

    if( result == DATASET_IDENT_CONCL ) {
      // Pure result - we can add a leaf node with the
      // correct target attribute value.
      try {
        label = target.getAttributeValueByNum( conclusion[0] );
      }
      catch( NonexistentAttributeValueException e ) {
        e.printStackTrace();
        return false;
      }

      //--------------------- Debug ---------------------
      if( DEBUG_ON ) {
        System.out.println();
        System.out.println( "DecisionTreeAlgorithm::learnDT: " +
          "All examples at the current position " +
          "have the same target class '" + label + "'." );
      }

      // Mask target value and add a leaf to the tree.
      mask.mask( 0, conclusion[0] );

      DecisionTreeNode node =
        m_tree.addLeafNode( parent,
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
                            conclusion[7] );

      i = m_algorithmListeners.iterator();

      if( m_verboseFlag )
        while( i.hasNext() )
          ((AlgorithmListener)
            i.next()).notifyAlgorithmStepStart( createLeafMsg( 2, label ) );
      else
        while( i.hasNext() )
          ((AlgorithmListener)
            i.next()).notifyAlgorithmStepStart();

      return true;
    }

    // Mixed conclusion - so we have to select
    // an attribute to split on, and then build a
    // new internal node with that attribute.

    // First, generate statistics - this may take awhile.
    int[]  nodeStats = new int[ numTargetVals ];
    Vector availableAtts = generateStats( mask, nodeStats );

    //----------------- Breakpoint -----------------
    if( !handleBreakpoint( 4, null ) ) return false;

    if( availableAtts.size() == 0 ) {
      // No attributes left to split on - so use
      // the most common target value at the current position.
      try {
        label = target.getAttributeValueByNum( conclusion[0] );
      }
      catch( NonexistentAttributeValueException e ) {
        e.printStackTrace();
        return false;
      }

      //--------------------- Debug ---------------------
      if( DEBUG_ON ) {
        System.out.println();
        System.out.println( "DecisionTreeAlgorithm::learnDT: " +
          "No attributes left to split on at current position." );
      }

      mask.mask( 0, conclusion[0] );

      DecisionTreeNode node =
        m_tree.addLeafNode( parent,
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
                            conclusion[7] );

      i = m_algorithmListeners.iterator();

      if( m_verboseFlag )
        while( i.hasNext() )
          ((AlgorithmListener)
            i.next()).notifyAlgorithmStepStart( createLeafMsg( 3, label ) );
      else
        while( i.hasNext() )
          ((AlgorithmListener)
            i.next()).notifyAlgorithmStepStart();

      return true;
    }

    // Choose an attribute, based on the set of
    // available attributes.
    Vector results = new Vector();
    Attribute att  = chooseAttribute( availableAtts, nodeStats, results );

    //--------------------- Debug ---------------------
    if( DEBUG_ON ) {
      System.out.println();
      System.out.println( "DecisionTreeAlgorithm::learnDT: " +
        "Preparing to split..." );
      System.out.println();
      System.out.println( "Available attributes and " +
        "associated " + m_splitFun + " values:" );

      for( int j = 0; j < availableAtts.size(); j++ ) {
        System.out.print(
          ((Attribute)availableAtts.elementAt( j )).getName() );

        if( j < results.size() ) {
          System.out.print( " - " );
          System.out.println( (Double)results.elementAt(j) );
        }
        else {
          System.out.println();
        }
      }

      System.out.println();
      System.out.println( "Selected " + att.getName() + "." );
      System.out.println();
    }

    //----------------- Breakpoint -----------------
    if( !handleBreakpoint( 5, createInternalMsg(1, att.getName() )) )
      return false;

    //----------------- Breakpoint -----------------
    if( !handleBreakpoint( 6, createInternalMsg(2, att.getName() )) )
      return false;

    int attPos;

    try {
      attPos = m_dataset.getAttributePosition( att.getName() );
    }
    catch( NonexistentAttributeException e ) {
      e.printStackTrace();
      return false;
    }

    DecisionTreeNode newParent =
      m_tree.addInternalNode( parent,
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
                              conclusion[7] );

    // Now, recursively decend along each branch of the new node.
    for( int j = 0; j < newParent.getArcLabelCount(); j++ ) {
      //----------------- Breakpoint -----------------
      if( !handleBreakpoint( 7, null ) ) return false;

      //----------------- Breakpoint -----------------
      if( !handleBreakpoint( 8, null ) ) return false;

      //----------------- Breakpoint -----------------
      if( !handleBreakpoint( 9, null ) ) return false;

      //--------------------- Debug ---------------------
      if( DEBUG_ON ) {
        System.out.println();
        System.out.println( "DecisionTreeAlgorithm::learnDT: " +
          "Descending along branch " + (j+1) + " of " +
          newParent.getArcLabelCount() + "." );
      }

      // Recursive call.
      if( !learnDT( newParent, j ) ) return false;

      //----------------- Breakpoint -----------------
      if( !handleBreakpoint( 10, null ) ) return false;

      //----------------- Breakpoint -----------------
      if( !handleBreakpoint( 11,
        createInternalMsg(3, newParent.getArcLabel(j)) ) )
        return false;
    }

    //----------------- Breakpoint -----------------
    if( !handleBreakpoint( 12, null ) ) return false;

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
  public boolean pruneReducedErrorDT( DecisionTreeNode node, double[] error )
  {
    // Post-order walk through the tree, marking
    // our path as we go along.
    //----------------- Breakpoint -----------------
    if( !handleBreakpoint( 1, null ) ) return false;

    //----------------- Breakpoint -----------------
    if( !handleBreakpoint( 2, null ) ) return false;

    if( node.isLeaf() ) {
      error[0] = node.getTestingEgsAtNode() -
        node.getTestingEgsCorrectClassUsingBestTrainingIndex();
      return true;
    }

    //----------------- Breakpoint -----------------
    if( !handleBreakpoint( 3, null ) ) return false;

    // We're at an internal node, so compute the error
    // of the children and use the result to determine
    // if we prune or not.
    double errorSum = 0;

    for( int i = 0; i < node.getArcLabelCount(); i++ ) {
      // Mark our current path.
      m_tree.flagNode( node, i );

      //----------------- Breakpoint -----------------
      if( !handleBreakpoint( 4, null ) ) return false;

      //----------------- Breakpoint -----------------
      if( !handleBreakpoint( 5, null ) ) return false;

      //----------------- Breakpoint -----------------
      if( !handleBreakpoint( 6, null ) ) return false;

      if( !pruneReducedErrorDT( node.getChild( i ), error ) ) {
        m_tree.flagNode( node, -2 );
        return false;
      }

      errorSum += error[0];
    }

    // Mark the node as our current target.
    m_tree.flagNode( node, -1 );

    //----------------- Breakpoint -----------------
    if( !handleBreakpoint( 8, null ) ) return false;

    // Get the best-case performance of this node.
    double errorBest = node.getTestingEgsAtNode() -
      node.getTestingEgsCorrectClassUsingBestTestingIndex();

    DecisionTreeNode newNode = node;

    //----------------- Breakpoint -----------------
    if( !handleBreakpoint( 9,
      createPruningMsg( errorSum, errorBest ) ) ) return false;

    if( errorBest < errorSum ) {
      // We need to "prune" this node to a leaf.
      DecisionTreeNode parent = node.getParent();
      int arcNum = -1;

      if( parent != null )
        arcNum = parent.getChildPosition( node );

      //--------------------- Debug ---------------------
      if( DEBUG_ON ) {
        System.out.println();
        System.out.println( "DecisionTreeAlgorithm::pruneReduceErrorDT: " +
                            " Pruning node " + node.getLabel() + "." );
      }

      m_tree.pruneSubtree( node );

      // Figure out the label for the new leaf.
      String label = null;

      try {
        label =
          m_dataset.getTargetAttribute().getAttributeValueByNum(
            node.getTestingBestTarget() );
      }
      catch( NonexistentAttributeValueException e ) {
        // Should never happen.
        e.printStackTrace();
      }

      node.getMask().mask( 0, node.getTestingBestTarget() );

      newNode =
        m_tree.addLeafNode( parent, arcNum, label,
          node.getMask(),
          node.getTrainingEgsAtNode(),
          node.getTestingBestTarget(),
          node.getTrainingEgsCorrectClassUsingBestTestingIndex(),
          node.getTestingEgsCorrectClassUsingBestTestingIndex(),
          node.getTestingEgsAtNode(),
          node.getTestingBestTarget(),
          node.getTestingEgsCorrectClassUsingBestTestingIndex(),
          node.getTrainingEgsCorrectClassUsingBestTestingIndex() );
    }

    // Update the count.
    if( newNode.isLeaf() )
      error[0] = errorBest;
    else
      error[0] = errorSum;

    //----------------- Breakpoint -----------------
    if( !handleBreakpoint( 10, null ) ) return false;

    // All finished, unmark the node if it still exists.
    m_tree.flagNode( node, -2 );

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
  public boolean prunePessimisticDT( DecisionTreeNode node, double[] error )
  {
    // Post-order walk through the tree, marking
    // our path as we go along.
    //----------------- Breakpoint -----------------
    if( !handleBreakpoint( 1, null ) ) return false;

    //----------------- Breakpoint -----------------
    if( !handleBreakpoint( 2, null ) ) return false;

    if( node.isLeaf() ) {
      if( node.getTrainingEgsAtNode() == 0 ) {
        error[0] = 0;
        return true;
      }
      else {
        // We do the error calculation in two steps -
        // Here we multiply the error value by the number
        // of examples that reach the node.  When the method
        // is called recursively, this value will be divided
        // by the number of examples that reach the parent
        // node (thus weighting the error from each child).
        int errors = node.getTrainingEgsAtNode() -
          node.getTrainingEgsCorrectClassUsingBestTrainingIndex();
	double p = (errors + 1.0) / (node.getTrainingEgsAtNode() + 2.0);
	
        error[0] = node.getTrainingEgsAtNode() * 
	  errorBar( p, node.getTrainingEgsAtNode() ) + errors;

        return true;
      }
    }

    //----------------- Breakpoint -----------------
    if( !handleBreakpoint( 3, null ) ) return false;

    // We're at an internal node, so compute the error
    // of the children and use the result to determine
    // if we prune or not.
    double errorSum = 0;

    for( int i = 0; i < node.getArcLabelCount(); i++ ) {
      // Mark our current path.
      m_tree.flagNode( node, i );

      //----------------- Breakpoint -----------------
      if( !handleBreakpoint( 4, null ) ) return false;

      //----------------- Breakpoint -----------------
      if( !handleBreakpoint( 5, null ) ) return false;

      //----------------- Breakpoint -----------------
      if( !handleBreakpoint( 6, null ) ) return false;

      if( !prunePessimisticDT( node.getChild( i ), error ) ) {
        m_tree.flagNode( node, -2 );
        return false;
      }

      errorSum += error[0];
    }

    // Mark the node as our current target.
    m_tree.flagNode( node, -1 );

    //----------------- Breakpoint -----------------
    if( !handleBreakpoint( 8, null ) ) return false;

    // Get the worst-case performance of this node.
    double errorWorst;

    if( node.getTrainingEgsAtNode() == 0 ) {
      error[0] = 0;
      return true;
    }

    int errors = node.getTrainingEgsAtNode() -
      node.getTrainingEgsCorrectClassUsingBestTrainingIndex();
    double p = (errors + 1.0) / (node.getTrainingEgsAtNode() + 2.0);
	
    errorWorst = node.getTrainingEgsAtNode() * 
      errorBar( p, node.getTrainingEgsAtNode() ) + errors;

    DecisionTreeNode newNode = node;

    //----------------- Breakpoint -----------------
    if( !handleBreakpoint( 9,
      createPruningMsg( errorSum, errorWorst ) ) ) return false;

    if( errorWorst < errorSum ) {
      // We need to "prune" this node to a leaf.
      DecisionTreeNode parent = node.getParent();
      int arcNum = -1;

      if( parent != null )
        arcNum = parent.getChildPosition( node );

      //--------------------- Debug ---------------------
      if( DEBUG_ON ) {
        System.out.println();
        System.out.println( "DecisionTreeAlgorithm::prunePessimisticDT: " +
                            " Pruning node " + node.getLabel() + "." );
      }

      m_tree.pruneSubtree( node );

      // Figure out the label for the new leaf.
      String label = null;

      try {
        label =
          m_dataset.getTargetAttribute().getAttributeValueByNum(
            node.getTrainingBestTarget() );
      }
      catch( NonexistentAttributeValueException e ) {
        // Should never happen.
        e.printStackTrace();
      }

      node.getMask().mask( 0, node.getTrainingBestTarget() );

      newNode =
        m_tree.addLeafNode( parent, arcNum, label,
          node.getMask(),
          node.getTrainingEgsAtNode(),
          node.getTrainingBestTarget(),
          node.getTrainingEgsCorrectClassUsingBestTrainingIndex(),
          node.getTestingEgsCorrectClassUsingBestTrainingIndex(),
          node.getTestingEgsAtNode(),
          node.getTestingBestTarget(),
          node.getTestingEgsCorrectClassUsingBestTestingIndex(),
          node.getTrainingEgsCorrectClassUsingBestTestingIndex() );
    }

    // Update the count.
    if( newNode.isLeaf() )
      error[0] = errorWorst;
    else
      error[0] = errorSum;

    //----------------- Breakpoint -----------------
    if( !handleBreakpoint( 10, null ) ) return false;

    // All finished, unmark the node if it still exists.
    m_tree.flagNode( node, -2 );

    return true;
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
  public int
    classifyExamples( AttributeMask mask,
                      int[] conclusion,
                      int[] trainingCounts,
                      int[] testingCounts,
                      boolean[] examples )
  {
    if( mask == null || conclusion == null )
      throw new
        NullPointerException( "Mask or conclusion array is null." );

    // Determine the number of target attribute values
    // and create some storage space for our counts.
    int[] currTrainingCounts = null;
    int[] currTestingCounts  = null;

    if( trainingCounts != null )
      currTrainingCounts = trainingCounts;
    else
      currTrainingCounts = new
        int[ m_dataset.getTargetAttribute().getNumValues() ];

    if( testingCounts != null )
      currTestingCounts = testingCounts;
    else
      currTestingCounts = new int[ currTrainingCounts.length ];

    getExampleCounts( mask,
      m_dataset.getTrainingExamples(), currTrainingCounts, examples );
    getExampleCounts( mask,
      m_dataset.getTestingExamples(), currTestingCounts, null );

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

    for( int i = 0; i < currTrainingCounts.length; i++ ) {
      // Increment # of examples that reach this position.
      conclusion[1] += currTrainingCounts[i];
      conclusion[5] += currTestingCounts[i];

      if( result == DATASET_EMPTY && currTrainingCounts[i] != 0 )
        result = DATASET_IDENT_CONCL;
      else if( result == DATASET_IDENT_CONCL && currTrainingCounts[i] != 0 )
        result = DATASET_MIXED_CONCL;

      if( currTrainingCounts[i] >= currTrainingCounts[ conclusion[0] ] ) {
        // This target value is more common in the training set.
        conclusion[0] = i;
        conclusion[2] = currTrainingCounts[i];
        conclusion[3] = currTestingCounts[i];
      }

      if( currTestingCounts[i] >= currTestingCounts[ conclusion[4] ] ) {
        // This target value is more common in the testing set.
        conclusion[4] = i;
        conclusion[6] = currTestingCounts[i];
        conclusion[7] = currTrainingCounts[i];
      }
    }

    return result;
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
  public Vector generateStats( AttributeMask mask, int stats[] )
  {
    // First, we fill the stats array - this is not the
    // most efficient approach, since we're looping through
    // the data several times.
    getExampleCounts( mask, m_dataset.getTrainingExamples(), stats, null );

    // Now, we have to go through the attribute mask
    // and locate the attributes that are still available.
    Vector results = new Vector();

    // Create a new mask that we can modify.
    AttributeMask newMask = new AttributeMask( mask );

    // We don't use position 0, that's where the target attribute is.
    for( int i = 1; i < mask.getNumAttributes(); i++ )  {
      if( newMask.isMasked( i ) == AttributeMask.UNUSED ) {
        // This attribute is available, so we calculate stats for it.
        Attribute att = null;

        try {
          att = m_dataset.getAttributeByNum( i );
        }
        catch( NonexistentAttributeException e ) {
          // This can't happen!!
          e.printStackTrace();
          return null;
        }

        int[][] attStats = att.getStatsArray();

        // Modify the mask and fill in the arrays.
        for( int j = 0; j < att.getNumValues(); j++ ) {
          newMask.mask( i, j );
          getExampleCounts( newMask, m_dataset.getTrainingExamples(),
            attStats[j], null );
        }

        // Reset the mask.
        newMask.unmask( i );
        results.add( att );
      }
    }

    return results;
  }

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
  public Attribute
    chooseAttribute( Vector atts, int[] stats, Vector results )
  {
    // If the list of available attributes is empty,
    // return null.
    if( atts.size() == 0 ) return null;

    int pos = 0;

    // Select an attribute, based on the current
    // splitting function.
    if( m_splitFun.equals( SPLIT_RANDOM ) )
      // Choose one of the unmasked attributes
      // at random.  We leave the results vector
      // empty in this case.
      pos = m_random.nextInt( atts.size() );
    else {
      // Calculate a result value for each
      // attribute that is available.
      double val = 0.0;
      double temp;

      for( int i = 0; i < atts.size(); i++ ) {
        if( m_splitFun.equals( SPLIT_GAIN ) ) {
          temp = getGain( stats, (Attribute)atts.elementAt( i ));

          if( temp > val ) {
            val = temp;
            pos = i;
          }

          results.add( new Double( temp ) );
        }
        else if( m_splitFun.equals( SPLIT_GAIN_RATIO ) ) {
          temp = getGainRatio( stats, (Attribute)atts.elementAt( i ) );

          if( temp > val ) {
            val = temp;
            pos = i;
          }

          results.add( new Double( temp ) );
        }
        else if( m_splitFun.equals( SPLIT_GINI ) ) {
          temp = getGINI( stats, (Attribute)atts.elementAt( i ));

          if( temp > val ) {
            val = temp;
            pos = i;
          }
        }
      }
    }

    return (Attribute)atts.elementAt( pos );
  }

  // Private methods

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
  private void getExampleCounts( AttributeMask mask,
    Iterator examples, int counts[], boolean[] reachedHere )
  {
    // Zero any values currently in stats.
    for( int i = 0; i < counts.length; i++ )
      counts[i] = 0;

    int i = 0;

    // Loop through and get totals.
    while( examples.hasNext() ) {
      int[] example = (int[])examples.next();

      if( mask.matchMask( example ) ) {
        counts[ example[0] ]++;   // Increment appropriate
                                  // target count.
        if( reachedHere != null && reachedHere.length > i )
          reachedHere[i] = true;
      }

      i++;
    }
  }

  /**
   * Creates a formatted HTML message to display when a
   * leaf node is added.  This is a convenience method -
   * it makes the learnDT() method less cluttered.
   *
   * @param step The current leaf addition step (which
   *        identifies the message that should be
   *        returned).
   *
   * @param label The label for the new leaf.
   *
   * @return An HTML text string, suitable for display
   *         in a GUI panel.
   */
  private String createLeafMsg( int step, String label )
  {
    StringBuffer msg =
      new StringBuffer( "<html><font size=\"-1\">" );

    if( step == 1 )
      msg.append( "No examples reach this position. " +
        "Adding new leaf with default target class " +
        "<font color=\"yellow\">" + label + "</font>." );
    else if( step == 2 )
      msg.append( "All examples have the same target " +
        "classification. Adding new leaf with common " +
        "target class <font color=\"yellow\">" + label +
        "</font>." );
    else if( step == 3 )
      msg.append( "The set of attributes available for " +
        "splitting is empty. Adding new leaf " +
        "with most common target class" +
        "<font color=\"yellow\">" + label + "</font>." );

    return msg.append( "</font>" ).toString();
  }

  /**
   * Creates a formatted HTML message to display when an
   * internal node is added.  This is a convenience method -
   * it makes the learnDT() method less cluttered.
   *
   * @param step The current internal addition step (which
   *        identifies the message that should be
   *        returned).
   *
   * @param label The label for the new node.
   *
   * @return An HTML text string, suitable for display
   *         in a GUI panel.
   */
  private String createInternalMsg( int step, String label )
  {
    StringBuffer msg =
      new StringBuffer( "<html><font size=\"-1\">" );

    if( step == 1 )
      msg.append( "Choosing the best attribute to " +
        "split on, based on " + m_splitFun + " criteria." );
    else if( step == 2 )
      msg.append( "Creating new internal node for the best " +
        "attribute, <font color=\"yellow\">" + label +
        "</font>." );
    else if( step == 3 )
      msg.append( "Attached new subtree along branch " +
        "<font color=\"yellow\">" + label + "</font>." );

    return msg.append( "</font>" ).toString();
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
  private double entropy( int[] stats, int numExamples )
  {
    double entropy = 0;

    if( numExamples == 0 ) return 0;

    for( int i = 0; i < stats.length; i++ ) {
      if( stats[i] == 0 ) continue;

      // Unfortunately, the Java math class only
      // supports log{base e}...
      entropy -= ((double)stats[i])/numExamples*
        (Math.log( ((double)stats[i])/numExamples) ) / Math.log( 2.0 );
    }

    return entropy;
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
  private double getGain( int stats[], Attribute att )
  {
    // First, we calculate the entropy of the set
    // of examples before splitting (using the counts
    // in the stats array).
    int numExamples = 0;

    for( int i = 0; i < stats.length; i++ )
      numExamples += stats[i];

    if( numExamples == 0 ) return 0;

    double originalEntropy = entropy( stats, numExamples );

    // Now, we determine the entropy after splitting.
    double splitEntropy = 0;
    int[][] attStats = att.getStatsArray();

    // Loop over all possible values.
    for( int j = 0; j < attStats.length; j++ ) {
      int numSubsetExamples = 0;

      // Determine number of examples along this path.
      for( int k = 0; k < attStats[j].length; k++ )
        numSubsetExamples += attStats[j][k];

        splitEntropy +=
          ((double)numSubsetExamples)/numExamples*
          entropy( attStats[j], numSubsetExamples );
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
  private double getGainRatio( int stats[], Attribute att )
  {
    // We recompute some of the same quantities
    // calculated in the gain method here - it would
    // be more efficient to merge the gain calculation
    // here.
    int numExamples = 0;

    for( int i = 0; i < stats.length; i++ )
      numExamples += stats[i];

    if( numExamples == 0 ) return 0;

    // Compute the gain.
    double gain = getGain( stats, att );

    // Compute the SplitInformation term.
    // (which is the entropy of the examples with
    // respect to the attribute values of att).
    int[] splitInfoStats = new int[ att.getNumValues() ];

    int[][] attStats = att.getStatsArray();

    // Loop over all possible values.
    for( int j = 0; j < attStats.length; j++ ) {
      // Determine number of examples along this path.
      for( int k = 0; k < attStats[j].length; k++ )
        splitInfoStats[j] += attStats[j][k];
    }

    double splitInfo = entropy( splitInfoStats, numExamples );
    return gain/splitInfo;
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
  private double getGINI( int stats[], Attribute att )
  {
    // Determine the total number of examples.
    int numExamples = 0;

    for( int i = 0; i < stats.length; i++ )
      numExamples += stats[i];

    if( numExamples == 0 ) return 0;

    double giniScore = 0;
    int[][] attStats = att.getStatsArray();

    // Loop over all possible values.
    for( int j = 0; j < attStats.length; j++ ) {
      int numSubsetExamples = 0;
      int sumOfSquares      = 0;

      // Determine number of examples along this path.
      for( int k = 0; k < attStats[j].length; k++ ) {
        sumOfSquares += attStats[j][k] * attStats[j][k];
        numSubsetExamples += attStats[j][k];
      }

      if( numSubsetExamples != 0 )
        giniScore += ((double)sumOfSquares)/numSubsetExamples;
    }

    // Now, compute the second term in the GINI score.
    for( int l = 0; l < stats.length; l++ )
      giniScore -= ((double)stats[l])*((double)stats[l]) / numExamples;

    // Finally, divide by the total number of examples.
    giniScore /= numExamples;
    return giniScore;
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
  private double errorRE( int numExamplesReachParent,
                          int numExamplesIncorrectClass )
  {
    // Include Laplacian correction.
    int total = 1 + numExamplesIncorrectClass;

    return ((double)total)/(numExamplesReachParent + 2);
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
  private double errorBar( double mean, int size )
  {
    return Math.sqrt( mean * (1 - mean) / size ) * m_pessPruneZScore;
  }

  /**
   * Creates a formatted HTML message to display during
   * reduced error pruning.  This is a convenience method -
   * it makes the pruneReducedErrorDT() method less cluttered.
   *
   * @param errCurrent The error produced by the current tree.
   *
   * @param errPrune The error that would result if the tree
   *        was pruned.
   *
   * @return An HTML text string, suitable for display
   *         in a GUI panel.
   */
  private String createPruningMsg( double errCurrent, double errPrune )
  {
    String errCurrentString = Double.toString( errCurrent );

    if( errCurrentString.length() > 5 )
     errCurrentString = errCurrentString.substring( 0, 4 );

    String errPruneString = Double.toString( errPrune );

    if( errPruneString.length() > 5 )
      errPruneString = errPruneString.substring( 0, 4 );

    StringBuffer msg =
      new StringBuffer( "<html><font size=\"-1\">" );

    msg.append( "Current error = " + errCurrentString +
                ", pruning error = " + errPruneString + "." );

    return msg.append( "</font>" ).toString();
  }
}
