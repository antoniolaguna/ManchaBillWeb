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
    public class ReturnsController : Controller
    {
        private readonly IReturnService returnService;
        private readonly IBillService billService;
        private readonly IModelService modelService;
        private readonly IParameterService parameterService;
        private readonly IBillLineService billLineService;
        public static List<ReturnLineDTO> lines = new List<ReturnLineDTO> { };
        public static int BillId;
        private readonly IMapper mapper;

        public ReturnsController(IReturnService returnService,IBillService billService, IModelService modelService, IParameterService parameterService, IBillLineService billLineService, IMapper mapper)
        {
            this.returnService = returnService;
            this.modelService = modelService;
            this.parameterService = parameterService;
            this.billService= billService;
            this.billLineService = billLineService;
            this.mapper = mapper;
        }

        // GET: Returns
        public async Task<IActionResult> Index()
        {
            lines = new List<ReturnLineDTO>();
            BillId = 0;


            List<Return> returns = await returnService.GetAllReturnsActives();
            List<ReturnDTO> returnsDTO = returns.Select(x => mapper.Map<ReturnDTO>(x)).ToList();
            return View(returnsDTO);
        }

        //AJAX
        public JsonResult GetReturnDetail(int idReturn)
        {

            Return returnDetail = returnService.GetByIdWithAllRelationships(idReturn);
            ReturnDTO rDTO = this.mapper.Map<ReturnDTO>(returnDetail);
            foreach (ReturnLineDTO line in rDTO.ReturnLines)
            {
                line.Return = null;
                line.Model.LineBills = null;
                line.Model.Item.Models = null;
            }

            return Json(rDTO);
        }



        // GET: Returns/Create
        public IActionResult Create()
        {
            BillId = 0;
            lines = new List<ReturnLineDTO>();

            return View();
        }

        // POST: Returns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,BillId,Date,SeriesReturn,YearReturn,NumberReturn,Prize,Tax,Value,Client,Active,DeleteDate,CreatedDate,UpdatedDate,Comments")] ReturnDTO returnDTO)
        {
            if (ModelState.IsValid)
            {
                GenerateReturn(returnDTO);
                Return objRet = this.mapper.Map<Return>(returnDTO);
                if (objRet.ReturnLines.Count > 0)
                {
                    Return result = this.returnService.Insert(objRet);
                    if (result != null)
                    {
                        IncreaseCountReturn();
                        UpdateBill(result);
                    }
                    return RedirectToAction("Create");
                }

            }
            return View(returnDTO);
        }

        private void UpdateBill(Return result)
        {
            foreach (ReturnLine l in result.ReturnLines)
            {
                int count = l.Units;
                int modelId = l.ModelId.Value;
                Bill b = billService.GetByIdWithAllRelationships(BillId);
                foreach(BillLine bl in b.BillLines)
                {
                    if (bl.ModelId == modelId)
                    {
                        bl.ReturnedUnits = bl.ReturnedUnits + l.Units;
                    }
                }
                billService.Update(b);
            }
        }

        private void IncreaseCountReturn()
        {
            Parameter p = parameterService.GetParameterByName("NUMBER_RETURN");
            string oldNumber = p.Value;
            int newNumberInt = Convert.ToInt32(oldNumber) + 1;
            string fmt = "0000000000.##";
            string newNumber = newNumberInt.ToString(fmt);
            p.Value = newNumber;
            parameterService.Update(p);
        }


        private void GenerateReturn(ReturnDTO returnDTO)
        {
            string serieReturn = this.parameterService.GetParameterByName("SERIES_RETURN").Value;
            string numberReturn = this.parameterService.GetParameterByName("NUMBER_RETURN").Value;
            int yearReturn = Convert.ToInt32(this.parameterService.GetParameterByName("YEAR_RETURN").Value);
            int tax = Convert.ToInt32(this.parameterService.GetParameterByName("TAX").Value);

            returnDTO.BillId = BillId;
            returnDTO.SeriesReturn = serieReturn;
            returnDTO.NumberReturn = numberReturn;
            returnDTO.YearReturn = yearReturn;
            returnDTO.ReturnLines = lines;
            foreach (ReturnLineDTO l in returnDTO.ReturnLines)
            {
                l.Model = null;
            }
            returnDTO.Date = DateTime.Now;
            foreach (ReturnLineDTO line in lines)
            {
                returnDTO.Prize = returnDTO.Prize + line.Sum;
            }
            returnDTO.Tax = tax;
            double taxMult = (100.0 - tax) / 100.0;
            returnDTO.Value = returnDTO.Prize * taxMult;
            



        }



        //AJAX
        public JsonResult GetBillByBillNumber(string billNumber)
        {

            Bill bill = billService.GetBillByBillNumber(billNumber);
            BillId = bill.Id;
            BillDTO bDTO = this.mapper.Map<BillDTO>(bill);
            foreach(BillLineDTO l in bDTO.BillLines)
            {
                l.Bill = null;
                l.Model.LineBills = null;
                l.Model.Item.Models = null;
            }
            return Json(bDTO);
        }

        public JsonResult AddToPreReturn(int idBillLine)
        {
            BillLine bl = billLineService.GetByIdWithAllRelationships(idBillLine);
            BillLineDTO blDTO = this.mapper.Map<BillLineDTO>(bl);
            AddBillLineToPreReturn(blDTO);
            return Json(lines);
        }

        public JsonResult DeleteToPreReturn(int idModel)
        {
            ReturnLineDTO lineToDelete = new ReturnLineDTO();
            foreach (ReturnLineDTO l in lines)
            {
                if (l.ModelId == idModel)
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
            return Json(lines);
        }

        private void AddBillLineToPreReturn(BillLineDTO blDTO)
        {
            if (ExistModelInPreReturn(blDTO.Model))
            {
                //actualizarLinea
                foreach (ReturnLineDTO l in lines)
                {
                    if (l.ModelId == blDTO.Model.Id)
                    {
                        l.Units++;
                        l.Sum = l.UnitPrize * l.Units;

                    }
                }
            }
            else
            {
                //nueva linea
                ReturnLineDTO line = new ReturnLineDTO();
                line.ModelId = blDTO.Model.Id;
                line.Model = blDTO.Model;
                line.Units = 1;
                line.Sum = blDTO.UnitPrize;
                line.UnitPrize = blDTO.UnitPrize;
                int tax = Convert.ToInt32(this.parameterService.GetParameterByName("TAX").Value);
                line.Tax = tax;
                line.Model.LineBills = null;
                line.Model.Item.Models = null;
                lines.Add(line);
            }
        }

        private bool ExistModelInPreReturn(ModelDTO model)
        {
            foreach (ReturnLineDTO l in lines)
            {
                if (l.ModelId == model.Id)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
