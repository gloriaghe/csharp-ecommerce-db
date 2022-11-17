
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Collections.Generic;
using System.Linq.Expressions;
EcommerceDbContext db = new EcommerceDbContext();
bool choiceOK = false;

//startApp();


List<Order> orders = db.Orders.Where(o => o.Status == false).ToList<Order>();

if (orders.Count > 0)
{
    Console.WriteLine("Ci sono box disponibili");
}



while (!choiceOK)
{

    Console.Write("SEi un CLIENTE o un DIPENDENTE? ");
    string choice = Console.ReadLine();

    switch (choice)
    {
        case "CLIENTE":
            choiceOK = true;
            Console.Write("Ciao, inserisci il tuo nome: ");
            string name = Console.ReadLine();
            Console.Write("Il tuo cognome: ");
            string surname = Console.ReadLine();
            Customer customerRicerca = db.Customers.Where(customer => customer.Name == name || customer.Surname == surname).FirstOrDefault();

            if (customerRicerca != null)
            {
                Console.Write("Scegli la Box di prodotti da acquistare: ");
                int choiceBox = 0;

                int number = 0;
                foreach (Order order in orders)
                {
                    Console.Write(number + ". " + order);

                    Console.Write("____");

                    Console.Write("Quale Box vuoi acquistare? Scegli in numero: ");

                    choiceBox = Convert.ToInt32(Console.ReadLine());
                    number++;
                }
                bool OrderOK = false;
                while (!OrderOK)
                {

                    orders[choiceBox].Date = DateTime.Now;
                    orders[choiceBox].CustomerId = customerRicerca.Id;
                    Random random = new Random();
                    //int rnd = random.Next(0, 1);
                    int rnd = 1;
                    if (rnd == 1)
                    {
                        Payment payment = new Payment() { Amount = orders[choiceBox].Amount, Date = DateTime.Now, Order = orders[choiceBox], OrderId = orders[choiceBox].Id, Status = true };
                        orders[choiceBox].Status = true;
                        db.Payments.Add(payment);


                    }
                    else
                    {
                        orders[choiceBox].Status = false;
                        Payment payment = new Payment() { Amount = orders[choiceBox].Amount, Date = DateTime.Now, Order = orders[choiceBox], OrderId = orders[choiceBox].Id, Status = false };
                        db.Payments.Add(payment);

                    }



                    db.SaveChanges();

                    if (rnd == 0)
                    {
                        Console.WriteLine("L'ordine non è andato a buon fine. Riprovo!");
                        OrderOK = false;

                    }
                    else
                    {
                        Console.WriteLine("Ordine effettuato!");
                        OrderOK = true;
                    }

                }
            }
            else
            {
                Console.Write("Utente non iscritto al negozio");

            }

            break;
        case "DIPENDENTE":
            Console.Write("Ciao, inserisci il tuo nome: ");
            string nameEmploye = Console.ReadLine();
            Console.Write("Il tuo cognome: ");
            string surnameEmploye = Console.ReadLine();
            Employe employeRicerca = db.Employee.Where(employe => employe.Name == nameEmploye || employe.Surname == surnameEmploye).FirstOrDefault();
            choiceOK = true;
            Console.WriteLine("1. Inserisci un nuovo cliente");
            Console.WriteLine("2. Inserisci un nuovo ordine");
            Console.WriteLine("3. Inserisci un nuovo prodotto");
            Console.WriteLine("4. Trova un prodotto");
            Console.WriteLine("5. Crea box prodotti");
            Console.WriteLine("6. Modifica o cancella un prodotto");


            int choiceEmployee = Convert.ToInt32(Console.ReadLine());
            if (choiceEmployee == 1)
            {
                Customer customerNew = newClient();
                Console.Write(customerNew);
            }
            if (choiceEmployee == 2)
            {
                Console.WriteLine("scelta prodotti:");
                SearchProduct();
                Console.WriteLine("");
            }
            if (choiceEmployee == 3)
            {
                Product productnew = newProduct();
                Console.Write(productnew);
            }
            if (choiceEmployee == 4)
            {
                Product product = SearchProduct();
                Console.Write(product);

            }
            if (choiceEmployee == 5)
            {
                bool orderOk = false;
                List<Product> box = null;
                while (!orderOk)
                {

                    Product product = SearchProduct();
                    Console.Write("Vuoi aggiungerlo alla box: SI/NO");
                    string boxChoice = Console.ReadLine();

                    if (boxChoice == "SI")
                    {
                        box = new List<Product>();
                        box.Add(product);
                        Console.WriteLine(box);
                    }
                    else
                    {
                        Console.Write("Non aggiunto");

                    }
                    Console.WriteLine("Vuoi aggiungere altri prodotti: SI/NO");
                    string orderChoice = Console.ReadLine();

                    if (orderChoice == "SI")
                        Console.WriteLine("OK");
                    else
                    {

                        Console.WriteLine("Box finita");
                        orderOk = true;
                    }
                }
                newOrder(employeRicerca, box);

            }
            if (choiceEmployee == 6)
            {
                bool modifyOK = false;

                while (!modifyOK)
                {
                    Product product = SearchProduct();
                    Console.WriteLine("1. Vuoi modificare il prodotto: ");
                    Console.WriteLine("2. Vuoi cancellare il prodotto: ");

                    int modifyChoice = Convert.ToInt32(Console.ReadLine());
                    if (modifyChoice == 1)
                    {
                        Console.Write("Vuoi modificare il Prezzo (1), il nome (2) o la descrizione (3): ");
                        modifyChoice = Convert.ToInt32(Console.ReadLine());
                        ModifyProduct( product, modifyChoice);
                        Console.WriteLine(product);
                    }
                    if (modifyChoice == 2)
                    {
                        Console.Write("Sei sicuro di volerlo cancellare? SI/NO ");
                        string deleteChoice = Console.ReadLine();
                        if(deleteChoice == "SI")
                        {
                            db.Remove(product);
                            db.SaveChanges();
                            Console.WriteLine("Prodotto cancellato! ");

                        }
                        else
                        {
                            Console.WriteLine("Prodotto non cancellato!");
                        }

                    }
                    else
                    {
                        Console.WriteLine("Scelta inesistente");

                    }
                }

            }
            break;
    }
}
db.SaveChanges();


