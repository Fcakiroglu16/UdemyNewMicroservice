#region

using System.ComponentModel.DataAnnotations;

#endregion

namespace UdemyNewMicroservice.Discount.Api.Options;

public class MongoOption
{
    [Required] public string DatabaseName { get; set; } = default!;
    [Required] public string ConnectionString { get; set; } = default!;
}