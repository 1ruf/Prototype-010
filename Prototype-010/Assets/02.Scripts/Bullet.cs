using System.Runtime.InteropServices;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 100;
    [SerializeField] private float recochetAngle = 85f;
    private Rigidbody _rigid;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _rigid.AddForce(transform.forward * speed, ForceMode.Force);
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("HIT:" + collision.gameObject.name);
        
        //Destroy(gameObject);
    }

    private void CheckRecochet(Collision target)
    {
        ContactPoint cP = target.contacts[0];
        Vector3 normal = cP.normal;
        Vector3 contactVelocity = target.relativeVelocity;

        float angle = Vector3.Angle(-contactVelocity, normal);

        if(CalcRecochet(angle))
        {
            print("recochet");
        }
        else
        {
            Destroy(gameObject);
        }
    }    

    private bool CalcRecochet(float angle)
    {
        return angle >= recochetAngle;
    }
}
