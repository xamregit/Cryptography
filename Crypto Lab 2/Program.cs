using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Crypto_Lab_2
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(FindPrimeUInt32.GenetarePrime());
            // Console.WriteLine(FindPrimeUInt32.FindPrimitiveRoot(11));
            using (StreamWriter fstream = new StreamWriter("output.txt"))
            {
                ulong p = FindPrimeUInt32.GenetarePrime();
                fstream.WriteLine("{0} {1}", p, FindPrimeUInt32.FindPrimitiveRoot(p));
            }
        }
    }
}
