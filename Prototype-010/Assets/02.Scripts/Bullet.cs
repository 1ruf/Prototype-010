using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 100;
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
}
