using UnityEngine;
using Random = System.Random;

namespace SeriousSwag3.Utils
{
    public static class ColorGenerator
    {

        private static string BLUE = "#0000FF";
        private static string YELLOW = "#FFFF00";
        private static string RED = "#FF0000";

        private static string[] listColors =
        {
            BLUE,
            YELLOW,
            RED
        };

        public static string GetRandomColor()
        {
            var randomInt = UnityEngine.Random.Range(0, listColors.Length);
            return listColors[randomInt];

        }

        public static Color GetColorFromHex(string hex)
        {
            Color color;
            ColorUtility.TryParseHtmlString(hex, out color);
            return color;
        }

    }
}