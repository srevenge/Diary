using System;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;

namespace Model
{
    public static class FileStreams
    {
        public static List<Memory> deSerialize(string path)
        {
            if (File.Exists(path))
            {
                using(Stream stream = File.Open(path, FileMode.Open))
                {
                    var bFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                    List<Memory> l = new List<Memory>();
                    if(stream.Length < 300)
                        l.Add((Memory)bFormatter.Deserialize(stream));
                    else
                        l = (List<Memory>)bFormatter.Deserialize(stream);
                    return l;
                
                }
            }
            return null;
        }


        public static void Serialize(string path, Memory m)
        {
            List<Memory> l = deSerialize(path);
            if(l == null)
                l = new List<Memory>();

            l.Add(m);
            using(Stream stream = File.Open(path, FileMode.Create))
            {
                var bFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                bFormatter.Serialize(stream, l);
            }
        }


        public static void SerializeAll(string path, List<Memory> l)
        {
            using(Stream stream = File.Open(path, FileMode.Create))
            {
                var bFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                bFormatter.Serialize(stream, l);
            }
        }
    }
}
