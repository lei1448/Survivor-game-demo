using System;
using UnityEngine;
using UnityEngine.Events;

// --- 2. “空包裹”专用的 UnityEvent ---
[Serializable]
public class UnityVoidEvent : UnityEvent<Void>
{
}
[Serializable]
public class UnityTransform_FloatEvent : UnityEvent<Transform_Float>
{
}
[Serializable]
public class UnityGameObjectEvent : UnityEvent<GameObject>
{
}
[Serializable]
public class UnityFloat_FloatEvent : UnityEvent<Float_Float>
{
}
[Serializable]
public class UnityString_StringEvent : UnityEvent<String_String>
{
}
[Serializable]
public class UnityIntEvent : UnityEvent<int>
{
}