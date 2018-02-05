using System;
using Urho;
using Urho.Gui;
using Urho.Physics;

namespace SamplyGameExercise.Samply
{
    partial class SamplyGame : Application
    {
        private PhysicsWorld PhysicsWorld;
        private Viewport Viewport;
        private Node CameraNode;

        private void CreateScene()
        {
            Scene = new Scene();
            Scene.CreateComponent<Octree>();

            CreatePhysics();
            CreateCamera();
            CreateViewport();

            CreateUI();

            CreateBackground();

            CreateLight();
        }

        private void CreateLight()
        {
            // Lights:
            var lightNode = Scene.CreateChild();
            lightNode.Position = new Vector3(0, -5, -40);
            lightNode.AddComponent(new Light { Range = 120, Brightness = 0.8f });

        }

        private void CreateBackground()
        {

            var background = new Background();
            Scene.AddComponent(background);
            background.Start();
        }

        private void CreateUI()
        {

            var coinsText = new Text()
            {
                HorizontalAlignment = HorizontalAlignment.Right,
                Value = "Coins"
            };
            coinsText.SetFont(ResourceCache.GetFont(Assets.Fonts.Font), Graphics.Width / 20);
            UI.Root.AddChild(coinsText);
            Input.SetMouseVisible(enable: true, suppressEvent: false);
        }

        private void CreateCamera()
        {
            CameraNode = Scene.CreateChild();
            CameraNode.Position = new Vector3(0, 0, -10.0f);
            CameraNode.CreateComponent<Camera>();
        }

        private void CreateViewport()
        {
            Viewport = new Viewport(Context, Scene, CameraNode.GetComponent<Camera>(), renderPath: null);

            if (Platform != Platforms.Android && Platform != Platforms.iOS)
            {
                var effectRenderPath = Viewport.RenderPath.Clone();
                var fxaaRp = ResourceCache.GetXmlFile(Assets.PostProcess.FXAA3);
                effectRenderPath.Append(fxaaRp);
                Viewport.RenderPath = effectRenderPath;
            }

            Renderer.SetViewport(0, Viewport);
        }

        private void CreatePhysics()
        {
            PhysicsWorld = Scene.CreateComponent<PhysicsWorld>();
            PhysicsWorld.SetGravity(new Vector3(0, 0, 0));
        }
    }
}