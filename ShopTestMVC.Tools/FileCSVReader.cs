using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace ShopTestMVC.Tools {
    public class FileCSVReader {
        List<string[]> table;

        public List<string[]> get_DataLines() {
            return table;
        }

        /// <summary>
        /// Разборка файла cразделителями в массив строк
        /// </summary>
        /// <param name="FilePath"></param>
        public FileCSVReader(string FilePath, Stream fileStream) {

            using (var br = new BinaryReader(fileStream)) {
                // приводим к utf-8
                byte[] ansiBytes = br.ReadBytes((int)fileStream.Length);
                string utf8String = Encoding.Default.GetString(ansiBytes);
                File.WriteAllText(FilePath, utf8String);
            }

            table = new List<string[]>();
            using (var r = new StreamReader(FilePath)) {
                while (!r.EndOfStream) {
                    string line = r.ReadLine();
                    // уберем лишние пробелы из строк
                    string linePrepare = Regex.Replace(line, @"\s+", System.String.Empty);
                    table.Add(Regex.Split(linePrepare, @"\s|[;]|[,]|[\b]"));
                }
                r.Close();
            }
            table = FindNameDuplicates(table);
        }

        /// <summary>
        /// Проверка на дубли по связке Name+Id
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        private List<string[]> FindNameDuplicates(List<string[]> table) {

            var stringSet = new Dictionary<string, int>();
            var tableChecked = new List<string[]>();
            tableChecked.Add(table[0]);

            foreach (string[] item in table) {
                try {
                    if (item[0] != "Id") {
                        int count = 0;
                        Int32.TryParse(item[0], out count);
                        if (!String.IsNullOrEmpty(item[1])) {
                            stringSet.Add(item[1], count);
                            tableChecked.Add(item);
                        }
                    }
                }
                catch (Exception) {
                    //пропускаем 
                }
            }
            return tableChecked;
        }

    }
}