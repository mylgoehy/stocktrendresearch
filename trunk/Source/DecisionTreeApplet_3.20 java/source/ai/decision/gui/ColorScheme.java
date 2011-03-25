package ai.decision.gui;

import java.awt.*;

/**
 * The ColorScheme class contains a single static method
 * that returns an instance of <code>java.awt.Color</code>,
 * mapped to a specific numerical ratio (between 0 and 1).
 *
 * <p>
 * The mapping is defined internally within the class.
 *
 * <p>
 * <b>Change History:</b>
 *
 * <p><pre>
 * Name:            Date:            Change:
 * =============================================================
 * J. Kelly         Nov-14-2000      Created.
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
public class ColorScheme
{
  // Class data members

  /**
   * The default color for a node that no examples reach.
   */
  private static final Color DEFAULT_COLOR = Color.white;

  private static final float SATURATION = 0.7f;

  private static final float BRIGHTNESS = 1.0f;

  private static final float START_HUE = 0.0f;       // Pure red

  private static final float END_HUE   = 2.0f/3.0f;  // Pure blue

  // Public methods

  /**
   * Return a color based on the ratio value.  The
   * HSB color space between red (0 degrees) and blue
   * (240 degrees) is mapped linearly to the ratio interval [0,1].
   *
   * <p>
   * If a ratio less than zero is supplied, the method returns
   * a default color (white).
   */
  public static Color getColor( double ratio )
  {
    // If the ratio is negative, return the default color.
    if( ratio < 0.0 )
      return DEFAULT_COLOR;

    float s = 0.7f;
    float b = 1.0f;

    float h =
      END_HUE - END_HUE * (float)ratio + START_HUE * (float)ratio;

    return Color.getHSBColor( h, s, b );
  }
}