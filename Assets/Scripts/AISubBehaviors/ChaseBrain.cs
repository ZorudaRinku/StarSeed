using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using Transform = UnityEngine.Transform;

[CreateAssetMenu(menuName = "Brains/Chase")]
public class ChaseBrain : AiBehavior
{
    public string Targettag;
    public Transform Player;

    public Rigidbody2D rigidbody;
    public override void Think(EnemyThinker thinker)
    {
        GameObject target = GameObject.FindGameObjectWithTag(Targettag);

            var movement = thinker.gameObject.GetComponent<EnemyMovement>();
            if (movement)
            {

                    movement.MoveTowardsTarget(target.transform.position);
                
            }
    }
}
