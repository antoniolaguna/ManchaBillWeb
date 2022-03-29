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
    public class ModelsController : Controller
    {
        private readonly IModelService modelService;
        private readonly IItemService itemService;
        private readonly ISizeService sizeService;

        private readonly IMapper mapper;

        public ModelsController(IModelService modelService,IItemService itemService,ISizeService sizeService, IMapper mapper)
        {
            this.modelService = modelService;
            this.itemService = itemService;
            this.sizeService = sizeService;
            this.mapper = mapper;
        }

        // GET: Models
        public async Task<IActionResult> Index()
        {

            List<Model> models = await modelService.GetAllModelsActives();
            List<ModelDTO> modelsDTO = models.Select(x => mapper.Map<ModelDTO>(x)).ToList();
            return View(modelsDTO);
        }

        public JsonResult GetSizesAvailibleByItemId(int itemID)
        {
            Item item = itemService.GetById(itemID).Result;
            int sizeTypeID = item.ItemTypeId;
            var data = sizeService.GetSizesBySizeType(sizeTypeID).Result;
            return Json(data);

        }

        // GET: Models/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await this.modelService.GetById(id.Value);

            if (model == null)
            {
                return NotFound();
            }

            ModelDTO modelDTO = mapper.Map<ModelDTO>(model);
            return View(modelDTO);
        }

        // GET: Models/Create
        public async Task<IActionResult> Create()
        {
            List<Item> item = await this.itemService.GetAllItemsActives();
            List<ItemDTO> itemDTOs = item.Select(x => mapper.Map<ItemDTO>(x)).ToList();
            //ViewBag.Items = itemDTOs;
            ViewData["ItemId"] = new SelectList(itemDTOs, "Id", "ShortDescription");
            return View();
        }

       

        // POST: Models/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Colour,CountStore,CountSold,ItemId,Active,DeleteDate,CreatedDate,UpdatedDate,Comments")] ModelDTO modelDTO)
        {
            if (ModelState.IsValid)
            {
                Model model = this.mapper.Map<Model>(modelDTO);
                this.modelService.Insert(model);
                return RedirectToAction(nameof(Index));
            }
            return View(modelDTO);
        }

        // GET: Models/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Model model = await this.modelService.GetById(id.Value);

            if (model == null)
            {
                return NotFound();
            }

            ModelDTO modelDTO = this.mapper.Map<ModelDTO>(model);
            return View(modelDTO);
        }

        // POST: Models/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Colour,CountStore,CountSold,ItemId,Active,DeleteDate,CreatedDate,UpdatedDate,Comments")] ModelDTO modelDTO)
        {
            if (id != modelDTO.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Model model = this.mapper.Map<Model>(modelDTO);
                    this.modelService.Update(model);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModelExists(modelDTO.Id).Result)
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
            return View(modelDTO);
        }

        // GET: Models/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await this.modelService.GetById(id.Value);

            if (item == null)
            {
                return NotFound();
            }
            ModelDTO modelDTO = this.mapper.Map<ModelDTO>(item);
            return View(modelDTO);
        }

        // POST: Models/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this.modelService.LogicDelete(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ModelExists(int id)
        {
            return await this.itemService.Exists(id);
        }
    }
}
