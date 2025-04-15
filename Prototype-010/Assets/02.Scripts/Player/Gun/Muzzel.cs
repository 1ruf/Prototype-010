using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class Muzzel : MonoBehaviour
{
    [SerializeField] private Light muzzelLight;
    private VisualEffect effect;
    private void Start()
    {
        effect = GetComponentInChildren<VisualEffect>();
        muzzelLight.enabled = false;
    }
    public void FireEffect()
    {
        StartCoroutine(SetLight(0.05f));
        effect.Play();
    }

    private IEnumerator SetLight(float time)
    {
        muzzelLight.enabled = true;
        yield return new WaitForSeconds(time);
        muzzelLight.enabled = false;
        effect.Stop();
    }
}
