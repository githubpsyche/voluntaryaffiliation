using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ExperimentalGoal.Models
{
    [Table("Game")]
    public class Game
    {
        [Key]
        public int GameID { get; set; }

        [Required(ErrorMessage = "Select Race")]
        public required string Race { get; set; }
        
        [Required(ErrorMessage = "Please Enter Age")]
        [Range(0, 99, ErrorMessage = "Age Can only be between 0 .. 99")]
        [StringLength(2, ErrorMessage = "Max 2 digits")]
        public required string Age { get; set; }

        [Required(ErrorMessage = "Select Gender")]
        public required string Gender { get; set; }

        public  string? GameSelectionText { get; set; }

        public string? PlayersOrder { get; set; }

        public string? Team1 { get; set; }

        public string? Team2 { get; set; }

        [Required(ErrorMessage = "Select College Pursuing or not")]
        public string? PursuingCollege { get; set; }

        public string? Rating { get; set; }

        public string? NoGame { get; set; }
    }
}
