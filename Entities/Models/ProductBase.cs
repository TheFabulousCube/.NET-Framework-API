﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class ProductBase
    {
        [Key]
        [Column(TypeName = "varchar(4)")]
        public string ProdId { get; set; }

        [Required]
        [Column(TypeName = "varchar(25)")]
        public string ProdPicture { get; set; }

        [Required]
        [Column(TypeName = "decimal(6,2)")]
        public decimal ProdPrice { get; set; }

        [Required]
        [Column(TypeName = "int(11)")]
        public int ProdQty { get; set; }

        [Required]
        [Column(TypeName = "varchar(10)")]
        public string Catagory { get; set; }

        public bool IsMagnet()
        {
            return this.Catagory == LookUps.Catagory.magnets.ToString();
        }
    }
}