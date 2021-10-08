using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;

namespace ConsoleApp
{
    class Client
    {
        public string Person { get; set; }
        public int Id { get; set; }
        public decimal Balance { get; set; }
        public string Name { get; set; }
        public override string ToString()
        {
            return "ID: " + Id + "   Name: " + Name + "Balance: " + Balance;
        }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            Client objAsPart = obj as Client;
            if (objAsPart == null) return false;
            else return Equals(objAsPart);
        }
        public override int GetHashCode()
        {
            return Id;
        }
        public bool Equals(Client other)
        {
            if (other == null) return false;
            return (this.Id.Equals(other.Id));
        }
        List<Client> clients = new List<Client>();
        List<Client> clientsclone = new List<Client>();
        public void Values()
        {
            clients.Add(new Client() { Id = 1, Name = "Musa   ", Balance = 5000 });
            clients.Add(new Client() { Id = 2, Name = "Hasan  ", Balance = 2000 });
            clients.Add(new Client() { Id = 3, Name = "Osim   ", Balance = 100 });
            clients.Add(new Client() { Id = 4, Name = "Lola   ", Balance = 3000 });

            clientsclone.Add(new Client() { Id = 1, Name = "Musa   ", Balance = 5000 });
            clientsclone.Add(new Client() { Id = 2, Name = "Hasan  ", Balance = 2000 });
            clientsclone.Add(new Client() { Id = 3, Name = "Osim   ", Balance = 100 });
            clientsclone.Add(new Client() { Id = 4, Name = "Lola   ", Balance = 3000 });
        }

        public void Insert()
        {
            Console.Write("Creat new Id for new client: ");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.Write("New clients name: ");
            string name = Console.ReadLine();
            Console.Write("New clients balance: ");
            decimal balance = Convert.ToDecimal(Console.ReadLine());
            clients.Add(new Client() { Id = id, Name = name, Balance = balance });
            clientsclone.Add(new Client() { Id = id, Name = name, Balance = balance });

        }
        public void Update()
        {
            Console.Write("Clients Id: ");
            int clientsId = Convert.ToInt32(Console.ReadLine());
            Console.Write("New balance value : ");
            decimal clientBalance = Convert.ToDecimal(Console.ReadLine());
            var validUser = clients.Where(c => c.Id == clientsId);
            foreach (Client cust in validUser)
            {
                cust.Balance = clientBalance;
            }
        }
        public void Delete()
        {
            Console.Write("Delete by index: ");
            var index = Convert.ToInt32(Console.ReadLine());
            if (clients.Count == 0)
            {
                Console.WriteLine("We dont have clients yet");
                return;
            }
            if (index < 0)
            {
                Console.WriteLine("Client not found");
                return;
            }
            clients.RemoveAt(index);
            clientsclone.RemoveAt(index);

        }
        public void Select()
        {
            Console.WriteLine();
            foreach (Client aClient in clients)
            {
                Console.WriteLine(aClient);
            }
        }
        public void CheckBalance(object a)
        {
            for (int i = 0; i < clients.Count; i++)
            {
                if (clients[i].Balance != clientsclone[i].Balance)
                {
                    if (clients[i].Balance > clientsclone[i].Balance)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Balance: {clientsclone[i].Balance}  To: {clients[i].Balance} Added: {clients[i].Balance - clientsclone[i].Balance}");
                        Console.ResetColor();
                        clientsclone[i].Balance = clients[i].Balance;
                    }
                    if (clients[i].Balance < clientsclone[i].Balance)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Balance: {clientsclone[i].Balance}  To: {clients[i].Balance} Get: {clients[i].Balance - clientsclone[i].Balance}");
                        Console.ResetColor();
                        clientsclone[i].Balance = clients[i].Balance;
                    }
                }
            }
        }
    }
    class Program
    {
        static object locker = new object();
        static void Main(string[] args)
        {
            string exit = null;
            Client client = new Client();
            client.Values();
            Timer timer = new Timer(client.CheckBalance, null, 0, 1000);
            while (exit != "exit")
            {
                Console.WriteLine("ConsoleApp is opened");


                Console.WriteLine("1. Insert  2. Update Balance  3. Delete  4. Select  5. exit");
                var choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        Thread insertThread = new Thread(client.Insert);
                        insertThread.Start();
                        insertThread.Join();
                        Console.WriteLine("\nPress any key . . . ");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case 2:
                        Console.Clear();
                        Thread updateThread = new Thread(client.Update);
                        updateThread.Start();
                        updateThread.Join();
                        Console.WriteLine("\nPress any key . . . ");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case 3:
                        Console.Clear();
                        Thread deleteThread = new Thread(client.Delete);
                        deleteThread.Start();
                        deleteThread.Join();
                        Console.WriteLine("\nPress any key . . . ");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case 4:
                        Console.WriteLine("Where is All clients of list");
                        Console.Clear();
                        Thread selectThread = new Thread(client.Select);
                        selectThread.Start();
                        selectThread.Join();
                        Console.WriteLine("\nPress any key . . . ");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case 5:
                        exit = "exit";
                        break;
                }
                Console.WriteLine("end of codes");

            }
            

        }
    }
}
