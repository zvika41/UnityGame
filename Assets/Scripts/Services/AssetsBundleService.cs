using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Object = UnityEngine;

public  class AssetsBundleService : MonoBehaviour
{
    private static AssetBundle _assetBundle;
    private static GameObject _prefab;
    private string _assetName;
    


    public void StartDownloadAsset(string assetName)
    {
        StartCoroutine(DownloadAsset(assetName));
    }

    public static void LoadAssetsBundleFromFile(string bundlePath)
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
    
    public static void LoadAssetsBundleFromServer(string assetName)
    {
        try
        {
            _prefab = (GameObject) _assetBundle.LoadAsset(assetName);
            Instantiate(_prefab);
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            throw;
        }
    }

    public static void InstantiateBundle(string assetName)
    {
        _prefab = (GameObject) _assetBundle.LoadAsset(assetName);
        Instantiate(_prefab);
    }
    
    public static void UnloadBundle()
    {
        _assetBundle.Unload(true);
    }
    
    private IEnumerator DownloadAsset(string assetName)
    {
        using UnityWebRequest uwr = UnityWebRequestAssetBundle.GetAssetBundle("https://drive.google.com/uc?export=download&id=1K-GrHBh4jRBIsK46SLO66igbNZ-mtqcc");
        yield return uwr.SendWebRequest();
 
        if (uwr.result == UnityWebRequest.Result.ConnectionError || uwr.result == UnityWebRequest.Result.ProtocolError || uwr.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.Log(uwr.error);
        }
        else
        {
            // Get downloaded asset bundle
            _assetBundle = DownloadHandlerAssetBundle.GetContent(uwr);
            _prefab = (GameObject) _assetBundle.LoadAsset(assetName);
            Instantiate(_prefab);
        }
    }
}
