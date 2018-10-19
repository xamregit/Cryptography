using System;
using System.Security.Cryptography;

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

        private static bool MillerRabinTest(ulong p, int k)
        {
            if (p == 2 || p == 3)
                return true;

            if (p < 2 || p % 2 == 0)
                return false;

            ulong t = p - 1;

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

                    while (a < 2 || a >= p)
                    {
                        rng.GetBytes(data);
                        a = BitConverter.ToUInt32(data, 0);
                    }
                    
                }

                ulong x = FastPowFunc(a, t, p);

                
                if (x == 1 || x == p - 1)
                    continue;

                for (ulong r = 1; r < s; r++)
                {
                    
                    x = x * x % p;

                    if (x == 1)
                        return false;

                    if (x == p - 1)
                        break;
                }

                if (x != p - 1)
                    return false;
            }

            return true;
        }

        public static ulong GenetarePrime()
        {
            uint min = uint.MaxValue / 2;
            uint max = uint.MaxValue;

            ulong p = 0;

            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                while (true)
                {
                    byte[] four_bytes = new byte[4];
                    rng.GetBytes(four_bytes);

                    long scale = BitConverter.ToUInt32(four_bytes, 0);

                    p = (ulong)(min + (max - min) * (scale / (uint.MaxValue + 1.0)));

                    if (p >= min && p < max && MillerRabinTest(p, 128))
                        return p;
                }

            }
        }
    }
}
