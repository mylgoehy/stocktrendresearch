package ai.decision.gui;

import java.awt.*;
import javax.swing.*;
import java.util.*;
import ai.decision.algorithm.*;

/**
 * The AttributeValueMenuItem class is a small utility class
 * that holds the name of a particular attribute value.  The
 * class contains methods that correctly format this
 * information for display in a popup menu.
 *
 * <p>
 * <b>Change History:</b>
 *
 * <p><pre>
 * Name:            Date:            Change:
 * =============================================================
 * J. Kelly         Jul-22-2000      Created.
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
public class AttributeValueMenuItem
  extends JMenuItem
{
  // Instance data members

  String m_attValue;        // Target attribute value name.

  int m_trainReachHere;     // Number of training examples
                            // that reach node.
  int m_testReachHere;      // Number of testing examples
                            // that reach node.

  int m_trainBestTarget;    // Target value index for *best*
                            // (most common) training value.
  int m_testBestTarget;     // Target value index for *best*
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
   * Builds a new AttributeValueMenuItem object.  The menu item
   * text contains the attribute value name, a hyphen, and
   * the corresponding training and testing accuracy.
   *
   * @param attValue The attribute value that this menu item
   *        represents.
   */
  public
    AttributeValueMenuItem( String attValue,
      int numTrainingEgsReachHere,
      int bestTrainingTargetIndex,
      int numTrainingEgsCorrectClassUsingBestTrainingIndex,
      int numTestingEgsCorrectClassUsingBestTrainingIndex,
      int numTestingEgsReachHere,
      int bestTestingTargetIndex,
      int numTestingEgsCorrectClassUsingBestTestingIndex,
      int numTrainingEgsCorrectClassUsingBestTestingIndex )
  {
    super();

    setFont( getFont().deriveFont( Font.PLAIN, 10 ) );

    m_attValue = attValue;

    m_trainReachHere  = numTrainingEgsReachHere;
    m_trainBestTarget = bestTrainingTargetIndex;
    m_trainTrainingCorrectClass =
      numTrainingEgsCorrectClassUsingBestTrainingIndex;
    m_trainTestingCorrectClass  =
      numTestingEgsCorrectClassUsingBestTrainingIndex;

    m_testReachHere  = numTestingEgsReachHere;
    m_testBestTarget = bestTestingTargetIndex;
    m_testTestingCorrectClass  =
      numTestingEgsCorrectClassUsingBestTestingIndex;
    m_testTrainingCorrectClass =
      numTrainingEgsCorrectClassUsingBestTestingIndex;

    setText( attValue + " - Training/Testing Accuracy: " +
      m_trainTrainingCorrectClass + "/" + m_trainReachHere + "  " +
      m_trainTestingCorrectClass  + "/" + m_testReachHere );
  }

  /**
   * Returns the name of the attribute value that this
   * menu item stores.
   *
   * @return A reference to the attribute value whose
   *         label is displayed as part of the menu
   *         item text.
   */
  public String getAttributeValue()
  {
    return m_attValue;
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
    return m_trainReachHere;
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
   * Returns the number of testing examples that reach
   * the node.
   *
   * @return The number of examples from the dataset that reach the node.
   */
  public int getTestingEgsAtNode()
  {
    return m_testReachHere;
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
    return m_testBestTarget;
  }

  /**
   * Returns the best (most common) training set
   * target attribute value index.
   */
  public int getTrainingBestTarget()
  {
    return m_trainBestTarget;
  }
}
