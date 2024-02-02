using Infrastructure.Entities;
using Infrastructure.Entities.ProductCatalog;
using Infrastructure.Services;
using Infrastructure.Services.ProductServices;
using System.Text.RegularExpressions;

namespace Presentation.Services;

public class MenuService(UserService userService, TaskService taskService, AssignmentService assignmentService, ProductService productService, CustomerService customerService, PurchaseService purchaseService, CategoryService categoryService, ManufacturerService manufacturerService)
{
    private readonly UserService _userService = userService;
    private readonly TaskService _taskService = taskService;
    private readonly AssignmentService _assignmentService = assignmentService;
    private readonly ProductService _productService = productService;
    private readonly CustomerService _customerService = customerService;
    private readonly PurchaseService _purchaseService = purchaseService;
    private readonly CategoryService _categoryService = categoryService;
    private readonly ManufacturerService _manufacturerService = manufacturerService;

    public void NavigationMenu()
    {
        Console.Clear();

        Console.WriteLine("1. Show task menu");
        Console.WriteLine("");
        Console.WriteLine("2. Show product menu");
        Console.WriteLine("");
        Console.WriteLine("3. Exit");
        Console.WriteLine("");
        Console.Write("Enter your choice: ");

        switch (Console.ReadLine())
        {
            case "1":
                ShowMenu();
                break;
            case "2":
                ShowProductMenu();
                break;
            case "3":
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("Invalid input, please try again");
                NavigationMenu();
                break;
        }
    }


    // All menus below are for the product part of the application
    public void ShowProductMenu()
    {
        Console.Clear();

        Console.WriteLine("1. Add a new product");
        Console.WriteLine("");
        Console.WriteLine("2. Show all products");
        Console.WriteLine("");
        Console.WriteLine("3. Create customer");
        Console.WriteLine("");
        Console.WriteLine("4. Show all customers");
        Console.WriteLine("");
        Console.WriteLine("5. Purchase menu");
        Console.WriteLine("");
        Console.WriteLine("6. Update a product or customer");
        Console.WriteLine("");
        Console.WriteLine("7. Delete a product or customer");
        Console.WriteLine("");
        Console.WriteLine("8. Exit to navigation menu");
        Console.WriteLine("");
        Console.Write("Enter your choice: ");

        switch (Console.ReadLine())
        {
            case "1":
                CreateProductMenu();
                break;
            case "2":
                ShowAllProductsMenu();
                break;
            case "3":
                CreateCustomerMenu();
                break;
            case "4":
                ShowAllCustomersMenu();
                break;
            case "5":
                ShowPurchaseMenu();
                break;
            case "6":
                UpdateProductOrCustomerMenu();
                break;
            case "7":
                DeleteProductOrCustomerMenu();
                break;
            case "8":
                NavigationMenu();
                break;
            default:
                Console.WriteLine("Invalid input, please try again");
                Console.WriteLine("press any key to go back");
                Console.ReadKey();
                ShowProductMenu();
                break;
        }
    }

    public void CreateProductMenu()
    {
        Console.Clear();
        Console.WriteLine("Enter the Article number");
        var articleNumber = Console.ReadLine()!;
        if (string.IsNullOrEmpty(articleNumber))
        {
            Console.WriteLine("Article number cannot be empty. Please enter a valid article number.");
            Console.WriteLine("Press any key to go back");
            Console.ReadKey();
            CreateProductMenu();
        }
        var existingProduct = _productService.GetProductByArticleNumber(articleNumber);
        if (existingProduct != null)
        {
            Console.WriteLine("The provided article number already exists. Please provide a different article number.");
            Console.WriteLine("Press any key to go back");
            Console.ReadKey();
            CreateProductMenu();
        }
        Console.WriteLine("");

        Console.WriteLine("Enter the name of the product");
        var productName = Console.ReadLine()!;
        if (string.IsNullOrEmpty(productName))
        {
            Console.WriteLine("Product name cannot be empty. Please enter a valid product name.");
            Console.WriteLine("Press any key to go back");
            Console.ReadKey();
            CreateProductMenu();
        }
        Console.WriteLine("");

        Console.WriteLine("Enter product description, this is not necessary");
        var productDescription = Console.ReadLine()!;
        Console.WriteLine("");

        Console.WriteLine("Enter the price of the product");
        string priceInput = Console.ReadLine()!;
        decimal productPrice;
        if (string.IsNullOrEmpty(priceInput))
        {
            Console.WriteLine("Price cannot be empty. Please enter a valid price.");
            Console.WriteLine("Press any key to go back");
            Console.ReadKey();
            CreateProductMenu();
        }
        if (!decimal.TryParse(priceInput, out productPrice))
        {
            Console.WriteLine("Invalid price format. Please enter a valid price.");
            Console.WriteLine("Press any key to go back");
            Console.ReadKey();
            CreateProductMenu();
        }
        if (productPrice < 0)
        {
            Console.WriteLine("Price cannot be negative. Please enter a valid price.");
            Console.WriteLine("Press any key to go back");
            Console.ReadKey();
            CreateProductMenu();
        }
        Console.WriteLine("");

        Console.WriteLine("Enter the category of the product");
        var productCategory = Console.ReadLine()!;
        if (string.IsNullOrEmpty(productCategory))
        {
            Console.WriteLine("Product category cannot be empty. Please enter a valid product category.");
            Console.WriteLine("Press any key to go back");
            Console.ReadKey();
            CreateProductMenu();
        }
        Console.WriteLine("");

        Console.WriteLine("Enter the manufacturer of the product");
        var productManufacturer = Console.ReadLine()!;
        if (string.IsNullOrEmpty(productManufacturer))
        {
            Console.WriteLine("Product manufacturer cannot be empty. Please enter a valid product manufacturer.");
            Console.WriteLine("Press any key to go back");
            Console.ReadKey();
            CreateProductMenu();
        }

        var product = new Product
        {
            ArticleNumber = articleNumber,
            ProductName = productName,
            Description = productDescription,
            Price = productPrice
        };

        var category = new Category
        {
            CategoryName = productCategory
        };

        var manufacturer = new Manufacturer
        {
            ManufacturerName = productManufacturer
        };

        if (_productService.AddProduct(product, manufacturer, category))
        {
            Console.WriteLine("Product created successfully");
        }
        else
        {
            Console.WriteLine("Product already exists, try again");
        }
        Console.WriteLine("");

        Console.WriteLine("Press any key to go to main menu");
        Console.ReadKey();
        ShowProductMenu();
    }

