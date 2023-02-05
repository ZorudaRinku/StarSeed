using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyThinker : MonoBehaviour
{
    public AiBehavior AiBehavior;
    void Update()
    {
        AiBehavior.Think(this);
    }
}
