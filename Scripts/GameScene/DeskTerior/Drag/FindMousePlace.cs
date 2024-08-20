using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindMousePlace : MonoBehaviour
{
    //마우스의 위치를 찾습니다. 서브 데스크로 넘어가면 자동으로 이미지를 변경해줍니다.
    public static FindMousePlace instance;

    private Vector2 vector2;
    public Vector2 mousePosition;
    public bool inSubDesk;

    public GameObject MainObjectPosition;
    public GameObject SubObjectPotation;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        vector2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Mathf.Abs(960f) > vector2.x && Mathf.Abs(540.1f) > vector2.y)
        {
            mousePosition = vector2;
        }

        if (mousePosition.x < -504) inSubDesk = true;
        else inSubDesk = false;
    }

    public void MoveObjectToMousePosition(GameObject gameObject)
    {
        gameObject.transform.position = mousePosition;
    }

}
