using ChatJaffApp.Server.Data.Contracts;

namespace ChatJaffApp.Server.Data.Models
{
    public class Member : IMember
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
    }
}
