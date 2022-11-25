using System;
using UnityEngine;

public  class AssetsBundleService : MonoBehaviour
{
    private static AssetBundle _assetBundle;
    

    public static void LoadAssetsBundle(string bundlePath)
    {
        try
        {
            _assetBundle = AssetBundle.LoadFromFile(bundlePath);
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            throw;
        }
    }

    public static void InstantiateBundle(string assetName)
    {
        var prefab = _assetBundle.LoadAsset(assetName);
        Instantiate(prefab);
    }
    
    public static void UnloadBundle()
    {
        _assetBundle.Unload(true);
    }
}
