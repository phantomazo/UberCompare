using System.Collections.Generic;
using System.IO;
namespace UberCompare.Comparers
{
    interface IComparer
    {
        bool UseConversion { get; set; }
        List<string> Compare(Stream left, Stream right);
    }
}
