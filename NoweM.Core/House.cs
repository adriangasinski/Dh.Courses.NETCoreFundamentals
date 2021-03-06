﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NoweM.Core
{
    public class House
    {
        public int Id { get; set; }
        public int NumberOfHouses { get; set; }
        public int DevId { get; set; }
        [Required, StringLength(80)]
        public string Address { get; set; }
        public HouseType HType { get; set; }
    }
}
