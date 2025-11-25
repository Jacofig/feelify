using UnityEngine;
using System.Collections.Generic;

public static class PythonKeyWordLibrary
{
    public const string IF = "if";
    public const string WHILE = "while";
    public const string FOR = "for";
    public const string DEF = "def";
    public const string ELSE = "else";

    public static readonly HashSet<string> Keywords = new HashSet<string>
    {
        IF, WHILE, FOR, DEF, ELSE
    };

    public static bool IsKeyword(string word)
    {
        return Keywords.Contains(word);
    }
}
