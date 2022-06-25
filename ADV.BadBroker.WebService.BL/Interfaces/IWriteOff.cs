namespace ADV.BadBroker.WebService.BL;

public interface IWriteOff
{
    public Task Accrual(DateTime dtNow);
}
