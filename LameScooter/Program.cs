using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LameScooter{
    internal static class Program{

        static async Task Main(string[] args){

            ILameScooterRental rental = new OfflineLameScooterRental();
            

            var count = await rental.GetScooterCountInStation("Myyrmäki station");
            
            Console.WriteLine($"Number of Scooters Available at this Station: {count}");

            /*throw new ArgumentException("My Message");
            try{
                // try something
            }
            catch (ArgumentException argumentException){
                Console.WriteLine(argumentException.Message);
                throw;
            }*/
        }
    }
}