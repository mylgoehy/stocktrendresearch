package ai.decision.gui;

import java.awt.*;
import java.awt.event.*;
import ai.decision.algorithm.*;

/**
 * The VisualTreeNode class provides a graphical representation
 * of a decision tree node.  Each VisualTreeNode consists of:
 *
 * <p>
 * <ul>
 *     <li>a rectangular box that contains the text label
 *         for the node.
 *     <li>labelled descending arcs if the node is not a
 *         leaf node.
 * </ul>
 *
 * <p>
 * <b>Change History:</b>
 *
 * <p><pre>
 * Name:            Date:            Change:
 * =============================================================
 * J. Kelly         Jun-18-2000      Created.
 * J. Kelly         Jul-12-2000      Converted nodes to use
 *                                   HSV color space for shading.
 * J. Kelly         Nov-14-2000      Converted nodes to use
 *                                   the ColorScheme class for
 *                                   shading.
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
public class VisualTreeNode
  extends AbstractTreeNode
{
  // Class data members

  private static final int TRAINING = 0;

  private static final int TESTING  = 1;

  private static int c_trainOrTest;  // Flag. Signals display of
                                     // training or testing stats.

  //-------------------------------------------------------

  DecisionTreeNode m_node;   // Underlying tree node.
  Color   m_trainingColor;   // Color for this node (training).
  Color   m_testingColor;    // Color for this node (testing).
  Font    m_font;            // Font used to draw labels.

  // Constructors

  /**
   * Builds a visual representation of a decision tree
   * node.
   *
   * @param visParent The visual parent node for this
   *        node.  If this value is null, this node
   *        is the root of the decision tree.
   *
   * @param arcNum The arc along which to 'attach'
   *        this node.  This visual node will be centered
   *        at the end of the given parent arc.
   *
   * @param node The decision tree node that this visual
   *        node will represent.
   *
   * @param font The font used to to render the label
   *        for this node.
   *
   * @param g The graphics context that will be used
   *        to render the node.
   */
  public VisualTreeNode( VisualTreeNode visParent,
    int arcNum, DecisionTreeNode node, Font font, Graphics g )
  {
    super( visParent, arcNum );

    if( node == null || g == null )
      throw new NullPointerException( "Node or graphics context is null." );

    m_node = node;

    if( font != null ) {
      m_font = font;
      g.setFont( font );
    }
    else
      m_font = g.getFont();

    // Compute the size of the rectangle required
    // to display this node's label text.
    m_width  = g.getFontMetrics().stringWidth(m_node.getLabel()) + 10;
    m_height = g.getFontMetrics().getHeight() + 4;

    // Attach this node to the parent (if there is
    // a parent).  The node can also figure out who its siblings are.
    if( m_parent != null ) {
      visParent.setChild( arcNum, this );

      if( arcNum != 0 ) {
        // Must have a left sibling.
        m_leftSibling = visParent.getChild( arcNum - 1 );
        m_leftSibling.setRightSibling( this );
      }

      if( arcNum != visParent.getMaxNumChildren() - 1 ) {
        // Must have right sibling.
        m_rightSibling = visParent.getChild( arcNum + 1 );
        m_rightSibling.setLeftSibling( this );
      }
    }

    // Compute the color(s) of this node.
    if( m_node.getTrainingEgsAtNode() == 0 )
      m_trainingColor = ColorScheme.getColor( -1.0 );
    else
      m_trainingColor =
        ColorScheme.getColor(
          ((double)m_node.getTrainingEgsCorrectClassUsingBestTrainingIndex())/
          m_node.getTrainingEgsAtNode() );

    if( m_node.getTestingEgsAtNode() == 0 )
      m_testingColor = ColorScheme.getColor( -1.0 );
    else
      m_testingColor  =
        ColorScheme.getColor(
          ((double)m_node.getTestingEgsCorrectClassUsingBestTrainingIndex())/
          m_node.getTestingEgsAtNode() );

    // If this node isn't a leaf, we build a series of
    // 'vacant' children to indicate that positions are available.
    if( !m_node.isLeaf() ) {
      // This is an internal node, so build
      // a series of 'vacant' children for the node.
      int numArcs = m_node.getArcLabelCount();
      m_children  = new AbstractTreeNode[ numArcs ];

      for( int i = 0; i < numArcs; i++ )
        m_children[i] = new VacantTreeNode( this, i );

      // One pass through the children, setting siblings.
      m_children[0].setLeftSibling( null );
      m_children[0].setRightSibling( m_children[1] );

      for( int i = 1; i < numArcs - 1; i++ ) {
        m_children[i].setLeftSibling(  m_children[i - 1] );
        m_children[i].setRightSibling( m_children[i + 1] );
      }

      m_children[numArcs - 1].setLeftSibling( m_children[numArcs - 2] );
      m_children[numArcs - 1].setRightSibling( null );
    }
  }

  // Public methods

  public boolean isLeaf()
  {
    return m_children == null;
  }

  /**
   * Returns a reference to the internal decision tree
   * node that this visual node represents.
   *
   * @return The decision tree node that this visual
   *         node represents.
   */
  public DecisionTreeNode getDecisionTreeNode()
  {
    return m_node;
  }

  /**
   * Searches through the current node's list of children
   * and attempts to locate a child that matches the
   * supplied child.  If a match is found, the reference
   * to the child is removed, leaving a vacant arc.
   *
   * @param child The child to remove.
   */
  public void removeChild( VisualTreeNode child )
  {
    if( this.isLeaf() ) return;

    // Search through the list of children, looking for a match.
    for( int i = 0; i < m_children.length; i++ ) {
      if( m_children[i] == child ) {
        m_children[i] = new VacantTreeNode( this, i );

        if( i > 0 ) {
          m_children[i].setLeftSibling( m_children[i-1] );
          m_children[i-1].setRightSibling( m_children[i] );
        }

        if( i < m_children.length - 1 ) {
          m_children[i].setRightSibling( m_children[i+1] );
          m_children[i+1].setLeftSibling( m_children[i] );
        }

        return;
      }
    }
  }

  public AbstractTreeNode withinBoundingBox( int x, int y )
  {
    // Perform the same computation that's done
    // when we draw the node.
    int ulcX   = m_xCoord - m_width/2;
    int ulcY   = m_yCoord;
    int width  = m_width;
    int height = m_height;

    // Apply scaling if required.
    if( VisualTreePanel.SCALING_FACTOR != 1.0 ) {
      ulcX   = (int)Math.round( ulcX * VisualTreePanel.SCALING_FACTOR );
      ulcY   = (int)Math.round( ulcY * VisualTreePanel.SCALING_FACTOR );
      width  = (int)Math.round( width * VisualTreePanel.SCALING_FACTOR );
      height = (int)Math.round( height * VisualTreePanel.SCALING_FACTOR );
    }

    // The border does not resize, so add border offset.
    ulcX += VisualTreePanel.ROOT_OFFSET;
    ulcY += VisualTreePanel.BORDER_SIZE;

    // Check for a hit.
    if( x >= ulcX && x <= ulcX + width  - 1 &&
        y >= ulcY && y <= ulcY + height - 1 )
      return this;

    // No hit on the parent - maybe on one of the children?
    if( !isLeaf() ) {
      for( int i = 0; i < m_children.length; i++ ) {
        if( m_children[i] instanceof VacantTreeNode &&
            m_children[i].withinBoundingBox( x, y ) != null )
          return m_children[i];
      }
    }

    return null;
  }

  /**
   * Paints the node and any associated edges/arcs.
   *
   * @param g The graphics context on which to paint.
   */
  public void paintNode( Graphics g )
  {
    int ulcX   = m_xCoord - m_width/2;
    int ulcY   = m_yCoord;
    int width  = m_width;
    int height = m_height;
    int ovalHorizontalPadding = 16;
    int ovalVerticalPadding   = 16;

    // Apply scaling if required.
    if( VisualTreePanel.SCALING_FACTOR != 1.0 ) {
      ulcX   = (int)Math.round( ulcX * VisualTreePanel.SCALING_FACTOR );
      ulcY   = (int)Math.round( ulcY * VisualTreePanel.SCALING_FACTOR );
      width  = (int)Math.round( width * VisualTreePanel.SCALING_FACTOR );
      height = (int)Math.round( height * VisualTreePanel.SCALING_FACTOR );
      ovalHorizontalPadding = (int)Math.round(
        ovalHorizontalPadding * VisualTreePanel.SCALING_FACTOR );
      ovalVerticalPadding = (int)Math.round(
        ovalVerticalPadding * VisualTreePanel.SCALING_FACTOR );
    }

    // The border does not resize, so add border offset.
    ulcX += VisualTreePanel.ROOT_OFFSET;
    ulcY += VisualTreePanel.BORDER_SIZE;

    g.setFont( m_font );

    // Paint the highlight ellipse, if necessary.
    if( m_node.getFlag() == -1 ) {
      g.setColor( Color.red );
      g.drawOval( ulcX - ovalHorizontalPadding/2,
                  ulcY - ovalVerticalPadding/2,
                  width + ovalHorizontalPadding,
                  height + ovalVerticalPadding );
    }

    // Paint the node rectangle.
    if( c_trainOrTest == TRAINING )
      g.setColor( m_trainingColor );
    else
      g.setColor( m_testingColor );

    g.fillRect( ulcX, ulcY, width, height );
    g.setColor(Color.black);
    g.drawRect( ulcX, ulcY, width - 1, height - 1);

    // Paint the label text - unless the scaling factor
    // is less than 1.0.
    if( VisualTreePanel.SCALING_FACTOR == 1.0 )
    g.drawString( m_node.getLabel(),
                  ulcX + 5, ulcY + 2 + g.getFontMetrics().getAscent() );

    // Paint the arcs to subtrees, and any vacant subtree nodes.
    if( m_children != null ) {
      for( int i = 0; i < m_children.length; i++ ) {
        // Paint arc - in red if appropriate.
        if( m_node.getFlag() == i )
          VisualTreeArc.paintArc( this, m_children[i], i, g, Color.red );
        else
          VisualTreeArc.paintArc( this, m_children[i], i, g, Color.black );

        if( m_children[i] instanceof VacantTreeNode )
          m_children[i].paintNode( g );
      }
    }
  }

  // Static methods

  /**
   * Informs all nodes that they should be
   * colored based on training set performance.
   */
  public static void showTrainingPerformance()
  {
    c_trainOrTest = TRAINING;
  }

  /**
   * Informs all nodes that they should
   * be colored based on testing set performance.
   */
  public static void showTestingPerformance()
  {
    c_trainOrTest = TESTING;
  }
}
