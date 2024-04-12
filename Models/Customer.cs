using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Copernicus2.Models
{
    public partial class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Configurar como autoincremental
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string First { get; set; } = null!;
        public string Last { get; set; } = null!;
        public string Company { get; set; } = null!;
        public string CreatedAt { get; set; } = null!;
        public string Country { get; set; } = null!;
    }
}
