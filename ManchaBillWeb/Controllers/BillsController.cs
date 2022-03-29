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
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.IO;
using ManchaBillWeb.Utils;

namespace ManchaBillWeb.Controllers
{
    public class BillsController : Controller
    {
        private readonly IBillService billService;
        private readonly IModelService modelService;
        private readonly IParameterService parameterService;
        private readonly IMapper mapper;
        public static List<BillLineDTO> lines = new List<BillLineDTO> { };
        public static int paymentType = 1;

        public BillsController(IBillService billService, IModelService modelService, IParameterService parameterService, IMapper mapper)
        {
            this.billService = billService;
            this.modelService = modelService;
            this.parameterService = parameterService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            lines = new List<BillLineDTO>();
            paymentType = 1;

            List<Bill> bills = await billService.GetAllBillsActives();
            List<BillDTO> billsDTO = bills.Select(x => mapper.Map<BillDTO>(x)).ToList();
            return View(billsDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,MoneyDelivered,MoneyReturned,PaymentTypeId,Active,DeleteDate,CreatedDate,UpdatedDate,Comments")] BillDTO billDTO)
        {

            if (ModelState.IsValid)
            {
                GenerateBill(billDTO);
                Bill bill = this.mapper.Map<Bill>(billDTO);
                if (bill.BillLines.Count > 0)
                {
                    Bill result = this.billService.Insert(bill);
                    if (result != null)
                    {
                        IncreaseCountBill();
                        UpdateStock(result);
                    }
                    return RedirectToAction("Create");
                }

            }
            return View(billDTO);
        }

        private void UpdateStock(Bill result)
        {
            foreach (BillLine l in result.BillLines)
            {
                int count = l.Units;
                int modelId = l.ModelId.Value;
                Model m = modelService.GetById(modelId).Result;

                m.CountStore = m.CountStore - count;
                m.CountSold = m.CountSold + count;
                modelService.Update(m);
            }
        }

        private void IncreaseCountBill()
        {
            Parameter p = parameterService.GetParameterByName("NUMBER_BILL");
            string oldNumber = p.Value;
            int newNumberInt = Convert.ToInt32(oldNumber) + 1;
            string fmt = "0000000000.##";
            string newNumber = newNumberInt.ToString(fmt);
            p.Value = newNumber;
            parameterService.Update(p);
        }

        private void GenerateBill(BillDTO billDTO)
        {
            string serieBill = this.parameterService.GetParameterByName("SERIES_BILL").Value;
            string numberBill = this.parameterService.GetParameterByName("NUMBER_BILL").Value;
            int yearBill = Convert.ToInt32(this.parameterService.GetParameterByName("YEAR_BILL").Value);
            int tax = Convert.ToInt32(this.parameterService.GetParameterByName("TAX").Value);

            billDTO.SeriesBill = serieBill;
            billDTO.NumberBill = numberBill;
            billDTO.YearBill = yearBill;
            billDTO.BillLines = lines;
            foreach (BillLineDTO l in billDTO.BillLines)
            {
                l.Model = null;
            }
            billDTO.Date = DateTime.Now;
            foreach (BillLineDTO line in lines)
            {
                billDTO.Prize = billDTO.Prize + line.Sum;
            }
            billDTO.Tax = tax;
            double taxMult = (100.0 - tax) / 100.0;
            billDTO.Value = billDTO.Prize * taxMult;
            billDTO.PaymentTypeId = paymentType;
            if (billDTO.PaymentTypeId == 1)
            {
                billDTO.MoneyDelivered = 0;
                billDTO.MoneyReturned = 0;
            }



        }

        public IActionResult Cancel()
        {
            lines = new List<BillLineDTO>();
            paymentType = 1;

            return View("Create");
        }


        public IActionResult Create()
        {
            lines = new List<BillLineDTO>();
            paymentType = 1;

            return View();
        }





        //AJAX
        public JsonResult GetItemsByBarcode(string barcode)
        {

            List<Model> models = modelService.GetAllModelsActivesByBarcode(barcode).Result;
            List<ModelDTO> modelsDTO = models.Select(x => mapper.Map<ModelDTO>(x)).ToList();
            foreach (var modelDto in modelsDTO)
            {
                modelDto.Item.Models = null;
            }
            return Json(modelsDTO);


        }

        //AJAX
        public JsonResult GetBillDetail(int idBill)
        {

            Bill billDetail = billService.GetByIdWithAllRelationships(idBill);
            BillDTO bDTO = this.mapper.Map<BillDTO>(billDetail);
            foreach (BillLineDTO line in bDTO.BillLines)
            {
                line.Bill = null;
                line.Model.LineBills = null;
                line.Model.Item.Models = null;
            }

            return Json(bDTO);
        }

        public JsonResult DeleteFromPreBill(int idModel)
        {
            Model m = modelService.GetByIdWithAllRelationships(idModel);
            ModelDTO mDTO = this.mapper.Map<ModelDTO>(m);
            DeleteModelToPreBill(mDTO);
            return Json("");
        }

        public JsonResult SelectPaymentType(int PaymentTypeId)
        {
            paymentType = PaymentTypeId;
            return Json("");
        }

        private void DeleteModelToPreBill(ModelDTO mDTO)
        {
            BillLineDTO lineToDelete = new BillLineDTO();
            foreach (BillLineDTO l in lines)
            {
                if (l.ModelId == mDTO.Id)
                {
                    lineToDelete = l;

                }
            }
            if (lineToDelete.Units > 1)
            {
                //actualizarLinea
                lineToDelete.Units--;
                lineToDelete.Sum = lineToDelete.Units * lineToDelete.Model.Item.Prize;

            }
            else
            {
                //eliminar linea
                lines.Remove(lineToDelete);
            }
        }

        public JsonResult AddToPreBill(int idModel)
        {
            Model m = modelService.GetByIdWithAllRelationships(idModel);
            ModelDTO mDTO = this.mapper.Map<ModelDTO>(m);
            AddModelToPreBill(mDTO);
            return Json("");
        }

        private void AddModelToPreBill(ModelDTO mDTO)
        {
            if (ExistModelInPreBill(mDTO))
            {
                //actualizarLinea
                foreach (BillLineDTO l in lines)
                {
                    if (l.ModelId == mDTO.Id)
                    {
                        l.Units++;
                        l.Sum = l.Model.Item.Prize * l.Units;

                    }
                }
            }
            else
            {
                //nueva linea
                BillLineDTO line = new BillLineDTO();
                line.ModelId = mDTO.Id;
                line.Model = mDTO;
                line.Units = 1;
                line.Sum = mDTO.Item.Prize;
                line.UnitPrize = mDTO.Item.Prize;
                int tax = Convert.ToInt32(this.parameterService.GetParameterByName("TAX").Value);
                line.Tax = tax;
                lines.Add(line);
            }

        }

        private bool ExistModelInPreBill(ModelDTO mDTO)
        {
            foreach (BillLineDTO l in lines)
            {
                if (l.ModelId == mDTO.Id)
                {
                    return true;
                }
            }
            return false;
        }




        public IActionResult ExportPDF(int? idBill)
        {
            Bill bill = billService.GetByIdWithAllRelationships(idBill.Value);
            MemoryStream ms = PDFGenerator.GenerateBillPDF(bill, parameterService);
            return File(ms, "application/pdf");
        }

        


    }
}
