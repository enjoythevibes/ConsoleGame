using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleGame.Engine;
using ConsoleGame.Engine.CoreModule;

namespace ConsoleGame.Game
{
    public class LightChanger : Behaviour
    {
        private float3 direction;
        private LightSource light;
        private static Random randomObj = new Random();

        public override void Start()
        {
            direction = new float3(5, 5, 5);
            light = gameObject.GetComponent<LightSource>();
            light.lightColor = new Color32((byte)randomObj.Next(0, 256), (byte)randomObj.Next(0, 256), (byte)randomObj.Next(0, 256));
        }

        public override void Update()
        {
            if(light.lightColor.r == 255 || light.lightColor.r == 0)
            {
                direction.x *= -1;
            }
            if (light.lightColor.g == 255 || light.lightColor.g == 0)
            {
                direction.y *= -1;
            }
            if (light.lightColor.b == 255 || light.lightColor.b == 0)
            {
                direction.z *= -1;
            }
            if (direction.x > 0)
            {
                light.lightColor += new Color32((byte)Math.Abs(direction.x), 0, 0);
            }
            else
            if (direction.x < 0)
            {
                light.lightColor -= new Color32((byte)Math.Abs(direction.x), 0, 0);
            }

            if (direction.y > 0)
            {
                light.lightColor += new Color32(0, (byte)Math.Abs(direction.y), 0);
            }
            else
            if (direction.y < 0)
            {
                light.lightColor -= new Color32(0, (byte)Math.Abs(direction.y), 0);
            }

            if (direction.z > 0)
            {
                light.lightColor += new Color32(0, 0, (byte)Math.Abs(direction.z));
            }
            else
            if (direction.z < 0)
            {
                light.lightColor -= new Color32(0, 0, (byte)Math.Abs(direction.z));
            }
        }
    }
}