    public void ShowAllProductsMenu()
    {
        Console.Clear();

        Console.WriteLine("1. Show all products");
        Console.WriteLine("");
        Console.WriteLine("2. Show all products by category");
        Console.WriteLine("");
        Console.WriteLine("3. Show all products by Manufacturer");
        Console.WriteLine("");
        Console.WriteLine("4. Go back to main menu");
        Console.WriteLine("");
        Console.Write("Enter your choice: ");

        switch (Console.ReadLine())
        {
            case "1":
                ShowAllProductsRegular();
                break;
            case "2":
                ShowAllProductsByCategory();
                break;
            case "3":
                ShowAllProductsByManufacturer();
                break;
            case "4":
                ShowProductMenu();
                break;
            default:
                Console.WriteLine("Invalid input, please try again");
                Console.WriteLine("press any key to go back");
                Console.ReadKey();
                ShowAllProductsMenu();
                break;
        }
    }

    public void ShowAllProductsRegular()
    {
        Console.Clear();

        var products = _productService.GetAllProducts();
        if (products == null)
        {
            Console.WriteLine("There are no products in the database");
            Console.WriteLine("");
            Console.WriteLine("Press any key to go to main menu");
            Console.ReadKey();
            ShowProductMenu();
        }
        else
        {
            foreach (var product in products)
            {
                Console.WriteLine($"Article number: {product.ArticleNumber}");
                Console.WriteLine($"Product: {product.ProductName}");
                Console.WriteLine($"price: {product.Price} SEK");
                Console.WriteLine($"Category: {product.Category.CategoryName}");
                Console.WriteLine($"Manufacturer: {product.Manufacturer.ManufacturerName}");
                Console.WriteLine($"Description: {product.Description}");
                Console.WriteLine("--------------------------------------------------------------------------------------");
            }
        }

        Console.WriteLine("");
        Console.WriteLine("Press any key to go to main menu");
        Console.ReadKey();
        ShowProductMenu();
    }

    public void ShowAllProductsByCategory()
    {
        Console.Clear();

        Console.WriteLine("Enter the category of the products you want to see");
        var productCategory = Console.ReadLine()!;
        if (string.IsNullOrEmpty(productCategory))
        {
            Console.WriteLine("Product category cannot be empty. Please enter a valid product category.");
            Console.WriteLine("Press any key to go back");
            Console.ReadKey();
            ShowAllProductsByCategory();
        }

        var products = _productService.GetProductsByCategory(productCategory);
        if (products == null)
        {
            Console.WriteLine("There are no products in the database");
            Console.WriteLine("");
            Console.WriteLine("Press any key to go to main menu");
            Console.ReadKey();
            ShowProductMenu();
        }
        else
        {
            Console.WriteLine($"The category: {productCategory} has these products:");
            Console.WriteLine("");
            foreach (var product in products)
            {
                Console.WriteLine($"Article number: {product.ArticleNumber}");
                Console.WriteLine($"Product: {product.ProductName} with the article number: {product.ArticleNumber} has the price: {product.Price} SEK");
                Console.WriteLine($"Price: {product.Price} SEK");
                Console.WriteLine($"Description: {product.Description}");
                Console.WriteLine($"Manufacturer: {product.Manufacturer.ManufacturerName}");
                Console.WriteLine("--------------------------------------------------------------------------------------");
            }
        }

        Console.WriteLine("");
        Console.WriteLine("Press any key to go to main menu");
        Console.ReadKey();
        ShowProductMenu();
    }

    public void ShowAllProductsByManufacturer()
    {
        Console.Clear();

        Console.WriteLine("Enter the manufacturer of the products you want to see");
        var productManufacturer = Console.ReadLine()!;
        if (string.IsNullOrEmpty(productManufacturer))
        {
            Console.WriteLine("Product manufacturer cannot be empty. Please enter a valid product manufacturer.");
            Console.WriteLine("Press any key to go back");
            Console.ReadKey();
            ShowAllProductsByManufacturer();
        }

        var products = _productService.GetProductsByManufacturer(productManufacturer);
        if (products == null)
        {
            Console.WriteLine("There are no products in the database");
            Console.WriteLine("");
            Console.WriteLine("Press any key to go to main menu");
            Console.ReadKey();
            ShowProductMenu();
        }
        else
        {
            Console.WriteLine($"The manufacturer: {productManufacturer} has these products:");
            Console.WriteLine("");
            foreach (var product in products)
            {
                Console.WriteLine($"Article number: {product.ArticleNumber}");
                Console.WriteLine($"Product: {product.ProductName}");
                Console.WriteLine($"Price: {product.Price} SEK");
                Console.WriteLine($"Description: {product.Description}");
                Console.WriteLine($"Category: {product.Category.CategoryName}");
                Console.WriteLine("--------------------------------------------------------------------------------------");
            }
        }

        Console.WriteLine("");
        Console.WriteLine("Press any key to go to main menu");
        Console.ReadKey();
        ShowProductMenu();
    }