Customer newClient()
{
    Console.Write("Inserisci il nome : ");
    string nameNewCustomer = Console.ReadLine();
    Console.Write("Inserisci il cognome : ");
    string surnameCUstomer = Console.ReadLine();
    Console.Write("Inserisci la email: ");
    string email = Console.ReadLine();
    Customer customerNew = new Customer() { Name = nameNewCustomer, Surname = surnameCUstomer, Email = email };
    db.Customers.Add(customerNew);
    db.SaveChanges();
    return customerNew;
}

Product newProduct()
{
    Console.Write("Inserisci il nome prodotto: ");
    string name = Console.ReadLine();
    Console.Write("Inserisci la descrizione : ");
    string description = Console.ReadLine();
    Console.Write("Inserisci il prezzo: ");
    double price = Convert.ToDouble(Console.ReadLine());
    Product product = new Product() { Name = name, Description = description, Price = price };
    db.Products.Add(product);
    db.SaveChanges();
    return product;

}

Product SearchProduct()
{
    Console.Write("Inserisci il nome prodotto: ");
    string name = Console.ReadLine();
    Product product = db.Products.Where(product => product.Name == name).FirstOrDefault();
    Console.WriteLine(product);
    return product;

}
Order newOrder(Employe employe, List<Product> products)
{
    Console.Write("Inserisci il totale: ");
    int amount = Convert.ToInt32(Console.ReadLine());

    Order order = new Order() { Date = DateTime.Now, Amount = amount, Status = false, EmployeId = employe.Id, Employe = employe, Products = products };
    db.Orders.Add(order);
    db.SaveChanges();
    return order;

}

Product ModifyProduct(Product product, int campo)
{



    if (campo == 1)
    {
        Console.Write("Inserisci il nuovo prezzo: ");
        double price = Convert.ToDouble(Console.ReadLine());
        product.Price = price;

    }
    else if (campo == 2)
    {
        Console.Write("Inserisci il nuovo nome prodotto: ");
        string name = Console.ReadLine();
        product.Name = name;

    }
    else if (campo == 3)
    {
        Console.Write("Inserisci la nuova descrizione prodotto: ");
        string description = Console.ReadLine();
        product.Description = description;
    }

    db.SaveChanges();

    return product;
}
void startApp()
{

    Product shampoo = new Product() { Name = "Shampoo", Description = "Per capelli stupendi", Price = 10.20 };
    Product balsamo = new Product() { Name = "Balsamo", Description = "Per capelli morbidi", Price = 9.80 };
    Product mascheraCapelli = new Product() { Name = "Maschera capelli di seta", Description = "Per capelli morbidi", Price = 15.20 };
    Product profumoUomo = new Product() { Name = "Profumo Uomo Bello", Description = "Profumo che piace", Price = 11.30 };
    Product cremaCorpo = new Product() { Name = "Coccolino", Description = "Per coccolarsi", Price = 12.80 };
    Product profumoDonna = new Product() { Name = "Profumo profumoso", Description = "Per capelli stupendi", Price = 11.20 };
    Product dopobarba = new Product() { Name = "Dopobarba", Description = "Per veri uomini", Price = 12.20 };
    Product balsamoCorpo = new Product() { Name = "Balsamo corpo", Description = "Per chi si vuole bene", Price = 9.80 };
    Product rossetto = new Product() { Name = "Rossetto", Description = "Per una bocca bellissima", Price = 10.70 };
    Product fondotinta = new Product() { Name = "Fondotinta", Description = "Pelle di seta", Price = 11.80 };
    Customer customer = new Customer() { Name = "Lucilla", Surname = "Verdi", Email = "lucilla@gmail.com" };
    Customer customer2 = new Customer() { Name = "Luca", Surname = "Rossi", Email = "luca@gmail.com" };
    Employe emplye = new Employe() { Name = "Simonetta", Surname = "Bianchi" };
    Employe emplye2 = new Employe() { Name = "Giulio", Surname = "Neri" };


    db.Products.Add(shampoo);
    db.Products.Add(balsamo);
    db.Products.Add(mascheraCapelli);
    db.Products.Add(profumoUomo);
    db.Products.Add(cremaCorpo);
    db.Products.Add(profumoDonna);
    db.Products.Add(dopobarba);
    db.Products.Add(balsamoCorpo);
    db.Products.Add(rossetto);
    db.Products.Add(fondotinta);
    db.Customers.Add(customer);
    db.Customers.Add(customer2);
    db.Employee.Add(emplye);
    db.Employee.Add(emplye2);

    // lista prodotti intera
    List<Product> products = db.Products.ToList<Product>();

    db.SaveChanges();

}
