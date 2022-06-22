using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialEvents : MonoBehaviour
{
    public bool isMoving;
    public bool isJumping;
    public bool isSprinting;
    public bool isInteracting;
    public bool isEscaping;
    public bool isInventory;
    public bool isReadingNotepad;
    public bool isAnswer;

    public bool waitOnMove,waitOnJump,waitOnSprint,waitOnInteract,waitOnEscape;
    
    private void OnMove()
    {
        if (waitOnMove)
        {
            isMoving = true;
            waitOnMove = false;
        }
    }
    private void OnJump()
    {
        if (waitOnJump)
        {
            isJumping = true;
            waitOnJump = false;
        }
    }
    private void OnSprint()
    {
        if (waitOnSprint)
        {
            isSprinting = true;
            waitOnSprint = false;
        }
    }
    public void OnInteract()
    {
       if(waitOnInteract)
        {
            isInteracting = true;
            waitOnInteract = false;
        }
    }
    private void OnEscape()
    {
        if (waitOnEscape)
        {
            isEscaping = true;
            waitOnEscape = false;
        }
    }
}
