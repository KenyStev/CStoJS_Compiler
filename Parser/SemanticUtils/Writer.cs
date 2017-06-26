using System;
using System.IO;

namespace Compiler.Writer
{
    public class Writer {
        private StreamWriter writer;

        public Writer(string path_to_file){

            if (!File.Exists(path_to_file)) {
                Console.WriteLine($"file {path_to_file} does not exist");
            }
            this.writer = File.CreateText(path_to_file);
        }

        public void WriteStringLine(string to_write)
        {
            writer.WriteLine(to_write);
        }

        public void WriteString(string to_write)
        {
            writer.Write(to_write);
        }

        public void Finish(){
            this.writer.Flush();
            this.writer.Dispose();
        }
    }
}