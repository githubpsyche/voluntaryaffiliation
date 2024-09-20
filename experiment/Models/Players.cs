using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExperimentalGoal.Models
{
    [Table("Players")]
    public class Players
    {
        [Key]
        public int PlayerID { get; set; }

        public required string Race { get; set; }

        public required string Age { get; set; }

        public required string Gender { get; set; }

        public required string Picture { get; set; }
    }
}
