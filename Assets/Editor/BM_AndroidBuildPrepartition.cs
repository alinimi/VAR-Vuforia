#if UNITY_EDITOR
using UnityEditor.Build;
using UnityEditor;
using System.IO;

public class BM_AndroidBuildPrepartion : IPreprocessBuild
{
    public int callbackOrder { get { return 0; } }
    public void OnPreprocessBuild(BuildTarget target, string path)
    {
        // Do the preprocessing here
        string[] fileEntries = Directory.GetFiles("Assets/Resources/", "*.prefab");
        System.IO.Directory.CreateDirectory("Assets/StreamingAssets/");
        using (StreamWriter sw = new StreamWriter("Assets/StreamingAssets/Prefabs.txt", false))
        {

            foreach (string filename in fileEntries)
            {
                sw.WriteLine(Path.GetFileNameWithoutExtension(filename));
            }

        }
    }
}
#endif
