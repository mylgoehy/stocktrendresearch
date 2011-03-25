package ai.decision.algorithm;

import java.util.*;

/**
 * An AttributeMask maintains a record of the attributes
 * available for splitting at a given node in a decsion tree.
 *
 * <p>
 * An internal list records which attributes (and attribute values)
 * have been used (split on) along the path to a given node.
 *
 * <p>
 * The length of the mask list is the same as the number of
 * attributes in the current dataset, including the target
 * attribute.  Attributes are indexed in the mask according
 * to their order in the original dataset.
 *
 * <p>
 * Each position in the mask contains one of the following
 * values:
 *
 * <p>
 * <ul>
 *     <li><code>UNUSED</code> if the attribute is not used
 *         along the path to the current node.
 *     <li><code>0...n</code> if the attribute is used along
 *         the path to the current node.  The integer value
 *         corresponds to an arc index, which indicates the
 *         attribute value that lies on this path through the
 *         tree.
 * </ul>
 *
 * <p>
 * The target attribute <b>always</b> maps to position 0 in the
 * mask.
 *
 * <p>
 * <b>Change History:</b>
 *
 * <p><pre>
 * Name:            Date:            Change:
 * =============================================================
 * J. Kelly         May-10-2000      Created.
 * J. Kelly         Jun-08-2000      Removed redundant USED
 *                                   designation in mask.
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
public class AttributeMask
{
  // Class data members

  /**
   * Indicates that a particular attribute has not been
   * split on.
   */
  public static final int UNUSED = -1;

  // Instance data members

  int[] m_mask;  // Mask - holds a value index if the
                 // attribute has been used along the
                 // path to a node, UNUSED otherwise.

  // Constructors

  /**
   * Creates a new attribute mask, with all attributes
   * initially marked as unused.
   *
   * @param numAttributes The number of attributes
   *        in the current dataset (including the
   *        target attribute).
   *
   * @throws IllegalArgumentException If the supplied
   *         number of attributes is less than one.
   */
  public AttributeMask( int numAttributes )
  {
    if( numAttributes < 1 )
      throw new
        IllegalArgumentException( "Cannot create " +
          "a mask with less than one attribute." );

    // Create a new mask array with nothing masked
    // off initially.
    m_mask = new int[ numAttributes ];

    for( int i = 0; i < m_mask.length; i++ ) m_mask[i] = UNUSED;
  }

  /**
   * Builds a new mask, copying the state of the
   * supplied mask to the new mask.
   */
  public AttributeMask( AttributeMask mask )
  {
    m_mask = (int[])mask.m_mask.clone();
  }

  // Public methods

  /**
   * Returns the number of attributes in the mask.  This
   * value should be identical to the number of attributes
   * in the dataset.
   *
   * @return The number of attributes in the mask (including
   *         the target attribute).
   */
  public int getNumAttributes()
  {
    return m_mask.length;
  }

  /**
   * Returns the number of UNUSED attributes in the mask,
   * excluding the target attribute.
   *
   * @return The number of UNUSED attributes that remain
   *         in the mask, excluding the target attribute.
   */
  public int getNumUnusedAttributes()
  {
    int numUnused = 0;

    for( int i = 1; i < m_mask.length; i++ )
      if( m_mask[i] == UNUSED ) numUnused++;

    return numUnused;
  }

  /**
   * Masks off the attribute at the specified position,
   * recording the attribute value index (path information)
   * The method does not check to see if the position is
   * already masked off.
   *
   * @param attIndex The index of a particular
   *        attribute in the internal storage array.  This
   *        position corresponds to the position of the
   *        attribute in the current dataset (based on
   *        the data metafile).
   *
   * @param valueIndex The position of an attribute
   *        value in an Attribute's internal storage
   *        list.  Indices are used instead of Strings
   *        to conserve memory.
   *
   * @throws IndexOutOfBoundsException If the supplied
   *         attribute position is less than zero,
   *         or larger than the number of attributes in
   *         the dataset.
   *
   * @throws IllegalArgumentException If the supplied
   *         value is less than zero.
   */
  public void mask( int attIndex, int valueIndex )
  {
    if( attIndex < 0 || attIndex > (m_mask.length - 1) )
      throw new
        ArrayIndexOutOfBoundsException( "Attribute " +
          "at position " + attIndex + " does not exist." );

    if( valueIndex < 0 )
      throw new
        IllegalArgumentException( "Value index must be >= 0." );

    m_mask[ attIndex ] = valueIndex;
  }

  /**
   * Unmasks a particular attribute.  The method does
   * not check to see if the attribute is already unmasked.
   *
   * @param attIndex The index of a particular
   *        attribute in the internal storage array.
   */
  public void unmask( int attIndex )
  {
    if( attIndex < 0 || attIndex > (m_mask.length - 1) )
      throw new
        ArrayIndexOutOfBoundsException( "Attribute " +
          "at position " + attIndex + " does not exist." );

    m_mask[ attIndex ] = UNUSED;
  }

  /**
   * Returns the value (as a number) for supplied attribute position
   * if the attribute is already masked off, or UNUSED otherwise.
   *
   * @return A value if the attribute at the
   *         supplied position is already masked off (has
   *         already been split on along the path to the
   *         current node), or UNUSED otherwise.
   *
   * @throws IndexOutOfBoundsException If the supplied
   *         attribute position is less than zero,
   *         or larger than the number of attributes in
   *         the dataset.
   */
  public int isMasked( int attIndex )
  {
    if( attIndex < 0 || attIndex > (m_mask.length - 1) )
      throw new
        ArrayIndexOutOfBoundsException( "Attribute " +
          "at position " + attIndex + " does not exist." );

    return m_mask[ attIndex ];
  }

  /**
   * Determines if the supplied example from the dataset
   * matches the current mask.  This means that the
   * attribute values in the example match the attribute
   * values in the <b>masked</b> portions of the
   * mask - or equivalently that the example follows
   * a specific path through the decision tree (as defined
   * by the mask).
   *
   * <p>
   * The method assumes that the target attribute value
   * is stored at position 0 of the supplied integer
   * array, and that the attribute order in the array
   * is the same as the attribute order in the mask.
   *
   * @param example An example from the current dataset.
   *
   * @return true if the example matches the mask, or
   *         false otherwise.
   *
   * @throws IllegalArgumentException If the number of
   *         attributes in the example (the example
   *         length) does not match the length of the
   *         attribute mask.
   */
  public boolean matchMask( int[] example )
  {
    if( example.length != m_mask.length )
      throw new
        IllegalArgumentException( "Number of " +
          "attributes in example does not match " +
          "number of attributes in mask." );

    boolean isAMatch = true;

    for( int i = 0; i < example.length; i++ ) {
      if( m_mask[i] != UNUSED && m_mask[i] != example[i] ) {
        // No match.
        isAMatch = false;
        break;
      }
    }

    return isAMatch;
  }
}
