using System.Net.Sockets;
using System.Threading;

namespace Models
{
    /// <summary>
    /// a multi player game.
    /// </summary>
    class MultiPlayerGame
    {
        /// <summary>
        /// first player.
        /// </summary>
        private TcpClient client1;
        /// <summary>
        /// second player.
        /// </summary>
        private TcpClient client2;
        /// <summary>
        /// has 2 players.
        /// </summary>
        private bool twoPlayers;
        /// <summary>
        /// the name of the game.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// the constructor of the multi player game.
        /// </summary>
        /// <param name="client">the first player.</param>
        /// <param name="name">name of the game.</param>
        public MultiPlayerGame(TcpClient client, string name)
        {
            client1 = client;
            twoPlayers = false;
            Name = name;
        }

        /// <summary>
        /// waut for second player to join.
        /// </summary>
        public void WaitForJoin()
        {
            // wait.
            while (!twoPlayers)
            {   
                Thread.Sleep(100);
            }
        }

        /// <summary>
        /// set the second player.
        /// </summary>
        /// <param name="client">second player.</param>
        public void SetSecondPlayer(TcpClient client)
        {
            client2 = client;
            twoPlayers = true;
        }

        /// <summary>
        /// check if the given player is playing.
        /// </summary>
        /// <param name="player">the player to check</param>
        /// <returns>true if exists false otherwise</returns>
        public bool IsPlayer(TcpClient player)
        {
            if (player == client1 || player == client2)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// get the oppnent of the given player.
        /// </summary>
        /// <param name="player1">the player that wants to get his opponent</param>
        /// <returns>get the opponent</returns>
        public TcpClient GetOpponent(TcpClient player1)
        {
            // client2 is the opponent.
            if (player1 == client1)
            {
                return client2;
            // client1 is the opponent.
            } else if (player1 == client2)
            {
                return client1;
            }
            // player dosesn't exist.
            else
            {
                return null;
            }
        }
    }
}
