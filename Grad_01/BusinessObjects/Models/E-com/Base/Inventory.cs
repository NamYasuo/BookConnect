﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace BusinessObjects.Models
{
    public class Inventory
    {
        [Key]
        public Guid InventoryId { get; set; }

        public Guid AgencyId { get; set; }

        public Guid BookId { get; set; }

        public int Price { get; set; }
        public DateTime DateAdded { get; set; }

        public int Quantity { get; set; }

        [ForeignKey("AgencyId"), JsonIgnore]
        public virtual Agency Agency { get; set; } = null!;

        [ForeignKey("BookId"), JsonIgnore]
        public virtual Book Book { get; set; } = null!;
    }
}
