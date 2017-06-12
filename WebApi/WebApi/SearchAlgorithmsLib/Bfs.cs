using System.Collections.Generic;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// a searcher with the bfs algorithm.
    /// </summary>
    /// <typeparam name="T">the type of the searchable.</typeparam>
    public class Bfs<T> : Searcher<T>
    {
        // <summary>
        /// the search method.
        /// </summary>
        /// <param name="searchable">the item we'll search on.</param>
        /// <returns>the solution of the search</returns>
        public override Solution<State<T>> Search(ISearchable<T> searchable)
        {
            // inherited from Searcher.
            AddToOpenList(searchable.GetInitialState());
            HashSet<State<T>> closed = new HashSet<State<T>>();
            State<T> goal = searchable.GetGoalState();
            // loop while the open list is'nt empty.
            while (OpenListSize > 0)
            {
                // inherited from Searcher, removes the best state.
                State<T> n = PopOpenList(); 
                closed.Add(n);
                // fount goal.
                if (n.Equals(goal))
                {
                    // find route to goal.
                    return BackTrace(n);
                }
                                        
                // calling the delegated method, returns a list of states with n as a parent
                List<State<T>> succerssors = searchable.GetAllPossibleStates(n);
                foreach (State<T> s in succerssors)
                {
                    if (!closed.Contains(s) && !OpenContains(s))
                    {
                        s.Cost = n.Cost + 1;
                        AddToOpenList(s);
                    }
                    else
                    {
                        // if cost is better.
                        if ((n.Cost + 1) < s.Cost)
                        {
                            if (!OpenContains(s))
                                AddToOpenList(s);
                            else
                                s.Cost = n.Cost + 1;
                        }
                    }
                }
            }
            return new Solution<State<T>>();
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
            Solution<State<T>> sol = new Solution<State<T>>();
            sol.SolLst = trace;
            return sol;
        }
       
    }
}