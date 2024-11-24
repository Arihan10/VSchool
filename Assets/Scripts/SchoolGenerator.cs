using System.Collections;
using UnityEngine;

public class SchoolGenerator : MonoBehaviour
{
    public SchoolGenerator instance; 
    
    [SerializeField] Classroom[] classes;

    private void Awake() {
        instance = this; 
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(GenerateSchool("Computer Engineering", "1st Year University")); 
    }

    public IEnumerator GenerateSchool(string subject, string grade) {
        yield return StartCoroutine(CreateSession.instance.PostApiRequest(subject, grade)); 

        // CreateSession.instance.session; 
        Debug.Log(CreateSession.instance.session.classes[0].teacher.name); 

        for (int i = 0; i < classes.Length; ++i) {
            classes[i].Setup(CreateSession.instance.session.classes[i]); 
        }
    }
}
