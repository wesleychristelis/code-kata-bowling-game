using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScoringClass
{
    public class Game
    {
        public Game()
        {

        }

        private List<Player> _players;

        public void Start(int playerCount)
        {
            if(playerCount > 0)
            {
                _players = new List<Player>();
                for (int i = 0; i < playerCount; i++)
                {
                    _players.Add(new Player());
                }
            }
        }

        public Player GetPlayer(int playerIndex)
        {
            return _players[playerIndex];
        }
    }
}
