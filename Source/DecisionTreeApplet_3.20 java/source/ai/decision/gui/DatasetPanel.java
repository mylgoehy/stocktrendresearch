package ai.decision.gui;

import java.net.*;
import java.awt.*;
import java.awt.event.*;
import javax.swing.*;
import javax.swing.event.*;
import ai.common.*;

/**
 * Displays examples from a decision tree dataset in table
 * form.  Each column in the table refers to a particular
 * attribute from the dataset, with column 0 <i>always</i>
 * refering to the target attribute for the dataset.
 *
 * <p>
 * <b>Change History:</b>
 *
 * <p><pre>
 * Name:            Date:            Change:
 * =============================================================
 * J. Kelly         Oct-04-2000      Ground-up rewrite. Controls
 *                                   moved to DatasetMenu.
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
public class DatasetPanel
  extends TitledPanel
  implements ActionListener,
             MouseListener
{
  // Class data members

  /**
   * Preferred and minimum width for the panel.
   */
  private static final int PANEL_WIDTH  = 370;

  /**
   * Preferred and minimum height for the panel.
   */
  private static final int PANEL_HEIGHT = 230;

  /**
   * The default amount of time to wait (in ms) before
   * clearing row/column highlights in the table.
   */
  private static final int DEFAULT_DELAY_TIME = 20000;

  // Instance data members

  JTable m_datasetTable;             // Table of examples.
  JLabel m_tableTitle;

  ComponentManager m_manager;

  JCheckBoxMenuItem m_fullSizeView;  // Full size flag.
  JPopupMenu        m_viewPopupMenu; // View control popup.

  Timer   m_timer;                   // Timer for row/column
                                     // selection clear.

  // Constructors

  /**
   * Creates a new panel that contains a scrollable table
   * of examples from the supplied dataset.
   *
   * @param manager The global component manager.
   *
   * @throws NullPointerException If the supplied
   *         ComponentManager is null.
   */
  public DatasetPanel( ComponentManager manager )
  {
    super( "Dataset View" );

    if( manager == null )
      throw new
        NullPointerException( "Component manager is null." );

    m_manager = manager;

    // Now, build the panel structure.
    buildPanel();

    // Setup timer.
    m_timer = new Timer( DEFAULT_DELAY_TIME, this );
    m_timer.setRepeats( false );
  }

  /**
   * ActionListener interface implementation.  The panel
   * updates itself, and sends messages to other panels
   * as required.
   */
  public void actionPerformed( ActionEvent e )
  {
    Object source = e.getSource();

    if( source == m_fullSizeView )
      handlePanelResize();
    else if( source == m_timer )
      clearSelectedRows();
  }

  /**
   * MouseAdapter interface implementation.  The panel
   * displays a popup menu in response to a right mouse
   * button click.
   */
  public void mouseClicked( MouseEvent e )
  {
    // If the right mouse button is clicked, the zoom menu pops up.
    if( SwingUtilities.isRightMouseButton( e ) ) {
      m_viewPopupMenu.show( e.getComponent(), e.getX(), e.getY() );
    }
  }

  public void mouseEntered( MouseEvent e )  {}

  public void mouseExited( MouseEvent e )   {}

  public void mousePressed( MouseEvent e )  {}

  public void mouseReleased( MouseEvent e ) {}

  /**
   * Provides access to the table of examples.
   *
   * @return A reference to the JTable that
   *         actually displays dataset examples.
   */
  public JTable getDatasetTable()
  {
    return m_datasetTable;
  }

  /**
   * Sets the text that appears above the table
   * of examples.
   *
   * @param title The new title for the table.
   */
  public void setTableTitle( String title )
  {
    m_tableTitle.setText( title );
  }

  /**
   * Selects the specified rows in the current table.
   * This method allows the application to highlight
   * examples that reach a particular position in the
   * decision tree.
   *
   * <p>
   * A timer automatically clears the selection
   * after a short period of time.
   *
   * @param selected An array of booleans, where each
   *        boolean specifies a whether the corresponding
   *        row should be selected.  The size of the
   *        array should match the number of examples in
   *        the table.  Values that are out of range are
   *        ignored.
   */
  public void setSelectedRows( boolean[] selected )
  {
    m_datasetTable.clearSelection();

    // Loop, highlighting rows.
    for( int i = 0; i < selected.length; i++ )
      if( selected[i] )
        m_datasetTable.getSelectionModel().addSelectionInterval( i, i );

    m_timer.restart();
  }

  /**
   * Clears the current set of selected rows in the table
   * (if any).
   */
  public void clearSelectedRows()
  {
    m_datasetTable.clearSelection();
    m_timer.stop();
  }

  // Private methods

  /**
   * Toggles the full-size view of the panel.  The
   * algorithm and tree view panels are hidden when the
   * dataset panel is viewed in full-size mode.
   */
  private void handlePanelResize()
  {
    if( m_fullSizeView.isSelected() ) {
      if( m_manager.getAlgorithmPanel() != null )
        m_manager.getAlgorithmPanel().setVisible( false );

      if( m_manager.getVisualTreePanel() != null )
        m_manager.getVisualTreePanel().setVisible( false );
    }
    else {
      if( m_manager.getAlgorithmPanel() != null )
        m_manager.getAlgorithmPanel().setVisible( true );

      if( m_manager.getVisualTreePanel() != null )
        m_manager.getVisualTreePanel().setVisible( true );
    }
  }

  /*
   * Builds and arranges the various GUI components
   * for this panel.
   */
  private void buildPanel()
  {
    // Setup the layout manager for the panel.
    GridBagLayout layout = new GridBagLayout();
    GridBagConstraints c = new GridBagConstraints();
    setLayout( layout );

    // Build the table title.
    JPanel titlePanel = new JPanel();
    titlePanel.setLayout( new BoxLayout( titlePanel, BoxLayout.X_AXIS ) );

    m_tableTitle = new JLabel();
    m_tableTitle.setAlignmentX( JComponent.CENTER_ALIGNMENT );
    titlePanel.add( m_tableTitle );

    c.gridx = 0;
    c.gridy = 0;
    c.gridwidth  = 1;
    c.gridheight = 1;
    c.weightx = 1.0;
    c.weighty = 0.0;
    c.insets = new Insets( 3, 0, 2, 0 );

    layout.setConstraints( titlePanel, c );
    this.add( titlePanel );

    // Build the table.
    // Create a new table with the model.
    m_datasetTable = new JTable();

    // Enable row selection.  Disable column selection/reordering.
    m_datasetTable.setColumnSelectionAllowed( true );
    m_datasetTable.setRowSelectionAllowed( true );
    m_datasetTable.setCellSelectionEnabled( false );
    m_datasetTable.getTableHeader().setReorderingAllowed( false );

    m_datasetTable.getSelectionModel().setSelectionMode(
    ListSelectionModel.MULTIPLE_INTERVAL_SELECTION );

    // Anonymous inner class that notifies the dataset
    // menu whenever the table row selection changes.
    m_datasetTable.getSelectionModel().addListSelectionListener(
      new ListSelectionListener() {
        public void valueChanged(ListSelectionEvent e) {
          if( m_manager.getDatasetMenu() != null &&
            m_manager.getAlgorithm().getTree().isEmpty() )
              m_manager.getDatasetMenu().setMoveExamplesEnabled( true );
        }
      } );

    // Set the background selection color for the table.
    m_datasetTable.setSelectionBackground( Color.yellow );
    m_datasetTable.setAutoResizeMode( JTable.AUTO_RESIZE_OFF );

    // Finally, add the table to a scroll pane, and
    // the scroll pane to the panel.
    JScrollPane scrollPane = new JScrollPane( m_datasetTable );
      scrollPane.setHorizontalScrollBarPolicy(
        JScrollPane.HORIZONTAL_SCROLLBAR_ALWAYS );
      scrollPane.setVerticalScrollBarPolicy(
        JScrollPane.VERTICAL_SCROLLBAR_ALWAYS );

    c.gridy = 1;
    c.weighty = 1.0;
    c.fill    = GridBagConstraints.BOTH;
    c.insets = new Insets( 4, 8, 4, 8 );

    layout.setConstraints( scrollPane, c );
    this.add( scrollPane );

    // Build the view menu.
    m_fullSizeView = new JCheckBoxMenuItem( "Full Panel Dataset View" );
    m_fullSizeView.setSelected( false );
    m_fullSizeView.addActionListener( this );

    m_viewPopupMenu = new JPopupMenu();
    addMouseListener( this );
    m_viewPopupMenu.add( m_fullSizeView );

    // Set the preferred and minimum
    // width and height for the panel.
    setPreferredSize( new Dimension( PANEL_WIDTH, PANEL_HEIGHT ) );
    setMinimumSize( new Dimension( PANEL_WIDTH, PANEL_HEIGHT ) );
  }
}
