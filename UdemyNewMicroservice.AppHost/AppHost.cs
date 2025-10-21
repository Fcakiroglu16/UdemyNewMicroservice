#region

using Projects;

#endregion

var builder = DistributedApplication.CreateBuilder(args);


#region RabbitMQ

var rabbitMqUserName = builder.AddParameter("RABBITMQ-USERNAME");
var rabbitMqPassword = builder.AddParameter("RABBITMQ-PASSWORD");


var rabbitMq = builder.AddRabbitMQ("rabbitMQ", rabbitMqUserName, rabbitMqPassword, 5672).WithManagementPlugin(15672);

#endregion

#region Catalog-API

var mongoUser = builder.AddParameter("MONGO-USERNAME");
var mongoPassword = builder.AddParameter("MONGO-PASSWORD");


var mongoCatalogDb = builder.AddMongoDB("mongo-db-catalog", 27030, mongoUser, mongoPassword).WithImage("mongo:8.0-rc")
    .WithDataVolume("mongo.db.catalog.volume").AddDatabase("catalog-db");

var catalogApi = builder.AddProject<UdemyNewMicroservice_Catalog_Api>("udemynewmicroservice-catalog-api");


catalogApi.WithReference(mongoCatalogDb).WaitFor(mongoCatalogDb).WithReference(rabbitMq).WaitFor(rabbitMq);

#endregion

#region Basket-API

var redisPassword = builder.AddParameter("REDIS-PASSWORD");


var redisBasketDb = builder.AddRedis("redis-db-basket").WithImage("redis:7.0-alpine")
    .WithDataVolume("redis.db.basket.volume").WithPassword(redisPassword);

var basketApi = builder.AddProject<UdemyNewMicroservice_Basket_Api>("udemynewmicroservice-basket-api");

basketApi.WithReference(redisBasketDb).WithReference(rabbitMq).WaitFor(rabbitMq);

#endregion

#region Discount-API

var mongoDiscountDb = builder.AddMongoDB("mongo-db-discount", 27034, mongoUser, mongoPassword).WithImage("mongo:8.0-rc")
    .WithDataVolume("mongo.db.discount.volume").AddDatabase("discount-db");

var discountApi = builder.AddProject<UdemyNewMicroservice_Discount_Api>("udemynewmicroservice-discount-api");

discountApi.WithReference(mongoDiscountDb).WaitFor(mongoDiscountDb).WithReference(rabbitMq).WaitFor(rabbitMq);

#endregion

#region File-API

var fileApi = builder.AddProject<UdemyNewMicroservice_File_Api>("udemynewmicroservice-file-api");
fileApi.WithReference(rabbitMq).WaitFor(rabbitMq);

#endregion

#region Payment-API

var paymentApi = builder.AddProject<UdemyNewMicroservice_Payment_Api>("udemynewmicroservice-payment-api");
paymentApi.WithReference(rabbitMq).WaitFor(rabbitMq);

#endregion

#region Order-API

var sqlserverPassword = builder.AddParameter("SQLSERVER-SA-PASSWORD");


var sqlserverOrderDb = builder.AddSqlServer("sqlserver-db-order").WithPassword(sqlserverPassword)
    .WithDataVolume("sqlserver.db.order.volume").AddDatabase("order-db-aspire");

var orderApi = builder.AddProject<UdemyNewMicroservice_Order_Api>("udemynewmicroservice-order-api");

orderApi.WithReference(sqlserverOrderDb).WaitFor(sqlserverOrderDb).WithReference(rabbitMq).WaitFor(rabbitMq);

#endregion

#region Gateway-API

builder.AddProject<UdemyNewMicroservice_Gateway>("udemynewmicroservice-gateway");

#endregion

#region Web

var web = builder.AddProject<UdemyNewMicroservice_Web>("udemynewmicroservice-web");
web.WithReference(basketApi).WithReference(catalogApi).WithReference(discountApi).WithReference(orderApi)
    .WithReference(fileApi).WithReference(paymentApi);

#endregion


builder.Build().Run();