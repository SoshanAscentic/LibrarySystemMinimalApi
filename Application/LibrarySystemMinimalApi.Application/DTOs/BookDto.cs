using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemMinimalApi.Application.DTOs
{
    public class BookDto
    {
        public string? Title { get; set; }
        public string? Author { get; set; }
        public int PublicationYear { get; set; }
        public string? Category { get; set; }
        public bool IsAvailable { get; set; }
    }
}
