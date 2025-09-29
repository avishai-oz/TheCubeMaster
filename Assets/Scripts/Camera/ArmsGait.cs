using UnityEngine;

public class ArmsGait : MonoBehaviour
{
    [Header("Refs")]
    public Rigidbody playerRb;     // גרור את ה-Rigidbody של השחקן

    [Header("Shape")]
    public float ampY = 0.05f;     // אמפליטודה למעלה/למטה
    public float ampZ = 0.04f;     // אמפליטודה קדימה/אחורה
    public float zLead = 0.25f;    // היסט קטן שמקדם את ה-"קדימה" אחרי העלייה (במחזורים)

    [Header("Speed Coupling")]
    public float stepFreq = 8f;    // תדירות בסיסית לצעידה
    public float speedScale = 1f;  // כמה המהירות משפיעה על התדירות
    public float startThreshold = 0.2f; // מהירות שמעליה מתחילים לצעוד

    [Header("Smoothing")]
    public float posLerp = 12f;    // החלקה למיקום
    public float returnLerp = 8f;  // החלקה בחזרה לבסיס כשעוצרים
    
    public int direction = 1;

    Vector3 _basePos;
    float _phase;

    void Start()
    {
        if (!playerRb) playerRb = FindFirstObjectByType<Rigidbody>();
        _basePos = transform.localPosition;
    }

    void Update()
    {
        float speed = 0f;
        if (playerRb)
        {
            var v = playerRb.linearVelocity; v.y = 0f;
            speed = v.magnitude;
        }

        // צבירת פאזה – מהר יותר ככל שזזים מהר
        if (speed > startThreshold)
        {
            _phase += (stepFreq + speed * speedScale) * Time.deltaTime;
        }
        else
        {
            // דועך בעדינות לפאזה 0 כשאין תנועה
            _phase = Mathf.MoveTowards(_phase, 0f, stepFreq * Time.deltaTime);
        }

        
        // Z מקבל היסט פאזי קטן (zLead) כדי שה"קדימה" תגיע מיד אחרי העלייה
        float y = Mathf.Sin(_phase) * ampY*direction;
        float z = Mathf.Cos(_phase + zLead * Mathf.PI * 2f) * ampZ;
        
        Vector3 target = (speed > startThreshold) ? _basePos + new Vector3(0f, y, z) : _basePos;
        float lerp = (speed > startThreshold) ? posLerp : returnLerp;

        transform.localPosition = Vector3.Lerp(transform.localPosition, target, lerp * Time.deltaTime);
    }
}