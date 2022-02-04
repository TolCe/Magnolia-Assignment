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
    }
    private void Start()
    {
        _initPos = transform.position;
    }

    public void LookAtTheEnemy(Transform target)
    {
        StartCoroutine(Look(target));
    }

    private IEnumerator Look(Transform target)
    {
        Vector3 targetPos = target.position;
        while (Quaternion.Angle(Quaternion.LookRotation(targetPos - transform.position), transform.rotation) > 2f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(targetPos - transform.position), _lookSpeed * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }
    }
}
