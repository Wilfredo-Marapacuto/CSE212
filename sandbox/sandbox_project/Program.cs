using System;
using System.Collections.Generic;

public class Program
{
    static void Main(string[] args)
    {
        var words = new List<string>
        {
            "Apple",
            "Banana",
            "Orange",
            "Grape"
        };

        DoAnotherThing(words);
    }

    static void DoAnotherThing(List<string> words)
    {
        string sentence = "The quick brown fox jumps over the lazy dog";

        for (int i = 0; i < words.Count; i++)
        {
            for (int j = 0; j < sentence.Length; j++)
            {
                Console.Write(".");
            }

            Console.WriteLine();
        }
    }
}