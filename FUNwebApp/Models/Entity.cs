using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KillerFUNwebApp1._0.Models.Enums;

namespace KillerFUNwebApp1._0.Models
{
    public abstract class Entity
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Health { get; set; }
        public int Attack { get; set; }
        public int Defence { get; set; }

        public void move(Direction direction)
        {
            throw new NotImplementedException();
        }

        public void attack(Direction directoin)
        {
            throw new NotImplementedException();
        }
    }
}