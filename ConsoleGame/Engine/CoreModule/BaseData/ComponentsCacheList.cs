using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.Engine.CoreModule.BaseData
{
    public class ComponentsCacheList<T> : IComponentsList where T : IComponent
    {
        public static ComponentsCacheList<T> instance { get; private set; }

        static ComponentsCacheList()
        {
            instance = new ComponentsCacheList<T>();
        }

        private Dictionary<int, T> componentsCacheList;

        public ComponentsCacheList()
        {
            componentsCacheList = new Dictionary<int, T>();
        }

        public void AddComponent(int entityID, IComponent component)
        {
            if (!componentsCacheList.ContainsKey(entityID))
            {
                componentsCacheList.Add(entityID, (T)component);
            }
        }

        public IComponent GetComponent(int entityID)
        {
            if (!componentsCacheList.ContainsKey(entityID)) return default(T);
            return componentsCacheList[entityID];
        }

        public void RemoveComponent(int entityID)
        {
            if (componentsCacheList.ContainsKey(entityID))
            {
                componentsCacheList.Remove(entityID);
            }
        }
    }
}