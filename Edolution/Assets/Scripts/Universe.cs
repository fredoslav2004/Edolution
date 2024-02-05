using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public static class Universe
{
    // Generate a random string of given length using allowed characters
    public static string GenerateRandomString(int length, string allowedChars)
    {
        if (string.IsNullOrEmpty(allowedChars))
        {
            throw new ArgumentException("Allowed characters set cannot be null or empty.");
        }

        var stringBuilder = new StringBuilder(length);
        for (int i = 0; i < length; i++)
        {
            stringBuilder.Append(allowedChars[Random.Range(0,allowedChars.Length)]);
        }

        return stringBuilder.ToString();
    }

    // Split the string into segments based on lengths provided in an array
    public static string[] SplitString(string str, int[] lengths)
    {
        if (str == null) throw new ArgumentNullException(nameof(str));
        if (lengths == null) throw new ArgumentNullException(nameof(lengths));

        return lengths
            .Select((length, index) => str.Substring(lengths.Take(index).Sum(), length))
            .ToArray();
    }

    // Get a substring corresponding to a given index based on segment lengths
    public static string GetSubstringByIndex(string str, int[] lengths, int index)
    {
        if (str == null) throw new ArgumentNullException(nameof(str));
        if (lengths == null) throw new ArgumentNullException(nameof(lengths));
        if (index < 0 || index >= lengths.Length) throw new ArgumentOutOfRangeException(nameof(index));

        int start = lengths.Take(index).Sum();
        return str.Substring(start, lengths[index]);
    }
    // Convert an angle in radians to a normalized 2D vector
    public static Vector2 AngleToNormalizedVector(float angleInDegrees)
    {
        return new Vector2((float)Math.Cos(math.radians(angleInDegrees)),(float)Math.Sin(math.radians(angleInDegrees)));
    }
    // Convert a string array to their normalized float representations
    public static float[] ConvertAndNormalize(string[] numbers, string numberSystem)
    {
        int baseSystem = numberSystem.Length; // Determine the base from the length of the numberSystem string

        // Convert each number to its decimal equivalent
        int[] decimalValues = numbers.Select(number => ConvertToDecimal(number, numberSystem)).ToArray();

        // Normalize each decimal value to a float between 0 and 1
        float[] normalizedValues = decimalValues.Select(value => Normalize(value, baseSystem, numbers.Max(n => n.Length))).ToArray();

        return normalizedValues;
    }

    // Convert a number from the given numbering system to decimal
    public static int ConvertToDecimal(string number, string numberSystem)
    {
        int baseSystem = numberSystem.Length;
        int decimalValue = 0;

        for (int i = 0; i < number.Length; i++)
        {
            int digitValue = numberSystem.IndexOf(number[i]);
            if (digitValue == -1) throw new ArgumentException($"Invalid character {number[i]} in the number {number}.");

            decimalValue = decimalValue * baseSystem + digitValue;
        }

        return decimalValue;
    }

    // Normalize a decimal number to a float between 0 and 1
    public static float Normalize(int value, int baseSystem, int maxLength)
    {
        double maxValue = Math.Pow(baseSystem, maxLength) - 1;
        return (float)(value / maxValue);
    }
    public static float[] ExpressGenome(string genome, int[] lengths, string numberSystem) => ConvertAndNormalize(SplitString(genome, lengths), numberSystem);
}
