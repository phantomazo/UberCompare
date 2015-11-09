using System;
using System.Text.RegularExpressions;
namespace UberCompare.Converters
{
    class CharConverter
    {  
        public static string ConvertCharsInString(string str)
        {
            return Regex.Replace(str.Trim(), @"[@^\|\[\]`}~{\\]", match => {
                switch (match.Value)
                {
                    case "^": return "Ü";
                    case "@": return "É";
                    case "[": return "Ä";
                    case "]": return "Å";
                    case "`": return "é";
                    case "|": return "ö";
                    case "}": return "å";
                    case "{": return "ä";
                    case "\\": return "Ö";
                    default: throw new Exception("Unexpected match!");
                }
            }); 
        }
    }
}
