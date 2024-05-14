using System;
using System.Collections;
using System.Linq;

public class Program
{
    public static void Main()
    {
        Console.WriteLine("Hello Thread Id statistics World");
        var maxAttempts = 15;
        IDictionary<int, int> results = new Dictionary<int, int>();

        // create tasks and collect their respective ThreadId
        for (int i = 0; i < maxAttempts; i++)
        {
            var t = Task<int>.Run(() =>
            {
                var threadId = Thread.CurrentThread.ManagedThreadId;
                return threadId;
            });

            t.Wait();

            results.Add(i + 1, t.Result);
        }

        // list overview
        Console.WriteLine("Details:");

        foreach (var item in results)
        {
            Console.WriteLine("Attempt = {0}, Thread Id = {1}", item.Key, item.Value);
        }

        // create new dictionary containing a statistical summary of count of Thread Ids assigned per run
        var query = 
            from q in results
            group q by q.Value into t
            select new { Key = t.Key, Value = t.Count() };

        Console.WriteLine();
        Console.WriteLine("Summary:");

        foreach (var item in query)
        {
            Console.WriteLine("Thread Id = {0}, Occurences = {1}", item.Key, item.Value);
        };
    }
}
