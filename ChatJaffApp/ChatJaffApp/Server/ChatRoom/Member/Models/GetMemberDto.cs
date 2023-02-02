namespace ChatJaffApp.Server.ChatRoom.Member.Models
{
    public class GetMemberDto
    {
        public Guid UserId  { get; set; }
        public string? UserName { get; set; }
    }
}
