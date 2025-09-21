using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovment : MonoBehaviour
{
    [Header("Move")]
    public float speed = 6f;
    public float acceleration = 12f;

    [Header("Facing")]
    public bool rotateToFaceMove = true;        
    public bool rotateOnlyWhenMovingForward = true; 
    public float turnSpeedDegPerSec = 180f;

    [Header("Jump")]
    public float jumpForce = 5.5f;
    public float groundCheckDistance = 0.25f;
    public LayerMask groundMask = LayerMask.GetMask("Ground"); 

    [Header("Camera")]
    public Transform cameraTransform;

    Rigidbody _rb;
    Collider _col;


    bool jumpQueued;
    float inputH, inputV;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<Collider>();
        if (cameraTransform == null && Camera.main != null)
            cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        inputH = Input.GetAxisRaw("Horizontal");
        inputV = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Jump"))
            jumpQueued = true;
    }

    void FixedUpdate()
    {
        // כיוון תנועה יחסית למצלמה
        Vector3 direction;
        if (cameraTransform != null)
        {
            Vector3 fwd = cameraTransform.forward; fwd.y = 0f; fwd.Normalize();
            Vector3 right = cameraTransform.right; right.y = 0f; right.Normalize();
            direction = right * inputH + fwd * inputV;
        }
        else
        {
            direction = new Vector3(inputH, 0f, inputV);
        }
        if (direction.sqrMagnitude > 1f) direction.Normalize();

        Vector3 targetPlanarVel = direction * speed;
        Vector3 vel = _rb.linearVelocity;
        Vector3 planar = new Vector3(vel.x, 0f, vel.z);
        Vector3 newPlanar = Vector3.MoveTowards(planar, targetPlanarVel, acceleration * Time.fixedDeltaTime);
        vel.x = newPlanar.x;
        vel.z = newPlanar.z;

        if (jumpQueued && IsGrounded())
        {
            jumpQueued = false; 
            vel.y = 0f; 
            _rb.linearVelocity = vel;
            _rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        }
        else
        {
            _rb.linearVelocity = vel;
        }


        if (rotateToFaceMove)
        {
            Vector3 faceDir = direction;
            if (rotateOnlyWhenMovingForward && inputV < 0f)
            {
                faceDir = new Vector3(transform.forward.x, 0f, transform.forward.z);
            }

            if (faceDir.sqrMagnitude > 0.0001f)
            {
                Quaternion target = Quaternion.LookRotation(faceDir, Vector3.up);
                float maxDeg = turnSpeedDegPerSec * Time.fixedDeltaTime;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, target, maxDeg);
            }
        }
    }

    bool IsGrounded()
    {
        var b = _col.bounds;
        float radius = Mathf.Max(0.05f, Mathf.Min(b.extents.x, b.extents.z) - 0.01f);
        Vector3 origin = new Vector3(b.center.x, b.min.y + radius + 0.02f, b.center.z);
        return Physics.SphereCast(origin, radius, Vector3.down, out _, groundCheckDistance, groundMask, QueryTriggerInteraction.Ignore);
    }
}