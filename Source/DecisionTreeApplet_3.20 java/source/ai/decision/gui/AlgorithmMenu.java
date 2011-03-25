package ai.decision.gui;

import java.awt.*;
import java.awt.event.*;
import javax.swing.*;
import ai.decision.algorithm.*;
import ai.common.*;

/**
 * Menu controls for the decision tree algorithm.
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
public class AlgorithmMenu
  extends    JMenu
  implements ActionListener,
             AlgorithmListener
{
  // Class data members

  // Keystrokes that activate various actions.

  private static final KeyStroke RUN_KEYSTROKE =
    KeyStroke.getKeyStroke( KeyEvent.VK_R,
                            Event.CTRL_MASK | Event.ALT_MASK, false );
  private static final String RUN_CMND = "Run";

  private static final KeyStroke TRACE_RUN_KEYSTROKE =
    KeyStroke.getKeyStroke( KeyEvent.VK_T,
                            Event.CTRL_MASK | Event.ALT_MASK, false );
  private static final String TRACE_RUN_CMND = "Trace Run";

  private static final KeyStroke PAUSE_KEYSTROKE =
    KeyStroke.getKeyStroke( KeyEvent.VK_P,
                            Event.CTRL_MASK | Event.ALT_MASK, false );
  private static final String PAUSE_CMND = "Pause";

  private static final KeyStroke STEP_KEYSTROKE =
    KeyStroke.getKeyStroke( KeyEvent.VK_S,
                            Event.CTRL_MASK | Event.ALT_MASK, false );
  private static final String STEP_CMND = "Step";

  private static final KeyStroke CANCEL_KEYSTROKE =
    KeyStroke.getKeyStroke( KeyEvent.VK_C,
                            Event.CTRL_MASK | Event.ALT_MASK, false );
  private static final String CANCEL_CMND = "Cancel";

  private static final KeyStroke BACKUP_KEYSTROKE =
    KeyStroke.getKeyStroke( KeyEvent.VK_B,
                            Event.CTRL_MASK | Event.ALT_MASK, false );
  private static final String BACKUP_CMND = "Backup";

  private static final KeyStroke INITIALIZE_KEYSTROKE =
    KeyStroke.getKeyStroke( KeyEvent.VK_I,
                            Event.CTRL_MASK | Event.ALT_MASK, false );
  private static final String INITIALIZE_CMND = "Initialize";

  //-------------------------------------------------------

  // Various internal 'state' settings for the menu, that
  // govern what menu items are available to the user at
  // any given time.

  public static final int INITIAL_STATE  = 0;

  public static final int RUN_STATE      = 1;

  public static final int PAUSE_STATE    = 2;

  public static final int COMPLETE_STATE = 4;

  //-------------------------------------------------------

  // Instance data members

  JMenuItem m_run;        // Run the algorithm to completion,
                          // building the entire tree.
  JMenuItem m_traceRun;   // Trace the algorithm - build
                          // the tree automatically.
  JMenuItem m_pause;      // Pause the algorithm if it's
                          // running.
  JMenuItem m_step;       // Step through the code, one
                          // line at a time.
  JMenuItem m_cancel;     // Stop running the algorithm
                          // immediately.
  JMenuItem m_backup;     // Back up the algorithm one
                          // step.
  JMenuItem m_initialize; // Restart the algorithm (build a new tree)

  JMenu     m_splitMenu;  // Splitting function menu.
  JMenu     m_pruneMenu;  // Pruning algorithm menu.
  JMenuItem m_prefs;      // Display preferences

  AlgorithmPrefsDialog m_prefsDialog;

  ComponentManager m_manager;

  // Constructors

  /**
   * Builds the algorithm menu.
   *
   * @param manager The ComponentManager that
   *        provides access to other panels/menus.
   */
  public AlgorithmMenu( ComponentManager manager )
  {
    super( "Algorithm" );

    if( manager == null )
      throw new
        IllegalArgumentException( "Component manager is null." );

    m_manager     = manager;
    m_prefsDialog = new AlgorithmPrefsDialog( manager );

    buildMenu();

    // Set the initial button states.
    setMenuState( INITIAL_STATE );
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

    // The state of each menu item determines
    // whether the associated action is valid.
    if( source == m_run )
      handleRun();
    else if( source == m_traceRun )
      handleTraceRun();
    else if( source == m_pause )
      handlePause();
    else if( source == m_step )
      handleStep();
    else if( source == m_cancel )
      handleCancel();
    else if( source == m_backup )
      handleBackup();
    else if( source == m_initialize )
      handleInitialize();
    else if( source == m_prefs )
      m_prefsDialog.show();
    else {
      for( int i = 0; i < m_splitMenu.getItemCount(); i++ )
        if( source == m_splitMenu.getItem( i ) ) {
          m_manager.getAlgorithm().setSplittingFunction(
            m_splitMenu.getItem( i ).getText() );
          return;
        }

      for( int i = 0; i < m_pruneMenu.getItemCount(); i++ )
        if( source == m_pruneMenu.getItem( i ) ) {
          m_manager.getAlgorithm().setPruningAlgorithm(
            m_pruneMenu.getItem( i ).getText() );
          return;
        }
    }
  }

  public void notifyAlgorithmStarted() {}

  public void notifyAlgorithmStepStart( String text ) {}

  public void notifyAlgorithmStepStart() {}

  public void notifyAlgorithmStepDone() {}

  /**
   * Notifies the menu that the underlying algorithm
   * has finished executing.  The panel will set
   * its various algorithm controls accordingly.
   */
  public void notifyAlgorithmFinished()
  {
    Runnable notifyAlgorithmFinished =
      new Runnable() {
        public void run() {
          // Clear the pseudo-code window.
          m_manager.getAlgorithmPanel().getCodePanel().notifyClearHighlight();

          // Set all buttons to the 'complete' state.
          setMenuState( COMPLETE_STATE );
          m_manager.setAlgorithmThread( null );
        }
      };

    try {
      if( SwingUtilities.isEventDispatchThread() )
        notifyAlgorithmFinished.run();
      else
        SwingUtilities.invokeLater( notifyAlgorithmFinished );
    }
    catch( Exception e ) {
    }
  }

  /**
   * Sets the states for various menu items.
   * These states dictate which actions can be performed
   * at any given point.
   *
   * <p>
   * There are four possible states:
   *
   * <ul>
   *     <li><code>INITIAL_STATE</code>, allows a user
   *     to start running or stepping through the decision
   *     tree algorithm.  The user can also initialize,
   *     backup, and change the current splitting function.
   *
   *     <li><code>RUN_STATE</code>, which is set when
   *     the algorithm is running in <code>NORMAL_MODE</code>
   *     or <code>TRACE_MODE</code>.
   *     The user can stop the algorithm, which resets the
   *     state to the <code>INITIAL_STATE</code>, or pause
   *     it's execution, which puts the algorithm into
   *     <code>BREAK_MODE</code>.
   *
   *     <li><code>PAUSE_STATE</code>, which is set when
   *     the algorithm is paused (when execution changes
   *     from <code>TRACE_MODE</code> to
   *     <code>BREAK_MODE</code>).  The user can
   *     continue running the algorithm (trace run), run
   *     the algorithm to completion, or stop the algorithm.
   *
   *     <li><code>COMPLETE_STATE</code>, is set indirectly
   *     by the algorithm object itself, once the algorithm
   *     has completed running.  In this state, the only
   *     available actions are 'backup' and 'initialize'.
   * </ul>
   *
   * @param state The new action control state.
   */
  public void setMenuState( int state )
  {
    if( state == INITIAL_STATE ) {
      m_run.setEnabled( true );
      m_traceRun.setEnabled( true );
      m_pause.setEnabled( false );
      m_step.setEnabled( true );
      m_cancel.setEnabled( false );
      m_initialize.setEnabled( true );

      m_splitMenu.setEnabled( true );
      m_pruneMenu.setEnabled( true );

      m_prefs.setEnabled( true );

      if( m_manager.getAlgorithm() != null )
        if( !m_manager.getAlgorithm().getTree().isEmpty() )
          m_backup.setEnabled( true );
        else
          m_backup.setEnabled( false );
      else
        m_backup.setEnabled( false );

      // We can change datasets.
      if( m_manager.getDatasetMenu() != null )
        m_manager.getDatasetMenu().setEnabled( true );

      // We can click on the tree panel.
      if( m_manager.getVisualTreePanel() != null )
        m_manager.getVisualTreePanel().setEnabled( true );
    }
    else if( state == RUN_STATE ) {
      m_run.setEnabled( false );
      m_traceRun.setEnabled( false );
      m_pause.setEnabled( true );
      m_step.setEnabled( false );
      m_cancel.setEnabled( true );
      m_backup.setEnabled( false );
      m_initialize.setEnabled( false );

      m_splitMenu.setEnabled( false );
      m_pruneMenu.setEnabled( false );

      m_prefs.setEnabled( false );

      // We *can't* change datasets.
      if( m_manager.getDatasetMenu() != null )
        m_manager.getDatasetMenu().setEnabled( false );

      // We *can't* click on the tree panel.
      if( m_manager.getVisualTreePanel() != null )
        m_manager.getVisualTreePanel().setEnabled( false );
    }
    else if( state == PAUSE_STATE ) {
      m_run.setEnabled( true );
      m_traceRun.setEnabled( true );
      m_pause.setEnabled( false );
      m_step.setEnabled( true );
      m_cancel.setEnabled( true );
      m_backup.setEnabled( false );
      m_initialize.setEnabled( false );

      m_splitMenu.setEnabled( false );
      m_pruneMenu.setEnabled( false );

      m_prefs.setEnabled( true );

      if( m_manager.getDatasetMenu() != null )
        m_manager.getDatasetMenu().setEnabled( false );

      if( m_manager.getVisualTreePanel() != null )
        m_manager.getVisualTreePanel().setEnabled( false );
    }
    else if( state == COMPLETE_STATE ) {
      m_run.setEnabled( false );
      m_traceRun.setEnabled( false );
      m_pause.setEnabled( false );
      m_step.setEnabled( false );
      m_cancel.setEnabled( false );
      m_initialize.setEnabled( true );

      m_splitMenu.setEnabled( true );
      m_pruneMenu.setEnabled( true );

      m_prefs.setEnabled( true );

      if( m_manager.getAlgorithm() != null )
        if( !m_manager.getAlgorithm().getTree().isEmpty() )
          m_backup.setEnabled( true );
        else
          m_backup.setEnabled( false );
      else
        m_backup.setEnabled( false );

      if( m_manager.getDatasetMenu() != null )
        m_manager.getDatasetMenu().setEnabled( true );

      if( m_manager.getVisualTreePanel() != null )
        m_manager.getVisualTreePanel().setEnabled( true );
    }
  }

  // Private methods

  /**
   * Handles and coordinates operation when the user
   * selects the 'Run' menu item.
   */
  private void handleRun()
  {
    // Check - are we trying to perform reduced-error
    // pruning without a testing dataset?
    if( !ensureTestDatasetAvailIfNeeded() ) return;

    // Disable menu items.
    setMenuState( RUN_STATE );

    // Turn on run mode and disable verbose messaging.
    m_manager.getAlgorithm().setRunMode( DecisionTreeAlgorithm.NORMAL_MODE );
    m_manager.getAlgorithm().setVerbose( false );

    continueRunning();
  }

  /**
   * Handles and coordinates operation when the user
   * selects the 'Trace Run' menu item.
   */
  private void handleTraceRun()
  {
    // Check - are we trying to perform reduced-error
    // pruning without a testing dataset?
    if( !ensureTestDatasetAvailIfNeeded() ) return;

    // Set all menu items to their 'trace' states.
    setMenuState( RUN_STATE );

    // Turn on trace mode and enable verbose messaging.
    m_manager.getAlgorithm().setRunMode( DecisionTreeAlgorithm.TRACE_MODE );
    m_manager.getAlgorithm().setVerbose( true );

    continueRunning();
  }

  /**
   * Handles and coordinates operation when the user
   * selects the 'Pause' menu item.
   */
  private void handlePause()
  {
    // Set all menu items to their 'pause' states.
    setMenuState( PAUSE_STATE );

    // Turn on break mode and enable verbose messaging.
    m_manager.getAlgorithm().setRunMode( DecisionTreeAlgorithm.BREAK_MODE );
    m_manager.getAlgorithm().setVerbose( true );
  }

  /**
   * Handles and coordinates operation when the user
   * selects the 'Step' menu item.
   */
  private void handleStep()
  {
    // Check - are we trying to perform reduced-error
    // pruning without a testing dataset?
    if( !ensureTestDatasetAvailIfNeeded() ) return;

    // Set all menu items to their 'pause' states.
    setMenuState( PAUSE_STATE );

    // Turn on break mode and enable verbose messaging.
    m_manager.getAlgorithm().setRunMode( DecisionTreeAlgorithm.BREAK_MODE );
    m_manager.getAlgorithm().setVerbose( true );

    continueRunning();
  }

  /**
   * Handles and coordinates operation when the user
   * selects the 'Cancel' menu item.
   */
  private void handleCancel()
  {
    // Reset all menu items to their initial states.
    setMenuState( INITIAL_STATE );

    // Stop the algorithm.
    m_manager.getAlgorithm().stopRunning();
    m_manager.setAlgorithmThread( null );
  }

  /**
   * Handles and coordinates operation when the user
   * selects the 'Backup' menu item.
   */
  private void handleBackup()
  {
    // If there's a thread currently running,
    // we have to kill it - otherwise, we might
    // end up backing up while we're in the middle
    // of adding a node to the tree.
    m_manager.setAlgorithmThread( null );
    m_manager.getAlgorithm().getTree().backup();

    // Once we've backed up, the algorithm can
    // no longer be finished, so reset the menu.
    setMenuState( INITIAL_STATE );
  }

  /**
   * Handles and coordinates operation when the user
   * selects the 'Initialize' menu item.
   */
  private void handleInitialize()
  {
    // Kill current thread, if one exists.
    m_manager.setAlgorithmThread( null );
    m_manager.getAlgorithm().reset();

    // Inform the tree view panel that the old
    // tree no longer exists.
    if( m_manager.getVisualTreePanel() != null )
      m_manager.getVisualTreePanel().notifyNewTree();

    // Reset all menu items to their start states.
    setMenuState( INITIAL_STATE );
  }

  private void continueRunning()
  {
    // Continue running the algorithm, or start fresh.
    if( m_manager.getAlgorithmThread() != null )
      m_manager.getAlgorithm().continueRunning();
    else {
      m_manager.setAlgorithmThread(
        new Thread( m_manager.getAlgorithm() ) );
        m_manager.getAlgorithmThread().start();
    }
  }

  /**
   * Checks to ensure that a testing dataset is available
   * if the algorithm is being run with reduced-error
   * pruning turned on.  If the algorithm <i>is</i> being run with
   * reduced-error pruning turned on and no testing dataset
   * is available, the method displays an error dialog
   * prompting the user to create a testing dataset before
   * continuing.
   *
   * @return <code>true</code> if a testing dataset is defined
   *         or the pruning algorithm is set to 'none' or
   *         'pessimistic', <code>false</code> otherwise.
   */
  private boolean ensureTestDatasetAvailIfNeeded()
  {
    // Check the current pruning function.
    String pruningAlg = m_manager.getAlgorithm().getPruningAlgorithm();

    if( !pruningAlg.equals( DecisionTreeAlgorithm.PRUNING_REDUCED_ERROR ) )
      return true;

    // Do we have at least one test example?
    Dataset dataset = m_manager.getAlgorithm().getDataset();

    if( dataset.getNumTestingExamples() > 0 )
      return true;

    // Display an error dialog
    JOptionPane.showMessageDialog( null,
      "You must define a testing dataset before you can use " +
      "reduced-error pruning", "Hey!", JOptionPane.ERROR_MESSAGE );

    return false;
  }

  /**
   * Build the menu.
   */
  private void buildMenu()
  {
    m_run        = new JMenuItem( RUN_CMND );
    m_traceRun   = new JMenuItem( TRACE_RUN_CMND );
    m_pause      = new JMenuItem( PAUSE_CMND );
    m_step       = new JMenuItem( STEP_CMND );
    m_cancel     = new JMenuItem( CANCEL_CMND );
    m_backup     = new JMenuItem( BACKUP_CMND );
    m_initialize = new JMenuItem( INITIALIZE_CMND );

    // Register keystrokes for the menu items.
    m_run.setAccelerator( RUN_KEYSTROKE );
    m_traceRun.setAccelerator( TRACE_RUN_KEYSTROKE );
    m_pause.setAccelerator( PAUSE_KEYSTROKE );
    m_step.setAccelerator( STEP_KEYSTROKE );
    m_cancel.setAccelerator( CANCEL_KEYSTROKE );
    m_backup.setAccelerator( BACKUP_KEYSTROKE );
    m_initialize.setAccelerator( INITIALIZE_KEYSTROKE );

    // Register ActionListeners for the menu items.
    m_run.addActionListener( this );
    m_traceRun.addActionListener( this );
    m_pause.addActionListener( this );
    m_step.addActionListener( this );
    m_cancel.addActionListener( this );
    m_backup.addActionListener( this );
    m_initialize.addActionListener( this );

    // Populate the menu.
    add( m_run );
    add( m_traceRun );
    add( m_pause );
    add( m_step );
    add( m_cancel );
    add( m_backup );
    add( m_initialize );
    addSeparator();

    m_splitMenu = new JMenu( "Set Splitting Function" );

    // Add list of the available splitting functions.
    String[] splitFunList = DecisionTreeAlgorithm.SPLIT_FUNCTIONS;

    ButtonGroup splitFunGroup = new ButtonGroup();

    for( int i = 0; i < splitFunList.length; i++) {
      JRadioButtonMenuItem splitFunItem =
        new JRadioButtonMenuItem( splitFunList[i] );

      splitFunItem.addActionListener( this );
      m_splitMenu.add( splitFunItem );
      splitFunGroup.add( splitFunItem );
    }

    // Set the first splitting function as the default.
    if( m_splitMenu.getItemCount() != 0 )
      m_splitMenu.getItem( 0 ).setSelected( true );

    add( m_splitMenu );

    m_pruneMenu = new JMenu( "Set Pruning Algorithm" );

    // Add list of the available pruning algorithms.
    String[] pruneAlgList = DecisionTreeAlgorithm.PRUNING_ALGORITHMS;

    ButtonGroup pruneAlgGroup = new ButtonGroup();

    for( int i = 0; i < pruneAlgList.length; i++ ) {
      JRadioButtonMenuItem pruneAlgItem =
        new JRadioButtonMenuItem( pruneAlgList[i] );

      pruneAlgItem.addActionListener( this );
      m_pruneMenu.add( pruneAlgItem );
      pruneAlgGroup.add( pruneAlgItem );
    }

    // Set the first pruning algorithm as the default.
    if( m_pruneMenu.getItemCount() != 0 )
      m_pruneMenu.getItem( 0 ).setSelected( true );

    add( m_pruneMenu );
    addSeparator();

    // Add preferences item.
    m_prefs = new JMenuItem( "Preferences..." );
    m_prefs.addActionListener( this );

    add( m_prefs );
  }
}
