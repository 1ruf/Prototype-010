using UnityEngine;

public class ScopeEffect : MonoBehaviour
{
    [SerializeField]
    private Transform eye;
    [SerializeField]
    private Transform eyeDistPos;
    [SerializeField]
    private float fadeDist;
    [SerializeField]
    private float maxAngle;
    // [SerializeField]
    // private float sharpenFactor;

    Vector2 posInput;

    [SerializeField]
    Material focusMat;

    float CircleCenterMovement;

    private void LateUpdate()
    {
        float distance = Vector3.Distance(eyeDistPos.position, eye.position);
        float mult = distance / fadeDist;
        mult = Mathf.Clamp(mult, 0, 1);

        Vector3 eyeToScope = transform.position - eye.position;
        float angle = Vector3.Angle(eyeToScope, transform.forward);

        CircleCenterMovement = angle / maxAngle;
        Vector3 localEyeToScope = transform.InverseTransformDirection(eyeToScope);
        posInput = new Vector2(localEyeToScope.x, localEyeToScope.y).normalized * CircleCenterMovement / 2;

        focusMat.SetFloat("_Mult", mult);
        focusMat.SetFloat("_XPos", 0.5f + posInput.x);
        focusMat.SetFloat("_YPos", 0.5f + posInput.y);
    }
}