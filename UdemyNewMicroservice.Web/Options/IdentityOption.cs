namespace UdemyNewMicroservice.Web.Options;

public class IdentityOption
{
    public required string Address { get; set; }

    public required string BaseAddress { get; set; }
    public IdentityOptionItem Admin { get; set; } = null!;
    public IdentityOptionItem Web { get; set; } = null!;
}

public class IdentityOptionItem
{
    public required string ClientId { get; set; }
    public required string ClientSecret { get; set; }
}