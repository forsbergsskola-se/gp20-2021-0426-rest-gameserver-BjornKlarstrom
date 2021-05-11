using System;
using System.Linq;
using System.Threading.Tasks;

namespace LameScooter{
    internal static class Program{
        static async Task Main(string[] args){

            ILameScooterRental rental = new DeprecatedLameScooterRental();

            try{
                var count = await rental.GetScooterCountInStation(args[0]);
                Console.WriteLine($"\nNumber of Scooters Available at {args[0]} is: {count}");
            }
            catch (ArgumentException argumentException){
                Console.WriteLine(argumentException);
            }
            catch (NotFoundException notFoundException){
                Console.WriteLine(notFoundException);
            }
        }
    }
}