using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Urho;
using Urho.Physics;
using Urho.Shapes;

namespace SamplyGameExercise.Samply
{
    public partial class SamplyGame : Application
    {
        private static void AddCollisionDebugBox(Node rootNode, bool add)
        {
            var nodes = rootNode.GetChildrenWithComponent<CollisionShape>(true);
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
