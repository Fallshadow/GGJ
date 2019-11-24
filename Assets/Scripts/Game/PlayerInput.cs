using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    public RolePhysics playerPhy = null;

    public KeyCode runLeftKey = KeyCode.A;
    public KeyCode runRightKey = KeyCode.D;
    public KeyCode jumpKey = KeyCode.K;
    public KeyCode sprintKey = KeyCode.L;




    private void FixedUpdate()
    {
        
        if (Input.GetKey(runLeftKey))
        {
            
            playerPhy.RunLeft();
        }

        if (Input.GetKey(runRightKey))
        {

            playerPhy.RunRight();
        }

        if (Input.GetKeyDown(jumpKey))
        {
            playerPhy.Jump();
        }

        if (Input.GetKey(jumpKey))
        {
            playerPhy.JumpContinue();
        }

        if (Input.GetKeyDown(sprintKey))
        {

            playerPhy.Sprint();
        }
    }
}
