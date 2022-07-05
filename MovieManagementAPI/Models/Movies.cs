using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieManagementAPI.Models
{
    public partial class Movies
    {
        public int? Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public string Release { get; set; }
    }
}
