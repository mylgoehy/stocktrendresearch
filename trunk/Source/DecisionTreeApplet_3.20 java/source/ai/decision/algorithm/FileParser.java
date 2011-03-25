package ai.decision.algorithm;

import java.io.*;
import java.util.*;

/**
 * The FileParser class contains methods that are used to
 * parse decision tree metadata and example files.
 *
 * <p>
 * Metadata files have the following format:
 *
 * <p><pre>
 *
 * &lt;infofile&gt; :==  CONCLUSION =
 *                    &lt;feature&gt;
 *
 *                   FEATURES =
 *                      &lt;feature&gt;,
 *                      ...
 *
 *                   TRAINDATA = &quot;&lt;string&gt;&quot;
 *
 *  &lt;feature&gt; :==  &quot;&lt;string&gt;&quot;
 *                       = { '&lt;string&gt;', ... }
 * </pre>
 *
 * <p>
 * Data files have the following format:
 *
 * <p><pre>
 * &lt;datafile&gt; :== &lt;featureset&gt; \n ...
 *
 * &lt;featureset&gt; :== &lt;string&gt;,...
 * </pre>
 *
 * <p>
 * <b>Change History:</b>
 *
 * <p><pre>
 * Name:            Date:            Change:
 * =============================================================
 * J. Kelly         May-04-2000      Created.
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
public class FileParser
{
  // Instance data members

  LineNumberReader m_lineReader;  // Reads file line-by-line.
  String           m_fileText;    // Text buffer.
  boolean          m_openBracket; // Indicates an open bracket
                                  // has been parsed - used as an
                                  // identifier.

  boolean m_attributeDomain;  // Indicates that the next
                              // item to be parsed is an
                              // attribute domain.
  boolean m_attributesRemain; // Indicates that there are
                              // remaining attributes to
                              // be parsed in this section of
                              // of the file.

  // Constructors

  /**
   * Creates a new FileParser.
   */
  public FileParser()
  {}

  // Public methods

  /**
   * Opens the specified configuration file and prepares
   * it for parsing by reading it into memory.
   *
   * @param metaInputStream A stream attached to the metadata
   *        file for a given data set.
   *
   * @throws IOException If a problem occurs while reading
   *         the configuration file.
   */
  public void startMetaParse( InputStream metaInputStream )
    throws IOException
  {
    // Try to create a new line reader.
    m_lineReader = new LineNumberReader(
    new InputStreamReader( metaInputStream ) );

    // Since the configuration files are small (generally) and
    // need to be parsed, we read the entire file at once and
    // concatenate the text into one long string.

    // At some point, we should use a more robust parsing system.
    StringBuffer tempFileText = new StringBuffer();
    String       tempLine = null;

    while( (tempLine = m_lineReader.readLine()) != null )
      tempFileText.append( tempLine );

    m_fileText = tempFileText.toString();
    m_lineReader = null;
  }

  /**
   * Opens the specified data file and prepares it for
   * parsing.  Data files are read one line at a time.
   *
   * @param dataInputStream A stream attached to the data file
   *        for a given dataset.
   *
   * @throws IOException If a problem occurs while
   *         opening the data file.
   */
  public void startDataParse( InputStream dataInputStream )
    throws IOException
  {
    // Try to open the data file.
    m_lineReader = new LineNumberReader(
      new InputStreamReader( dataInputStream ) );

    // Data files (which can be extremely long) are read
    // and parsed one line at a time.
  }

  /**
   * Moves through the configuration file data (stored in
   * memory) to the position where the target attribute
   * name should be located.
   *
   * @throws InvalidMetaFileException If a syntax
   *         error is found.
   */
  public void moveToTargetAttribute()
    throws InvalidMetaFileException
  {
    stripString( "CONCLUSION" );
    m_attributesRemain = true;
  }

  /**
   * Moves through the configuration file data (stored in
   * memory) to the position where the general attribute
   * information should be located.
   *
   * @throws InvalidMetaFileException If a syntax
   *         error is found.
   */
  public void moveToAttributes()
    throws InvalidMetaFileException
  {
    stripString( "FEATURES" );
    m_attributesRemain = true;
  }

  /**
   * Moves through the configuration file (stored in
   * memory) to the position where the training data file
   * path should be located.
   *
   * @throws InvalidMetaFileException if a syntax
   *         error is found.
   */
  public void moveToDataFilePath()
    throws InvalidMetaFileException
  {
    stripString( "TRAINDATA" );
  }

  /**
   * Returns the character string between the first pair of
   * double quotes.  The method checks for and removes the quotes,
   * any characters between the quotes, and the '=' sign that
   * follows the quotes.
   *
   * @return A String containing the name of the attribute that is
   *         extracted.
   *
   * @throws InvalidMetaFileException If a syntax error is
   *         found.
   */
  public String extractAttributeName()
    throws InvalidMetaFileException
  {
    // Have we already finished parsing all attribute names?
    if( !m_attributesRemain ) return null;

    // Attribute names are enclosed in double-quotes.
    String attributeName = extractString( '\"', '\"', true );

    // Strip the '=' sign after the quoted attribute name.
    if( !stripCharacter( '=' ) )
      throw new
        InvalidMetaFileException( "Syntax error in " +
          "configuration file: No '=' sign after attribute name." );

    // Set the attribute domain flag to indicate that
    // the next thing we should be looking to parse
    // is a set of attribute values.
    m_attributeDomain = true;

    return attributeName;
  }

  /**
   * Returns the character string between the first pair of
   * single quotes. The method checks for and removes the quotes,
   * any characters between the quotes, and any curly brace
   * delimiters.
   *
   * @return A String containing the name of the next
   *         attribute value, or null if there are no
   *         more attribute values.
   */
  public String extractAttributeValue()
    throws InvalidMetaFileException
  {
    // Have we already finished parsing values?
    if( !m_attributeDomain ) return null;

    // Have we started parsing values yet?
    // (i.e. have we stripped out the initial curly brace?)
    if( !m_openBracket ) {
      if( !stripCharacter( '{' ) )
        throw new
          InvalidMetaFileException( "Syntax error " +
            "in configuration file: Missing '{' before attribute values." );

      m_openBracket = true;
    }

    // Extract the value name from the file text.
    String valueName = extractString( '\'', '\'', true );

    // Strip the ',' after the quoted value name.  If
    // there's no ',' character, then look for the closing curly brace.
    if( !stripCharacter( ',' ) ) {
      if( !stripCharacter( '}' ) )
        throw new
          InvalidMetaFileException( "Syntax error in " +
            "configuration file: No ',' or '}' after attribute name." );
      else {
        m_openBracket     = false;  // bracket matched
        m_attributeDomain = false;  // finished this domain

        if( !stripCharacter( ',' ) )
          // There are no more attributes to parse.
          m_attributesRemain = false;
      }
    }

    return valueName;
  }

  /**
   * Extracts and returns a string from the current
   * file text.  Double-quote characters are used as
   * delimeters.  The string and the quotes are removed
   * from the file text.
   *
   * <p>
   * This method exists to keep the file format opaque to parent objects.
   *
   * @throws InvalidMetaFileException If a syntax error is found.
   */
  public String extractString()
    throws InvalidMetaFileException
  {
    return extractString( '\"', '\"', true );
  }

  /**
   * Extracts a sample from the data file.  The method
   * expects that the data file has one sample per line,
   * which consists of a comma-delimited list of
   * attributes.  A line may or not be terminated by an
   * additional comma.
   *
   * @return A Vector of Strings from the comma
   *         delimited sample, in the order they appear
   *         on the line in the file, or null if the
   *         end of the file has been reached.
   *
   * @throws InvalidDataFileException If a syntax error
   *         is found.
   *
   * @throws IOException If an IO error occurs while
   *         reading a line from the file.
   */
  public Vector extractDataSample()
    throws InvalidDataFileException,
           IOException
  {
    do {
      m_fileText = m_lineReader.readLine();

      if( m_fileText == null ) return null;
    }
    while( isEmptyLine( m_fileText ) );

    int pos;
    Vector attribVals = new Vector();

    // Parse the line.
    while( (pos = m_fileText.indexOf( ',' )) > -1 ) {
      if( pos == 0 )
        throw new
          InvalidDataFileException( "Syntax error " +
            "in data file (line " + m_lineReader.getLineNumber() + "): " +
            "Null attribute before comma." );

      attribVals.add( m_fileText.substring( 0, pos ).trim() );

      // Strip off the value we just grabbed, and
      // the trailing comma.
      if( (pos + 1) == m_fileText.length() )
        m_fileText = new String();
      else
        m_fileText = m_fileText.substring( pos + 1 );
    }

    // There may or may not be something left on the
    // line after parsing the last comma - if there
    // is, add it to our values vector.
    if( !isEmptyLine( m_fileText ) )
      attribVals.add( m_fileText.trim() );

    return attribVals;
  }

  /**
   * Returns the line number for the line that was
   * last read from the current file.
   *
   * <p>
   * <b>Note:</b> This method is only useful for
   * data files.
   *
   * @return the current line number, or -1 if no file
   *         is open.
   */
  public int getCurrentLineNum()
  {
    if( m_lineReader == null ) return -1;
    return m_lineReader.getLineNumber();
  }

  // Private methods

  /**
   * Extracts and returns a string from the current
   * file text, using the supplied characters as delimiters
   * (starting from the beginning of the text).  The string
   * and the delimiters are removed from the file text.
   *
   * @param startDelim Delimiter character that marks the
   *        start of the string.
   *
   * @param stopDelim  Delimiter character that marks the
   *        end of the string.
   *
   * @param trim Determines if whitespace is trimmed from
   *        the beginning and the end of the extracted string.
   *        Set to true to remove whitespace, or false
   *        otherwise.
   *
   * @throws InvalidMetaFileException If there
   *         is a problem extracting a String contained
   *         within the specified delimiters.
   */
  private
    String extractString( char startDelim, char stopDelim, boolean trim )
      throws InvalidMetaFileException
  {
    int d1pos, d2pos;      // delimiter positions

    d1pos = m_fileText.indexOf( startDelim );

    if( d1pos < 0 )
      throw new
        InvalidMetaFileException( "Syntax error in " +
          "configuration file: Missing opening " +
          "delimiter character " + startDelim + "." );
    else if( m_fileText.length() == (d1pos+1) )
      throw new
        InvalidMetaFileException( "Syntax error in " +
          "configuration file: Empty after first " +
          "delimiter character." );

    d2pos = m_fileText.indexOf( stopDelim, d1pos + 1 );

    if( d2pos < 0 )
      throw new
        InvalidMetaFileException( "Syntax error in " +
          "configuration file: Missing closing " +
          "delimiter character." );

    if( (d1pos+1) == d2pos )
      // The string is empty.
      throw new
        InvalidMetaFileException( "Syntax error in " +
          "configuration file: String between delimiters is empty." );

    // Extract the attribute name from the file text.
    String extractedString = m_fileText.substring( d1pos+1, d2pos );

    if( (d2pos+1) < m_fileText.length() )
      m_fileText = m_fileText.substring( d2pos+1 );
    else
      // There was nothing after the last delimiter.
      m_fileText = new String( "" );

    if( trim ) return extractedString.trim();

    return extractedString;
  }

  /**
   * Strips the specified string, and anything before
   * it, out of the file text.  The method also searches
   * for and removes the '=' sign immediately following
   * the string.
   *
   * @throws InvalidMetaFileException If the string
   *         is not located in the file text, or there
   *         is no trailing '=' sign.
   */
  private void stripString( String stringToStrip )
    throws InvalidMetaFileException
  {
    // Determine where in the file buffer the string
    //  is located, and strip it, and anything before it, out.
    int pos;

    if( (pos = m_fileText.indexOf( stringToStrip )) == -1 )
      throw new
        InvalidMetaFileException( "Syntax error in " +
          "configuration file: Missing '" +
          stringToStrip + "' keyword(s)." );

    try {
      m_fileText = m_fileText.substring( pos + stringToStrip.length() );
    }
    catch( StringIndexOutOfBoundsException e ) {
      throw new
        InvalidMetaFileException( "Syntax error in " +
          "configuration file: Empty after '" +
          stringToStrip + "' keyword(s)." );
    }

    if( !stripCharacter( '=' ) )
      throw new
        InvalidMetaFileException( "Syntax error in " +
          "configuration file: No '=' sign after '" +
          stringToStrip + "' keyword(s)." );
  }

  /**
   * Starts at the beginning of the current file text
   * and finds the first non-whitespace character.  If
   * the character matches, the method removes
   * the sign and any whitespace before and after it.
   * Otherwise, the method leaves the file text untouched.
   *
   * @param  charToStrip Character to remove.
   *
   * @return true if the character was found and chopped,
   *         false otherwise.
   */
  private boolean stripCharacter( char charToStrip )
  {
    int pos = 0;
    boolean foundChar = false;

    while( pos < m_fileText.length() ) {
      char currChar = m_fileText.charAt( pos );

      if( currChar == ' '  || currChar == '\r' ||
          currChar == '\t' || currChar == '\n' )
        pos++;
      else if( currChar == charToStrip ) {
        foundChar = true;
        break;
      }
      else
        break;
    }

    if( foundChar ) {
      pos++;

      while( pos < m_fileText.length() ) {
        char currChar = m_fileText.charAt( pos );

        if( currChar == ' '  || currChar == '\r' ||
            currChar == '\t' || currChar == '\n' )
          pos++;
        else
          break;
      }

      // Chop the file text.
      if( pos < m_fileText.length() )
        m_fileText = m_fileText.substring( pos );
      else
        // The only thing after the character
        // was whitespace.
        m_fileText = new String( "" );
    }

    return foundChar;
  }

  /**
   * Returns true if the supplied string only
   * contains whitespace (tab and space) characters,
   * or is empty.
   */
  private boolean isEmptyLine( String line )
  {
    if( line.length() == 0 ) return true;

    int pos = 0;
    boolean isEmpty = true;

    while( pos < line.length() ) {
      char currChar = m_fileText.charAt( pos );

      if( currChar == ' '  || currChar == '\r' ||
          currChar == '\t' || currChar == '\n' )
        pos++;
      else {
        isEmpty = false;
        break;
      }
    }

    return isEmpty;
  }
}
