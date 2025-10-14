#region

using MongoDB.Bson.Serialization.Attributes;

#endregion

namespace UdemyNewMicroservice.Catalog.Api.Repositories;

public class BaseEntity
{
    [BsonElement("_id")] public Guid Id { get; set; }
}