using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace ShopTestMVC.Tools
{
    public class FileCSVReader
    {
        List<string[]> table;

        public FileCSVReader()
        {
        }


        public List<string[]> get_DataLines()
        {
            return table;
        }


        /// <summary>
        /// Разборка файла cразделителями в массив строк
        /// </summary>
        /// <param name="FilePath"></param>
        public FileCSVReader(string FilePath)
        {
            table = new List<string[]>();
            using (var r = new StreamReader(FilePath))
            {
                while (!r.EndOfStream)
                {
                    string line = r.ReadLine();
                    var linePrepare = Regex.Replace(line, @"\s+", System.String.Empty);
                    table.Add(Regex.Split(linePrepare, @"\s|[;]|[,]|[\b]"));
                }

                r.Close();
            }
        }

    }
}