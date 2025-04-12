using System;
using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private PlayerInputSO inputSO;

    [SerializeField] private int MaxbulletCnt = 30;
    [SerializeField] private bool autoMode;
    [SerializeField] private int currentBulletCnt = 0;
    private bool isBulletLoaded;

    private bool HammerCocked;

    private bool triggerPulling;

    private string result;

    private void Awake()
    {
        inputSO.OnTriggerPressed += HandleTrigger;
        inputSO.OnCockingPressed += HandleCocking;
        inputSO.OnChangemagPressed += HandleChangeMag;
    }

    private void HandleChangeMag()
    {
        print("źâ�� ���ŵ�.");
        StartCoroutine(ChangingMag(3f));
    }
    private IEnumerator ChangingMag(float time)
    {
        currentBulletCnt = 0;
        yield return new WaitForSeconds(time);
        currentBulletCnt = MaxbulletCnt;
        print("���ο� źâ�� ���Ե�.");
    }

    private void HandleCocking()
    {
        if (currentBulletCnt > 0)
        {
            if (isBulletLoaded)
            {
                result = "�߻���� ���� �Ѿ��� �ѹ� �ٴڿ� ��������.";
                currentBulletCnt--;
            }
            else
            {
                result = "��Ĭ!(�Ѿ��� ��Ƿ� ������.)";
                isBulletLoaded = true;
                currentBulletCnt--;
            }
        }
        HammerCocked = true;
        print(result);
    }

    private void OnDestroy()
    {
        inputSO.OnTriggerPressed -= HandleTrigger;
        inputSO.OnCockingPressed -= HandleCocking;
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
                result = "��!!!";
                currentBulletCnt--;
                isBulletLoaded = false;
                if (currentBulletCnt > 0)
                    isBulletLoaded = true;
            }
            else
            {
                result = "ƽ.";
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
