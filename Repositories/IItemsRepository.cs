using Project.Entities;

namespace Project.Repositories{
        public interface IItemsRepository{
            Task<Item> GetItemAsync(Guid id);
            Task<IEnumerable<Item>> GetItemsAsync();

            Task CreateItemAsync(Item item);

            Task UpdateItemAsync(Item item);
            Task DeleteItemAsync(Guid id);
    }
}