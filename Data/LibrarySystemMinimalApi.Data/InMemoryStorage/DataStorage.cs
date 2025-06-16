using LibrarySystemMinimalApi.Domain.Entities;
using LibrarySystemMinimalApi.Domain.Entities.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemMinimalApi.Data.InMemoryStorage
{
    public class DataStorage
    {
        public List<Book> Books { get; set; } = new List<Book>();
        public List<Member> Members { get; set; } = new List<Member>();
        public int NextMemberId { get; set; } = 1;

    }
}
