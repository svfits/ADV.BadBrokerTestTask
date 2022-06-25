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
            .ReverseMap();
            ;

            CreateMap<Rates, List<СurrencyValue>>().ConvertUsing(i => СurrencyValueToRates(i));         
            ;

            CreateMap<List<СurrencyValue>, Rates>().ConvertUsing(i => RatesToCurrencyValue(i));
        }

        private static Rates RatesToCurrencyValue(List<СurrencyValue> i)
        {
            var rates = new Rates() 
            {
                EUR = (float)i.First(a => a.Сurrency == Сurrency.EUR).Value,
                GBR = (float)i.First(a => a.Сurrency == Сurrency.GBR).Value,
                JPY = (float)i.First(a => a.Сurrency == Сurrency.JPY).Value,
                RUB = (float)i.First(a => a.Сurrency == Сurrency.RUB).Value,
            };

            return rates;
        }

        private static List<СurrencyValue> СurrencyValueToRates(Rates v)
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
