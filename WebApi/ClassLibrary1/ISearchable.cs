using System.Collections.Generic;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// interface for a searchable item
    /// </summary>
    /// <typeparam name="T">the type of item that we'll be searchable.</typeparam>
    public interface ISearchable<T>
    {
        State<T> GetInitialState();
        State<T> GetGoalState();
        List<State<T>> GetAllPossibleStates(State<T> s);
    }
}