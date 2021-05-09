using System;

namespace LameScooter{
    public class NotFoundException : Exception{
        public NotFoundException(string x){
            Console.WriteLine($"Could not find: {x}");
        }
    }
}