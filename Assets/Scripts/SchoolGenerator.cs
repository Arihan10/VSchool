using UnityEngine;

public class SchoolGenerator : MonoBehaviour
{
    [SerializeField] GameObject cubePrefab; 
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void GenerateSchool() {
        GameObject ground = Instantiate(cubePrefab, Vector3.zero, Quaternion.identity); 
        // ground.transform.localScale = new Vector3(100f, 100f, )
    }
}
