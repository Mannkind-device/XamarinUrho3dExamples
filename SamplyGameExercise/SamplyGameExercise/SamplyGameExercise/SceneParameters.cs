using Urho;
using Urho.Gui;
using Urho.Resources;

namespace SamplyGameExercise
{
    public class SceneParameters
    {
        public Platforms Platform { get; set; }

        public SceneParameters(Scene scene, Context context, UI ui, ResourceCache resourceCache, Renderer renderer)
        {
            Scene = scene;
            Context = context;
            Ui = ui;
            ResourceCache = resourceCache;
            Renderer = renderer;
        }

        public Scene Scene { get; private set; }
        public Context Context { get; private set; }
        public UI Ui { get; private set; }
        public ResourceCache ResourceCache { get; private set; }
        public Renderer Renderer { get; private set; }
    }
}