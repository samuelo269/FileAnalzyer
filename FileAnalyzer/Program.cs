using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace FileAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            // Main loop
            while (true)
            {
                // Get path input 
                Console.WriteLine("Please provide a folder or a json with folder information: ");
                string path = Console.ReadLine();
                List<FileDesc> files = new List<FileDesc>();

                // Check if path is .json | path is folder | exit command
                if (path.EndsWith(".json"))
                {
                    files = LoadJson(path);
                    List<string> extensions = GetExtensions(files);

                    Console.WriteLine(string.Format("Extensions found in folder: ({0}).", string.Join(", ", extensions)));
                }
                else if (path == "exit")
                {
                    break;
                }
                else
                {
                    files = GetFiles(path);
                    List<string> extensions = GetExtensions(files);

                    Console.WriteLine(string.Format("Extensions found in folder: ({0}).", string.Join(", ", extensions)));
                }

                // Ask for save in json file
                Console.WriteLine("Save to JSON? (y/n)");
                string jsonSave = Console.ReadLine();

                if (jsonSave == "n")
                {
                    continue;
                }
                else if (jsonSave == "y")
                {
                    HandleJson(files);
                }
                else
                {
                    Console.WriteLine("Invalid input");
                    break;
                }
            }
        }

        private static List<FileDesc> LoadJson(string path)
        {
            // Load and deserialize json file
            var jsonFolder = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<List<FileDesc>>(jsonFolder);
        }

        private static void HandleJson(List<FileDesc> files)
        {
            // Serialize object to json and write it to file
            var filesJson = JsonConvert.SerializeObject(files);
            Console.WriteLine("Please prvide the JSON file location");
            string jsonPath = Console.ReadLine();
            if (File.Exists(jsonPath))
            {
                File.WriteAllText(jsonPath, filesJson);
            }
            else
            {
                Console.WriteLine("Invalid File");
            }
        }

        private static List<string> GetExtensions(List<FileDesc> files)
        {
            // Get each extension in folder and return list of extensions
            List<string> ext = new List<string>();
            foreach (FileDesc file in files)
            {
                if (!ext.Contains(file.extension))
                {
                    ext.Add(file.extension);
                }
            }

            return ext;
        }

        private static List<FileDesc> GetFiles(string path)
        {
            // Get all the files paths, creates and return list of objects
            List<FileDesc> files = new List<FileDesc>();
            string[] paths = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);

            foreach (string file in paths)
            {
                FileInfo fileInfo = new FileInfo(file);
                files.Add(new FileDesc
                {
                    name = fileInfo.Name,
                    path = fileInfo.DirectoryName,
                    extension = fileInfo.Extension,
                    created = fileInfo.CreationTime
                });
            }
            return files;
        }
    }
}
