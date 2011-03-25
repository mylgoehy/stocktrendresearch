package ai.decision.gui;

import java.awt.*;

/**
 * A VacantTreeNode is a visual representation of an empty
 * or 'available' position within a decision tree.
 * VacantTreeNodes are <b>not</b> part of the underlying tree,
 * they act only as placeholders. They provide:
 *
 * <p>
 * <ul>
 *     <li>A visual indication that a decision tree is
 *         incomplete, and
 *     <li>A clickable hotspot area, so that a user can
 *         manually build the tree.
 * </ul>
 *
 * <p>
 * VacantTreeNodes appear as small icons that look significantly
 * different from normal VisualTreeNodes.
 *
 * <p>
 * <b>Change History:</b>
 *
 * <p><pre>
 * Name:            Date:            Change:
 * =============================================================
 * J. Kelly         Jun-29-2000      Created.
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
public class VacantTreeNode
    extends  AbstractTreeNode
{
  // Class data members

  /**
   * The height of the triangle icon.
   */
  private static final int TRIANGLE_HEIGHT = 10;

  /**
   * The width of the base of the triangle icon for
   * the node.
   */
  private static final int TRIANGLE_BASE = 15;

  /**
   * The default color for vacant node triangles.
   */
  private static final Color TRIANGLE_COLOR = Color.blue;

  /**
   * The default color for vacant node triangle outlines.
   */
  private static final Color TRIANGLE_OUTLINE = Color.black;

  //-------------------------------------------------------

  // Constructors

  /**
   * Builds a new VacantTreeNode.
   *
   * @param parent The parent node for this node.  The
   *        parent must be a non-null VisualTreeNode.
   *
   * @param arcNum The parent arc that this node descends
   *        from.
   */
  public VacantTreeNode( VisualTreeNode parent, int arcNum )
  {
    super( parent, arcNum );

    if( parent == null )
      throw new
        NullPointerException( "Parent node is null." );

    // Set width and height to the current constants.
    // This is necessary so that the superclass width
    // and height methods will work.
    m_width  = TRIANGLE_BASE;
    m_height = TRIANGLE_HEIGHT;

    // Vacant nodes never have children.
  }

  // Public methods

  public boolean isLeaf()
  {
    return true;
  }

  public AbstractTreeNode withinBoundingBox( int x, int y )
  {
    // First, perform the same y-coordinate calculations
    // that are done when we draw the node.
    int[] yCoords = { m_yCoord, m_yCoord + m_height - 1 };

    // Apply scaling if required.
    if( VisualTreePanel.SCALING_FACTOR != 1.0 ) {
      yCoords[0] = (int) Math.round( yCoords[0] *
        VisualTreePanel.SCALING_FACTOR );
      yCoords[1] = (int)Math.round( yCoords[1] *
        VisualTreePanel.SCALING_FACTOR );
    }

    // The border does not resize, so add border offset.
    yCoords[0] += VisualTreePanel.BORDER_SIZE;
    yCoords[1] += VisualTreePanel.BORDER_SIZE;

    if( y < yCoords[0] || y > yCoords[1] ) return null;

    // We're within the vertical bounds, so now we check
    // the horizontal bounds.
    int[] xCoords = { m_xCoord, m_xCoord - m_width/2, m_xCoord + m_width/2 };

    if( VisualTreePanel.SCALING_FACTOR != 1.0 ) {
      xCoords[0] = (int)Math.round( xCoords[0] *
        VisualTreePanel.SCALING_FACTOR );
      xCoords[1] = (int)Math.round( xCoords[1] *
        VisualTreePanel.SCALING_FACTOR );
      xCoords[2] = (int)Math.round( xCoords[2] *
        VisualTreePanel.SCALING_FACTOR );
    }

    xCoords[0] += VisualTreePanel.ROOT_OFFSET;
    xCoords[1] += VisualTreePanel.ROOT_OFFSET;
    xCoords[2] += VisualTreePanel.ROOT_OFFSET;

    // Calculate ratio of base to height.
    int midlineDeviation = (int)Math.round( (y - yCoords[0]) *
    ((double)m_width/2.0)/((double)m_height) );

    if( x >= xCoords[0] - midlineDeviation &&
        x <= xCoords[0] + midlineDeviation )
      return this;

    return null;
  }

  public void paintNode( Graphics g )
  {
    // Set the color.
    g.setColor( TRIANGLE_COLOR );

    // Generate drawing coordinates.
    int[] xCoords =
      { m_xCoord, m_xCoord - m_width/2, m_xCoord + m_width/2 };

    int[] yCoords =
      { m_yCoord, m_yCoord + m_height - 1, m_yCoord + m_height - 1 };

    // Apply scaling if required.
    if( VisualTreePanel.SCALING_FACTOR != 1.0 ) {
      xCoords[0] = (int)Math.round( xCoords[0] *
        VisualTreePanel.SCALING_FACTOR );
      xCoords[1] = (int)Math.round( xCoords[1] *
        VisualTreePanel.SCALING_FACTOR );
      xCoords[2] = (int)Math.round( xCoords[2] *
        VisualTreePanel.SCALING_FACTOR );

      yCoords[0] = (int)Math.round( yCoords[0] *
        VisualTreePanel.SCALING_FACTOR );
      yCoords[1] = (int)Math.round( yCoords[1] *
        VisualTreePanel.SCALING_FACTOR );
      yCoords[2] = (int)Math.round( yCoords[2] *
        VisualTreePanel.SCALING_FACTOR );
    }

    // The border does not resize, so add border offset.
    xCoords[0] += VisualTreePanel.ROOT_OFFSET;
    xCoords[1] += VisualTreePanel.ROOT_OFFSET;
    xCoords[2] += VisualTreePanel.ROOT_OFFSET;

    yCoords[0] += VisualTreePanel.BORDER_SIZE;
    yCoords[1] += VisualTreePanel.BORDER_SIZE;
    yCoords[2] += VisualTreePanel.BORDER_SIZE;

    // Draw the shape.
    g.setColor( TRIANGLE_OUTLINE );
    g.fillPolygon( xCoords, yCoords, 3 );
    g.setColor( TRIANGLE_COLOR );
    g.drawPolygon( xCoords, yCoords, 3 );
  }
}

