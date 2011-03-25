package ai.common;

import java.util.*;

/**
 * A framework class that provides underlying functionality
 * (thread support) for an algorithm subclass.
 *
 * <p>
 * <b>Change History:</b>
 *
 * <p><pre>
 * Name:            Date:            Change:
 * =============================================================
 * J. Kelly         Sep-25-2000      Created.
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
public abstract class AlgorithmFramework
    implements Runnable
{
  // Debug data members

  public boolean DEBUG_ON = true; // Turn on/off debugging info.

  // Class data members

  /**
   * Specifies that the algorithm should be run in 'normal'
   * mode.  The algorithm will run to completion, pausing
   * very briefly at each step.
   */
  public static final int NORMAL_MODE = 0;

  /**
   * Specifies that the algorithm should be run in 'trace'
   * mode.  The algorithm will pause for a longer
   * period of time after each major step and then continue.
   */
  public static final int TRACE_MODE = 1;

  /**
   * Specifies that the algorithm should be run in 'break'
   * mode.  The algorithm will suspend the current thread
   * after each major step.
   */
  public static final int BREAK_MODE = 2;

  //------------------------------------------------------

  /**
   * The amount of time (in ms) to wait between executing each
   * step in the algorithm.  This is set to a default
   * value, but can be modified if necessary.
   */
  public static int DEFAULT_WAIT_TIME = 5000;

  //-------------------------------------------------------

  // Instance data members

  protected int     m_runMode;     // Current run mode.
  protected int     m_waitTime;    // Time to wait between steps.
  protected boolean m_stopFlag;    // Indicates that the algorithm
                                   // should be stopped at the
                                   // next breakpoint.
  protected boolean m_verboseFlag; // Determines whether the
                                   // algorithm uses verbose
                                   // messaging.

  protected LinkedList m_algorithmListeners;
  protected LinkedList m_highlightListeners;

  // Constructors

  /**
   * Prepares to run the algorithm.
   *
   * <p>
   * The default run mode is set to NORMAL_MODE, and
   * the default wait time is set to DEFAULT_WAIT_TIME.
   */
  public AlgorithmFramework()
  {
    m_runMode  = NORMAL_MODE;
    m_waitTime = DEFAULT_WAIT_TIME;

    m_algorithmListeners = new LinkedList();
    m_highlightListeners = new LinkedList();
  }

  // Public methods

  /**
   * Registers an AlgorithmListener.  Whenever the algorithm
   * starts, stops, or completes one 'step' all
   * AlgorithmListeners are notified.
   *
   * @param l The AlgorithmListener to add.
   */
  public void addAlgorithmListener( AlgorithmListener l )
  {
    if( l == null || m_algorithmListeners.contains( l ) ) return;
    m_algorithmListeners.add( l );
  }

  /**
   * Removes an AlgorithmListener from the listener list.
   *
   * @param l The AlgorithmListener to be removed.  If the
   *          supplied AlgorithmListener is null, or is
   *          not a member of the current list, no action
   *          is taken.
   */
  public void removeAlgorithmListener( AlgorithmListener l )
  {
    m_algorithmListeners.remove( l );
  }

  /**
   * Registers a HighlightListener.  Whenever one step
   * in the algorithm is completed, all HighlightListeners
   * are notified.
   *
   * @param l The HighlightListener to add.
   */
  public void addHighlightListener( HighlightListener l )
  {
    if( l == null || m_highlightListeners.contains( l ) ) return;
    m_highlightListeners.add( l );
  }

  /**
   * Removes an HighlightListener from the listener list.
   *
   * @param l The HighlightListener to be removed.  If the
   *          supplied HighlightListener is null, or is
   *          not a member of the current list, no action
   *          is taken.
   */
  public void removeHighlightListener( HighlightListener l )
  {
    m_highlightListeners.remove( l );
  }

  /**
   * Returns the current verbosity setting.
   *
   * @return true if verbose output is enabled, false otherwise.
   */
  public boolean getVerbose()
  {
    return m_verboseFlag;
  }

  /**
   * Turns verbose output on and off.
   *
   * @param verbose Setting the parameter to true causes
   *        the algorithm to post status messsages as it
   *        executes.  Setting the parameter to false
   *        disables messaging (the algorithm executes
   *        silently).
   */
  public void setVerbose( boolean verbose )
  {
    m_verboseFlag = verbose;
  }

  /**
   * Returns the current wait time between algorithm steps.
   *
   * @return The current wait time (in ms) between algorithm
   *         steps.
   */
  public int getWaitTime()
  {
    return m_waitTime;
  }

  /**
   * Sets the pause time inbetween algorithm steps while
   * running in <code>TRACE_MODE</code>.
   *
   * @param time New wait time, in ms.  Negative values
   *        are ignored (i.e. the wait time is set to
   *        zero).
   */
  public void setWaitTime( int time )
  {
    m_waitTime = time > 0 ? time : 0;
  }

  /**
   * Sets the current run mode.  There are three possible
   * modes:
   *
   * <p>
   * <ul>
   *     <li><code>NORMAL_MODE</code>, which runs the
   *         algorithm to completion.
   *     <li><code>TRACE_MODE</code>, which pauses
   *         after each major algorithm step.
   *     <li><code>BREAK_MODE</code>, which suspends
   *         the current thread after each major
   *         algorithm step.
   * </ul>
   *
   * <p>
   * This method is synchronized to ensure that the
   * run mode can only be changed at appropriate times
   * (i.e. when the algorithm is stopped or suspended).
   *
   * @param mode The desired run mode.
   *
   * @throws IllegalArgumentException If the supplied
   *         mode is unrecognized.
   */
  public synchronized void setRunMode( int mode )
  {
    if( mode != NORMAL_MODE && mode != TRACE_MODE  && mode != BREAK_MODE )
      throw new
        IllegalArgumentException( "Unrecognized run mode." );

    m_runMode = mode;
  }

  /**
   * If the algorithm is currently running, a call
   * to this method causes it to terminate at the next
   * breakpoint.
   */
  public synchronized void stopRunning()
  {
    m_stopFlag = true;
    notifyAll();
  }

  /**
   * If the algorthm has reached a breakpoint in
   * BREAK_MODE, this method causes it to continue
   * executing.  Otherwise, a call to the method has
   * no effect.
   */
  public synchronized void continueRunning()
  {
    // Notify all waiting threads that it's ok to continue.
    notifyAll();
  }

  public abstract void run();

  public abstract void reset();

  // Protected methods

  /**
   * Handles breakpoints in the algorithm.  If a stop
   * request has been made, the algorithm will
   * terminate immediately.  Otherwise, the method
   * notifies any listeners that may be waiting and then
   * does one of the following:
   *
   * <p>
   * <ul>
   *     <li>In <code>NORMAL_MODE</code>, forces the current
   *         thread to wait very briefly before continuing.
   *     <li>In <code>TRACE_MODE</code>, forces the current
   *         thread to wait for a period of time before
   *         continuing.
   *     <li>In <code>BREAK_MODE</code>, forces the current
   *         thread to wait indefinately.  A call to
   *         <code>continueRunning()</code> allows the
   *         algorithm to continue executing.
   * </ul>
   *
   * <p>
   * Note that the algorithm should run in a separate thread
   * - otherwise, a call to run() in <code>BREAK_MODE</code>
   * will result in deadlock.
   */
  protected boolean handleBreakpoint( int line, String msg )
  {
    Iterator i;

    // If the stop flag is set, terminate immediately.
    if( m_stopFlag ) {
      // Inform any listeners that highlights should be cleared.
      i = m_highlightListeners.iterator();

      while( i.hasNext() )
        ((HighlightListener)i.next()).notifyClearHighlight();

      m_stopFlag = false;
      return false;
    }

    if( m_runMode == NORMAL_MODE ) {
      try {
        // Wait very briefly before continuing.
        wait( 4 );
      }
      catch( InterruptedException e ) {
      }
    }

    // Now, we have to double check - if the run mode changed
    // while we were waiting above we may have to wait again.
    if( m_runMode == TRACE_MODE || m_runMode == BREAK_MODE ) {
      // Notify any AlgorithmListeners that we've reached
      // a breakpoint.
      i = m_algorithmListeners.iterator();

      if( m_verboseFlag && msg != null )
        while( i.hasNext() )
          ((AlgorithmListener)i.next()).notifyAlgorithmStepStart( msg );
      else
        while( i.hasNext() )
          ((AlgorithmListener)i.next()).notifyAlgorithmStepStart();

      // Highlight the appropriate line of code.
      i = m_highlightListeners.iterator();

      while( i.hasNext() )
        ((HighlightListener)i.next()).notifyHighlight( line );

      try {
        if( m_runMode == TRACE_MODE ) {
          // Wait before continuing.
          wait( m_waitTime );

          if( m_runMode == BREAK_MODE ) wait();
        }
        else {
          // Wait indefinately.
          wait();
        }
      }
      catch( InterruptedException e ) {
      }

      // Notify listeners.
      i = m_algorithmListeners.iterator();

      while( i.hasNext() )
        ((AlgorithmListener)i.next()).notifyAlgorithmStepDone();

      i = m_highlightListeners.iterator();

      while( i.hasNext() )
        ((HighlightListener)i.next()).notifyClearHighlight();
    }

    return true;
  }
}

