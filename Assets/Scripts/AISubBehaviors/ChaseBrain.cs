using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Brains/Chase")]
public class ChaseBrain : AiBehavior
{
    public string Targettag;
    public override void Think(EnemyThinker thinker)
    {
        GameObject target = GameObject.FindGameObjectWithTag(Targettag);
        if (target)
        {
            var movement = thinker.gameObject.GetComponent<EnemyMovement>();
            if (movement)
            {
                movement.MoveTowardsTarget(target.transform.position);
            }
        }
    }
}
