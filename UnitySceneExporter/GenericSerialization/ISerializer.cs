using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitySceneExporter.GenericSerialization
{
    public interface ISerializer
    {
        byte[] data { get; set; }

        void Serialize(object obj);
        void Deserialize(object obj);
    }
}
