using System.Diagnostics;

public class Order
{
    public int Id { get; set; }
    public DateTime Date { get; set; }

    public double Amount { get; set; }
    public bool Status { get; set; }

    public List<Product> Products { get; set; }

    public List<Payment> Payments { get; set; }
    public int? CustomerId { get; set; }
    public Customer Customer { get; set; }
    public int EmployeId { get; set; }
    public Employe Employe { get; set; }
    public override string ToString()
    {

            string productString = "";
       
        string stringa = " Ordine: \n";
        stringa += "Totale:\t" + this.Amount + "\n";
        stringa += "Contiene:" + "\n";
        return stringa;
        
    }
}
