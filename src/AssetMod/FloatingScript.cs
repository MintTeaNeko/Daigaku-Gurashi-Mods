using System;
using System.Collections;
using System.IO;
using System.Reflection;
using ModLoader;
using UnityEngine;

namespace AssetMod
{
    public class FloatingScript : MonoBehaviour
    {
        public ModSettings settings;
        public GameObject head;
        private Vector3 temp = new Vector3();
        
        private void Update()
        {
            temp = new Vector3(
                head.transform.position.x + settings.offSetX,
                head.transform.position.y + settings.offSetY,
                head.transform.position.z + settings.offSetZ
            );
            
            temp.y += Mathf.Sin(Time.fixedTime * Mathf.PI * 1f) * 0.15f;
            transform.position = temp;
        }
    }
}