using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovment : MonoBehaviour
{
    [Header("Move")]
    public float speed = 6f;
    public float acceleration = 12f;

    [Header("Facing")]
    public bool rotateToFaceMove = true;        // כבה אם המצלמה "משתגעת"
    public bool rotateOnlyWhenMovingForward = true; // כשהוא הולך אחורה—לא נסובב (נלך לאחור)
    public float turnSpeedDegPerSec = 180f;

    [Header("Jump")]
    public float jumpForce = 5.5f;
    public float groundCheckDistance = 0.25f;
    public LayerMask groundMask = ~0; // עדיף להגדיר Layer "Ground" ולבחור אותו

    [Header("Camera")]
    public Transform cameraTransform; // גרור Main Camera; אם ריק—נאתר לבד

    Rigidbody rb;
    Collider col;

    // נאגרים ב-Update כדי לא לפספס פריימים בפיזיקה
    bool jumpQueued;
    float inputH, inputV;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        if (cameraTransform == null && Camera.main != null)
            cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        inputH = Input.GetAxisRaw("Horizontal");
        inputV = Input.GetAxisRaw("Vertical");

        // חשוב: GetButtonDown ב-Update (לא ב-FixedUpdate) כדי לא לפספס פריים
        if (Input.GetButtonDown("Jump"))
            jumpQueued = true;
    }

    void FixedUpdate()
    {
        // כיוון תנועה יחסית למצלמה
        Vector3 wish;
        if (cameraTransform != null)
        {
            Vector3 fwd = cameraTransform.forward; fwd.y = 0f; fwd.Normalize();
            Vector3 right = cameraTransform.right; right.y = 0f; right.Normalize();
            wish = right * inputH + fwd * inputV;
        }
        else
        {
            wish = new Vector3(inputH, 0f, inputV);
        }
        if (wish.sqrMagnitude > 1f) wish.Normalize();

        // מהירות יעד והאצה/בלימה חלקה
        Vector3 targetPlanarVel = wish * speed;
        Vector3 vel = rb.linearVelocity;
        Vector3 planar = new Vector3(vel.x, 0f, vel.z);
        Vector3 newPlanar = Vector3.MoveTowards(planar, targetPlanarVel, acceleration * Time.fixedDeltaTime);
        vel.x = newPlanar.x;
        vel.z = newPlanar.z;

        // קפיצה
        if (jumpQueued && IsGrounded())
        {
            jumpQueued = false; // צורכים את הלחיצה
            vel.y = 0f; // כדי לקבל גובה עקבי
            rb.linearVelocity = vel;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        }
        else
        {
            rb.linearVelocity = vel;
        }

        // סיבוב יציב (לא כשצועדים אחורה, אם רוצים להימנע מסיבובים)
        if (rotateToFaceMove)
        {
            Vector3 faceDir = wish;
            if (rotateOnlyWhenMovingForward && inputV < 0f)
            {
                // שומרים על הכיוון הנוכחי, הולכים לאחור בלי לסובב
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
        // בדיקה יציבה לפי גודל הקוליידר: SphereCast קצר למטה
        var b = col.bounds;
        float radius = Mathf.Max(0.05f, Mathf.Min(b.extents.x, b.extents.z) - 0.01f);
        Vector3 origin = new Vector3(b.center.x, b.min.y + radius + 0.02f, b.center.z);
        return Physics.SphereCast(origin, radius, Vector3.down, out _, groundCheckDistance, groundMask, QueryTriggerInteraction.Ignore);
    }
}