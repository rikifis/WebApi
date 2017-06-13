using Priority_Queue;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// searcher class
    /// </summary>
    /// <typeparam name="T">the type of the searchable</typeparam>
    public abstract class Searcher<T> : ISearcher<T>
    {
        /// <summary>
        /// open list for th algo.
        /// </summary>
        private SimplePriorityQueue<State<T>> openList;
        /// <summary>
        /// number nodes evaluated in the algo.
        /// </summary>
        private int evaluatedNodes;

        /// <summary>
        /// searcher constructor.
        /// </summary>
        public Searcher()
        {
            openList = new SimplePriorityQueue<State<T>>();
            evaluatedNodes = 0;
        }

        /// <summary>
        /// get state from open list.
        /// </summary>
        /// <returns></returns>
        protected State<T> PopOpenList()
        {
            // this node is now evaluated.
            evaluatedNodes++;
            return openList.Dequeue();
        }
        
        /// <summary>
        /// add state to open list.
        /// </summary>
        /// <param name="s">the state to add.</param>
        protected void AddToOpenList(State<T> s)
        {
            openList.Enqueue(s, s.Cost);
        }

        /// <summary>
        /// check if open list contains a given state.
        /// </summary>
        /// <param name="s">th estate to check</param>
        /// <returns></returns>
        protected bool OpenContains(State<T> s)
        {
            return openList.Contains(s);
        }

        /// <summary>
        /// update states priority,
        /// </summary>
        /// <param name="s">the state to update</param>
        /// <param name="cost">the updated cost</param>
        protected void UpdatePriority(State<T> s, float cost)
        {
            openList.UpdatePriority(s, cost);
        }

        /// <summary>
        /// get the open list size.
        /// </summary>
        public int OpenListSize
        { 
            get  {return openList.Count; }
        }
        // ISearcher’s methods:
        public int GetNumberOfNodesEvaluated()
        {
            return evaluatedNodes;
        }

        /// <summary>
        /// abstract method for searching.
        /// </summary>
        /// <param name="searchable">the item to search</param>
        /// <returns></returns>
        public abstract Solution<State<T>> Search(ISearchable<T> searchable);
    }
}
