using AutoMapper;
using ManchaBillWeb.Models;
using System;

namespace ManchaBillWeb.DTOs
{
    public class Mapper:Profile
    {
        public Mapper()
        {
            CreateMap<SizeTypeDTO, SizeType>();
            CreateMap<SizeType, SizeTypeDTO>();

            CreateMap<Size, SizeDTO>();
            CreateMap<SizeDTO, Size>();

            CreateMap<Item, ItemDTO>();
            CreateMap<ItemDTO, Item>();

            CreateMap<ItemType, ItemTypeDTO>();
            CreateMap<ItemTypeDTO, ItemType>();

            CreateMap<Model, ModelDTO>();
            CreateMap<ModelDTO, Model>();

            CreateMap<Supplier, SupplierDTO>();
            CreateMap<SupplierDTO, Supplier>();

            CreateMap<Season, SeasonDTO>();
            CreateMap<SeasonDTO, Season>();

            CreateMap<PaymentType, PaymentTypeDTO>();
            CreateMap<PaymentTypeDTO, PaymentType>();

            CreateMap<Bill, BillDTO>();
            CreateMap<BillDTO, Bill>();

            CreateMap<BillLine, BillLineDTO>();
            CreateMap<BillLineDTO, BillLine>();

            CreateMap<Parameter, ParameterDTO>();
            CreateMap<ParameterDTO, Parameter>();

            CreateMap<Return, ReturnDTO>();
            CreateMap<ReturnDTO, Return>();

            CreateMap<ReturnLine, ReturnLineDTO>();
            CreateMap<ReturnLineDTO, ReturnLine>();

            CreateMap<OutFlow, OutFlowDTO>();
            CreateMap<OutFlowDTO, OutFlow>();

            CreateMap<CashRegister, CashRegisterDTO>();
            CreateMap<CashRegisterDTO, CashRegister>();
        }

      
    }
}
