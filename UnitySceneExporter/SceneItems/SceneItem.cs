using System.Collections.Generic;
using UnitySceneExporter.GenericSerialization;

namespace UnitySceneExporter
{
    public abstract class SceneItem
    {
        public abstract void Export(ISerializer serializer);
        public abstract List<SceneItem> Import(ISerializer serializer);
    }
}