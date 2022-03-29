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
    public class SizesController : Controller
    {
        private readonly ISizeService sizeService;
        private readonly ISizeTypeService sizeTypeService;
        private readonly IMapper mapper;
        public SizesController(ISizeService sizeService, ISizeTypeService sizeTypeService, IMapper mapper)
        {
            this.sizeService = sizeService;
            this.sizeTypeService = sizeTypeService;
            this.mapper = mapper;
        }

        // GET: Sizes
        public async Task<IActionResult> Index()
        {
            List<Size> sizes = await sizeService.GetAllSizesActives();
            List<SizeDTO> sizeDTO = sizes.Select(x => mapper.Map<SizeDTO>(x)).ToList();
            return View(sizeDTO);
        }

        // GET: Sizes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var size = await this.sizeService.GetById(id.Value);

            if (size == null)
            {
                return NotFound();
            }

            var sizeType = await this.sizeTypeService.GetById(size.SizeTypeId);

            SizeDTO sizeDTO = mapper.Map<SizeDTO>(size);
            SizeTypeDTO sizeTypeDTO = mapper.Map<SizeTypeDTO>(sizeType);
            sizeDTO.SizeType = sizeTypeDTO;
            return View(sizeDTO);
        }

        // GET: Sizes/Create
        public async Task<IActionResult> Create()
        {
            List<SizeType> sizeTypes = await this.sizeTypeService.GetAllSizeTypesActives();
            List<SizeTypeDTO> sizeTypeDTOs= sizeTypes.Select(x => mapper.Map<SizeTypeDTO>(x)).ToList();


            ViewData["SizeTypeId"] = new SelectList(sizeTypeDTOs, "Id", "Description");
            return View();
        }


        // POST: Sizes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Description,SizeTypeId,Active,DeleteDate,CreatedDate,UpdatedDate,Comments")] SizeDTO sizeDTO)
        {
            if (ModelState.IsValid)
            {
                Size size = this.mapper.Map<Size>(sizeDTO);
                this.sizeService.Insert(size);
                return RedirectToAction(nameof(Index));
            }
            return View(sizeDTO);
        }

        // GET: Sizes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Size size = await this.sizeService.GetById(id.Value);

            if (size == null)
            {
                return NotFound();
            }
            List<SizeType> sizeTypes = await this.sizeTypeService.GetAllSizeTypesActives();
            List<SizeTypeDTO> sizeTypeDTOs = sizeTypes.Select(x => mapper.Map<SizeTypeDTO>(x)).ToList();


            ViewData["SizeTypeId"] = new SelectList(sizeTypeDTOs, "Id", "Description");
            SizeDTO sizeDTO = this.mapper.Map<SizeDTO>(size);
            return View(sizeDTO);
        }

        // POST: Sizes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Description,SizeTypeId,Active,DeleteDate,CreatedDate,UpdatedDate,Comments")] SizeDTO sizeDTO)
        {
            if (id != sizeDTO.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Size size = this.mapper.Map<Size>(sizeDTO);
                    this.sizeService.Update(size);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SizeExists(sizeDTO.Id).Result)
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
            return View(sizeDTO);
        }

        // GET: Sizes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var size = await this.sizeService.GetById(id.Value);

            if (size == null)
            {
                return NotFound();
            }
            SizeDTO sizeDTO = this.mapper.Map<SizeDTO>(size);
            return View(sizeDTO);
        }

        // POST: Sizes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this.sizeService.LogicDelete(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> SizeExists(int id)
        {
            return await this.sizeService.Exists(id);
        }
    }
}
