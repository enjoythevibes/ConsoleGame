using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.Engine.CoreModule.BaseData
{
    public interface IComponent
    {
        void Initialize(GameObject gameObject);
    }

    public interface IComponentsList
    {
        void AddComponent(int entityID, IComponent component);
        void RemoveComponent(int entityID);
        IComponent GetComponent(int entityID);
    }
}
