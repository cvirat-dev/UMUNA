namespace Umuna.Core.Tests.Mocking
{
    public class MockData
    {
        public User[] users { get; set; }
        public Product[] products { get; set; }
        public Metadata metadata { get; set; }
    }

    public class Metadata
    {
        public double version { get; set; }
        public string lastUpdated { get; set; }
        public int totalRecords { get; set; }
    }

    public class User
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public int age { get; set; }
        public bool isActive { get; set; }
    }

    public class Product
    {
        public int id { get; set; }
        public string name { get; set; }
        public float price { get; set; }
        public string category { get; set; }
        public bool inStock { get; set; }
    }

}

