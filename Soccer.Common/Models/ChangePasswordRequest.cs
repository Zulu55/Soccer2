﻿using System.ComponentModel.DataAnnotations;

namespace Soccer.Common.Models
{
    public class ChangePasswordRequest
    {
        [Required]
        [StringLength(20, MinimumLength = 6)]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 6)]
        public string NewPassword { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string CultureInfo { get; set; }
    }
}
