using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KillerFUNwebApp1._0.Models;
using KillerFUNwebApp1._0.Models.Enums;

namespace KillerAppFUN2.DAL
{
    interface IPlayerRepo
    {
        Player getPlayer(string playerName);
        List<Player> getAllPlayers();
        void addPlayer(string Name, Class playerClass);
        void updatePlayer(Player p);
        void deletePlayer(string name);
    }
}
