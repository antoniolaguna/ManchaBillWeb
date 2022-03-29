using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using ManchaBillWeb.Services;
using ManchaBillWeb.Models;
using ManchaBillWeb.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace ManchaBillWeb.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class SizeTypesController : Controller
    {
        private readonly ISizeTypeService sizeTypeService;
        private readonly IMapper mapper;

        public SizeTypesController(ISizeTypeService sizeTypeService, IMapper mapper)
        {
            this.sizeTypeService = sizeTypeService;
            this.mapper = mapper;
        }

        // GET: SizeTypes
        public async Task<IActionResult> Index()
        {
            List<SizeType> sizeTypes = await sizeTypeService.GetAllSizeTypesActives();
            List<SizeTypeDTO> sizeTypesDTO = sizeTypes.Select(x => mapper.Map<SizeTypeDTO>(x)).ToList();
            return View(sizeTypesDTO);
        }

        // GET: SizeTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            SizeType sizeType = await sizeTypeService.GetById(id.Value);

            if (sizeType == null)
            {
                return NotFound();
            }

            SizeTypeDTO sizeTypeDTO = mapper.Map<SizeTypeDTO>(sizeType);
            return View(sizeTypeDTO);
        }

        // GET: SizeTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SizeTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Create([Bind("Id,Description,Active,DeleteDate,CreatedDate,UpdatedDate,Comments")] SizeTypeDTO sizeTypeDTO)
        {
            if (ModelState.IsValid)
            {
                SizeType sizeType = mapper.Map<SizeType>(sizeTypeDTO);
                this.sizeTypeService.Insert(sizeType);
                return RedirectToAction(nameof(Index));
            }
            return View(sizeTypeDTO);
        }

        // GET: SizeTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SizeType sizeType = await this.sizeTypeService.GetById(id.Value);
            if (sizeType == null)
            {
                return NotFound();
            }

            SizeTypeDTO sizeTypeDTO = mapper.Map<SizeTypeDTO>(sizeType);
            return View(sizeTypeDTO);

        }

        // POST: SizeTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Description,Active,DeleteDate,CreatedDate,UpdatedDate,Comments")] SizeTypeDTO sizeTypeDTO)
        {
            if (id != sizeTypeDTO.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    SizeType sizeType = this.mapper.Map<SizeType>(sizeTypeDTO);
                    this.sizeTypeService.Update(sizeType);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SizeTypeExists(sizeTypeDTO.Id).Result)
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
            return View(sizeTypeDTO);
        }

        // GET: SizeTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SizeType sizeType = await this.sizeTypeService.GetById(id.Value);

            if (sizeType == null)
            {
                return NotFound();
            }

            SizeTypeDTO sizeTypeDTO = mapper.Map<SizeTypeDTO>(sizeType);
            return View(sizeTypeDTO);
        }

        // POST: SizeTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this.sizeTypeService.LogicDelete(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> SizeTypeExists(int id)
        {
            return await this.sizeTypeService.Exists(id);
        }
    }
}
