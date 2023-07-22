using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public bool MoveByTouch,gameState,attackToTheBoss;
    private Vector3 direction;
    public List<Rigidbody> rbList = new List<Rigidbody>();
    [SerializeField] private float runSpeed,velocity,swipeSpeed,roadSpeed;
    [SerializeField] private Transform road;

    public static PlayerManager Instance;

    void Awake(){
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }
    void Start()
    {
        rbList.Add(transform.GetChild(0).GetComponent<Rigidbody>());
        gameState = true;

    }

    // Update is called once per frame
    void Update()
    {
       if(gameState)
       {
         if (Input.GetMouseButtonDown(0))
        {
            MoveByTouch = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            MoveByTouch = false;
        }
        
        if(MoveByTouch){
            direction.x = Mathf.Lerp(direction.x,Input.GetAxis("Mouse X"), Time.deltaTime * runSpeed);
           
            direction = Vector3.ClampMagnitude(direction,1f);
           
            road.position = new Vector3(0f,0f,Mathf.SmoothStep(road.position.z,-100f,Time.deltaTime * roadSpeed));

            foreach (var playerAnim in rbList)
            {
                playerAnim.GetComponent<Animator>().SetFloat("run",1f);
            }
            
        }
        else
            foreach (var playerAnim in rbList)
            {
                playerAnim.GetComponent<Animator>().SetFloat("run",0f);
            }
     
        foreach (var stickman_rb in rbList)
            {
                if (stickman_rb.velocity.magnitude > 0.5f)
                {
                    stickman_rb.rotation = Quaternion.Slerp(stickman_rb.rotation,Quaternion.LookRotation(stickman_rb.velocity), Time.deltaTime * velocity );
                }
                else
                {
                    stickman_rb.rotation = Quaternion.Slerp(stickman_rb.rotation,Quaternion.identity, Time.deltaTime * velocity );
                }
            }
       }
       else{
        if(!BossManager.Instance.BossIsAlive){
            foreach (var stickMan in rbList)
            {
                stickMan.GetComponent<Animator>().SetFloat("attackmode",4f);
            }
        }
       }
    }
    private void FixedUpdate()
    {
      if(gameState)
      {
          if (MoveByTouch)
            {
                Vector3 displacement = new Vector3(direction.x,0f,0f) * Time.fixedDeltaTime;
            
                foreach (var stickman_rb in rbList)
                    stickman_rb.velocity = new Vector3(direction.x * Time.fixedDeltaTime * swipeSpeed,0f,0f) + displacement;
            }
            else
            {
                foreach (var stickman_rb in rbList)
                    stickman_rb.velocity = Vector3.zero;
            }
      }
    }
}
