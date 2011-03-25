package ai.decision.gui;

import java.awt.*;
import java.awt.event.*;
import javax.swing.*;
import java.util.*;
import ai.decision.algorithm.*;
import ai.common.*;

/**
 * A panel that formats and displays a tree decision tree.
 * This class uses a node-positioning algorithm described in
 * &quot;A Node-Positioning Algorithm for General Trees&quot;
 * by John Q. Walker II, in <i>Software - Practise and
 * Experience</i>, Vol. 20(7), 685-705 (July 1990).
 *
 * <p>
 * A TreeLayoutPanel is only responsible for computing and
 * rendering a graphical representation of a decision tree.
 * Mouse events (i.e. clicks, drag operations) should be
 * handled by a subclass.
 *
 * <p>
 * <b>Change History:</b>
 *
 * <p><pre>
 * Name:            Date:            Change:
 * =============================================================
 * J. Kelly         Nov-15-2000      Created.
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
public class TreeLayoutPanel
  extends JPanel
  implements TreeChangeListener
{
  // Class data members

  /**
   * The distance between adjacent levels in the tree.
   * (this is fixed).
   */
  public static final int LEVEL_SEPARATION = 60;

  /**
   * The minimum distance between adjacent siblings
   * in a subtree.
   */
  public static final int SIBLING_SEPARATION = 50;

  /**
   * The minimum distance between adjacent subtrees.
   */
  public static final int SUBTREE_SEPARATION = 50;

  /**
   * Size (in pixels) of the current border -
   * there is a least this much whitespace between
   * any tree node and the edge of the panel.
   */
  public static int BORDER_SIZE = 15;

  /**
   * The <i>scaled</i> left offset of the center of the
   * root node of the tree.  All nodes adjust their
   * x-coordinate by this value prior to rendering.
   */
  public static int ROOT_OFFSET = 0;

  /**
   * The left extent of the tree from the center of
   * the root node.  This value is used to adjust
   * the size of the panel dynamically, so it's always
   * just large enought to display the tree.
   */
  public static int LEFT_EXTENT = 0;

  /**
   * The right extent of the tree from the center of
   * the root node.  This value is used to adjust the
   * size of the panel dynamically, so it's always
   * just large enough to display the tree.
   */
  public static int RIGHT_EXTENT = 0;

  /**
   * The total height of the tree, from the top of
   * the root node to the bottom of the deepest leaf.
   * This value is used to adjust the size of the
   * panel dynamically, so it's always just large
   * enough to display the tree.
   */
  public static int TREE_HEIGHT = 0;

  /**
   * The current scaling factor.  Nodes automatically
   * check this value when they draw themselves.
   */
  public static double SCALING_FACTOR = 1.0;

  //-------------------------------------------------------

  // Instance data members

  /*
   * A list containing the previous node at each level
   * (i.e. adjacent neighbor to the left).
   */
  Vector m_previousNodeList;

  /**
   * Value used to determine the absolute x-cooridinate
   * of a node with respect to the apex node of the tree.
   */
  int m_xTopAdjustment = 0;

  /**
   * Value used to determine the absolute y-coordinate
   * of a node with respect to the apex node of the tree.
   */
  int m_yTopAdjustment = 0;

  Vector      m_nodes;     // List of visual tree nodes.
  Font        m_font;      // Font used to render all text labels.

  JViewport   m_viewport;  // Viewport that displays the tree.

  ComponentManager m_manager;

  // Constructors

  /**
   * Builds a new TreeLayoutPanel.  The panel has a white
   * background by default.
   *
   * @param manager The global component manager.
   *
   * @throws NullPointerException If the supplied
   *         ComponentManager is null.
   */
  public TreeLayoutPanel( ComponentManager manager )
  {
    if( manager == null )
      throw new
        NullPointerException( "Component manager is null." );

    m_manager          = manager;
    m_nodes            = new Vector();
    m_previousNodeList = new Vector();

    // Now, build the panel structure.
    buildPanel();
  }

  // Public methods

  /**
   * Informs that panel that it's being tracked by
   * a viewport.  The panel will automatically adjust
   * the viewport window so that it displays the
   * active (most recently modified) portion of the
   * tree.
   *
   * <p>
   * The panel also uses the viewport width to compute
   * the centering coordinates for the decision tree.
   */
  public void setViewport( JViewport viewport )
  {
    m_viewport = viewport;
  }

  /**
   * Notifies the panel that a new tree has been
   * created.  The panel will clear itself in response.
   */
  public void notifyNewTree()
  {
    // Remove all current nodes.
    m_nodes.clear();
    m_previousNodeList.clear();

    // Reset the width and height of the panel.
    setMinimumSize(   new Dimension( 0, 0 ) );
    setPreferredSize( new Dimension( 0, 0 ) );
    revalidate();

    // Repaint the panel.
    repaint();
  }

  /**
   * Notifies the panel that a node has been added to
   * the tree.  The panel recomputes the optimal display
   * configuration for the tree with the new node attached
   * and then repaints the display.
   *
   * @param node The most recently added node.
   */
  public void notifyNodeAdded( DecisionTreeNode node )
  {
    if( node == null )
      throw new NullPointerException( "Node is null." );

    // First, clear any selected rows in the dataset table.
    if( m_manager.getDatasetPanel() != null )
      m_manager.getDatasetPanel().clearSelectedRows();

    // Determine if a visual representation of the
    // node's parent already exists - this gives us
    // a place to attach the node.
    VisualTreeNode visParent = null;
    int arcNum = 0;

    for( int i = 0; i < m_nodes.size(); i++ ) {
      VisualTreeNode currNode = (VisualTreeNode)m_nodes.elementAt(i);

      if( node.getParent() == currNode.getDecisionTreeNode() ) {
        visParent = currNode;
        arcNum = node.getParent().getChildPosition( node );
        break;
      }
    }

    // Add the node to our internal node list.
    VisualTreeNode newNode =
      new VisualTreeNode( visParent, arcNum,
                          node, this.getFont(), getGraphics() );

    m_nodes.add( newNode );

    // Reset the root position.  If there's a viewport
    // available, then we position the root at the
    // horizontal center position.
    VisualTreeNode root = (VisualTreeNode)m_nodes.elementAt( 0 );
    root.setXCoord( 0 );
    root.setYCoord( 0 );

    // Reset the tree size, as it may have changed.
    LEFT_EXTENT  = 0;
    RIGHT_EXTENT = 0;
    TREE_HEIGHT  = 0;

    // Reposition all nodes in the tree - this may take
    // a moment or two.
    positionTree();

    // The call to positionTree() might have changed
    // the required panel size - we also have to take
    // into account the current scaling factor.
    handlePanelAdjust( newNode );

    // We can't modify the training/testing datasets (by moving
    // examples around) while there are nodes in the tree.
    m_manager.getDatasetMenu().setMoveExamplesEnabled( false );

    // We've recalculated, now repaint.
    repaint();
  }

  /**
   * Notifies the panel that a node had been removed
   * from the tree.  The panel recomputes the optimal
   * display configuration for the tree with the
   * node removed and then repaints the display.
   *
   * <p>
   * This method can be used to remove <i>any</i> visual
   * node within the tree - the structure of the entire
   * tree is rebuilt before any painting occurs.
   *
   * @param node The most recently removed node.
   */
  public void notifyNodeRemoved( DecisionTreeNode node )
  {
    if( node == null )
      throw new NullPointerException( "Node is null." );

    // First, clear any selected rows in the dataset table.
    if( m_manager.getDatasetPanel() != null )
      m_manager.getDatasetPanel().clearSelectedRows();

    // Find the visual representation of the
    // node to remove. We have to tell the parent to
    // replace the deleted node with a 'vacant' node.
    VisualTreeNode visParent = null;
    int nodeNum = 0;

    for( int i = 0; i < m_nodes.size(); i++ ) {
      VisualTreeNode currNode = (VisualTreeNode)m_nodes.elementAt(i);

      if( node == currNode.getDecisionTreeNode() ) {
        if( currNode.getParent() != null )
        visParent = (VisualTreeNode)currNode.getParent();

        nodeNum = i;
        break;
      }
    }

    // Remove the node from the node list.
    pruneSubtree( nodeNum );

    // Reset the root position.  If there's a viewport
    // available, then we position the root at the
    // horizontal center position.
    if( m_nodes.size() != 0 ) {
      VisualTreeNode root = (VisualTreeNode)m_nodes.elementAt( 0 );
      root.setXCoord( 0 );
      root.setYCoord( 0 );
    }

    // Reset the tree size, as it may have changed.
    LEFT_EXTENT  = 0;
    RIGHT_EXTENT = 0;
    TREE_HEIGHT  = 0;

    // Reposition all nodes in the tree - this may take
    // a moment or two.
    positionTree();

    // The call to positionTree() might have changed
    // the required panel size - we also have to take
    // into account the current scaling factor.
    handlePanelAdjust( visParent );

    // We can't modify the training/testing datasets (by moving
    // examples around) while there are nodes in the tree.
    if( m_manager.getAlgorithm().getTree().isEmpty() )
      m_manager.getDatasetMenu().setMoveExamplesEnabled( true );

    // We've recalculated, now repaint.
    repaint();
  }

  /**
   * Notifies the panel that a node had been modified.
   *
   * @param node The most recently modified node.
   */
  public void notifyNodeModified( DecisionTreeNode node )
  {
    // All we have to do is repaint the display,
    // VisualTreeNodes already know how to handle
    // flagged / modified nodes.
    repaint();
  }

  /**
   * Paints a visual representation of the current tree.
   */
  public void paintComponent( Graphics g )
  {
    super.paintComponent( g );

    // Paint each node.
    for( int i = 0; i < m_nodes.size(); i++ )
      ((VisualTreeNode)m_nodes.elementAt(i)).paintNode(g);
  }

  // Protected methods

  /**
   * Prunes off the subtree whose root is at position
   * <code>pos</code> in the internal visual node list.
   * The node and any nodes below it are removed from
   * the tree.
   *
   * @param pos The position of the node to remove
   *        in the internal visual node list.
   */
  protected void pruneSubtree( int pos )
  {
    // Grab the node.
    VisualTreeNode pruneRoot =
      (VisualTreeNode)m_nodes.elementAt( pos );

    pruneSubtree( pruneRoot );
  }

  /**
   * Prunes off the subtree whose rooted at <code>pruneRoot</code>.
   * The node and any nodes below it are removed from the tree.
   *
   * @param pruneRoot The root of the subtree to prune.
   */
  protected void pruneSubtree( VisualTreeNode pruneRoot )
  {
    // If the node has a visual parent, tell
    // the parent to replace the link to the node
    // with a link to a new 'vacant' node.
    if( pruneRoot.getParent() != null )
      ((VisualTreeNode)pruneRoot.getParent()).removeChild( pruneRoot );

      // Now, tell the vector to remove all the node's children.
      recursiveRemoveSubtree( pruneRoot );
  }

  /**
   * Recursively descends through the tree, removing
   * the supplied visual node and any descendants from
   * the internal visual node list.
   *
   * @param node The root node of the subtree to remove.
   */
  protected void recursiveRemoveSubtree( VisualTreeNode node )
  {
    // Remove this node from the vector.
    m_nodes.remove( node );

    // Recursively remove all it's children.
    if( node.isLeaf() ) return;

    for( int i = 0; i < node.getMaxNumChildren(); i++ )
      if( node.getChild( i ) instanceof VisualTreeNode )
      this.recursiveRemoveSubtree( (VisualTreeNode)node.getChild( i ) );
  }

  /**
   * Handles panel size adjustment and tree positioning.
   * This method adjusts the ROOT_OFFSET value used when
   * drawing the tree, based on the current LEFT_EXTENT,
   * RIGHT_EXTENT and SCALING_FACTOR values.
   *
   * @param newNode The most recently added node.  If
   *        this parameter is non-null, the node is
   *        positioned in the view window.
   */
  protected void handlePanelAdjust( VisualTreeNode newNode )
  {
    // First, we resize the panel.
    int treeWidth  = (int)
      Math.round( (LEFT_EXTENT + RIGHT_EXTENT + 1) * SCALING_FACTOR );
    int treeHeight = (int)
      Math.round( TREE_HEIGHT * SCALING_FACTOR );

    int panelWidth  = 0;
    int panelHeight = 0;
    int viewWidth   = 0;
    int viewHeight  = 0;

    // Now, shift the entire tree for optimal viewing.
    if( m_viewport == null ) {
      // Center the whole tree in the middle of the panel.
      ROOT_OFFSET = treeWidth/2;

      panelWidth  = treeWidth +  2 * BORDER_SIZE;
      panelHeight = treeHeight + 2 * BORDER_SIZE;
    }
    else {
      // We have a viewport.  If the view window
      // is large enough to accomodate the scaled
      // tree, then we center the tree in the window.
      // Otherwise, we push the tree as far to the
      // left as possible, and then center the view
      // on the supplied coordinates.

      viewWidth  = m_viewport.getExtentSize().width;
      viewHeight = m_viewport.getExtentSize().height;

      // Panel height is largest of tree height and
      // view height.
      panelHeight = treeHeight + 2 * BORDER_SIZE >
        viewHeight ? treeHeight + 2 * BORDER_SIZE : viewHeight;

      int scaledLeftExtent  = (int)
        Math.round( LEFT_EXTENT * SCALING_FACTOR );
      int scaledRightExtent = treeWidth - scaledLeftExtent;

      // Case 1: The tree is skinny enough that the root
      // can be centered in the current viewport.
      if( scaledLeftExtent  + BORDER_SIZE <= viewWidth/2 &&
          scaledRightExtent + BORDER_SIZE <= viewWidth/2 ) {
        ROOT_OFFSET = viewWidth/2;
        panelWidth  = viewWidth;
      }

      // Case 2: The tree is skinny, but can't be centered
      // in the current viewport - so push it as far to
      // left as possible.
      else if( treeWidth + 2 * BORDER_SIZE <= viewWidth ) {
        ROOT_OFFSET = scaledLeftExtent + BORDER_SIZE;
        panelWidth  = viewWidth;
      }

      // Case 3: The tree is too wide to fit in the
      // viewport, so we push it as far to the left
      // as possible and expand the panel size.
      else {
        ROOT_OFFSET = scaledLeftExtent + BORDER_SIZE;
        panelWidth  = treeWidth + 2 * BORDER_SIZE;
      }
    }

    // Resize the panel.
    setPreferredSize( new Dimension( panelWidth, panelHeight ) );

    // If necessary, we can now adjust the viewport view
    // so that it is centered over the active area of the
    // tree (i.e. the area where the last node was added
    // or removed).
    if( m_viewport != null && newNode != null ) {
      int scaledUlcX   = (int)(ROOT_OFFSET +
        Math.round( (newNode.getXCoord() - newNode.getWidth()/2)
          * SCALING_FACTOR ));
      int scaledUlcY   = (int)(BORDER_SIZE + Math.round( newNode.getYCoord()
          * SCALING_FACTOR ));
      int scaledWidth  = (int)Math.round( newNode.getWidth() );
      int scaledHeight = (int)Math.round( newNode.getHeight() );

      m_viewport.scrollRectToVisible(
        new Rectangle( scaledUlcX, scaledUlcY, scaledWidth, scaledHeight ) );
    }

    revalidate();
  }

  // Private methods

  /**
   * Builds and arranges the various GUI components
   * for this panel.
   */
  private void buildPanel()
  {
    // Make the panel opaque - this helps speed up the drawing process.
    setOpaque( true );

    // White background - easy to see.
    setBackground( Color.white );

    // Font setup - 9 pt. version of the current panel font.
    setFont( getFont().deriveFont( Font.PLAIN, 9 ) );
  }

  //-------------------------------------------------------

  // Node-positioning algorithm implementation

  /**
   * Properly positions all nodes in the tree.  This
   * requires two recursive passes through all the nodes.
   */
  private void positionTree()
  {
    // First, we grab a reference to the root node
    // of the tree.  If the tree is empty, we don't
    // do anything.
    if( m_nodes.size() == 0 ) return;

    AbstractTreeNode root = (AbstractTreeNode)m_nodes.elementAt( 0 );

    // Initialize the list of previous nodes at each level.
    initPreviousNodeList();

    // Perform the first walk through the tree,
    // setting the preliminary positions for all the nodes.
    firstWalk( root, 0 );

    // Figure out how to adjust node positions
    // with respect to the root of the tree.
    m_xTopAdjustment = root.getXCoord() - root.getPrelim();
    m_yTopAdjustment = root.getYCoord();

    // Perform the second walk through the tree,
    // setting the absolute positions of each node.
    secondWalk( root, 0, 0 );

    // All finished!
  }

  /**
   * Initializes the list of previous nodes at each
   * level in the tree.
   */
  private void initPreviousNodeList()
  {
    // Simply set all the current positions
    // in the list to null.
    for( int i = 0; i < m_previousNodeList.size(); i++ )
      m_previousNodeList.setElementAt( null, i );
  }

  /**
   * Returns the previous node at a certain level.
   *
   * @param level The level to retrieve the previous
   *        node from.  The identity of the previous
   *        node depends on the position of the
   *        algorithm within the tree.
   *
   * @return The previous node at the current level.
   */
  private AbstractTreeNode getPrevNodeAtLevel( int level )
  {
    if( level >= m_previousNodeList.size() )
      return null;  // No node at the specified level

    return (AbstractTreeNode)m_previousNodeList.elementAt( level );
  }

  /**
   * Set the 'current' previous node for a level.
   *
   * @param level The level to set the previous node at.
   *
   * @param node The 'new' previous node.
   */
  private void setPrevNodeAtLevel( int level, AbstractTreeNode node )
  {
    if( level < m_previousNodeList.size() )
      m_previousNodeList.setElementAt( node, level );
    else
      m_previousNodeList.add( node );
  }

  /**
   * Perform a post-order walk through the tree, setting
   * the preliminary x-coordinate and adjusting the modifier
   * value for each node.
   *
   * @param node The node to begin the post-order walk at.
   *
   * @param level The current level.
   */
  private void firstWalk( AbstractTreeNode node, int level )
  {
    // Setup the reference to the previous node at this level.
    node.setLeftNeighbor( getPrevNodeAtLevel( level ) );
    setPrevNodeAtLevel( level, node );

    node.setModifier( 0 );  // Set default modifier

    if( node.isLeaf() ) {
      // Preliminary x-coord is based on the
      // x-coord of the left sibling, if available.
      // (This accounts for the variable node sizes,
      //  and required separation between siblings).
      if( node.getLeftSibling() != null ) {
        AbstractTreeNode leftSibling = node.getLeftSibling();
        node.setPrelim(
          leftSibling.getPrelim() + SIBLING_SEPARATION +
          meanNodeSize( leftSibling, node ) );
      }
      else {
        // No siblings to the left.
        node.setPrelim( 0 );
      }
    }
    else {
      // The node is not a leaf, so we have to
      // recurively descend to all of its offspring.
      AbstractTreeNode leftmost  = null;
      AbstractTreeNode rightmost = null;

      // Get the leftmost child and recursively descend.
      leftmost = rightmost = node.getChild( 0 );
      firstWalk( leftmost, level + 1 );

      while( rightmost.getRightSibling() != null ) {
        rightmost = rightmost.getRightSibling();
        firstWalk( rightmost, level + 1 );
      }

      // Find the midpoint between the leftmost
      // and rightmost children - this will become
      // the position of the parent.
      int midpoint = ( leftmost.getPrelim() + rightmost.getPrelim() )/2;

      if( node.getLeftSibling() != null ) {
        AbstractTreeNode leftSibling = node.getLeftSibling();
        node.setPrelim(
          leftSibling.getPrelim() + SIBLING_SEPARATION +
          meanNodeSize( leftSibling, node ) );
        node.setModifier( node.getPrelim() - midpoint );

        // Shift the node (and any intermediate nodes
        // to avoid collisions).
        apportion( node, level );
      }
      else {
        // No siblings to the left.
        node.setPrelim( midpoint );
      }
    }
  }

  /**
   * Shifts subtrees beneath a node to ensure that
   * there are no collisions.
   *
   * @param node The node whose subtrees are examined
   *        and shifted.
   *
   * @param level The current level.
   */
  private void apportion( AbstractTreeNode node, int level )
  {
    AbstractTreeNode leftmost     = node.getChild( 0 );
    AbstractTreeNode leftNeighbor = leftmost.getLeftNeighbor();
    int compareDepth = 1;

    while( leftmost != null && leftNeighbor != null ) {
      // Figure out where leftmost should be
      // with respect to it's neighbor.
      int leftModSum  = 0;
      int rightModSum = 0;

      AbstractTreeNode ancestorLeftmost = leftmost;
      AbstractTreeNode ancestorNeighbor = leftNeighbor;

      for( int i = 0; i < compareDepth; i++ ) {
        // We're moving back up the tree,
        // figuring out how far we're going to
        // have to move things.
        ancestorLeftmost = ancestorLeftmost.getParent();
        ancestorNeighbor = ancestorNeighbor.getParent();

        rightModSum += ancestorLeftmost.getModifier();
        leftModSum  += ancestorNeighbor.getModifier();
      }

      // Compute the actual move distance, and
      // adjust smaller internal subtrees so that
      // everything is still balanced.
      int moveDistance = (leftNeighbor.getPrelim() + leftModSum +
        SUBTREE_SEPARATION + meanNodeSize( leftmost, leftNeighbor )) -
        (leftmost.getPrelim() + rightModSum);

      if( moveDistance > 0 ) {
        // Check for and count any interior
        // subtrees that need to be repositioned.
        AbstractTreeNode tempNode = node;
        int leftSiblingCount = 0;

        while( tempNode != null && tempNode != ancestorNeighbor ) {
          leftSiblingCount++;
          tempNode = tempNode.getLeftSibling();
        }

        // If we encountered subtrees that need
        // to be shifted, shift them.
        if( tempNode != null ) {
          int portion = (int)Math.round(
            ((double)moveDistance)/leftSiblingCount );
          tempNode = node;

          while( tempNode != ancestorNeighbor ) {
            tempNode.setPrelim( tempNode.getPrelim() + moveDistance );
            tempNode.setModifier( tempNode.getModifier() + moveDistance );

            moveDistance -= portion;
            tempNode = tempNode.getLeftSibling();
          }
        }
        else {
          // We don't need to move anything.
          return;
        }
      }

      // Find the leftmost descendent of 'node'
      // at the next lower level, and compare
      // it's position against that of it's
      // neighbor (checking for a collision).
      compareDepth++;

      if( leftmost.isLeaf() )
        leftmost = getLeftmost( node, 0, compareDepth );
      else
        leftmost = leftmost.getChild( 0 );

      if( leftmost != null )
        leftNeighbor = leftmost.getLeftNeighbor();
    }
  }

  /**
   * Returns the leftmost descendant of a node at a given
   * depth.
   *
   * @param node The node to recursively search under.
   *
   * @param level The current <i>relative</i> level,
   *        in relation to the level we were at when
   *        the method was first called.
   *
   * @param depth Maximum number of levels to descend.
   *
   * @return The leftmost node at the requested depth,
   *         or null if the supplied node is a leaf node.
   */
  private AbstractTreeNode getLeftmost( AbstractTreeNode node,
                                        int level, int depth )
  {
    // Base cases.
    if( level >= depth )
      return node;
    else if( node.isLeaf() )
      return null;
    else {
      // Recursive case.
      AbstractTreeNode rightmost = node.getChild( 0 );
      AbstractTreeNode leftmost  =
        getLeftmost( rightmost, level + 1, depth );

      // Post-order walk through the subtree below node.
      while( leftmost == null && rightmost.getRightSibling() != null ) {
        rightmost = rightmost.getRightSibling();
        leftmost  = getLeftmost( rightmost, level + 1, depth );
      }

      return leftmost;
    }
  }

  /**
   * Performs a pre-order walk through the tree, setting
   * the final x and y coordinates for all nodes.
   *
   * <p>
   * Final x-coordinate values are determined by summing
   * a node's preliminary placement value and the
   * modifiers of all the node's ancestors.
   *
   * @param node The node at which to start the
   *        recursive traversal.
   *
   * @param level The level for the supplied node.
   *
   * @param modSum The current modifier sum.
   */
  private void secondWalk( AbstractTreeNode node, int level, int modSum )
  {
    node.setXCoord( m_xTopAdjustment + node.getPrelim() + modSum );
    node.setYCoord( m_yTopAdjustment + level*LEVEL_SEPARATION );

    if( !node.isLeaf() )
      secondWalk( node.getChild( 0 ), level + 1, modSum + node.getModifier() );

    if( node.getRightSibling() != null )
      secondWalk( node.getRightSibling(), level, modSum );

    // An addition to the original algorithm -
    // If this is the leftmost node in the tree thus
    // far, we adjust LEFT_EXTENT.  If this is the
    // rightmost node in the tree thus far, we adjust
    // RIGHT_EXTENT.
    if( node.getXCoord() - node.getWidth()/2 < (- LEFT_EXTENT) )
      LEFT_EXTENT  = - node.getXCoord() + node.getWidth()/2;
    else if( node.getXCoord() + node.getWidth()/2 > RIGHT_EXTENT )
      RIGHT_EXTENT = node.getXCoord() + node.getWidth()/2;

    // If this node is the lowest node in the tree, we
    // adjust the overall height of the tree.
    if( node.getYCoord() + node.getHeight() - 1 > TREE_HEIGHT )
      TREE_HEIGHT = node.getYCoord() + node.getHeight() - 1;
  }

  /**
   * Returns the mean width of the two supplied nodes.
   *
   * @return The mean width of the two supplied nodes.
   *         If one of the nodes is null, the method
   *         returns the width of the non-null node
   *         divided by 2.  If both nodes are null, the
   *         method returns 0.
   */
  private int meanNodeSize( AbstractTreeNode leftNode,
                            AbstractTreeNode rightNode )
  {
    int widthSum =
      (leftNode  != null ? leftNode.getWidth()  : 0) +
      (rightNode != null ? rightNode.getWidth() : 0);

      return widthSum/2;
  }
}
