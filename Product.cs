﻿public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }

    public string Description { get; set; }
    public double price { get; set; }
    public List<Order> Orders { get; set; }

}