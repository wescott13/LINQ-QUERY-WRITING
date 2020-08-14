using LINQ.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Channels;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace LINQ
{
    class Program
    {
        static void Main()
        {
            //PrintAllProducts();
            //PrintAllCustomers();

            //Exercise1();
            //Exercise2();
            //Exercise3();
            //Exercise4();
            //Exercise5();
            //Exercise6();
            //Exercise7();
            //Exercise8();
            //Exercise9();
            //Exercise10();
            //Exercise11();
            //Exercise12();
            //Exercise13();
            //Exercise14();
            //Exercise15();
            //Exercise16();
            //Exercise17();
            //Exercise18();
            //Exercise19();
            //Exercise20();
            //Exercise21();
            //Exercise22();
            //Exercise23();
            //Exercise24();
            //Exercise25();
            //Exercise26();
            //Exercise27();
            //Exercise28();
            //Exercise29();
            //Exercise30();
            //Exercise31();

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        #region "Sample Code"
        /// <summary>
        /// Sample, load and print all the product objects
        /// </summary>
        static void PrintAllProducts()
        {
            List<Product> products = DataLoader.LoadProducts();
            PrintProductInformation(products);
        }

        /// <summary>
        /// This will print a nicely formatted list of products
        /// </summary>
        /// <param name="products">The collection of products to print</param>
        static void PrintProductInformation(IEnumerable<Product> products)
        {
            string line = "{0,-5} {1,-35} {2,-15} {3,6:c} {4,6}";
            Console.WriteLine(line, "ID", "Product Name", "Category", "Unit", "Stock");
            Console.WriteLine("==============================================================================");

            foreach (var product in products)
            {
                Console.WriteLine(line, product.ProductID, product.ProductName, product.Category,
                    product.UnitPrice, product.UnitsInStock);
            }

        }

        /// <summary>
        /// Sample, load and print all the customer objects and their orders
        /// </summary>
        static void PrintAllCustomers()
        {
            var customers = DataLoader.LoadCustomers();
            PrintCustomerInformation(customers);
        }

        /// <summary>
        /// This will print a nicely formated list of customers
        /// </summary>
        /// <param name="customers">The collection of customer objects to print</param>
        static void PrintCustomerInformation(IEnumerable<Customer> customers)
        {
            foreach (var customer in customers)
            {
                Console.WriteLine("==============================================================================");
                Console.WriteLine(customer.CompanyName);
                Console.WriteLine(customer.Address);
                Console.WriteLine("{0}, {1} {2} {3}", customer.City, customer.Region, customer.PostalCode, customer.Country);
                Console.WriteLine("p:{0} f:{1}", customer.Phone, customer.Fax);
                Console.WriteLine();
                Console.WriteLine("\tOrders");
                foreach (var order in customer.Orders)
                {
                    Console.WriteLine("\t{0} {1:MM-dd-yyyy} {2,10:c}", order.OrderID, order.OrderDate, order.Total);
                }
                Console.WriteLine("==============================================================================");
                Console.WriteLine();
            }
        }
        #endregion

        /// <summary>
        /// Print all products that are out of stock.
        /// </summary>
        static void Exercise1()
        {
            List<Product> products = DataLoader.LoadProducts();
            var outOfStock = DataLoader.LoadProducts().Where(p => p.UnitsInStock == 0);
            
            PrintProductInformation(outOfStock);
        }

        /// <summary>
        /// Print all products that are in stock and cost more than 3.00 per unit.
        /// </summary>
        static void Exercise2()
        {
            List<Product> products = DataLoader.LoadProducts();
            var inOverThree = DataLoader.LoadProducts().Where(p => p.UnitsInStock > 0 && p.UnitPrice > 3);

            PrintProductInformation(inOverThree);
        }

        /// <summary>
        /// Print all customer and their order information for the Washington (WA) region.
        /// </summary>
        static void Exercise3()
        {
            var waRegion = DataLoader.LoadCustomers().Where(p => p.Region == "WA");

            PrintCustomerInformation(waRegion);
        }

        /// <summary>
        /// Create and print an anonymous type with just the ProductName
        /// </summary>
        static void Exercise4()
        {
            var productNames = from pn in DataLoader.LoadProducts()
                               select new { productName = pn.ProductName };
            
            foreach (var pn in productNames)
            {
                Console.WriteLine(pn.productName);
            }
        }

        /// <summary>
        /// Create and print an anonymous type of all product information but increase the unit price by 25%
        /// </summary>
        static void Exercise5()
        {
            var products25P = from p in DataLoader.LoadProducts()
                               select new { product25P = p.ProductID + ", " + p.ProductName + ", " +
                               ((p.UnitPrice * .25M)+p.UnitPrice) + ", " + p.UnitsInStock };
            
            foreach (var p in products25P)
            {
                Console.WriteLine(p.product25P);
            }
        }

        /// <summary>
        /// Create and print an anonymous type of only ProductName and Category with all the letters in upper case
        /// </summary>
        static void Exercise6()
        {
            var productsNcUpper = from p in DataLoader.LoadProducts()
                              select new
                              {    
                                  productNcUpper = p.ProductName + ", " + p.Category    
                              };
            

            foreach (var p in productsNcUpper)
            {
                string productToUpper = p.productNcUpper.ToUpper();
                Console.WriteLine(productToUpper);
            }
        }

        /// <summary>
        /// Create and print an anonymous type of all Product information with an extra bool property ReOrder which should 
        /// be set to true if the Units in Stock is less than 3
        /// 
        /// Hint: use a ternary expression
        /// </summary>
        static void Exercise7()
        {
            var productsInfo = from p in DataLoader.LoadProducts()
                               select new
                               {
                                   productInfo =  p.ProductID + ", " + p.ProductName + ", " +
                                    p.UnitPrice + ", " + p.UnitsInStock,
                                   p.UnitsInStock
                               };

            foreach (var p in productsInfo)
            {  
                bool reOrder = false;
                if (p.UnitsInStock < 3) reOrder = true;

                Console.WriteLine(p.productInfo);
                Console.WriteLine(reOrder);
            } 
        }

        /// <summary>
        /// Create and print an anonymous type of all Product information with an extra decimal called 
        /// StockValue which should be the product of unit price and units in stock
        /// </summary>
        static void Exercise8()
        {
            var productsInfo = from p in DataLoader.LoadProducts()
                               select new
                               {
                                   productInfo = p.ProductID + ", " + p.ProductName + ", " +
                                    p.UnitPrice + ", " + p.UnitsInStock,
                                   p.UnitsInStock,
                                   p.UnitPrice
                               };
            foreach (var p in productsInfo)
            {
                decimal stockValue = p.UnitsInStock + p.UnitPrice;
                
                Console.WriteLine(p.productInfo);
                Console.WriteLine(stockValue);
            }
        }

        /// <summary>
        /// Print only the even numbers in NumbersA
        /// </summary>
        static void Exercise9()
        {
            List<int> numbersA = new List<int>(DataLoader.NumbersA);
            var onlyEven = from number in numbersA
                           where number % 2 == 0
                           select number;

            foreach (var number in onlyEven)
            {
                Console.WriteLine(number);
            }
        }

        /// <summary>
        /// Print only customers that have an order whos total is less than $500
        /// </summary>
        static void Exercise10()
        {
            var customers = DataLoader.LoadCustomers();

            foreach (var customer in customers)
            {
                decimal sumOrderTotal = 0;
                foreach (var order in customer.Orders)
                {
                    decimal orderAdd = sumOrderTotal + order.Total;
                    sumOrderTotal = orderAdd;   
                }
                if (sumOrderTotal < 500)
                {
                    Console.WriteLine("==============================================================================");
                    Console.WriteLine(customer.CompanyName);
                    Console.WriteLine(customer.Address);
                    Console.WriteLine("{0}, {1} {2} {3}", customer.City, customer.Region, customer.PostalCode, customer.Country);
                    Console.WriteLine("p:{0} f:{1}", customer.Phone, customer.Fax);
                    Console.WriteLine();
                    Console.WriteLine("\tOrders");
                    foreach (var order in customer.Orders)
                    {
                        Console.WriteLine("\t{0} {1:MM-dd-yyyy} {2,10:c}", order.OrderID, order.OrderDate, order.Total);
                    }
                    Console.WriteLine("Total orders:  {0:c}", sumOrderTotal);
                    Console.WriteLine();
                }
            }
        }

        /// <summary>
        /// Print only the first 3 odd numbers from NumbersC
        /// </summary>
        static void Exercise11()
        {
            List<int> numbersC = new List<int>(DataLoader.NumbersC);
            var onlyOdd = (from number in numbersC
                           where number % 2 != 0
                           select number).Take(3);

            foreach (var number in onlyOdd)
            {
                Console.WriteLine(number);
            }
        }

        /// <summary>
        /// Print the numbers from NumbersB except the first 3
        /// </summary>
        static void Exercise12()
        {
            List<int> numbersB = new List<int>(DataLoader.NumbersB);

            foreach (int number in numbersB.Skip(3))
            {
                Console.WriteLine(number);
            }
        }

        /// <summary>
        /// Print the Company Name and most recent order for each customer in Washington
        /// </summary>
        static void Exercise13()
        {
            var waRegion = DataLoader.LoadCustomers().Where(p => p.Region == "WA");

            //PrintCustomerInformation(waRegion);
            foreach (var customer in waRegion)
            {
                Console.WriteLine("==============================================================================");
                Console.WriteLine(customer.CompanyName);
                Console.WriteLine("\tOrders");
                foreach (var order in customer.Orders)
                {
                    var lastOrder = (from l in waRegion select l).Last();
                    Console.WriteLine("\t{0} {1:MM-dd-yyyy} {2,10:c}", order.OrderID, order.OrderDate, order.Total);
                    break;
                }
                Console.WriteLine("==============================================================================");
                Console.WriteLine();  
            }  
        }

        /// <summary>
        /// Print all the numbers in NumbersC until a number is >= 6
        /// </summary>
        static void Exercise14()
        {
            List<int> numbersC = new List<int>(DataLoader.NumbersC);

            var greaterThan6 = numbersC.TakeWhile(number => number < 6);

            foreach (int number in greaterThan6)
            {   
                    Console.WriteLine(number);  
            }
        }

        /// <summary>
        /// Print all the numbers in NumbersC that come after the first number divisible by 3
        /// </summary>
        static void Exercise15()
        {
            List<int> numbersC = new List<int>(DataLoader.NumbersC);

            var numDiv3 = numbersC.SkipWhile(number => number % 3 != 0);

            foreach (var number in numDiv3)
            {
                Console.WriteLine(number);
            }
        }

        /// <summary>
        /// Print the products alphabetically by name
        /// </summary>
        static void Exercise16()
        {
            List<Product> products = DataLoader.LoadProducts();

            var productNameOrder = from product in DataLoader.LoadProducts()
                                    orderby product.ProductName ascending
                                    select product;

            PrintProductInformation(productNameOrder);
        }

        /// <summary>
        /// Print the products in descending order by units in stock
        /// </summary>
        static void Exercise17()
        {
            List<Product> products = DataLoader.LoadProducts();
            
            var inStock = from product in DataLoader.LoadProducts()
                          orderby product.UnitsInStock descending
                          select product;

            PrintProductInformation(inStock);
        }

        /// <summary>
        /// Print the list of products ordered first by category, then by unit price, from highest to lowest.
        /// </summary>
        static void Exercise18()
        {
            List<Product> products = DataLoader.LoadProducts();

            var orderPrice = from product in DataLoader.LoadProducts()
                          orderby product.UnitPrice descending
                          select product;

            var category = from product in orderPrice
                           group product by product.Category into newgroup
                           orderby newgroup.Key
                           select newgroup;

            foreach (var group in category)
            {
                Console.WriteLine("");
                Console.WriteLine(group.Key);
                Console.WriteLine("----------");
                
                foreach (var product in group)
                {
                    PrintProductInformation(group);
                    break;
                }
            }
        }

        /// <summary>
        /// Print NumbersB in reverse order
        /// </summary>
        static void Exercise19()
        {
            List<int> numbersB = new List<int>(DataLoader.NumbersB);

            IQueryable<int> revNumB = numbersB.AsQueryable().Reverse();

            foreach (int number in revNumB)
            {
                Console.WriteLine(number);
            }
        }

        /// <summary>
        /// Group products by category, then print each category name and its products
        /// ex:
        /// 
        /// Beverages
        /// Tea
        /// Coffee
        /// 
        /// Sandwiches
        /// Turkey
        /// Ham
        /// </summary>
        static void Exercise20()
        {
            List<Product> products = DataLoader.LoadProducts();

            var category = from product in DataLoader.LoadProducts()
                           group product by product.Category into newgroup
                           orderby newgroup.Key
                           select newgroup;

            foreach (var group in category)
            {
                Console.WriteLine("");
                Console.WriteLine(group.Key);
                Console.WriteLine("----------");

                foreach (var product in group)
                {
                    Console.WriteLine(product.ProductName);
                }
            }
        }

        /// <summary>
        /// Print all Customers with their orders by Year then Month
        /// ex:
        /// 
        /// Joe's Diner
        /// 2015
        ///     1 -  $500.00
        ///     3 -  $750.00
        /// 2016
        ///     2 - $1000.00
        /// </summary>
        static void Exercise21()
        {
            var customers = DataLoader.LoadCustomers();

            foreach (var customer in customers)
            {
                Console.WriteLine(customer.CompanyName);

                int currentYear = 0 ;

                foreach (var order in customer.Orders)

                {
                    //    if (currentYear != order.OrderDate.Year)
                    //    {
                    //        currentYear = order.OrderDate.Year;
                    //        Console.WriteLine("{0:yyyy}", order.OrderDate);
                    //    }

                    //    Console.WriteLine("{0:MM} {1,10:c}", order.OrderDate, order.Total);

                    //________________________________________

                    List<int> cYear = new List<int>();
                   
                    var cDate =
                        from order2 in customer.Orders
                        group order2 by  new
                        { order.OrderDate.Year 
                        } into newGroup 
                        orderby newGroup.Key
                        select newGroup;

                    foreach (var group in cDate)
                    {
                        if (order.OrderDate.Year != currentYear)
                        { 
                            currentYear = order.OrderDate.Year;
                            Console.WriteLine("{0}", currentYear);
                        }

                        foreach (var yGroup in cDate)
                        {
                            Console.WriteLine("{0:MM} {1,10:c}", order.OrderDate, order.Total);
                        }
                    }
                }
                Console.WriteLine();
            }
        }

         

/// <summary>
/// Print the unique list of product categories
/// </summary>
static void Exercise22()
        {
            List<Product> products = DataLoader.LoadProducts();

            var category = from product in DataLoader.LoadProducts()
                           group product by product.Category into newgroup
                           orderby newgroup.Key
                           select newgroup;

            foreach (var group in category)
            {
                Console.WriteLine("");
                Console.WriteLine(group.Key);
                Console.WriteLine("----------");
            }
        }

        /// <summary>
        /// Write code to check to see if Product 789 exists
        /// </summary>
        static void Exercise23()
        {
            List<Product> products = DataLoader.LoadProducts();

            var product789 = from product in DataLoader.LoadProducts()
                                       where product.ProductID == 789
                                       select product;

            bool hasElements = product789.Any();
            Console.WriteLine("The product list {0} empty.",
                                hasElements ? "is not" : "is");

            if (product789 != null)
            {
                foreach (var product in product789)
                {
                    PrintProductInformation(product789);
                }
            }
        }

        /// <summary>
        /// Print a list of categories that have at least one product out of stock
        /// </summary>
        static void Exercise24()
        {
            List<Product> products = DataLoader.LoadProducts();

            var productOutOfStock = from product in DataLoader.LoadProducts()
                                    where product.UnitsInStock == 0
                                    orderby product.Category
                                    select product;

            string pOOS = null;

            foreach (var product in productOutOfStock)
            {
                    if (pOOS != product.Category)
                {
                    pOOS = product.Category;
                    Console.WriteLine("{0}", product.Category);
                }
            }
        }

        /// <summary>
        /// Print a list of categories that have no products out of stock
        /// </summary>
        static void Exercise25()
        {
            List<Product> products = DataLoader.LoadProducts();

            var productInStock = from product in DataLoader.LoadProducts()
                                 where product.UnitsInStock > 0
                                 orderby product.Category
                                 select product;

            bool hasElements = productInStock.Any();
            Console.WriteLine("The product list {0} empty.",
                                hasElements ? "is not" : "is");

            if (productInStock != null)
            {
                PrintProductInformation(productInStock);
            }
        }

        /// <summary>
        /// Count the number of odd numbers in NumbersA
        /// </summary>
        static void Exercise26()
        {
            List<int> numbersA = new List<int>(DataLoader.NumbersA);

            int count = (from number in numbersA
                        where number % 2 != 0
                        select number).Count();

            Console.WriteLine(count);
        }

        /// <summary>
        /// Create and print an anonymous type containing CustomerId and the count of their orders
        /// </summary>
        static void Exercise27()
        {
            var customers = from cID in DataLoader.LoadCustomers()
                            select new
                            {
                                cID = cID.CustomerID,
                                Count = cID.Orders
                            };
            foreach (var customerID in customers)
            {
                Console.WriteLine("{0}", customerID.cID);
                
                List<Customer> custOrder = new List<Customer>();

                foreach (var order in customerID.Count)
                {
                    custOrder.Add(new Customer());
                }
                int count = (from number in custOrder
                             select number).Count();

                Console.WriteLine(count);
            }
        }

        /// <summary>
        /// Print a distinct list of product categories and the count of the products they contain
        /// </summary>
        static void Exercise28()
        {
            List<Product> products = DataLoader.LoadProducts();

            var category = from product in DataLoader.LoadProducts()
                           group product by product.Category into newgroup
                           orderby newgroup.Key
                           select newgroup;

            foreach (var group in category)
            {
                Console.WriteLine("");
                Console.WriteLine(group.Key);
                Console.WriteLine("----------");

                List<Product> productCount = new List<Product>();
                foreach (var product in group)
                {
                    productCount.Add(new Product());
                    //Console.WriteLine(product.ProductName);
                }
                int count = (from number in productCount
                             select number).Count();

                Console.WriteLine(count);
            }
        }

        /// <summary>
        /// Print a distinct list of product categories and the total units in stock
        /// </summary>
        static void Exercise29()
        {
            List<Product> products = DataLoader.LoadProducts();

            var units = from p in products
                        group p by p.Category into categories
                        select new 
                        {
                            categoryName = categories.Key,
                            unitsInStock = categories.Sum(x => x.UnitsInStock)
                        };

            foreach (var group in units)
            {
                Console.WriteLine("");
                Console.WriteLine(group.categoryName);
                Console.WriteLine(group.unitsInStock);
                Console.WriteLine("----------");
            }
        }

        /// <summary>
        /// Print a distinct list of product categories and the lowest priced product in that category
        /// </summary>
        static void Exercise30()
        {
            List<Product> products = DataLoader.LoadProducts();

            var units = from p in products
                        group p by p.Category into categories
                        select new
                        {
                            categoryName = categories.Key,
                            lowestPrice = categories.Min(x => x.UnitPrice)
                        };

            foreach (var group in units)
            {
                Console.WriteLine("");
                Console.WriteLine(group.categoryName);
                Console.WriteLine("{0:c}", group.lowestPrice);
                Console.WriteLine("----------");
            }
        }

        /// <summary>
        /// Print the top 3 categories by the average unit price of their products
        /// </summary>
        static void Exercise31()
        {
            List<Product> products = DataLoader.LoadProducts();

            var units = from p in products
                        group p by p.Category into categories
                        select new
                        {
                            categoryName = categories.Key,
                            avgPrice = categories.Average(x => x.UnitPrice)
                        };

            foreach (var group in units.Take(3))
            {
                Console.WriteLine("");
                Console.WriteLine(group.categoryName);
                Console.WriteLine("{0:c}", group.avgPrice);
                Console.WriteLine("----------");
            }
        }
    }
}
