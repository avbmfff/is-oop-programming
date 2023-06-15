using Banks;
using Banks.Accounts;
using Banks.Client;
using Banks.Observe;

string answer;
Client client = new Client();
var forbank = new Dictionary<int, float>()
{
    { 500, 5 },
    { 1000, 6 },
};
ClientInfo clientInfo1 = new ClientInfo(150, 6, forbank, 150);
Bank bank1 = new Bank("Bel", clientInfo1);

Console.WriteLine("Hello, we are Bel Bank. Do you want to create account? If yes, enter 1, no - 0");
answer = Console.ReadLine();

switch (answer)
{
    case "0":
        break;
    case "1":
        Console.WriteLine("Ok! What's your name?");
        answer = Console.ReadLine();
        string name = answer;
        Console.WriteLine("What's your lastname?");
        answer = Console.ReadLine();
        client.SetFullName(name, answer);
        break;
    default:
        Console.WriteLine("Please, try again");
        break;
}

Console.WriteLine("Can you add your passport's data? Without them, we will consider your account suspicious.If yes, enter 1, no - 0");
answer = Console.ReadLine();

switch (answer)
{
    case "0":
        break;
    case "1":
        Console.WriteLine("Let's go! Get me your series number");
        string series = Console.ReadLine();
        Console.WriteLine("Get me your passport's number");
        string number = Console.ReadLine();
        Console.WriteLine("Get me your register date, format yyyy, mm, dd");
        DateTime dateTime = DateTime.Parse(Console.ReadLine());
        Console.WriteLine("Get me your location");
        string location = Console.ReadLine();
        Passport passport = new Passport(series.ToCharArray(), number.ToCharArray(), dateTime, location);
        client.SetPassport(passport);
        break;
    default:
        Console.WriteLine("Please, try again");
        break;
}

Console.WriteLine("Congratulations! You have successfully registered.");
Console.WriteLine("Now you can: create a debit account by clicking on 1");
Console.WriteLine("create a credit account by clicking on 2");
Console.WriteLine("create a deposit account by clicking on 3");
Console.WriteLine("exit by clicking 0");
answer = Console.ReadLine();
bank1.AddClient(client);
int sum;
Account account = null;
switch (answer)
{
    case "0":
        break;
    case "1":
        Console.WriteLine("Ok! Let's create the debit account. Check out the terms.");
        Console.Write($"Percent is {bank1.ClientInfo.GetPercentForDebit()} and you can replenishment, withdraw and transfer your money");
        Console.WriteLine("What starter sum put on account?");
        sum = int.Parse(Console.ReadLine());
        account = bank1.CreateDebitAccount(client, sum);
        Console.WriteLine("Success!");
        break;
    case "2":
        Console.WriteLine("Ok! Let's create the credit account. Check out the terms.");
        Console.Write($"Commission if you in negative is {bank1.ClientInfo.GetCommision()} and you can replenishment, withdraw and transfer your money");
        Console.WriteLine("What credit sum put on account?");
        sum = int.Parse(Console.ReadLine());
        account = bank1.CreateCreditAccount(client, sum);
        Console.WriteLine("Success!");
        break;
    case "3":
        Console.WriteLine("Ok! Let's create the credit account. Check out the terms.");
        Console.WriteLine("Percent for sum is different an check list and you you cannot withdraw, replenishment and transfer money until the end of the period");
        foreach (var value in bank1.ClientInfo.GetPercentForDeposit())
        {
            Console.WriteLine($"min sum: {value.Key}  percent: {value.Value}");
        }

        Console.WriteLine("What deposit sum put on account?");
        sum = int.Parse(Console.ReadLine());
        Console.WriteLine("How much month for deposit you choose?");
        int month = int.Parse(Console.ReadLine());
        account = bank1.CreateDepositAccount(client, sum, month);
        Console.WriteLine("Success!");
        break;
    default:
        Console.WriteLine("Please, try again");
        break;
}

Console.WriteLine("The following actions are available to you: add and withdraw money to your account.");
Console.WriteLine("For add - 1, for withdraw - 2, exit - 0");
switch (answer)
{
    case "0":
        break;
    case "1":
        Console.WriteLine("What amount do we transfer?");
        sum = int.Parse(Console.ReadLine());
        account.AddSum(sum);
        Console.WriteLine("Success!");
        break;
    case "2":
        Console.WriteLine("What amount do we withdraw?");
        sum = int.Parse(Console.ReadLine());
        account.WithdrawSum(sum);
        Console.WriteLine("Success!");
        break;
    default:
        Console.WriteLine("Please, try again");
        break;
}
