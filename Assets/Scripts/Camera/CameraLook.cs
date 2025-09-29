using UnityEngine;

public class CameraLook : MonoBehaviour
{
    [Header("References")]
    public Transform playerRoot;       // גרור פה את אובייקט השחקן (עם ה-Rigidbody)

    [Header("Mouse")]
    public float sensitivity = 2.0f;
    public float pitchMin = -85f;
    public float pitchMax =  85f;
    public bool invertY = false;

    float _pitch; // הצטברות סיבוב למעלה/למטה (על ה-CamPivot)

    void Start()
    {
        if (!playerRoot) playerRoot = transform.parent;  // ברירת מחדל: ההורה של ה-CamPivot
        _pitch = 0f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible   = false;
    }

    void Update()
    {
        float mx = Input.GetAxis("Mouse X");
        //float my = Input.GetAxis("Mouse Y") * (invertY ? 1f : -1f);

        // yaw – מסובב את גוף השחקן סביב Y
        if (playerRoot) playerRoot.Rotate(0f, mx * sensitivity, 0f, Space.Self);

        // pitch – מסובב את ה-CamPivot בלבד
        //_pitch = Mathf.Clamp(_pitch + my * sensitivity, pitchMin, pitchMax);
        transform.localRotation = Quaternion.Euler(_pitch, 0f, 0f);
    }
}