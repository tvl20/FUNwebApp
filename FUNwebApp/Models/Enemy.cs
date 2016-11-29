using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KillerFUNwebApp1._0.Models.Enums;

namespace KillerFUNwebApp1._0.Models
{
    public abstract class Enemy : Entity
    {
        public Soort Soort { get; set; }
    }
}