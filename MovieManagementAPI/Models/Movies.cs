using System;
using System.Collections.Generic;

namespace MovieManagementAPI.Models
{
    public partial class Movies
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Release { get; set; }
    }
}
