using System;
using System.IO;
using UnityEngine;
using XLua;

public class LuaMgr : Singleton<LuaMgr>
{
    private LuaEnv luaEnv;
    private LuaEnv LuaEnv
    {
        get
        {
            luaEnv ??= new LuaEnv();
            return luaEnv;
        }
    }

    public LuaTable Global
    {
        get
        {
            return LuaEnv.Global;
        }
    }

    public LuaMgr()
    {
        LuaEnv.AddLoader(CustomLoderAA);
        //LuaEnv.AddLoader(CustomLoderAB);
    }

    private byte[] CustomLoder(ref string fileName)
    {
        string path = Application.dataPath + "/Lua/" + fileName + ".lua";

        if(File.Exists(path))
            return File.ReadAllBytes(path);
        else 
            Debug.Log("该路径lua脚本不存在:" + path);
        return null;
    }

    private byte[] CustomLoderAB(ref string fileName)
    {
        TextAsset lua = ABMgr.Instance.LoadRes<TextAsset>("lua", fileName + ".lua");
        if(lua != null)
            return lua.bytes;
        else
            Debug.Log("AB包中lua脚本不存在: " + fileName);

        return null;
    }

    private byte[] CustomLoderAA(ref string fileName)
    {
        TextAsset lua = AAMgr.Instance.LoadAsset<TextAsset>(fileName + ".lua");
        if(lua != null)
            return lua.bytes;
        else
            Debug.Log("lua脚本不存在: " + fileName);
        return null;
    }

    public void DoLuaFile(string fileName)
    {
        string str = string.Format("require('{0}')", fileName);
        LuaEnv.DoString(str);
    }

    public void Dispose()
    {
        if(luaEnv == null)
        {
            Debug.Log("解析器未初始化");
            return;
        }
        luaEnv.Dispose();
        luaEnv = null;
    }

    public void Tick()
    {
        if(luaEnv == null)
        {
            Debug.Log("解析器未初始化");
            return;
        }
        luaEnv.Tick();
    }
}
