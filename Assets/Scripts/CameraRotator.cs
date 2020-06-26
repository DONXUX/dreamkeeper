using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    // 회전속도
    // [SerializeField]에 의해 인스펙터 뷰에 변수를 노출하여 값 수정 가능 (public과 동일)
    [SerializeField] float angularVelocity = 30f;
    // 수평 및 수직 방향의 회전량(각도)의 매 프레임 누적 값
    float horizontalAngle = 0f;
    float verticalAngle = 0f;

    private void Start()
    {
        // 마우스 커서 비가시, 고정 모드
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // 전 프레임에서 현 프레임까지의 수평/수직 회전량(각도)
        var horizontalRotation = Input.GetAxis("Mouse X") * angularVelocity * Time.deltaTime;
        var verticalRotation = -Input.GetAxis("Mouse Y") * angularVelocity * Time.deltaTime;

        horizontalAngle += horizontalRotation;
        verticalAngle += verticalRotation;

        // 누적 각도의 최소/최대값 제한
        verticalAngle = Mathf.Clamp(verticalAngle, -80f, 80f);

        // 오일러 방식에 의한 회전(zxy순)을 쿼터니언 방식의 회전으로 변환
        transform.rotation = Quaternion.Euler(verticalAngle, horizontalAngle, 0f);
    }
}
