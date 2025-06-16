﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemMinimalApi.Application.DTOs
{
    public class CreateMemberDto
    {
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string? Name { get; set; }

        [Required]
        [Range(0, 2)]
        public int MemberType { get; set; }
    }
}
