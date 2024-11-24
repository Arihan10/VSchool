using System.Collections;
using UnityEngine;

public class SchoolGenerator : MonoBehaviour
{
    public static SchoolGenerator instance; 
    
    [SerializeField] Classroom[] classes; 

    public bool sessionCreated = false; 

    private void Awake() {
        instance = this; 
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // StartCoroutine(GenerateSchool("Computer Engineering", "1st Year University")); 
    }

    public IEnumerator GenerateSchool(string subject, string grade) {
        Debug.Log("Generating school... "); 

        yield return StartCoroutine(CreateSession.instance.PostApiRequest(subject, grade)); 

        // CreateSession.instance.session; 
        Debug.Log(CreateSession.instance.session.classes[0].teacher.name); 
        sessionCreated = true; 

        for (int i = 0; i < classes.Length; ++i) {
            classes[i].Setup(CreateSession.instance.session.classes[i]); 
        }
    }
}
