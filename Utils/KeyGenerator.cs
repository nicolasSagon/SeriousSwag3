using System;
using Random = UnityEngine.Random;

namespace SeriousSwag3.Utils
{
    public static class KeyGenerator
    {
        public static char GetRandomKey()
        {
            var charSet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var chars = charSet.ToCharArray();

            var randomNumber = Random.Range(0, charSet.Length);

            return chars[randomNumber];
        }
    }
}