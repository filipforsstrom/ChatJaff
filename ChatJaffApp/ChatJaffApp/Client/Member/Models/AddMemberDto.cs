namespace ChatJaffApp.Client.Member.Models
{
    public class AddMemberDto
    {
        public Guid UserId { get; set; }
        public Guid? ChatId { get; set; }
    }
}
