using System;
using System.Globalization;
using UnityEngine;
using Random = System.Random;

namespace SeriousSwag3.Utils
{
    public static class ColorGenerator
    {

        private static string BLUE = "#0000FF";
        private static string GREEN = "#00FF00";
        private static string RED = "#FF0000";

        private static string[] listColors =
        {
            BLUE,
            GREEN,
            RED
        };

        public static string GetRandomColor()
        {
            var randomInt = new Random().Next(0, 2);
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