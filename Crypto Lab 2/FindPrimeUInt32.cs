using System;
using System.Security.Cryptography;

namespace PGGeneratorUInt32
{
    class FindPrimeUInt32
    {
        private static ulong RandUInt32(ulong _from, ulong _to)
        {
            ulong randNumber;
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {

                byte[] data = new byte[4];
                rng.GetBytes(data);
                ulong scale = BitConverter.ToUInt32(data, 0);
                randNumber = (ulong)(_from + (_to - _from) * (scale / (UInt32.MaxValue + 1.0)));
                
            }
            return randNumber;
        }


        public static ulong FindPrimitiveRoot(ulong[] pk)
        {
            ulong p = pk[0];
            ulong k = pk[1];

            bool isRoot;
            ulong g;
            ulong j = 0;
            do
            {
                do {
                    g = RandUInt32(1, p - 2);
                }
                while (!MillerRabinTest(g, 128));

                isRoot = true;
                if (FastPow.FastPowFunc(g, p - 1, p) == 1)
                {
                    for (ulong i = 1; i <= k; i++)
                    {
                        if (FastPow.FastPowFunc(g, i, p) == 1)
                        {
                            isRoot = false;
                            break;
                        }
                    }
                }
                j++;
            }
            while (!isRoot);
            return g;
        }

        private static bool MillerRabinTest(ulong p, ulong l)
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

            for (ulong i = 0; i < l; i++)
            {
                ulong a = 1;

                a = RandUInt32(2, p - 2);

                ulong x = FastPow.FastPowFunc(a, t, p);

                
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


        public static ulong[] GenetarePrime()
        {
            ulong min = uint.MaxValue / 4;
            ulong max = uint.MaxValue / 2;
            ulong[] pk = new ulong[2];

            while(true)
            {
                pk[1] = RandUInt32(min, max);
                if (MillerRabinTest(pk[1], 128) && MillerRabinTest(2 * pk[1] + 1, 128))
                {
                    pk[0] = 2 * pk[1] + 1;
                    return pk;
                }
            }
        }
    }
}
