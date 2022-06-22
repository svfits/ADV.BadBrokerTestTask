using ADV.BadBroker.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ADV.BadBroker.WebService.BL;

public class WriteOff : IWriteOff
{
    private readonly Context db;
    private readonly ILogger<WriteOff> _logger;

    public WriteOff(Context contex, ILogger<WriteOff> logger)
    {
        db = contex;
        _logger = logger;
    }

    /// <summary>
    /// interval can be stored in the database
    /// </summary>
    /// <param name="dtNow">Current time</param>
    /// <returns></returns>
    public async Task Accrual(DateTime dtNow)
    {
        var interval = new TimeSpan(24, 0, 0);

        var usersIds = db
            .SettlementAccount
            .Where(d => d.TotalCurrency > 0 && (dtNow - d.Start) > interval && d.Currency != Сurrency.USD)
            .Select(d => d.User.Id)
            ;

        foreach (var userId in usersIds)
        {
            var trans = await db.Database.BeginTransactionAsync();

            var sacUser = await db.SettlementAccount.FirstAsync(g => g.User.Id == userId && g.Currency == Сurrency.USD);
            sacUser.TotalCurrency--;

            await db.SaveChangesAsync();
            await trans.CommitAsync();
        }
    }
}
