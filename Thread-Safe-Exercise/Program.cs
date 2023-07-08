class BankAccount
{
    // This bit is thread safe
    public decimal m_balance = 0.0M;
    private object m_balanceLock = new object();
    internal void Deposit(decimal delta)
    {
        lock (m_balanceLock) { m_balance += delta; }
    }
    internal void Withdraw(decimal delta)
    {
        lock (m_balanceLock)
        {
            if (m_balance < delta)
                throw new Exception("Insufficient funds");
            m_balance -= delta;
        }
    }
    // End This bit is thread safe
    //The bit is thread-safe because it  locks the operations that accesses the state, m_balance so only one thread can change the value at a time.

    // Proposed transfer method
    internal static void Transfer(
      BankAccount a, BankAccount b, decimal delta)
    {
        //Withdraw(a, delta);
        //Deposit(b, delta);

        a.Withdraw(delta);
        b.Deposit(delta);


        //I COULDN'T FIGURE OUT SO I LOOKED AT THE SOLUTION. 
        lock (b.m_balanceLock)
        {
            lock (a.m_balanceLock)
            {
                a.Withdraw(delta);
                b.Deposit(delta);
            }
        }
    }
}

public class program
{
    public static void Main(string[] args)
    {
        List<BankAccount> accountsToWithdraw = new List<BankAccount>();
        List<BankAccount> accountsToDeposite = new List<BankAccount>();

        for (int i = 0; i < 10; i++)
        {
            accountsToWithdraw.Add(new BankAccount() { m_balance = 2000 });
        }

        for (int i = 0; i < 10; i++)
        {
            accountsToDeposite.Add(new BankAccount() { m_balance = 1000 });
        }

        Parallel.For(0, accountsToDeposite.Count, (index) => { BankAccount.Transfer(accountsToWithdraw[index], accountsToDeposite[index], 200); });

        for (int i = 0; i < accountsToDeposite.Count; i++) { 
            Console.WriteLine($"{accountsToWithdraw[i].m_balance} : {accountsToDeposite[i].m_balance}");
        }
    }
}

