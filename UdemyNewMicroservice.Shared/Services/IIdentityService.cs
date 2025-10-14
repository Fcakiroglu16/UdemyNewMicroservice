namespace UdemyNewMicroservice.Shared.Services;

public interface IIdentityService
{
    Guid UserId { get; }
    string UserName { get; }

    List<string> Roles { get; }
}