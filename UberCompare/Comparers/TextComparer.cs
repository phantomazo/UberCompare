using System.Collections.Generic;
using System.Threading.Tasks;
using UberCompare.Converters;
using System.IO;
namespace UberCompare.Comparers
{
    class TextComparer : IComparer
    {
   
        public bool UseConversion { get; set; }
        private void ConvertAndSort(List<string> list, bool useConversion)
        {

            if (useConversion)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    list[i] = CharConverter.ConvertCharsInString(list[i]);
                }
            }
            list.Sort();
        }

        public List<string> Compare(Stream left, Stream right)
        {
            List<string> leftLines = new List<string>();
            List<string> rightLines = new List<string>();

            var leftWrite = Task.Run(() =>
            {
                using (StreamReader reader = new StreamReader("example.txt"))
                {
                    while (!reader.EndOfStream)
                    {
                        leftLines.Add(reader.ReadLine());
                    }
                }
            });
            var rightWrite = Task.Run(() =>
            {
                using (StreamReader reader = new StreamReader("example.txt"))
                {
                    while (!reader.EndOfStream)
                    {
                        rightLines.Add(reader.ReadLine());
                    }
                }
            });

            rightWrite.Wait();
            leftWrite.Wait();

            return Compare(leftLines, rightLines);
    
        }
        public List<string> Compare(List<string> left, List<string> right)
        {
            List<string> errors = new List<string>();
            if (left.Count == right.Count)
            {
                var leftSort = Task.Run(() =>
                {
                    ConvertAndSort(left, UseConversion);
                });
                var rightSort = Task.Run(() =>
                {
                    ConvertAndSort(right, UseConversion);
                });

                rightSort.Wait();
                leftSort.Wait();

                for (int i = 0; i < left.Count; i++)
                {
                    if (!left[i].Equals(right[i]))
                    {
                        errors.Add("Difference in sorted file row number: " + (i + 1));
                    }
                }
            }
            else
            {
                errors.Add("Different numbers of rows.\r\n Left file: " + left.Count + ". \r\n Right file:" + right.Count);

            }
            return errors;
        }
    }
}
