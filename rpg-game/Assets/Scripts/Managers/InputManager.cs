using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = FindObjectOfType<Camera>();
        DontDestroyOnLoad(this.gameObject);
    }

    public Vector2 GetMovement() => new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
    public Vector2 GetAim() => new Vector2(Input.GetAxis("HorizontalAim"), Input.GetAxis("VerticalAim")).normalized;

    public Vector3 GetMousePosition() => mainCamera.ScreenToWorldPoint(Input.mousePosition);

    public bool GetAttack() => Input.GetButton("Attack");
    public bool GetAttackDown() => Input.GetButtonDown("Attack");

    public bool GetDefend() => Input.GetButton("Defend");
    public bool GetDefendDown() => Input.GetButtonDown("Defend");

    public bool GetDash() => Input.GetButton("Dash");
    public bool GetDashDown() => Input.GetButtonDown("Dash");

    public bool GetSubmit() => Input.GetButton("Submit");
    public bool GetSubmitDown() => Input.GetButtonDown("Submit");

    public bool GetCancel() => Input.GetButton("Cancel");
    public bool GetCancelDown() => Input.GetButtonDown("Cancel");
}
