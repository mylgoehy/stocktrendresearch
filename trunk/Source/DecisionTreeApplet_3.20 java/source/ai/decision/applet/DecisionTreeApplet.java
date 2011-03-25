package ai.decision.applet;

import java.io.*;
import java.util.*;
import java.awt.*;
import java.awt.event.*;
import javax.swing.*;
import javax.swing.border.*;
import ai.decision.algorithm.*;
import ai.decision.gui.*;

/**
 * An applet that lets users interactively explore
 * decision trees.
 *
 * <p>
 * The applet looks for the following parameters (specified
 * in the HTML applet tag):
 *
 * <ul>
 * <li><i>datasets</i> - A semicolon-delimited list
 *     of available datasets (including path information,
 *     relative to the applet's codebase).
 * </ul>
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
public class DecisionTreeApplet
    extends JApplet
{
  // Class data members

  /**
   * The embedded page tag used to specify available datasets.
   * The datasets are grouped into a semicolon delimited list
   * (e.g. &quot;path1/dataset1;path2/dataset2&quot;).
   */
  public static final String DATASETS_PARAMETER = "datasets";

  //-------------------------------------------------------

  // Public methods

  /**
   * Builds the required display panels and initializes
   * the applet.
   */
  public void init()
  {
    // Print the current codebase.
    System.out.println( "Running from codebase " + getCodeBase() );

    // Parse parameters embedded in the web page.
    Vector availableDatasets =
      parseDatasetsParam( getParameter( DATASETS_PARAMETER ) );

    JPanel contentPane   = new JPanel();
    GridBagLayout layout = new GridBagLayout();
    GridBagConstraints c = new GridBagConstraints();
    contentPane.setLayout( layout );

    // Create the ComponentManager for the applet.
    ComponentManager manager = new ComponentManager();

    // Add the dataset panel.
    DatasetPanel datasetPanel = new DatasetPanel( manager );
    c.gridx = 0;
    c.gridy = 0;
    c.gridwidth  = 1;
    c.gridheight = 1;
    c.weightx    = 0.5;
    c.weighty    = 0.00001;
    c.fill = GridBagConstraints.BOTH;
    c.insets = new Insets( 4, 2, 1, 1 );

    layout.setConstraints( datasetPanel, c );
    contentPane.add( datasetPanel );
    manager.setDatasetPanel( datasetPanel );

    // Add the algorithm panel.
    AlgorithmPanel algorithmPanel =
      new AlgorithmPanel( getCodeBase(), manager );
    c.gridx = 1;
    c.gridy = 0;
    c.insets = new Insets( 4, 1, 1, 2 );

    layout.setConstraints( algorithmPanel, c );
    contentPane.add( algorithmPanel );
    manager.setAlgorithmPanel( algorithmPanel );

    // Add the tree view panel.
    TreeViewPanel treeViewPanel = new TreeViewPanel( manager );
    c.gridx = 0;
    c.gridy = 1;
    c.gridwidth = 2;
    c.weightx   = 1.0;
    c.weighty   = 1.0;
    c.insets = new Insets( 2, 1, 1, 2 );

    layout.setConstraints( treeViewPanel, c );
    contentPane.add( treeViewPanel );
    manager.setVisualTreePanel( treeViewPanel.getVisualTreePanel() );

    // Add the status bar.
    StatusBar statusBar = new StatusBar();
    c.gridy   = 2;
    c.weighty = 0.0;
    c.insets = new Insets( 3, 4, 5, 4 );

    layout.setConstraints( statusBar, c );
    contentPane.add( statusBar );
    manager.setStatusBar( statusBar );

    // Add menu bar and menus.
    JMenuBar menuBar = new JMenuBar();

    AlgorithmMenu algorithmMenu =
      new AlgorithmMenu( manager );
    DatasetMenu datasetMenu =
      new DatasetMenu( getCodeBase(), availableDatasets, manager );

    menuBar.add( datasetMenu );
    menuBar.add( algorithmMenu );

    setJMenuBar( menuBar );

    manager.setDatasetMenu( datasetMenu );
    manager.setAlgorithmMenu( algorithmMenu );

    // Finally, set the applet's content pane.
    setContentPane( contentPane );

    // Set the border for the entire applet.
    getRootPane().setBorder( BorderFactory.createLineBorder( Color.black ) );

    // Attempt to load an initial dataset
    // (the first one parsed, by default)
    if( availableDatasets.size() != 0 )
      datasetMenu.loadNewDataset( (String)availableDatasets.elementAt( 0 ) );
  }

  /**
   * Overrides the setSize method.  Allows the applet to
   * be dynamically resized.
   */
  public void setSize( int width, int height )
  {
    super.setSize( width, height );
    validate();
  }

  // Private methods

  /**
   * Parses and extracts the names of available datasets
   * from the parameter tag embedded in the applet's web
   * page.
   */
  private Vector parseDatasetsParam( String datasets )
  {
    Vector datasetNames = new Vector();

    // Was the parameter actually specified?
    if( datasets == null ) return datasetNames;

    // Chop and extract each name.
    StringTokenizer tokenizer = new StringTokenizer( datasets, ";" );

    while( tokenizer.hasMoreTokens() )
      datasetNames.add( tokenizer.nextToken() );

    return datasetNames;
  }
}
