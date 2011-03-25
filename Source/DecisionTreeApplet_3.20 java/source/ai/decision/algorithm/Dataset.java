package ai.decision.algorithm;

import java.io.*;
import java.util.*;
import java.net.*;

/**
 * The Dataset class encapsulates a dataset used to build a decision tree.
 * A Dataset object can be queried for attribute information (used during the
 * splitting process).
 *
 * <p>
 * The class manages and provides access to:
 *
 * <p>
 * <ul>
 *     <li>Raw data tuples (for training and testing).
 *     <li>Target attribute information:
 *     <ul>
 *         <li>The name of the target attribute for this dataset.
 *         <li>Possible values for the target attribute.
 *     </ul>
 *     <li>General attribute information:
 *     <ul>
 *         <li>The name of each attribute.
 *         <li>Possible values for each attribute.
 *     </ul>
 * </ul>
 *
 * <p>
 * Data tuples are stored internally as integer arrays.  Each
 * array cell contains an integer that corresponds to a particular
 * attribute value index (as defined by the order of attributes
 * and attribute values in the metadata file).  This is a
 * reasonably efficient way to store large datasets.
 *
 * <p>
 * Once a dataset has been loaded, it is immutable - no new
 * examples can be stored.  To extend or change a dataset, a
 * new Dataset object must be created.
 *
 * <p>
 * <b>Change History:</b>
 *
 * <p><pre>
 * Name:            Date:            Change:
 * =============================================================
 * J. Kelly         May-03-2000      Created.
 * J. Kelly         May-12-2000      Moved attribute classes
 *                                   (now external).
 * J. Kelly         Sep-27-2000      Added support for
 *                                   testing set.
 * J. Kelly         Feb-06-2001      Added ability to create
 *                                   random testing sets.
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
public class Dataset
{
  // Debug data members

  boolean DEBUG_ON = true;   // Turn on/off debugging info.

  // Instance data members

  Vector m_attributes;       // Attributes for data set.
  Vector m_trainingSet;      // Training data storage.
  Vector m_testingSet;       // Testing data storage.
  int[]  m_targetSums;       // Number of examples in each
                             // target attribute class.

  // Constructors

  /**
   * Builds a new Dataset.  Creates a FileParser
   * to parse the metadata and example files for
   * the dataset.
   *
   * <p>
   * This constructor retrieves the data from the specified
   * URL.
   *
   * @param repository The location of the metadata and
   *        example files.
   *
   * @param metaFile The name of the metadata file -
   *        the constructor attempts to open the effective
   *        URL: repository + metaFile for reading.
   *
   * @throws MalformedURLException if the URL protocol is
   *         unrecognized.
   *
   * @throws InvalidMetaFileException If the
   *         metadata file contains syntax errors.
   *
   * @throws InvalidDataFileException If the data
   *         (example) file contains syntax errors.
   *
   * @throws IOException If the configuration or data file
   *         cannot be read.
   */
  public Dataset( URL repository, String metaFile )
    throws MalformedURLException,
           InvalidMetaFileException,
           InvalidDataFileException,
           IOException
  {
    m_attributes  = new Vector();
    m_trainingSet = new Vector();
    m_testingSet  = new Vector();

    // Build the URL for the meta file.
    URL metaFileURL =
      new URL( repository + metaFile );

    // Attempt to open an input stream attached to the file.
    InputStream metaInputStream = metaFileURL.openStream();

    // First, we parse the metadata file
    // and grab the various attribute names etc.
    String dataFile = parseMetaFile( metaInputStream );

    // The data file location specified in the meta file
    // is relative to the meta file location.
    String fileRootPath = new String();
    int pos;

    if( (pos = metaFile.lastIndexOf( '/' )) != -1 )
      fileRootPath = metaFile.substring( 0, pos + 1 );

    // Build the URL for the data file.
    URL dataFileURL =
      new URL( repository + fileRootPath + dataFile );

    InputStream dataInputStream = dataFileURL.openStream();

    // Initialize the target counts array.
    m_targetSums = new int[ getTargetAttribute().getNumValues() ];

    // Now, we parse the data file and store the data in memory.
    parseDataFile( dataInputStream );
  }

  /**
   * Builds a new Dataset.  Creates a FileParser to parse the metadata
   * and example files for the dataset.
   *
   * @param metaFile A file containing the metadata
   *        for this dataset and a pointer to the actual
   *        example file.
   *
   * @throws InvalidMetaFileException If the
   *         metadata file contains syntax errors.
   *
   * @throws InvalidDataFileException If the data
   *         (example) file contains syntax errors.
   *
   * @throws IOException If the configuration or data file
   *         cannot be read.
   */
  public Dataset( String metaFile )
    throws InvalidMetaFileException,
           InvalidDataFileException,
           IOException
  {
    m_attributes  = new Vector();
    m_trainingSet = new Vector();
    m_testingSet  = new Vector();

    // First, we parse the configuration file and grab the various
    // attribute names etc.
    FileInputStream metaInputStream =
      new FileInputStream( metaFile );

    String dataFile = parseMetaFile( metaInputStream );

    // The data file location specified in the meta file
    // is relative to the meta file location.
    String fileRootPath = new String();
    int pos;

    if( (pos = metaFile.lastIndexOf( '/' )) != -1 )
      fileRootPath = metaFile.substring( 0, pos + 1 );

    // Now, we parse the data file and store the data in memory.
    FileInputStream dataInputStream =
      new FileInputStream( fileRootPath + dataFile );

    // Initialize the target counts array.
    m_targetSums = new int[ getTargetAttribute().getNumValues() ];

    parseDataFile( dataInputStream );
  }

  // Public methods

  /**
   * Returns the target attribute for this data set.
   *
   * @return An Attribute object for the target attribute
   *         in this dataset.
   */
  public Attribute getTargetAttribute()
  {
    // The target attribute is always stored at
    // position 0 in the attributes vector.
    return (Attribute)m_attributes.elementAt( 0 );
  }

  /**
   * Finds and returns a particular attribute.
   *
   * @return An Attribute object with the specified
   *         name.
   *
   * @throws NonexistentAttributeException If
   *         the attribute does not exist in the dataset.
   */
  public Attribute getAttributeByName( String attName )
    throws NonexistentAttributeException
  {
    // Inefficient linear search of the vector
    for( int i = 0; i < m_attributes.size(); i++ ) {
      Attribute att = (Attribute)m_attributes.elementAt(i);

      if( att.getName().equals( attName ) ) return att;
    }

    throw new
      NonexistentAttributeException( "Attribute " + attName +
                                     " does not exist." );
  }

  /**
   * Finds and returns a particular attribute, using the attribute's
   * location in the internal attributes Vector.
   *
   * <p>
   * This method is primarily available to allow for iteration over all
   * attributes in the dataset.
   *
   * @return An Attribute object stored at the
   *         specified index in the attributes vector.
   *
   * @throws NonexistentAttributeException If
   *         the attribute does not exist in the dataset
   *         (i.e. the index is out of range).
   *
   */
  public Attribute getAttributeByNum( int attNum )
    throws NonexistentAttributeException
  {
    if( attNum < 0 || attNum >= m_attributes.size() )
      throw new
        NonexistentAttributeException( "Attribute" +
          " at location " + attNum + " does not exist." );

    return (Attribute)m_attributes.elementAt(attNum);
  }

  /**
   * Finds and returns the position of a particular attribute in the
   * Dataset's internal storage list.
   *
   * @param attName The name of the attribute to locate.
   *
   * @return The position of the attribute  in
   *         the Dataset's internal storage list.
   *
   * @throws NonexistentAttributeException If an
   *         attribute value with the supplied name
   *         does not exist.
   */
  public int getAttributePosition( String attName )
    throws NonexistentAttributeException
  {
    // Inefficient linear search of the vector
    for( int i = 0; i < m_attributes.size(); i++ ) {
      String name = ((Attribute)m_attributes.elementAt(i)).getName();
      if( name.equals( attName ) ) return i;
    }

    throw new
      NonexistentAttributeException( "Attribute" +
        " named " + attName + " does not exist." );
  }

  /**
   * Returns the number of attributes (including the
   * target attribute) in this dataset.
   *
   * @return The total number of attributes in the
   *         dataset, including the target attribute.
   */
  public int getNumAttributes()
  {
    return m_attributes.size();
  }

  /**
   * Returns the number of training examples <i>currently</i> in
   * this dataset.
   *
   * @return The total number of training examples in the dataset.
   */
  public int getNumTrainingExamples()
  {
    return m_trainingSet.size();
  }

  /**
   * Returns the number of testing examples currently
   * in this dataset.
   *
   * @return The total number of testing examples in
   *         the dataset.
   */
  public int getNumTestingExamples()
  {
    return m_testingSet.size();
  }

  /**
   * Creates and returns a Vector that contains the names of
   * all attributes in this data set (including the target
   * attribute).
   *
   * @return A Vector containing Strings which are the
   *         names of all the attributes (in the order
   *         that they were parsed from the metadata file -
   *         target attribute first).
   */
  public Vector getAttributeNames()
  {
    // Create and fill the vector of names
    Vector names = new Vector();

    for( int i = 0; i < m_attributes.size(); i++ )
      names.add( ((Attribute)m_attributes.elementAt(i)).getName() );

    return names;
  }

  /**
   * Returns an iterator over the training examples in the current dataset.
   *
   * @return An iterator over all the training examples.
   */
  public Iterator getTrainingExamples()
  {
    return m_trainingSet.iterator();
  }

  /**
   * Returns an iterator over the testing examples in the current dataset.
   *
   * @return An iterator over all the testing examples.
   */
  public Iterator getTestingExamples()
  {
    return m_testingSet.iterator();
  }

  /**
   * Moves an example from the training set to
   * the testing set.  If the supplied example number
   * is out of range no example is moved.
   *
   * @param exampleNum The example to transfer to the testing set.
   */
  public void moveToTestingSet( int exampleNum )
  {
    if( exampleNum < 0 || exampleNum > (m_trainingSet.size() - 1) )
      return;

    m_testingSet.add( m_trainingSet.remove( exampleNum ) );
  }

  /**
   * Moves an example from the training set to
   * the testing set.  If the supplied example number
   * is out of range no example is moved.
   *
   * @param exampleNum The example to transfer to the training set.
   */
  public void moveToTrainingSet( int exampleNum )
  {
    if( exampleNum < 0 || exampleNum > (m_testingSet.size() - 1) )
      return;

    m_trainingSet.add( m_testingSet.remove( exampleNum ) );
  }

  /**
   * Returns a particular training example from the examples in the
   * dataset.
   *
   * @return The selected example from the training
   *         dataset as an integer array.
   *
   * @throws IndexOutOfBoundsException If the example
   *         number is less than zero, or greater than
   *         the number of training examples in the dataset
   *         minus one.
   */
  public int[] getTrainingExample( int exampleNum )
  {
    if( exampleNum < 0 || exampleNum >= m_trainingSet.size() )
      throw new
        IndexOutOfBoundsException( "Example number " +
          exampleNum + " does not exist." );

    return (int[])m_trainingSet.elementAt( exampleNum );
  }

  /**
   * Returns a particular testing example from the examples in the
   * dataset.
   *
   * @return The selected example from the testing
   *         dataset as an integer array.
   *
   * @throws IndexOutOfBoundsException If the example
   *         number is less than zero, or greater than
   *         the number of testing examples in the
   *         dataset minus one.
   */
  public int[] getTestingExample( int exampleNum )
  {
    if( exampleNum < 0 || exampleNum >= m_testingSet.size() )
      throw new
        IndexOutOfBoundsException( "Example number " +
          exampleNum + " does not exist." );

    return (int[])m_testingSet.elementAt( exampleNum );
  }

  /**
   * Creates a random testing dataset.  Calling this
   * method will destroy any previously built testing set.
   *
   * @param percentage Percentage of the entire dataset to
   *        use for testing.
   *
   * @param balanced <code>true</code> to create a balanced
   *        testing set, where the testing set and the
   *        remaining training set have the same proportion
   *        of each class.
   *
   * @throws IllegalArgumentException If the percentage value
   *         is < 0 or > 100.
   */
  public void createRndTestSet( int percentage, boolean balanced )
  {
    if( percentage < 0 || percentage > 100 )
      throw new
        IllegalArgumentException( "Percentage value out of range." );

    // Move any examples that are part of the current testing
    // set back to the training set.
    m_trainingSet.addAll( m_testingSet );
    m_testingSet.clear();

    // Calculate the number of examples that should be
    // in the testing set.
    int totalNumExamples = m_trainingSet.size();
    int numTestingExamples =
      Math.round( totalNumExamples * ((float)percentage)/100.0f );
      Random rand = new Random();

    // If the set doesn't have to be balanced, then just
    // pick examples at random.
    if( !balanced ) {
      for( int i = 0; i < numTestingExamples; i++ ) {
        m_testingSet.add(
          m_trainingSet.remove( rand.nextInt( m_trainingSet.size() ) ) );
      }
    }
    else {
      // We have the target value distribution for the dataset,
      // so reference it.
      for( int i = 0; i < m_targetSums.length; i++ ) {
        int numExamplesToMove =
          Math.round( m_targetSums[i] / ((float)totalNumExamples)
                      * numTestingExamples );

        for( int j = 0; j < numExamplesToMove; j++ ) {
          // Attempt to randomly  pick examples from the
          // dataset that have the required target classification.
          int[] example = null;

          while( true ) {
            example = (int[])
              m_trainingSet.get( rand.nextInt( m_trainingSet.size() ) );

            if( example[0] == i ) break;
          }

          m_testingSet.add( m_trainingSet.remove(
                            m_trainingSet.indexOf( example ) ) );
        }
      }
    }
  }

  // Private methods

  /**
   * Parses a decision tree configuration file.
   *
   * @param  metaFileInputStream An input stream attached
   *         to the metadata file.
   *
   * @return A String containing the name of the ID3
   *         data file to open and read.
   *
   * @throws An InvalidMetaFileException if a
   *         syntax error is encountered during the
   *         parsing process.
   *
   * @throws An IOException if a problem occurs while
   *         reading the configuration file.
   */
  private String parseMetaFile( InputStream metaInputStream )
    throws InvalidMetaFileException,
           IOException
  {
    String buf; // temporary buffer

    FileParser parser = new FileParser();

    // 1. Open the metadata file and read in (ignoring comments).
    parser.startMetaParse( metaInputStream );

    // 2. Extract the target attribute name.
    parser.moveToTargetAttribute();

    String targetName   = new String( parser.extractAttributeName() );
    Vector targetValues = new Vector();
    int numTargetValues;

    // 3. Loop, extracting the possible values for the target attribute.
    while( (buf = parser.extractAttributeValue()) != null )
      targetValues.add( new String( buf ) );

    // Build the target attribute and add it to the attributes vector.
    Attribute targetAttribute = new Attribute( targetName, targetValues, 1 );

    numTargetValues = targetValues.size();
    m_attributes.add( targetAttribute );

    // 4. Get ready to extract general attribute information.
    parser.moveToAttributes();

    // 5. Loop, extracting each attribute one after the other.
    while( (buf = parser.extractAttributeName()) != null ) {
      String attName   = new String( buf );
      Vector attValues = new Vector();

      // 5.a. Loop, extracting the possible values for this
      //      attribute.
      while( (buf = parser.extractAttributeValue()) != null )
        attValues.add( new String( buf ) );

      Attribute nextAtt = new Attribute( attName, attValues, numTargetValues );
      m_attributes.add( nextAtt );
    }

    // 6. Extract the name of the data file.
    parser.moveToDataFilePath();

    String dataFilePath = parser.extractString();

    //------------------------- DEBUG -------------------------
    if( DEBUG_ON )
    {
      System.out.println( "Dataset::parseMetaFile: " +
        "Finished parsing configuration file." );
      System.out.println();

      // Target attribute information.
      System.out.println( "Target Attribute" );
      System.out.println( "------------------------------" );
      System.out.println();
      System.out.println( "Target Attribute Name:  " +
        ((Attribute)m_attributes.elementAt(0)).getName() );
      System.out.println( "Number of Values:       " +
        ((Attribute)m_attributes.elementAt(0)).getNumValues() );
      System.out.println( "Possible Values:        " +
        ((Attribute)m_attributes.elementAt(0)).getValueNames() );
      System.out.println();

      // General attribute information.
      System.out.println( "Attributes" );
      System.out.println( "------------------------------" );
      System.out.println();
      System.out.println( "Number of Attributes:   " +
        (getNumAttributes() - 1) );
      System.out.println();

      for( int i = 1; i < getNumAttributes(); i++ ) {
        Attribute currAtt = null;

        try {
          currAtt = getAttributeByNum( i );
        }
        catch( NonexistentAttributeException e ) {
          // can't happen
        }

        System.out.println( "Attribute " + i + " Name:       " +
          currAtt.getName() );
        System.out.println( "Number of Values:       " +
          currAtt.getNumValues() );
        System.out.println( "Possible Values:        " +
          currAtt.getValueNames() );
        System.out.println();
      }

      // Data file information.
      System.out.println( "Training/Testing Data" );
      System.out.println( "------------------------------" );
      System.out.println();
      System.out.println( "Data will be extracted from " + dataFilePath );
      System.out.println();
    }

    return dataFilePath;
  }

  /**
   * Parses a decision tree data file.
   *
   * @param  dataFileInputStream An input stream attached
   *         to the data file.
   *
   * @throws InvalidDataFileException If a
   *         syntax error is encountered during the
   *         parsing process.
   *
   * @throws IOException If a problem occurs while
   *         reading the data file.
   */
  private void parseDataFile( InputStream dataInputStream )
    throws InvalidDataFileException,
           IOException
  {
    Vector rawSample;  // Vector of strings extracted
                       // directly from the data file.

    FileParser parser = new FileParser();

    // 1. Open the data file.
    parser.startDataParse( dataInputStream );

    // 2. Start extracting samples.
    while( (rawSample = parser.extractDataSample()) != null ) {
      // We have the raw samples, so now we verify
      // that all the values are legal and store the
      // sample in compact form (using the integer index of
      // each value).
      if( rawSample.size() != m_attributes.size() )
        throw new
          InvalidDataFileException( "Syntax error in data file (line " +
            parser.getCurrentLineNum() + "): Wrong " +
            "number of attributes on line." );

      int[] dataSample = new int[ m_attributes.size() ];

      try {
        // 2.a. Deal with all the attributes.
        for( int i = 0; i < rawSample.size(); i++ ) {
          // There should be a 1-to-1 ordering between
          // the internal attributes vector and the
          // raw sample vector.
          Attribute currAtt = (Attribute)m_attributes.elementAt( i );

          int attPos = currAtt.getAttributeValuePosition(
                         (String)rawSample.elementAt( i ) );
          dataSample[i] = attPos;

            if( i == 0 ) m_targetSums[ attPos ]++;
        }
      }
      catch( NonexistentAttributeValueException e ) {
        // One of the attribute values on this line of
        // the data file doesn't correspond with the values
        // specified in the config file.
        throw new
          InvalidDataFileException( "Syntax error in data " +
            "file (line " + parser.getCurrentLineNum() +
            "): Attribute value does not match any values in " +
            "configuration file." );
      }

      // Add the last data sample to our current training set.
      m_trainingSet.add( dataSample );
    }

    //------------------------- DEBUG -------------------------
    if( DEBUG_ON ) {
      System.out.println( "Dataset::parseDataFile: " +
        "Finished parsing data file." );
      System.out.println();

      // Target attribute information.
      System.out.println( "Data Samples" );
      System.out.println( "------------------------------" );
      System.out.println();
      System.out.println( "Parsed and stored " + m_trainingSet.size() +
                          " data samples." );
      System.out.println();
      System.out.print( "Target classification counts are: " );

      for( int i = 0; i < m_targetSums.length; i++ )
        System.out.print( m_targetSums[i] + "  " );

      System.out.println();
      System.out.println();

    }
  }
}
