using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using ItemApi.Models;

namespace ItemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private static List<Item> Items = new List<Item>
        {
            new Item { Id = 1, Name = "Sample Item 1", Description = "This is a sample item." },
            new Item { Id = 2, Name = "Sample Item 2", Description = "This is another sample item." }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Item>> GetItems()
        {
            return Items;
        }

        [HttpGet("{id}")]
        public ActionResult<Item> GetItem(int id)
        {
            var item = Items.FirstOrDefault(i => i.Id == id);
            if (item == null) return NotFound();
            return item;
        }

        [HttpPost]
        public ActionResult<Item> CreateItem(Item item)
        {
            item.Id = Items.Count > 0 ? Items.Max(i => i.Id) + 1 : 1;
            Items.Add(item);
            return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateItem(int id, Item item)
        {
            var existingItem = Items.FirstOrDefault(i => i.Id == id);
            if (existingItem == null) return NotFound();

            existingItem.Name = item.Name;
            existingItem.Description = item.Description;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteItem(int id)
        {
            var item = Items.FirstOrDefault(i => i.Id == id);
            if (item == null) return NotFound();

            Items.Remove(item);
            return NoContent();
        }
    }
}
