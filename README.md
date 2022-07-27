# Daigaku Gurashi Mods
a repo for mods I've made for Daigaku Gurashi `v0.9.0`

# How to install this (WARNING: read all the sentence)
you need to have Daigaku Gurashi version `v0.9.0`
- drag the repo's `Assembly-CSharp.dll` into `Daigaku Gurashi_Data > Managed`, and replace it (might want to back up the old version).
- put the `ModLoader.dll`, and the `mods` folder in the game folder itself.


# Mod Creation (Basic)

If you want to make scripted mods for this game then you need `Visual Studio` or `JetBrains Rider`. 
You also need to get `.Net Framework v4.8` Class Library, and a copy of the game (v0.9.0 ofc).
In addition, a C# decompiler like `dotpeek`, and or `dnspy` (for looking, and understanding in game functions). 

First step: would be creating a new class library project that utilizes `.Net Framework v4.8`.

Second step: in `visual studio`, and `JetBrains Rider` you'll need to refrence a few dlls you want to work with such as:
```
Assembly-CSharp.dll (to access in game classes)
ModLoader.dll
UnityEngine.dll
UnityEngine.CoreModule.dll
...
```
Now the fun stuff begin (I'd highly recommend working with dotpeek, or dnspy open).
In the C# decompiler look for `EventManager` class (it's the one we gonna use in the example).
it has `NotifyPlayer(string MessageText, int wwww)` 
 - `MessageText =>` the message it gonna show in the notification.
 - `wwww =>` is an int of a sound it's gonna play (just use 0).

```cs
using System;
using UnityEngine;

namespace myMod
{
  public class firstmod : MonoBehavior
  {
      private EventManager manager;
      
      private void Start()
      {
        // this gets an in game class EventManager
        manager = FindObjectOfType<EventManager>(true);
        
        // notifies the player at the bottom left
        manager.NotifyPlayer("Yay the mod has loaded!", 0);
        
        // ...
      }
      
      // ...
  }
}
```

build this bad boy then find where `rider`, or `visual studio` built the dll
then make a nice neat folder in mods called `myMod` or anything you'd like.

put the dll in the folder, and then make or copy a `mod.json` in it you need to put.

```json
{
  "type": "namespace.class, dllname",
  "modName": "myModName",
  "modGameVersion": "0.9.0",
  "objAddresses": []
}
```

the file structure for mods is like so.

```
mods 
  - modName
    - mod.json
    - modDlluwu.dll
  - ...
```

in the json you'd notice `"type": "namespace.class, dllname"` change it to your namespace, and your monobehavior class.
In my example it was `namespace myMod` with the `class firstmod`, and the dll it will build is `MyMod.dll` 
so in my case it would be `"type": "myMod.firstmod, MyMod"`.

Goodjob you made you first working mod (the notification shows at the bottom left).

for extra stuff `ModManager` has a `LogToFile(string text)` method which logs to a file in `%appdata%` with the name "ModLoaderLog.txt".
I wish you good luck for now, and Happy modding! <3

# Mod Creation (Advanced)

In this section you need to know how to make, and build `assetbundles` in unity, and basic things in C#.

The `ModLoader.dll` has a class for the mod's infromation called `ModInformation`, and another class called `Catalog`.

`Catalog` => It has a method `LoadObjectAsset(string path, string address, Action<GameObject> callbackMethod)` which lets you load assetbundle `GameObjects`(will explain better in a bit).

`ModInformation` => this is the `mod.json` it holds infromation about your mod like name, game version, etc.

```cs
using System;
using UnityEngine;

namespace myMod
{
  public class firstmod : MonoBehavior
  {
      private EventManager manager;
      private ModInformation infromation;
      private string modPath;
      
      private void Start()
      {
        manager = FindObjectOfType<EventManager>(true);
        
        // this gets the mod's directory aka "./../../mods/myMod"
        modPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        
        // reads the mod.json, and converts it to an object.
        information = JsonUtility.FromJson<ModInformation>(File.ReadAllText(Path.Combine(modPath, "mod.json")));
        
        manager.NotifyPlayer($"{infromation.modName} for {information.modGameVersion} has loaded", 0);
        
        /*
         first parameter takes path to assetbundle
         second parameter takes the GameObject's name you can use the mod.json's objAddresses for easier usage => mod.objAddresses[0..n]
        */
        Catalog.LoadObjectAsset(Path.Combine(modPath, "assetbundle"), "gameObjectName", obj => {
        
           // assign a material else the GameObject is invisible
           Material mat = new Material(Shader.Find("Standard"));
           mat.color = new Color(1, 1, 1);
           obj.GetComponent<MeshRenderer>().material = mat;
           
           obj.transform.position = manager.Student[0].transform.position;
           
           // ...
        });
      }
      
      // ...
  }
}
```
