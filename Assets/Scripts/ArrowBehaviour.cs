using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehaviour : MonoBehaviour
{
    [SerializeField] float _speed;
    public bool isLanded;
    Vector3 _startPosition;
    [SerializeField] float _distanceToDisapear, _secondsToDisapearAfterHit;

    void FixedUpdate()
    {
        FlyForward();
        DeactivateOutOfDistance();
    }

    private void OnCollisionEnter(Collision collision)
    {
        isLanded = true;
        transform.parent = collision.gameObject.transform;

            var playerControllerScript = collision.gameObject.GetComponentInParent<EnemyBehaviour>();
            if (playerControllerScript != null)
            {
                playerControllerScript.ActivateRagDoll();
            }
            else Debug.Log("There is no EnemyBehavior.");
        StartCoroutine("DeactivateAfterDelay");
    }

    public void UseArrow()
    {
        _startPosition = transform.position;
        isLanded = false;
        transform.parent = null;
    }

    void DeactivateOutOfDistance()
    {
        if (Vector3.Distance(transform.position, _startPosition) > _distanceToDisapear) gameObject.SetActive(false);
    }

    void FlyForward()
    {
        if (!isLanded)
            transform.Translate(Vector3.forward * _speed, Space.Self);
    }

    IEnumerator DeactivateAfterDelay()
    {
        yield return new WaitForSeconds(_secondsToDisapearAfterHit);
        gameObject.SetActive(false);
    }
}
