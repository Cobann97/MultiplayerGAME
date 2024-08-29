using Fusion;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    public Camera Camera;
    private GameManager GameManager;
    private CharacterController _controller;
    private bool _akey = false;
    public float Speed = 0f;
    public bool SpeedOnZ = false;
    public bool isReady = false;
    public float count = 3;
    public float animSpeed = 0;
    private string obstacleStr = "Obstacle";
    private string BarrierStr = "Barriers";

    public float gridSize = 5f; // Size of each grid unit
    private Vector3 _velocity;
    private Vector3 _targetPosition;

    public override void Spawned()
    {
        if (HasStateAuthority)
        {
            Camera = Camera.main;
            Camera.GetComponent<FirstPersonCamera>().Target = transform;
            _targetPosition = transform.position; // Initialize target position
        }
    }

    private void Awake()
    {
        GameManager = FindObjectOfType<GameManager>();
        _controller = GetComponent<CharacterController>();
        Speed = 0f;
        _akey = false;
    }

    private void Update()
    {
        isReady = GameManager.IsReadyFunc();
        count = GameManager.countReturner();
        if (isReady == true && count <= 0)
        {
            SpeedOnZ = true;

            if (Input.GetKeyDown(KeyCode.A) && !_akey)
            {
                _akey = true;
            }

            if (Input.GetKeyDown(KeyCode.D) && _akey)
            {
                if (Speed < 19f)
                {
                    _akey = false;
                    Speed += 80f;
                    Debug.Log("Speed increased");
                }
            }
            else
            {
                if (Speed > 0)
                {
                    Speed = -80f * Runner.DeltaTime;
                    if (_velocity.z < 0)
                        Speed = 0;
                }
            }

            // Grid-based movement logic
            if (Input.GetKeyDown(KeyCode.LeftArrow)) // Move left
            {
                _targetPosition += Vector3.left * gridSize;
                Debug.Log("A is pressed");
            }
            if (Input.GetKeyDown(KeyCode.RightArrow)) // Move right
            {
                _targetPosition += Vector3.right * gridSize;
                Debug.Log("D is pressed");
            }

            // Move towards the target position more quickly
            _velocity.x = Mathf.MoveTowards(_velocity.x, _targetPosition.x - transform.position.x, 1f);
            //Debug.Log(_velocity.x);

            _velocity.z = Mathf.Max(_velocity.z, 0f);
            _velocity.z += Speed * Runner.DeltaTime;
            animSpeed = _velocity.z;
            Debug.Log(_velocity.z);

        }
    }

    public override void FixedUpdateNetwork()
    {
        if (HasStateAuthority == false)
        {
            return;
        }

        if (_controller.isGrounded)
        {
            _velocity = new Vector3(0f, -1f, 0f);
        }

        if (SpeedOnZ == true)
        {
            _controller.Move(_velocity * Runner.DeltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == obstacleStr)
        {
            _velocity.z = 1f;
            Speed = 0f;
            
            Debug.Log("Hit obstacle");
        }
        if (collision.collider.tag == BarrierStr)
        {
            _velocity.z -= 1f;
            Debug.Log("Hit Barrier");
            if (_velocity.z < 0f)
            {
                _velocity.z = 0f;
            }
        }
    }


}


