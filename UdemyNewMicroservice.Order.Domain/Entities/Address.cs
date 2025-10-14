namespace UdemyNewMicroservice.Order.Domain.Entities;

public class Address : BaseEntity<int>
{
    public string Province { get; set; } = null!;
    public string District { get; set; } = null!;
    public string Street { get; set; } = null!;
    public string ZipCode { get; set; } = null!;
    public string Line { get; set; } = null!;
}