using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using Object = UnityEngine.Object;

public class SlimeAI : MonoBehaviour
{
    private GameObject _player;
    private bool _playerInSightRange;
    private bool _playerInAttackRange;
    private bool _playerTooCloseRange;
    private Collider2D selfCollider;
    private AIPath _aiPath;
    private float maxSpeed;
    private Animator _animator;
    private string _direction = "Right";
    [SerializeField] private GameObject bolt;
    private bool _shot;
    [SerializeField] private float attackSpeed = 1;


    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.Play("SlimeMove" + _direction);
        _aiPath = GetComponent<AIPath>();
        maxSpeed = _aiPath.maxSpeed;
        selfCollider = GetComponent<Collider2D>();
    }

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (_aiPath.remainingDistance > 40)
            _aiPath.maxSpeed = 0f;
        else
            _aiPath.maxSpeed = maxSpeed;
        
        if (_aiPath.destination.x >= transform.position.x)
            _direction = "Right";
        else if (_aiPath.destination.x <= transform.position.x)
            _direction = "Left";
        if (_aiPath.remainingDistance <= 10f)
            ShootPlayer();
        else 
            MoveToPlayer();
    }

    private void MoveToPlayer()
    {
        if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("SlimeMove" + _direction))
            _animator.Play("SlimeMove" + _direction, 0, _animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1);
    }

    private void ShootPlayer()
    {
        if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("SlimeShoot" + _direction))
            _animator.Play("SlimeShoot" + _direction, 0, _animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1);
        _animator.speed = attackSpeed;
        if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1 < 0.6667f &&
            _animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1 > 0.5f)
        {
            if (!_shot)
            {
                Vector3 slimePosition = transform.position;
                Quaternion rotatedVectorToTarget =
                    Quaternion.FromToRotation(Vector3.down, _player.transform.position - slimePosition);
                GameObject boltGameObject = Instantiate(bolt, new Vector3(slimePosition.x, slimePosition.y, -1), new Quaternion(0, 0, rotatedVectorToTarget.z, rotatedVectorToTarget.w));
                Physics2D.IgnoreCollision(boltGameObject.GetComponent<Collider2D>(), selfCollider);
                _shot = true;
            }
        }
        else
            _shot = false;
    }
}
