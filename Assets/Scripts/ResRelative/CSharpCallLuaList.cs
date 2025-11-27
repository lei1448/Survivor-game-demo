using System;
using System.Collections.Generic;
using XLua;
using UnityEngine;
using UnityEngine.Events; 
public static class CSharpCallLuaList
{
    [CSharpCallLua]
    public static List<Type> csharpCallLuaList = new()
    {
        typeof(UnityAction<bool>),
        typeof(UnityAction),
        typeof(UnityAction<float>),
        typeof(UnityAction<int>),
        typeof(UnityAction<string>),
        typeof(UnityAction<Vector2>)
    };
}
