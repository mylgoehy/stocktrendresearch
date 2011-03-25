package ai.decision.gui;

import java.awt.*;
import java.awt.event.*;
import java.util.*;
import javax.swing.*;
import javax.swing.event.*;
import ai.common.*;

/**
 * A modal dialog box that displays various algorithm
 * preferences.
 *
 * <p>
 * <b>Change History:</b>
 *
 * <p><pre>
 * Name:            Date:            Change:
 * =============================================================
 * J. Kelly         Nov-07-2000      Created.
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
public class AlgorithmPrefsDialog
  extends JDialog
  implements ActionListener,
             WindowListener
{
  // Instance data members

  ComponentManager m_manager;

  // Preferences items

  JSlider m_algSpeedSlider;     // Controls the speed of the algorithm
                                // in TRACE_MODE.
  JTextField m_zScoreTextField; // Holds the current Z-score value.

  JButton m_okButton;           // 'Ok' button.
  JButton m_cancelButton;       // 'Cancel' button.

  // Constructors

  public AlgorithmPrefsDialog( ComponentManager manager )
  {
    // Modal dialog box.
    super();
    setTitle( "Algorithm Preferences" );
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
    if( e.getSource() == m_okButton )
      handleOkButton();
    else
      handleCancelButton();
  }

  /**
   * WindowListener interface implementation.
   * Preference values are adjusted when the
   * dialog box is closed - closing the box is
   * equivalent to clicking the 'Ok' button.
   */
  public void windowClosing(WindowEvent e) {
    // Closing the window is the same as clicking 'Ok'.
    handleOkButton();
  }

  public void windowClosed(WindowEvent e) {
  }

  public void windowOpened(WindowEvent e) {
    if( m_manager.getAlgorithm() != null )
      m_zScoreTextField.setText(
        Double.toString(
          m_manager.getAlgorithm().getPessimisticPruningZScore() ) );
  }

  public void windowIconified(WindowEvent e) {
  }

  public void windowDeiconified(WindowEvent e) {
  }

  public void windowActivated(WindowEvent e) {
  }

  public void windowDeactivated(WindowEvent e) {
  }

  // Private methods

  /**
   * Handles and coordinates actions when the user
   * clicks the 'Ok' button.
   */
  private void handleOkButton()
  {
    // Save changes to the algorithm speed slider.
    // Add 100 ms to the delay time - to ensure
    // that highlights are properly painted on the screen.
    if( m_manager.getAlgorithm() != null )
      m_manager.getAlgorithm().setWaitTime(
        (int)(1000 * m_algSpeedSlider.getValue()) + 100 );

    // Attempt to parse the value in the Z-score text field.
    double newZScore;

    try {
      newZScore = Double.parseDouble( m_zScoreTextField.getText() );

      if( m_manager.getAlgorithm() != null )
        m_manager.getAlgorithm().setPessimisticPruningZScore( newZScore );
    }
    catch( NumberFormatException f ) {
      // If it's not a number, discard it.
      if( m_manager.getAlgorithm() != null )
        m_zScoreTextField.setText(
          Double.toString(
            m_manager.getAlgorithm().getPessimisticPruningZScore() ) );
    }
    catch( IllegalArgumentException f ) {
      // If the algorithm object doesn't like the value
      // discard it.
      if( m_manager.getAlgorithm() != null )
        m_zScoreTextField.setText(
          Double.toString(
            m_manager.getAlgorithm().getPessimisticPruningZScore() ) );
    }

    this.hide();
  }

  /**
   * Handles and coordinates actions when the user clicks the
   * 'Cancel' button.
   */
  private void handleCancelButton()
  {
    // We have to reset the algorithm slider and the
    // Z-score box.
    if( m_manager.getAlgorithm() != null ) {
      m_algSpeedSlider.setValue(
        m_manager.getAlgorithm().getWaitTime() / 1000 );

      m_zScoreTextField.setText(
        Double.toString(
          m_manager.getAlgorithm().getPessimisticPruningZScore() ) );
    }

    this.hide();
  }

  /**
   * Builds the dialog box.
   */
  private void buildDialog()
  {
    // Create the algorithm speed slider panel.
    TitledPanel algSpeedPanel =
      new TitledPanel( "Algorithm Trace Step Delay" );

    // Set the algorithm speed slider to a full-scale value
    // of 10 seconds.
    m_algSpeedSlider = new JSlider( 0, 10, 5 );
    m_algSpeedSlider.setMajorTickSpacing( 1 );
    m_algSpeedSlider.setPaintTicks( true );
    m_algSpeedSlider.setSnapToTicks( true );
    algSpeedPanel.add( m_algSpeedSlider );

    //Create the label table.
    Hashtable labelTable = new Hashtable();
    labelTable.put( new Integer( 0 ), new JLabel("0 sec.") );
    labelTable.put( new Integer( 5 ), new JLabel("5") );
    labelTable.put( new Integer( 10 ), new JLabel("10 sec.") );
    m_algSpeedSlider.setLabelTable( labelTable );
    m_algSpeedSlider.setPaintLabels( true );

    if( m_manager.getAlgorithm() != null )
      m_manager.getAlgorithm().setWaitTime(
        (int)(1000 * m_algSpeedSlider.getValue()) );

    getContentPane().add( algSpeedPanel, BorderLayout.NORTH );

    // Create the pessimistic pruning z-score panel.
    TitledPanel pessimisticPrunePanel =
      new TitledPanel( "Pessimistic Pruning Z-score" );

    pessimisticPrunePanel.add( new JLabel( "Score: " ) );

    m_zScoreTextField = new JTextField( 5 );
    pessimisticPrunePanel.add( m_zScoreTextField );

    getContentPane().add( pessimisticPrunePanel, BorderLayout.CENTER );

    // Whenever the dialog box closes, we check and update the
    // Z-score value.
    addWindowListener( this );

    // Add 'Ok' and 'Cancel buttons.
    JPanel buttonPanel = new JPanel();
    m_okButton     = new JButton( "Ok" );
    m_cancelButton = new JButton( "Cancel" );

    buttonPanel.add( m_okButton );
    buttonPanel.add( m_cancelButton );

    m_okButton.addActionListener( this );
    m_cancelButton.addActionListener( this );

    getContentPane().add( buttonPanel, BorderLayout.SOUTH );

    setSize( 250, 214 );
    setResizable( false );

    Dimension d =
      Toolkit.getDefaultToolkit().getScreenSize();

    setLocation( (d.width  - getSize().width)  / 2,
                 (d.height - getSize().height) / 2 );
  }
}
