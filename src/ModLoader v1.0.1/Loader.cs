using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using System.IO;

namespace ModLoader
{
    public class Loader : MonoBehaviour
    {
        private void Start()
        {
            ModManager.LogToFile("Loader is ready!");
            string[] directories = Directory.GetDirectories(ModManager.modsFolder);
            for (int i = 0; i < directories.Length; i++)
            {
                string directory = directories[i];
                string[] files = Directory.GetFiles(directory);
                for (int n = 0; n < files.Length; n++)
                {
                    string file = files[n];
                    if (file.Contains("mod.json"))
                    {
                        try
                        {
                            ModInformation modInformation = JsonUtility.FromJson<ModInformation>(File.ReadAllText(file));
                            string[] splitType = modInformation.type.Split(',');
                            string assemblyName = splitType[1].Replace(" ", "") + ".dll";
                            Type mono = Assembly.LoadFile(Path.Combine(directory, assemblyName)).GetType(splitType[0]);
                            GameObject go = new GameObject();
                            go.AddComponent(mono);
                        }
                        catch (Exception e)
                        {
                            ModManager.LogToFile($"failed to load {file}: {e}");
                        }
                    }
                }
            }
        }
    }

    public class ModInformation
    {
        public string type;
        public string modName;
        public string modGameVersion;
        public List<string> objAddresses;
    }
}