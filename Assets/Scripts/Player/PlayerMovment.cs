using UnityEngine;

namespace Player
{
    public class PlayerMovment : MonoBehaviour
    {
        [Header("Move")]
        public float speed = 6f;
        public float acceleration = 12f;

        [Header("Jump")]
        public float jumpForce = 5.5f;
        public float groundCheckDistance = 0.25f;
        public LayerMask groundMask;
        
        Rigidbody _rb;
        Collider _col;
        
        public PowerupManager powerupManager;

        bool _jumpQueued;
        float _inputH, _inputV;

        void Awake()
        {
            groundMask= LayerMask.GetMask("Ground"); 
            _rb = GetComponent<Rigidbody>();
            _col = GetComponent<Collider>();
            if (powerupManager == null)
                powerupManager = GetComponent<PowerupManager>() ?? gameObject.AddComponent<PowerupManager>();
        }

        void Update()
        {
            _inputH = Mathf.MoveTowards(_inputH, Input.GetAxis("Horizontal"), 6f * Time.deltaTime);
            _inputV = Mathf.MoveTowards(_inputV, Input.GetAxis("Vertical"),   6f * Time.deltaTime);
            
            if (Input.GetButtonDown("Jump"))
            {
                _jumpQueued = true;
            }
        }

        void FixedUpdate()
        {
            // כיוון תנועה RELATIVE ל-orientation (שה-CameraLook מסובב)
            Vector3 fwd   = transform.forward; fwd.y = 0f; fwd.Normalize();
            Vector3 right = transform.right;   right.y = 0f; right.Normalize();
            Vector3 dir = right * _inputH + fwd * _inputV;
            if (dir.sqrMagnitude > 1f) dir.Normalize();

            // תאוצה למהירות מטרה
            float speedMult = powerupManager ? powerupManager.SpeedMult : 1f;
            Vector3 targetPlanar = dir * (speed * speedMult);

            Vector3 vel    = _rb.linearVelocity;
            Vector3 planar = new Vector3(vel.x, 0f, vel.z);

            // שליטה חלשה יותר באוויר (אופציונלי)
            float airControl = 0.45f;
            bool grounded = IsGrounded();
            float accelNow = acceleration * (grounded ? 1f : airControl);

            Vector3 newPlanar = Vector3.MoveTowards(planar, targetPlanar, accelNow * Time.fixedDeltaTime);
            vel.x = newPlanar.x; vel.z = newPlanar.z;

            // קפיצה
            if (_jumpQueued && grounded)
            {
                _jumpQueued = false;
                vel.y = 0f;
                float jumpMult = powerupManager ? powerupManager.JumpMult : 1f;
                _rb.linearVelocity = vel;
                _rb.AddForce(Vector3.up * (jumpForce * jumpMult), ForceMode.VelocityChange);
            }
            else
            {
                _rb.linearVelocity = vel;
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
}
