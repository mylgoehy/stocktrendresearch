package ai.decision.gui;

import java.awt.*;
import java.awt.event.*;
import javax.swing.*;
import ai.common.*;

/**
 * A GUI panel that displays text messages.  A message
 * can contain HTML tags if special formatting is required.
 *
 * <p>
 * <b>Change History:</b>
 *
 * <p><pre>
 * Name:            Date:            Change:
 * =============================================================
 * J. Kelly         May-24-2000      Created.
 * J. Kelly         May-31-2000      Added Messenger interface.
 * J. Kelly         Jul-10-2000      Added timer to auto-remove
 *                                   display text after a given
 *                                   interval.
 * J. Kelly         Sep-26-2000      Added to Algorithm-Listener
 *                                   interface.
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
public class StatusBar
  extends    JPanel
  implements AlgorithmListener,
             ActionListener
{
  // Class data members

  /**
   * The default amount of time to wait (in ms) before
   * clearing any text in the status bar.
   */
  public static final int DEFAULT_DELAY_TIME = 12000;

  // Instance data members

  JLabel m_statusText;  // Label in the status bar that
                        // contains the current message.
  Timer  m_timer;       // Timer for text clear.
  int    m_delay;       // Time to delay before clearing
                        // panel text.

  // Constructors

  /**
   * Creates a new (empty) status bar.
   */
  public StatusBar()
  {
    this( null );
  }

  /**
   * Creates a new status bar that displays the supplied
   * text.  The bar does not set its own dimensions
   * initially, although it <i>will</i> resize itself
   * depending on the amount of text displayed.
   *
   * <p>
   * Text will be displayed in the default Swing
   * JPanel font.
   *
   * @param text The initial text string to display.
   */
  public StatusBar( String text )
  {
    super();

    // Create a new label to hold the status text.
    if( text == null )
      m_statusText = new JLabel();
    else
      m_statusText = new JLabel( text );

    m_statusText.setBorder( BorderFactory.createEmptyBorder( 2, 3, 2, 3 ) );
    m_statusText.setFont( this.getFont() );
    m_statusText.setForeground( Color.black );

    // Build the panel.  We use a BoxLayout, so that
    // we can left-justify the label in the status bar.
    setLayout( new BoxLayout( this, BoxLayout.X_AXIS ) );
    add( m_statusText );
    add( Box.createVerticalStrut( 24 ) );
    add( Box.createHorizontalGlue() );
    setBorder( BorderFactory.createLoweredBevelBorder() );

    // Setup the timer.
    m_timer = new Timer( DEFAULT_DELAY_TIME, this );
    m_timer.setRepeats( false );
  }

  /**
   * Creates a new status bar that displays the supplied
   * text. the bar does not set its own dimensions
   * initially, although it <i>will</i> resize itself
   * depending on the amount of text displayed.
   *
   * @param text The initial text string to display.
   *
   * @param font The display font.
   */
  public StatusBar( String text, Font font )
  {
    this( text );
    setFont( font );
  }

  // Public methods

  /**
   * ActionListener inteface implementation.
   */
  public void actionPerformed( ActionEvent e )
  {
    if( e.getSource() == m_timer )
    // Clear the status bar text.
    m_statusText.setText( null );
  }

  public void notifyAlgorithmStarted() {}

  /**
   * Sets the text in the status bar.  This method
   * is thread-safe.
   *
   * @param text The text string to display. If the
   *        argument is null then no change is made
   *        to the current text.
   */
  public void postMessage( final String text )
  {
    Runnable postMessage =
      new Runnable() {
        public void run() {
          if( text != null ) {
            m_statusText.setText( text );
            m_timer.restart();
          }
        }
      };

    try {
      if( SwingUtilities.isEventDispatchThread() )
        postMessage.run();
      else
        SwingUtilities.invokeLater( postMessage );
    }
    catch( Exception e ) {
    }
  }

  /**
   * Clears the text in the status bar.  This method
   * is thread-safe.
   */
  public void clearMessage()
  {
    Runnable clearMessage =
      new Runnable() {
        public void run() {
          m_statusText.setText( null );
          m_timer.stop();
        }
      };

    try {
      if( SwingUtilities.isEventDispatchThread() )
        clearMessage.run();
      else
        SwingUtilities.invokeLater( clearMessage );
    }
    catch( Exception e ) {
    }
  }

  public void notifyAlgorithmStepStart( final String text )
  {
    postMessage( text );
  }

  public void notifyAlgorithmStepStart() {}

  public void notifyAlgorithmStepDone()
  {
    clearMessage();
  }

  public void notifyAlgorithmFinished() {}

  /**
   * Sets the font for the label contained within
   * the panel, as well as for the panel itself.
   */
  public void setFont( Font font )
  {
    super.setFont( font );

    if( m_statusText != null )
    m_statusText.setFont( font );
  }
}
