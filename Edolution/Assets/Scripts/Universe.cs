using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public static Vector2 AngleToNormalizedVector(float angleInRadians)
    {
        return new Vector2((float)Math.Cos(angleInRadians),(float)Math.Sin(angleInRadians));
    }
}
