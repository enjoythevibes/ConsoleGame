using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleGame.Engine.CoreModule.BaseData;

namespace ConsoleGame.Engine
{
    public sealed class LightSource : IComponent
    {
        public Transform transform;
        public Color32 lightColor;
        public int lightRange;
        public int lightFalloff;
        public Rect boudns { get => CalculateBounds(); }

        public void SetLightData(Color32 lightColor, int lightRange, int lightFalloff = LightingSettings.lightFalloff)
        {
            this.lightColor = lightColor;
            this.lightRange = lightRange;
            this.lightFalloff = lightFalloff;
        }

        private Rect CalculateBounds()
        {
            var objectRectPoint = transform.position;
            return new Rect(objectRectPoint.x - lightRange - lightFalloff, objectRectPoint.y - lightRange - lightFalloff, lightRange * 2 + 1 + lightFalloff * 2, lightRange * 2 + 1 + lightFalloff * 2);
        }

        public void Initialize(GameObject gameObject)
        {
            transform = gameObject.GetComponent<Transform>();
            CoreModule.GameLoop.AddLightSource(this);
        }
    }
}