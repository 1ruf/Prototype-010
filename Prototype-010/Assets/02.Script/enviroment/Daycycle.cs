using UnityEngine;

public class Daycycle : MonoBehaviour
{
    [SerializeField] private float cloudRotationSpeed;
    [SerializeField] private Transform cloud;
    private float _rY;

    private void Start()
    {
        _rY = cloud.rotation.y;
    }
    private void Update()
    {
        CloudCycle();
    }

    private void CloudCycle()
    {
        _rY += cloudRotationSpeed;
        cloud.rotation = Quaternion.Euler(0, _rY, 0);
    }
}
