using System.Diagnostics;

public class Customer
{

    public int Id { get; set; }

    public string Name { get; set; }
    public string Surname { get; set; }

    public string Email { get; set; }

    public List<Order> Orders { get; set; }

    public override string ToString()
    {
        return "Cliente: " + Name + " " + Surname + " Email: " + Email;
    }
}
