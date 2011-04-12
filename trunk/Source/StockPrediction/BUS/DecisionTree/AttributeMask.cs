using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecisionTree
{
    public class AttributeMask
    {
        public static int UNUSED = -1;

        #region Attributes
        private int[] _mask;    // Mask - holds a value index if the
        // attribute has been used along the
        // path to a node, UNUSED otherwise.
        #endregion

        #region Properties
        public int[] Mask
        {
            get { return _mask; }
            set { _mask = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new attribute mask, with all attributes
        /// initially marked as unused.
        /// </summary>
        /// <param name="numAttributes">
        /// The number of attributes
        /// in the current dataset (including the target attribute).
        /// </param>
        public AttributeMask(int numAttributes)
        {
            if (numAttributes < 1)
                throw new Exception("Cannot create a mask with less than one attribute.");

            // Create a new mask array with nothing masked
            // off initially.
            Mask = new int[numAttributes];

            for (int i = 0; i < Mask.Length; i++)
            {
                Mask[i] = UNUSED;
            }
        }
        /// <summary>
        /// Builds a new mask, copying the state of the
        /// supplied mask to the new mask.
        /// </summary>
        /// <param name="mask">An Attribute mask</param>
        public AttributeMask(AttributeMask mask)
        {
            Mask = (int[])mask.Mask.Clone();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Returns the number of attributes in the mask.  This
        /// value should be identical to the number of attributes
        /// in the dataset.
        /// </summary>
        /// <returns>
        /// The number of attributes in the mask (including
        /// the target attribute).
        /// </returns>
        public int getNumAttributes()
        {
            return Mask.Length;
        }
        /// <summary>
        /// Returns the number of UNUSED attributes in the mask,
        /// excluding the target attribute.
        /// </summary>
        /// <returns>
        /// The number of UNUSED attributes that remain
        /// in the mask, excluding the target attribute.
        /// </returns>
        public int getNumUnusedAttributes()
        {
            int numUnused = 0;

            for (int i = 1; i < Mask.Length; i++)
            {
                if (Mask[i] == UNUSED)
                {
                    numUnused++;
                }
            }
            return numUnused;
        }
        /// <summary>
        /// Masks off the attribute at the specified position,
        /// recording the attribute value index (path information)
        /// The method does not check to see if the position is
        /// already masked off.
        /// </summary>
        /// <param name="attIndex">
        ///  The index of a particular
        ///  attribute in the internal storage array.  This
        ///  position corresponds to the position of the
        ///  attribute in the current dataset (based on
        ///  the data metafile).
        /// </param>
        /// <param name="valueIndex">
        /// The position of an attribute
        /// value in an Attribute's internal storage
        /// list.  Indices are used instead of Strings
        /// to conserve memory.
        /// </param>
        public void mask(int attIndex, int valueIndex)
        {
            if (attIndex < 0 || attIndex > (Mask.Length - 1))
                throw new Exception("Attribute at position " + attIndex.ToString() + " does not exist.");

            if (valueIndex < 0)
                throw new Exception("Value index must be >= 0.");

            Mask[attIndex] = valueIndex;
        }
        /// <summary>
        /// Unmasks a particular attribute.  The method does
        /// not check to see if the attribute is already unmasked.
        /// </summary>
        /// <param name="attIndex">
        /// The index of a particular
        /// attribute in the internal storage array.
        /// </param>
        public void unmask(int attIndex)
        {
            if (attIndex < 0 || attIndex > (Mask.Length - 1))
            {
                throw new Exception("Attribute at position " + attIndex.ToString() + " does not exist.");
            }

            Mask[attIndex] = UNUSED;
        }
        /// <summary>
        /// Returns the value (as a number) for supplied attribute position
        /// if the attribute is already masked off, or UNUSED otherwise.
        /// </summary>
        /// <param name="attIndex"> index of attribute </param>
        /// <returns>
        /// A value if the attribute at the
        /// supplied position is already masked off (has
        /// already been split on along the path to the
        /// current node), or UNUSED otherwise.
        /// </returns>
        public int isMasked(int attIndex)
        {
            if (attIndex < 0 || attIndex > (Mask.Length - 1))
                throw new Exception("Attribute at position " + attIndex + " does not exist.");
            return Mask[attIndex];
        }
        /// <summary>
        /// Determines if the supplied example from the dataset
        /// matches the current mask.  This means that the
        /// attribute values in the example match the attribute
        /// values in the <b>masked</b> portions of the
        /// mask - or equivalently that the example follows
        /// a specific path through the decision tree (as defined
        /// by the mask).
        /// </summary>
        /// <param name="example">An example from the current dataset.</param>
        /// <returns>true if the example matches the mask, or
        /// false otherwise.
        /// </returns>
        public bool matchMask(int[] example)
        {
            if (example.Length != Mask.Length)
                throw new Exception("Number of attributes in example does not match number of attributes in mask.");

            bool isAMatch = true;

            for (int i = 0; i < example.Length; i++)
            {
                if (Mask[i] != UNUSED && Mask[i] != example[i])
                {
                    // No match.
                    isAMatch = false;
                    break;
                }
            }

            return isAMatch;
        }
        #endregion
    }
}
