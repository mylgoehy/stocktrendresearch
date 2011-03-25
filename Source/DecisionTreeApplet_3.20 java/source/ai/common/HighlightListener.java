package ai.common;

/**
 * The listener interface the receives &quot;highlight&quot;
 * events.  Classes that are interested in processing these
 * events should implement this interface.
 *
 * <p>
 * The HighlighterListerner interface provides a common way for
 * an application to highlight lines of pseudo-code or
 * indicate that a series of steps are being performed
 * a in sequence.
 *
 * <p>
 * <b>Change History:</b>
 *
 * <p><pre>
 * Name:            Date:            Change:
 * =============================================================
 * J. Kelly         Jun-04-2000      Created.
 * J. Kelly         Sep-26-2000      Name revision.
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
public interface HighlightListener
{
  /**
   * Displays (or prepares to display) a particular
   * function.
   *
   * <p>
   * The caller is expected to be aware of the contents
   * of the function (i.e. the caller should know what
   * it's doing).
   *
   * @param name The name of the function to display.
   */
  public void displayFunction( String name );

  /**
   * Displays (or prepares to display) a particular
   * function.
   *
   * <p>
   * The caller is expected to be aware of the contents
   * of the function (i.e. the caller should know what
   * it's doing).
   *
   * @param index The index of the function, which depends
   *        on the order in which the functions are stored.
   */
  public void displayFunction( int index );


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
  public void
    setDynamicText( String functionName, String dynamicName, String text );

  /**
   * Informs the receiving object that it should
   * display or hightlight a line of pseudo-code
   * or some other marker item.  The actual highlighting
   * action depends upon the object that implements
   * this interface.
   *
   * <p>
   * The caller is expected to be aware of the contents
   * of the highlighted line/item (i.e. the caller
   * should know what it's doing).
   */
  public void notifyHighlight( int lineOrItem );

  /**
   * Informs the receiving object that any current highlights
   * should be cleared.  This method is available primarily to
   * allow GUI-based components to clear themselves.
   */
  public void notifyClearHighlight();
}
