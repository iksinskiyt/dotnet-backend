using System;

namespace Project.Entities
{
    public record Item
    {
        public Guid Id {get;init;}
        public string Name {get; init;}
        public decimal Price {get;init;}
        public DateTimeOffset CreatedDate {get;init;}
    }

}