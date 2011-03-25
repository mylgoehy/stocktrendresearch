package ai.common;

import javax.swing.*;
import javax.swing.border.*;

/**
 * Creates a panel that contains a text label in the
 * upper left-hand corner and an etched border.
 *
 * <p>
 * <b>Change History:</b>
 *
 * <p><pre>
 * Name:            Date:            Change:
 * =============================================================
 * J. Kelly         Oct-02-2000      Created.
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
public class TitledPanel
    extends JPanel
{
  /**
   * Builds a new TitledPanel.  The panel has an
   * etched border.
   *
   * @param title The title that will appear in the
   *        upper left-hand corner of the panel.
   */
  public TitledPanel( String title )
  {
    // Set the border for the panel.
    setBorder( BorderFactory.createTitledBorder(
      BorderFactory.createEtchedBorder(), title ) );
  }
}

