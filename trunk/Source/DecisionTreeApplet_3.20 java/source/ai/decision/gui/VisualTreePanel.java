package ai.decision.gui;

import java.awt.*;
import java.awt.event.*;
import javax.swing.*;
import java.util.*;
import ai.decision.algorithm.*;
import ai.common.*;

/**
 * A panel that displays a visual representation of a decision
 * tree.
 *
 * <p>
 * <b>Change History:</b>
 *
 * <p><pre>
 * Name:            Date:            Change:
 * =============================================================
 * J. Kelly         Jun-18-2000      Created.
 * J. Kelly         Jun-27-2000      Added new positioning
 *                                   algorithm.
 * J. Kelly         Oct-11-2000      Updated to reflect
 *                                   changes to GUI classes.
 * J. Kelly         Nov-15-2000      Moved formatting code to
 *                                   TreeLayoutPanel.
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
public class VisualTreePanel
  extends    TreeLayoutPanel
  implements ActionListener,
             MouseListener,
             MouseMotionListener
{
  // Class data members

  private static final String SHOW_TRAINING_RESULT = "Show Training Results";
  private static final String SHOW_TESTING_RESULT  = "Show Testing Results";

  private static final int TRAINING = 0;
  private static final int TESTING  = 1;

  // Instance data members

  GradientBar m_gradBar; // Gradient bar (node colors).
  PieChart    m_chart;   // Performance chart.
  Object      m_drag;    // Object being dragged.

  AbstractTreeNode m_tempNode;    // Temporary node.
  VisualTreeNode   m_tempParent;  // Temporary parent node.
  int              m_tempArcNum;  // Temporary arc number.
  AttributeMask    m_tempMask;    // Temporary mask.
  int[]            m_tempConcl;   // Temporary conclusion.
  int[]            m_tempTrainingCounts;  // Temporary counts.
  int[]            m_tempTestingCounts;   // Temporary counts.

  // View control.
  JCheckBoxMenuItem     m_infoOn;       // Show/hide graphic stats
  JCheckBoxMenuItem     m_fullSizeView; // Full size tree display.
  JRadioButtonMenuItem  m_zoom100;      // 100% (full-size) zoom.
  JRadioButtonMenuItem  m_zoom75;       // 75%  (3/4-size)  zoom.
  JRadioButtonMenuItem  m_zoom50;       // 50%  (half-size) zoom.
  JRadioButtonMenuItem  m_zoom25;       // 25%  (1/4-size)  zoom.
  JMenuItem             m_showExampleResult;  // Show training, or testing
                                              // examples.
  JPopupMenu            m_viewMenu;     // View control popup.
  JPopupMenu            m_attMenu;      // Menu of attributes

  int m_resultToShow;   // Determines whether the panel shows
                        // training or testing results.

  // Constructors

  /**
   * Builds a new VisualTreePanel.  The panel has a white
   * background by default.
   *
   * @param manager The global component manager.
   *
   * @throws NullPointerException If the supplied
   *         ComponentManager is null.
   */
  public VisualTreePanel( ComponentManager manager )
  {
    super( manager );

    m_resultToShow = TRAINING;

    // Build the panel structure.
    buildPanel();
  }

  // Public methods

  /**
   * ActionListener interface implementation.  The panel
   * updates itself, and sends messages to other panels
   * as required.
   */
  public void actionPerformed( ActionEvent e )
  {
    Object source = e.getSource();

    // Was it a view action?
    if( source == m_infoOn )
      // Simply repaint to update the display.
      repaint();
    else if( source == m_showExampleResult )
      handleShowResult();
    else if( source == m_fullSizeView )
      handlePanelResize();
    else if( source == m_zoom100 )
      handleZoom( m_zoom100 );
    else if( source == m_zoom75 )
      handleZoom( m_zoom75 );
    else if( source == m_zoom50 )
      handleZoom( m_zoom50 );
    else if( source == m_zoom25 )
      handleZoom( m_zoom25 );
    else if( source instanceof AttributeMenuItem ||
             source instanceof AttributeValueMenuItem )
      // An attribute or value was selected, so we'll
      // be adding an internal or leaf node to the tree.
      handleAttributeOrValueSelected( (JMenuItem)source );
  }

  /**
   * MouseListener interface implementation.  The panel
   * displays various popup menus in response to mouse
   * clicks.
   */
  public void mouseClicked( MouseEvent e )
  {
    // If the right mouse button is clicked,
    // the zoom menu pops up.
    if( SwingUtilities.isRightMouseButton( e ) )
      m_viewMenu.show( e.getComponent(), e.getX(), e.getY() );

    // If the left button is clicked:
    // - find out if it was clicked on a node
    //   and display appropriate info or menu, or
    // - do nothing.
    else if( SwingUtilities.isLeftMouseButton( e ) )
      if( handleNodeClickDetect( e.getX(), e.getY() ) )
        m_attMenu.show( e.getComponent(), e.getX(), e.getY() );
  }

  public void mouseEntered( MouseEvent e )  {}

  public void mouseExited( MouseEvent e )   {}

  public void mousePressed( MouseEvent e )
  {
    // Determine if the user clicked on the pie chart or gradient bar.
    if( SwingUtilities.isLeftMouseButton( e ) &&
        m_infoOn.isSelected() ) {

      if( m_gradBar.hitDetect( e.getX(), e.getY() ) ) {
        m_drag = m_gradBar;
        addMouseMotionListener( this );
      }
      else if( m_chart.hitDetect( e.getX(), e.getY() ) ) {
        m_drag = m_chart;
        addMouseMotionListener( this );
      }
      else {
        m_drag = null;
      }
    }
  }

  public void mouseReleased( MouseEvent e )
  {
    removeMouseMotionListener( this );
  }

  /**
   * MouseMotionListener interface implementation.  A user
   * can click and drag any of the statistical displays
   * to move them out of the way.
   */
  public void mouseDragged( MouseEvent e )
  {
    if( m_drag == m_gradBar )
      m_gradBar.move( e.getX(), e.getY() );
    else if( m_drag == m_chart )
      m_chart.move( e.getX(), e.getY() );

    repaint();
  }

  public void mouseMoved( MouseEvent e ) {}

  public void setViewport( JViewport viewport )
  {
    super.setViewport( viewport );

    // Now that we have a viewport, we can
    // setup a pie chart and gradient color bar
    m_gradBar = new GradientBar( this, viewport );
    m_chart   = new PieChart( this );

    if( m_manager.getAlgorithm() != null ) {
      m_chart.updateTrainingPerformance(
        m_manager.getAlgorithm().getTree().getNumTrainingEgCorrectClass(),
        m_manager.getAlgorithm().getDataset().getNumTrainingExamples() );

      m_chart.updateTestingPerformance(
        m_manager.getAlgorithm().getTree().getNumTestingEgCorrectClass(),
        m_manager.getAlgorithm().getDataset().getNumTestingExamples() );
    }
  }

  /**
   * Notifies the panel that a new tree has been
   * created.  The panel will clear itself in response.
   */
  public void notifyNewTree()
  {
    super.notifyNewTree();

    // Update performance chart.
    if( m_chart != null ) {
      m_chart.reset();

      m_chart.updateTrainingPerformance( 0,
        m_manager.getAlgorithm().getDataset().getNumTrainingExamples() );

      m_chart.updateTestingPerformance( 0,
        m_manager.getAlgorithm().getDataset().getNumTestingExamples() );
    }

    if( m_gradBar != null ) m_gradBar.reset();

    // Repaint the panel.
    repaint();
  }

  /**
   * Notifies the panel that a node has been added to
   * the tree.  The panel recomputes the optimal display
   * configuration for the tree with the new node attached
   * and then repaints the display.
   *
   * @param node The most recently added node.
   */
  public void notifyNodeAdded( DecisionTreeNode node )
  {
    super.notifyNodeAdded( node );
    refresh();
  }

  /**
   * Notifies the panel that a node had been removed
   * from the tree.  The panel recomputes the optimal
   * display configuration for the tree with the
   * node removed and then repaints the display.
   *
   * <p>
   * This method can be used to remove <i>any</i> visual
   * node within the tree - the structure of the entire
   * tree is rebuilt before any painting occurs.
   *
   * @param node The most recently removed node.
   */
  public void notifyNodeRemoved( DecisionTreeNode node )
  {
    super.notifyNodeRemoved( node );
    refresh();
  }

  /**
   * A utility method that refreshes and updates the
   * display (including the pie chart).
   */
  public void refresh()
  {
    // Update the state of the pie chart tracking tree performance.
    if( m_chart != null ) {
      m_chart.updateNumNodes(
        m_manager.getAlgorithm().getTree().getNumNodes(),
        m_manager.getAlgorithm().getTree().getNumInternalNodes() );

      m_chart.updateTrainingPerformance(
        m_manager.getAlgorithm().getTree().getNumTrainingEgCorrectClass(),
        m_manager.getAlgorithm().getDataset().getNumTrainingExamples() );

      m_chart.updateTestingPerformance(
        m_manager.getAlgorithm().getTree().getNumTestingEgCorrectClass(),
        m_manager.getAlgorithm().getDataset().getNumTestingExamples() );
    }

    repaint();
  }

  /**
   * Paints a visual representation of the current tree.
   */
  public void paintComponent( Graphics g )
  {
    super.paintComponent( g );

    // Paint additional graphic objects - the tree
    // will appear underneath.
    if( m_infoOn.isSelected() ) {
      // Draw pie chart, gradient bar etc.
      if( m_chart != null )
        m_chart.paintChart( g );

      if( m_gradBar != null )
        m_gradBar.paintBar( g );
    }
  }

  // Private methods

  /**
   * Toggles the full-size tree view on and off.  The
   * dataset and algorithm panels are hidden when the
   * tree is viewed at full-size.
   */
  private void handlePanelResize()
  {
    if( m_fullSizeView.isSelected() ) {
      if( m_manager.getAlgorithmPanel() != null )
        m_manager.getAlgorithmPanel().setVisible( false );

      if( m_manager.getDatasetPanel() != null )
        m_manager.getDatasetPanel().setVisible( false );
    }
    else {
      if( m_manager.getAlgorithmPanel() != null )
        m_manager.getAlgorithmPanel().setVisible( true );

      if( m_manager.getDatasetPanel() != null )
        m_manager.getDatasetPanel().setVisible( true );
    }

    revalidate();

    if( m_chart != null ) m_chart.adjust();
    if( m_gradBar != null ) m_gradBar.adjust();
  }

  /**
   * Sets the current zoom state, based on the
   * selected zoom level.  The panel is resized and
   * repainted if the zoom level has changed.
   */
  private void handleZoom( JRadioButtonMenuItem zoomLevel )
  {
    boolean changed = false;

    if( zoomLevel == m_zoom100 && SCALING_FACTOR != 1.0 ) {
      changed = true;

      // Set the global zoom level.
      SCALING_FACTOR = 1.0;
    }
    else if( zoomLevel == m_zoom75 && SCALING_FACTOR != 0.75 ) {
      changed = true;

      // Set the global zoom level.
      SCALING_FACTOR = 0.75;
    }
    else if(  zoomLevel == m_zoom50 && SCALING_FACTOR != 0.50 ) {
      changed = true;

      // Set the global zoom level.
      SCALING_FACTOR = 0.50;
    }
    else if( zoomLevel == m_zoom25 && SCALING_FACTOR != 0.25 ) {
      changed = true;

      // Set the global zoom level.
      SCALING_FACTOR = 0.25;
    }

    if( changed ) {
      // Resize the panel and repaint.
      handlePanelAdjust( null );
      repaint();
    }
  }

  /**
   * Handles and coordinates operation when the user
   * selects the 'show training / testing results' menu item.
   */
  private void handleShowResult()
  {
    m_resultToShow = (m_resultToShow == TRAINING ? TESTING : TRAINING );

    // Modify the current menu text.
    if( m_resultToShow == TRAINING ) {
      m_showExampleResult.setText( SHOW_TESTING_RESULT );
      VisualTreeNode.showTrainingPerformance();

      if( m_chart != null )
        m_chart.showTrainingPerformance();
    }
    else {
      m_showExampleResult.setText( SHOW_TRAINING_RESULT );
      VisualTreeNode.showTestingPerformance();

      if( m_chart != null )
        m_chart.showTestingPerformance();
    }

    refresh();
  }

  /**
   * Handles node click detection.  If a user clicks
   * on an existing node in the panel, statistics
   * about the node are displayed in the status bar.
   * If the user clicks on a 'vacant' node, a pop-up
   * menu appears - the menu contains a selection
   * of attributes available for splitting at the node.
   *
   * @return true if a vacant node was clicked on,
   *         or false otherwise.
   */
  private boolean handleNodeClickDetect( int x, int y )
  {
    m_tempNode = null;

    for( int i = 0; i < m_nodes.size(); i++ ) {
      AbstractTreeNode temp = null;

      // Run through the list of internal nodes,
      // looking for a collision.
      if( (temp = ((AbstractTreeNode)
        m_nodes.elementAt( i )).withinBoundingBox( x, y )) != null ) {
        m_tempNode = temp;
        break;
      }
    }

    if( m_tempNode != null ) {
      // If we found a collision, perform the appropriate action.
      if( m_tempNode instanceof VisualTreeNode &&
          m_manager.getStatusBar() != null ) {
        DecisionTreeNode DTNode =
          ((VisualTreeNode)m_tempNode).getDecisionTreeNode();

        // Assemble a text string with info about the node.
        StringBuffer msg = new StringBuffer( "<html><font size=\"-1\">" );

        if( DTNode.isLeaf() )
          msg.append( "Leaf " );
        else
          msg.append( "Internal " );

        msg.append( "node <font color=\"yellow\">" +
        DTNode.getLabel() + "</font> " );

        if( m_resultToShow == TRAINING )
          msg.append( "has a training classification accuracy of " +
          DTNode.getTrainingEgsCorrectClassUsingBestTrainingIndex() +
          " / " + DTNode.getTrainingEgsAtNode() + "." );
        else
          msg.append( "has a testing classification accuracy of " +
          DTNode.getTestingEgsCorrectClassUsingBestTrainingIndex() +
          " / " + DTNode.getTestingEgsAtNode() + "." );

        m_manager.getStatusBar().postMessage( msg.toString() );

        if( !isEnabled() )
          return false;
        else {
          m_tempParent = (VisualTreeNode)m_tempNode.getParent();
          m_tempArcNum = m_tempNode.getArcNum();
        }
      }
      else if( m_tempNode instanceof VacantTreeNode && isEnabled() ) {
        // We have a 'vacant' node, so we figure
        // out who it's parent is, and then ask
        // for a list of available attributes.

        // Save the parent and arc number - if
        // user clicks on an item from the
        // popup menu, we'll need to know about this.
        m_tempParent = (VisualTreeNode)m_tempNode.getParent();
        m_tempArcNum = m_tempNode.getArcNum();
      }
      else {
        if( m_manager.getStatusBar() != null ) {
          String msg =
            new String( "<html><font size=\"-1\">" +
              "You must 'Cancel' the algorithm " +
              "before you can manually build the decision tree." );

          m_manager.getStatusBar().postMessage( msg.toString() );
        }

        return false;
      }
    }
    else if( m_nodes.size() != 0 ) {
      return false;
    }

    Dataset dataset = m_manager.getAlgorithm().getDataset();

    if( m_tempNode != null && m_tempNode.getParent() != null ) {
      DecisionTreeNode dtNode = m_tempParent.getDecisionTreeNode();
      m_tempMask = new AttributeMask( dtNode.getMask() );

      try {
        m_tempMask.mask( dataset.getAttributePosition( dtNode.getLabel() ),
                          m_tempArcNum );
      }
      catch( NonexistentAttributeException e ) {
      }
    }
    else if( m_nodes.size() == 0 || m_tempNode.getParent() == null ) {
      m_tempParent = null;
      m_tempMask = new AttributeMask( dataset.getNumAttributes() );
    }

    // Now we have a mask that indicates where we are
    // in the tree, and what attributes are available
    // at that point - so we build a popup menu that
    // contains the list.
    m_tempConcl  = new int[ 8 ];
    m_tempTrainingCounts =
      new int[ dataset.getTargetAttribute().getNumValues() ];
    m_tempTestingCounts =
      new int[ dataset.getTargetAttribute().getNumValues() ];

    int[] nodeStats = new int[ dataset.getTargetAttribute().getNumValues() ];
    boolean[] reachedHere = new boolean[ dataset.getNumTrainingExamples() ];

    int result =
      m_manager.getAlgorithm().classifyExamples(
        m_tempMask,
        m_tempConcl,
        m_tempTrainingCounts,
        m_tempTestingCounts,
        reachedHere );

    // First, we clear out any old items that
    // might still be in the menu.
    m_attMenu = new JPopupMenu();

    Vector availableAtts =
      m_manager.getAlgorithm().generateStats( m_tempMask, nodeStats );
    Vector attResults = new Vector();

    // Choose an attribute, but discard the result...we're
    // only interested in filling attResults.
    m_manager.getAlgorithm().chooseAttribute(
      availableAtts, nodeStats, attResults );
    generateAttributesPopup( availableAtts, attResults );

    // Highlight rows in the table
    if( m_manager.getDatasetPanel() != null )
      m_manager.getDatasetPanel().setSelectedRows( reachedHere );

    return true;
  }

  /**
   * Builds a popup attribute menu that contains a list
   * of all the attributes available for splitting at
   * a specific position in the tree.
   *
   * @param attributes A Vector that contains all of the
   *        available attributes at the current tree position.
   *
   * @param results A parallel Vector of splitting function
   *        results for each attribute in the attribute
   *        vector.  This vector can be empty (if the splitting
   *        function used did not generate numerical results).
   */
  private void generateAttributesPopup( Vector attributes, Vector results )
  {
    // If there are more than 10 attributes
    // available, we split the menu into multiple
    // submenus - each with 10 elements.
    int numToAdd = attributes.size() < 10 ? attributes.size() : 10;

    // No matter what, we can ALWAYS add or change
    // the leaf node at the current position in
    // the tree - we add the names of the target classes
    // as AttributeValueMenuItems - and this is how we
    // distinguish between attributes and target attribute values.
    Attribute targetAtt =
      m_manager.getAlgorithm().getDataset().getTargetAttribute();

    for( int i = 0; i < targetAtt.getNumValues(); i++ ) {
      try {
        AttributeValueMenuItem item =
          new AttributeValueMenuItem(
            targetAtt.getAttributeValueByNum( i ),
            m_tempConcl[1],
            i,
            m_tempTrainingCounts[i],
            m_tempTestingCounts[i],
            m_tempConcl[5],
            m_tempConcl[4],
            m_tempConcl[6],
            m_tempConcl[7] );

        item.addActionListener( this );
        m_attMenu.add( item );
      }
      catch( NonexistentAttributeValueException e ) {
      }
    }

    if( attributes.size() > 0 )
      m_attMenu.addSeparator();

    // Add the first bunch to the popup menu.
    for( int i = 0; i < numToAdd; i++ ) {
      AttributeMenuItem item = null;

      if( results.size() - 1 < i  )
        item = new AttributeMenuItem(
          (Attribute)attributes.elementAt( i ), null, null );
      else
        item = new AttributeMenuItem(
          (Attribute)attributes.elementAt( i ),
          m_manager.getAlgorithm().getSplittingFunction(),
          (Double)results.elementAt( i ) );

      item.addActionListener( this );
      m_attMenu.add( item );
    }

    // If necessary, add additional menus.
    if( attributes.size() <= 10 ) return;

    JMenu subMenu  = null;
    JMenu prevMenu = null;

    int numItemsRemaining = attributes.size() - 10;
    int pos = 10;

    while( numItemsRemaining > 0 ) {
      prevMenu = subMenu;
      subMenu  = new JMenu( "More..." );

      numToAdd = numItemsRemaining < 10 ? numItemsRemaining : 10;

      for( int j = 0; j < numToAdd; j++ ) {
        AttributeMenuItem item = null;

        if( results.size() - 1 < pos  )
          item = new AttributeMenuItem(
            (Attribute)attributes.elementAt( pos ), null, null );
        else
          item = new AttributeMenuItem(
            (Attribute)attributes.elementAt( pos ),
            m_manager.getAlgorithm().getSplittingFunction(),
            (Double)results.elementAt( pos ) );

        item.addActionListener( this );
        subMenu.add( item );
        pos++;
      }

      numItemsRemaining -= numToAdd;

      if( prevMenu == null )
        m_attMenu.add( subMenu );
      else
        prevMenu.add( subMenu );
    }
  }

  /**
   * Handles attribute or attribute value selection
   * from a popup menu.  A new internal or leaf node is
   * added to the tree, with with attribute's or value's
   * label text.
   *
   * @param menuItem The selected AttributeMenuItem or
   *        AttributeValueMenuItem.
   */
  private void
    handleAttributeOrValueSelected( JMenuItem menuItem )
  {
    DecisionTreeNode parent = null;

    // Find out where we clicked.
    if( m_tempParent != null )
      parent = m_tempParent.getDecisionTreeNode();

    // If we're replacing a pre-existing node,
    // prune the subtree below it.
    if( m_tempNode != null && m_tempNode instanceof VisualTreeNode ) {
      // Each node removed from the backend decision tree will
      // trigger a call to notifyNodeRemoved(), which will
      // remove the corresponding visual node.
      DecisionTreeNode pruneRoot =
        ((VisualTreeNode)m_tempNode).getDecisionTreeNode();
      m_manager.getAlgorithm().getTree().pruneSubtree( pruneRoot );
    }

    if( menuItem instanceof AttributeMenuItem ) {
      try {
        Attribute att = ((AttributeMenuItem)menuItem).getAttribute();
        int pos = m_manager.getAlgorithm()
          .getDataset().getAttributePosition( att.getName() );

        m_manager.getAlgorithm().getTree().addInternalNode(
          parent,
          m_tempArcNum,
          pos,
          att,
          m_tempMask,
          m_tempConcl[1],
          m_tempConcl[0],
          m_tempConcl[2],
          m_tempConcl[3],
          m_tempConcl[5],
          m_tempConcl[4],
          m_tempConcl[6],
          m_tempConcl[7] );
      }
      catch( NonexistentAttributeException e ) {
      }

      if( m_manager.getAlgorithmMenu() != null )
        m_manager.getAlgorithmMenu()
          .setMenuState( AlgorithmMenu.INITIAL_STATE );
    }
    else {
      AttributeValueMenuItem item = (AttributeValueMenuItem)menuItem;

      String valName = item.getAttributeValue();

      Attribute targetAtt =
        m_manager.getAlgorithm().getDataset().getTargetAttribute();

      m_manager.getAlgorithm().getTree().addLeafNode(
        parent,
        m_tempArcNum,
        valName,
        m_tempMask,
        item.getTrainingEgsAtNode(),
        item.getTrainingBestTarget(),
        item.getTrainingEgsCorrectClassUsingBestTrainingIndex(),
        item.getTestingEgsCorrectClassUsingBestTrainingIndex(),
        item.getTestingEgsAtNode(),
        item.getTestingBestTarget(),
        item.getTestingEgsCorrectClassUsingBestTestingIndex(),
        item.getTrainingEgsCorrectClassUsingBestTestingIndex() );

      if( m_manager.getAlgorithmMenu() != null )
        m_manager.getAlgorithmMenu()
          .setMenuState( AlgorithmMenu.INITIAL_STATE );
    }
  }

  /**
   * Builds and arranges the various GUI components
   * for this panel.
   */
  private void buildPanel()
  {
    // Build the view menu.
    m_showExampleResult = new JMenuItem( SHOW_TESTING_RESULT );
    m_showExampleResult.addActionListener( this );

    m_infoOn = new JCheckBoxMenuItem( "Display Tree Statistics Icons" );
    m_infoOn.setSelected( true );
    m_infoOn.addActionListener( this );
    m_fullSizeView = new JCheckBoxMenuItem( "Full Panel Tree View" );
    m_fullSizeView.setSelected( false );
    m_fullSizeView.addActionListener( this );

    m_zoom100 = new JRadioButtonMenuItem( "Zoom Level 100%", true );
    m_zoom100.addActionListener( this );
    m_zoom75  = new JRadioButtonMenuItem( "Zoom Level 75%", false );
    m_zoom75.addActionListener( this );
    m_zoom50  = new JRadioButtonMenuItem( "Zoom Level 50%", false );
    m_zoom50.addActionListener( this );
    m_zoom25  = new JRadioButtonMenuItem( "Zoom Level 25%", false );
    m_zoom25.addActionListener( this );

    ButtonGroup zoomGroup = new ButtonGroup();
    zoomGroup.add( m_zoom100 );
    zoomGroup.add( m_zoom75 );
    zoomGroup.add( m_zoom50 );
    zoomGroup.add( m_zoom25 );

    m_viewMenu = new JPopupMenu();
    addMouseListener( this );

    m_viewMenu.add( m_showExampleResult );
    m_viewMenu.addSeparator();
    m_viewMenu.add( m_infoOn );
    m_viewMenu.add( m_fullSizeView );
    m_viewMenu.addSeparator();
    m_viewMenu.add( m_zoom100 );
    m_viewMenu.add( m_zoom75 );
    m_viewMenu.add( m_zoom50 );
    m_viewMenu.add( m_zoom25 );
  }
}

