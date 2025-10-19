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
    
    // Predictive shooting variables
    private Vector2 lastPlayerPosition;
    private Vector2 playerVelocity;
    [SerializeField] private float boltSpeed = 3f; // Should match the bolt's speed
    [SerializeField] private float predictionAccuracy = 0.8f; // How accurate the prediction is (0-1)


    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.Play("SlimeMove" + _direction);
        _aiPath = GetComponent<AIPath>();
        maxSpeed = _aiPath.maxSpeed;
        selfCollider = GetComponent<Collider2D>();
        
        // Initialize player tracking
        if (_player != null)
        {
            lastPlayerPosition = _player.transform.position;
        }
    }

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // Track player velocity for predictive shooting
        UpdatePlayerTracking();
        
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

    private void UpdatePlayerTracking()
    {
        if (_player != null)
        {
            Vector2 currentPlayerPosition = _player.transform.position;
            
            // Smooth the velocity calculation to avoid jittery predictions
            Vector2 instantVelocity = (currentPlayerPosition - lastPlayerPosition) / Time.deltaTime;
            playerVelocity = Vector2.Lerp(playerVelocity, instantVelocity, 0.5f);
            
            lastPlayerPosition = currentPlayerPosition;
        }
    }
    
    private Vector3 CalculatePredictedPlayerPosition()
    {
        if (_player == null) return Vector3.zero;
        
        Vector3 slimePosition = transform.position;
        Vector3 playerPosition = _player.transform.position;
        
        // Calculate distance to player
        float distanceToPlayer = Vector3.Distance(slimePosition, playerPosition);
        
        // Calculate time for bolt to reach player at current position
        float timeToReachPlayer = distanceToPlayer / boltSpeed;
        
        // Reduce prediction time to avoid overshooting - multiply by a factor less than 1
        float predictionTimeFactor = 0.6f; // Adjust this value (0.3-0.8 range works well)
        float adjustedPredictionTime = timeToReachPlayer * predictionTimeFactor;
        
        // Predict where player will be based on their current velocity
        Vector3 predictedPosition = playerPosition + (Vector3)playerVelocity * adjustedPredictionTime;
        
        // Apply prediction accuracy (allows for some error in AI aiming)
        Vector3 errorOffset = Vector3.zero;
        if (predictionAccuracy < 1f)
        {
            float errorAmount = (1f - predictionAccuracy) * 1.5f; // Slightly reduced error scaling
            errorOffset = new Vector3(
                UnityEngine.Random.Range(-errorAmount, errorAmount),
                UnityEngine.Random.Range(-errorAmount, errorAmount),
                0
            );
        }
        
        return predictedPosition + errorOffset;
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
                Vector3 targetPosition = CalculatePredictedPlayerPosition();
                
                // Calculate direction from slime to predicted target position
                Vector3 direction = (targetPosition - slimePosition).normalized;
                
                // Calculate the angle in degrees for 2D rotation
                // Since the bolt moves down in its local space, we need to rotate so its "down" points toward the target
                float angle = Mathf.Atan2(direction.x, -direction.y) * Mathf.Rad2Deg;
                
                // Create the rotation quaternion
                Quaternion rotation = Quaternion.Euler(0, 0, angle);
                
                GameObject boltGameObject = Instantiate(bolt, new Vector3(slimePosition.x, slimePosition.y, -1), rotation);
                Physics2D.IgnoreCollision(boltGameObject.GetComponent<Collider2D>(), selfCollider);
                _shot = true;
            }
        }
        else
            _shot = false;
    }
}
