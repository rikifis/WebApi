using System.Collections.Generic;
using System.Linq;
using MazeLib;
using SearchAlgorithmsLib;
using MazeGeneratorLib;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Sockets;

namespace Models
{
    /// <summary>
    /// the maze mode.
    /// </summary>
    class MazeModel : IModel<Maze>
    {
        /// <summary>
        /// single game mazes.
        /// </summary>
        private static Dictionary<string, Maze> singleMazes;
        /// <summary>
        /// single player solved mazes.
        /// </summary>
        private static Dictionary<string, string> solvedMazes;
        /// <summary>
        /// multi player mazes that are witing for another player.
        /// </summary>
        private static Dictionary<string, Maze> toJoinMazes;
        /// <summary>
        /// multi player pagmes.
        /// </summary>
        private static Dictionary<string, MultiPlayerGame> multiGames;
        /// <summary>
        /// mazes that are currently playing.
        /// </summary>
        private static Dictionary<string, Maze> playingMazes;

        /// <summary>
        /// constructor for the maze model.
        /// </summary>
        public MazeModel()
        {
            singleMazes = new Dictionary<string, Maze>();
            solvedMazes = new Dictionary<string, string>();
            toJoinMazes = new Dictionary<string, Maze>();
            multiGames = new Dictionary<string, MultiPlayerGame>();
            playingMazes = new Dictionary<string, Maze>();
        }
       

        /// <summary>
        /// generate a new game.
        /// </summary>
        /// <param name="name">the name of the game</param>
        /// <param name="x">number of colloms</param>
        /// <param name="y">number of rows</param>
        /// <returns>the new maze</returns>
        public Maze Generate(string name, int x, int y)
        {
             // override the old maze if exists.
            if (singleMazes.ContainsKey(name))
            {
                singleMazes.Remove(name);
                if (solvedMazes.ContainsKey(name))
                {
                    solvedMazes.Remove(name);
                }
            }
            // generate the maze.
            IMazeGenerator gen = new DFSMazeGenerator();
            Maze maze = gen.Generate(x, y);
            maze.Name = name;
            // add to single mazes.
            singleMazes.Add(name, maze);
            // return the new maze.
            return singleMazes[name];
        }

        /// <summary>
        /// solve a maze with a search algorithm.
        /// </summary>
        /// <param name="name">the name of the maze</param>
        /// <param name="searcher">the seaech algorithm for solving the maze</param>
        /// <returns>the solution for the maze</returns>
        public string Solve(string name, int searcher)
        {
            // the maze was'nt found.
            if (!singleMazes.ContainsKey(name))
            {
                return "maze not found";
            }
            // the maze algo is illegal.
            if (searcher != 0 && searcher != 1)
            {
                return "no algorithm";
            }
            // if the solution dos'nt exist.
            if (!solvedMazes.ContainsKey(name))
            {
                // adds the solution.
                AddSolution(name, searcher);
            }
            return solvedMazes[name];
        }

        /// <summary>
        /// add a solution to a maze.
        /// </summary>
        /// <param name="name">the name of the maze</param>
        /// <param name="searcher">the searcher algo</param>
        private void AddSolution(string name, int searcher)
        {
            // the solution.
            Solution<State<Position>> solution = new Solution<State<Position>>();
            int nodesEv = 0;
            ISearchable<Position> srMaze = new MazeSearchable(singleMazes[name]);
            // bfs algo.
             if (searcher == 0)
            {
                ISearcher<Position> bfs = new Bfs<Position>();
                solution = bfs.Search(srMaze);
                nodesEv = bfs.GetNumberOfNodesEvaluated();
            }
             // dfs algo.
            else
            {
                ISearcher<Position> dfs = new Dfs<Position>();
                solution = dfs.Search(srMaze);
                nodesEv = dfs.GetNumberOfNodesEvaluated();
            }
            string strSol = " ";
            // backtrace the maze solution.
            for (int i = solution.SolLst.Count - 1; i > 0; i--)
            {
                // went down.
                if (solution.SolLst[i].StateType.Row > solution.SolLst[i - 1].StateType.Row)
                {
                    strSol += "2";
                }
                // went up.
                else if (solution.SolLst[i].StateType.Row < solution.SolLst[i - 1].StateType.Row)
                {
                    strSol += "3";
                }
                // went left.
                else if (solution.SolLst[i].StateType.Col > solution.SolLst[i - 1].StateType.Col)
                {
                    strSol += "0";
                }
                // went right.
                else if (solution.SolLst[i].StateType.Col < solution.SolLst[i - 1].StateType.Col)
                {
                    strSol += "1";
                }
            }
            // the solution in json.
            JObject sol = new JObject
            {
                { "Name", name },
                { "Solution", strSol },
                { "NodesEvaluated", nodesEv }
            };
            string Jsol = JsonConvert.SerializeObject(sol);
             JsonConvert.DeserializeObject(Jsol);
            // add the solutin.
            solvedMazes.Add(name, Jsol);
        }

        /// <summary>
        /// start a multi player game.
        /// </summary>
        /// <param name="client">the client that will be player1</param>
        /// <param name="name">the name of the game</param>
        /// <param name="x">number of rows.</param>
        /// <param name="y">number off cols</param>
        /// <returns>returns the game maze</returns>
        public Maze Start(TcpClient client, string name, int x, int y)
        {
            // the game exist.
            if (toJoinMazes.ContainsKey(name))
            {
                // error.
                return null;
            }
            // generate the 
            IMazeGenerator gen = new DFSMazeGenerator();
            Maze maze = gen.Generate(x, y);
            maze.Name = name;
            // add the game,
            toJoinMazes.Add(name, maze);
            // buils a muli player game.
            MultiPlayerGame game = new MultiPlayerGame(client, name);
            multiGames.Add(name, game);
            game.WaitForJoin();
            // we got another player.
            return playingMazes[name];
        }

        /// <summary>
        /// join a game.
        /// </summary>
        /// <param name="client">player2</param>
        /// <param name="name">name of game to join.</param>
        /// <returns>the game.</returns>
        public Maze Join(TcpClient client, string name)
        {
            if (!toJoinMazes.ContainsKey(name))
            {
                // error. we dont have such game available.
                return null;
            }
            // add to a playing game.
            playingMazes.Add(name, toJoinMazes[name]);
            // remove from to join mazes.
            toJoinMazes.Remove(name);
            // set the second player.
            multiGames[name].SetSecondPlayer(client);
            // return the game.
            return playingMazes[name];
        }

        /// <summary>
        /// list of games that are ready to join.
        /// </summary>
        /// <returns>the list</returns>
        public string List()
        {
            JArray Jsol = new JArray(toJoinMazes.Keys);
            return Jsol.ToString();
        }

        /// <summary>
        /// get a multiplayer game.
        /// </summary>
        /// <param name="player">the player</param>
        /// <returns>the game</returns>
        public MultiPlayerGame GetGame(TcpClient player)
        {
            MultiPlayerGame game = null;
            // go over the game
            for (int i = 0; i < multiGames.Count; i++)
            {
                game = multiGames.ElementAt(i).Value;
                // check if that is the players game.
                if (game.IsPlayer(player))
                {
                    return game;
                }
            }
            // game wasn't found.
            return null;
        }
    }
}
