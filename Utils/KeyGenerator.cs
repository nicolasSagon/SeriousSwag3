using System;

namespace SeriousSwag3.Utils
{
    public static class KeyGenerator
    {
        public static char GetRandomKey()
        {
            var charSet = "abcdefghijkmnopqrstuvwxyz";
            var chars = charSet.ToCharArray();

            var randomNumber = new Random().Next(0, chars.Length);

            return chars[randomNumber];
        }
    }
}