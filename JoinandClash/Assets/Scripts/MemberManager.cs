using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemberManager : MonoBehaviour
{
    public Animator character_animator;
    public GameObject particle_death;
    private Transform Boss;
    public int Health;
    public float MinDistanceOfEnemey, MaxDistanceOfEnemy,moveSpeed;
    public bool member,fight;
    private Rigidbody rb;
    private CapsuleCollider _capsuleCollider;   


    void Start()
    {
        character_animator = GetComponent<Animator>();
        Boss = GameObject.FindWithTag("Boss").transform;
        rb = GetComponent<Rigidbody>();
        _capsuleCollider = GetComponent<CapsuleCollider>();

        Health = 5;

        
    }

    // Update is called once per frame
    void Update()
    {
        var bossDistance = Boss.position - transform.position; 

        if(!fight){
            if(bossDistance.sqrMagnitude <= MaxDistanceOfEnemy*MaxDistanceOfEnemy){
                PlayerManager.Instance.attackToTheBoss = true;
                PlayerManager.Instance.gameState = false;
            }
            if(PlayerManager.Instance.attackToTheBoss && member){
                transform.position= Vector3.MoveTowards(transform.position,Boss.position,moveSpeed*Time.deltaTime);

                var StickManRotation = new Vector3(Boss.position.x,transform.position.y,Boss.position.z) -transform.position;

                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(StickManRotation,Vector3.up),10f*Time.deltaTime);

                character_animator.SetFloat("run",1f);

                rb.velocity = Vector3.zero;
            }
        }
        if(bossDistance.sqrMagnitude <= MinDistanceOfEnemey*MinDistanceOfEnemey)
        {
            fight = true;

            var StickManRotation = new Vector3(Boss.position.x,transform.position.y,Boss.position.z) -transform.position;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(StickManRotation,Vector3.up),10f*Time.deltaTime);
            
            character_animator.SetBool("fight",true); 
            
            MinDistanceOfEnemey = MaxDistanceOfEnemy;   

            rb.velocity = Vector3.zero;
        }
        else{
            fight = false;
        }
    }

    public void ChangeAttackMode()
    {
        character_animator.SetFloat("attackmode",Random.Range(0,3));

        print("Attack changed.");
    }
}
