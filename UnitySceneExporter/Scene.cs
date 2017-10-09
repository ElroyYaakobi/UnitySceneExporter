﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitySceneExporter
{
    public class Scene
    {
        internal Dictionary<Type, List<SceneItem>> SceneItems { get; set; } = new Dictionary<Type, List<SceneItem>>();

        public List<T> GetItems<T>() 
            where T : SceneItem
        {
            SceneItems.TryGetValue(typeof(T), out var items);
            return items?.Cast<T>().ToList();
        }
    }
}