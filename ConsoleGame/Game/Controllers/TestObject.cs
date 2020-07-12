using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleGame.Engine;

namespace ConsoleGame.Game
{
    public class TestObject : Behaviour
    {
        private Vector2 position;
        private int direction;
        private int updirection;

        public override void Start()
        { 
            position = gameObject.transform.position;
            direction = 1;
            updirection = 1;
        }        

        public override void Update()
        {
            position += new Vector2(Time.deltaTime * 10f * direction, 0);
            gameObject.transform.position = (Vector2Int)position;
            Console.WriteLine(gameObject.transform.position.x);
            if(position.x >= 26 || position.x <= -25)
            {
                position += new Vector2(0, -1f * updirection);
                direction *= -1;       
            }
        }
    }
}
