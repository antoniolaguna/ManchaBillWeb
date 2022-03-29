using ManchaBillWeb.Models;
using ManchaBillWeb.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ManchaBillWeb.Services.Implements
{
    public class ItemTypeService:GenericService<ItemType>,IItemTypeService
    {
        private readonly IItemTypeRepository itemTypeRepository;
        public ItemTypeService(IItemTypeRepository itemTypeRepository) : base(itemTypeRepository)
        {
            this.itemTypeRepository = itemTypeRepository;
        }

        public Task<List<ItemType>> GetAllItemTypesActives()
        {
            return this.itemTypeRepository.GetAllItemTypesActives();
        }

        public Task LogicDelete(int id)
        {
            return this.itemTypeRepository.LogicDelete(id);
        }
    }
}
