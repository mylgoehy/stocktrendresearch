package ai.decision.gui;

import java.awt.*;
import javax.swing.*;

/**
 * A utility class that draw a gradient-filled bar on
 * a supplied graphics context.  Each shade of color on the
 * bar corresponds to a particular decision tree node
 * classification accuracy.
 *
 * <p>
 * The bar is initially drawn horizontally in the upper
 * right-hand corner of the viewport.
 *
 * <p>
 * <b>Change History:</b>
 *
 * <p><pre>
 * Name:            Date:            Change:
 * =============================================================
 * J. Kelly         Jul-08-2000      Created.
 * J. Kelly         Jul-23-2000      Changed static method to
 *                                   public method.  Added
 *                                   constructor.
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
public class GradientBar
{
  // Class data members

  /**
   * The total width of the bar, in pixels.
   */
  public static final int BAR_WIDTH  = 128;

  /**
   * The height of the bar, in pixels.
   */
  public static final int BAR_HEIGHT = 5;

  /**
   * The offset, in pixels, between the right-hand
   * edge of the last text character in the bar
   * label and the right side of the viewport.
   */
  public static final int BAR_RIGHT_OFFSET = 20;

  /**
   * The offset, in pixels, between the top of the
   * bar and the top of the viewport.
   */
  public static final int BAR_TOP_OFFSET = 12;

  //-------------------------------------------------------

  /**
   * The offset (down and right) of the shadow rectangle
   * drawn beneath the gradient bar.
   */
  private static final int SHADOW_OFFSET = 4;

  /**
   * The color of the shadow rectangle drawn beneath the
   * gradient bar.
   */
  private static final Color SHADOW_COLOR = new Color( 220, 220, 220 );

  private static final String ZERO_ACCURACY = new String( "0 %" );

  private static final String PERFECT_ACCURACY = new String( "100 %" );

  private static final String BAR_TITLE = new String( "Node Accuracy" );

  // Instance data members

  JPanel    m_panel;   // The panel on which the bar is drawn
  JViewport m_view;    // Panel viewport

  Point m_ulcPoint;    // Upper left corner of chart
                       // display
  Point m_lastClick;   // The last point that was clicked
                       // inside the pie chart
                       // (for translation)
  boolean m_paintFlag; // Flag that indicates it's ok
                       // to paint the bar.

  // Constructors

  /**
   * Builds a new gradient bar.
   *
   * @param panel The panel on which the bar will be
   *        drawn.  Initially, the bar is drawn in the
   *        upper right hand corner of the panel's
   *        viewport.
   *
   * @throws NullPointerException If the panel or the
   *         view is null.
   */
  public GradientBar( JPanel panel, JViewport view )
  {
    if( panel == null || view == null )
      throw new
        NullPointerException( "Panel or viewport is null." );

    m_panel = panel;
    m_view  = view;

    m_ulcPoint  = new Point( 0, 0 );
    m_lastClick = new Point( 0, 0 );
    m_paintFlag = false;

    reset();
  }

  // Public methods

  /**
   * Resets the position of the gradient bar.
   */
  public void reset()
  {
    Graphics g = m_panel.getGraphics();

    if( g == null ) return;  // Don't place the bar until we know
                             // where it goes.
    m_paintFlag = true;

    Font oldFont = g.getFont();
    g.setFont( oldFont.deriveFont( Font.PLAIN, 10 ) );

    // Determine where the upper left corner of the
    // bar should be initially.
    m_ulcPoint.x =
      m_view.getExtentSize().width -
      BAR_WIDTH - BAR_RIGHT_OFFSET - 7 -
      g.getFontMetrics().stringWidth( PERFECT_ACCURACY );

    // Set the upper left corner y-coordinate.
    m_ulcPoint.y = BAR_TOP_OFFSET;

    m_lastClick.x = 0;
    m_lastClick.y = 0;

    // Reset the font.
    g.setFont( oldFont );
  }

  /**
   * Determines if the supplied point lies
   * inside the bar's 'hotspot' (the bar itself).
   *
   * @param xPos The x-coordinate of the supplied point.
   *
   * @param yPos The y-coordinate of the supplied point.
   *
   * @return true if the point lies inside the pie, or
   *         false otherwise.
   */
  public boolean hitDetect( int xPos, int yPos )
  {
    if( xPos >= m_ulcPoint.x &&
        xPos <= m_ulcPoint.x + BAR_WIDTH - 1 &&
        yPos >= m_ulcPoint.y &&
        yPos <= m_ulcPoint.y + BAR_HEIGHT - 1 ) {
      // Save the click differential.
      m_lastClick.x = xPos - m_ulcPoint.x;
      m_lastClick.y = yPos - m_ulcPoint.y;
      return true;
    }

    return false;
  }

  /**
   * Shifts the position of the gradient bar on the panel
   * display.  This method uses the location of the last
   * click on the bar as a guide - preventing
   * the bar from 'jumping' when it's being dragged around.
   *
   * @param xPos The x-coordinate of the drag point.
   *
   * @param yPos The y-coordinate of the drag point.
   */
  public void move( int xPos, int yPos )
  {
    int moveX;
    int moveY;

    if( xPos - m_lastClick.x < BAR_RIGHT_OFFSET ) {
      moveX = BAR_RIGHT_OFFSET;
    }
    else {
      moveX = xPos - m_lastClick.x + BAR_WIDTH +
        BAR_RIGHT_OFFSET < m_panel.getSize().width ?
        xPos - m_lastClick.x : m_panel.getSize().width -
        BAR_WIDTH - BAR_RIGHT_OFFSET;
    }

    if( yPos - m_lastClick.y < BAR_TOP_OFFSET ) {
      moveY = BAR_TOP_OFFSET;
    }
    else {
      moveY = yPos - m_lastClick.y + BAR_HEIGHT +
        BAR_TOP_OFFSET < m_panel.getSize().height ?
        yPos - m_lastClick.y : m_panel.getSize().height -
        BAR_HEIGHT - BAR_TOP_OFFSET;
    }

    m_ulcPoint.x = moveX;
    m_ulcPoint.y = moveY;
  }

  /**
   * Shifts the position of the gradient bar in response
   * to a change in the size of the display panel.  If
   * the change is size results in the bar being out
   * of view, the bar will move itself.
   */
  public void adjust()
  {
    if( m_ulcPoint.x + BAR_WIDTH +
      BAR_RIGHT_OFFSET > m_panel.getSize().width )
      m_ulcPoint.x = m_panel.getSize().width -
      BAR_WIDTH - BAR_RIGHT_OFFSET;

    if( m_ulcPoint.y + BAR_HEIGHT +
      BAR_TOP_OFFSET > m_panel.getSize().height )
      m_ulcPoint.y = m_panel.getSize().height -
      BAR_HEIGHT - BAR_TOP_OFFSET;

    m_ulcPoint.x = m_ulcPoint.x > BAR_RIGHT_OFFSET ?
      m_ulcPoint.x : BAR_RIGHT_OFFSET;

    m_ulcPoint.y = m_ulcPoint.y > BAR_TOP_OFFSET ?
      m_ulcPoint.y : BAR_TOP_OFFSET;
  }

  /**
   * Draws a labelled gradient bar on the display.
   *
   * @param g The graphics context on which the bar will
   *        be drawn.
   */
  public void paintBar( Graphics g )
  {
    if( !m_paintFlag ) return;  // Can't paint yet.

    // Save and set the font.
    Font oldFont = g.getFont();
    g.setFont( oldFont.deriveFont( Font.PLAIN, 10 ) );

    // Currently, the bar will be drawn outside
    // the view, if the view isn't large enough.

    // Draw the shadow.
    g.setColor( SHADOW_COLOR );
    g.fillRect( m_ulcPoint.x + SHADOW_OFFSET - 1,
      m_ulcPoint.y + SHADOW_OFFSET - 1,
      BAR_WIDTH + 2, BAR_HEIGHT + 2 );

    // Draw the gradient.
    for( int i = 0; i < BAR_WIDTH; i++ ) {
      g.setColor( ColorScheme.getColor( ((double)i)/BAR_WIDTH ) );
      g.drawLine( m_ulcPoint.x + i, m_ulcPoint.y,
                  m_ulcPoint.x + i, m_ulcPoint.y +
                  BAR_HEIGHT - 1 );
    }

    // Draw the black outline.
    g.setColor( Color.black );
    g.drawRect( m_ulcPoint.x - 1, m_ulcPoint.y - 1,
                BAR_WIDTH + 1, BAR_HEIGHT + 1 );

    // Draw the text.
    int baseline = m_ulcPoint.y + BAR_HEIGHT/2 +
    g.getFontMetrics().getAscent()/2;

    g.drawString( ZERO_ACCURACY,
      m_ulcPoint.x - 6 - g.getFontMetrics().stringWidth( ZERO_ACCURACY ),
      baseline );

    g.drawString( PERFECT_ACCURACY,
      m_ulcPoint.x + BAR_WIDTH + 7, baseline );

    g.drawString( BAR_TITLE,
      m_ulcPoint.x + BAR_WIDTH/2 -
      g.getFontMetrics().stringWidth( BAR_TITLE )/2,
      m_ulcPoint.y + BAR_HEIGHT + 6 +
      g.getFontMetrics().getAscent() );

    // Reset the font.
    g.setFont( oldFont );
  }
}
