using System.IO;

namespace PGGeneratorUInt32
{
    class Program
    {
        static void Main(string[] args)
        {
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
