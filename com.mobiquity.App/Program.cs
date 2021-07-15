using com.mobiquity.exception;
using com.mobiquity.service;
using System;

namespace com.mobiquity.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                try
                {
                    Console.WriteLine(MobiquityService.pack(args[0]));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Please, enter a valid absolute filepath.");
            }
        }
    }
}
