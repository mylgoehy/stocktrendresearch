package ai.decision.gui;

import java.awt.*;
import java.awt.event.*;
import java.util.*;
import javax.swing.*;
import javax.swing.event.*;
import javax.swing.table.*;
import ai.common.*;
import ai.decision.algorithm.*;

/**
 * A modal dialog box that allows a user to build a random
 * (balanced or unbalanced) testing dataset.
 *
 * <p>
 * <b>Change History:</b>
 *
 * <p><pre>
 * Name:            Date:            Change:
 * =============================================================
 * J. Kelly         Feb-02-2000      Created.
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
public class DatasetTestSetDialog
  extends JDialog
  implements ActionListener, ChangeListener
{
  // Instance data members

  ComponentManager m_manager;

  // Preferences items

  JSlider   m_percentageSlider;
  JCheckBox m_balancedCheckbox;
  JButton   m_okButton;
  JButton   m_cancelButton;

  // Constructors

  public DatasetTestSetDialog( ComponentManager manager )
  {
    // Modal dialog box.
    super();
    setTitle( "Create Random Testing Set" );
    setModal( true );

    if( manager == null )
      throw new
        IllegalArgumentException( "Component manager is null." );

    m_manager = manager;

    buildDialog();
  }

  // Public methods

  /**
   * ActionListener interface implementation.
   */
  public void actionPerformed( ActionEvent e )
  {
    Object source = e.getSource();

    if( source == m_cancelButton ) {
      hide();
      m_percentageSlider.setValue( 50 );
      return;
    }

    if( source == m_okButton && m_manager.getAlgorithm() != null ) {
      hide();
      Dataset dataset = m_manager.getAlgorithm().getDataset();
      dataset.createRndTestSet( m_percentageSlider.getValue(),
                                m_balancedCheckbox.isSelected() );
    }

    // If there's a table displaying the data, notify it that
    // the rows on display have changed.
    JTable table = m_manager.getDatasetPanel().getDatasetTable();
    ((AbstractTableModel)table.getModel()).fireTableDataChanged();

    // Notify the tree panel that training/testing stats may
    // have changed.
    m_manager.getVisualTreePanel().refresh();
  }

  /**
   * ChangeListener interface implementation.
   */
  public void stateChanged( ChangeEvent e )
  {
    Object source = e.getSource();

    if( source == m_percentageSlider )
      if( !((JSlider)source).getValueIsAdjusting()) {
        m_percentageSlider.setToolTipText(
          m_percentageSlider.getValue() + "%" );
      }
  }

  // Private methods

  /**
   * Builds the dialog box.
   */
  private void buildDialog()
  {
    JPanel percentagePanel = new JPanel( );
    percentagePanel.setBorder(
      BorderFactory.createTitledBorder(
        BorderFactory.createEtchedBorder(),
        "Percentage of Entire Dataset to Use for Testing" ) );

    percentagePanel.setLayout(
      new BoxLayout( percentagePanel, BoxLayout.Y_AXIS ) );

    // Set the percentage slider to a full scale value of 100%.
    m_percentageSlider = new JSlider( 0, 100, 50 );
    m_percentageSlider.setToolTipText( "50%" );
    m_percentageSlider.setMajorTickSpacing( 10 );
    m_percentageSlider.setPaintTicks( true );
    m_percentageSlider.addChangeListener( this );
    m_percentageSlider.setAlignmentX( Component.CENTER_ALIGNMENT );
    percentagePanel.add( m_percentageSlider );

    //Create the label table.
    Hashtable labelTable = new Hashtable();
    labelTable.put( new Integer( 0 ), new JLabel("0%") );
    labelTable.put( new Integer( 50 ), new JLabel("50%") );
    labelTable.put( new Integer( 100 ), new JLabel("100%") );
    m_percentageSlider.setLabelTable( labelTable );
    m_percentageSlider.setPaintLabels( true );

    // Add a bit of filler.
    percentagePanel.add( Box.createVerticalStrut( 10 ) );

    // Create the 'balanced' checkbox.
    m_balancedCheckbox = new JCheckBox( "Make Testing Set Balanced" );
    m_balancedCheckbox.setAlignmentX( Component.CENTER_ALIGNMENT );
    percentagePanel.add( m_balancedCheckbox );

    // Add a bit more filler.
    percentagePanel.add( Box.createVerticalStrut( 10 ) );

    // Add 'Ok' and 'Cancel' buttons.
    JPanel buttonPanel = new JPanel();
    buttonPanel.setLayout( new FlowLayout( FlowLayout.CENTER, 14, 0 ) );

    m_okButton = new JButton( "Ok" );
    m_okButton.addActionListener( this );
    m_cancelButton = new JButton( "Cancel" );
    m_cancelButton.addActionListener( this );

    buttonPanel.add( m_okButton );
    buttonPanel.add( m_cancelButton );
    percentagePanel.add( buttonPanel );

    getContentPane().add( percentagePanel );
    setSize( 320, 200 );

    Dimension d = Toolkit.getDefaultToolkit().getScreenSize();

    setLocation( (d.width  - getSize().width)  / 2,
                 (d.height - getSize().height) / 2 );
  }
}
