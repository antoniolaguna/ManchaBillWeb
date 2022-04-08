using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using ManchaBillWeb.Services;
using ManchaBillWeb.Models;
using ManchaBillWeb.DTOs;
namespace ManchaBillWeb.Controllers
{
    public class CashRegistersController : Controller
    {
        private readonly ICashRegisterService cashRegisterService;
        private readonly IBillService billService;
        private readonly IReturnService returnService;
        private readonly IOutFlowService outFlowService;
        private readonly IMapper mapper;

        public CashRegistersController(ICashRegisterService cashRegisterService, IBillService billService, IReturnService returnService, IOutFlowService outFlowService,  IMapper mapper)
        {
            this.cashRegisterService = cashRegisterService;
            this.billService = billService;
            this.returnService = returnService;
            this.outFlowService = outFlowService;
            this.mapper = mapper;
        }
        public IActionResult Index()
        {
            List<CashRegister> cashRegister = cashRegisterService.GetAllCashRegistersActives().Result;
            List<CashRegisterDTO> cashRegisterDTO = cashRegister.Select(x => mapper.Map<CashRegisterDTO>(x)).ToList();
            return View(cashRegisterDTO);
        }

        // GET: CashRegisters/Create
        public IActionResult Create()
        {
            CashRegister lastCashRegister = cashRegisterService.GetLastCashRegister();
            double lastCloseRem = 0;
            DateTime lastCloseDT = DateTime.MinValue;
            
            if(lastCashRegister != null)
            {
                lastCloseRem = lastCashRegister.Remander;
                lastCloseDT = lastCashRegister.ClosingDate;
            }

            List<Bill> cashSales = billService.GetAllBillsActives(lastCloseDT).Result;
            List<Return> cashReturns = returnService.GetAllReturnsActives(lastCloseDT).Result;
            List<OutFlow> outFlows = outFlowService.GetAllOutFlowsActives(lastCloseDT).Result;

            double cashSalesAmmount = 0;
            double cashReturnsAmmount = 0;
            double outFlowsAmmount = 0;

            foreach(Bill b in cashSales)
            {
                cashSalesAmmount += b.Prize;
            }

            foreach (Return r in cashReturns)
            {
                cashReturnsAmmount += r.Prize;
            }

            foreach (OutFlow o in outFlows)
            {
                outFlowsAmmount += o.Amount;
            }

            double totalCash = lastCloseRem+cashSalesAmmount - cashReturnsAmmount - outFlowsAmmount;

            ViewData["LastCloseRemander"] = lastCloseRem;
            ViewData["ClosingDate"] = DateTime.Now.ToString();
            ViewData["OpeningDate"] = DateTime.Now.ToString();
            ViewData["CashSales"] = Decimal.Round(Convert.ToDecimal(cashSalesAmmount),2);
            ViewData["Returns"] = Decimal.Round(Convert.ToDecimal(cashReturnsAmmount), 2);
            ViewData["OutFlows"] = Decimal.Round(Convert.ToDecimal(outFlowsAmmount), 2);
            ViewData["Cash"] = Decimal.Round(Convert.ToDecimal(totalCash), 2);
            return View();
        }
    }
}
