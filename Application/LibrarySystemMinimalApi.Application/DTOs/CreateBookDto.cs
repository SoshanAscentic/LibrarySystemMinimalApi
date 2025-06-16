﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemMinimalApi.Application.DTOs
{
    public class CreateBookDto
    {
        [Required]
        [StringLength(200, MinimumLength = 1)]
        public string? Title { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string? Author { get; set; }

        [Range(1450, 2024)]
        public int PublicationYear { get; set; }

        [Required]
        [Range(0, 2)]
        public int Category { get; set; }
    }
}
