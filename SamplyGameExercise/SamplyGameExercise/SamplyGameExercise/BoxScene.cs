using Urho;
using Urho.Actions;
using Urho.Gui;
using Urho.Resources;

namespace SamplyGameExercise
{
    public class BoxScene
    {
        public BoxScene(SceneParameters sceneData)
        {
            this.SceneData = sceneData;
        }

        public SceneParameters SceneData { get; set; }

        public async void Show()
        {
            // UI text
            AddHelloText(SceneData.Context, SceneData.Ui, SceneData.ResourceCache);

            // 3D scene with Octree

            SceneData.Scene.CreateComponent<Octree>();

            // Box
            var boxNode = AddBox(SceneData.ResourceCache, SceneData.Scene);

            // Light
            AddLight(SceneData.Scene);

            // Camera
            AddCamera(SceneData.Context, SceneData.Renderer, SceneData.Scene);

            // Do actions
            await boxNode.RunActionsAsync(new EaseBounceOut(new ScaleTo(duration: 1f, scale: 1)));
            await boxNode.RunActionsAsync(new RepeatForever(
                new RotateBy(duration: 1, deltaAngleX: 90, deltaAngleY: 0, deltaAngleZ: 0)));
        }

        private void AddCamera(Context context, Renderer renderer, Scene scene)
        {
            Node cameraNode = scene.CreateChild(name: "camera");
            Camera camera = cameraNode.CreateComponent<Camera>();
            renderer.SetViewport(0, new Viewport(context, scene, camera, null));
        }

        private void AddLight(Scene scene)
        {
            Node lightNode = scene.CreateChild(name: "light");
            var light = lightNode.CreateComponent<Light>();
            light.Range = 10;
            light.Brightness = 1.5f;
        }

        private Node AddBox(ResourceCache resourceCache, Scene scene)
        {
            Node boxNode = scene.CreateChild(name: "Box node");
            boxNode.Position = new Vector3(x: 0, y: 0, z: 5);
            boxNode.SetScale(0f);
            boxNode.Rotation = new Quaternion(x: 60, y: 0, z: 30);

            StaticModel boxModel = boxNode.CreateComponent<StaticModel>();
            boxModel.Model = resourceCache.GetModel("Models/Box.mdl");
            boxModel.SetMaterial(resourceCache.GetMaterial("Materials/BoxMaterial.xml"));
            return boxNode;
        }

        private void AddHelloText(Context context, UI ui, ResourceCache resourceCache)
        {
            var helloText = new Text(context);
            helloText.Value = "Hello World from UrhoSharp";
            helloText.HorizontalAlignment = HorizontalAlignment.Center;
            helloText.VerticalAlignment = VerticalAlignment.Top;
            helloText.SetColor(new Color(r: 0f, g: 1f, b: 1f));
            helloText.SetFont(font: resourceCache.GetFont("Fonts/Font.ttf"), size: 30);
            ui.Root.AddChild(helloText);
        }
    }
}