package ai.decision.gui;

import java.awt.*;
import javax.swing.*;
import java.util.*;
import ai.decision.algorithm.*;

/**
 * The AttributeMenuItem class is a small utility class that
 * holds a reference to a particular attribute and
 * associated splitting result.  The class contains methods
 * that correctly format this information for display
 * in a popup menu.
 *
 * <p>
 * <b>Change History:</b>
 *
 * <p><pre>
 * Name:            Date:            Change:
 * =============================================================
 * J. Kelly         Jul-5-2000       Created.
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
public class AttributeMenuItem
  extends JMenuItem
{
  // Instance data members

  Attribute m_attribute;  // A reference to an attribute.

  // Constructors

  /**
   * Builds a new AttributeMenuItem object.  The menu item
   * text contains the attribute name, a hyphen, the current
   * splitting function, a colon, and the corresponding
   * attribute score (to four decimal places). If no score
   * value is available, only the attribute name is displayed.
   *
   * @param attribute The attribute that this menu item
   *        represents.
   *
   * @param splitFun The current splitting function.  If this
   *        value is null, no splitting function text is
   *        included.
   *
   * @param score The corresponding attribute score (based
   *        on the current splitting function).
   */
  public
    AttributeMenuItem( Attribute attribute, String splitFun, Double score )
  {
    super();

    setFont( getFont().deriveFont( Font.PLAIN, 10 ) );

    m_attribute = attribute;

    // Build the text label.
    StringBuffer text = new StringBuffer( attribute.getName() );

    if( score != null ) {
      text.append( " - " );

      if( splitFun != null ) text.append( splitFun + ": " );
      String scoreStr = score.toString();

      // Four decimal places.
      int endIndex = scoreStr.length() < 6 ? scoreStr.length() - 1 : 5;
      text.append( scoreStr.substring( 0, endIndex ) );
    }

    setText( text.toString() );
  }

  /**
   * Returns a reference to the attribute that this menu item
   * stores.
   *
   * @return A reference to the attribute whose label is
   *         displayed as part of the menu item text.
   */
  public Attribute getAttribute()
  {
    return m_attribute;
  }
}
