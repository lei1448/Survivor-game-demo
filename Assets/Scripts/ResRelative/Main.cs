using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using TMPro;
public class Main : MonoBehaviour
{
    //public RawImage img;
    // Start is called before the first frame update
    void Start()
    {
        LuaMgr.Instance.DoLuaFile("Main");
        //Addressables.LoadAssetAsync<Texture2D>("Assets/Dark UI/New Icons/White A1.png");
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
