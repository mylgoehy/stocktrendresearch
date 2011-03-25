package ai.decision.algorithm;

import java.util.*;

/**
 * This class provides a representation of one attribute in
 * a decision tree dataset.
 *
 * <p>
 * There are two types of attributes, target attributes and
 * general attributes.  Both types are respresented by the
 * Attribute class.
 *
 * <p>
 * An Attribute objects stores an attribute name and a list of
 * the values that the given attribute takes on.  Additionally,
 * each attribute can store statistics about examples in the
 * training dataset.  Stats are stored in a 2-D array, where the
 * first dimension corresponds to the number of values for the
 * attribute, and the second dimension corresponds to the number
 * of possible values for the target attribute.  Using this
 * format, the array can be populated with information used
 * to 'split' the dataset.
 *
 * <p>
 * <b>Change History:</b>
 *
 * <p><pre>
 * Name:            Date:            Change:
 * =============================================================
 * J. Kelly         May-03-2000      Created.
 * J. Kelly         May-18-2000      Added support for statistics
 *                                   within the Attribute class.
 * J. Kelly         Aug-23-2000      Added utility method to clear
 *                                   stats array.
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
public class Attribute
{
  // Instance data members

  String m_name;      // Name of this attribute.
  Vector m_values;    // Vector containing the name of each
                      // attribute value.
  int[][] m_stats;    // 2-D array of stats for this
                      // attribute - this can be filled in
                      // as needed.

  // Constructors

  /**
   * Initializes the attribute with the supplied name and
   * list of values.
   *
   * <p>
   * The method also builds a new internal statistics array.
   *
   * @param attributeName The name of this attribute.
   *
   * @param values A Vector that contains the names of the
   *        values for this attribute.
   *
   * @param numTargetValues The number of target attribute
   *        values in the dataset.  This determines the
   *        dimensions of the statistics array.  If this
   *        attribute is the target attribute, the value
   *        should be set to 1.
   *
   * @throws NullPointerException If the supplied name
   *         is null or the values Vector is null.
   *
   * @throws IllegalArgumentException If the number of
   *         attribute values in the vector is 0, or the
   *         number of target attribute values is less than 1.
   */
  public Attribute( String attributeName, Vector values, int numTargetValues )
  {
    if( attributeName == null || values == null )
      throw new
        NullPointerException( "Attribute name or values is null." );

    if( values.size() == 0 )
      throw new
        IllegalArgumentException( "Vector of attribute values is empty." );

    if( numTargetValues < 1 )
      throw new
        IllegalArgumentException( "Number of target " +
          "attribute values specified is less than 1." );

    m_name   = attributeName;
    m_values = values;
    m_stats = new int[ values.size() ][ numTargetValues ];
  }

  // Public methods

  /**
   * Returns the number of possible values for this attribute.
   *
   * @return The size of the set of values available for
   *         this attribute.
   */
  public int getNumValues()
  {
    return m_values.size();
  }

  /**
   * Returns the name of this attribute.
   *
   * @return The name of this attribute.
   */
  public String getName()
  {
    return m_name;
  }

  /**
   * Finds and returns the position of a particular
   * attribute value in the internal storage Vector.
   *
   * @return The position of the attribute value in the attribute's
   *         internal storage Vector.
   *
   * @throws NonexistentAttributeValueException if a value with
   *         the supplied name does not exist.
   */
  public int getAttributeValuePosition( String valName )
    throws NonexistentAttributeValueException
  {
    // Inefficient linear search of the vector
    for( int i = 0; i < m_values.size(); i++ ) {
      String val = (String)m_values.elementAt(i);

      if( val.equals( valName ) ) return i;
    }

    throw new
      NonexistentAttributeValueException( "Attribute" +
        " value " + this.m_name + "." + valName + " does not exist." );
  }

  /**
   * Returns the name of a particular value in this attribute's
   * internal storage Vector.
   *
   * @return The name of the value located at the specified position
   *         in the internal storage Vector.
   *
   * @throws NonexistentAttributeValueException If no value exists
   *         at the specified position.
   */
  public String getAttributeValueByNum( int valNum )
    throws NonexistentAttributeValueException
  {
    if( valNum < 0 || valNum >= m_values.size() )
      throw new
        NonexistentAttributeValueException( "Attribute" +
          " value at location " + valNum + " does not exist." );

    return (String)m_values.elementAt( valNum );
  }

  /**
   * Creates and returns a vector with the names of all
   * values for this attribute.
   *
   * @return A vector containing Strings which are the
   *         names of all the attribute values (in the order
   *         that they were added to the Attribute object).
   */
  public Vector getValueNames()
  {
    // Return the vector of names.
    return new Vector( m_values );
  }

  /**
   * Provides direct access to the internal statistics
   * array.  This avoids the overhead of method calls,
   * but assumes that the caller understands the structure
   * of the array and can manipulate it accordingly.
   *
   * @return The Attribute's 2-D statistics array.
   */
  public int[][] getStatsArray()
  {
    return m_stats;
  }

  /**
   * A utility method that clears the internal statistics
   * array.
   */
  public void clearStatsArray()
  {
    for( int i = 0; i < m_stats.length; i++ )
      for( int j = 0; j < m_stats[i].length; j++ )
        m_stats[i][j] = 0;
  }
}


