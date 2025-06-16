using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemMinimalApi.Application.DTOs
{
    public class BorrowReturnDto
    {
        [Required]
        [StringLength(200, MinimumLength = 1)]

        public string? Title { get; set; }

        [Range(1450, 2024)]
        public int PublicationYear { get; set; }

        [Required]
        public int MemberID { get; set; }
    }
}
