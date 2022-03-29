using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using ManchaBillWeb.Services;
using ManchaBillWeb.Models;
using ManchaBillWeb.DTOs;

namespace ManchaBillWeb.Controllers
{
    public class SeasonsController : Controller
    {
        private readonly ISeasonService seasonService;
        private readonly IMapper mapper;

        public SeasonsController(ISeasonService seasonService, IMapper mapper)
        {
            this.seasonService = seasonService;
            this.mapper = mapper;
        }

        // GET: Seasons
        public async Task<IActionResult> Index()
        {
            List<Season> season=await seasonService.GetAllSeasonsActives();
            List<SeasonDTO> seasonDTOs= season.Select(x => mapper.Map<SeasonDTO>(x)).ToList();
            return View(seasonDTOs);
        }

        // GET: Seasons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            Season season = await seasonService.GetById(id.Value);

            if (season == null)
            {
                return NotFound();
            }

            SeasonDTO seasonDTO = mapper.Map<SeasonDTO>(season);
            return View(seasonDTO);
        }

        // GET: Seasons/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Seasons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Description,Active,DeleteDate,CreatedDate,UpdatedDate,Comments")] SeasonDTO seasonDTO)
        {
            if (ModelState.IsValid)
            {
                Season season = mapper.Map<Season>(seasonDTO);
                this.seasonService.Insert(season);
                return RedirectToAction(nameof(Index));
            }
            return View(seasonDTO);
        }

        // GET: Seasons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Season season = await this.seasonService.GetById(id.Value);
            if (season == null)
            {
                return NotFound();
            }

            SeasonDTO seasonDTO = mapper.Map<SeasonDTO>(season);
            return View(seasonDTO);
        }

        // POST: Seasons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Description,Active,DeleteDate,CreatedDate,UpdatedDate,Comments")] SeasonDTO seasonDTO)
        {
            if (id != seasonDTO.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Season season = this.mapper.Map<Season>(seasonDTO);
                    this.seasonService.Update(season);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SeasonExists(seasonDTO.Id).Result)
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
            return View(seasonDTO);
        }

        // GET: Seasons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Season season = await this.seasonService.GetById(id.Value);

            if (season == null)
            {
                return NotFound();
            }

            SeasonDTO seasonDTO = mapper.Map<SeasonDTO>(season);
            return View(seasonDTO);
        }

        // POST: Seasons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this.seasonService.LogicDelete(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> SeasonExists(int id)
        {
            return await this.seasonService.Exists(id);
        }
    }
}
