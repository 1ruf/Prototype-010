using UnityEngine;
using UnityEngine.InputSystem;

namespace VT2.Player
{
    public class RocketMovement : MonoBehaviour
    {
        [SerializeField] private Rocket player;
        void Update()
        {
            if(Keyboard.current.spaceKey.isPressed)
            {
                player.Rigidbody.AddForce(player.transform.up * 0.5f, ForceMode.Impulse);
            }
            if (Keyboard.current.aKey.isPressed)
            {
                player.Rigidbody.AddTorque(player.transform.right * -1 * 0.5f, ForceMode.Force);
            }
            if (Keyboard.current.dKey.isPressed)
            {
                player.Rigidbody.AddTorque(player.transform.right * 0.5f, ForceMode.Force);
            }
        }
    }
}
