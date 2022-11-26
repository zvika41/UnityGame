using UnityEditor;

public class AssetBundles : Editor
{
    [MenuItem("Assets/AssetsBundle/Build")]
    static void BuildAssetsBundle()
    {
        BuildPipeline.BuildAssetBundles(@"https://drive.google.com/drive/folders/1orEly7QQWMImFdmkj5-ID-WBcEGe3LG7?usp=share_link", BuildAssetBundleOptions.ChunkBasedCompression,
            EditorUserBuildSettings.activeBuildTarget);
    }
}