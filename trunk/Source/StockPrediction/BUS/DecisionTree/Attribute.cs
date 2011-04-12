using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecisionTree
{
    public class Attribute
    {
        #region Attributes
        private String _name;      // Name of this attribute.
        private String[] _values;  // Vector containing the name of each
        // attribute value.
        private int[][] _stats;    // 2-D array of stats for this
        // attribute - this can be filled in
        // as needed.
        #endregion

        #region Properties
        public String Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public String[] Values
        {
            get { return _values; }
            set { _values = value; }
        }
        public int[][] Stats
        {
            get { return _stats; }
            set { _stats = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes the attribute with the supplied name and list of values.
        /// </summary>
        /// <param name="attributeName">
        /// The name of this attribute.
        /// </param>
        /// <param name="values">
        /// Vector that contains the names of the values for this attribute.
        /// </param>
        /// <param name="numTargetValues">
        /// numTargetValues The number of target attribute
        /// values in the dataset.  This determines the
        /// dimensions of the statistics array.  If this
        /// attribute is the target attribute, the value
        /// should be set to 1.
        /// </param>
        public Attribute(String attributeName, String[] values, int numTargetValues)
        {
            Name = attributeName;
            Values = values;
            Stats = new int[values.Length][];
            for (int i = 0; i < values.Length; i++)
            {
                Stats[i] = new int[numTargetValues];
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Returns the number of possible values for this attribute.
        /// </summary>
        /// <returns>
        /// The size of the set of values available for this attribute.
        /// </returns>
        public int getNumValues()
        {
            return Values.Length;
        }
        /// <summary>
        ///   Finds and returns the position of a particular
        ///   attribute value in the internal storage Vector.
        /// </summary>
        /// <param name="valName">Name of Attiribute</param>
        /// <returns>
        /// The position of the attribute value in the attribute's
        /// internal storage Vector.
        /// </returns>
        public int getAttributeValuePosition(String valName)
        {
            // Inefficient linear search of the vector
            for (int i = 0; i < Values.Length; i++)
            {
                String val = (String)Values[i];

                if (val.CompareTo(valName) == 0) return i;
            }
            return -1;
        }       
        /**
         * Returns the name of this attribute.
         *
         * @return The name of this attribute.
         */        
        
        
        public String getName()
        {
            return Name;
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
            return Stats;
        }
        /**
         * Creates and returns a vector with the names of all
         * values for this attribute.
         *
         * @return A vector containing Strings which are the
         *         names of all the attribute values (in the order
         *         that they were added to the Attribute object).
         */

        public String[] getValueNames()
        {
            // Return the vector of names.
            return Values;
        }
        /// <summary>
        /// Returns the name of a particular value in this attribute's
        /// internal storage Vector.
        /// </summary>
        /// <param name="valNum">index of attribute</param>
        /// <returns>
        /// The name of the value located at the specified position
        /// in the internal storage Vector.
        /// </returns>
        public string getAttributeValueByNum(int valNum)
        {
            if (valNum < 0 || valNum >= Values.Length)
            {
                return null;
            }
            return (String)Values[valNum];
        }
        /// <summary>
        /// Clear Stats by Reset to Zero
        /// </summary>
        public void clearStatsArray()
        {
            for (int i = 0; i < Stats.Length; i++)
                for (int j = 0; j < Stats[i].Length; j++)
                    Stats[i][j] = 0;
        }
        #endregion
    }
}
