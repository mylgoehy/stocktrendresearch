package ai.common;

import java.net.*;
import java.io.*;
import java.util.*;
import java.awt.*;
import javax.swing.*;

/**
 * A CodePanel displays pseudo-code in a scrollable, highlightable window.
 *
 * <p>
 * CodePanel expects pseudo-code files to conform to a specific
 * syntax.  Functions are identified by the <code>&lt;function&gt;
 * </code> HTML-like pseudo-tag (with a <code>&lt;/function&gt;
 * </code> closing tag). A line break in the pseudo-code is marked
 * by a period, which should be the last non-whitespace
 * character on a line.  The periods are automatically stripped before the
 * text is stored.
 *
 * <p>
 * <b>Change History:</b>
 *
 * <p><pre>
 * Name:            Date:            Change:
 * =============================================================
 * J. Kelly         May-31-2000      Created. Based on original
 *                                   work by S. Nychka.
 * J. Kelly         Jun-04-2000      Added Highlighter interface.
 * J. Kelly         Jul-10-2000      Added dynamic text display
 *                                   capability.
 * J. Kelly         Oct-01-2000      Revised with
 *                                   HighlightListener interface.
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
public class CodePanel
    extends    JPanel
    implements HighlightListener
{
  // Class data members

  // Marks the beginning of a function tag.
  private static final String FUNCTION_START = "<function";

  // Marks the start of the name of a function (inside
  // the 'function' tag).
  private static final String NAME_START = "name=\"";

  // Marks the end of the name of a function (inside
  // the 'function' tag).
  private static final String NAME_END = "\">";

  // Marks the end of a function.
  private static final String FUNCTION_END = "</function>";

  // Marks a tab insertion point.
  private static final String TAB_MARKER = "<tab>";

  // HTML character string that replaces a tab marker.
  private static final String TAB = "&nbsp;&nbsp;";

  // Marks the start of a dynamic portion of algorithm
  // text. A custom text string can be inserted between
  // a pair of 'dynamic' tags.
  private static final String DYNAMIC_START = "<meta";

  // Marks the end of a dynamic portion of algorithm text.
  private static final String DYNAMIC_END = "</meta>";

  // This string must preceed every line - it
  // informs JLabel that the text should be rendered
  // as HTML, and sets the #&&@! font size
  private static final String HTML_LINE_HEADER = "<html><font size=-2>";

  // This string terminates every line.
  private static final String HTML_LINE_TERMINATOR = "</font>";

  // The height of each cell in the list.  This can
  // be adjusted, depending on the size of the text, to
  // keep the rows as closely spaced as possible.
  // Otherwise, the default JLabel renderer tends to
  // add too much excess space above and below each line
  // of text.
  private static final int ROW_HEIGHT = 16;

  // Instance data members

  // A Vector of functions, where each function is a Vector of
  // Strings, and each String is one line of code.
  private Vector m_functions;

  // Vector in parallel to 'functions', which contains the names
  // of each function.
  private Vector m_functionNames;

  // Name of the function currently being displayed.
  String m_functionName;

  // The list that displays the code.
  JList m_list;

  // Constructors

  /**
   * Builds a new CodePanel.  The panel initially
   * displays the first function parsed from the
   * pseudo-code file.
   *
   * @param repository The URL where available code text
   *        is located.
   *
   * @param codeFile The file to read code text from.  The
   *        method attempts to open the effective URL:
   *        repository + codeFile for reading.
   *
   * @throws NullPointerException If the supplied URL
   *         is null.
   *
   * @throws MalformedURLException If the URL protocol is
   *         unrecognized.
   *
   * @throws IOException If a problem occurs while
   *         reading data from the specified URL.
   *
   * @throws InvalidCodeFileException If the code file
   *         contains syntax errors.
   */
  public CodePanel( URL repository, String codeFile )
    throws MalformedURLException,
           IOException,
           InvalidCodeFileException
  {
    super();

    if( repository == null )
      throw new NullPointerException( "Repository URL is null." );

    // Build the panel structure.
    buildPanel();

    // Load and parse the code file.
    CodeReader codeSrc = new CodeReader( repository );
    Vector lines = codeSrc.read( codeFile, false );
    parseLines( lines );
  }

  // Public methods

  /**
   * Returns a Vector which contains the names of all functions that
   * are available for display.
   *
   * @return A Vector of Strings, where each String is the name of a
   *         function that can be displayed.
   */
  public Vector getFunctionNames()
  {
    return (Vector)m_functionNames.clone();
  }

  /**
   * Returns the number of functions that are available for display.
   *
   * @return The total number of functions stored and available for display.
   */
  public int getNumFunctions()
  {
    return m_functions.size();
  }

  /**
   * Searches for the function with the given function
   * name, and locates the line containing a dynamic text
   * tag with the supplied 'dynamic' ID.  Replaces any
   * text between the dynamic tags with <code>text</code>
   *
   * <p>
   * If no function with the supplied name exists, or the
   * function doesn't contain the dynamic name tag, no
   * changes are made.
   *
   * @param functionName The name of the function in which
   *        text should be modified.
   *
   * @param dynamicName The name of the 'dynamic' pseudo-tag
   *        (embedded in the tag itself) to locate. Any
   *        text between the opening and closing dynamic
   *        tags will be replaced.
   *
   * @param text The replacement text string.
   */
  public void setDynamicText( final String functionName,
                              final String dynamicName,
                              final String text )
  {
    Runnable setDynamicText =
      new Runnable() {
        public void run() {
          // Find the function with the supplied name.
          int functionNum = -1;

          for( int i = 0; i < m_functionNames.size(); i++ )
            if( ((String) m_functionNames.elementAt( i ))
              .equals( functionName ) )
              functionNum = i;

          if( functionNum == -1 ) return;  // Didn't find it.

          Vector functionVec = (Vector)m_functions.elementAt( functionNum );

          // Locate a line that contains the dynamic tag with
          // the supplied name.
          for( int j = 0; j < functionVec.size(); j++ ) {
            int pos = 0;
            String line = (String)functionVec.elementAt( j );

            while( (pos = line.indexOf( DYNAMIC_START, pos )) != -1 ) {
              // Check to see if the name is correct.
              // We don't do any rigorous syntax checking here.
              pos = line.indexOf( NAME_START, pos + DYNAMIC_START.length() );

              if( pos == -1 ) break;

              int nameStart = pos + NAME_START.length();
              int nameEnd   = line.indexOf( NAME_END,
                              pos + NAME_START.length() );

              String name = line.substring( nameStart, nameEnd );

              if( !name.equals( dynamicName ) ) break;

              // We've got a match - figure out where the closing dynamic
              // tag is, and replace all the text in between.
              int dynamicEnd =
                line.indexOf( DYNAMIC_END, nameEnd + NAME_END.length() );

              if( dynamicEnd == -1 ) break;

              functionVec.set( j, new String(
                line.substring( 0, nameEnd + NAME_END.length() ) +
                text + line.substring( dynamicEnd ) ) );
            }
          }

          if( m_functionName != null && m_functionName.equals( functionName ) )
            // We need to refresh the display.
            displayFunction( m_functionName );
          }
        };

    try {
      if( SwingUtilities.isEventDispatchThread() )
        setDynamicText.run();
      else
        SwingUtilities.invokeLater( setDynamicText );
    }
    catch( Exception e ) {
      e.printStackTrace();
    }
  }

  /**
   * Displays the function with the supplied name
   * in the code window.  This method is thread-safe.
   *
   * @param name The name of the function to display.
   *        If no function with the supplied name
   *        exists, the display remains unchanged.
   */
  public void displayFunction( final String name )
  {
    Runnable displayFunction =
      new Runnable() {
        public void run() {
          // Locate the position of the function
          // in the internal storage vector.
          for( int i = 0; i < m_functionNames.size(); i++ )
            if( ((String)m_functionNames.elementAt( i )).equals( name ) ) {
              m_list.setListData( (Vector)m_functions.elementAt( i ) );
              m_functionName = new String( name );
              return;
            }
          }
        };

    try {
      if( SwingUtilities.isEventDispatchThread() )
        displayFunction.run();
      else
        SwingUtilities.invokeLater( displayFunction );
    }
    catch( Exception e ) {
      e.printStackTrace();
    }
  }

  /**
   * Displays the function at the specified index
   * in the code window.  The index corresponds to the
   * position of the function in the original code file,
   * with the first function at index position 1.  This
   * method is thread-safe.
   *
   * @param index The index position of the function
   *        to display.  If the index is out of
   *        range, the display remains unchanged.
   */
  public void displayFunction( final int index )
  {
    if( index < 1 || index > m_functions.size() ) return;

    Runnable displayFunction =
      new Runnable() {
        public void run() {
          Vector listData =
            (Vector)m_functions.elementAt( index - 1 );
          m_list.setListData( listData );
          m_functionName =
            new String( (String)m_functionNames.elementAt( index - 1) );
        }
      };

    try {
      if( SwingUtilities.isEventDispatchThread() )
        displayFunction.run();
      else
        SwingUtilities.invokeLater( displayFunction );
    }
    catch( Exception e ) {
      e.printStackTrace();
    }
  }

  /**
   * Highlights the specified line of code in the
   * code window.  This method is thread-safe.
   *
   * @param lineNum The line number to highlight.
   *        The first line in the window is line 1.
   *        If the line number is out of range, the
   *        display remains unchanged.
   */
  public void notifyHighlight( final int lineNum )
  {
    if( lineNum < 1 || lineNum > m_list.getModel().getSize() ) return;

    Runnable highlight =
      new Runnable() {
        public void run() {
          m_list.ensureIndexIsVisible( lineNum - 1 );
          m_list.setSelectedIndex( lineNum - 1 );
        }
      };

    try {
      if( SwingUtilities.isEventDispatchThread() )
        highlight.run();
      else
        SwingUtilities.invokeLater( highlight );
    }
    catch( Exception e ) {
      e.printStackTrace();
    }
  }

  /**
   * Clears the highlighted line in the code window.
   * This method is thread-safe.
   */
  public void notifyClearHighlight()
  {
    Runnable clearHighlight =
      new Runnable() {
        public void run() {
          m_list.getSelectionModel().clearSelection();
        }
      };

    try {
      if( SwingUtilities.isEventDispatchThread() )
        clearHighlight.run();
      else
        SwingUtilities.invokeLater( clearHighlight );
    }
    catch( Exception e ) {
      e.printStackTrace();
    }
  }

  // Private methods

  /**
   * Builds and arranges the various GUI components
   * for this panel.
   */
  private void buildPanel()
  {
    GridBagLayout layout = new GridBagLayout();
    setLayout( layout );
    GridBagConstraints c = new GridBagConstraints();

    // Currently, there is only a single cell inside
    // the panel, which holds the scrollable list.
    c.gridx = 0;
    c.gridy = 0;
    c.gridwidth  = 1;
    c.gridheight = 1;
    c.weightx = 1.0;
    c.weighty = 1.0;
    c.fill = GridBagConstraints.BOTH;

    m_list = new JList();

    // The list is disabled, so the user can't manually
    // highlight lines of code.
    m_list.setEnabled( false );

    // Fix the cell height, so that lines of
    // pseudocode are spaced as closely as possible.
    m_list.setFixedCellHeight( ROW_HEIGHT );

    JScrollPane scrollPane = new JScrollPane( m_list );
    scrollPane.setHorizontalScrollBarPolicy(
      JScrollPane.HORIZONTAL_SCROLLBAR_AS_NEEDED );
    scrollPane.setVerticalScrollBarPolicy(
      JScrollPane.VERTICAL_SCROLLBAR_ALWAYS );

    layout.setConstraints( scrollPane, c );
    add( scrollPane );
  }

  /**
   * Parses the lines contained in the 'lines' Vector,
   * extracting and assembling the various functions.
   *
   * @throws InvalidCodeFileException If the code file
   *         contains a syntax error.
   */
  private void parseLines( Vector lines )
    throws InvalidCodeFileException
  {
    m_functions     = new Vector();
    m_functionNames = new Vector();

    while( true ) {
      // Find the start of a function.
      String text = null;

      if( lines.size() == 0 ) return;

      while( lines.size() != 0 ) {
        text = (String)lines.remove( 0 );

        if( text.indexOf( FUNCTION_START ) != -1 ) break;

        if( lines.size() == 0 ) return;
      }

      // Extract the name of the function and
      // store it.
      text = extractFunctionName( text );

      // Extract the lines of this function.
      extractFunctionLines( text, lines );
    }
  }

  /**
   * Extract the name of a function from a 'function'
   * tag contained in the supplied line of text.
   *
   * @return The remainder of the line, after the
   *         'function' tag has been removed, or null
   *         if the line is empty after the tag.
   *
   * @throws InvalidCodeFileException If a function
   *         name cannot be extracted.
   */
  private String extractFunctionName( String line )
    throws InvalidCodeFileException
  {
    // Ignore the start of the line.
    String parsedLine;

    int functionStart =
      line.indexOf( FUNCTION_START ) + FUNCTION_START.length();

    try {
      parsedLine = line.substring( functionStart );
    }
    catch( IndexOutOfBoundsException e ) {
      throw new
        InvalidCodeFileException( "Missing 'name' " +
        "parameter after function tag start." );
    }

    int nameStart;

    // Grab the function name.
    if( (nameStart = parsedLine.indexOf( NAME_START ) ) != -1 ) {
      nameStart += NAME_START.length();
      int nameEnd = parsedLine.indexOf( NAME_END );

      if( nameEnd > nameStart ) {
        // Extract name.
        m_functionNames.add( parsedLine.substring( nameStart, nameEnd ) );

        if( parsedLine.length() > (nameEnd + NAME_END.length()) )
          return parsedLine.substring( nameEnd + NAME_END.length() );
        else
          return null;
      }
      else {
        throw new
          InvalidCodeFileException( "Function name " +
            "missing or tag malformed." );
      }
    }
    else {
      throw new
        InvalidCodeFileException( "Function name " +
          "missing or tag malformed." );
    }
  }

  /**
   * Extract the lines of text that form the function
   * body.
   *
   * @param text A String containing any text remaining
   *        from previous parsing.
   *
   * @param lines Remaining lines of code text.
   *
   * @throws InvalidCodeFileException If the pseudo-code
   *         file contains a syntax error.
   *
   */
  private void extractFunctionLines( String text, Vector lines )
    throws InvalidCodeFileException
  {
    StringBuffer codeLine = new StringBuffer( HTML_LINE_HEADER );
    String       currLine = null;
    Vector       function = new Vector();

    if( text != null )
      currLine = new String( text );
    else
      currLine = new String();

    while( true ) {
      while( !currLine.endsWith( "." ) ) {
        if( currLine.length() > 0 )
          codeLine.append( currLine );

        if( lines.size() == 0 )
          throw new
            InvalidCodeFileException( "Missing " +
              "line terminator and closing function tag." );

        currLine = ((String)lines.remove( 0 )).trim();
      }

      // Strip the period off the end of the line.
      codeLine.append( currLine.substring( 0, currLine.length() - 1 ) );

      // Grabbed one complete line.
      String newLine = modifyTabs( codeLine ) + HTML_LINE_TERMINATOR;

      function.add( newLine );
      codeLine = new StringBuffer( HTML_LINE_HEADER );

      if( lines.size() == 0 )
        throw new
          InvalidCodeFileException( "Missing closing function tag." );

      currLine = (String)lines.remove( 0 );

      if( currLine.trim().endsWith( FUNCTION_END ) ) {
        m_functions.add( function );
        return;
      }
    }
  }

  /**
   * Performs a search and replace, removing all the tab
   * markers on a line and replacing them with their
   * HTML equivalent.
   *
   * @return A String with all tab markers converted
   *         to HTML 'non-breaking' spaces.
   */
  private String modifyTabs( StringBuffer text )
  {
    while( true ) {
      String line = text.toString();

      int pos;

      if( (pos = line.indexOf( TAB_MARKER )) == - 1 ) return text.toString();
        text.replace( pos, pos + TAB_MARKER.length(), TAB );
    }
  }
}
