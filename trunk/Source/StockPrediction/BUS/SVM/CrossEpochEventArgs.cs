using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BUS.SVM
{
    /// <summary>
    /// Cross Epoch Event Handler. This delegate handles events invoked whenever a training epoch
    /// starts or ends
    /// </summary>
    /// <param name="sender">
    /// The sender invoking the event
    /// </param>
    /// <param name="e">
    /// Event Arguments
    /// </param>
    public delegate void CrossEpochEventHandler(object sender, CrossEpochEventArgs e);

    /// <summary>
    /// Cross Epoch Event Arguments
    /// </summary>
    public class CrossEpochEventArgs : EventArgs
    {
        private int _trainingIteration;
        private int _cycles;

        /// <summary>
        /// Gets the current training iteration
        /// </summary>
        /// <value>
        /// Current Training Iteration.
        /// </value>
        public int TrainingIteration
        {
            get { return _trainingIteration; }
        }

        /// <summary>
        /// Get the number of cycles
        /// </summary>
        public int Cycles
        {
            get { return _cycles; }
            set { _cycles = value; }
        }

        /// <summary>
        /// Creates a new instance of training epoch event arguments
        /// </summary>
        /// <param name="trainingIteration">
        /// Current training iteration
        /// </param>
        /// <param name="trainingSet">
        /// The training set associated with the event
        /// </param>
        public CrossEpochEventArgs(int trainingIteration, int cycles)
        {
            this.Cycles = cycles;
            this._trainingIteration = trainingIteration;
        }
    }
}
