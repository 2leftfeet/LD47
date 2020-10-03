using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    PhysicsController player;
    

    List<InputAction> testList;


    float previousX = 0.0f;

    bool createHorizontal = false;
    bool createJump = false;
    void Awake()
    {
        player = GetComponent<PhysicsController>();
        testList = new List<InputAction>();
    }

    void Update()
    {
        float currentX = Input.GetAxisRaw("Horizontal");
        if(Mathf.Abs(previousX - currentX) > 0.0f)
        {
            createHorizontal = true;   
        }

        previousX = currentX;
    }

    void FixedUpdate()
    {
        if(createHorizontal)
        {
            var horizontalAction = new HorizontalAction(previousX, player);
            testList.Add(horizontalAction);
            Debug.Log(testList.Count);
            horizontalAction.PlayAction();
            createHorizontal = false;
        }
    }
}
