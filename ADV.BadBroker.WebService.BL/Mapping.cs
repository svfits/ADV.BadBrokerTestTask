using ADV.BadBroker.DAL;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADV.BadBroker.WebService.BL
{


    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Rootobject, CurrencyReference>()
            .ForMember(d => d.Id, mem => mem.Ignore())
            .ForMember(d => d.СurrencyValues, mem => mem.MapFrom(r => r.Rates))
            .ForMember(d => d.Date, mem => mem.MapFrom(d => DateOnly.Parse(d.Date)))
           ;

            CreateMap<Rates, List<СurrencyValue>>().ConvertUsing(i => MapGrade(i));
            //.ForMember(d => d., mem => mem.Ignore())
            //.ForMember(d => d.Сurrency, mem => mem.MapFrom(y => y.))
            ;


            // CreateMap<PacketsFilter, FilterInfo>()
            // .ForMember(d => d.Rows, mem => mem.MapFrom(s => s.PacketsFiltersElements.OrderBy(k => k.Ord)))
            // .ForMember(d => d.Type, mem => mem.MapFrom(s => (RecordType)s.PacketTypeId))
            // .ForMember(d => d.TokenPath, mem => mem.MapFrom(s => s.ObjectIdSelector))
            //;

            // CreateMap<PacketsFiltersElement, FilterElementInfo>()
            //  .ForMember(d => d.Token, mem => mem.MapFrom(s => s.Selector))
            //  .ForMember(d => d.Oper, mem => mem.MapFrom(s => (OperationsTypes)s.OperationId))
            //  .ForMember(d => d.Value, mem => mem.MapFrom(s => JToken.Parse(s.Value)))
            // ;

            // CreateMap<PacketsEvent, EventView>()
            //   .ForMember(d => d.Id, mem => mem.MapFrom(s => s.Id))
            //   .ForMember(d => d.RecordId, mem => mem.MapFrom(s => s.ObjectId))
            //   .ForMember(d => d.Type, mem => mem.MapFrom(s => (RecordType)s.PacketTypeId))
            // ;
        }

        private static List<СurrencyValue> MapGrade(Rates v)
        {
            var ls = new List<СurrencyValue>()
            {
                new СurrencyValue() { Value = (Decimal)v.RUB, Сurrency = Сurrency.RUB },
                new СurrencyValue() { Value = (Decimal)v.EUR, Сurrency = Сurrency.EUR },
                new СurrencyValue() { Value = (Decimal)v.GBR, Сurrency = Сurrency.GBR },
                new СurrencyValue() { Value = (Decimal)v.JPY, Сurrency = Сurrency.JPY },
            };

            return ls;
        }
    }
}
