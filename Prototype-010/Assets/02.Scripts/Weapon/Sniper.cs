using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class Sniper : MonoBehaviour
{
    [SerializeField] private PlayerInputSO input;
    [SerializeField] private CinemachineImpulseSource gunImpulse;

    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private Transform muzzel;

    private void Awake()
    {
        input.OnTriggerPressed += HandleFire;
    }
    private void OnDestroy()
    {
        input.OnTriggerPressed -= HandleFire;
    }

    private void HandleFire(bool pressed)
    {
        if (pressed)
        {
            Fire();
        }
    }

    private void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab,null);
        bullet.transform.position = muzzel.position;
        bullet.transform.rotation = muzzel.rotation;

        gunImpulse.GenerateImpulseWithForce(0.1f);
        Quaternion curRotation = transform.rotation;
        curRotation.x += -0.025f;
        transform.rotation = curRotation;
        StartCoroutine(Revert());
    }

    private IEnumerator Revert()
    {
        for(int i = 0;i < 20;++i)
        {
            yield return new WaitForSeconds(0.01f);
            Quaternion curRotation = transform.rotation;
            curRotation.x += +0.001f;
            transform.rotation = curRotation;
        }
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0.1f;
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Time.timeScale = 1f;
        }
    }
}
