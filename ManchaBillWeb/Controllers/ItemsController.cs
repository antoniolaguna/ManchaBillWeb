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
using Microsoft.AspNetCore.Authorization;

namespace ManchaBillWeb.Controllers
{
    public class ItemsController : Controller
    {
        private readonly IItemService itemService;
        private readonly IItemTypeService itemTypeService;
        private readonly ISupplierService iSupplierService;
        private readonly ISeasonService iSeasonService;
        private readonly ISizeService iSizeService;
        private readonly IModelService iModelService;
        private readonly IMapper mapper;
        public static List<ModelDTO> models = new List<ModelDTO> { };
        public static List<ModelDTO> modelsToDelete = new List<ModelDTO> { };
        public ItemsController(IItemService itemService, IItemTypeService itemTypeService, ISupplierService iSupplierService, ISeasonService iSeasonService, ISizeService iSizeService, IModelService iModelService, IMapper mapper)
        {
            this.itemService = itemService;
            this.itemTypeService = itemTypeService;
            this.iSupplierService = iSupplierService;
            this.iSeasonService = iSeasonService;
            this.iSizeService = iSizeService;
            this.iModelService = iModelService;
            this.mapper = mapper;
        }

        // GET: Items
      
        public async Task<IActionResult> Index()

        {
            models = new List<ModelDTO>();
            modelsToDelete = new List<ModelDTO>();
            List<Item> items = await itemService.GetAllItemsActives();
            List<ItemDTO> itemDTO = items.Select(x => mapper.Map<ItemDTO>(x)).ToList();
            return View(itemDTO);
        }

        // GET: Items/Details/5
        public IActionResult Details(int? id)
        {
            models = new List<ModelDTO>();
            modelsToDelete = new List<ModelDTO>();

            if (id == null)
            {
                return NotFound();
            }

            Item item =  this.itemService.GetByIdWithAllRelationships(id.Value);

            if (item == null)
            {
                return NotFound();
            }


            ItemDTO itemDTO = mapper.Map<ItemDTO>(item);

           

            return View(itemDTO);
        }

        // GET: Items/Create
        public async Task<IActionResult> Create()
        {
            models = new List<ModelDTO>();
            modelsToDelete = new List<ModelDTO>();

            List<ItemType> itemTypes = await this.itemTypeService.GetAllItemTypesActives();
            List<ItemTypeDTO> itemTypeDTOs = itemTypes.Select(x => mapper.Map<ItemTypeDTO>(x)).ToList();
            ViewData["ItemTypeId"] = new SelectList(itemTypeDTOs, "Id", "Description");

            List<Supplier> suppliers = await this.iSupplierService.GetAllSuppliersActives();
            List<SupplierDTO> suppliersDTO = suppliers.Select(x => mapper.Map<SupplierDTO>(x)).ToList();
            ViewData["SupplierId"] = new SelectList(suppliersDTO, "Id", "Description");

            List<Season> seasons = await this.iSeasonService.GetAllSeasonsActives();
            List<SeasonDTO> seasonsDTO = seasons.Select(x => mapper.Map<SeasonDTO>(x)).ToList();
            ViewData["SeasonId"] = new SelectList(seasonsDTO, "Id", "Description");

            return View();
        }


        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ShortDescription,LongDescription,CostPrize,Value,Tax,Discount,Prize,Barcode,ItemTypeId,SupplierId,SeasonId,Active,DeleteDate,CreatedDate,UpdatedDate,Comments")] ItemDTO itemDTO)
        {
            if (ModelState.IsValid)
            {
                Item item = this.mapper.Map<Item>(itemDTO);
                GetModels(item);
                Item itemResult = this.itemService.Insert(item);
                //InserModels(itemResult);
                return RedirectToAction(nameof(Index));
            }
            List<ItemType> itemTypes = await this.itemTypeService.GetAllItemTypesActives();
            List<ItemTypeDTO> itemTypeDTOs = itemTypes.Select(x => mapper.Map<ItemTypeDTO>(x)).ToList();
            ViewData["ItemTypeId"] = new SelectList(itemTypeDTOs, "Id", "Description");

            List<Supplier> suppliers = await this.iSupplierService.GetAllSuppliersActives();
            List<SupplierDTO> suppliersDTO = suppliers.Select(x => mapper.Map<SupplierDTO>(x)).ToList();
            ViewData["SupplierId"] = new SelectList(suppliersDTO, "Id", "Description");

            List<Season> seasons = await this.iSeasonService.GetAllSeasonsActives();
            List<SeasonDTO> seasonsDTO = seasons.Select(x => mapper.Map<SeasonDTO>(x)).ToList();
            ViewData["SeasonId"] = new SelectList(seasonsDTO, "Id", "Description");

            return View(itemDTO);
        }

        

