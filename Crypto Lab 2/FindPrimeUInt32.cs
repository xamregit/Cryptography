using System;
using System.Security.Cryptography;

namespace Crypto_Lab_2
{
    class FindPrimeUInt32
    {

        private static ulong RandUInt32(ulong _from, ulong _to)
        {
            ulong randNumber;
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {

                byte[] data = new byte[4];

                do
                {
                    rng.GetBytes(data);
                    ulong scale = BitConverter.ToUInt32(data, 0);
                    randNumber = (ulong)(_from + (_to - _from) * (scale / (UInt32.MaxValue + 1.0)));
                }
                while (randNumber < _from || randNumber >= _to);
            }
            return randNumber;
        }


        public static ulong FindPrimitiveRoot(ulong p)
        {
            bool isRoot;
            ulong g;
            do
            {
                g = RandUInt32(1, p - 2);
                isRoot = true;
                if (FastPow.FastPowFunc(g, p - 1, p) == 1)
                {
                    for (ulong i = 1; i < p - 2; i++)
                    {
                        if (FastPow.FastPowFunc(g, i, p) == 1)
                        {
                            isRoot = false;
                            break;
                        }
                    }
                }
            }
            while (!isRoot);
            return g;
        }

        private static bool MillerRabinTest(ulong p, ulong k)
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

            for (ulong i = 0; i < k; i++)
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


        public static ulong GenetarePrime()
        {
            ulong min = uint.MaxValue / 2;
            ulong max = uint.MaxValue;

            ulong p = 0;

            while(true)
            {
                p = RandUInt32(min, max);
                if (MillerRabinTest(p, 128))
                    return p;
            }
        }
    }
}
