﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    [HideInInspector]
    public Vector2Int posIndex;
    public Board board;

    private Vector2 firstTouchPosition;
    private Vector2 finalTouchPosition;

    private bool mousePressed;
    private float swipeAngle = 0;

    private Gem otherGem;

    public enum GemType { green, blue, pink, red, yellow, star, shell, bomb, dice}
    public GemType type;

    public bool isMatched;

    [HideInInspector]
    public Vector2Int previousPosition; // Vị trí của gạch trước đó (dùng để trả về vị trí cũ nếu gạch không tạo match)

    public GameObject destroyEffect;

    public int blastSize = 2;

    public int scoreValue = 10;
    void Update()
    {
        if (Vector2.Distance(transform.position, posIndex) > .01f)
        {
            transform.position = Vector2.Lerp(transform.position, posIndex, board.gemSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = new Vector3(posIndex.x, posIndex.y, 0f);
            board.allGems[posIndex.x, posIndex.y] = this;
        }


        if (mousePressed && Input.GetMouseButtonUp(0))
        {
            mousePressed = false;
            if(board.currentState == Board.BoardState.move && board.roundMan.roundTime > 0)
            {
                Debug.Log(" ngang " + board.width);
                Debug.Log(" doc " + board.height);
                finalTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if(finalTouchPosition.x > -0.43f &&
                    finalTouchPosition.x < (float)board.width - 0.6 &&
                    finalTouchPosition.y < (float)board.height - 0.6 && 
                    finalTouchPosition.y > - 0.47f )
                {

                    
                    CalculateAngle();
                }
                
            }

        }
    }
    public void SetUpGem(Vector2Int pos, Board theBoard)
    {
        posIndex = pos;
        board = theBoard;
    }

    private void OnMouseDown()
    {
        if(board.currentState == Board.BoardState.move && board.roundMan.roundTime > 0)
        {
            firstTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePressed = true;
        }

    }

    private void CalculateAngle()
    {
        swipeAngle = Mathf.Atan2(finalTouchPosition.y - firstTouchPosition.y, finalTouchPosition.x - firstTouchPosition.x);
        swipeAngle = swipeAngle * 180 / Mathf.PI;
        Debug.Log(swipeAngle);

        if(Vector3.Distance(firstTouchPosition, finalTouchPosition) > .2f)
        {
            MovePiece();
        }

    }

    private void MovePiece()
    {
        previousPosition = posIndex;
        if (swipeAngle < 45 && swipeAngle > -45 && posIndex.x < board.width - 1)
        {
            // Right 
            otherGem = board.allGems[posIndex.x + 1, posIndex.y];
            otherGem.posIndex.x--;
            posIndex.x++;
        }
        else if (swipeAngle > 45 && swipeAngle <= 135 && posIndex.y < board.height - 1)
        {
            // Up
            otherGem = board.allGems[posIndex.x, posIndex.y + 1];
            otherGem.posIndex.y--;
            posIndex.y++;
        }
        else if (swipeAngle < -45 && swipeAngle >= -135 && posIndex.y > 0)
        {
            // Down
            otherGem = board.allGems[posIndex.x, posIndex.y - 1];
            otherGem.posIndex.y++;
            posIndex.y--;
        }
        else if (swipeAngle > 135 || swipeAngle < -135 && posIndex.x > 0)
        {
            // Left
            otherGem = board.allGems[posIndex.x - 1, posIndex.y];
            otherGem.posIndex.x++;
            posIndex.x--;
        }
        board.allGems[posIndex.x, posIndex.y] = this;
        board.allGems[otherGem.posIndex.x, otherGem.posIndex.y] = otherGem;

        StartCoroutine(CheckMoveCo());
    }

    public IEnumerator CheckMoveCo() // hàm check xem có tạo match hay không nếu không thì trả về vị trí cũ
    {
        board.currentState = Board.BoardState.wait;
        yield return new WaitForSeconds(.5f);

        board.matchFind.FindAllMatches();

        if(otherGem != null)
        {
            if (!isMatched && !otherGem.isMatched)
            {
                otherGem.posIndex = posIndex;
                posIndex = previousPosition;

                board.allGems[posIndex.x, posIndex.y] = this;
                board.allGems[otherGem.posIndex.x, otherGem.posIndex.y] = otherGem;
                yield return new WaitForSeconds(.5f);
                board.currentState = Board.BoardState.move;
            }
            else
            {
                board.DestroyMatches();
            }
        }
    }
    
}
