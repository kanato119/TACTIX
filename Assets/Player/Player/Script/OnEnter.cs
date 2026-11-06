using UnityEngine;

public class OnEnter : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            Debug.Log("地面と触れている");
            PlayerMove.instance.grounded = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("地面と離れている");
        PlayerMove.instance.grounded = false;
    }
}
