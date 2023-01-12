using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public bool Touch, gameState;
    private Vector3 mouseStartPosition, playerStartPosition;
    public float playerTouchSpeed;
    private Camera camera;
    [SerializeField] private Transform player;
    public float playerMoveSpeed;

    private void Start()
    {
        camera = Camera.main;

    }
    private void Update()
    {
        MoveThePlayer();
    }

    private void MoveThePlayer()
    {
        if (Input.GetMouseButtonDown(0) && gameState)
        {
            Touch = true;

            Plane plane = new Plane(Vector3.up, 0f);
            var ray = camera.ScreenPointToRay(Input.mousePosition);

            if (plane.Raycast(ray, out var distance))
            {
                mouseStartPosition = ray.GetPoint(distance + 1f);
                playerStartPosition = transform.position;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            Touch = false;
        }

        if (Touch)
        {
            var plane = new Plane(Vector3.up, 0f);
            var ray = camera.ScreenPointToRay(Input.mousePosition);

            if (plane.Raycast(ray, out var distance))
            {
                Vector3 mousePos = ray.GetPoint(distance + 1f);
                Vector3 move = mousePos - mouseStartPosition;
                Vector3 control = playerStartPosition + move;

                control.x = Math.Clamp(control.x, -1f, 1f);

                transform.position = new Vector3(Mathf.Lerp(transform.position.x, control.x, Time.deltaTime * playerTouchSpeed), transform.position.y, transform.position.z);
            }
        }

        if (gameState)
        {
            player.Translate(player.forward * Time.deltaTime * playerMoveSpeed);
        }
    }




}



