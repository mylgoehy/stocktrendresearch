package ai.decision.gui;

import java.net.*;
import java.awt.*;
import java.awt.event.*;
import javax.swing.*;
import ai.common.*;

/**
 * An AlgorithmPanel displays pseudo-code for a specific
 * algorithm.
 *
 * <p>
 * <b>Change History:</b>
 *
 * <p><pre>
 * Name:            Date:            Change:
 * =============================================================
 * J. Kelly         Oct-04-2000      Ground-up rewrite. Controls
 *                                   moved to AlgorithmMenu.
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
public class AlgorithmPanel
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
   * Full path to pseudo-code file on server machine.
   */
  public static final String CODE_FILE_PATH =
    "ai/decision/algorithm/DecisionTreeAlgorithm.html";

  // Instance data members

  CodePanel m_codePanel;  // Panel that displays pseudo-code.
  URL       m_repository; // Location where code text is stored.

  ComponentManager m_manager;

  JCheckBoxMenuItem m_fullSizeView;  // Full size flag.
  JPopupMenu        m_viewPopupMenu; // View control popup.

  // Constructors

  /**
   * Creates a new panel that contains a scrollable
   * code listing.
   *
   * @param repository The URL where available code text
   *        is located.
   *
   * @param manager The global component manager.
   *
   * @throws NullPointerException If the supplied URL
   *         or ComponentManager is null.
   *
   * @throws IOException If a problem occurs while
   *         reading data from the specified URL.
   */
  public AlgorithmPanel( URL repository, ComponentManager manager )
  {
    super( "Algorithm View" );

    if( repository == null || manager == null  )
      throw new
        NullPointerException( "Repository URL or " +
                              "component manager is null." );

    m_repository  = repository;
    m_manager     = manager;

    // Now, build the panel structure.
    buildPanel();
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
  }

  /**
   * MouseAdapter interface implementation.  The panel
   * displays a popup menu in response to a right mouse
   * button click.
   */
  public void mouseClicked( MouseEvent e )
  {
    // If the right mouse button is clicked,
    // the zoom menu pops up.
    if( SwingUtilities.isRightMouseButton( e ) )
      m_viewPopupMenu.show( e.getComponent(), e.getX(), e.getY() );
  }

  public void mouseEntered( MouseEvent e )  {}

  public void mouseExited( MouseEvent e )   {}

  public void mousePressed( MouseEvent e )  {}

  public void mouseReleased( MouseEvent e ) {}

  /**
   * Provides access to the code panel.
   *
   * @return A reference to the CodePanel that
   *         actually displays the pseudo-code.
   */
  public CodePanel getCodePanel()
  {
    return m_codePanel;
  }

  // Private methods

  /**
   * Toggles the full-size view of the panel.  The
   * dataset and tree view panels are hidden when the
   * algorithm panel is viewed in full-size mode.
   */
  private void handlePanelResize()
  {
    if( m_fullSizeView.isSelected() ) {
      if( m_manager.getDatasetPanel() != null )
        m_manager.getDatasetPanel().setVisible( false );

      if( m_manager.getVisualTreePanel() != null )
        m_manager.getVisualTreePanel().setVisible( false );
    }
    else {
      if( m_manager.getDatasetPanel() != null )
        m_manager.getDatasetPanel().setVisible( true );

      if( m_manager.getVisualTreePanel() != null )
        m_manager.getVisualTreePanel().setVisible( true );
    }
  }

  /**
   * Builds and arranges the various GUI components
   * for this panel.
   */
  private void buildPanel()
  {
    // Setup the layout manager for the panel.
    GridBagLayout layout = new GridBagLayout();
    GridBagConstraints c = new GridBagConstraints();
    setLayout( layout );

    // Build the code window.
    try {
      m_codePanel = new CodePanel( m_repository, CODE_FILE_PATH );
    }
    catch( Exception e ) {
      e.printStackTrace();
      return;
    }

    // Before telling the code panel to display function 1,
    // we have to set the pruning algorithm text.
    // We're cheating here by setting this manually -
    // if the default pruning function changes, the code
    // below has to be changed also :-(
    m_codePanel.setDynamicText( "BuildDT", "prunealg", "None" );
    m_codePanel.displayFunction( 1 );

    c.gridx = 0;
    c.gridy = 1;
    c.gridwidth  = 2;
    c.gridheight = 1;
    c.weightx = 1.0;
    c.weighty = 1.0;
    c.fill = GridBagConstraints.BOTH;
    c.insets = new Insets( 4, 8, 4, 8 );

    layout.setConstraints( m_codePanel, c );
    add( m_codePanel );

    // Build the view menu.
    m_fullSizeView = new JCheckBoxMenuItem( "Full Panel Algorithm View" );
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
