using System.Collections.Generic;
using System.Linq;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// a searcher with the dfs algorithm.
    /// </summary>
    /// <typeparam name="T">the type of the searchable.</typeparam>
    public class Dfs<T> : ISearcher<T>
    {
        /// <summary>
        /// number of nodes evaluated in the dfs solution.
        /// </summary>
        private int evaluatedNodes;

        /// <summary>
        /// the search method.
        /// </summary>
        /// <param name="searchable">the item we'll search on.</param>
        /// <returns>the solution of the search</returns>
        public Solution<State<T>> Search(ISearchable<T> searchable)
        {
            evaluatedNodes = 0;
            Stack <State<T>> stack = new Stack<State<T>>();
            // save the nodes we evaluated.
            HashSet<State<T>> discovered = new HashSet<State<T>>();
            State<T> goal = searchable.GetGoalState();
            stack.Push(searchable.GetInitialState());
            // loop unti stack is empty.
            while (stack.Count() > 0)
            {
                State<T> currState = stack.Pop();
                evaluatedNodes++;
                // found goal.
                if (currState.Equals(goal))
                {
                    // find the route to it.
                    return BackTrace(currState);
                }
                // was;nt discovered yet.
                if (!discovered.Contains(currState))
                {
                    discovered.Add(currState);
                    // calling the delegated method, returns a list of states with n as a parent
                    List<State<T>> succerssors = searchable.GetAllPossibleStates(currState);
                    foreach (State<T> s in succerssors)
                    {
                        stack.Push(s);
                    }
                }
            }
            return new Solution<State<T>>();
        }

        /// <summary>
        /// get how many nodes were evaluated by the algorithm.
        /// </summary>
        public int GetNumberOfNodesEvaluated()
        {
            return evaluatedNodes;
        }

        /// <summary>
        /// backtrace the route to a given state.
        /// </summary>
        /// <param name="st">the given state</param>
        /// <returns></returns>
        private Solution<State<T>> BackTrace(State<T> st)
        {
            // list of the states in the route.
            List<State<T>> trace = new List<State<T>>();
            do
            {
                trace.Add(st);
                st = st.CameFrom;
            } while (st != null);
            Solution<State<T>> sol = new Solution<State<T>>()
            {
                SolLst = trace
            };
            return sol;
        }
    }
}