    public void CreateCustomerMenu()
    {
        Console.Clear();
        Console.WriteLine("Enter Customers First Name");
        var customerFirstName = Console.ReadLine()!;
        if (string.IsNullOrEmpty(customerFirstName))
        {
            Console.WriteLine("Customers first name cannot be empty. Please enter a valid customer name.");
            Console.WriteLine("Press any key to go back");
            Console.ReadKey();
            CreateCustomerMenu();
        }
        Console.WriteLine("");

        Console.WriteLine("Enter Customers Last Name");
        var customerLastName = Console.ReadLine()!;
        if (string.IsNullOrEmpty(customerLastName))
        {
            Console.WriteLine("Customers last name cannot be empty. Please enter a valid customer name.");
            Console.WriteLine("Press any key to go back");
            Console.ReadKey();
            CreateCustomerMenu();
        }
        Console.WriteLine("");

        Console.WriteLine("Enter Customers Email");
        var customerEmail = Console.ReadLine()!;
        if (string.IsNullOrEmpty(customerEmail))
        {
            Console.WriteLine("Customers email cannot be empty. Please enter a valid customer email.");
            Console.WriteLine("Press any key to go back");
            Console.ReadKey();
            CreateCustomerMenu();
        }
        var emailRegex = new Regex(@"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$");
        if (!emailRegex.IsMatch(customerEmail))
        {
            Console.WriteLine("Invalid email format. Please enter a valid user email.");
            Console.WriteLine("Press any key to go back");
            Console.ReadKey();
            CreateUserMenu();
        }
        var existingCustomer = _customerService.GetCustomerByEmail(customerEmail);
        if (existingCustomer != null)
        {
            Console.WriteLine("The provided email already exists in the database. Please provide a different email.");
            Console.WriteLine("Press any key to go back");
            Console.ReadKey();
            CreateCustomerMenu();
        }
        Console.WriteLine("");

        Console.WriteLine("Enter Customer Phone number, this is not necessary");
        var customerPhoneNumber = Console.ReadLine()!;
        Console.WriteLine("");

        var customer = new Customer
        {
            FirstName = customerFirstName,
            LastName = customerLastName,
            Email = customerEmail,
            Phone = customerPhoneNumber
        };

        if (_customerService.AddCustomer(customer))
        {
            Console.WriteLine("Customer created successfully");
        }
        else
        {
            Console.WriteLine("Customer already exists, try again");
        }
        Console.WriteLine("");

        Console.WriteLine("Press any key to go to main menu");
        Console.ReadKey();
        ShowProductMenu();


    }

    public void ShowAllCustomersMenu()
    {
        Console.Clear();

        var customers = _customerService.GetAllCustomers();
        if (customers == null)
        {
            Console.WriteLine("There are no customers in the database");
            Console.WriteLine("");
            Console.WriteLine("Press any key to go to main menu");
            Console.ReadKey();
            ShowProductMenu();
        }
        else
        {
            foreach (var customer in customers)
            {
                Console.WriteLine($"Customer: {customer.FirstName} {customer.LastName} with the email: {customer.Email} and the phone number: {customer.Phone}");
                Console.WriteLine("--------------------------------------------------------------------------------------");
            }
        }

        Console.WriteLine("");
        Console.WriteLine("Press any key to go to main menu");
        Console.ReadKey();
        ShowProductMenu();

    }

    public void ShowPurchaseMenu()
    {
        Console.Clear();

        Console.WriteLine("1. Make a purchase");
        Console.WriteLine("");
        Console.WriteLine("2. Show a customers purchases");
        Console.WriteLine("");
        Console.WriteLine("3. Go back to main menu");
        Console.WriteLine("");
        Console.Write("Enter your choice: ");

        switch (Console.ReadLine())
        {
            case "1":
                MakePurchaseMenu();
                break;
            case "2":
                ShowCustomersPurchasesMenu();
                break;
            case "3":
                ShowProductMenu();
                break;
            default:
                Console.WriteLine("Invalid input, please try again");
                Console.WriteLine("press any key to go back");
                Console.ReadKey();
                ShowPurchaseMenu();
                break;
        }
    }

    public void MakePurchaseMenu()
    {
        Console.Clear();

        Console.WriteLine("Enter the email of the customer making the purchase");
        var customerEmail = Console.ReadLine()!;
        if (string.IsNullOrEmpty(customerEmail))
        {
            Console.WriteLine("Customer email cannot be empty. Please enter a valid customer email.");
            Console.WriteLine("Press any key to go back");
            Console.ReadKey();
            MakePurchaseMenu();
        }
        var customer = _customerService.GetCustomerByEmail(customerEmail);
        if (customer == null)
        {
            Console.WriteLine("Customer does not exist, try again");
            Console.WriteLine("");
            Console.WriteLine("Press any key to go to purchase menu");
            Console.ReadKey();
            ShowPurchaseMenu();
        }
        Console.WriteLine("");

        Console.WriteLine("Enter the article number of the product being purchased");
        var articleNumber = Console.ReadLine()!;
        if (string.IsNullOrEmpty(articleNumber))
        {
            Console.WriteLine("Article number cannot be empty. Please enter a valid article number.");
            Console.WriteLine("Press any key to go back");
            Console.ReadKey();
            MakePurchaseMenu();
        }
        var product = _productService.GetProductByArticleNumber(articleNumber);
        if (product == null)
        {
            Console.WriteLine("Product does not exist, try again");
            Console.WriteLine("");
            Console.WriteLine("Press any key to go to purchase menu");
            Console.ReadKey();
            ShowPurchaseMenu();
        }
        Console.WriteLine("");

        Console.WriteLine("Enter the quantity of the product being purchased");
        string quantityInput = Console.ReadLine()!;
        int quantity;
        if (string.IsNullOrEmpty(quantityInput))
        {
            Console.WriteLine("Quantity cannot be empty. Please enter a valid quantity.");
            Console.WriteLine("Press any key to go back");
            Console.ReadKey();
            MakePurchaseMenu();
        }
        if (!int.TryParse(quantityInput, out quantity))
        {
            Console.WriteLine("Invalid quantity format. Please enter a valid quantity.");
            Console.WriteLine("Press any key to go back");
            Console.ReadKey();
            MakePurchaseMenu();
        }
        if (quantity < 0)
        {
            Console.WriteLine("Quantity cannot be negative. Please enter a valid quantity.");
            Console.WriteLine("Press any key to go back");
            Console.ReadKey();
            MakePurchaseMenu();
        }
        Console.WriteLine("");

        var purchase = new Purchase
        {
            CustomerId = customer!.CustomerId,
            ProductArticleNumber = product!.ArticleNumber,
            Quantity = quantity,
            UnitPrice = product.Price
        };

        if (_purchaseService.AddPurchase(purchase, customer, product))
        {
            Console.WriteLine("Purchase created successfully");
        }
        else
        {
            Console.WriteLine("Purchase already exists, try again");
        }
        Console.WriteLine("");

        Console.WriteLine("Press any key to go to main menu");
        Console.ReadKey();
        ShowProductMenu();

    }

