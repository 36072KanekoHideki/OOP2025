﻿using System.Globalization;

namespace Section01 {
    internal class Program {
        static void Main(string[] args) {
            var target = "C# Programming";
            var isExist = target.All(c => Char.IsLower(c));
            Console.WriteLine(target);
        }
    }
}