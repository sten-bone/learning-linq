namespace LearningLinq.Queries;

public class CustomerQueries
{
    private static readonly Product[] Products = new Product[]
    {
        new Product(1, "Pencil", 0.49),
        new Product(2, "Pen", 0.99),
        new Product(3, "Paper (200 ct)", 1.99),
        new Product(4, "Eraser", 1.49),
        new Product(5, "Sticky Notes (100 ct)", 2.99)
    };

    private static readonly PostalCode[] PostalCodes = new PostalCode[]
    {
        new PostalCode("52240", "Iowa City", "Iowa"),
        new PostalCode("50312", "Des Moines", "Iowa"),
        new PostalCode("50265", "West Des Moines", "Iowa"),
        new PostalCode("03718", "Seattle", "Washington"),
        new PostalCode("12345", "Somewhere", "Colorado"),
        new PostalCode("12346", "Somewhere Else", "Colorado"),
        new PostalCode("84712", "San Antonio", "Texas")
    };

    private static readonly Company[] Companies = new Company[]
    {
        new Company(100, "University of Iowa", "123 Iowa Ave", "52240"),
        new Company(101, "Cutting Edge Products", "8412 Street Blvd", "037178"),
        new Company(102, "Drake University", "100 42nd St", "50312"),
        new Company(104, "Middle of Nowhere Goods", "23 Dusty Rd", "12345"),
        new Company(105, "School Supplies of Texas", "45214 Paper Rd", "84712"),
        new Company(106, "Clark and Clark, LLC.", "1 Clark St", "12346"),
        new Company(107, "OfficeMin", "321 Average Ave", "52240"),
        new Company(108, "OfficeMin", "678 Maximum Rd", "03718"),
        new Company(109, "OfficeMin", "5672 Main St", "84712"),
        new Company(110, "Reel Deal General Goods and Bait Shop", "561 Bass Rd", "50265")
    };

    private static readonly Order[] Orders = new Order[]
    {
        new Order(1, 100, new DateTime(2022, 2, 16, 3, 32, 0)),
        new Order(2, 101, new DateTime(2022, 2, 16, 4, 47, 32)),
        new Order(3, 100, new DateTime(2022, 2, 28, 13, 4, 57)),
        new Order(4, 108, new DateTime(2022, 3, 4, 14, 43, 08)),
        new Order(5, 108, new DateTime(2022, 3, 4, 15, 03, 57)),
        new Order(6, 102, new DateTime(2022, 4, 13, 11, 27, 29)),
        new Order(7, 107, new DateTime(2022, 4, 14, 9, 19, 19))
    };

    private static readonly OrderDetail[] OrderDetails = new OrderDetail[]
    {
        new OrderDetail(1, 1, 67),
        new OrderDetail(1, 2, 271),
        new OrderDetail(1, 4, 461),
        new OrderDetail(2, 2, 200),
        new OrderDetail(2, 3, 84),
        new OrderDetail(2, 5, 127),
        new OrderDetail(3, 1, 1237),
        new OrderDetail(3, 2, 7461),
        new OrderDetail(3, 3, 1500),
        new OrderDetail(3, 4, 1500),
        new OrderDetail(3, 5, 90),
        new OrderDetail(4, 1, 10000),
        new OrderDetail(5, 1, 10000),
        new OrderDetail(5, 2, 10000),
        new OrderDetail(6, 1, 25000),
        new OrderDetail(6, 5, 1000),
        new OrderDetail(7, 3, 100),
        new OrderDetail(7, 4, 147)
    };

    public static void RunQueries()
    {
        // GetCompanyInformation();
        GetOrderDetails();
    }

    // get information (name & address) for each company
    private static void GetCompanyInformation()
    {
        Console.WriteLine("Company Information");

        var companyInformationQuery = Companies
            .Join(PostalCodes, c => c.PostalCode, p => p.Code,
                (c, p) => new
                {
                    c.Name,
                    c.Street,
                    p.City,
                    p.State,
                    c.PostalCode
                });

        foreach (var companyInformation in companyInformationQuery)
        {
            Console.WriteLine($"{companyInformation.Name}");
            Console.WriteLine($"{companyInformation.Street}");
            Console.WriteLine($"{companyInformation.City}, {companyInformation.State} {companyInformation.PostalCode}\n");
        }

        Console.WriteLine();
    }

    // query for details of each order
    private static void GetOrderDetails()
    {
        Console.WriteLine("Details of each order\n");

        var companyInformationQuery = Companies
            .Join(PostalCodes, c => c.PostalCode, p => p.Code,
                (c, p) => new
                {
                    c.Id,
                    c.Name,
                    c.Street,
                    p.City,
                    p.State,
                    c.PostalCode
                });

        var orderProductTotalsQuery = Orders
            .Join(OrderDetails, o => o.OrderNumber, od => od.OrderNumber,
                (o, od) => new
                {
                    o.OrderNumber,
                    o.CompanyId,
                    o.OrderDate,
                    od.ProductCode,
                    od.Quantity,
                })
            .Join(Products, o => o.ProductCode, p => p.ProductCode,
                (o, p) => new
                {
                    o.OrderNumber,
                    o.CompanyId,
                    o.OrderDate,
                    p.Name,
                    p.Price,
                    o.Quantity,
                    ProductTotal = p.Price * o.Quantity
                });

        var orderCompanyDetailsQuery = Orders
            .Join(companyInformationQuery, o => o.CompanyId, c => c.Id,
                (o, c) => new
                {
                    c.Name,
                    c.Street,
                    c.City,
                    c.State,
                    c.PostalCode,
                    o.OrderNumber
                });

        var orderDetailsQuery = orderCompanyDetailsQuery
            .Join(orderProductTotalsQuery, c => c.OrderNumber, p => p.OrderNumber,
                (o, p) => new
                {
                    CompanyDetails = o,
                    ProductTotals = p
                })
            .GroupBy(x => x.ProductTotals.OrderNumber)
            .Select(x => new
            {
                x.ToList()[0].CompanyDetails,
                x.ToList()[0].ProductTotals.OrderDate,
                ProductDetails = x.ToList(),
                OrderTotal = x.ToList().Sum(y => y.ProductTotals.ProductTotal)
            });

        foreach (var x in orderDetailsQuery)
        {
            var companyDetail = x.CompanyDetails;
            Console.WriteLine("=============================================================");
            Console.WriteLine($"| Order on {x.OrderDate:F}");
            Console.WriteLine($"| {companyDetail.Name}");
            Console.WriteLine($"| {companyDetail.Street}");
            Console.WriteLine($"| {companyDetail.City}, {companyDetail.State} {companyDetail.PostalCode}");
            Console.WriteLine("-------------------------------------------------------------");

            foreach (var product in x.ProductDetails)
            {
                var totals = product.ProductTotals;
                Console.WriteLine($"|\t{totals.Name}: {totals.Quantity} * ${totals.Price} = {totals.ProductTotal:F2}");
            }

            Console.WriteLine($"|\n|\tTotal: ${x.OrderTotal:F2}\n|");

            Console.WriteLine("=============================================================\n");
        }

        Console.WriteLine();
    }
}

// Record structs
public readonly record struct Company(int Id, string Name, string Street, string PostalCode);
public readonly record struct PostalCode(string Code, string City, string State);
public readonly record struct Order(int OrderNumber, int CompanyId, DateTime OrderDate);
public readonly record struct OrderDetail(int OrderNumber, int ProductCode, int Quantity);
public readonly record struct Product(int ProductCode, string Name, double Price);