namespace LibrarySystemMinimalApi.Domain.Entities.Members
{
    public abstract class Member
    {
        public string Name { get; set; }
        public int MemberID { get; protected set; }
        public int BorrowedBooksCount { get; set; } = 0;

        // Parameterless constructor for EF Core
        protected Member() { }

        protected Member(string name, int memberId)
        {
            Name = string.IsNullOrWhiteSpace(name) ? throw new ArgumentException("Name cannot be empty") : name;
            MemberID = memberId > 0 ? memberId : throw new ArgumentException("Member ID must be positive");
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