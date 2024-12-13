using Microsoft.Extensions.Hosting;
using MongoDB.Driver;

namespace FlowerSales.API.Models
{
    public static class ModelBuilderExtension
    {
        private static Random _random = new Random();
        public static void SeedData(ShopContext context)
        {
            SeedProducts(context);
        }

        private static int GenerateUniqueRandomId(ShopContext context)
        {
            int newId;
            var filter = Builders<Product>.Filter;
            do
            {
                newId = _random.Next(1, int.MaxValue);
            } while (context.Products.Find(filter.Eq(p => p.Id, newId)).Any());

            return newId;
        }

        private static void SeedProducts(ShopContext context)
        {
            if (context.Products.Find(_ => true).FirstOrDefault() == null)
            {
                context.Products.InsertMany(new[]
                {
                    new Product { Id = GenerateUniqueRandomId(context), CategoryName = "Bouquetes", Name = "Flowers in the city", StoreLocation = "Canning Vale", PostCode = 6155, Price = 68, IsAvailable = true },
                    new Product { Id = GenerateUniqueRandomId(context), CategoryName = "Bouquetes", Name = "Gerberas", StoreLocation = "Willeton", PostCode = 6155, Price = 35, IsAvailable = true },
                    new Product { Id = GenerateUniqueRandomId(context), CategoryName = "Bouquetes", Name = "Aziatic Lilies", StoreLocation = "Palmyra", PostCode = 6123, Price = 33, IsAvailable = true },
                    new Product { Id = GenerateUniqueRandomId(context), CategoryName = "Bouquetes", Name = "European Lilies", StoreLocation = "Melville", PostCode = 6145, Price = 125, IsAvailable = true },
                    new Product { Id = GenerateUniqueRandomId(context), CategoryName = "Bouquetes", Name = "Chrisantemum", StoreLocation = "Canninghton", PostCode = 6112, Price = 60, IsAvailable = true },
                    new Product { Id = GenerateUniqueRandomId(context), CategoryName = "Bouquetes", Name = "Alstroemeria", StoreLocation = "Waikiki", PostCode = 6112, Price = 95, IsAvailable = true },
                    new Product { Id = GenerateUniqueRandomId(context), CategoryName = "Bouquetes", Name = "Snapdragon small", StoreLocation = "Tuart Hill", PostCode = 6112, Price = 65, IsAvailable = true },
                    new Product { Id = GenerateUniqueRandomId(context), CategoryName = "Bouquetes", Name = "V-Crocus", StoreLocation = "Willeton", PostCode = 6113, Price = 65, IsAvailable = true },
                    new Product { Id = GenerateUniqueRandomId(context), CategoryName = "Bouquetes", Name = "Crocus", StoreLocation = "Armadale", PostCode = 6114, Price = 17, IsAvailable = true },
                    new Product { Id = GenerateUniqueRandomId(context), CategoryName = "Box Flowers", Name = "Calla Lily", StoreLocation = "Aubin Grove", PostCode = 6115, Price = 99, IsAvailable = true },
                    new Product { Id = GenerateUniqueRandomId(context), CategoryName = "Box Flowers", Name = "Geranium small", StoreLocation = "Darch", PostCode = 6116, Price = 0, IsAvailable = false },
                    new Product { Id = GenerateUniqueRandomId(context), CategoryName = "Box Flowers", Name = "Grunge Skater Jeans", StoreLocation = "Jonndana", PostCode = 6117, Price = 68, IsAvailable = true },
                    new Product { Id = GenerateUniqueRandomId(context), CategoryName = "Box Flowers", Name = "Geranium Large", StoreLocation = "Joonedaloop", PostCode = 6112, Price = 125, IsAvailable = true },
                    new Product { Id = GenerateUniqueRandomId(context), CategoryName = "Box Flowers", Name = "Stretchy Dance Pants", StoreLocation = "GEralton", PostCode = 6118, Price = 55, IsAvailable = true },
                    new Product { Id = GenerateUniqueRandomId(context), CategoryName = "Box Flowers", Name = "Alstroemeria", StoreLocation = "Piara Waters", PostCode = 6121, Price = 22, IsAvailable = false },
                    new Product { Id = GenerateUniqueRandomId(context), CategoryName = "Box Flowers", Name = "Gerberas", StoreLocation = "Byford", PostCode = 6132, Price = 95, IsAvailable = true },
                    new Product { Id = GenerateUniqueRandomId(context), CategoryName = "Box Flowers", Name = "Marigold", StoreLocation = "Dianella", PostCode = 6342, Price = 17, IsAvailable = true },
                    new Product { Id = GenerateUniqueRandomId(context), CategoryName = "Wrapps", Name = "Azalea", StoreLocation = "Leong", PostCode = 6123, Price = 2.8M, IsAvailable = true },
                    new Product { Id = GenerateUniqueRandomId(context), CategoryName = "Wrapps", Name = "Lemon-LAzalea", StoreLocation = "Fremantle", PostCode = 6124, Price = 2.8M, IsAvailable = true },
                    new Product { Id = GenerateUniqueRandomId(context), CategoryName = "Wrapps", Name = "Zinnia", StoreLocation = "BEaconsfield", PostCode = 6125, Price = 2.8M, IsAvailable = false },
                    new Product { Id = GenerateUniqueRandomId(context), CategoryName = "Wrapps", Name = "Peach Zinnia", StoreLocation = "North Freo", PostCode = 6126, Price = 2.8M, IsAvailable = true },
                    new Product { Id = GenerateUniqueRandomId(context), CategoryName = "Wrapps", Name = "Raspberry Zinnia", StoreLocation = "Munster", PostCode = 6127, Price = 2.8M, IsAvailable = true },
                    new Product { Id = GenerateUniqueRandomId(context), CategoryName = "Wrapps", Name = "Snapdragon big", StoreLocation = "Coogee", PostCode = 6128, Price = 2.8M, IsAvailable = true },
                    new Product { Id = GenerateUniqueRandomId(context), CategoryName = "Single Flower", Name = "Petunia", StoreLocation = "South Freo", PostCode = 6129, Price = 24.99M, IsAvailable = true },
                    new Product { Id = GenerateUniqueRandomId(context), CategoryName = "Additional", Name = "Dahlia (long lasting)", StoreLocation = "City", PostCode = 6112, Price = 9.99M, IsAvailable = true },
                    new Product { Id = GenerateUniqueRandomId(context), CategoryName = "Additional", Name = "Dahlia", StoreLocation = "West Perth", PostCode = 6130, Price = 12.49M, IsAvailable = true },
                    new Product { Id = GenerateUniqueRandomId(context), CategoryName = "Additional", Name = "Orchid domestic", StoreLocation = "East Perth", PostCode = 6131, Price = 13.99M, IsAvailable = true },
                    new Product { Id = GenerateUniqueRandomId(context), CategoryName = "Additional", Name = "Orchid Expensive", StoreLocation = "Bentley", PostCode = 6132, Price = 12.49M, IsAvailable = true },
                    new Product { Id = GenerateUniqueRandomId(context), CategoryName = "Additional", Name = "Marigold", StoreLocation = "Carslie", PostCode = 6133, Price = 9.99M, IsAvailable = true },
                    new Product { Id = GenerateUniqueRandomId(context), CategoryName = "Additional", Name = "Gardenia type C", StoreLocation = "Lathlain", PostCode = 6134, Price = 11.99M, IsAvailable = true },
                    new Product { Id = GenerateUniqueRandomId(context), CategoryName = "Additional", Name = "Gardenia type-B", StoreLocation = "Booragoon", PostCode = 6135, Price = 12.99M, IsAvailable = true },
                    new Product { Id = GenerateUniqueRandomId(context), CategoryName = "Additional", Name = "Gardenia", StoreLocation = "Applecross", PostCode = 6136, Price = 9.99M, IsAvailable = true },
                    new Product { Id = GenerateUniqueRandomId(context), CategoryName = "Additional", Name = "Calla Lily", StoreLocation = "Rockyngham", PostCode = 6001, Price = 12.49M, IsAvailable = true }
                });
            }
        }

        public static int GenerateUniqueRandomIdForNewProduct(ShopContext context)
        {
            return GenerateUniqueRandomId(context);
        }
    }
}