namespace ADV.BadBroker.DAL;

public class UserExtradition
{
    public Guid Id { get; set; }

    /// <summary>
    /// this one must be obtained from the authorization server
    /// </summary>
    public User User { get; set; } = null!;

    /// <summary>
    /// Last Bought
    /// </summary>
    public DateTime PaymentDate { get; set; }


    public List<SettlementAccount> Scores { get; set; } = new List<SettlementAccount>();
}
