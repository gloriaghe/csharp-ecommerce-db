﻿public class Order
{
    public int Id { get; set; }
    public DateTime Date { get; set; }

    public double amount { get; set; }
    public bool status { get; set; }

    public List<Product> Products { get; set; }

    public List<Payment> Payments { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    public int EmployeId { get; set; }
    public Employe Employe { get; set; }
}