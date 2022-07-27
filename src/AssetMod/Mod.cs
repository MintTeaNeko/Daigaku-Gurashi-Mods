using System;
using System.IO;
using System.Reflection;
using ModLoader;
using UnityEngine;

namespace AssetMod
{
    public class Mod : MonoBehaviour
    {
        private string modPath;
        private ModInformation information;
        private ModSettings settings;
        private EventManager manager;
        private Character character;
        
        private void Start()
        {
            manager = FindObjectOfType<EventManager>(true);
            manager.NotifyPlayer("assetmod has loaded", 0);
            modPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            information = JsonUtility.FromJson<ModInformation>(File.ReadAllText(Path.Combine(modPath, "mod.json")));
            settings = JsonUtility.FromJson<ModSettings>(File.ReadAllText(Path.Combine(modPath, "mod_settings.json")));
            character = manager.Student[0];
            
            Catalog.LoadObjectAsset(Path.Combine(modPath, "crownbundle"), information.objAddresses[0], obj =>
            {
                obj.transform.position = character.MyHair.gameObject.transform.position + new Vector3(settings.offSetX, settings.offSetY, settings.offSetZ);
                obj.transform.parent = character.MyHair.gameObject.transform;
                obj.transform.localScale = new Vector3(1, 1, 1);
                Material mat = new Material(Shader.Find("Standard"));
                mat.color = new Color(settings.ColorR/255, settings.ColorG/255, settings.ColorB/255);
                
                obj.GetComponent<MeshRenderer>().material = mat;
            });
        }
    }

    public class ModSettings
    {
        public float offSetX;
        public float offSetY;
        public float offSetZ;
        public float ColorR;
        public float ColorG;
        public float ColorB;
    }
}