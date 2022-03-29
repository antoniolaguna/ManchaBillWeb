using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ManchaBillWeb.Data;
using ManchaBillWeb.Models;
using ManchaBillWeb.Services;
using AutoMapper;
using ManchaBillWeb.DTOs;
using ManchaBillWeb.Services.Implements;
using ManchaBillWeb.Repositories.Implements;

namespace ManchaBillWeb.Controllers
{
    public class ItemTypesController : Controller
    {
        private readonly ISizeTypeService sizeTypeService;
        private readonly IItemTypeService itemTypeService;
        private readonly IMapper mapper;

        public ItemTypesController(ISizeTypeService sizeTypeService, IItemTypeService itemTypeService,IMapper mapper)
        {
            this.sizeTypeService = sizeTypeService;
            this.itemTypeService = itemTypeService;
            this.mapper = mapper;
        }

        // GET: ItemTypes
        public async Task<IActionResult> Index()
        {
            List<ItemType> itemTypes = await itemTypeService.GetAllItemTypesActives();
            List<ItemTypeDTO> itemTypesDTO = itemTypes.Select(x => mapper.Map<ItemTypeDTO>(x)).ToList();
            return View(itemTypesDTO);
        }

        // GET: ItemTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemType = await this.itemTypeService.GetById(id.Value);

            if (itemType == null)
            {
                return NotFound();
            }
            var sizeType = await this.sizeTypeService.GetById(itemType.SizeTypeId);
            SizeTypeDTO sizeTypeDTO = this.mapper.Map<SizeTypeDTO>(sizeType);
            
            ItemTypeDTO itemTypeDTO = mapper.Map<ItemTypeDTO>(itemType);
            itemTypeDTO.SizeType = sizeTypeDTO;
            return View(itemTypeDTO);
        }

        // GET: ItemTypes/Create
        public async Task<IActionResult> Create()
        {
            List<SizeType> sizeTypes = await this.sizeTypeService.GetAllSizeTypesActives();
            List<SizeTypeDTO> sizeTypeDTOs = sizeTypes.Select(x => mapper.Map<SizeTypeDTO>(x)).ToList();


            ViewData["SizeTypeId"] = new SelectList(sizeTypeDTOs, "Id", "Description");
            return View();
        }

        // POST: ItemTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Description,SizeTypeId,Active,DeleteDate,CreatedDate,UpdatedDate,Comments")] ItemTypeDTO itemTypeDTO)
        {
            if (ModelState.IsValid)
            {
                ItemType itemType = this.mapper.Map<ItemType>(itemTypeDTO);
                this.itemTypeService.Insert(itemType);
                return RedirectToAction(nameof(Index));
            }
            return View(itemTypeDTO);
        }

        // GET: ItemTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ItemType itemType = await this.itemTypeService.GetById(id.Value);

            if (itemType == null)
            {
                return NotFound();
            }
            List<SizeType> sizeTypes = await this.sizeTypeService.GetAllSizeTypesActives();
            List<SizeTypeDTO> sizeTypeDTOs = sizeTypes.Select(x => mapper.Map<SizeTypeDTO>(x)).ToList();


            ViewData["SizeTypeId"] = new SelectList(sizeTypeDTOs, "Id", "Description");

            ItemTypeDTO itemTypeDTO = this.mapper.Map<ItemTypeDTO>(itemType);
            return View(itemTypeDTO);
        }

        // POST: ItemTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Edit(int id, [Bind("Id,Description,SizeTypeId,Active,DeleteDate,CreatedDate,UpdatedDate,Comments")] ItemTypeDTO itemTypeDTO)
        {
            if (id != itemTypeDTO.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    ItemType itemType = this.mapper.Map<ItemType>(itemTypeDTO);
                    this.itemTypeService.Update(itemType);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemTypeExists(itemTypeDTO.Id).Result)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(itemTypeDTO);
        }

        // GET: ItemTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemType = await this.itemTypeService.GetById(id.Value);

            if (itemType == null)
            {
                return NotFound();
            }

            
            ItemTypeDTO itemTypeDTO = this.mapper.Map<ItemTypeDTO>(itemType);
            
            return View(itemTypeDTO);
        }

        // POST: ItemTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this.itemTypeService.LogicDelete(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ItemTypeExists(int id)
        {
            return await this.itemTypeService.Exists(id);
        }
    }
}
