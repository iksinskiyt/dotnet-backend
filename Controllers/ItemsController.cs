using Microsoft.AspNetCore.Mvc;
using Project.Dtos;
using Project.Entities;
using Project.Repositories;

namespace Project.Controllers
{
    [Route("[controller]")]
    public class ItemsController : ControllerBase{
        private readonly IItemsRepository repository;
        
        public ItemsController(IItemsRepository repository){
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<ItemDto>> GetItemsAsync(){
            var items = (await repository.GetItemsAsync())
                .Select(item => item.AsDto());
            return items;    
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetItemAsync(Guid id){
            var item = await repository.GetItemAsync(id);

            if(item is null){
                return NotFound();
            }

            return item.AsDto();
        }
        [HttpPost]
        public async Task<ActionResult<ItemDto>> CreateItemAsync(CreateItemDto itemDto){
            Item item = new(){
                Id = Guid.NewGuid(),
                Name = itemDto.Name,
                Price = itemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };
            if(item.Name == null){
                return BadRequest();
            }
            if(item.Price == 0){
                return BadRequest();
            }
            await repository.CreateItemAsync(item);

            return CreatedAtAction(nameof(GetItemAsync),new {id = item.Id},item.AsDto());
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItemAsync(Guid id,UpdateItemDto itemDto){
            var existingItem = await repository.GetItemAsync(id);
            if(existingItem is null){
                return NotFound();
            }

            Item updatedItem = existingItem with {
                Name = itemDto.Name,
                Price = itemDto.Price
            };

            await repository.UpdateItemAsync(updatedItem);

            return NoContent();
        }

        [HttpDelete("id")]
        public async Task<ActionResult> DeleteItemAsync(Guid id){
            var existingItem = repository.GetItemAsync(id);
            if(existingItem is null){
                return NotFound();
            }
            await repository.DeleteItemAsync(id);
            return NoContent();
        }
    }
}