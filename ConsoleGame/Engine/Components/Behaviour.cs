using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleGame.Engine.CoreModule.BaseData;

namespace ConsoleGame.Engine
{
    public abstract class Behaviour : IComponent
    {
        protected virtual int ExecuteOrder { get => -1; }

        public GameObject gameObject;

        public virtual void Start() { }
        public virtual void Update() { }

        public void Initialize(GameObject gameObject)
        {
            this.gameObject = gameObject;
            Start();
            Engine.CoreModule.GameLoop.AddBehaviourToUpdate(this, ExecuteOrder);
        }
    }
}