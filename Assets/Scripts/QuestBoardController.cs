using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class QuestBoardController : MonoBehaviour
{
    public List<BasicBodyPart> questDisplay = new List<BasicBodyPart>();
    public Vector3 targetMove = Vector3.up;
    

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void OnMouseEnter()
    {
        if (transform.position == startPos)
        {
            OpenScreen();
        }
    }

    private void OnMouseExit()
    {
        if (transform.position != startPos)
        {
            CloseScreen();
        }
    }

    protected void CloseScreen()
    {
        transform.DOMove(startPos, 0.2f);
    }

    protected void OpenScreen()
    {
        transform.DOMove(startPos + targetMove, 0.5f);
    }
}
