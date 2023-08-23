using System.Security.Principal;
using System.Xml.Linq;

namespace Yein_A3;
//Yein An 301316062
class Program
{
    static void Main(string[] args)
    {
        TestAccounts();
    }

    static void TestAccounts()
    {

        Bank.AccountList.Add(new SavingsAccount("S647", "Alex Du", 222290192, 4783.98));
        Bank.AccountList.Add(new ChequingAccount("C576", "Dale Stayne", 333312312, 12894.56));
        Console.WriteLine($"{new string('-', 90)}");
        Console.WriteLine($"{"Consumer ID", -20}{"Name", -20}{"Account Number", -20}{"Type", -20}{"Balance", -20}");
        Console.WriteLine($"{new string('-', 90)}");
        Bank.ShowAll();

        Console.WriteLine($"{new string('-', 90)}");
        Console.WriteLine("Trying to withdraw $500.00 from the following account");
        Console.WriteLine(Bank.AccountList[0].ToString());
        Bank.AccountList[0].Withdraw(500.00);
        Console.WriteLine($"{new string('-', 90)}");

        Console.WriteLine($"{new string('-', 90)}");
        Console.WriteLine("Trying to deposit $-250.00 to the following account");
        Console.WriteLine(Bank.AccountList[1].ToString());
        Bank.AccountList[1].Deposit(-250.00);
        Console.WriteLine($"{new string('-', 90)}");

        Console.WriteLine($"{new string('-', 90)}");
        Console.WriteLine("Trying to withdraw $10000.00 to the following account");
        Console.WriteLine(Bank.AccountList[2].ToString());
        Bank.AccountList[2].Withdraw(10000.00);
        Console.WriteLine($"{new string('-', 90)}");

        Console.WriteLine($"{new string('-', 90)}");
        Console.WriteLine("Trying to deposit $5000.00 to the following account");
        Console.WriteLine(Bank.AccountList[2].ToString());
        Bank.AccountList[2].Deposit(5000.00);
        Console.WriteLine($"{new string('-', 90)}");

        Console.WriteLine($"{new string('-', 90)}");
        Console.WriteLine("Trying to deposit $7300.00 to the following account");
        Console.WriteLine(Bank.AccountList[3].ToString());
        Bank.AccountList[3].Deposit(7300.90);
        Console.WriteLine($"{new string('-', 90)}");

        Console.WriteLine($"{new string('-', 90)}");
        Console.WriteLine("Trying to withdraw $45000.40 from the following account");
        Console.WriteLine(Bank.AccountList[4].ToString());
        Bank.AccountList[4].Withdraw(45000.40);
        Console.WriteLine($"{new string('-', 90)}");

        Console.WriteLine($"{new string('-', 90)}");
        Console.WriteLine("Trying to withdraw $37000.00 from the following account");
        Console.WriteLine(Bank.AccountList[5].ToString());
        Bank.AccountList[5].Withdraw(37000);
        Console.WriteLine($"{new string('-', 90)}");

        Console.WriteLine($"{new string('-', 90)}");
        Bank.ShowAll(67676767);
        Console.WriteLine($"{new string('-', 90)}");

        Console.WriteLine($"{"Consumer ID",-20}{"Name",-20}{"Account Number",-20}{"Type",-20}{"Balance",-20}");
        Console.WriteLine($"{new string('-', 90)}");
        Bank.ShowAll();
        Console.WriteLine($"{new string('-', 90)}");
    }
}



public class Consumer {

    public string Id { get; }
    public string Name { get; }

    public Consumer(string id,string name)
    {

        this.Id = id;
        this.Name = name;


    }

    public override string ToString()
    {
        return $"{this.Id,-20}{this.Name,-20}";
    }


}


abstract public class Account : Consumer {

    public int AccountNum { get; set; }

    public Account(string id, string name, int accountNum): base(id,name) {

        this.AccountNum = accountNum;

    }

    public abstract void Withdraw(double amount);

    public abstract void Deposit (double amount);

    public override string ToString()
    {
        return $"{base.ToString()}{this.AccountNum,-20}";
    }

}

public class InsufficientBalanceException : Exception {

    public override string Message => "Account not having enough balance to withdraw";

}

public class MinimumBalanceException : Exception {

    public override string Message => "You must maintain minimum $3000 balance in Saving account";

}

public class IncorrectAmountException : Exception
{

    public override string Message => "You must provide positive number for amount to be deposited.";

}

public class OverdraftLimitException : Exception
{

