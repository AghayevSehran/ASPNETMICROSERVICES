// See https://aka.ms/new-console-template for more information
using System;
using System.Collections;
using System.Collections.Generic;
//
public class TextInput
{
    public string current = "";
    public virtual void Add(char c)
    {
        current += c;
    }
    public string GetValue()
    {
        return current;
    }
}

public class NumericInput : TextInput
{

    public override void Add(char c)
    {
        if (Char.IsDigit(c))
            current += c;
    }

}
public class MergeNames
{
    public static string[] UniqueNames(string[] names1, string[] names2)
    {
        Hashtable data = new Hashtable();

        for (int i = 0; i < names1.Length; i++)
        {
            if (!data.ContainsKey(names1[i]))
            {
                data.Add(names1[i], names1[i]);
            }

        }
        for (int i = 0; i < names2.Length; i++)
        {
            if (!data.ContainsKey(names2[i]))
            {
                data.Add(names2[i], names2[i]);
            }

        }
        string[] r = new string[data.Count];
        int j = 0;
        foreach (DictionaryEntry entry in data)
        {
        
            r[j] = entry.Key.ToString();
            j++;
        }
    
        
        return r;

    }

    public static void Main(string[] args)
    {
        /*
        string[] names1 = new string[] { "Ava", "Emma", "Olivia" };
        string[] names2 = new string[] { "Olivia", "Sophia", "Emma" };
        Console.WriteLine(string.Join(", ", MergeNames.UniqueNames(names1, names2))); // should print Ava, Emma, Olivia, Sophia
        */

        TextInput input = new NumericInput();
        input.Add('1');
        input.Add('a');
        input.Add('0');
        Console.WriteLine(input.GetValue());

    }
}
