using UnityEngine;

public class Player : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Classroom") {
            other.GetComponent<Classroom>().InitSession(); 
        }
    }

    /*private void OnTriggerExit(Collider other) {
        Debug.Log("TRIGGER EXIT YES!!!"); 
    } */
}