    public override string Message => "Overdraft Limit exceeded. You can only use $2000 from overdraft.";

}
public class AccountNotFoundException : Exception

{

    public override string Message => "Account with given number does not exist.";

}
public class SavingsAccount : Account{

    public double Balance { get; set; }

    public SavingsAccount(string id,string name,int accountNum, double balance = 0.0):base(id,name,accountNum) {

        this.Balance = balance;
    }

    public override void Withdraw(double amount) {

        try
        {
          

            if(Balance < amount) {

                throw new InsufficientBalanceException();

            }

            if(Balance - amount < 3000) {


                throw new MinimumBalanceException();
            }
            else {

                this.Balance -= amount;

                Console.WriteLine($"Successfuly withdrawn {amount:C} from the account number {this.AccountNum}\nUpdated balance is ${this.Balance}");



            }


        }
        catch (InsufficientBalanceException IBE) {

            Console.WriteLine($"{IBE.Message}");

        }
        catch (MinimumBalanceException MBE)
        {

            Console.WriteLine($"{MBE.Message}");

        }
      

    }

    public override void Deposit(double amount) {

        try{
          
            if(amount < 0) {

                throw new IncorrectAmountException();
            }

            else {

                this.Balance += amount;
                Console.WriteLine($"Successfuly deposited {amount:C} to the account number {this.AccountNum}\nUpdated balance is ${this.Balance}");

            }
        }
        catch(IncorrectAmountException IAE) {

            Console.WriteLine($"{IAE.Message}");
        }
     
    }


    public override string ToString()
    {
        return $"{this.Id,-20}{this.Name,-20}{this.AccountNum,-20}{"Saving",-20}${Math.Round(this.Balance,2)}";
    }


}

public class ChequingAccount : Account {

    public double Balance;

    public ChequingAccount(string id,string name,int accountNum,double balance =0.0):base(id,name,accountNum) {


        this.Balance = balance;
    }

    public override void Withdraw(double amount) {

        
        try {

            if(amount > Balance+2000) {

                throw new OverdraftLimitException();
               
            }
            else
            {

                this.Balance -= amount;


                Console.WriteLine($"Successfuly withdrawn {amount:C} from the account number {this.AccountNum}\nUpdated balance is {Math.Round(this.Balance,2)}");


            }


        }
        catch(OverdraftLimitException OLE) {

            Console.WriteLine($"{OLE.Message}");
        }


    }

    public override void Deposit(double amount) {

        try {

            if(amount < 0) {

                throw new IncorrectAmountException();
            }
            else
            {

                this.Balance += amount;
                Console.WriteLine($"Successfuly deposited {amount:C} to the account number {this.AccountNum}\nUpdated balance is {this.Balance:C}");


            }

        }
        catch (IncorrectAmountException IAE) {

            Console.WriteLine($"{IAE.Message}");
        }

    }

    public override string ToString()
    {
        return $"{this.Id,-20}{this.Name,-20}{this.AccountNum,-20}{"Chequing",-20}${Math.Round(this.Balance,2),-20}";
    }



}

public class Bank {

    public static List<Account> AccountList { get; set; }


    static Bank() {

       
         AccountList = new List<Account>();

         AccountList.Add(new SavingsAccount("S101", "James Finch", 222210212, 3400.90));
         AccountList.Add(new SavingsAccount("S102", "Kell Forest", 222214500, 42520.32));
         AccountList.Add(new SavingsAccount("S103", "Amy Stone", 222212000, 8273.45));
         AccountList.Add(new ChequingAccount("C104", "Kaitlin Ross", 333315002, 91230.45));
         AccountList.Add(new ChequingAccount("C105", "Adem First", 333303019, 43987.67));
         AccountList.Add(new ChequingAccount("C106", "John Doe", 333358927, 34829.76));
        

    }


    public static void ShowAll()
    {
      
        foreach(Account account in AccountList) {
            Console.WriteLine(account.ToString());
        }
        Console.WriteLine();
    }

    public static void ShowAll(int accountNum) {

        Console.WriteLine($"Details of account number {accountNum}");


        bool found = false;


        foreach(Account account in AccountList) {

            if (account.AccountNum.ToString() == accountNum.ToString()) {

                Console.WriteLine($"{account}");

                found = true;
            }


        }
        try
        {
            if (!found)
            {

                throw new AccountNotFoundException();
            }
        }
        catch (AccountNotFoundException ANF)
        {

            Console.WriteLine(ANF.Message);
        }

        Console.WriteLine();


    }
}

