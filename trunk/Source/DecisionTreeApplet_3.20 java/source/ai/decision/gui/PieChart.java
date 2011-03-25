package ai.decision.gui;

import java.awt.*;
import javax.swing.*;

/**
 * A utility class that draws a small pie chart, which provides
 * a dynamic view of the performance of a given decision tree.
 *
 * <p>
 * The chart is painted in the upper left-hand corner of the
 * panel, along with some text that lists the number of
 * examples from the dataset that are correctly classified
 * by the tree, as well as the number of nodes in the tree.
 *
 * <p>
 * <b>Change History:</b>
 *
 * <p><pre>
 * Name:            Date:            Change:
 * =============================================================
 * J. Kelly         Jul-12-2000      Created.
 * J. Kelly         Oct-11-2000      Added support for testing
 *                                   set performance display.
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
public class PieChart
{
  // Class data members

  /**
   * The diameter of the chart, in pixels.
   */
  public static int CHART_DIAMETER = 40;

  /**
   * The offset, in pixels, between the left-hand edge
   * of the chart and the left side of the panel (minimum).
   */
  public static final int CHART_LEFT_OFFSET = 18;

  /**
   * The offset, in pixels, between the top of the chart
   * and the top of the panel (minimum).
   */
  public static final int CHART_TOP_OFFSET = 12;

  //-------------------------------------------------------

  /**
   * The offset (down and right) of the shadow circle
   * drawn beneath the chart.
   */
  private static final int SHADOW_OFFSET = 4;

  /**
   * The color of the shadow circle drawn beneath the
   * chart.
   */
  private static final Color SHADOW_COLOR = new Color( 220, 220, 220 );

  private static final String TRAINING_CLASSIFIED_TEXT =
    new String( " training examples correctly classified." );

  private static final String TESTING_CLASSIFIED_TEXT =
    new String( " testing examples correctly classified." );

  private static final int TRAINING = 0;

  private static final int TESTING  = 1;

  // Instance data members

  JPanel m_panel;         // The panel on which the chart
                          // is drawn.
  Point m_ulcPoint;       // Upper left corner of chart
                          // display.
  Point m_lastClick;      // The last point that was clicked
                          // inside the pie chart
                          // (for translation).
  boolean m_paintFlag;    // Flag that indicates it's ok
                          // to paint the chart.

  int m_trainingCorrect;  // Number of training examples
                          // currently correctly classified.
  int m_trainingExamples; // The total number of examples
                          // in the training dataset.
  int m_testingCorrect;   // Number of testing examples
                          // currently correctly classified.
  int m_testingExamples;  // The total number of examples
                          // in the testing dataset.

  int m_numInternalNodes;   // The number of internal nodes
                            // currently in the tree.
  int m_numNodes;           // The number of nodes currently
                            // in the tree.
  int m_trainingArcDegrees;
  int m_testingArcDegrees;

  int m_trainOrTest;        // Flag. Signals display of
                            // training or testing stats.

  // Constructors

  /**
   * Builds a new pie chart.  Initially, the chart
   * is unfilled (0 examples correctly classified).
   *
   * @param panel The panel on which the chart will be
   *        drawn.  Initially, the chart is drawn in
   *        the upper left hand corner of the panel.
   *
   * @throws NullPointerException If the panel is null.
   */
  public PieChart( JPanel panel )
  {
    if( panel == null )
      throw new NullPointerException( "Panel is null." );

    m_panel = panel;

    // Set the initial upper left corner point.
    m_ulcPoint  = new Point( 0, 0 );
    m_lastClick = new Point( 0, 0 );
    m_paintFlag = false;

    m_trainOrTest = TRAINING; // Show training set by default.
    reset();
  }

  /**
   * Updates the chart's internal record of the number of
   * nodes in the tree.  The chart will <b>not</b>
   * automatically repaint itself - the display will update
   * the next time the <code>paintChart</code> method is
   * called.
   *
   * @param numNodes The new number of nodes in the tree.
   *
   * @param numInternalNodes The new number of internal nodes
   *        in the tree.
   */
  public void updateNumNodes( int numNodes, int numInternalNodes )
  {
    m_numNodes         = numNodes;
    m_numInternalNodes = numInternalNodes;
  }

  /**
   * Updates the chart's internal record of the number
   * of training examples that are currently correctly
   * classified by the tree.
   *
   * @param numCorrectClass The number of training examples
   *        correctly classified by the tree.
   *
   * @param numExamples The total number of training examples
   *        in the dataset.
   */
  public void updateTrainingPerformance( int numCorrectClass, int numExamples )
  {
    m_trainingCorrect  = numCorrectClass;
    m_trainingExamples = numExamples;

    m_trainingArcDegrees =
      (int)Math.round( ((double)m_trainingCorrect)/m_trainingExamples*360 );
  }

  /**
   * Updates the chart's internal record of the number
   * of testing examples that are currently correctly
   * classified by the tree.
   *
   * @param numCorrectClass The number of testing examples
   *        correctly classified by the tree.
   *
   * @param numExamples The total number of testing examples
   *        in the dataset.
   */
  public void updateTestingPerformance( int numCorrectClass, int numExamples )
  {
    m_testingCorrect  = numCorrectClass;
    m_testingExamples = numExamples;

    m_testingArcDegrees =
      (int)Math.round( ((double)m_testingCorrect)/m_testingExamples*360 );
  }

  /**
   * Notifies the chart that it should display
   * training results.
   */
  public void showTrainingPerformance()
  {
    m_trainOrTest = TRAINING;
  }

  /**
   * Notifies the chart that it should display
   * testing results.
   */
  public void showTestingPerformance()
  {
    m_trainOrTest = TESTING;
  }

  /**
   * Determines if the supplied point lies
   * inside the chart's 'hotspot' (the pie itself).
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
    double pieRadius = CHART_DIAMETER / 2.0;

    double diffX = xPos - m_ulcPoint.x - pieRadius;
    double diffY = yPos - m_ulcPoint.y - pieRadius;

    if( Math.sqrt( diffX * diffX + diffY * diffY ) <= pieRadius ) {
      // Save the click differential.
      m_lastClick.x = (int)(diffX + pieRadius);
      m_lastClick.y = (int)(diffY + pieRadius);
      return true;
    }

    return false;
  }

  /**
   * Shifts the position of the pie chart on the panel
   * display.  This method uses the location of the last
   * click on the chart as a guide during shifting, preventing
   * the chart from 'jumping' when it's being dragged around.
   *
   * @param xPos The x-coordinate of the drag point.
   *
   * @param yPos The y-coordinate of the drag point.
   */
  public void move( int xPos, int yPos )
  {
    int moveX;
    int moveY;

    if( xPos - m_lastClick.x < CHART_LEFT_OFFSET )
      moveX = CHART_LEFT_OFFSET;
    else
      moveX = xPos - m_lastClick.x + CHART_DIAMETER +
        CHART_LEFT_OFFSET < m_panel.getSize().width ?
          xPos - m_lastClick.x : m_panel.getSize().width -
          CHART_DIAMETER - CHART_LEFT_OFFSET;

    if( yPos - m_lastClick.y < CHART_TOP_OFFSET )
      moveY = CHART_TOP_OFFSET;
    else
      moveY = yPos - m_lastClick.y + CHART_DIAMETER +
        CHART_TOP_OFFSET < m_panel.getSize().height ?
          yPos - m_lastClick.y : m_panel.getSize().height -
          CHART_DIAMETER - CHART_TOP_OFFSET;

    m_ulcPoint.x = moveX;
    m_ulcPoint.y = moveY;
  }

  /**
   * Shifts the position of the pie chart in response
   * to a change in the size of the display panel.  If
   * the change is size results in the chart being out
   * of view, the chart will move itself.
   */
  public void adjust()
  {
    if( m_ulcPoint.x + CHART_DIAMETER +
      CHART_LEFT_OFFSET > m_panel.getSize().width )
      m_ulcPoint.x = m_panel.getSize().width -
      CHART_DIAMETER - CHART_LEFT_OFFSET;

    if( m_ulcPoint.y + CHART_DIAMETER +
      CHART_TOP_OFFSET > m_panel.getSize().height )
      m_ulcPoint.y = m_panel.getSize().height -
      CHART_DIAMETER - CHART_TOP_OFFSET;

    m_ulcPoint.x = m_ulcPoint.x > CHART_LEFT_OFFSET ?
                   m_ulcPoint.x : CHART_LEFT_OFFSET;

    m_ulcPoint.y = m_ulcPoint.y > CHART_TOP_OFFSET ?
                   m_ulcPoint.y : CHART_TOP_OFFSET;
  }

  /**
   * Resets the chart for a tree with 0 nodes.
   */
  public void reset()
  {
    Graphics g = m_panel.getGraphics();

    if( g == null ) return;  // Don't place the bar until we know
                             // where it goes.
    m_paintFlag = true;

    m_numNodes           = 0;
    m_numInternalNodes   = 0;
    m_trainingCorrect    = 0;
    m_trainingExamples   = 0;
    m_testingCorrect     = 0;
    m_testingExamples    = 0;
    m_trainingArcDegrees = 0;

    // Move the chart back to the upper left corner
    // of the panel.
    m_ulcPoint.x = CHART_LEFT_OFFSET;
    m_ulcPoint.y = CHART_TOP_OFFSET;

    m_lastClick.x = 0;
    m_lastClick.y = 0;
  }

  /**
   * Draws a labelled pie chart on the display.
   *
   * @param g The graphics context on which the chart
   *        will be drawn.
   */
  public void paintChart( Graphics g )
  {
    if( !m_paintFlag ) return;  // Can't paint yet.

    // Save and set the font.
    Font oldFont = g.getFont();
    g.setFont( oldFont.deriveFont( Font.PLAIN, 10 ) );

    // Draw the shadow.
    g.setColor( SHADOW_COLOR );
    g.fillOval( m_ulcPoint.x + SHADOW_OFFSET,
      m_ulcPoint.y + SHADOW_OFFSET, CHART_DIAMETER, CHART_DIAMETER );

    // Draw the chart background.
    g.setColor( Color.yellow );
    g.fillOval( m_ulcPoint.x, m_ulcPoint.y, CHART_DIAMETER, CHART_DIAMETER );

    // Fill in the chart.
    g.setColor( new Color( 255, 0, 0 ) );

    if( m_trainOrTest == TRAINING && m_trainingArcDegrees != 0 )
      g.fillArc( m_ulcPoint.x, m_ulcPoint.y,
      CHART_DIAMETER, CHART_DIAMETER, 90, -m_trainingArcDegrees );
    else if( m_testingArcDegrees != 0 )
      g.fillArc( m_ulcPoint.x, m_ulcPoint.y,
      CHART_DIAMETER, CHART_DIAMETER, 90, -m_testingArcDegrees );

    // Draw the text.
    g.setColor( Color.black );

    int baseline1 = m_ulcPoint.y + g.getFontMetrics().getAscent();
    int baseline2 = baseline1 + 13;

    if( m_trainOrTest == TRAINING )
      g.drawString( m_trainingCorrect + " of " +
        m_trainingExamples + TRAINING_CLASSIFIED_TEXT,
        m_ulcPoint.x + CHART_DIAMETER + 15, baseline1 );
    else
      g.drawString( m_testingCorrect + " of " +
        m_testingExamples + TESTING_CLASSIFIED_TEXT,
        m_ulcPoint.x + CHART_DIAMETER + 15, baseline1 );

      g.drawString( "Tree contains " + m_numNodes +
        ( m_numNodes == 1 ? " node " : " nodes ") +
        "(" + m_numInternalNodes + " internal).",
        m_ulcPoint.x + CHART_DIAMETER + 15, baseline2 );

    // Reset the font.
    g.setFont( oldFont );
  }
}
