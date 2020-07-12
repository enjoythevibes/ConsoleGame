using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleGame.Engine.CoreModule
{
    public static class GameLoop
    {   
        public static float deltaTime;
        private static bool enabled;
        private static long currentTime { get => DateTime.Now.Ticks * 100; }

        private static Behaviour[] toUpdate;
        private static RendererComponent[] toRender;
        private static LightSource[] lightSources;
        private static Camera cameraToRender;

        static GameLoop()
        {
            enabled = true;
            toUpdate = new Behaviour[0];
            toRender = new RendererComponent[0];
            lightSources = new LightSource[0];
        }

        private static void Main(string[] args)
        {
            SystemFunctions.DisableQuickEdit();
            new Thread(Run).Start();
            Game.GameManager.Initialize();
        }

        private static void Run()
        {
            var lastLoopTime = currentTime;
            var optimalUpdateTime = EngineSettings.OPTIMAL_UPDATE_TIME;
            while (enabled)
            {
                var timeNow = currentTime;
                var updateLenght = timeNow - lastLoopTime;
                lastLoopTime = timeNow; 
                deltaTime = updateLenght / 1000000000f;

                EndOfFrame();
                Input();
                Update();
                Render();

                var timeWait = (int)((lastLoopTime - currentTime + optimalUpdateTime) / 1000000);
                if (timeWait > 0)
                    Thread.Sleep(timeWait);
            }
        }       

        private static void EndOfFrame()
        {
            InputCore.Reset();
            // while(Console.KeyAvailable)
            // {
            //     Console.ReadKey(true);
            // }
        }

        public static void AddBehaviourToUpdate(Behaviour behaviour, int executeOrder)
        {
            var _toUpdate = new List<Behaviour>(toUpdate);
            if (_toUpdate.Contains(behaviour)) return;
            if (executeOrder > -1 && executeOrder < toUpdate.Length)
                _toUpdate.Insert(executeOrder, behaviour);
            else
                _toUpdate.Add(behaviour);
            toUpdate = _toUpdate.ToArray();
        }

        public static void AddObjectToRender(RendererComponent rendererComponent)
        {
            var _toRender = new List<RendererComponent>(toRender);
            if (_toRender.Contains(rendererComponent)) return;
            _toRender.Add(rendererComponent);
            toRender = _toRender.ToArray();
        }

        public static void AddCameraToRender(Camera camera)
        {
            cameraToRender = camera;
        }

        public static void AddLightSource(LightSource lightSource)
        {
            var _lightSources = new List<LightSource>(lightSources);
            if (_lightSources.Contains(lightSource)) return;
            _lightSources.Add(lightSource);
            lightSources = _lightSources.ToArray();
        }

        private static void Render()
        {
            Renderer.RenderScene(toRender, lightSources, cameraToRender);
            #if DEBUG_MODE
            // Debug.DebugConsole.RenderDebugConsole();
            #endif

        }

        private static void Update()
        {
            for (int i = 0; i < toUpdate.Length; i++)
            {
                toUpdate[i].Update();
            }
        }

        private static void Input()
        {
            if (Console.KeyAvailable)
            {
                var input = Console.ReadKey(true).Key;
                if (InputManager.buttons.ContainsKey(input))
                {
                    var button = InputManager.buttons[input];
                    if (!InputCore.keysPressedCache.Contains(button))
                    {
                        InputCore.Press(button);                    
                    }
                }
            }
            else
            {
                InputCore.ClearCache();
            }
            while (Console.KeyAvailable)
            {
                Console.ReadKey(true);
            }
        }
    }
}
