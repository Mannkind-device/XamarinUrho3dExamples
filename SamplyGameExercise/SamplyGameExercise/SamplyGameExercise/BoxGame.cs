using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Urho;
using Urho.Gui;
using Urho.Actions;
using Urho.Physics;
using Urho.Shapes;

namespace SamplyGameExercise
{
    public class BoxGame : Application
    {
        [Preserve]
        public BoxGame(ApplicationOptions opts) : base(opts) { }

        static BoxGame()
        {
            UnhandledException += (s, e) =>
            {
                if (Debugger.IsAttached)
                    Debugger.Break();
                e.Handled = true;
            };
        }

        private Scene Scene { get; set; }

        protected override async void Start()
        {

            Scene = new Scene(Context);
            var sceneParameters = new SceneParameters(Scene, Context, UI, ResourceCache, Renderer);
            sceneParameters.Platform = Platform;

            var boxScene = new BoxScene(sceneParameters);

            boxScene.Show();

            // Subscribe to Esc key:
            Input.KeyDown += async args =>
            {
                switch (args.Key)
                {
                    case Key.Esc:
                         await Exit();
                        break;
                    case Key.C:
                        AddCollisionDebugBox(Scene, true);
                        break;
                    case Key.V:
                        AddCollisionDebugBox(Scene, false);
                        break;
                }
            };
        }

        private void AddCollisionDebugBox(Scene scene, bool add)
        {
            var nodes = scene.GetChildrenWithComponent<CollisionShape>(true);
            foreach (var node in nodes)
            {
                node.GetChild("CollisionDebugBox", false)?.Remove();
                if (!add)
                    continue;


                var subNode = node.CreateChild("CollisionDebugBox");
                var box = subNode.CreateComponent<Box>();
                subNode.Scale = node.GetComponent<CollisionShape>().WorldBoundingBox.Size;
                box.Color = new Color(Color.Red, 0.4f);

            }

        }
    }
}
