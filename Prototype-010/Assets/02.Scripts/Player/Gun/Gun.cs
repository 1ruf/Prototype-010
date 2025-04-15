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
        inputSO.OnAimInteracted += HandleAimming;//³ªÁß¿¡ fsmÇØ¼­ »©±â
        inputSO.OnTriggerPressed += HandleTrigger;
        inputSO.OnCockingPressed += HandleCocking;
        inputSO.OnChangemagPressed += HandleChangeMag;
    }

    private void HandleChangeMag()
    {
        print("ÅºÃ¢ÀÌ Á¦°ÅµÊ.");
        StartCoroutine(ChangingMag(3f));
    }
    private IEnumerator ChangingMag(float time)
    {
        currentBulletCnt = 0;
        yield return new WaitForSeconds(time);
        currentBulletCnt = MaxbulletCnt;
        print("»õ·Î¿î ÅºÃ¢ÀÌ »ðÀÔµÊ.");
    }

    private void HandleCocking()
    {
        if (currentBulletCnt > 0)
        {
            if (isBulletLoaded)
            {
                _result = "¹ß»çµÇÁö ¾ÊÀº ÃÑ¾ËÀÌ ÇÑ¹ß ¹Ù´Ú¿¡ ¶³¾îÁ³´Ù.";
                currentBulletCnt--;
            }
            else
            {
                _result = "ÂûÄ¬!(ÃÑ¾ËÀÌ ¾à½Ç·Î ÀåÀüµÊ.)";
                isBulletLoaded = true;
                currentBulletCnt--;
            }
        }
        else
        {
            _result = "ÂûÄ¬(...)";
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

                animator.SetParam("Fire");

                _result = "ÅÁ!!!";
                currentBulletCnt--;
                isBulletLoaded = false;
                if (currentBulletCnt > 0)
                    isBulletLoaded = true;
            }
            else
            {
                _result = "Æ½.";
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
