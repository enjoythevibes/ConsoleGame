using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleGame.Engine.CoreModule.BaseData;

namespace ConsoleGame.Engine
{
    public sealed class Transform : IComponent
    {
        public GameObject gameObject;
        public Vector2Int position;
        public Vector2Int scale;
        public Vector2Int pivot;

        public Transform()
        {
            this.position = new Vector2Int(0, 0);
            this.scale = new Vector2Int(0, 0);
            this.pivot = new Vector2Int(0, 0);
        }

        // public void CalculatePivot()
        // {
        //     var centerX = 0;
        //     var centerY = 0;
        //     if(scale.x % 2 == 1)
        //     {
        //         centerX = scale.x / 2;
        //     }
        //     else
        //     {
        //         Debug.Error($"{gameObject.GetInstanceID()}, Cant calculate pivot point _X_, scale size must be odd to calculate center of pivot point");
        //     }

        //     if(scale.y % 2 == 1)
        //     {
        //         centerY = scale.y / 2;
        //     }
        //     else
        //     {
        //         Debug.Error($"{gameObject.GetInstanceID()}, Cant calculate pivot point _Y_, scale size must be odd to calculate center of pivot point");
        //     }
        //     pivot = new Vector2Int(centerX, centerY);
        // }

        public void Initialize(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }
    }
}