        private void GetModels(Item item)
        {
            foreach (ModelDTO m in models)
            {
                Model model = this.mapper.Map<Model>(m);
                model.CountSold = 0;
                model.Item = item;
                model.Size = null;
                item.Models.Add(model);
            }


        }

        //AJAX
        public JsonResult FilterSizes(int itemTypeId)
        {

            ItemType itemType = itemTypeService.GetById(itemTypeId).Result;
            List<Size> sizes = iSizeService.GetSizesBySizeType(itemType.SizeTypeId).Result;
            List<SizeDTO> sizesDTO = sizes.Select(x => mapper.Map<SizeDTO>(x)).ToList();
            return Json(sizesDTO);
        }

        //AJAX
        public JsonResult AddTemporalModel(string color, int store, int size,string barcode)
        {
            ModelDTO m = new ModelDTO();
            m.SizeId = size;
            m.Colour = color;
            m.CountStore = store;
            m.Barcode = barcode;
            Size s = iSizeService.GetById(size).Result;
            SizeDTO sizeDTO = mapper.Map<SizeDTO>(s);
            m.Size = sizeDTO;
            models.Add(m);
            return new JsonResult(m);
        }

        public JsonResult DeleteTmpModel(int idRow)
        {
            ModelDTO modelToDelete = models[idRow];
            models.Remove(modelToDelete);
            modelsToDelete.Add(modelToDelete);
            return new JsonResult(null);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            models = new List<ModelDTO>();
            modelsToDelete = new List<ModelDTO>();

            if (id == null)
            {
                return NotFound();
            }

            Item item = this.itemService.GetByIdWithAllRelationships(id.Value);
            models = item.Models.Select(x => mapper.Map<ModelDTO>(x)).ToList();
            if (item == null)
            {
                return NotFound();
            }

            List<ItemType> itemTypes = await this.itemTypeService.GetAllItemTypesActives();
            List<ItemTypeDTO> sizeTypeDTOs = itemTypes.Select(x => mapper.Map<ItemTypeDTO>(x)).ToList();
            ViewData["ItemTypeId"] = new SelectList(sizeTypeDTOs, "Id", "Description");


            List<Supplier> suppliers = await this.iSupplierService.GetAllSuppliersActives();
            List<SupplierDTO> suppliersDTO = suppliers.Select(x => mapper.Map<SupplierDTO>(x)).ToList();
            ViewData["SupplierId"] = new SelectList(suppliersDTO, "Id", "Description");



            List<Season> seasons = await this.iSeasonService.GetAllSeasonsActives();
            List<SeasonDTO> seasonsDTO = seasons.Select(x => mapper.Map<SeasonDTO>(x)).ToList();
            ViewData["SeasonId"] = new SelectList(seasonsDTO, "Id", "Description");

            ItemDTO itemDTO = this.mapper.Map<ItemDTO>(item);
            return View(itemDTO);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,ShortDescription,LongDescription,CostPrize,Value,Tax,Discount,Prize,Barcode,ItemTypeId, SeasonId,SupplierId,Active,DeleteDate,CreatedDate,UpdatedDate,Comments")] ItemDTO itemDTO)
        {
            if (id != itemDTO.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                
                try
                {
                    Item item = this.mapper.Map<Item>(itemDTO);
                    GetModels(item);
                    this.itemService.Update(item);
                    RemoveModelsToRemove();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(itemDTO.Id).Result)
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
            return View(itemDTO);
        }

        private void RemoveModelsToRemove()
        {
            foreach(var a in modelsToDelete)
            {
                iModelService.LogicDelete(a.Id);
            }
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await this.itemService.GetById(id.Value);

            if (item == null)
            {
                return NotFound();
            }
            ItemDTO itemDTO = this.mapper.Map<ItemDTO>(item);
            return View(itemDTO);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this.itemService.LogicDelete(id);
            await this.iModelService.LogicDeleteByItem(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ItemExists(int id)
        {
            return await this.itemService.Exists(id);
        }
    }
}
