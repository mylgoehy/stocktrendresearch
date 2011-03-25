package ai.decision.gui;

import java.awt.*;
import javax.swing.*;
import ai.common.*;

/**
 * Displays a graphical representation of a decision tree.
 *
 * <p>
 * <b>Change History:</b>
 *
 * <p><pre>
 * Name:            Date:            Change:
 * =============================================================
 * J. Kelly         Oct-10-2000      Ground-up rewrite based
 *                                   on TreeDisplayPanel.
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
public class TreeViewPanel
  extends TitledPanel
{
  // Instance data members

  VisualTreePanel   m_visualTree; // The visual decision tree.

  ComponentManager m_manager;

  // Constructors

  /**
   * Creates a new panel that can contain a visual
   * representation of a decision tree.
   *
   * @param manager The global component manager.
   *
   * @throws NullPointerException If the supplied
   *         ComponentManager is null.
   */
  public TreeViewPanel( ComponentManager manager )
  {
    super( "Tree View" );

    if( manager == null )
      throw new
        NullPointerException( "Component manager is null." );

    m_manager = manager;

    // Now, build the panel structure.
    buildPanel();
  }

  /**
   * Enables or disables this panel, and the components
   * it contains.
   *
   * @param enabled True to enable the panel, or
   *        false to disable it.
   */
  public void setEnabled( boolean enabled )
  {
    super.setEnabled( enabled );
    m_visualTree.setEnabled( enabled );
  }

  /**
   * Provides access to the panel displaying a
   * visual representation of the current decision
   * tree.
   */
  public VisualTreePanel getVisualTreePanel()
  {
    return m_visualTree;
  }

  // Private methods

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

    c.gridx = 0;
    c.gridy = 0;
    c.gridwidth  = 1;
    c.gridheight = 1;
    c.weightx = 1.0;
    c.weighty = 1.0;
    c.fill = GridBagConstraints.BOTH;
    c.insets = new Insets( 4, 8, 4, 8 );

    m_visualTree = new VisualTreePanel( m_manager );

    JScrollPane scrollPane = new JScrollPane( m_visualTree );
    scrollPane.setHorizontalScrollBarPolicy(
      JScrollPane.HORIZONTAL_SCROLLBAR_ALWAYS );
    scrollPane.setVerticalScrollBarPolicy(
      JScrollPane.VERTICAL_SCROLLBAR_ALWAYS );

    m_visualTree.setViewport( scrollPane.getViewport() );

    layout.setConstraints( scrollPane, c );
    add( scrollPane );
  }
}
