using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleGame.Engine;

namespace ConsoleGame.Game.Entities
{
    public class SimpleBox : Behaviour
    {
        private Vector2 position;
        public int direction = -1;

        public override void Start()
        {
            // Debug.Log($"{gameObject.GetInstanceID()} Simple box start");
            position = gameObject.transform.position;
        }

        public override void Update()
        {
            position += new Vector2(Time.deltaTime * direction, 0);
            // if(((Vector2Int)position).x == 0)
            //     position = new Vector2(direction, 0);
            // System.Console.WriteLine($"{gameObject.GetInstanceID()}: {position.x}");
            gameObject.transform.position = (Vector2Int)position;
        }
    }
}
