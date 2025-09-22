namespace UdemyNewMicroservice.Web.Options
{
    public class IdentityOption
    {
        public IdentityOptionItem Admin { get; set; } = null!;
        public IdentityOptionItem Web { get; set; } = null!;
    }

    public class IdentityOptionItem
    {
        public required string Address { get; set; }
        public required string ClientId { get; set; }
        public required string ClientSecret { get; set; }
    }
}