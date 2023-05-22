using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HW_LINQ1
{
    public class File_reader {
        public  File_reader(string _path) {
        files = new List<object>();
            Read(_path);
        }
        private static Regex reg_word = new Regex(@"\w*(-||_)\w*");
        public List<object> files { get; set; }
        private void Read(string _path) {
            string strFile = "";
            try
            {
                StreamReader streamReader = new StreamReader(_path);
                for (int i = 0; !streamReader.EndOfStream; i++)
                {
                    strFile += streamReader.ReadLine();
                }
                MatchCollection words = reg_word.Matches(strFile);
                foreach (Match match in words)
                {
                    if (match.Value == "")
                        continue;
                    files.Add(match.Value.ToLower());
                }
                streamReader.Close();
            }
            catch
            {
                throw new Exception($"{_path} - неверный путь к файлу");
            }
        }        
    }
    internal class Program
    {
        static void WriteFile(List<object> listWord,string _path) {
            string path = Environment.CurrentDirectory;
            string time = DateTime.Now.ToString("HH.mm.ss");
            var streamwriter = new StreamWriter(_path + time + ".txt");
            for (int i = 0; i < listWord.Count; i++)
            {
                streamwriter.Write($"{listWord[i]} ");
            }
            streamwriter.Close();
            Console.WriteLine($"Файл сохранен в папке {path }");
        }
        static List<object> Сomparison(List<object> _arr1, List<object> _arr2) {            
            var resault = _arr1.Intersect(_arr2);
            List<object> list = new List<object>();
            foreach (var item in resault) {
                list.Add(item);
            }
            return list;
        }
        static void Main(string[] args)
        {
            string path1,path2;
            bool complete = false;
            try
            {
                if (args.Length == 2)
                {
                    path1 = args[0];
                    path2 = args[1];
                }
                else
                {
                    Console.WriteLine("Введите путь к первому файлу");
                    path1 = Console.ReadLine();
                    Console.WriteLine("Введите путь ко второму файлу");
                    path2 = Console.ReadLine();
                }
              
                File_reader file1 = new File_reader(path1);
                File_reader file2 = new File_reader(path2);
                List<object> files1 = file1.files;
                List<object> files2 = file2.files;
                List<object> resFile = Сomparison(files1,files2);
                Console.WriteLine( "Введите имя сохраняемого файла");
                string respath=Console.ReadLine();
                WriteFile(resFile, respath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        
    }
}