    public void ShowCustomersPurchasesMenu()
    {
        Console.Clear();

        Console.WriteLine("Enter the email of the customer you wish to see the purchases of");
        var customerEmail = Console.ReadLine()!;
        if (string.IsNullOrEmpty(customerEmail))
        {
            Console.WriteLine("Customer email cannot be empty. Please enter a valid customer email.");
            Console.WriteLine("Press any key to go back");
            Console.ReadKey();
            ShowCustomersPurchasesMenu();
        }
        var customer = _customerService.GetCustomerByEmail(customerEmail);
        if (customer == null)
        {
            Console.WriteLine("Customer does not exist, try again");
            Console.WriteLine("");
            Console.WriteLine("Press any key to go to purchase menu");
            Console.ReadKey();
            ShowPurchaseMenu();
        }
        else
        {
            var purchases = _purchaseService.GetPurchasesbyCustomer(customer.CustomerId);
            if (purchases == null || !purchases.Any())
            {
                Console.WriteLine("This customer hasn't made any purchases.");
            }
            else
            {
                Console.WriteLine($"Customer: {customer.FirstName} {customer.LastName} with the email: {customer.Email} and the phone number: {customer.Phone} has made these purchases:");
                Console.WriteLine("");
                foreach (var purchase in purchases)
                {
                    Console.WriteLine($"Purchased: {purchase.Quantity} units of the product with the article number: {purchase.ProductArticleNumber} for the price of: {purchase.UnitPrice} SEK per unit");
                    Console.WriteLine($"Total price: {purchase.Quantity * purchase.UnitPrice} SEK");
                    Console.WriteLine("--------------------------------------------------------------------------------------");
                }
            }
        }
        Console.WriteLine("");

        Console.WriteLine("Press any key to go to main menu");
        Console.ReadKey();
        ShowProductMenu();
    }

    public void UpdateProductOrCustomerMenu()
    {
        Console.Clear();

        Console.WriteLine("1. Update a Product");
        Console.WriteLine("");
        Console.WriteLine("2. Update a Customer");
        Console.WriteLine("");
        Console.WriteLine("3. Go back to main menu");
        Console.WriteLine("");

        switch (Console.ReadLine())
        {
            case "1":
                UpdateProductMenu();
                break;
            case "2":
                UpdateCustomerMenu();
                break;
            case "3":
                ShowProductMenu();
                break;
            default:
                Console.WriteLine("Invalid input, please try again");
                Console.WriteLine("press any key to go back");
                Console.ReadKey();
                UpdateProductOrCustomerMenu();
                break;
        }

    }

    public void UpdateProductMenu()
    {
        Console.Clear();

        Console.WriteLine("Enter the product article number");
        var articleNumber = Console.ReadLine()!;
        if (string.IsNullOrEmpty(articleNumber))
        {
            Console.WriteLine("Article number cannot be empty. Please enter a valid article number.");
            Console.WriteLine("Press any key to go back");
            Console.ReadKey();
            UpdateProductMenu();
        }
        Console.WriteLine("");

        var product = _productService.GetProductByArticleNumber(articleNumber);
        if (product == null)
        {
            Console.WriteLine("Product does not exist, try again");
            Console.WriteLine("");
            Console.WriteLine("Press any key to go to update menu");
            Console.ReadKey();
            UpdateProductOrCustomerMenu();
        }
        Console.WriteLine("");

        Console.WriteLine("Enter new product name");
        var newProductName = Console.ReadLine()!;
        if (string.IsNullOrEmpty(newProductName))
        {
            Console.WriteLine("Product name cannot be empty. Please enter a valid product name.");
            Console.WriteLine("Press any key to go back");
            Console.ReadKey();
            UpdateProductMenu();
        }
        Console.WriteLine("");

        Console.WriteLine("Enter new product description, this is not necessary");
        var newProductDescription = Console.ReadLine()!;
        Console.WriteLine("");

        Console.WriteLine("Enter new product price");
        string priceInput = Console.ReadLine()!;
        decimal newProductPrice;
        if (string.IsNullOrEmpty(priceInput))
        {
            Console.WriteLine("Price cannot be empty. Please enter a valid price.");
            Console.WriteLine("Press any key to go back");
            Console.ReadKey();
            UpdateProductMenu();
        }
        if (!decimal.TryParse(priceInput, out newProductPrice))
        {
            Console.WriteLine("Invalid price format. Please enter a valid price.");
            Console.WriteLine("Press any key to go back");
            Console.ReadKey();
            UpdateProductMenu();
        }
        Console.WriteLine("");

        Console.WriteLine("Enter new product category (leave blank to keep current category)");
        var newProductCategory = Console.ReadLine();
        if (!string.IsNullOrEmpty(newProductCategory) && product?.Category != null)
        {
            product.Category.CategoryName = newProductCategory;
        }
        Console.WriteLine("");

        Console.WriteLine("Enter new product manufacturer (leave blank to keep current manufacturer)");
        var newProductManufacturer = Console.ReadLine();
        if (!string.IsNullOrEmpty(newProductManufacturer) && product?.Manufacturer != null)
        {
            product.Manufacturer.ManufacturerName = newProductManufacturer;
        }
        Console.WriteLine("");

        product!.ProductName = newProductName;
        product.Description = newProductDescription;
        product.Price = newProductPrice;

        if (_productService.UpdateProduct(product))
        {
            Console.WriteLine("Product updated successfully");
        }
        else
        {
            Console.WriteLine("An error occurred, try again");
        }

        Console.WriteLine("");
        Console.WriteLine("Press any key to go to main menu");
        Console.ReadKey();
        ShowProductMenu();
    }

