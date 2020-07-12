using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleGame.Engine.CoreModule.BaseData;

namespace ConsoleGame.Engine
{
    public sealed class RendererComponent : IComponent, IComparable<RendererComponent>
    {
        public CoreModule.Pixel objectPixel;
        public byte renderLayer;
        public Transform transform;
        public Rect bounds { get => RecalculateBounds(); }

        public RendererComponent()
        {
            this.objectPixel = new CoreModule.Pixel(' ', new Color32(), new Color32());
            this.renderLayer = 0;
        }

        public void SetRenderData(char symbol, Color32 symbolColor, Color32 backgroundColor, byte renderLayer = 3)
        {
            this.objectPixel = new CoreModule.Pixel(symbol, symbolColor, backgroundColor);
            this.renderLayer = renderLayer;
        }

        public int CompareTo(RendererComponent other)
        {
            if (this.renderLayer > other.renderLayer)
                return 1;
            if (this.renderLayer < other.renderLayer)
                return -1;
            return 0;
        }

        private Rect RecalculateBounds()
        {
            var objectRectPoint = transform.position + transform.pivot;
            return new Rect(objectRectPoint.x, objectRectPoint.y, transform.scale.x, transform.scale.y);
        }

        public void Initialize(GameObject gameObject)
        {
            transform = gameObject.GetComponent<Transform>();
            CoreModule.GameLoop.AddObjectToRender(this);
        }
    }
}