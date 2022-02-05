using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _lookSpeed;
    private Vector3 _initPos;

    private void Awake()
    {
        GameEvents.Instance.OnNewEnemySpawned += LookAtTheEnemy;
        //GameEvents.Instance.OnWeaponRecoiled += OnWeaponRecoiled;
    }

    private void Start()
    {
        _initPos = transform.position;
    }

    public void LookAtTheEnemy(Transform target)
    {
        Vector3 targetPos = target.position;
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

    private void OnWeaponRecoiled(Quaternion targetRotation, float time)
    {
        float angle = Quaternion.Angle(transform.rotation, targetRotation);
        StartCoroutine(RecoilCamera(targetRotation, time, angle / time));
    }

    private IEnumerator RecoilCamera(Quaternion targetRotation, float time, float speed)
    {
        Quaternion initRotation = transform.rotation;
        StartCoroutine(Look(targetRotation, speed));
        yield return new WaitForSeconds(time);
        StartCoroutine(Look(initRotation, speed * 2f));
    }
}
