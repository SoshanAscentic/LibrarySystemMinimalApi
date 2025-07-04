﻿using LibrarySystemMinimalApi.Domain.Entities.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemMinimalApi.Domain.Entities.Staff
{
    public abstract class Staff : Member
    {
        // Parameterless constructor for EF Core
        protected Staff() { }

        protected Staff(string name) : base(name) { }

        public override bool CanManageBooks() => true;
        public override bool CanViewBooks() => true;
        public override bool CanViewMembers() => true;
    }
}
