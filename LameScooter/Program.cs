using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LameScooter{
    internal static class Program{

        static async Task Main(string[] args){

            ILameScooterRental rental = new OfflineLameScooterRental();
            try{
                if (args[0].Any(char.IsDigit)) throw new ArgumentException("Invalid Message:");
                var count = await rental.GetScooterCountInStation(args[0]);
                Console.WriteLine($"Number of Scooters Available at {args[0]} is: {count}");
            }
            catch (ArgumentException argumentException){
                Console.WriteLine(argumentException);
            }
            catch (NotFoundException notFoundException){
                Console.WriteLine($"Could not find: {notFoundException}");
            }
        }
    }
}