var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.UdemyNewMicroservice_Basket_Api>("udemynewmicroservice-basket-api");

builder.AddProject<Projects.UdemyNewMicroservice_Catalog_Api>("udemynewmicroservice-catalog-api");

builder.AddProject<Projects.UdemyNewMicroservice_Discount_Api>("udemynewmicroservice-discount-api");

builder.AddProject<Projects.UdemyNewMicroservice_File_Api>("udemynewmicroservice-file-api");

builder.AddProject<Projects.UdemyNewMicroservice_Gateway>("udemynewmicroservice-gateway");

builder.AddProject<Projects.UdemyNewMicroservice_Order_Api>("udemynewmicroservice-order-api");

builder.AddProject<Projects.UdemyNewMicroservice_Payment_Api>("udemynewmicroservice-payment-api");

builder.AddProject<Projects.UdemyNewMicroservice_Web>("udemynewmicroservice-web");

builder.Build().Run();
