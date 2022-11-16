
using System.Linq.Expressions;
EcommerceDbContext db = new EcommerceDbContext();
bool choiceOK = false;
//Product marco = new Product() { Name = "Marco", Description = "Gialli", Price = 10.20 };



while (!choiceOK)
{

    Console.Write("SEi un CLIENTE o un DIPENTENTE? ");
    string choice = Console.ReadLine();

    switch (choice)
    {
        case "CLIENTE":
            choiceOK = true;
            break;
        case "DIPENTENTE":
            choiceOK = true;
            break;
    }
}
