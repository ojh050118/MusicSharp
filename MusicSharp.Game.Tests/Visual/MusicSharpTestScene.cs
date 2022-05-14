using osu.Framework.Testing;

namespace MusicSharp.Game.Tests.Visual
{
    public class MusicSharpTestScene : TestScene
    {
        protected override ITestSceneTestRunner CreateRunner() => new MusicSharpTestSceneTestRunner();

        private class MusicSharpTestSceneTestRunner : MusicSharpGameBase, ITestSceneTestRunner
        {
            private TestSceneTestRunner.TestRunner runner;

            protected override void LoadAsyncComplete()
            {
                base.LoadAsyncComplete();
                Add(runner = new TestSceneTestRunner.TestRunner());
            }

            public void RunTestBlocking(TestScene test) => runner.RunTestBlocking(test);
        }
    }
}
