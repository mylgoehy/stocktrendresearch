package ai.decision.gui;

import javax.swing.*;
import javax.swing.table.*;
import ai.decision.algorithm.*;

/**
 * A DatasetTableModel sits on top of a decision tree  dataset,
 * and queries the dataset for information used to populate
 * a table.
 *
 * <p>
 * <b>Change History:</b>
 *
 * <p><pre>
 * Name:            Date:            Change:
 * =============================================================
 * J. Kelly         May-17-2000      Created.
 * J. Kelly         Oct-06-2000      Updated to support training
 *                                   and testing sets.
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
public class DatasetTableModel
    extends AbstractTableModel
{
  // Class data members

  /**
   * Indicates that examples from the training dataset
   * should be displayed.
   */
  public static final int TRAINING_SET = 0;

  /**
   * Indicates that examples from the testing dataset\
   * should be displayed.
   */
  public static final int TESTING_SET = 1;

  // Instance data members

  Dataset m_dataset;  // Dataset for the model.
  int m_setToShow;    // Determines if the model
                      // represents the training set,
                      // or the testing set.

  // Constructors

  /**
   * Creates a new DatasetTableModel based on the supplied
   * dataset.
   *
   * @param examples The dataset to display in the table.
   *
   * @param trainOrTest Determines if training examples
   *        or testing examples should be shown.
   *
   * @throws NullPointerException If the supplied Dataset
   *         is null.
   */
  public DatasetTableModel( Dataset examples, int trainOrTest )
  {
    if( examples == null )
      throw new
        NullPointerException( "Dataset cannot be null." );

    if( trainOrTest != TRAINING_SET && trainOrTest != TESTING_SET )
      m_setToShow = TRAINING_SET;
    else
      m_setToShow = trainOrTest;

    m_dataset = examples;
  }

  // Public methods

  /**
   * Returns the number of columns in the table (which is
   * equivalent to the number of attributes in the dataset,
   * including the target attribute).
   *
   * @return The number of columns in the table, or,
   *         equivalently, the number of attributes in the
   *         dataset.
   */
  public int getColumnCount()
  {
    // Ask for the number of attributes in the dataset.
    return m_dataset.getNumAttributes();
  }

  /**
   * Returns the number of rows in the table (which is
   * equivalent to the number of examples in the datset).
   *
   * @return The number of rows in the table, or, equivalently,
   * the number of examples in the dataset.
   */
  public int getRowCount()
  {
    if( m_setToShow == TRAINING_SET )
      return m_dataset.getNumTrainingExamples();
    else
      return m_dataset.getNumTestingExamples();
  }

  /**
   * Returns the name of the attribute displayed in
   * a particular column.
   *
   * @return The name of the attribute shown in the specified
   * column.
   */
  public String getColumnName( int column )
  {
    try {
      return m_dataset.getAttributeByNum( column ).getName();
    }
    catch( NonexistentAttributeException e ) {
      // This won't happen, as the table will
      // never ask for a value that is out of range.
    }

    return null;
  }

  /**
   * Returns the cell value at the specified row/column
   * coordinate.  The result is a String that identifies
   * the value of a particular attribute in a particular
   * example.
   *
   * @return The value of the particular attribute (column)
   *         for the particular example (row).
   */
  public Object getValueAt( int rowIndex, int columnIndex )
  {
    // The column index determines which attribute
    // we're looking at, and the row index determines
    // which example.
    try {
      Attribute att = m_dataset.getAttributeByNum( columnIndex );

      int[] example = null;

      if( m_setToShow == TRAINING_SET )
        example = m_dataset.getTrainingExample( rowIndex );
      else
        example = m_dataset.getTestingExample( rowIndex );

      // Ask the attribute for the name associated
      // with the particular value index.
      return att.getAttributeValueByNum( example[ columnIndex ] );
    }
    catch( NonexistentAttributeException e ) {
      // Won't happen.
      e.printStackTrace();
    }
    catch( NonexistentAttributeValueException e ) {
      // Won't happen.
      e.printStackTrace();
    }

    return null;
  }

  /**
   * Returns false, as the examples in the table are not
   * editable.
   *
   * @return false.
   */
  public boolean isCellEditable()
  {
    return false;
  }

  /**
   * Returns an integer that identifies the set of data
   * (training or testing) that is currently displayed
   * in the table.
   *
   * @return <code>TRAINING_SET</code> if the training
   *         set is displayed, or <code>TESTING_SET</code>
   *         if the testing set is displayed.
   */
  public int getDisplaySet()
  {
    return m_setToShow;
  }
}
