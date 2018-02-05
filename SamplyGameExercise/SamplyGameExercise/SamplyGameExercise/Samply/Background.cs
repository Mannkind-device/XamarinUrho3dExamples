using System;
using System.Threading.Tasks;
using Urho;
using Urho.Actions;

namespace SamplyGameExercise.Samply
{
    public class Background : Component
    {
        Node FrontTile { get; set; }
        Node RearTile { get; set; }

        private const float BackgroundRotationX = 45f;
        private const float BackgroundRotationY = 15f;
        private const float BackgroundScale = 40f;
        private const float BackgroundSpeed = 0.05f;
        private const float FlightHeight = 10f;

        public void Start()
        {
            FrontTile = CreateTile(0);
            RearTile = CreateTile(1);

            AnimateBackground();
        }

        private Node CreateTile(int index)
        {
            var cache = Application.ResourceCache;
            var tile = Node.CreateChild();
            var planeNode = tile.CreateChild();
            planeNode.Scale = new Vector3(BackgroundScale, 0.0001f, BackgroundScale);
            var planeObject = planeNode.CreateComponent<StaticModel>();
            planeObject.Model = cache.GetModel(Assets.Models.Plane);
            planeObject.SetMaterial(cache.GetMaterial(Assets.Materials.Grass));

            // area for trees:
            var sizeZ = BackgroundScale / 2.1f;
            var sizeX = BackgroundScale / 3.8f;

            Node treeNode = tile.CreateChild();
            treeNode.Rotate(new Quaternion(0, RandomHelper.NextRandom(0, 5) * 90, 0), TransformSpace.Local);
            treeNode.Scale = new Vector3(0.3f, 0.4f, 0.3f);
            var treeGroup = treeNode.CreateComponent<StaticModel>();
            treeGroup.Model = cache.GetModel(Assets.Models.Tree);
            treeGroup.SetMaterial(cache.GetMaterial(Assets.Materials.Pyramid));

            for (float i = -sizeX; i < sizeX; i += 3.2f)
            for (float j = -sizeZ; j < sizeZ; j += 3.2f)
            {
                var clonedTreeNode = treeNode.Clone(CreateMode.Local);
                clonedTreeNode.Position = new Vector3(i + RandomHelper.NextRandom(-0.3f, 0.3f), 0, j);
            }

            treeNode.Remove();

            tile.Rotate(new Quaternion(270 + BackgroundRotationX, 0, 0), TransformSpace.Local);
            tile.RotateAround(new Vector3(0, 0, 0), new Quaternion(0, BackgroundRotationY, 0), TransformSpace.Local);
            var tilePosX = BackgroundScale * (float)Math.Sin(MathHelper.DegreesToRadians(90 - BackgroundRotationX));
            var tilePosY = BackgroundScale * (float)Math.Sin(MathHelper.DegreesToRadians(BackgroundRotationX));
            tile.Position = new Vector3(0, (tilePosX + 0.01f) * index, tilePosY * index + FlightHeight);
            return tile;
        }

        async void AnimateBackground()
        {
            while (true)
            {
                // calculate positions using Law of sines
                var x = BackgroundScale * (float)Math.Sin(MathHelper.DegreesToRadians(90 - BackgroundRotationX));
                var y = BackgroundScale * (float)Math.Sin(MathHelper.DegreesToRadians(BackgroundRotationX)) + FlightHeight;

                var moveTo = x + 1f; //a small adjusment to hide that gap between two tiles
                var h = (float)Math.Tan(MathHelper.DegreesToRadians(BackgroundRotationX)) * moveTo;
                await Task.WhenAll(FrontTile.RunActionsAsync(new MoveBy(1 / BackgroundSpeed, new Vector3(0, -moveTo, -h))),
                    RearTile.RunActionsAsync(new MoveBy(1 / BackgroundSpeed, new Vector3(0, -moveTo, -h))));

                //switch tiles
                var tmp = FrontTile;
                FrontTile = RearTile;
                RearTile = tmp;

                RearTile.Position = new Vector3(0, x, y);
            }
        }


    }
}