    public void UpdateCustomerMenu()
    {
        Console.Clear();

        Console.WriteLine("Enter current customer email");
        var currentEmail = Console.ReadLine()!;
        if (string.IsNullOrEmpty(currentEmail))
        {
            Console.WriteLine("Customer email cannot be empty. Please enter a valid customer email.");
            Console.WriteLine("Press any key to go back");
            Console.ReadKey();
            UpdateCustomerMenu();
        }
        var customer = _customerService.GetCustomerByEmail(currentEmail);
        if (customer == null)
        {
            Console.WriteLine("Customer does not exist, try again");
            Console.WriteLine("");
            Console.WriteLine("Press any key to go to update menu");
            Console.ReadKey();
            UpdateProductOrCustomerMenu();
        }
        Console.WriteLine("");

        Console.WriteLine("Enter new customer first name");
        var newCustomerFirstName = Console.ReadLine()!;
        if (string.IsNullOrEmpty(newCustomerFirstName))
        {
            Console.WriteLine("Customer first name cannot be empty. Please enter a valid customer name.");
            Console.WriteLine("Press any key to go back");
            Console.ReadKey();
            UpdateCustomerMenu();
        }
        Console.WriteLine("");

        Console.WriteLine("Enter new customer last name");
        var newCustomerLastName = Console.ReadLine()!;
        if (string.IsNullOrEmpty(newCustomerLastName))
        {
            Console.WriteLine("Customer last name cannot be empty. Please enter a valid customer name.");
            Console.WriteLine("Press any key to go back");
            Console.ReadKey();
            UpdateCustomerMenu();
        }
        Console.WriteLine("");

        Console.WriteLine("Enter new customer email");
        var newCustomerEmail = Console.ReadLine()!;
        if (string.IsNullOrEmpty(newCustomerEmail))
        {
            Console.WriteLine("Customer email cannot be empty. Please enter a valid customer email.");
            Console.WriteLine("Press any key to go back");
            Console.ReadKey();
            UpdateCustomerMenu();
        }
        var emailRegex = new Regex(@"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$");
        if (!emailRegex.IsMatch(newCustomerEmail))
        {
            Console.WriteLine("Invalid email format. Please enter a valid user email.");
            Console.WriteLine("Press any key to go back");
            Console.ReadKey();
            CreateUserMenu();
        }
        if (_customerService.GetCustomerByEmail(newCustomerEmail) != null)
        {
            Console.WriteLine("Email already exists, try again");
            Console.WriteLine("");
            Console.WriteLine("Press any key to go to update menu");
            Console.ReadKey();
            UpdateProductOrCustomerMenu();
        }
        Console.WriteLine("");

        Console.WriteLine("Enter new customer phone number, this is not necessary");
        var newCustomerPhoneNumber = Console.ReadLine()!;
        Console.WriteLine("");

        customer!.FirstName = newCustomerFirstName;
        customer.LastName = newCustomerLastName;
        customer.Email = newCustomerEmail;
        customer.Phone = newCustomerPhoneNumber;

        if (_customerService.UpdateCustomer(customer))
        {
            Console.WriteLine("Customer updated successfully");
        }
        else
        {
            Console.WriteLine("An error occurred, try again");
        }

        Console.WriteLine("");
        Console.WriteLine("Press any key to go to main menu");
        Console.ReadKey();
        ShowProductMenu();


    }

    public void DeleteProductOrCustomerMenu()
    {
        Console.Clear();

        Console.WriteLine("1. Delete a Product");
        Console.WriteLine("");
        Console.WriteLine("2. Delete a Category (and all products related to it)");
        Console.WriteLine("");
        Console.WriteLine("3. Delete a Manufacturer (and all products related to it)");
        Console.WriteLine("");
        Console.WriteLine("4. Delete a Customer (and all purchases related to it)");
        Console.WriteLine("");
        Console.WriteLine("5. Go back to main menu");
        Console.WriteLine("");

        switch (Console.ReadLine())
        {
            case "1":
                DeleteProductMenu();
                break;
            case "2":
                DeleteCategoryMenu();
                break;
            case "3":
                DeleteManufacturerMenu();
                break;
            case "4":
                DeleteCustomerMenu();
                break;
            case "5":
                ShowProductMenu();
                break;
            default:
                Console.WriteLine("Invalid input, please try again");
                Console.WriteLine("press any key to go back");
                Console.ReadKey();
                DeleteProductOrCustomerMenu();
                break;
        }

    }

    public void DeleteProductMenu()
    {
        Console.Clear();

        Console.WriteLine("Enter the product article number");
        var articleNumber = Console.ReadLine()!;
        if (string.IsNullOrEmpty(articleNumber))
        {
            Console.WriteLine("Article number cannot be empty. Please enter a valid article number.");
            Console.WriteLine("Press any key to go back");
            Console.ReadKey();
            DeleteProductMenu();
        }
        Console.WriteLine("");

        var product = _productService.GetProductByArticleNumber(articleNumber);
        if (product == null)
        {
            Console.WriteLine("Product does not exist, try again");
            Console.WriteLine("");
            Console.WriteLine("Press any key to go to delete menu");
            Console.ReadKey();
            DeleteProductOrCustomerMenu();
        }
        Console.WriteLine("");

        if (_productService.DeleteProduct(articleNumber))
        {
            Console.WriteLine("Product deleted successfully");
        }
        else
        {
            Console.WriteLine("An error occurred, try again");
        }

        Console.WriteLine("");
        Console.WriteLine("Press any key to go to main menu");
        Console.ReadKey();
        ShowProductMenu();

    }

    public void DeleteCategoryMenu()
    {
        Console.Clear();
        Console.WriteLine("Enter the category you want to delete");
        var categoryName = Console.ReadLine()!;
        if (string.IsNullOrEmpty(categoryName))
        {
            Console.WriteLine("Category name cannot be empty. Please enter a valid category name.");
            Console.WriteLine("Press any key to go back");
            Console.ReadKey();
            DeleteCategoryMenu();
        }
        var category = _categoryService.GetCategoryByName(categoryName);
        if (category == null)
        {
            Console.WriteLine("Category does not exist, try again");
            Console.WriteLine("");
            Console.WriteLine("Press any key to go to delete menu");
            Console.ReadKey();
            DeleteProductOrCustomerMenu();
        }
        Console.WriteLine("");

        if (_categoryService.DeleteCategory(categoryName))
        {
            Console.WriteLine("Category deleted successfully");
        }
        else
        {
            Console.WriteLine("An error occurred, try again");
        }
        Console.WriteLine("");
        Console.WriteLine("Press any key to go to main menu");
        Console.ReadKey();
        ShowProductMenu();

    }

