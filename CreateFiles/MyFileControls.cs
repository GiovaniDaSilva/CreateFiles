using System;
using System.Collections.Generic;
using System.IO;

namespace CreateFiles
{
    public static class MyFileControls
    {

        public class MyFile
        {
            public MyFile(string path, string nome, string extensao)
            {
                this.path = path;
                this.nome = nome;
                this.extensao = extensao;
            }

            public MyFile(string path)
            {              
                if (File.Exists(path)){

                    string ext = Path.GetExtension(path).Replace(".", "");
                    string name = Path.GetFileName(path).Replace(ext, "").Replace(".", "");
                    string path2 = Path.GetDirectoryName(path);

                    this.path = path2;
                    this.extensao = ext;
                    this.nome = name;
                }

                ValidarPath();

            }

            public string path { get; set; }
            public string nome { get; set; }
            public string extensao { get; set; }


            private List<string> extensaoAceita = new List<string> {"txt", "json"};
            

            public bool IsValid()
            {
                ValidarPath();

                if (string.IsNullOrWhiteSpace(nome))
                {
                    throw new ArgumentNullException("Nome is null or white space");
                }

                if (string.IsNullOrWhiteSpace(extensao))
                {
                    throw new ArgumentNullException("Extensao is null or white space");
                }

                if (!extensaoAceita.Contains(extensao))
                {
                    // throw new FormatException("Extensão inválida");
                }

                return true;
            }

            private void ValidarPath()
            {
                if (string.IsNullOrWhiteSpace(path))
                {
                    throw new ArgumentNullException("Path is null or white space");
                }
            }

            public string getPathComp()
            {
                return path + @$"\{nome}.{extensao}";
            }
           
        }

        public static List<MyFile> GetFilesDirectory(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException("Path is null or white space");
            }

            string[] files = Directory.GetFiles(path);
            List<MyFile> result = new List<MyFile>();

            foreach(var file in files)
            {
                result.Add(new MyFile(file));
            }

            return result;
        }        
        
        public static MyFile Create(string path, string nome, string extensao)
        {
            return Create(new MyFile(path, nome, extensao));
        }

        public static void DeleteFile(MyFile? myFile)
        {
            if (File.Exists(myFile.getPathComp()))
            {
                File.Delete(myFile.getPathComp());
            }
        }

        public static MyFile Create(MyFile file)
        {
            if (!file.IsValid()) return file;

            if (!Directory.Exists(file.path))
            {
                Directory.CreateDirectory(file.path);
            }

            if (!File.Exists(file.getPathComp())){
                File.Create(file.getPathComp());
            }

            return file;
        }



    }
}
