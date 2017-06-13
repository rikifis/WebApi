
namespace SearchAlgorithmsLib
{
    /// <summary>
    /// an object that can search a searchable item.
    /// </summary>
    /// <typeparam name="T">the type of the searchable.</typeparam>
    public interface ISearcher<T>
    {
        Solution<State<T>> Search(ISearchable<T> searchable);
        // get how many nodes were evaluated by the algorithm
        int GetNumberOfNodesEvaluated();
    }
}
