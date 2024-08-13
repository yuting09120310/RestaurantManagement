namespace RestaurantManagement.API.Dtos
{
    public class MemberDto
    {
        public int MemberId { get; set; }
        public string MemberAccount { get; set; }
        public string MemberName { get; set; }
        public string MemberPhone { get; set; }
        public string MemberEmail { get; set; }
        public int MemberPublish { get; set; }
        public DateTime? LastLogin { get; set; }
        public string IP { get; set; }
    }

    public class UpdateMemberDto
    {
        public int MemberId { get; set; }
        public string MemberName { get; set; }
        public string MemberEmail { get; set; }
        public string MemberPhone { get; set; }
        public string MemberPassword { get; set; }
    }

    public class CreateMemberDto
    {
        public string MemberAccount { get; set; }
        public string MemberPassword { get; set; }
        public string MemberName { get; set; }
        public string MemberPhone { get; set; }
        public string MemberEmail { get; set; }
        public int MemberPublish { get; set; } = 1; 
        public DateTime? LastLogin { get; set; }
        public string IP { get; set; }
    }

    namespace RestaurantManagement.API.Dtos
    {
        public class DeleteMemberDto
        {
            public int MemberId { get; set; }
        }
    }

}
