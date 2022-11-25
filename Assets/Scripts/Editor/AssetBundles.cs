using UnityEditor;

public class AssetBundles : Editor
{
    [MenuItem("Assets/AssetsBundle/Build")]
    static void BuildAssetsBundle()
    {
        BuildPipeline.BuildAssetBundles(@"C:\Users\zvika\Desktop\Assets", BuildAssetBundleOptions.ChunkBasedCompression,
            BuildTarget.StandaloneWindows);
    }
}