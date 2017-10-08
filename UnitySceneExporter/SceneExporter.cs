using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnitySceneExporter.GenericSerialization;

namespace UnitySceneExporter
{
    /// <summary>
    /// Handles the export and import progress of the scene.
    /// </summary>
    public static class SceneExporter
    {
        #region Export & Import Methods
        /// <summary>
        /// Export the scene...
        /// </summary>
        public static byte[] Export()
        {
            // create serializer
            BFSerializer serializer = new BFSerializer();

            // get items and export data
            List<Type> items = GetAllTypes();
            items.CallSerializationMethod("Export", serializer);

            return serializer.data;
        }

        /// <summary>
        /// Import the scene...
        /// </summary>
        public static Scene Import(byte[] data)
        {
            // create scene instance
            Scene sInstance = new Scene();

            // create serializer
            BFSerializer serializer = new BFSerializer();
            serializer.data = data;

            // get items and import data
            List<Type> items = GetAllTypes();
            sInstance.sceneItems = items.CallSerializationMethod("Import", serializer);

            return sInstance;
        }
        #endregion

        #region Utility
        /// <summary>
        /// Are we inheriting from that type?
        /// </summary>
        /// <param name="type"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        private static bool Inherits(this Type type, Type target)
        {
            Type current = type;
            while(current != null)
            {
                if (current == target)
                {
                    return true;
                }
                
                // if we are not inheriting from the base type, move to the next one till theres not a next one
                current = current.BaseType;
            }

            return false;
        }
        
        /// <summary>
        /// Get all of the types in the assemblies.
        /// </summary>
        /// <returns></returns>
        private static List<Type> GetAllTypes()
        {
            // statics
            Type sceneItemType = typeof(SceneItem);

            // local variables
            List<Type> items = new List<Type>();
            Assembly assembly;
            Type[] types;
            Type type;

            // loop through
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            for (int i = 0; i < assemblies.Length; i++)
            {
                assembly = assemblies[i];
                types = assembly.GetTypes();

                for (int b = 0; b < types.Length; b++)
                {
                    type = types[b];

                    if (!type.IsAbstract && type.Inherits(sceneItemType))
                    {
                        items.Add(type);
                    }
                }
            }

            return items;
        }

        /// <summary>
        /// Get all of the types in the assemblies.
        /// </summary>
        /// <returns></returns>
        private static MethodInfo GetInstancedMethod(this object instance, string methodName)
        {
            var type = instance.GetType();
            var method = type.GetMethod(methodName);

            return method;
        }

        /// <summary>
        /// Get all of the types in the assemblies.
        /// </summary>
        /// <returns></returns>
        private static Dictionary<Type, List<SceneItem>> CallSerializationMethod(this List<Type> types, string methodName, ISerializer serializer)
        {
            // variables
            Dictionary<Type, List<SceneItem>> returnData = new Dictionary<Type, List<SceneItem>>();
            object[] parameters = new object[] { serializer };

            // run method
            Type type;
            MethodInfo method;
            object tempInstance;

            for (int i = 0; i < types.Count; i++)
            {
                type = types[i];

                // create a temp instance for serialization
                tempInstance = Activator.CreateInstance(type);

                // run
                method = tempInstance.GetInstancedMethod(methodName);
                if (method == null) continue;

                returnData.Add(type, method.Invoke(null, parameters) as List<SceneItem>);

                // reset
                tempInstance = null;
            }

            return returnData;
        }
        #endregion
    }

    /// <summary>
    /// Scene
    /// </summary>
    public class Scene
    {
        #region Variables
        private Dictionary<Type, List<SceneItem>> _sceneItems = new Dictionary<Type, List<SceneItem>>();
        internal Dictionary<Type, List<SceneItem>> sceneItems
        {
            get
            {
                return _sceneItems;
            }
            set
            {
                _sceneItems = value;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Get items...
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> GetItems<T>() where T : SceneItem
        {
            List<SceneItem> items = new List<SceneItem>();
            sceneItems.TryGetValue(typeof(T), out items);

            return items.Cast<T>().ToList();
        }
        #endregion
    }
}
