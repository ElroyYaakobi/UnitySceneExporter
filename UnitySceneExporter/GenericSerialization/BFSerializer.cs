using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace UnitySceneExporter.GenericSerialization
{
    public class BFSerializer : ISerializer
    {
        #region Custom
        private BinaryFormatter bf;
        private MemoryStream ms;

        public BFSerializer()
        {
            bf = new BinaryFormatter();
            ms = new MemoryStream();
        }
        #endregion

        #region Serialization
        public byte[] data
        {
            get
            {
                return ms.ToArray();
            }
            set
            {
                ms = new MemoryStream(value);
            }
        }

        public void Deserialize(object obj)
        {
            throw new NotImplementedException();
        }
        public void Serialize(object obj)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
