using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NewVector3_FloatEvent", menuName = "SpellBrigade/Events/Vector3_Float Event")]
public class Vector3_FloatEvent : GameEvent<Vector3_Float>
{
}

[System.Serializable]
public class UnityVector3_FloatEvent : UnityEvent<Vector3_Float>
{
}

public class Vector3_FloatEventListener : GameEventListener<Vector3_Float, Vector3_FloatEvent, UnityVector3_FloatEvent>
{
}
