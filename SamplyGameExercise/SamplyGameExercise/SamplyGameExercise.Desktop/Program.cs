using SamplyGameExercise.Samply;
using Urho;

namespace SamplyGameExercise.Desktop
{
    class Program
    {
        static void Main(string[] args)
        {
            //new BoxGame(new ApplicationOptions("Data")).Run();
            new SamplyGame().Run();
        }
    }
}
