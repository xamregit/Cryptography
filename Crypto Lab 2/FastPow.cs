namespace PGGeneratorUInt32
{
    
    class FastPow
    {
        public static ulong FastPowFunc(ulong number, ulong pow, ulong mod)
        {
            ulong result = 1;
            ulong bit = number % mod;

            while (pow > 0)
            {
                if ((pow & 1) == 1)
                {
                    result *= bit;
                    result %= mod;
                }
                bit *= bit;
                bit %= mod;
                pow >>= 1;
            }
            return result;
        }
    }
}
