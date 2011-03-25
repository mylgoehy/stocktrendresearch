package ai.decision.gui;

import java.awt.*;
import java.awt.event.*;

/**
 * A VisualTreeArc is a graphic representation of an edge
 * between two tree nodes.  The edge has an associated text
 * label that is rendered at the vertical midpoint of the
 * edge.
 *
 * <p>
 * The VisualTreeArc class contains a single static method
 * that draws an arc between two supplied nodes.  Arcs positions
 * are recomputed each time the tree is redrawn,
 * instead of being stored.  While this is less efficient, it
 * avoids the difficultly of shifting all the arcs in the tree
 * each time a node is added or deleted.
 *
 * <p>
 * <b>Change History:</b>
 *
 * <p><pre>
 * Name:            Date:            Change:
 * =============================================================
 * J. Kelly         Jun-18-2000      Created.
 * J. Kelly         Jun-29-2000      Modified to use static
 *                                   paint method.
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
public class VisualTreeArc
{
  // Public methods

  /**
   * Paints an arc from the middle of the bottom of
   * <code>startNode</code> to the middle of the top
   * of <code>endNode</code>.  If the current global
   * scaling factor is greater than or equal to 1.0,
   * the text label is drawn as part of the arc -
   * otherwise only the arc itself is drawn.
   *
   * @param startNode The node that the arc starts at.
   *        This has to be a VisualTreeNode, as only
   *        internal visual nodes have descending
   *        arcs attached.
   *
   * @param endNode The node that the arc ends at.  This
   *        can be a VisualTreeNode or a VacantTreeNode.
   *
   * @param arcNum An integer that identifies which
   *        <code>startNode</code> arc <code>endNode</code>
   *        is attached to.
   *
   * @param g The graphics context on which to draw the
   *        arc.
   *
   * @param arcColor The color of the arc.
   */
  public static void paintArc( VisualTreeNode startNode,
    AbstractTreeNode endNode, int arcNum, Graphics g, Color arcColor )
  {
    // Retrieve the text label associated with the arc.
    String label = startNode.getDecisionTreeNode().getArcLabel( arcNum );

    // Compute the start and end coordinates.
    int startX = startNode.getXCoord();
    int startY = startNode.getYCoord() +
    startNode.getHeight() - 1;
    int endX   = endNode.getXCoord();
    int endY   = endNode.getYCoord();

    Rectangle labelArea = null;

    // Apply scaling if required.
    if( VisualTreePanel.SCALING_FACTOR != 1.0 ) {
      startX = (int)Math.round( startX * VisualTreePanel.SCALING_FACTOR );
      startY = (int)Math.round( startY * VisualTreePanel.SCALING_FACTOR );
      endX   = (int)Math.round( endX * VisualTreePanel.SCALING_FACTOR );
      endY   = (int)Math.round( endY * VisualTreePanel.SCALING_FACTOR );
    }
    else {
      // We calculate the midpoint of the line segment,
      // and use this point combined with the current
      // font metrics to calculate the bounding rectangle
      // for our label text.
      int labelWidth  = g.getFontMetrics().stringWidth( label ) + 4;
      int labelHeight = g.getFontMetrics().getHeight() + 4;

      double midX = startX + (endX - startX)/2.0;
      double midY = startY + (endY - startY)/2.0;

      int ulcX = (int)(midX - labelWidth/2.0);
      int ulcY = (int)(midY - labelHeight/2.0);

      labelArea = new Rectangle( ulcX, ulcY, labelWidth, labelHeight );
    }

    g.setColor( arcColor );

    // Include border offset.
    g.drawLine( startX + VisualTreePanel.ROOT_OFFSET,
                startY + VisualTreePanel.BORDER_SIZE,
                endX   + VisualTreePanel.ROOT_OFFSET,
                endY   + VisualTreePanel.BORDER_SIZE );

    // If we have a label to paint, paint it.
    if( labelArea != null ) {
      g.clearRect( labelArea.x + VisualTreePanel.ROOT_OFFSET,
                   labelArea.y + VisualTreePanel.BORDER_SIZE,
                   labelArea.width, labelArea.height );

      // Note that the node label is painted with the
      // current font at the current size.
      g.drawString( label, labelArea.x + 2 + VisualTreePanel.ROOT_OFFSET,
                    labelArea.y + 2 + VisualTreePanel.BORDER_SIZE +
                    g.getFontMetrics().getAscent() );
    }
  }
}
