/// <summary>
/// Maintain a Customer Service Queue. Allows new customers to be
/// added and allows customers to be serviced.
/// </summary>
public class CustomerService
{
    public static void Run()
    {
        // Test 1
        // Scenario: Create a queue with an invalid maximum size of 0.
        // Expected Result: The maximum size should default to 10.
        Console.WriteLine("Test 1");
        var customerService = new CustomerService(0);
        Console.WriteLine(customerService);
        // Expected: [size=0 max_size=10 => ]
        // Defect(s) Found: None. The constructor correctly defaults to 10.

        Console.WriteLine("=================");

        // Test 2
        // Scenario: Add one customer to a queue with available space.
        // Expected Result: The customer should be added to the back of the queue.
        Console.WriteLine("Test 2");
        customerService = new CustomerService(3);

        Console.SetIn(new StringReader(
            "Ana\n" +
            "A100\n" +
            "Password reset\n"));

        customerService.AddNewCustomer();
        Console.WriteLine(customerService);
        // Expected:
        // [size=1 max_size=3 => Ana (A100) : Password reset]
        // Defect(s) Found: None when space is available.

        Console.WriteLine("=================");

        // Test 3
        // Scenario: Add customers until the queue is full, and then try to
        // add one more customer.
        // Expected Result: An error message should be displayed, and the
        // additional customer should not be added.
        Console.WriteLine("Test 3");
        customerService = new CustomerService(2);

        Console.SetIn(new StringReader(
            "Bob\n" +
            "B200\n" +
            "Billing problem\n" +
            "Carlos\n" +
            "C300\n" +
            "Account locked\n" +
            "Diana\n" +
            "D400\n" +
            "Technical problem\n"));

        customerService.AddNewCustomer();
        customerService.AddNewCustomer();
        customerService.AddNewCustomer();

        Console.WriteLine(customerService);
        // Expected: Maximum Number of Customers in Queue.
        // Expected queue size: 2
        // Defect(s) Found:
        // The original condition used _queue.Count > _maxSize.
        // It must use _queue.Count >= _maxSize.

        Console.WriteLine("=================");

        // Test 4
        // Scenario: Add two customers and serve both customers.
        // Expected Result: Customers should be served in FIFO order:
        // Elena first, followed by Frank.
        Console.WriteLine("Test 4");
        customerService = new CustomerService(2);

        Console.SetIn(new StringReader(
            "Elena\n" +
            "E500\n" +
            "Update address\n" +
            "Frank\n" +
            "F600\n" +
            "Close account\n"));

        customerService.AddNewCustomer();
        customerService.AddNewCustomer();

        customerService.ServeCustomer();
        customerService.ServeCustomer();
        // Expected:
        // Elena (E500) : Update address
        // Frank (F600) : Close account
        // Defect(s) Found:
        // The original code removed the first customer before reading it.
        // It then displayed the next customer instead of the customer served.

        Console.WriteLine("=================");

        // Test 5
        // Scenario: Try to serve a customer from an empty queue.
        // Expected Result: An error message should be displayed.
        Console.WriteLine("Test 5");
        customerService = new CustomerService(2);
        customerService.ServeCustomer();
        // Expected: No customers in the queue.
        // Defect(s) Found:
        // The original code did not check whether the queue was empty.
    }

    private readonly List<Customer> _queue = new();
    private readonly int _maxSize;

    public CustomerService(int maxSize)
    {
        if (maxSize <= 0)
        {
            _maxSize = 10;
        }
        else
        {
            _maxSize = maxSize;
        }
    }

    /// <summary>
    /// Defines a Customer record for the service queue.
    /// This is an inner class. Its real name is CustomerService.Customer.
    /// </summary>
    private class Customer
    {
        public Customer(string name, string accountId, string problem)
        {
            Name = name;
            AccountId = accountId;
            Problem = problem;
        }

        private string Name { get; }
        private string AccountId { get; }
        private string Problem { get; }

        public override string ToString()
        {
            return $"{Name} ({AccountId})  : {Problem}";
        }
    }

    /// <summary>
    /// Prompt the user for the customer and problem information.
    /// Put the new record into the queue.
    /// </summary>
    private void AddNewCustomer()
    {
        // Verify there is room in the service queue.
        if (_queue.Count >= _maxSize)
        {
            Console.WriteLine("Maximum Number of Customers in Queue.");
            return;
        }

        Console.Write("Customer Name: ");
        var name = Console.ReadLine()!.Trim();

        Console.Write("Account Id: ");
        var accountId = Console.ReadLine()!.Trim();

        Console.Write("Problem: ");
        var problem = Console.ReadLine()!.Trim();

        // Create the customer object and add it to the back of the queue.
        var customer = new Customer(name, accountId, problem);
        _queue.Add(customer);
    }

    /// <summary>
    /// Dequeue the next customer and display the information.
    /// </summary>
    private void ServeCustomer()
    {
        if (_queue.Count == 0)
        {
            Console.WriteLine("No customers in the queue.");
            return;
        }

        var customer = _queue[0];
        _queue.RemoveAt(0);
        Console.WriteLine(customer);
    }

    /// <summary>
    /// Support the WriteLine function to provide a string representation of the
    /// customer service queue object.
    /// </summary>
    /// <returns>A string representation of the queue</returns>
    public override string ToString()
    {
        return $"[size={_queue.Count} max_size={_maxSize} => " +
               string.Join(", ", _queue) + "]";
    }
}