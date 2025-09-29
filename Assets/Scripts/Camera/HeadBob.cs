using UnityEngine;

public class HeadBob : MonoBehaviour
{
    [Header("Refs")]
    public Rigidbody playerRb;      // גרור את ה-Rigidbody של השחקן
    public Transform cam;           // לרוב המצלמה (Camera.main.transform)

    [Header("Tune")]
    public float walkFreq = 9f;     // 7–12 נעים
    public float ampY = 0.03f;      // גובה תנודה
    public float ampZ = 0.02f;      // דחיפת קדימה-אחורה
    public float minSpeed = 0.2f;   // סף להפעלת הבוב
    public float lerp = 10f;        // החלקה למיקום הסופי

    Vector3 _baseLocalPos;
    float _phase;

    void Start()
    {
        if (!cam) cam = Camera.main ? Camera.main.transform : transform;
        _baseLocalPos = cam.localPosition;
    }

    void Update()
    {
        if (!playerRb || !cam) return;

        // מהירות אופקית
        Vector3 v = playerRb.linearVelocity; v.y = 0f;
        float speed = v.magnitude;

        // צבירת פאזה רק כשזזים
        if (speed > minSpeed) _phase += walkFreq * speed * Time.deltaTime;
        else _phase = Mathf.MoveTowards(_phase, 0f, walkFreq * Time.deltaTime);

        // תנודה
        Vector3 offset = (speed > minSpeed)
            ? new Vector3(0f, Mathf.Sin(_phase) * ampY, Mathf.Cos(_phase * 2f) * ampZ)
            : Vector3.zero;

        // החלקה למיקום
        cam.localPosition = Vector3.Lerp(cam.localPosition, _baseLocalPos + offset, lerp * Time.deltaTime);
    }
}