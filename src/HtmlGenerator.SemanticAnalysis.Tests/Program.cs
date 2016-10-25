using System;
using HtmlGenerator.SemanticAnalysis;

namespace HtmlGenerator.Tests
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Sema.GetElementInformation("a");

            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}
