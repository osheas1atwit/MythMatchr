using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera cm;
    [SerializeField] float speed;
    [SerializeField] float zoomSpeed;
    private Vector2 dir;
    [SerializeField] float camMaxSize;
    // Start is called before the first frame update
    void Start()
    {
        cm.m_Lens.OrthographicSize = camMaxSize;
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        dir = new Vector2(x, y);
        transform.Translate(dir.normalized * speed * Time.deltaTime);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -60, 60), Mathf.Clamp(transform.position.y, -50, 50), transform.position.z);

        float z = Input.mouseScrollDelta.y;

        cm.m_Lens.OrthographicSize += z * zoomSpeed;
        cm.m_Lens.OrthographicSize = Mathf.Clamp(cm.m_Lens.OrthographicSize, 3, camMaxSize);
    }
}
