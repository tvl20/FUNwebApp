using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KillerFUNwebApp1._0.Models;
using KillerFUNwebApp1._0.Models.Enums;

namespace KillerAppFUN2.DAL
{
    class PlayerBizLog
    {
        private IPlayerRepo repo;

        public PlayerBizLog(IPlayerRepo _repo)
        {
            repo = _repo;
        }

        public List<string> getPlayerNames()
        {
            List<Player> players = repo.getAllPlayers();
            List<string> names = new List<string>();

            foreach (Player p in players)
            {
                names.Add(p.Name);
            }

            return names;
        }

        public List<Player> GetAllPlayers()
        {
            return repo.getAllPlayers();
        }

        public bool playerNameTaken(string playerName)
        {
            List<string> takenNames = getPlayerNames();

            foreach (string name in takenNames)
            {
                if (playerName == name)
                {
                    return true;
                }
            }

            return false;
        }

        public Player getPlayer(string playerName)
        {
            return repo.getPlayer(playerName);
        }

        public void addPlayer(string Name, Class playerClass)
        {
            repo.addPlayer(Name, playerClass);
        }

        public void updatePlayer(Player p)
        {
            repo.updatePlayer(p);
        }

        public void deletePlayer(string name)
        {
            repo.deletePlayer(name);
        }
    }
}
