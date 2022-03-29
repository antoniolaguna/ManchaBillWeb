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
    public class PaymentTypesController : Controller
    {
        private readonly IPaymentTypeService paymentTypeService;
        private readonly IMapper mapper;

        public PaymentTypesController(IPaymentTypeService paymentTypeService, IMapper mapper)
        {
            this.paymentTypeService = paymentTypeService;
            this.mapper = mapper;
        }

        // GET: PaymentTypes
        public async Task<IActionResult> Index()
        {
            List<PaymentType> paymentTypes = await paymentTypeService.GetAllPaymentTypesActives();
            List<PaymentTypeDTO> paymentTypesDTO = paymentTypes.Select(x => mapper.Map<PaymentTypeDTO>(x)).ToList();
            return View(paymentTypesDTO);
        }

        // GET: PaymentTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            PaymentType paymentType = await paymentTypeService.GetById(id.Value);

            if (paymentType == null)
            {
                return NotFound();
            }

            PaymentTypeDTO paymentTypeDTO = mapper.Map<PaymentTypeDTO>(paymentType);
            return View(paymentTypeDTO);
        }

        // GET: PaymentTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PaymentTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Description,Active,DeleteDate,CreatedDate,UpdatedDate,Comments")] PaymentTypeDTO paymentTypeDTO)
        {
            if (ModelState.IsValid)
            {
                PaymentType paymentType = mapper.Map<PaymentType>(paymentTypeDTO);
                this.paymentTypeService.Insert(paymentType);
                return RedirectToAction(nameof(Index));
            }
            return View(paymentTypeDTO);
        }

        // GET: PaymentTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PaymentType paymentType = await this.paymentTypeService.GetById(id.Value);
            if (paymentType == null)
            {
                return NotFound();
            }

            PaymentTypeDTO paymentTypeDTO = mapper.Map<PaymentTypeDTO>(paymentType);
            return View(paymentTypeDTO);

        }

        // POST: PaymentTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Description,Active,DeleteDate,CreatedDate,UpdatedDate,Comments")] PaymentTypeDTO paymentTypeDTO)
        {
            if (id != paymentTypeDTO.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    PaymentType paymentType = this.mapper.Map<PaymentType>(paymentTypeDTO);
                    this.paymentTypeService.Update(paymentType);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentTypeExists(paymentTypeDTO.Id).Result)
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
            return View(paymentTypeDTO);
        }

        // GET: PaymentTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PaymentType paymentType = await this.paymentTypeService.GetById(id.Value);

            if (paymentType == null)
            {
                return NotFound();
            }

            PaymentTypeDTO paymentTypeDTO = mapper.Map<PaymentTypeDTO>(paymentType);
            return View(paymentTypeDTO);
        }

        // POST: PaymentTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this.paymentTypeService.LogicDelete(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> PaymentTypeExists(int id)
        {
            return await this.paymentTypeService.Exists(id);
        }
    }
}
