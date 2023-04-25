using DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessNew.Models
{
    public class Order : Entity
    {
        public int UserId { get; set; }

        public bool IsPaid { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

    }
}
