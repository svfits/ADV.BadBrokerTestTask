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

            CreateMap<HashSet<CurrencyReference>, DTO.Rates[]>().ConstructUsing(i => ConvertHashSetToRates(i));
        }

        private DTO.Rates[] ConvertHashSetToRates(HashSet<CurrencyReference> i)
        {
            var rates = new List<DTO.Rates>();

            foreach (var r in i)
            {
                var ttt = r.СurrencyValues;
                rates.Add(new DTO.Rates()
                {
                    Date = r.Date.ToDateTime(new TimeOnly(0, 0, 0)),
                    EUR = ttt.First(a => a.Сurrency == Сurrency.EUR).Value,
                    GBR = ttt.First(a => a.Сurrency == Сurrency.GBR).Value,
                    JPY = ttt.First(a => a.Сurrency == Сurrency.JPY).Value,
                    RUB = ttt.First(a => a.Сurrency == Сurrency.RUB).Value
                });
            }

            return rates.ToArray();
        }

        private static Rates RatesToCurrencyValue(List<СurrencyValue> lsCurrencyValue)
        {
            var rates = new Rates()
            {
                EUR = (float)lsCurrencyValue.First(a => a.Сurrency == Сurrency.EUR).Value,
                GBR = (float)lsCurrencyValue.First(a => a.Сurrency == Сurrency.GBR).Value,
                JPY = (float)lsCurrencyValue.First(a => a.Сurrency == Сurrency.JPY).Value,
                RUB = (float)lsCurrencyValue.First(a => a.Сurrency == Сurrency.RUB).Value,
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
