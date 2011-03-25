package ai.decision.gui;

import java.awt.*;

/**
 * The AbstractTreeNode class provides underlying functionality
 * for graphical decision tree nodes.
 *
 * <p>
 * <b>Change History:</b>
 *
 * <p><pre>
 * Name:            Date:            Change:
 * =============================================================
 * J. Kelly         Jun-25-2000      Created.
 * J. Kelly         Oct-10-2000      Changed to AbstractTreeNode.
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
public abstract class AbstractTreeNode
{
  // Instance data members

  int m_xCoord;     // X-coord of node center.
  int m_yCoord;     // Y-coord of node top.
  int m_prelim;     // Preliminary x position.
  int m_modifier;   // Position modifier.

  int m_width;      // Node width.
  int m_height;     // Node height.

  int              m_arcNum;       // Arc number.
  AbstractTreeNode m_parent;       // Parent of this node.
  AbstractTreeNode m_leftSibling;  // Left  sibling.
  AbstractTreeNode m_rightSibling; // Right sibling.
  AbstractTreeNode m_leftNeighbor; // Left adjacent neighbor.

  AbstractTreeNode[] m_children;   // Children (if any) of
                                   // this node.

  // Constructors

  /**
   * Builds a basic graphical tree node.
   *
   * @param parent The parent node for this node.  If this
   *        value is null, this node is the root of the
   *        decision tree.
   *
   * @param arcNum The parent arc that this node descends
   *        from.  The leftmost arc always has arc number
   *        0.
   */
  public AbstractTreeNode( AbstractTreeNode parent, int arcNum )
  {
    m_parent = parent;
    m_arcNum = arcNum;
    m_leftSibling  = null;
    m_rightSibling = null;

    // No children or neighbor initially.
    m_children     = null;
    m_leftNeighbor = null;
  }

  // Public methods

  /**
   * Identifies this node as a leaf node or an internal
   * node.
   *
   * @return true if the node is a leaf node, or false
   *         if it is an internal node.
   */
  public abstract boolean isLeaf();

  /**
   * Returns the arc number associated with the node.
   *
   * @return The arc number associated with the node.
   */
  public int getArcNum()
  {
    return m_arcNum;
  }

  /**
   * Returns the width of the node.
   *
   * @return The width of the node in pixels.
   */
  public int getWidth()
  {
    return m_width;
  }

  /**
   * Returns the height of the node.
   *
   * @return The height of the node in pixels.
   */
  public int getHeight()
  {
    return m_height;
  }

  /**
   * Returns the preliminary position value for the node.
   *
   * @return The preliminary x position of the node.
   */
  public int getPrelim()
  {
    return m_prelim;
  }

  /**
   * Sets the preliminary position value for the node.
   *
   * @param prelim The preliminary x position of the node.
   */
  public void setPrelim( int prelim )
  {
    m_prelim = prelim;
  }

  /**
   * Returns the modifier value for the node.
   *
   * @return The x modifier value for the node.
   */
  public int getModifier()
  {
    return m_modifier;
  }

  /**
   * Sets the modifier value for the node.
   *
   * @param modifier The x modifier value for the node.
   */
  public void setModifier( int modifier )
  {
    m_modifier = modifier;
  }

  /**
   * Returns the x-coordinate of the node.
   *
   * @return The x-coordinate of the node.
   */
  public int getXCoord()
  {
    return m_xCoord;
  }

  /**
   * Sets the x-coordinate for the node.
   *
   * @param xCoord The x-coordinate of the <i>center</i> of the node.
   */
  public void setXCoord( int xCoord )
  {
    m_xCoord = xCoord;
  }

  /**
   * Returns the y-coordinate of the node.
   *
   * @return The y-coordinate of the node.
   */
  public int getYCoord()
  {
    return m_yCoord;
  }

  /**
   * Sets the y-coordinate for the node.
   *
   * @param yCoord The y-coordinate of the <i>top</i> of the node.
   */
  public void setYCoord( int yCoord )
  {
    m_yCoord = yCoord;
  }

  /**
   * Returns the parent node for this node.
   *
   * @return The parent node for this node, or null
   *         if this node represents the root of the
   *         tree.
   */
  public AbstractTreeNode getParent()
  {
    return m_parent;
  }

  /**
   * Returns the left neighbor for this node.
   *
   * @return The left neighbor of this node, or null
   *         if the node has no left neighbor.
   */
  public AbstractTreeNode getLeftNeighbor()
  {
    return m_leftNeighbor;
  }

  /**
   * Sets the left neighbor for this node.
   *
   * @param leftNeighbor The left neighbor for this
   *        node.  The value can be null if the node
   *        has no left neighbor.
   */
  public void setLeftNeighbor( AbstractTreeNode leftNeighbor )
  {
    m_leftNeighbor = leftNeighbor;
  }

  /**
   * Returns the left sibling for this node, or null
   * if the node has no left sibling.
   *
   * @return The left sibling for this node.
   */
  public AbstractTreeNode getLeftSibling()
  {
    return m_leftSibling;
  }

  /**
   * Returns the right sibling for this node, or null
   * if the node has no left sibling.
   *
   * @return The right sibling for this node.
   */
  public AbstractTreeNode getRightSibling()
  {
    return m_rightSibling;
  }

  /**
   * Sets the left sibling for this node.  This value
   * can be null, which indicates that the node has
   * no left sibling.
   *
   * @param leftSibling The left sibling for this node.
   */
  public void setLeftSibling( AbstractTreeNode leftSibling )
  {
    m_leftSibling = leftSibling;
  }

  /**
   * Sets the right sibling for this node.  This value
   * can be null, which indicates that the node has
   * no right sibling.
   *
   * @param rightSibling The right sibling for this node.
   */
  public void setRightSibling( AbstractTreeNode rightSibling )
  {
    m_rightSibling = rightSibling;
  }

  /**
   * Returns the maxium number of children this
   * node can accomodate - the node does not
   * necessarily have the maxiumum number of children
   * currently attached, however.
   *
   * @return The maximum number of children beneath
   *         this node.
   */
  public int getMaxNumChildren()
  {
    if( m_children == null ) return 0;
      return m_children.length;
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
  public AbstractTreeNode getChild( int childNum )
  {
    if( this.isLeaf() ) return null;

    if( childNum < 0 || childNum > (m_children.length - 1) )
      throw new
        ArrayIndexOutOfBoundsException( "No child " +
            "exists at position " + childNum + "." );

    return m_children[ childNum ];
  }

  /**
   * Attaches the supplied node to the current node at
   * specified child position.  The method does not check
   * to see if a node already exists at the insertion
   * position.  If this node is a leaf node, the method
   * has no effect.
   *
   * @throws IndexOutOfBoundsException If the supplied
   *         child number is less than 0 or greater
   *         than the number of possible children at
   *         this node.
   */
  public void setChild( int childNum, AbstractTreeNode node )
  {
    if( this.isLeaf() ) return;

    if( childNum < 0 || childNum > (m_children.length - 1) )
      throw new
        ArrayIndexOutOfBoundsException( "Cannot add " +
            "child at position " + childNum + "." );

    m_children[ childNum ] = node;
  }

  /**
   * Determines if the supplied point lies anywhere within
   * this node's bounding box (including the border of the
   * box), or in the bounding box of any 'vacant' nodes
   * attached to this node.
   *
   * @return A reference to the node that contains the supplied
   *         coordinates, or null otherwise.
   */
  public abstract AbstractTreeNode withinBoundingBox( int x, int y );

  /**
   * Paints this node on the current graphics context.
   *
   * @param g The graphics context on which to paint.
   */
  public abstract void paintNode( Graphics g );
}
