using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] float moveSpeed = 8f;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        PrintInstruction();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    void PrintInstruction()
    {
        Debug.Log("Welcome to the game");
        Debug.Log("Move with wasd or arrow keys");
        Debug.Log("Don't bump into things");
    }

    void MovePlayer()
    {
        float xValue = moveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;
        float zValue = moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
        
        transform.Translate(xValue, 0, zValue);
    }
}
