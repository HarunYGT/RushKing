using System.Collections;
using System.Collections.Generic;
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
        if(Health <= 0 && BossIsAlive){
            gameObject.SetActive(false);
            BossIsAlive = false;
        }
        
    }
}
