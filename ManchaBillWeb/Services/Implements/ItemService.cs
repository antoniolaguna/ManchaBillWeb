using ManchaBillWeb.Models;
using ManchaBillWeb.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManchaBillWeb.Services.Implements
{
    public class ItemService:GenericService<Item>,IItemService
    {
        private readonly IItemRepository itemRepository;
        public ItemService(IItemRepository itemRepository) : base(itemRepository)
        {
            this.itemRepository = itemRepository;
        }

        public Task<List<Item>> GetAllItemsActives()
        {
            return this.itemRepository.GetAllItemsActives();
        }

        public Task LogicDelete(int id)
        {
            return this.itemRepository.LogicDelete(id);
        }

        public Item GetByIdWithAllRelationships(int id)
        {
            return this.itemRepository.GetByIdWithAllRelationships(id);
        }
    }
}
