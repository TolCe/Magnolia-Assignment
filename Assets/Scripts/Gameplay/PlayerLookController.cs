using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookController : MonoBehaviour
{
    [SerializeField] private float _lookSpeed;

    private void Awake()
    {
        GameEvents.Instance.OnNewEnemySpawned += LookAtTheEnemy;
    }

    public void LookAtTheEnemy(Transform target)
    {
        Vector3 targetPos = target.position;
        targetPos.y = transform.position.y;
        StartCoroutine(Look(Quaternion.LookRotation(targetPos - transform.position), _lookSpeed));
    }

    private IEnumerator Look(Quaternion targetRotation, float speed)
    {
        while (Quaternion.Angle(targetRotation, transform.rotation) > 0.001f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, speed * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }
    }
}
