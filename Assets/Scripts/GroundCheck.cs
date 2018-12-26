using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{

    private bool isGrounded = true;
    private float timeBetweenJumps = 0.3f;
    private float timeTouchingGround = 0;
    private float timeTouchingBounderies = 0;


    private void OnTriggerStay(Collider other)
    {
        if (!other.gameObject.CompareTag("Bounderies") && !other.gameObject.CompareTag("PostProcessing"))
        {
            timeTouchingGround += Time.deltaTime;
            if (timeTouchingGround >= timeBetweenJumps && !isGrounded)
            {
                isGrounded = true;
            }
        }
       /* else
        {
            timeTouchingBounderies += Time.deltaTime;
            if (timeTouchingBounderies >= 3.0f)
            {
                GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().DamageHealth(100);
            }
        }*/
    }

    public bool getTrigger()
    {
        return isGrounded;
    }

    public void setTrigger(bool isGrounded)
    {
        this.isGrounded = isGrounded;
        if (!isGrounded) timeTouchingGround = 0;
    }

    public void setTimeBetweenJumps(float t)
    {
        timeBetweenJumps = t;
    }


}
