using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.Engine.CoreModule
{
    public static class Renderer
    {    
        private static Pixel[] geometryBuffer;
        private static Pixel[] lightingBuffer;
        private static string[] screenBuffer;
        private static int leftOffset;
        private static string pixelFormat = "\x1b[48;2;{0};{1};{2}m\x1b[38;2;{3};{4};{5}m{6}";

        static Renderer()
        {
            var x = EngineSettings.RENDER_AREA_SIZE.x;
            var y = EngineSettings.RENDER_AREA_SIZE.y;
            if (x % 2 != 1 || y % 2 != 1)
                throw new ArgumentException("Render area size must be odd");
            var windowWidth = System.Console.WindowWidth;
            var windowHeight = System.Console.WindowHeight;
            Console.SetBufferSize(windowWidth, windowHeight);
            System.Console.SetWindowSize(x * 2 + 4, y + 6);
            System.Console.SetBufferSize(x * 2 + 4, y + 6);
            leftOffset = 3;
            screenBuffer = new string[(x * 2 + leftOffset + 1) * (y + 2)];     
            geometryBuffer = new Pixel[x * y];
            lightingBuffer = new Pixel[x * y];   
        }

        public static void RenderScene(RendererComponent[] objectsToRender, LightSource[] lightSources, Camera camera)
        {
            System.Console.CursorVisible = false; 
            System.Console.SetCursorPosition(0, 0);
            Array.Sort(objectsToRender);
            Array.Reverse(objectsToRender);
            Array.Clear(geometryBuffer, 0, geometryBuffer.Length);
            Array.Clear(lightingBuffer, 0, lightingBuffer.Length);
            if (camera == null) return;
            Geometry(objectsToRender, camera);
            Lighting(lightSources, camera);
            FinalRender();
        }

        private static void Geometry(RendererComponent[] objectsToRender, Camera camera)
        {
            var screenSizeX = EngineSettings.RENDER_AREA_SIZE.x;
            var screenSizeY = EngineSettings.RENDER_AREA_SIZE.y;
            var cameraPosition = camera.transform.position;
            var cameraRect = new Rect(cameraPosition.x - screenSizeX / 2, cameraPosition.y - screenSizeY / 2, screenSizeX - 1, screenSizeY - 1);
            var objectsToRenderLength = objectsToRender.Length;
            for (int i = 0; i < objectsToRenderLength; i++)
            {
                var objectToRender = objectsToRender[i];
                var objectBounds = objectToRender.bounds;
                if (Rect.DoesOverlapping(cameraRect, objectBounds))
                {
                    // var objectPosition = objectToRender.transform.position;
                    // var objectPivot = objectToRender.transform.pivot;
                    // var objectScaleY = objectToRender.transform.scale.y;
                    // var objectScaleX = objectToRender.transform.scale.x;
                    var objectBoundsWidth = objectBounds.x + objectBounds.width;
                    var objectBoundsHeight = objectBounds.y + objectBounds.height;
                    for (int y = objectBounds.y; y <= objectBoundsHeight; y++)
                    {
                        for (int x = objectBounds.x; x <= objectBoundsWidth; x++)
                        {
                            var pixelWorldPosition = new Vector2Int(x, y);
                            if (cameraRect.CheckIfPointInside(pixelWorldPosition))
                            {
                                var screenPosition = new Vector2Int(pixelWorldPosition.x - cameraPosition.x + screenSizeX / 2, (screenSizeY - 1) - (pixelWorldPosition.y - cameraPosition.y + screenSizeY / 2));
                                var pixelIndex = screenPosition.x + screenPosition.y * screenSizeX;
                                if (geometryBuffer[pixelIndex].symbol == default(char))
                                {
                                    geometryBuffer[pixelIndex] = objectToRender.objectPixel;                                                             
                                }
                            }
                        }
                    }
                }
            }
        }

        private static void Lighting(LightSource[] lightSources, Camera camera)
        {
            var screenSizeX = EngineSettings.RENDER_AREA_SIZE.x;
            var screenSizeY = EngineSettings.RENDER_AREA_SIZE.y;
            var cameraPosition = camera.transform.position;
            var cameraRect = new Rect(cameraPosition.x - screenSizeX / 2, cameraPosition.y - screenSizeY / 2, screenSizeX - 1, screenSizeY - 1);
            var lightSourcesLenght = lightSources.Length;
            for (int i = 0; i < lightSourcesLenght; i++)
            {
                var lightSource = lightSources[i];
                var boudnsLight = lightSource.boudns;
                if (Rect.DoesOverlapping(cameraRect, boudnsLight))
                {
                    var boundsLightHeight = boudnsLight.y + boudnsLight.height;
                    var boundsLightWidth = boudnsLight.x + boudnsLight.width;
                    for (int y = boudnsLight.y; y < boundsLightHeight; y++)
                    {
                        for (int x = boudnsLight.x; x < boundsLightWidth; x++)
                        {
                            var pixelWorldPosition = new Vector2Int(x, y);
                            if (cameraRect.CheckIfPointInside(pixelWorldPosition))
                            {
                                var screenPosition = new Vector2Int(pixelWorldPosition.x - cameraPosition.x + screenSizeX / 2, (screenSizeY - 1) - (pixelWorldPosition.y - cameraPosition.y + screenSizeY / 2));                                
                                var pixelIndex = screenPosition.x + screenPosition.y * screenSizeX;
                                if (geometryBuffer[pixelIndex].symbol != default(char))
                                {
                                    var distanceFromLightCenter = Vector2Int.Distance(pixelWorldPosition, lightSource.transform.position);
                                    var lightFalloff = Mathf.Lerp01(lightSource.lightRange + lightSource.lightFalloff + 1, lightSource.lightRange, distanceFromLightCenter);
                                    var lightIntensityAtPoint = (lightSource.lightColor * lightFalloff).LightIntensity;

                                    var additionalLightSymbol = geometryBuffer[pixelIndex].symbolColor * lightIntensityAtPoint;
                                    var additionalLightBackground = geometryBuffer[pixelIndex].backgroundColor * lightIntensityAtPoint;
                                    lightingBuffer[pixelIndex].symbolColor = lightingBuffer[pixelIndex].symbolColor + additionalLightSymbol;
                                    lightingBuffer[pixelIndex].backgroundColor = lightingBuffer[pixelIndex].backgroundColor + additionalLightBackground;
                                    lightingBuffer[pixelIndex].symbol = geometryBuffer[pixelIndex].symbol;
                                }
                            }                            
                        }
                    }
                }
            }

            var lightBufferLenght = lightingBuffer.Length;
            for (int i = 0; i < lightBufferLenght; i++)
            {
                if (lightingBuffer[i].symbolColor < LightingSettings.ambientColor && lightingBuffer[i].backgroundColor < LightingSettings.ambientColor)
                {
                    var lightIntensityAtPoint = LightingSettings.ambientColor.LightIntensity;
                    lightingBuffer[i].symbolColor = geometryBuffer[i].symbolColor * lightIntensityAtPoint;
                    lightingBuffer[i].backgroundColor = geometryBuffer[i].backgroundColor * lightIntensityAtPoint;
                    lightingBuffer[i].symbol = geometryBuffer[i].symbol;
                }
            }
        }

        
        private static void FinalRender()
        {
            var screenSizeX = EngineSettings.RENDER_AREA_SIZE.x;
            var screenSizeY = EngineSettings.RENDER_AREA_SIZE.y;
            var width = screenSizeX * 2 + leftOffset + 1;
            for (int x = 0; x < width; x++)
            {
                if (x < leftOffset - 1)
                {
                    screenBuffer[x] = string.Intern("\x1b[48;2;0;0;0m\x1b[38;2;255;255;255m ");
                }
                else
                if (x == leftOffset - 1)
                {
                    screenBuffer[x] = string.Intern("\x1b[48;2;0;0;0m\x1b[38;2;255;255;255m┌");
                }
                else
                if (x == width - 1)
                {
                    screenBuffer[x] = string.Intern("\x1b[48;2;0;0;0m\x1b[38;2;255;255;255m┐\n");
                }
                else
                {
                    screenBuffer[x] = string.Intern("\x1b[48;2;0;0;0m\x1b[38;2;255;255;255m─");
                }
            }
            for (int y = 1; y < screenSizeY + 1; y++)
            {
                for (int x = 0; x < leftOffset; x++)
                {
                    if (x == leftOffset - 1)
                    {
                        screenBuffer[x + y * width] = string.Intern("\x1b[48;2;0;0;0m\x1b[38;2;255;255;255m│");
                    }
                    else
                    {
                        screenBuffer[x + y * width] = string.Intern("\x1b[48;2;0;0;0m\x1b[38;2;255;255;255m ");

                    }
                }
                screenBuffer[(width - 1) + y * width] = string.Intern("\x1b[48;2;0;0;0m\x1b[38;2;255;255;255m│\n");
            }
            for (int x = 0; x < width; x++)
            {
                int elementIndex = x + (screenSizeY + 1) * width;
                if (x < leftOffset - 1)
                {
                    screenBuffer[elementIndex] = string.Intern("\x1b[48;2;0;0;0m\x1b[38;2;255;255;255m ");
                }
                else
                if (x == leftOffset - 1)
                {
                    screenBuffer[elementIndex] = string.Intern("\x1b[48;2;0;0;0m\x1b[38;2;255;255;255m└");
                }
                else
                if (x == width - 1)
                {
                    screenBuffer[elementIndex] = string.Intern("\x1b[48;2;0;0;0m\x1b[38;2;255;255;255m┘\n");
                }
                else
                {
                    screenBuffer[elementIndex] = string.Intern("\x1b[48;2;0;0;0m\x1b[38;2;255;255;255m─");
                }
            }
            
            for (int i = 0; i < lightingBuffer.Length; i++)
            {
                var pixelPositionY = i / screenSizeX;
                var pixelPositionX = i - (pixelPositionY * screenSizeX);

                var screenPositionX = leftOffset + (pixelPositionX * 2);   
                var screenPositionY = pixelPositionY + 1;             

                screenBuffer[(screenPositionX + 1) + screenPositionY * width] = string.Intern(" ");
                var bgcolor = lightingBuffer[i].backgroundColor;
                var color = lightingBuffer[i].symbolColor;
                
                screenBuffer[screenPositionX + screenPositionY * width] = string.Intern(string.Format(pixelFormat, bgcolor.r, bgcolor.g, bgcolor.b, color.r, color.g, color.b, lightingBuffer[i].symbol));
            }
            var finalBuffer = string.Concat(screenBuffer);     
            System.Console.Write(finalBuffer);     
        }
    }
}
