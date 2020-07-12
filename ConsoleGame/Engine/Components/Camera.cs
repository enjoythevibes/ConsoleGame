using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleGame.Engine.CoreModule.BaseData;

namespace ConsoleGame.Engine
{
    public sealed class Camera : IComponent
    {
        public Transform transform;             

        public void Initialize(GameObject gameObject)
        {
            transform = gameObject.GetComponent<Transform>();
            if (transform == null) throw new ArgumentException($"{GetType().Name}, component Transform not found / Add Transform component first");
            Engine.CoreModule.GameLoop.AddCameraToRender(this);
        }
    }
}