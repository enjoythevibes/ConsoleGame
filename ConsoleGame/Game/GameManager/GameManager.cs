using System;
using System.Collections.Generic;
using ConsoleGame.Engine;
using ConsoleGame.Game.Entities;

namespace ConsoleGame.Game
{
    public static class GameManager
    {
        // Entry point
        public static void Initialize()
        {
            var camera = new GameObject().AddComponent<Camera>().AddComponent<CameraController>();
            camera.transform.position = new Vector2Int(0, 0);

            new GameObject(new Vector2Int(-25, -10), new Vector2Int(0, 1)).AddComponent<RendererComponent>().GetComponent<RendererComponent>().SetRenderData('$', new Color32(255, 255, 255), new Color32(100, 100, 100));
            var box = new GameObject(new Vector2Int(-10, 10), new Vector2Int(2, 2)).AddComponent<SimpleBox>().AddComponent<RendererComponent>();
            box.GetComponent<RendererComponent>().SetRenderData('*', new Color32(255, 255, 255), new Color32(255, 0, 0));
            box.GetComponent<SimpleBox>().direction = 1;

            var box2 = new GameObject(new Vector2Int(5, 0), new Vector2Int(1, 1)).AddComponent<SimpleBox>().AddComponent<RendererComponent>();
            box2.GetComponent<RendererComponent>().SetRenderData('▀', new Color32(255, 0, 255), new Color32(50, 50, 50), 1);

            // new GameObject(new Vector2Int(-8, 1), new Vector2Int(5, 2)).AddComponent<RendererComponent>().GetComponent<RendererComponent>().SetRenderData('^', new Color32(0, 255, 1), new Color32());
            // new GameObject(new Vector2Int(-1, 1), new Vector2Int(5, 2)).AddComponent<RendererComponent>().GetComponent<RendererComponent>().SetRenderData('^', new Color32(0, 255, 1), new Color32());

            var light = new GameObject(new Vector2Int(-2, 0)).AddComponent<RendererComponent>().AddComponent<LightSource>();
            light.GetComponent<RendererComponent>().SetRenderData('%', new Color32(255, 255, 255), new Color32(50, 50, 50));
            light.GetComponent<LightSource>().SetLightData(new Color32(255, 255, 255), 1);
            light.AddComponent<LightChanger>();
            light.AddComponent<TestObject>();

            var floor = new GameObject(new Vector2Int(0, 0), new Vector2Int(50, 20)).AddComponent<RendererComponent>();
            floor.transform.pivot = new Vector2Int(-25, -10);
            floor.GetComponent<RendererComponent>().SetRenderData('*', new Color32(255, 255, 255), new Color32(50, 50, 50), 0);

            // new GameObject(new Vector2Int(0, 5)).AddComponent<RendererComponent>().GetComponent<RendererComponent>().SetRenderData('^', new Color32(255, 255, 255), new Color32());

            AddLight(new Vector2Int(9, 5));
            AddLight(new Vector2Int(20, 0));
            AddLight(new Vector2Int(2, 0));
            AddLight(new Vector2Int(-2, 0));
            AddLight(new Vector2Int(-10, 10));
            AddLight(new Vector2Int(20, 10));

            void AddLight(Vector2Int pos)
            {
                var _light = new GameObject(pos).AddComponent<LightSource>();
                _light.GetComponent<LightSource>().SetLightData(new Color32(255, 255, 255), 1);
                _light.AddComponent<LightChanger>();
            }

            new GameObject(new Vector2Int(-25, 10)).AddComponent<RendererComponent>().AddComponent<TestObject>().GetComponent<RendererComponent>().SetRenderData('@', new Color32(255, 255, 255), new Color32(), 5);
        }
    }
}