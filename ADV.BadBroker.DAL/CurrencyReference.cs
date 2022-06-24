using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADV.BadBroker.DAL;

public class CurrencyReference
{
    public int Id { get; set; }

    public DateOnly Date { get; set; }

    public List<СurrencyValue> СurrencyValues { get; set; }
}

/// <summary>
/// Currency value to USD
/// </summary>
public class СurrencyValue
{
    public Guid Id { get; set; }

    public Сurrency Сurrency { get; set; }

    public Decimal Value { get; set; }
}

