using System;
using UnityEngine;

namespace ModLoader
{
    public class Catalog : MonoBehaviour
    {
        public static void LoadObjectAsset(string path, string address, Action<GameObject> callbackMethod)
        {
            try
            {
                var assetBundle = AssetBundle.LoadFromFile(path);
                var prefab = assetBundle.LoadAsset<GameObject>(address);
                callbackMethod(Instantiate(prefab));
                assetBundle.Unload(false);
            }
            catch (Exception e)
            {
                ModManager.LogToFile($"error loading asset {address}: {e}");
            }
        }
    }
}