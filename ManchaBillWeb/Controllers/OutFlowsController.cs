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
    public class OutFlowsController : Controller
    {
        private readonly IOutFlowService outflowService;
        private readonly IMapper mapper;

        public OutFlowsController(IOutFlowService outflowService, IMapper mapper)
        {
            this.outflowService = outflowService;
            this.mapper = mapper;
        }


        public IActionResult Index()
        {
            List<OutFlow> outflows = outflowService.GetAllOutFlowsActives().Result;
            List<OutFlowDTO> outFlowsDTOs = outflows.Select(x => mapper.Map<OutFlowDTO>(x)).ToList();
            return View(outFlowsDTOs);
        }

        //AJAX
        [HttpPost]
        public JsonResult CreateNewOutFlow(double ammount, string description)
        {
            try
            {
                if (ammount > 0 && description != null)
                {
                    OutFlow outflow = new OutFlow();
                    outflow.Amount = ammount;
                    outflow.Description = description;
                    outflow.Date=DateTime.Now;
                    outflowService.Insert(outflow);
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }
            }
            catch(Exception ex)
            {
                return Json(false);
            }
           



        }
    }
}
