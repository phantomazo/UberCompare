using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UberCompare.Models;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;
namespace UberCompare
{
    class FileHandler
    {
        FileStream file = null;
        private bool loaded;

        public bool Loaded
        {
            get
            {
                return loaded;
            }
        }
  
        public static long offset = 0x00000000; // 0 megabytes
        public static long ChunkSize = 0x800000; // 8 megabytes
        public FileHandler()
        {
            offset = 0;
        }
        public void GetNextChunk()
        {
            throw new NotImplementedException();
            //if (Loaded && file.Length > offset )
            //    file.CreateViewAccessor(offset, ChunkSize);
            //offset += ChunkSize;
        }

        public Stream GetStream()
        {
            if (loaded)
            {
                return file;
            }
            throw new Exception("Error creating stream.");
        }
        public BinaryReader GetBinaryReader()
        {
            if (loaded)
            {
                return new BinaryReader(GetStream());
            }
            throw new Exception("Error creating stream.");
        }

        public TextReader GetTextReader()
        {
            if (loaded)
            {
                return new StreamReader(GetStream());
            }
            throw new Exception("Error creating stream.");
        }
        public void SaveFile(string fileName)
        {
            throw new NotImplementedException();
        }
        public bool LoadFile(string fileName)
        {
            try
            {
                file = File.OpenRead(fileName);             
                loaded = true;
            }
            catch (Exception e)
            {
                //Log("Error loading left file. Msg: " + e.Message);
                loaded = false;
            }
            return loaded;
        }
    }
}
