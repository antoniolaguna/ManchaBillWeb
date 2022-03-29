﻿using ManchaBillWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManchaBillWeb.Repositories
{
    public interface IItemRepository : IGenericRepository<Item>
    {
        Task<List<Item>> GetAllItemsActives();
        Task LogicDelete(int id);
        Item GetByIdWithAllRelationships(int id);
    }
}
