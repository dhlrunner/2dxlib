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
        public static object[] vaild2dx(string filepath)
        {
            object[] rtn = new object[4];
            string a = "";
            string fname = "";
            int b;
            try
            {
                using (BinaryReader rdr = new BinaryReader(File.Open(filepath, FileMode.Open)))
                {
                    char[] name = rdr.ReadChars(16);
                    UInt32 headersize = rdr.ReadUInt32();
                    UInt32 filecount = rdr.ReadUInt32();
                    char[] unknown = rdr.ReadChars(48);
                    char[] dummy = rdr.ReadChars(Convert.ToInt16(headersize) - 72);
                    char[] dx = rdr.ReadChars(4);
                    UInt32 dxheadersize = rdr.ReadUInt32();
                    UInt32 wavSize = rdr.ReadUInt32();
                    Int16 unk1 = rdr.ReadInt16();
                    Int16 trackID = rdr.ReadInt16();
                    Int16 unk2 = rdr.ReadInt16();
                    Int16 attenuation = rdr.ReadInt16();
                    Int32 looppoint = rdr.ReadInt32();
                    rdr.Close();
                    for (int i = 0; i < 4; i++)
                    {
                        a += dx[i];
                    }
                    for (int i = 0; i < 16; i++)
                    {
                        fname += name[i];
                    }
                    b = unk1;
                    if (a == "2DX9" && b == 12849)
                    {
                        rtn[0] = true;
                        rtn[1] = fname;
                        rtn[2] = headersize;
                        rtn[3] = filecount;
                    }
                    else
                    {
                        rtn[0] = false;
                    }
                    return rtn;
                }
            }
            catch
            {
                rtn[0] = false;
                return rtn;
            }


        }
        public static bool dxextract(string @infilepath, string outfilepath)
        {
            
                using (BinaryReader rdr = new BinaryReader(File.Open(@infilepath, FileMode.Open)))
                {
                    char[] name = rdr.ReadChars(16);
                    UInt32 headersize = rdr.ReadUInt32();
                    UInt32 filecount = rdr.ReadUInt32();
                    char[] unknown = rdr.ReadChars(48);
                    char[] dummy = rdr.ReadChars(Convert.ToInt16(headersize) - 72);

                    //Console.WriteLine("{0} 파일 정보", @infilepath);
                    //Console.Write("헤더 이름:");
                    //for (int i = 0; i < 16; i++)
                    //{
                    //    Console.Write(name[i]);
                    //}

                    //Console.WriteLine();
                    //Console.WriteLine("헤더 크기: " + headersize + " 바이트");
                    //Console.WriteLine("파일에 포함된 wav 파일 갯수: " + filecount + "개");
                    for (int i = 0; i < filecount; i++)
                    {
                        char[] dx = rdr.ReadChars(4);
                        UInt32 dxheadersize = rdr.ReadUInt32();
                        UInt32 wavSize = rdr.ReadUInt32();
                        Int16 unk1 = rdr.ReadInt16();
                        Int16 trackID = rdr.ReadInt16();
                        Int16 unk2 = rdr.ReadInt16();
                        Int16 attenuation = rdr.ReadInt16();
                        Int32 looppoint = rdr.ReadInt32();
                        //Console.WriteLine((i + 1) + "번째 dx 헤더 크기: " + dxheadersize + " 바이트");
                        //Console.WriteLine((i + 1) + "번째 wav 파일 크기: " + wavSize + "바이트");
                        //Console.WriteLine((i + 1) + "번째 트랙ID: " + trackID);
                        FileStream writewav = File.Open(@outfilepath +"\\"+ i + @".wav", FileMode.Create);
                        using (BinaryWriter wr = new BinaryWriter(writewav))
                        {
                            //Console.WriteLine("{0}.wav 쓰는 중...", i);
                            wr.Write(rdr.ReadBytes(Convert.ToInt32(wavSize)));
                            wr.Close();
                        }

                    }
                    rdr.Close();
                    return true;
                    //for(int i = 0; i < 48; i++)
                    //{
                    //    Console.Write(unknown[i]+" ");
                    // }
                    //Console.WriteLine("모든 파일 추출이 완료되었습니다.");
                }
                
            
            
        }
    }
}
