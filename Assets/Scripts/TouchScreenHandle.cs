using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchScreenHandle : MonoBehaviour
{
    Touch touch;
    [SerializeField] float _speedOfRotation;
    [SerializeField] Vector3 direction;
    PlayerController playerController;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }


    private void Update()
    {
        OnTouchRotate2();
    }
    
    public void OnTouchRotate2()
    {
        
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended)
            {
                playerController.Shoot();
            }
        }
    }

    

    public void RotatePlayer(float angle)
    {
        transform.localEulerAngles = new Vector3(transform.rotation.x, angle, transform.rotation.z);
    }
}
