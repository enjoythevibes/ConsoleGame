using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleGame.Engine.CoreModule.BaseData;

namespace ConsoleGame.Engine
{
    public sealed class GameObject
    {
        private int instanceID;
        private List<IComponentsList> componentsLists;
        public Transform transform { get; private set; }

        public GameObject()
        {
            componentsLists = new List<IComponentsList>();
            instanceID = GetHashCode();
            transform = AddComponent<Transform>().GetComponent<Transform>();
        }

        public GameObject(Vector2Int position, Vector2Int scale = new Vector2Int(), Vector2Int pivot = new Vector2Int())
        {
            componentsLists = new List<IComponentsList>();
            instanceID = GetHashCode();
            transform = AddComponent<Transform>().GetComponent<Transform>();
            transform.position = position;
            transform.scale = scale;
            transform.pivot = pivot;
        }

        public int GetInstanceID()
        {
            return instanceID;
        }

        public T GetComponent<T>() where T : IComponent
        {
            return (T)ComponentsCacheList<T>.instance.GetComponent(instanceID);
        }

        public GameObject AddComponent<T>() where T : IComponent, new()
        {
            var component = new T();
            component.Initialize(this);
            var componentCacheList = ComponentsCacheList<T>.instance;
            if (componentCacheList.GetComponent(instanceID) != null)
            {
                throw new ArgumentNullException($"GameObject {instanceID} already contain component with type {component.GetType().Name}");
            }
            componentsLists.Add(componentCacheList);
            componentCacheList.AddComponent(instanceID, component);
            return this;
        }

        public void RemoveComponent<T>() where T : IComponent
        {
            var componentCacheList = ComponentsCacheList<T>.instance;
            if (componentCacheList.GetComponent(instanceID) == null)
            {
                throw new ArgumentException($"GameObject {instanceID} doesnt contain component with type {typeof(T)}");
            }
            componentCacheList.RemoveComponent(instanceID);
            componentsLists.Remove(componentCacheList);
        }
    }
}
