using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace UnitySceneExporter.GenericSerialization
{
    public class BinaryFormatterSerializer : ISerializer
    {
        private readonly BinaryFormatter binaryFormatter = new BinaryFormatter();
        private MemoryStream memoryStream = new MemoryStream();

        public byte[] Data
        {
            get => memoryStream.ToArray();
            set => memoryStream = new MemoryStream(value);
        }

        public void Serialize(object obj)
        {
            throw new NotImplementedException();
        }

        public void Deserialize(object obj)
        {
            throw new NotImplementedException();
        }
    }
}