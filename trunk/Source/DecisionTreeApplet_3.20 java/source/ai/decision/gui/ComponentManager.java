package ai.decision.gui;

import ai.common.*;
import ai.decision.algorithm.*;

/**
 * A simple reference storage class.  A ComponentManager
 * handles all references to GUI and backend components.
 * This removes the need to supply each panel and menu
 * with numerous individual references.  Instead, components
 * can query the ComponentManager for the required object reference.
 * If a particular component isn't currently available, the
 * component manager will return <code>null</code> for that item.
 *
 * <p>
 * <b>Change History:</b>
 *
 * <p><pre>
 * Name:            Date:            Change:
 * =============================================================
 * J. Kelly         Oct-04-2000      Created.
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
public class ComponentManager
{
  // Instance data members

  DecisionTreeAlgorithm m_algorithm;
  Thread                m_algorithmThread;

  DatasetMenu     m_datasetMenu;
  AlgorithmMenu   m_algorithmMenu;

  DatasetPanel    m_datasetPanel;
  AlgorithmPanel  m_algorithmPanel;
  VisualTreePanel m_visualTreePanel;
  StatusBar       m_statusBar;

  // Constructors

  // Public methods

  public DecisionTreeAlgorithm getAlgorithm()
  {
    return m_algorithm;
  }

  public synchronized void setAlgorithm( DecisionTreeAlgorithm algorithm )
  {
    m_algorithm = algorithm;
    notifyAll();
  }

  public Thread getAlgorithmThread()
  {
    return m_algorithmThread;
  }

  public synchronized void setAlgorithmThread( Thread thread )
  {
    m_algorithmThread = thread;
    notifyAll();
  }

  public DatasetMenu getDatasetMenu()
  {
    return m_datasetMenu;
  }

  public synchronized void setDatasetMenu( DatasetMenu menu )
  {
    m_datasetMenu = menu;
  }

  public AlgorithmMenu getAlgorithmMenu()
  {
    return m_algorithmMenu;
  }

  public synchronized void setAlgorithmMenu( AlgorithmMenu menu )
  {
    m_algorithmMenu = menu;
  }

  public DatasetPanel getDatasetPanel()
  {
    return m_datasetPanel;
  }

  public synchronized void setDatasetPanel( DatasetPanel panel )
  {
    m_datasetPanel = panel;
    notifyAll();
  }

  public AlgorithmPanel getAlgorithmPanel()
  {
    return m_algorithmPanel;
  }

  public synchronized void setAlgorithmPanel( AlgorithmPanel panel )
  {
    m_algorithmPanel = panel;
    notifyAll();
  }

  public VisualTreePanel getVisualTreePanel()
  {
    return m_visualTreePanel;
  }

  public synchronized void setVisualTreePanel( VisualTreePanel panel )
  {
    m_visualTreePanel = panel;
    notifyAll();
  }

  public StatusBar getStatusBar()
  {
    return m_statusBar;
  }

  public synchronized void setStatusBar( StatusBar statusBar )
  {
    m_statusBar = statusBar;
    notifyAll();
  }
}
