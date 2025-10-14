namespace UdemyNewMicroservice.Web.Options;

public class MicroserviceOption
{
    public required MicroserviceOptionItem Catalog { get; set; }

    public required MicroserviceOptionItem File { get; set; }

    public required MicroserviceOptionItem Basket { get; set; }
    public required MicroserviceOptionItem Discount { get; set; }

    public required MicroserviceOptionItem Order { get; set; }
}

public class MicroserviceOptionItem
{
    public required string BaseAddress { get; set; }
}