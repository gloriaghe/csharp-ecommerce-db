
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Linq.Expressions;
EcommerceDbContext db = new EcommerceDbContext();
bool choiceOK = false;
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


//db.Products.Add(shampoo);
//db.Products.Add(balsamo);
//db.Products.Add(mascheraCapelli);
//db.Products.Add(profumoUomo);
//db.Products.Add(cremaCorpo);
//db.Products.Add(profumoDonna);
//db.Products.Add(dopobarba);
//db.Products.Add(balsamoCorpo);
//db.Products.Add(rossetto);
//db.Products.Add(fondotinta);
//db.Customers.Add(customer);
//db.Customers.Add(customer2);
//db.Employee.Add(emplye);
//db.Employee.Add(emplye2);


//lista prodotti intera
List<Product> products = db.Products.ToList<Product>();
//List<Product> productsDonna = new List<Product>();
//productsDonna.Add(shampoo);
//productsDonna.Add(balsamo);
//Console.WriteLine(productsDonna);




while (!choiceOK)
{

    Console.Write("SEi un CLIENTE o un DIPENTENTE? ");
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
            //Employe employe1 = db.Employee.Where(employe => employe.Name == "Simonetta" ).FirstOrDefault();

            if (customerRicerca != null)
            {
                Console.Write("Scegli la Box di prodotti da acquistare:");
                Console.Write("1 Box Donna");
                Console.Write("2 Box Uomo");
                Console.Write("3 Box Luxury");
                int choiceBox = Convert.ToInt32(Console.ReadLine());
                bool OrderOK = false;
                while (!OrderOK)
                {

                    Order ordine = new Order();
                    ordine.Date = DateTime.Now;
                    ordine.Amount = 40.50;
                    Random random = new Random();
                    int rnd = random.Next(0, 1);
                    if (rnd == 1)
                        ordine.Status = true;
                    else
                        ordine.Status = false;

                    customerRicerca.Orders = new List<Order>();
                    customerRicerca.Orders.Add(ordine);
                    ordine.EmployeId = 1;
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
        case "DIPENTENTE":
            choiceOK = true;



            break;
    }
}
db.SaveChanges();
