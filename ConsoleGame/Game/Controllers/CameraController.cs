using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleGame.Engine;
using static ConsoleGame.Engine.InputManager;

namespace ConsoleGame.Game
{
    public class CameraController : Behaviour
    {
        private Vector2 position;

        public override void Update()
        {
            // position += new Vector2(Time.deltaTime, 0f);
            // gameObject.transform.position = (Vector2Int)position;
            if(Input.GetKey(KeyCode.Right))
            {
                gameObject.transform.position += new Vector2Int(1, 0);
            }
            else
            if(Input.GetKey(KeyCode.Left))
            {
                gameObject.transform.position -= new Vector2Int(1, 0);
            }
            else
            if(Input.GetKey(KeyCode.Up))
            {
                gameObject.transform.position += new Vector2Int(0, 1);
            }
            else
            if(Input.GetKey(KeyCode.Down))
            {
                gameObject.transform.position -= new Vector2Int(0, 1);
            }
        }
    }
}
