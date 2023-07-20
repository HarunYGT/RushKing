using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageTheBoss : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<Rigidbody>().isKinematic = true;
    }

    private void OnTriggerEnter(Collider other){
        if(other.CompareTag("Boss")){
            BossManager.Instance.Health--;
            BossManager.Instance.Health_bar_amount.text = BossManager.Instance.Health.ToString();
            BossManager.Instance.HealthBar.value = BossManager.Instance.Health--;
        }
    }
}
