using System;
using System.Collections.Generic;
using System.Text;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// State on a searchable.
    /// </summary>
    /// <typeparam name="T">type of state.</typeparam>
    public class State<T>
    {
        /// <summary>
        /// the state represented by T.
        /// </summary>
        public T StateType { get; set; }
        /// <summary>
        ///  cost to reach this state (set by a setter)
        /// </summary>
        public float Cost { get; set; }
        /// <summary>
        /// the state we came from to this state (setter)
        /// </summary>
        public State<T> CameFrom { get; set; }

        /// <summary>
        /// state constroctor.
        /// </summary>
        /// <param name="state">the state represented by T</param>
        public State(T state)
        {
            this.StateType = state;
            this.Cost = 0;
            this.CameFrom = null;
        }

        /// <summary>
        /// equals method overriden for state.
        /// </summary>
        /// <param name="obj">the obj to be equaled to</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return StateType.Equals((obj as State<T>).StateType);
        }

        /// <summary>
        /// GetHashCode overrided for state.
        /// </summary>
        /// <returns>the hash code</returns>
        public override int GetHashCode()
        {
            return StateType.ToString().GetHashCode();
        }
    }
}
