using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2dxlib
{
    public class dxlib
    {
        public static bool vaild2dx(string filepath)
        {
            string a = "";
            using (BinaryReader rdr = new BinaryReader(File.Open(filepath, FileMode.Open)))
            {
                char[] name = rdr.ReadChars(16);
                UInt32 headersize = rdr.ReadUInt32();
                UInt32 filecount = rdr.ReadUInt32();
                char[] unknown = rdr.ReadChars(48);
                char[] dummy = rdr.ReadChars(Convert.ToInt16(headersize) - 72);
                //for (int i = 0; i < filecount; i++)
                //{
                    char[] dx = rdr.ReadChars(4);
                    UInt32 dxheadersize = rdr.ReadUInt32();
                    UInt32 wavSize = rdr.ReadUInt32();
                    Int16 unk1 = rdr.ReadInt16();
                    Int16 trackID = rdr.ReadInt16();
                    Int16 unk2 = rdr.ReadInt16();
                    Int16 attenuation = rdr.ReadInt16();
                    Int32 looppoint = rdr.ReadInt32();
                    /*FileStream writewav = File.Open(i + @".wav", FileMode.Create);
                    using (BinaryWriter wr = new BinaryWriter(writewav))
                    {
                        Console.WriteLine("{0}.wav 쓰는 중...", i);
                        wr.Write(rdr.ReadBytes(Convert.ToInt32(wavSize)));
                        wr.Close();
                    }*/
                //}
                rdr.Close();
                for(int i=0; i<4; i++)
                {
                    a += dx[i];
                }
            }
            if (a == "2DX9")
            {
                return true;
            }
            else
            {
                return false;
            }
        } 
    }
}
