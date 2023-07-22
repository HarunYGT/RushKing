using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossManager : MonoBehaviour
{
    public List<GameObject> Enemies = new List<GameObject>();
    public Animator BossAnimator;
    public static BossManager Instance;
    private float attackmode;
    public bool LockOnTarget,BossIsAlive;
    private Transform target;
    public Slider HealthBar;
    public TextMeshProUGUI Health_bar_amount;
    public int Health;
    public GameObject Particle_Death;
    public float maxDistance,minDistance;


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
        var enemy = GameObject.FindGameObjectsWithTag("add");
        foreach (var stickMan in enemy)
            Enemies.Add(stickMan);

        BossAnimator.GetComponent<Animator>();

        BossIsAlive = true;

        HealthBar.value = HealthBar.maxValue = Health = 150;

        Health_bar_amount.text = Health.ToString();

    }

    // Update is called once per frame
    void Update()
    {

        HealthBar.transform.rotation = Quaternion.Euler(HealthBar.transform.rotation.x,0f,HealthBar.transform.rotation.z);
        if(Enemies.Count > 0)
        {
            foreach (var stickMan in Enemies)
            {
            var StickManDistance = stickMan.transform.position - transform.position;

            if(StickManDistance.sqrMagnitude <= maxDistance*maxDistance  && !LockOnTarget)
                {
                target = stickMan.transform;
                BossAnimator.SetBool("fight",true);

                transform.position = Vector3.MoveTowards(transform.position,target.position,1f*Time.deltaTime);
                }
            if(StickManDistance.sqrMagnitude <= minDistance*minDistance)
                {
                LockOnTarget = true;

                }
            }
        }
        
        if(LockOnTarget)
        {
            var bossRotation = new Vector3(target.position.x,transform.position.y,target.position.z) - transform.position;
     
            transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(bossRotation,Vector3.up),10f*Time.deltaTime);

            for(int i = 0; i<Enemies.Count; i++)
                if(!Enemies.ElementAt(i).GetComponent<MemberManager>().member)
                    Enemies.RemoveAt(i);
        }

        if(Enemies.Count == 0){
            BossAnimator.SetBool("fight",false);
            BossAnimator.SetFloat("attackmode",4f);
        }


        if(Health <= 0 && BossIsAlive){
            gameObject.SetActive(false);
            BossIsAlive = false;
            Instantiate(Particle_Death,transform.position,Quaternion.identity);
        }
        
    }
    public void ChangeTheBossAttackMode()
    {
        BossAnimator.SetFloat("attackmode",Random.Range(2,4));
    }
}
