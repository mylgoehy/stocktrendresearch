package ai.common;

import ai.decision.algorithm.*;

/**
 * The listener interface that receives &quot;tree change&quot;
 * events.  Classes that are interested in processing these
 * events should implement this interface.
 *
 * <p>
 * <b>Change History:</b>
 *
 * <p><pre>
 * Name:            Date:            Change:
 * =============================================================
 * J. Kelly         Jun-18-2000      Created.
 * J. Kelly         Sep-24-2000      Revised w/ name change.
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
public interface TreeChangeListener
{
  /**
   * Notifies the receiving object that a new
   * tree has been created.
   */
  public void notifyNewTree();

  /**
   * Notifies the receiving object that a node
   * has been added to the tree.
   *
   * @param node The node most recently added.
   */
  public void notifyNodeAdded( DecisionTreeNode node );

  /**
   * Notifies the receiving object that a node
   * has been removed from the tree.
   *
   * @param node The node most recently removed.
   */
  public void notifyNodeRemoved( DecisionTreeNode node );

  /**
   * Notifies the receiving object that a node in
   * the tree has been modified in some way.
   *
   * @param node The modified node.
   */
  public void notifyNodeModified( DecisionTreeNode node );
}
