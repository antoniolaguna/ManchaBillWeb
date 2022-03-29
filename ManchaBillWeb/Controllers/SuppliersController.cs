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
    public class SuppliersController : Controller
    {
        private readonly ISupplierService supplierService;
        private readonly IMapper mapper;

        public SuppliersController(ISupplierService supplierService, IMapper mapper)
        {
            this.supplierService = supplierService;
            this.mapper = mapper;
        }

        // GET: Suppliers
        public async Task<IActionResult> Index()
        {
            List<Supplier> supplier = await supplierService.GetAllSuppliersActives();
            List<SupplierDTO> supplierDTOs = supplier.Select(x => mapper.Map<SupplierDTO>(x)).ToList();
            return View(supplierDTOs);
        }

        // GET: Suppliers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            Supplier supplier = await supplierService.GetById(id.Value);

            if (supplier == null)
            {
                return NotFound();
            }

            SupplierDTO supplierDTO = mapper.Map<SupplierDTO>(supplier);
            return View(supplierDTO);
        }

        // GET: Suppliers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Suppliers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Description,Address,EMail,Phone,Fax,Active,DeleteDate,CreatedDate,UpdatedDate,Comments")] SupplierDTO supplierDTO)
        {
            if (ModelState.IsValid)
            {
                Supplier supplier = mapper.Map<Supplier>(supplierDTO);
                this.supplierService.Insert(supplier);
                return RedirectToAction(nameof(Index));
            }
            return View(supplierDTO);
        }

        // GET: Suppliers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Supplier supplier = await this.supplierService.GetById(id.Value);
            if (supplier == null)
            {
                return NotFound();
            }

            SupplierDTO supplierDTO = mapper.Map<SupplierDTO>(supplier);
            return View(supplierDTO);
        }

        // POST: Suppliers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Description,Address,EMail,Phone,Fax,Active,DeleteDate,CreatedDate,UpdatedDate,Comments")] SupplierDTO supplierDTO)
        {
            if (id != supplierDTO.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Supplier supplier = this.mapper.Map<Supplier>(supplierDTO);
                    this.supplierService.Update(supplier);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SupplierExists(supplierDTO.Id).Result)
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
            return View(supplierDTO);
        }

        // GET: Suppliers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Supplier supplier = await this.supplierService.GetById(id.Value);

            if (supplier == null)
            {
                return NotFound();
            }

            SupplierDTO supplierDTO = mapper.Map<SupplierDTO>(supplier);
            return View(supplierDTO);
        }

        // POST: Suppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this.supplierService.LogicDelete(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> SupplierExists(int id)
        {
            return await this.supplierService.Exists(id);
        }
    }
}
