using System;
using System.Collections;
using System.Security.Claims;
using UnityEngine;
using UnityEngine.UIElements;

public class Gun : MonoBehaviour
{
    [SerializeField] private PlayerInputSO inputSO;

    [SerializeField] private FirstPersonAnimator animator;

    [SerializeField] private FirstPersonCamera _mainCam;
    [SerializeField] private Muzzel muzzel;

    [SerializeField] private float recoilMulti = 0.3f;
    [SerializeField] private int MaxbulletCnt = 30;
    [SerializeField] private bool autoMode;
    [SerializeField] private int currentBulletCnt = 0;
    [SerializeField] private bool isBulletLoaded;

    [SerializeField] private bool HammerCocked;

    private bool _triggerPulling;

    private string _result;

    private bool _isAimming;

    private void Awake()
    {
        inputSO.OnAimInteracted += HandleAimming;//나중에 fsm해서 빼기
        inputSO.OnTriggerPressed += HandleTrigger;
        inputSO.OnCockingPressed += HandleCocking;
        inputSO.OnChangemagPressed += HandleChangeMag;
    }

    private void HandleChangeMag()
    {
        print("탄창이 제거됨.");
        StartCoroutine(ChangingMag(3f));
    }
    private IEnumerator ChangingMag(float time)
    {
        currentBulletCnt = 0;
        yield return new WaitForSeconds(time);
        currentBulletCnt = MaxbulletCnt;
        print("새로운 탄창이 삽입됨.");
    }

    private void HandleCocking()
    {
        if (currentBulletCnt > 0)
        {
            if (isBulletLoaded)
            {
                _result = "발사되지 않은 총알이 한발 바닥에 떨어졌다.";
                currentBulletCnt--;
            }
            else
            {
                _result = "찰칵!(총알이 약실로 장전됨.)";
                isBulletLoaded = true;
                currentBulletCnt--;
            }
        }
        else
        {
            _result = "찰칵(...)";
        }
        HammerCocked = true;
        print(_result);
    }

    private void OnDestroy()
    {
        inputSO.OnAimInteracted -= HandleAimming;
        inputSO.OnTriggerPressed -= HandleTrigger;
        inputSO.OnCockingPressed -= HandleCocking;
    }

    private void HandleAimming(bool val)
    {
        animator.SetParam("Aim",val);
        _mainCam.HandleAim(val);
        _isAimming = val;
    }

    private void HandleTrigger(bool isPressing)
    {
        _triggerPulling = isPressing;
        if (_triggerPulling)
        {
            Fire();
        }
        
    }
    private void Fire()
    {
        if (!HammerCocked)
        {
            _result = ". . .";
        }
        else if (_triggerPulling)
        {
            if(isBulletLoaded)
            {
                muzzel.FireEffect();

                //animator.SetParam("Fire");
                animator.PlayerAnimation("IdleFire");
                _mainCam.AddCameraRotate(UnityEngine.Random.Range(1f, 10f),UnityEngine.Random.Range(-recoilMulti, recoilMulti));
                _result = "탕!!!";
                currentBulletCnt--;
                isBulletLoaded = false;
                if (currentBulletCnt > 0)
                    isBulletLoaded = true;
            }
            else
            {
                _result = "틱.";
                HammerCocked = false;
            }
        }
        print(_result);

        if (_triggerPulling)
            StartCoroutine(Refire(0.1f));
    }
    private IEnumerator Refire(float time)
    {
        yield return new WaitForSeconds(time);
        Fire();
    }
}
