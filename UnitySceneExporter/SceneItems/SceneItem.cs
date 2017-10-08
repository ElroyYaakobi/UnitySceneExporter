using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnitySceneExporter.GenericSerialization;

namespace UnitySceneExporter
{
    /// <summary>
    /// A scene item
    /// </summary>
    public abstract class SceneItem
    {
        #region Serialization
        /// <summary>
        /// Deserialize 
        /// </summary>
        /// <param name="ms"></param>
        /// <param name="bf"></param>
        public abstract void Export(ISerializer serializer);

        /// <summary>
        /// Serialize
        /// </summary>
        /// <param name="ms"></param>
        /// <param name="bf"></param>
        public abstract List<SceneItem> Import(ISerializer serializer);
        #endregion
    }
}
