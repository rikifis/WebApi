using System.Net.Sockets;
using System.Threading;

namespace WebApi.Models
{
    /// <summary>
    /// a multi player game.
    /// </summary>
    class MultiPlayerGame
    {
        /// <summary>
        /// first player.
        /// </summary>
        private User user1;
        /// <summary>
        /// second player.
        /// </summary>
        private User user2;
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
        public MultiPlayerGame(User user, string name)
        {
            user1 = user;
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
        public void SetSecondPlayer(User user)
        {
            user2 = user;
            twoPlayers = true;
        }

        /// <summary>
        /// check if the given player is playing.
        /// </summary>
        /// <param name="player">the player to check</param>
        /// <returns>true if exists false otherwise</returns>
        public bool IsPlayer(User user)
        {
            if (user == user1 || user == user2)
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
        public User GetOpponent(User player1)
        {
            // client2 is the opponent.
            if (player1 == user1)
            {
                return user2;
            // client1 is the opponent.
            } else if (player1 == user2)
            {
                return user1;
            }
            // player dosesn't exist.
            else
            {
                return null;
            }
        }
    }
}
