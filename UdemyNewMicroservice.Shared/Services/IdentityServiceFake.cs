namespace UdemyNewMicroservice.Shared.Services;

public class IdentityServiceFake : IIdentityService
{
    public Guid UserId => Guid.Parse("332ee8cd-f3f6-49fa-92e2-5fdb188b3377");
    public string UserName => "Ahmet16";
    public List<string> Roles => [];
}