package ai.decision.gui;

import java.net.*;
import java.util.Vector;
import java.io.*;
import java.awt.*;
import java.awt.event.*;
import javax.swing.*;
import ai.decision.algorithm.*;
import ai.common.*;

/**
 * Menu controls for loading and viewing decision tree datasets.
 *
 * <p>
 * <b>Change History:</b>
 *
 * <p><pre>
 * Name:            Date:            Change:
 * =============================================================
 * J. Kelly         Oct-02-2000      Created.
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
public class DatasetMenu
  extends    JMenu
  implements ActionListener
{
  // Class data members

  /**
   * Indicates that a custom dataset should be loaded, and
   * added to the list of available datasets.
   */
  private static final String CUSTOM_DATASET = "Custom...";

  /**
   * String for table title and menu.
   */
  private static final String TRAINING_EXAMPLES = "Training Examples" ;
  private static final String TESTING_EXAMPLES  = "Testing Examples";

  private static final String SHOW_TRAINING_SET = "Show Training Set";
  private static final String SHOW_TESTING_SET  = "Show Testing Set";

  private static final String MOVE_TRAINING_SET = "Move to Training Set";
  private static final String MOVE_TESTING_SET  = "Move to Testing Set";

  //-------------------------------------------------------

  // Instance data members

  URL    m_repository;          // Location where datasets are
                                // stored.
  Vector m_datasets;            // Names/path for all available
                                // datasets.
  String m_datasetName;         // Name of the current dataset.
                                // Including non-common path.
  String m_datasetNameWoutPath; // Name of current dataset
                                // with all path info removed.

  JMenu     m_loadMenu;         // Load a new dataset.
  JMenuItem m_createRndTestSet; // Create a random testing set.
  JMenuItem m_showExampleSet;   // Show training, or testing
                                // examples.
  JMenuItem m_mvToExampleSet;   // Move examples from
                                // one set to another.

  DatasetTestSetDialog m_testSetDialog;

  ComponentManager m_manager;

  // Constructors

  /**
   * Builds the dataset menu.
   *
   * @param repository The URL where available datasets
   *        are located.
   *
   * @param datasets A Vector containing a series of
   *        Strings, which form a list of the available
   *        datasets.  The Strings can include path
   *        information.
   *
   * @param manager The ComponentManager that
   *        provides access to other panels/menus.
   *
   * @throws NullPointerException If the supplied URL
   *         or datasets Vector is null.
   */
  public DatasetMenu( URL repository,
                      Vector datasets,
                      ComponentManager manager )
  {
    super( "Dataset" );

    if( manager == null || datasets   == null || repository == null )
      throw new
        IllegalArgumentException( "Repository URL, " +
          "datasets Vector or component manager is null." );

    m_repository = repository;
    m_datasets = datasets;
    m_manager = manager;
    m_testSetDialog = new DatasetTestSetDialog( manager );

    buildMenu();
  }

  // Public methods

  /**
   * ActionListener interface implementation.  The
   * menu sends messages to other menus and panels
   * (through the ComponentManager) as required.
   */
  public void actionPerformed( ActionEvent e )
  {
    Object source = e.getSource();

    if( source == m_createRndTestSet )
      m_testSetDialog.show();
    else if( source == m_showExampleSet )
      handleShowSet();
    else if( source == m_mvToExampleSet )
      handleMoveToSet();
    else if( source instanceof JMenuItem )
      // We must have a reference to a dataset.
      handleDatasetChoice( ((JMenuItem)source).getText() );
  }

  /**
   * A utility method that launches a new thread to
   * load and display decision tree data.  The method
   * is called each time a new dataset is selected.
   *
   * @param datasetName The name of the new dataset to
   *        load.  The dataset is retrieved from the
   *        current repository.  The name can include
   *        path information.
   */
  public void loadNewDataset( final String datasetName )
  {
    Dataset tempDataset = null;

    // Attempt to create a new Dataset object,
    // using data retreived from a server.  This code
    // executes in a separate thread.
    final SwingWorker worker =
      new SwingWorker()
      {
        Exception ex = null;
        Dataset tempDataset;

        public Object construct()
        {
          // Wait until we're sure we have a
          // neighboring algorithm panel.

          // Disable the neighboring algorithm menu.
          if( m_manager.getAlgorithmMenu() != null )
            m_manager.getAlgorithmMenu().setEnabled( false );

          setEnabled( false );

          // If a status bar exists, send a message
          // indicating that we're loading a dataset.
          if( m_manager.getStatusBar() != null )
            m_manager.getStatusBar().postMessage( "Loading dataset..." );

          try {
            tempDataset = new Dataset( m_repository, datasetName );
          }
          catch( Exception e ) {
            ex = e; // hang onto the exception
            return null;
          }

          // We successfully retrieved the dataset.
          if( m_manager.getAlgorithm() != null )
            m_manager.getAlgorithm().setDataset( tempDataset );
          else {
            DecisionTreeAlgorithm alg =
              new DecisionTreeAlgorithm( tempDataset, m_manager );
            m_manager.setAlgorithm( alg );

            // Register listeners with new algorithm
            // object.
            if( m_manager.getAlgorithmPanel() != null )
              alg.addHighlightListener(
                m_manager.getAlgorithmPanel().getCodePanel() );

            if( m_manager.getVisualTreePanel() != null )
              alg.getTree().addTreeChangeListener(
                m_manager.getVisualTreePanel() );

            if( m_manager.getStatusBar() != null )
              alg.addAlgorithmListener( m_manager.getStatusBar() );

            if( m_manager.getAlgorithmMenu() != null )
              alg.addAlgorithmListener( m_manager.getAlgorithmMenu() );
          }

          m_datasetName = datasetName;

          // Get the dataset name by itself.
          if( m_datasetName.lastIndexOf( '/' ) >= 0 )
            m_datasetNameWoutPath =
              m_datasetName.substring( m_datasetName.lastIndexOf( '/' ) + 1 );
          else
            m_datasetNameWoutPath = new String( m_datasetName );

          // Create a new table model for this dataset.
          DatasetTableModel model =
            new DatasetTableModel(
              tempDataset, DatasetTableModel.TRAINING_SET );

          // Inform the table that the data has changed,
          // so it can update itself.
          if( m_manager.getDatasetPanel() != null )
            m_manager.getDatasetPanel().getDatasetTable().setModel( model );

          return null; // dummy return
        }

        public void finished()
        {
          if( m_manager.getStatusBar() != null )
            m_manager.getStatusBar().clearMessage();

          // This code will execute on the event dispatch thread.
          if( ex != null ) {
            String errorText = null;

            // We caught something - inform the user
            // of the error.
            if( ex instanceof IOException )
              errorText = "IO problem";
            else if( ex instanceof FileNotFoundException )
              errorText = "File not found";
            else if( ex.getMessage() != null )
              errorText = ex.getMessage();

            JOptionPane.showMessageDialog( null,
              "Unable to load dataset.  The following " +
              "error occurred: " + errorText + ".", "Error loading dataset!",
              JOptionPane.ERROR_MESSAGE );
          }
          else {
            if( m_manager.getDatasetPanel() != null ) {
              m_showExampleSet.setText( SHOW_TESTING_SET );
              m_mvToExampleSet.setText( MOVE_TESTING_SET );

              m_manager.getDatasetPanel().setTableTitle(
                m_datasetNameWoutPath + " - " + TRAINING_EXAMPLES );
          }

          // Inform the tree view panel that a new tree will be
          // created with the data.
          if( m_manager.getVisualTreePanel() != null )
            m_manager.getVisualTreePanel().notifyNewTree();

          m_manager.getAlgorithmMenu().setMenuState(
            AlgorithmMenu.INITIAL_STATE );
        }

        m_manager.getAlgorithmMenu().setEnabled( true );
        setEnabled( true );
      }
    };

    // Start the worker thread.
    worker.start();
  }

  /**
   * Enables or disables the 'Move to Training / Testing set'
   * menu item.
   *
   * @param enabled <code>true</code> to enable the 'Move to Training
   *        / Testing' menu item (depending on whether there are
   *        highlighted rows in the dataset table).  <code>false</code>
   *        to disable the menu item.
   */
  void setMoveExamplesEnabled( boolean enabled )
  {
    if( enabled && m_manager.getDatasetPanel() != null ) {
      JTable table = m_manager.getDatasetPanel().getDatasetTable();

      if( table.getSelectedRows().length > 0 )
        m_mvToExampleSet.setEnabled( true );
      else
        m_mvToExampleSet.setEnabled( false );
    }
    else
      m_mvToExampleSet.setEnabled( false );
  }

  // Private methods

  /**
   * Handles and coordinates operation when the user
   * selects the 'show training / testing' menu item.
   */
  private void handleShowSet()
  {
    // We ask the table model what it's currently displaying.
    if( m_manager.getDatasetPanel() != null ) {
      JTable table = m_manager.getDatasetPanel().getDatasetTable();

      int currentDisplay =
        ((DatasetTableModel)table.getModel()).getDisplaySet();

      int newDisplay = (currentDisplay ==
          DatasetTableModel.TRAINING_SET ?
          DatasetTableModel.TESTING_SET : DatasetTableModel.TRAINING_SET );

      // Create a new table model.
      DatasetTableModel model =
        new DatasetTableModel(
          m_manager.getAlgorithm().getDataset(), newDisplay );

      // Inform the table that the data has changed,
      // so it can update itself.
      table.setModel( model );

      // Modify the current menu item and table title.
      if( newDisplay == DatasetTableModel.TRAINING_SET ) {
        m_showExampleSet.setText( SHOW_TESTING_SET );
        m_mvToExampleSet.setText( MOVE_TESTING_SET );

        m_manager.getDatasetPanel().setTableTitle(
        m_datasetNameWoutPath + " - " + TRAINING_EXAMPLES );
      }
      else {
        m_showExampleSet.setText( SHOW_TRAINING_SET );
        m_mvToExampleSet.setText( MOVE_TRAINING_SET );

        m_manager.getDatasetPanel().setTableTitle(
        m_datasetNameWoutPath + " - " + TESTING_EXAMPLES );
      }
    }
  }

  /**
   * Handles and coordinates operation when the user
   * selects the 'Move to Training / Testing Set' menu item.
   */
  private void handleMoveToSet()
  {
    if( m_manager.getDatasetPanel() != null &&
        m_manager.getAlgorithm()    != null ) {
      JTable table = m_manager.getDatasetPanel().getDatasetTable();

      int[] selections = table.getSelectedRows();

      if( selections.length == 0 ) {
        // No rows selected - disable the 'move' menu item.
        m_mvToExampleSet.setEnabled( false );
        return;
      }

      // At least one row is selected, so transfer
      // examples from one set to the other.
      int currentDisplay =
        ((DatasetTableModel)table.getModel()).getDisplaySet();

      if( currentDisplay == DatasetTableModel.TRAINING_SET )
        for( int i = 0; i < selections.length; i++ )
          m_manager.getAlgorithm().getDataset()
              .moveToTestingSet( selections[i] - i );
      else
        for( int i = 0; i < selections.length; i++ )
          m_manager.getAlgorithm().getDataset()
            .moveToTrainingSet( selections[i] - i );

      table.clearSelection();
      table.revalidate();

      // Inform the tree view panel that it should refresh
      // itself.
      if( m_manager.getVisualTreePanel() != null )
        m_manager.getVisualTreePanel().refresh();
    }
  }

  /**
   * Handles and coordinates operation when the user
   * selects an item from the 'Datasets' menu. Loads a
   * dataset and tells the table panel to display
   * the new data.
   */
  private void handleDatasetChoice( String datasetName )
  {
    // Has the dataset changed?  If so, load the new dataset.
    if( datasetName.equals( CUSTOM_DATASET ) ) {
      // Load a custom dataset.
      // Currently unimplemented.
    }
    else if( !(datasetName.equals( m_datasetName )) )
      loadNewDataset( datasetName );
    }

  /**
   * Build the menu.
   */
  private void buildMenu()
  {
    m_loadMenu = new JMenu( "Load Dataset" );

    for( int i = 0; i < m_datasets.size(); i++ ) {
      JMenuItem newItem = new JMenuItem( (String)m_datasets.elementAt( i ) );

      newItem.addActionListener( this );
      m_loadMenu.add( newItem );
    }

    JMenuItem customMenuItem = new JMenuItem( CUSTOM_DATASET );
    customMenuItem.setEnabled( false );
    m_loadMenu.add( customMenuItem );

    m_createRndTestSet = new JMenuItem( "Create Random Testing Set..." );
    m_showExampleSet   = new JMenuItem( SHOW_TESTING_SET );
    m_mvToExampleSet   = new JMenuItem( MOVE_TESTING_SET );

    m_createRndTestSet.addActionListener( this );
    m_showExampleSet.addActionListener( this );
    m_mvToExampleSet.addActionListener( this );

    m_mvToExampleSet.setEnabled( false );

    // Populate the menu.
    add( m_loadMenu );
    addSeparator();
    add( m_createRndTestSet );
    addSeparator();
    add( m_showExampleSet );
    addSeparator();
    add( m_mvToExampleSet );
  }
}