    public void DeleteManufacturerMenu()
    {
        Console.Clear();
        Console.WriteLine("Enter the manufacturer you want to delete");
        var manufacturerName = Console.ReadLine()!;
        if (string.IsNullOrEmpty(manufacturerName))
        {
            Console.WriteLine("Manufacturer name cannot be empty. Please enter a valid manufacturer name.");
            Console.WriteLine("Press any key to go back");
            Console.ReadKey();
            DeleteManufacturerMenu();
        }
        var manufacturer = _manufacturerService.GetManufacturerByName(manufacturerName);
        if (manufacturer == null)
        {
            Console.WriteLine("Manufacturer does not exist, try again");
            Console.WriteLine("");
            Console.WriteLine("Press any key to go to delete menu");
            Console.ReadKey();
            DeleteProductOrCustomerMenu();
        }
        Console.WriteLine("");

        if (_manufacturerService.DeleteManufacturer(manufacturerName))
        {
            Console.WriteLine("Manufacturer deleted successfully");
        }
        else
        {
            Console.WriteLine("An error occurred, try again");
        }
        Console.WriteLine("");
        Console.WriteLine("Press any key to go to main menu");
        Console.ReadKey();
        ShowProductMenu();

    }

    public void DeleteCustomerMenu()
    {
        Console.Clear();

        Console.WriteLine("Enter the customer email");
        var customerEmail = Console.ReadLine()!;
        if (string.IsNullOrEmpty(customerEmail))
        {
            Console.WriteLine("Customer email cannot be empty. Please enter a valid customer email.");
            Console.WriteLine("Press any key to go back");
            Console.ReadKey();
            DeleteCustomerMenu();
        }
        Console.WriteLine("");

        var customer = _customerService.GetCustomerByEmail(customerEmail);
        if (customer == null)
        {
            Console.WriteLine("Customer does not exist, try again");
            Console.WriteLine("");
            Console.WriteLine("Press any key to go to delete menu");
            Console.ReadKey();
            DeleteProductOrCustomerMenu();
        }
        Console.WriteLine("");

        if (_customerService.DeleteCustomer(customerEmail))
        {
            Console.WriteLine("Customer deleted successfully");
        }
        else
        {
            Console.WriteLine("An error occurred, try again");
        }

        Console.WriteLine("");
        Console.WriteLine("Press any key to go to main menu");
        Console.ReadKey();
        ShowProductMenu();

    }


    // All menus below are for the task part of the application
    public void ShowMenu()
    {
        Console.Clear();

        Console.WriteLine("1. Show all tasks");
        Console.WriteLine("");
        Console.WriteLine("2. Show all users");
        Console.WriteLine("");
        Console.WriteLine("3. Create User");
        Console.WriteLine("");
        Console.WriteLine("4. Add new task");
        Console.WriteLine("");
        Console.WriteLine("5. Update a task or a user");
        Console.WriteLine("");
        Console.WriteLine("6. Delete task or user");
        Console.WriteLine("");
        Console.WriteLine("7. Exit to navigation menu");
        Console.WriteLine("");
        Console.Write("Enter your choice: ");

        switch (Console.ReadLine())
        {
            case "1":
                ShowAllTasksMenu();
                break;
            case "2":
                ShowAllUsersMenu();
                break;
            case "3":
                CreateUserMenu();
                break;
            case "4":
                CreateTaskMenu();
                break;
            case "5":
                UpdateMenu();
                break;
            case "6":
                DeleteMenu();
                break;
            case "7":
                NavigationMenu();
                break;
            default:
                Console.WriteLine("Invalid input, please try again");
                Console.WriteLine("press any key to go back");
                Console.ReadKey();
                ShowMenu();
                break;
        }
    }

    public void ShowAllUsersMenu()
    {
        Console.Clear();

        var users = _userService.GetAllUsers();
        if (users == null)
        {
            Console.WriteLine("There are no users in the database");
            Console.WriteLine("");
            Console.WriteLine("Press any key to go to main menu");
            Console.ReadKey();
            ShowMenu();
        }
        else
        {
            foreach (var user in users)
            {
                Console.WriteLine($"User: {user.Name} with the email: {user.Email}");
                Console.WriteLine("--------------------------------------------------------------------------------------");
            }
        }

        Console.WriteLine("");
        Console.WriteLine("Press any key to go to main menu");
        Console.ReadKey();
        ShowMenu();
    }

    public void ShowAllTasksMenu()
    {
        Console.Clear();

        var assignments = _assignmentService.GetAllAssignments();
        if (assignments == null)
        {
            Console.WriteLine("There are no tasks in the database");
            Console.WriteLine("");
            Console.WriteLine("Press any key to go to main menu");
            Console.ReadKey();
            ShowMenu();
        }
        else
        {
            foreach (var assignment in assignments)
            {
                Console.WriteLine($"User: {assignment.User.Name} with the email: {assignment.User.Email} has the task: {assignment.Task.TaskName} with the status : {assignment.Task.Status.StatusName}");
                Console.WriteLine($"Description: {assignment.Task.Description}");
                Console.WriteLine($"Category: {assignment.Task.Category.CategoryName}");
                Console.WriteLine("--------------------------------------------------------------------------------------");
            }
        }

        Console.WriteLine("");
        Console.WriteLine("Press any key to go to main menu");
        Console.ReadKey();
        ShowMenu();

    }

