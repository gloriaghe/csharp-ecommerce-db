public class Payment
{
    public int Id { get; set; }
    public DateTime Date { get; set; }

    public double amount { get; set; }
    public bool status { get; set; }
    public int OrderId { get; set; }
    public Order Order { get; set; }

}
