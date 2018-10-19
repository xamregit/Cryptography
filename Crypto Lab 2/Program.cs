using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PGGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(FindPrimeUInt32.GenetarePrime());
            //Console.WriteLine(FindPrimeUInt32.FindPrimitiveRoot(11));

            //Console.WriteLine(FastPow.FastPowFunc(3, 2188910080, 2188910081));

            using (StreamWriter fstream = new StreamWriter("output.txt"))
            {
                ulong[] pk = new ulong[2];

                pk = FindPrimeUInt32.GenetarePrime();
                ulong g = FindPrimeUInt32.FindPrimitiveRoot(pk);
                fstream.WriteLine("{0} {1}", pk[0], g);
            }
        }
    }
}
