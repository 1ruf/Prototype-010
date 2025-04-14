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

    [SerializeField] private int MaxbulletCnt = 30;
    [SerializeField] private bool autoMode;
    [SerializeField] private int currentBulletCnt = 0;
    [SerializeField] private bool isBulletLoaded;

    [SerializeField] private bool HammerCocked;

    [SerializeField] private float aimMultipleVal = 0.3f;


    private bool triggerPulling;

    private string result;

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
                result = "¹ß»çµÇÁö ¾ÊÀº ÃÑ¾ËÀÌ ÇÑ¹ß ¹Ù´Ú¿¡ ¶³¾îÁ³´Ù.";
                currentBulletCnt--;
            }
            else
            {
                result = "ÂûÄ¬!(ÃÑ¾ËÀÌ ¾à½Ç·Î ÀåÀüµÊ.)";
                isBulletLoaded = true;
                currentBulletCnt--;
            }
        }
        else
        {
            result = "ÂûÄ¬(...)";
        }
        HammerCocked = true;
        print(result);
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
    }

    private void HandleTrigger(bool isPressing)
    {
        triggerPulling = isPressing;
        if (triggerPulling)
        {
            Fire();
        }
        
    }
    private void Fire()
    {
        if (!HammerCocked)
        {
            result = ". . .";
        }
        else if (triggerPulling)
        {
            if(isBulletLoaded)
            {
                result = "ÅÁ!!!";
                currentBulletCnt--;
                isBulletLoaded = false;
                if (currentBulletCnt > 0)
                    isBulletLoaded = true;
            }
            else
            {
                result = "Æ½.";
                HammerCocked = false;
            }
        }
        print(result);

        if (autoMode)
            StartCoroutine(Refire(0.1f));
    }
    private IEnumerator Refire(float time)
    {
        yield return new WaitForSeconds(time);
        Fire();
    }
}
