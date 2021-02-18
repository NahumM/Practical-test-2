using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject _arrowPrefab;
    List<GameObject> arrowsPool = new List<GameObject>();

    [SerializeField] GameObject _shootingPlace;
    [SerializeField] GameObject _player;
    [SerializeField] GameObject _aim;
    [SerializeField] float _speedOfMoving, _speedOfRotation;
    [SerializeField] Rigidbody[] playerRagDollRigidbodies;
    [SerializeField] Animator _bodyAnimator;


    bool isDead;
    Vector3 movementDir;


    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        movementDir = new Vector3(0, horizontalInput, 0);

        // PlayerMovement();

        if (Input.GetKeyDown(KeyCode.Space))
            Shoot();
    }

    private void FixedUpdate()
    {
        PlayerMovement();
    }

    public void Shoot()
    {
        if (_bodyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Empty")) // Allows to shoot when previous animation of shooting is finished.
        {
            GameObject arrow = GetPooledArrow();
            arrow.transform.position = _shootingPlace.transform.position;
            arrow.transform.rotation = transform.rotation;
            _bodyAnimator.Play("Shooting");
        }
    }

    void PlayerDeath()
    {
        isDead = true;
        _aim.SetActive(false);
        Camera.main.transform.parent = null;
        _player.transform.parent = null;
        _player.GetComponent<Animator>().enabled = false;
        foreach (Rigidbody rb in playerRagDollRigidbodies)
        {
            rb.isKinematic = false;
        }
    }

    void PlayerMovement()
    {
        if (!isDead)
        {
           transform.Rotate(movementDir * Time.fixedDeltaTime * _speedOfRotation);
        }
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, 5f, 13f), transform.position.y, transform.position.z);
        transform.Translate(Vector3.forward * Time.fixedDeltaTime * _speedOfMoving);

    }

    public GameObject GetPooledArrow()
    {
        for (int i = 0; i < arrowsPool.Count; i++) // Checks if there is arrows which deactivated and can be used
        {
            if (!arrowsPool[i].activeInHierarchy)
            {
                arrowsPool[i].SetActive(true); // Activates the arrow and returns it
                arrowsPool[i].GetComponent<ArrowBehaviour>().UseArrow();
                return arrowsPool[i];
            }
        }
        // If there is no arrows to use, instantiates new arrow and returns it
        GameObject arrow = Instantiate(_arrowPrefab);
        arrowsPool.Add(arrow);
        arrow.GetComponent<ArrowBehaviour>().UseArrow();
        return arrow;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            var playerControllerScript = other.gameObject.GetComponentInParent<EnemyBehaviour>();
            if (playerControllerScript != null)
            {
                playerControllerScript.ActivateRagDoll();
            }
            else Debug.Log("There is no EnemyBehavior.");

            PlayerDeath();
        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        float start = (min + max) * 0.5f - 180;
        float floor = Mathf.FloorToInt((angle - start) / 360) * 360;
        min += floor;
        max += floor;
        return Mathf.Clamp(angle, min, max);
    }

}
