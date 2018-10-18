using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Lab_2
{
    class FindPrime
    {
        private static ulong FastPowFunc(ulong Number, ulong Pow, ulong Mod)
        {
            ulong Result = 1;
            ulong Bit = Number % Mod;

            while (Pow > 0)
            {
                if ((Pow & 1) == 1)
                {
                    Result *= Bit;
                    Result %= Mod;
                }
                Bit *= Bit;
                Bit %= Mod;
                Pow >>= 1;
            }
            return Result;
        }

        private static bool MillerRabinTest(ulong n, int k)
        {
            if (n == 2 || n == 3)
                return true;

            if (n < 2 || n % 2 == 0)
                return false;

            ulong t = n - 1;

            ulong s = 0;

            while (t % 2 == 0)
            {
                t /= 2;
                s += 1;
            }

            for (uint i = 0; i < k; i++)
            {
                ulong a = 1;
                
                using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
                {

                    byte[] data = new byte[4];

                    while (a < 2 || a >= n)
                    {
                        rng.GetBytes(data);
                        a = BitConverter.ToUInt32(data, 0);
                    }
                    
                }

                ulong x = FastPowFunc(a, t, n);

                
                if (x == 1 || x == n - 1)
                    continue;

                for (ulong r = 1; r < s; r++)
                {
                    
                    x = x * x % n;

                    if (x == 1)
                        return false;

                    if (x == n - 1)
                        break;
                }

                if (x != n - 1)
                    return false;
            }

            return true;
        }

        public static ulong GenetarePrime()
        {
            uint min = uint.MaxValue / 2;
            uint max = uint.MaxValue;
            ulong n = 0;
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                while (true)
                {
                    byte[] four_bytes = new byte[4];
                    rng.GetBytes(four_bytes);

                    long scale = BitConverter.ToUInt32(four_bytes, 0);

                    n = (ulong)(min + (max - min) * (scale / (uint.MaxValue + 1.0)));
                    if (n >= min && n < max && MillerRabinTest(n, 128))
                        return n;
                }

            }
        }
    }
}
