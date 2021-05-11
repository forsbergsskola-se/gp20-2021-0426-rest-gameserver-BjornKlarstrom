using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LameScooter{
    internal static class Program{
        static async Task Main(string[] args){

            var stationInput = args[0];
            var rental = SetDatabaseVersion(args);

            try{
                var count = await rental.GetScooterCountInStation(stationInput);
                Console.WriteLine($"\nNumber of Scooters Available at {stationInput} is: {count}");
            }
            catch (ArgumentException argumentException){
                Console.WriteLine(argumentException);
            }
            catch (NotFoundException notFoundException){
                Console.WriteLine(notFoundException);
            }
        }

        static ILameScooterRental SetDatabaseVersion(IReadOnlyList<string> args){
            
            if(args.Count != 2)
                throw new ArgumentException("Missing argument for database type");

            switch (args[^1]){
                case "offline":
                    return new OfflineLameScooterRental();
                    break;
                case "deprecated":
                    return new DeprecatedLameScooterRental();
                    break;
                case "realtime":
                    Console.WriteLine("Soon to be implemented...");
                    break;
                case "mongodb":
                    Console.WriteLine("Soon to be implemented...");
                    break;
                default:
                    break;
            }
            throw new ArgumentException("Type of database doesn't exist");
        }
    }
}