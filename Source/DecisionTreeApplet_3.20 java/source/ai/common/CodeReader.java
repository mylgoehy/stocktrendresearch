package ai.common;

import java.io.*;
import java.net.*;
import java.util.*;

/**
 * Reads each line from a specified file and returns a Vector
 * of those lines as String objects.
 *
 * <p>
 * <b>Change History:</b>
 *
 * <p><pre>
 * Name:            Date:            Change:
 * =============================================================
 * S. Nychka        May-01-2000      Created.
 * J. Kelly         May-23-2000      Javadoc'ified.  Reader now
 *                                   grabs data from a URL.
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
public class CodeReader
{
  // Class data members

  /**
   * Uniform-width 'tab' string composed of spaces.
   */
  public static final String TAB = "     ";

  private static final char SLASH = '/';

  private static final char ASTERISK = '*';

  // Instance data members

  URL m_repository;   // URL of file to read code text from

  // Constructors

  /**
   * Creates a new CodeReader.
   *
   * @param repository The URL where code text is located.
   */
  public CodeReader( URL repository )
  {
    m_repository = repository;
  }

  // Public methods

  /**
   * Reads lines from the current file into a Vector,
   * stripping comments if desired.
   *
   * @param codeFile The file to read code text from.  The
   *        method attempts to open the effective URL:
   *        repository + codeFile for reading.
   *
   * @param comments If true, comments are included in the
   *        lines returned. Otherwise, comments are stripped
   *        from the file.
   *
   * @return A Vector that contains lines from the file.
   *
   * @throws MalformedURLException If the current
   *         repository URL protocol is unrecognized.
   *
   * @throws IOExeption If the code file cannot be read.
   */
  public Vector read( String codeFile, boolean comments )
    throws MalformedURLException,
           IOException
  {
    // Build the code file URL.
    URL codeFileURL = new URL( m_repository + codeFile );

    BufferedReader codeReader =
      new BufferedReader( new InputStreamReader( codeFileURL.openStream() ) );

    if( comments )
      // Leave comments in.
      return read( codeReader );
    else
      // Strip comments.
      return readNoComments( codeReader);
  }

  /**
   * Removes a substring from a string.
   *
   * @param entire String from which 'sub' is to be stripped.
   *
   * @param sub String to be stripped from 'entire'.
   *
   * @return 'entire' without 'sub', or null if 'sub' is
   *         not in 'entire'.
   */
  public static String strip(String entire, String sub)
  {
    int first, last;
    String stripped = null;

    if (sub != null && (first = entire.lastIndexOf(sub)) >= 0) {
      last = first + sub.length();
      stripped = entire.substring(0,first) + entire.substring(last);
    }

    return stripped;
  }


  // Private methods

  /**
   * Reads from the specified file and stores each line from
   * that file in a Vector.
   *
   * <p>
   * NOTE: Because there is identical functionality shared
   *	     between this method and 'readNoComments', it
   *	     would be nice to combine them or to share
   *	     another private method.
   *
   * @param reader A BufferedReader attached to the
   *        file to read from.
   *
   * @return A Vector of Strings which are the lines from the
   *         file.
   */
  private Vector read( BufferedReader reader )
    throws IOException
  {
    Vector storage = new Vector();
    char temp;
    int check;
    String line = new String();

    while ((check = reader.read()) >= 0) {
      temp = (char)check;

      // Have reached the end of a line.
      // Put everything aside from the line
      // feed (UNIX) or carriage return followed a
      // line feed (MS Windows) into the Vector.  (A List,
      // which will eventually be diplaying this String,
      // does not expect a line feed or a carriage
      // return followed by a line feed at the end of a line.)
      // NOTE: 10 is LF, 13 is CR.
      if (check == 10 || check == 13) {
        // On MS systems, must exclude a
        // carriage return, and the subsequent
        // line feed.
        if ( check == 13 && (check = reader.read()) < 0 ) break;

        if( line.trim().length() > 0 ) storage.add(line);

        line = new String();
      }

      // Otherwise, concatenate the read character.
      else {
        // If it's a tab, put in spaces
        // as not all systems display tabs properly.
        if (check == 9) line += CodeReader.TAB;
        else line += temp;
      }
    }

    return storage;
  }

  /**
   * Reads from the specified file and stores each line from
   * that file in a Vector.  All comments (delimited by a
   * forward slash and an asterisk and the reverse of those
   * characters) are stripped out.
   *
   * @param reader A BufferedReader attached to the
   *        file to read from.
   *
   * @return A Vector of Strings which are the lines from the
   *         file.
   */
  private Vector readNoComments( BufferedReader reader )
    throws IOException
  {
    Vector storage = new Vector();
    boolean inComments = false;
    char temp;
    int check;
    String line = new String();

    // Stores lines in Vector, aside from comments.
    // (comments preceded by a double-slash are left in.)
    while ((check = reader.read()) >= 0) {
      temp = (char)check;

      // If not already in a comment, and starting
      // upon a possible comments.
      if ( !inComments && temp == CodeReader.SLASH) {
        // Confirmed start of comment.
        if( ((check = reader.read()) >= 0) &&
            ((char)check) == CodeReader.ASTERISK)
            inComments = true;

        // Found EOF instead of '*'
        else if (check < 0)
          // Assumes EOF is the only character on last line of file.
          break;

        // It is not a comment, so tack the
        // characters onto the String.
        else {
          line += String.valueOf(temp);
          line += String.valueOf((char)check);
        }
      }

      // If in a comment, and have come upon a
      // potential end of the current comment.
      else if( inComments && temp == CodeReader.ASTERISK) {
        // End of comment.
        if ( ((check = reader.read()) >= 0) &&
             ((char)check) == CodeReader.SLASH)
          inComments = false;
          // EOF instead.
        else if (check < 0) break;
      }

      // Not in a comment, and have reached the end of
      // a line.  Put everything aside from the line
      // feed (UNIX) or carriage return followed a
      // line feed (MS Windows) into the Vector.  (A List,
      // which will eventually be diplaying this String,
      // does not expect a line feed or a carriage
      // return followed by a line feed at the end of a line.)
      // NOTE: 10 is LF, 13 is CR.
      else if (!inComments && (check == 10 || check == 13)) {
        // On MS systems, must exclude a
        // carriage return, and the subsequent line feed.
        if ( check == 13 && (check = reader.read()) < 0) break;

        if( line.trim().length() > 0 ) storage.add(line);
        line = new String();
      }

      // otherwise, concatenate the read character.
      else if (!inComments) {
        // If it's a tab, put in spaces
        // as not all systems display tabs properly.
        if (temp == 9) line += CodeReader.TAB;
        else line += temp;
      }
    }

    return storage;
  }
}








