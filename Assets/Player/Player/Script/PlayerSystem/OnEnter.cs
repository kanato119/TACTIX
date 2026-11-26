using UnityEngine;

public class OnEnter : MonoBehaviour
{
    [SerializeField] private PlayerMove playerMove;

    private void OnTriggerStay(Collider other)
    {


        if(other.gameObject.CompareTag("Ground"))
        {
            Debug.Log("地面と触れている");

            playerMove.grounded = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("地面と離れている");
        playerMove.grounded = false;
    }
}
