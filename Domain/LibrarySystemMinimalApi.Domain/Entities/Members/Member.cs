namespace LibrarySystemMinimalApi.Domain.Entities.Members
{
    public abstract class Member
    {
        private string name;

        public string Name
        {
            get => name ?? string.Empty; // Return empty string if null to avoid null reference issues
            set => name = value; // No validation in setter for EF Core compatibility
        }

        public int MemberID { get; protected set; }
        public int BorrowedBooksCount { get; set; } = 0;

        // Parameterless constructor for EF Core
        protected Member() { }

        protected Member(string name)
        {
            // Validation only in the constructor used for creating new entities
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty", nameof(name));
            this.name = name;
        }

        public abstract string GetMemberType();
        public virtual bool CanBorrowBooks() => false;
        public virtual bool CanViewBooks() => false;
        public virtual bool CanViewMembers() => false;
        public virtual bool CanManageBooks() => false;

        public override string ToString()
        {
            return $"Name: {Name}, ID: {MemberID}, Type: {GetMemberType()}, Borrowed Books: {BorrowedBooksCount}";
        }
    }
}