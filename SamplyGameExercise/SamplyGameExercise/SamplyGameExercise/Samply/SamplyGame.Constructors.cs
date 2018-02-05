using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Urho;

namespace SamplyGameExercise.Samply
{
    public partial class SamplyGame : Application
    {
        [Preserve]
        public SamplyGame(ApplicationOptions options) : base(options)
        {
        }

        public SamplyGame(IntPtr handle) : base(handle)
        {
        }

        protected SamplyGame(UrhoObjectFlag emptyFlag) : base(emptyFlag)
        {
        }

        [Preserve]
        public SamplyGame() : base(new ApplicationOptions(assetsFolder: "Data") {Height = 1024, Width=576,  Orientation=ApplicationOptions.OrientationType.Portrait })
        {
        }

        static SamplyGame()
        {
            UnhandledException += (s, e) =>
            {
                if (Debugger.IsAttached)
                    Debugger.Break();
                e.Handled = true;
            };
        }

        private Scene Scene { get; set; }

        protected override void Start()
        {
            base.Start();
            CreateScene();

            Input.KeyDown += async e =>
            {
                if (e.Key == Key.Esc) await Exit();
                if (e.Key == Key.C) AddCollisionDebugBox(Scene, true);
                if (e.Key == Key.V) AddCollisionDebugBox(Scene, false);
            };
        }


        
    }
}
