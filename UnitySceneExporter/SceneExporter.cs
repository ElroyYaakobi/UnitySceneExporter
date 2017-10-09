using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using UnitySceneExporter.GenericSerialization;

namespace UnitySceneExporter
{
    /// <summary>
    /// SceneExporter handles the export and import progress of the scene
    /// </summary>
    public static class SceneExporter
    {
        public static byte[] Export()
        {
            var serializer = new BinaryFormatterSerializer();

            var items = GetAllTypes();
            items.CallSerializationMethod("Export", serializer);

            return serializer.Data;
        }

        public static Scene Import(byte[] data)
        {
            var scene = new Scene();
            var serializer = new BinaryFormatterSerializer { Data = data };

            var items = GetAllTypes();
            scene.SceneItems = items.CallSerializationMethod("Import", serializer);

            return scene;
        }

        /// <summary>
        /// Are we inheriting from that type?
        /// </summary>
        private static bool Inherits(this Type type, Type target)
        {
            var current = type;
            while(current != null)
            {
                if (current == target)
                {
                    return true;
                }
                
                // if we are not inheriting from the base type, move to the next one till theres not a next one.
                current = current.BaseType;
            }
            return false;
        }
        
        /// <summary>
        /// Get all of the types in the assemblies
        /// </summary>
        private static List<Type> GetAllTypes()
        {
            var sceneItemType = typeof(SceneItem);
            var items = new List<Type>();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes();
                items.AddRange(types.Where(type => !type.IsAbstract && type.Inherits(sceneItemType)));
            }

            return items;
        }

        /// <summary>
        /// Get all of the types in the assemblies
        /// </summary>
        /// <returns></returns>
        private static MethodInfo GetInstancedMethod(this object instance, string methodName)
        {
            var type = instance.GetType();
            var method = type.GetMethod(methodName);
            return method;
        }

        /// <summary>
        /// Get all of the types in the assemblies
        /// </summary>
        private static Dictionary<Type, List<SceneItem>> CallSerializationMethod(this IEnumerable<Type> types, string methodName, ISerializer serializer)
        {
            var data = new Dictionary<Type, List<SceneItem>>();
            var parameters = new object[] { serializer };

            foreach (var type in types)
            {
                // Create a temp instance for serialization
                var instance = Activator.CreateInstance(type);

                var method = instance.GetInstancedMethod(methodName);
                if (method == null)
                {
                    continue;
                }

                data.Add(type, method.Invoke(instance, parameters) as List<SceneItem>);
            }

            return data;
        }
    }
}