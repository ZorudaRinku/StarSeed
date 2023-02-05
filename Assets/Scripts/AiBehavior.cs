using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AiBehavior : ScriptableObject
{
    public abstract void Think(EnemyThinker thinker);
}
