﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KillerFUNwebApp1._0.Models.Enums;

namespace KillerFUNwebApp1._0.Models
{
    public abstract class Thing
    {
        public int ID { get; set; }
        public ThingType Type { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}