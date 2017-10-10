using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace UnitySceneExporter.GenericSerialization
{
    public class BinaryFormatterSerializer : ISerializer
    {
        private readonly BinaryFormatter binaryFormatter = new BinaryFormatter();
        private MemoryStream memoryStream = new MemoryStream();

        public byte[] data
        {
            get
            {
                return memoryStream.ToArray();
            }
            set
            {
                memoryStream = new MemoryStream(value);
            }
        }

        public void Serialize(object obj)
        {
            binaryFormatter.Serialize(memoryStream, obj);
        }
        public object Deserialize()
        {
            return binaryFormatter.Deserialize(memoryStream);
        }
    }
}