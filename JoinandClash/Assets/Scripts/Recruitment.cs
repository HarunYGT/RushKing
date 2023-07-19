using UnityEngine;
using System.Linq;

public class Recruitment : MonoBehaviour
{
    private void OnCollisionEnter(Collision other){
        if(other.collider.CompareTag("add")){
            Debug.Log("Diğer karakter eklendi.");
            PlayerManager.Instance.rbList.Add(other.collider.GetComponent<Rigidbody>());

            other.transform.parent = null;

            other.transform.parent = PlayerManager.Instance.transform;

            if(!other.collider.gameObject.GetComponent<Recruitment>()){
                other.collider.gameObject.AddComponent<Recruitment>();
            }

            other.collider.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material =
                PlayerManager.Instance.rbList.ElementAt(0).transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material;
        }
    }
}
