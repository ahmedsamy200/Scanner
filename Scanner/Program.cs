using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class Tokenizer
{
    // Define regex patterns for different token classes
    private static readonly string[] Keywords = new string[] {
        "if", "else", "while", "for", "return", "int", "float", "char", "void"
    };

    private static readonly Dictionary<string, string> Patterns = new Dictionary<string, string>()
    {
        { "keyword", @"\b(?:if|else|while|for|return|int|float|char|void)\b" },  // Matches any keyword
        { "identifier", @"\b[a-zA-Z_][a-zA-Z0-9_]*\b" },  // Matches valid identifiers
        { "operator", @"[+\-*/%=<>!&|^~]" },  // Matches common operators
        { "numeric_constant", @"\b\d+(\.\d+)?\b" },  // Matches numeric constants (integer or float)
        { "character_constant", @"'[^']*'" },  // Matches character constants (e.g., 'a', '\n')
        { "special_character", @"[()\[\]{}.,;]" },  // Matches special characters like ( ) { } , ;
        { "comment", @"//.?$|/\.?\/" },  // Matches single-line and multi-line comments
        { "whitespace", @"\s+" }  // Matches whitespace (spaces, tabs, newlines)
    };

    private static string ClassifyToken(string token)
    {
        foreach (var pattern in Patterns)
        {
            if (Regex.IsMatch(token, pattern.Value))
            {
                return pattern.Key;
            }
        }
        return "unknown";
    }

    private static List<(string Token, string TokenClass)> TokenizeCode(string code)
    {
        List<(string Token, string TokenClass)> tokenClasses = new List<(string, string)>();

        string formattedCode = Regex.Replace(code, @"([()\[\]{}.,;])", " $1 ");
        formattedCode = Regex.Replace(formattedCode, @"\s+", " ");

        string[] tokens = formattedCode.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        foreach (string token in tokens)
        {
            string tokenClass = ClassifyToken(token.Trim());
            tokenClasses.Add((token.Trim(), tokenClass));
        }

        return tokenClasses;
    }

    static void Main()
    {
        Console.WriteLine("Enter the context: ");
        string codeInput = Console.ReadLine();


        List<(string Token, string TokenClass)> tokenClasses = TokenizeCode(codeInput);

        foreach (var (token, tokenClass) in tokenClasses)
        {
            Console.WriteLine($"Token: '{token}', Class: {tokenClass}");
        }
    }
}