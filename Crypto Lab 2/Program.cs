using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Lab_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(FindPrimeUInt32.GenetarePrime().ToString());
            Console.ReadKey();
        }
    }
}
