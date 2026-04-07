using UnityEngine;

[ExecuteInEditMode]
public class DebugEditMove : MonoBehaviour
{
    public Camera _cam;
    public static Camera cam;

    public void hoge()
    {
        transform.position += new Vector3(5, 0, 0);
    }
}