    public void CreateTaskMenu()
    {
        Console.Clear();

        if (!_userService.AreThereAnyUsers())
        {
            Console.WriteLine("There are no users in the database, you need to create a user first");
            Console.WriteLine("");
            Console.WriteLine("Press any key to go to create user menu");
            Console.ReadKey();
            CreateUserMenu();
            return;
        }

        Console.WriteLine("");
        Console.WriteLine("Enter task name");
        var taskName = Console.ReadLine()!;
        if (string.IsNullOrEmpty(taskName))
        {
            Console.WriteLine("Task name cannot be empty. Please enter a valid task name.");
            Console.WriteLine("Press any key to go back");
            Console.ReadKey();
            CreateTaskMenu();
        }

        var existingTask = _taskService.GetTaskByName(taskName);
        if (existingTask != null)
        {
            Console.WriteLine("The provided task name already exists. Please provide a different task name.");
            Console.WriteLine("Press any key to go back");
            Console.ReadKey();
            CreateTaskMenu();
        }

        Console.WriteLine("");
        Console.WriteLine("Enter task description, this is not necessary");
        var taskDescription = Console.ReadLine()!;

        Console.WriteLine("");
        Console.WriteLine("Enter task status");
        var taskStatus = Console.ReadLine()!;
        if (string.IsNullOrEmpty(taskStatus))
        {
            Console.WriteLine("Task status cannot be empty. Please enter a valid task status.");
            CreateTaskMenu();
        }
        Console.WriteLine("");
        Console.WriteLine("Enter task category");
        var taskCategory = Console.ReadLine()!;
        if (string.IsNullOrEmpty(taskCategory))
        {
            Console.WriteLine("Task category cannot be empty. Please enter a valid task category.");
            CreateTaskMenu();
        }
        Console.WriteLine("");
        Console.WriteLine("Enter the email of the user with the task");
        var userEmail = Console.ReadLine()!;
        if (string.IsNullOrEmpty(userEmail))
        {
            Console.WriteLine("User email cannot be empty. Please enter a valid user email.");
            Console.WriteLine("Press any key to go back");
            Console.ReadKey();
            CreateTaskMenu();
        }
        else
        {
            var user = _userService.GetUserByEmail(userEmail);
            if (user == null)
            {
                Console.WriteLine("No user with the provided email exists. Please enter a valid user email.");
                Console.WriteLine("Press any key to go back");
                Console.ReadKey();
                CreateTaskMenu();
            }
        }
        Console.WriteLine("");


        var task = new TaskEntity
        {
            TaskName = taskName,
            Description = taskDescription
        };

        var status = new StatusEntity
        {
            StatusName = taskStatus
        };

        var category = new CategoryEntity
        {
            CategoryName = taskCategory
        };

        var userId = _userService.GetIdByEmail(userEmail);
        if (userId == 0)
        {
            Console.WriteLine("The provided email does not exist in the database. Please provide a valid email.");
            return;
        }


        if (_taskService.CreateTask(task, status, category, userId))
        {
            Console.WriteLine("Task created successfully");
        }
        else
        {
            Console.WriteLine("Task already exists, try again");
        }

        Console.WriteLine("");
        Console.WriteLine("Press any key to go to main menu");
        Console.ReadKey();
        ShowMenu();

    }

    public void CreateUserMenu()
    {
        Console.Clear();

        Console.WriteLine("Enter user name");
        var userName = Console.ReadLine()!;
        if (string.IsNullOrEmpty(userName))
        {
            Console.WriteLine("User name cannot be empty. Please enter a valid user name.");
            Console.WriteLine("Press any key to go back");
            Console.ReadKey();
            CreateUserMenu();
        }
        Console.WriteLine("Enter user email");
        var userEmail = Console.ReadLine()!;

        if (string.IsNullOrEmpty(userEmail))
        {
            Console.WriteLine("User email cannot be empty. Please enter a valid user email.");
            Console.WriteLine("Press any key to go back");
            Console.ReadKey();
            CreateUserMenu();
        }

        var emailRegex = new Regex(@"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$");
        if (!emailRegex.IsMatch(userEmail))
        {
            Console.WriteLine("Invalid email format. Please enter a valid user email.");
            Console.WriteLine("Press any key to go back");
            Console.ReadKey();
            CreateUserMenu();
        }

        var existingUser = _userService.GetUserByEmail(userEmail);
        if (existingUser != null)
        {
            Console.WriteLine("The provided email already exists in the database. Please provide a different email.");
            Console.WriteLine("Press any key to go back");
            Console.ReadKey();
            CreateUserMenu();
        }

        var user = new UserEntity
        {
            Name = userName,
            Email = userEmail
        };

        if (_userService.CreateUser(user))
        {
            Console.WriteLine("User created successfully");
        }
        else
        {
            Console.WriteLine("User already exists, try again");
        }

        Console.WriteLine("");
        Console.WriteLine("Press any key to go to main menu");
        Console.ReadKey();
        ShowMenu();
    }

    public void UpdateMenu()
    {
        Console.Clear();

        Console.WriteLine("1. Update User");
        Console.WriteLine("2. Update Task");
        Console.WriteLine("3. Go back to main menu");
        Console.Write("Enter your choice: ");

        switch (Console.ReadLine())
        {
            case "1":
                UpdateUserMenu();
                break;
            case "2":
                UpdateTaskMenu();
                break;
            case "3":
                ShowMenu();
                break;
            default:
                Console.WriteLine("Invalid input, please try again");
                Console.WriteLine("press any key to go back");
                Console.ReadKey();
                UpdateMenu();
                break;
        }
    }

    public void UpdateUserMenu()
    {
        Console.Clear();

        Console.WriteLine("Enter current user email");
        var currentEmail = Console.ReadLine()!;
        if (string.IsNullOrEmpty(currentEmail))
        {
            Console.WriteLine("User email cannot be empty. Please enter a valid user email.");
            UpdateUserMenu();
        }

        var user = _userService.GetUserByEmail(currentEmail);
        if (user == null)
        {
            Console.WriteLine("User does not exist, try again");
            Console.WriteLine("");
            Console.WriteLine("Press any key to go to update menu");
            Console.ReadKey();
            UpdateMenu();
        }

        Console.WriteLine("Enter new user name");
        var newUserName = Console.ReadLine()!;
        if (string.IsNullOrEmpty(newUserName))
        {
            Console.WriteLine("User name cannot be empty. Please enter a valid user name.");
            UpdateUserMenu();
        }

        Console.WriteLine("Enter new user email");
        var newEmail = Console.ReadLine()!;
        if (string.IsNullOrEmpty(newEmail))
        {
            Console.WriteLine("User email cannot be empty. Please enter a valid user email.");
            UpdateUserMenu();
        }

        if (_userService.GetUserByEmail(newEmail) != null)
        {
            Console.WriteLine("Email already exists, try again");
            Console.WriteLine("");
            Console.WriteLine("Press any key to go to update menu");
            Console.ReadKey();
            UpdateMenu();
        }

        user!.Name = newUserName;
        user.Email = newEmail;

        if (_userService.UpdateUser(user))
        {
            Console.WriteLine("User updated successfully");
        }
        else
        {
            Console.WriteLine("An error occurred, try again");
        }

        Console.WriteLine("");
        Console.WriteLine("Press any key to go to main menu");
        Console.ReadKey();
        ShowMenu();
    }

