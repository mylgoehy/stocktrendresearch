package ai.common;

/**
 * The listener interface that receives &quot;algorithm&quot;
 * events.  Classes that are interested in processing these
 * events should implement this interface.
 *
 * <p>
 * <b>Change History:</b>
 *
 * <p><pre>
 * Name:            Date:            Change:
 * =============================================================
 * J. Kelly         Jun-15-2000      Created.
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
public interface AlgorithmListener
{
  /**
   * Notifies the receiving object that the algorithm
   * has started executing.
   */
  public void notifyAlgorithmStarted();

  /**
   * Notifies the receiving object that the algorithm
   * has started one 'step' (as defined by the
   * particular algorithm).
   */
  public void notifyAlgorithmStepStart();

  /**
   * Notifies the receiving object that the algorithm
   * has started one 'step' (as defined by the
   * particular algorithm).
   *
   * @param text A text message associated with this
   *        step.  The message may or may not be
   *        displayed.
   */
  public void notifyAlgorithmStepStart( String text );

  /**
   * Notifies the receiving object that the algorithm
   * has completed one 'step' (as defined by the
   * particular algorithm).
   */
  public void notifyAlgorithmStepDone();

  /**
   * Notifies the receiving object that the algorithm
   * has finished executing.
   */
  public void notifyAlgorithmFinished();
}
