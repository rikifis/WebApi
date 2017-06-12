using System.Collections.Generic;
using MazeLib;
using SearchAlgorithmsLib;

namespace Models
{
    /// <summary>
    /// MazeSearchable is a maze that we can search. 
    /// </summary>
    public class MazeSearchable : ISearchable<Position>
    {
        /// <summary>
        /// the maze we'll search.
        /// </summary>
        private Maze maze;
        /// <summary>
        /// constroctor of MazeSearchable.
        /// </summary>
        /// <param name="maze">the maze we'll search</param>
        public MazeSearchable(Maze maze)
        {
            this.maze = maze;
        }

        /// <summary>
        /// get the starting state of the maze.
        /// </summary>
        /// <returns></returns>
        public State<Position> GetInitialState()
        {
            return new State<Position>(maze.InitialPos);
        }

        /// <summary>
        /// get the goat state of the game.
        /// </summary>
        /// <returns></returns>
        public State<Position> GetGoalState()
        {
            return new State<Position>(maze.GoalPos);
        }

        /// <summary>
        /// gets all the states we can move to from a current state.
        /// </summary>
        /// <param name="s">the current state</param>
        /// <returns></returns>
        public List<State<Position>> GetAllPossibleStates(State<Position> s)
        {
            List<State<Position>> posSt = new List<State<Position>>();
            // checks if we're not out of row bound.
            if (s.StateType.Row + 1 < maze.Rows)
            {
                // check if we can go up, (not a wall).
                if (maze[s.StateType.Row + 1, s.StateType.Col] == 0)
                {
                    State<Position> st = 
                            new State<Position>(new Position(s.StateType.Row + 1, s.StateType.Col))
                    {
                        CameFrom = s
                    };
                    posSt.Add(st);
                }
            }
            // checks if we're not out of row bound.
            if (s.StateType.Row - 1 >= 0)
            {
                // check if we can go down, (not a wall).
                if (maze[s.StateType.Row - 1, s.StateType.Col] == 0)
                {
                    State<Position> st =
                            new State<Position>(new Position(s.StateType.Row - 1, s.StateType.Col))
                    {
                        CameFrom = s
                    };
                    posSt.Add(st);
                }
            }
            // checks if we're not out of collom bound.
            if (s.StateType.Col + 1 < maze.Cols)
            {
                // check if we can go right, (not a wall).
                if (maze[s.StateType.Row, s.StateType.Col + 1] == 0)
                {
                    State<Position> st = 
                            new State<Position>(new Position(s.StateType.Row, s.StateType.Col + 1))
                    {
                        CameFrom = s
                    };
                    posSt.Add(st);
                }
            }
            // checks if we're not out of collom bound.
            if (s.StateType.Col - 1 >= 0)
            {
                // check if we can go left, (not a wall).
                if (maze[s.StateType.Row, s.StateType.Col - 1] == 0)
                {
                    State<Position> st =
                            new State<Position>(new Position(s.StateType.Row, s.StateType.Col - 1))
                    {
                        CameFrom = s
                    };
                    posSt.Add(st);
                }
            }
            return posSt;
        }
    }
}