    public void UpdateTaskMenu()
    {
        Console.Clear();
        Console.WriteLine("Enter current task name");
        var currentTaskName = Console.ReadLine()!;
        if (string.IsNullOrEmpty(currentTaskName))
        {
            Console.WriteLine("Task name cannot be empty. Please enter a valid task name.");
            UpdateTaskMenu();
        }

        var task = _taskService.GetTaskByName(currentTaskName);
        if (task == null)
        {
            Console.WriteLine("Task does not exist, try again");
            Console.WriteLine("");
            Console.WriteLine("Press any key to go to update menu");
            Console.ReadKey();
            UpdateMenu();
        }

        Console.WriteLine("Enter new task name");
        var newTaskName = Console.ReadLine()!;
        if (string.IsNullOrEmpty(newTaskName))
        {
            Console.WriteLine("Task name cannot be empty. Please enter a valid task name.");
            Console.WriteLine("Press any key to go to update menu");
            Console.ReadKey();
            UpdateTaskMenu();
        }
        var existingTask = _taskService.GetTaskByName(newTaskName);
        if (existingTask != null)
        {
            Console.WriteLine("The provided task name already exists. Please provide a different task name.");
            Console.WriteLine("Press any key to go to update menu");
            Console.ReadKey();
            UpdateTaskMenu();
        }

        Console.WriteLine("Enter new task description if you want");
        var newTaskDescription = Console.ReadLine()!;


        Console.WriteLine("Enter new task status");
        var newTaskStatus = Console.ReadLine()!;
        if (string.IsNullOrEmpty(newTaskStatus))
        {
            Console.WriteLine("Task status cannot be empty. Please enter a valid task status.");
            Console.WriteLine("Press any key to go to update menu");
            Console.ReadKey();
            UpdateTaskMenu();
        }

        Console.WriteLine("Enter new task category");
        var newTaskCategory = Console.ReadLine()!;
        if (string.IsNullOrEmpty(newTaskCategory))
        {
            Console.WriteLine("Task category cannot be empty. Please enter a valid task category.");
            Console.WriteLine("Press any key to go to update menu");
            Console.ReadKey();
            UpdateTaskMenu();
        }

        task!.TaskName = newTaskName;
        task.Description = newTaskDescription;
        task.Status.StatusName = newTaskStatus;
        task.Category.CategoryName = newTaskCategory;

        if (_taskService.UpdateTask(task))
        {
            Console.WriteLine("Task updated successfully");
        }
        else
        {
            Console.WriteLine("An error occurred, try again");
        }

        Console.WriteLine("");
        Console.WriteLine("Press any key to go to main menu");
        Console.ReadKey();
        ShowMenu();
    }

    public void DeleteMenu()
    {
        Console.Clear();

        Console.WriteLine("1. Delete User");
        Console.WriteLine("2. Delete Task");
        Console.WriteLine("3. Go back to main menu");
        Console.Write("Enter your choice: ");

        switch (Console.ReadLine())
        {
            case "1":
                DeleteUserMenu();
                break;
            case "2":
                DeleteTaskMenu();
                break;
            case "3":
                ShowMenu();
                break;
            default:
                Console.WriteLine("Invalid input, please try again");
                Console.WriteLine("press any key to go back");
                Console.ReadKey();
                DeleteMenu();
                break;
        }
    }

    public void DeleteUserMenu()
    {
        Console.Clear();

        Console.WriteLine("Enter user email");
        var userEmail = Console.ReadLine()!;
        if (string.IsNullOrEmpty(userEmail))
        {
            Console.WriteLine("User email cannot be empty. Please enter a valid user email.");
            Console.WriteLine("Press any key to go back");
            Console.ReadKey();
            DeleteUserMenu();
        }

        var user = _userService.GetUserByEmail(userEmail);
        if (user == null)
        {
            Console.WriteLine("User does not exist, try again");
            Console.WriteLine("");
            Console.WriteLine("Press any key to go back");
            Console.ReadKey();
            DeleteUserMenu();
        }

        if (_userService.DeleteUser(userEmail))
        {
            Console.WriteLine("User deleted successfully");
        }
        else
        {
            Console.WriteLine("An error occurred, try again");
        }

        Console.WriteLine("");
        Console.WriteLine("Press any key to go to main menu");
        Console.ReadKey();
        ShowMenu();
    }

    public void DeleteTaskMenu()
    {
        Console.Clear();

        Console.WriteLine("Enter task name");
        var taskName = Console.ReadLine()!;
        if (string.IsNullOrEmpty(taskName))
        {
            Console.WriteLine("Task name cannot be empty. Please enter a valid task name.");
            Console.WriteLine("Press any key to go back");
            Console.ReadKey();
            DeleteTaskMenu();
        }

        var task = _taskService.GetTaskByName(taskName);
        if (task == null)
        {
            Console.WriteLine("Task does not exist, try again");
            Console.WriteLine("");
            Console.WriteLine("Press any key to go back");
            Console.ReadKey();
            DeleteTaskMenu();
            return;
        }

        if (_taskService.DeleteTask(taskName))
        {
            Console.WriteLine("Task deleted successfully");
        }
        else
        {
            Console.WriteLine("An error occurred, try again");
        }

        Console.WriteLine("");
        Console.WriteLine("Press any key to go to main menu");
        Console.ReadKey();
        ShowMenu();
    }


}
