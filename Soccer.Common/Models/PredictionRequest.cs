using System;
using System.ComponentModel.DataAnnotations;

namespace Soccer.Common.Models
{
    public class PredictionRequest
    {
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public int MatchId { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public int GoalsLocal { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public int GoalsVisitor { get; set; }

        [Required]
        public string CultureInfo { get; set; }
    }
}
