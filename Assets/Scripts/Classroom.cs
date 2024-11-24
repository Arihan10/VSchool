using System.Collections;
using UnityEngine;
using UnityEngine.UI; 

public class Classroom : MonoBehaviour
{
    public ClassModel classModel;

    [SerializeField] Text className, teacherResponse;

    bool started = false; 

    public void Setup(ClassModel model) {
        classModel = model;

        className.text = classModel.name; 
    }

    public void InitSession() {
        if (started) return;

        StartCoroutine(StartSessionRequest());
        started = true; 
    }

    IEnumerator StartSessionRequest() {
        yield return StartCoroutine(StartSession.instance.PostApiRequest(CreateSession.instance.session.id, classModel.id)); 

        teacherResponse.text = StartSession.instance.response.content; 
        Debug.Log(teacherResponse.text); 
    }

    public void Next() {
        StartCoroutine(TalkTeacherRequest("skip"));
    }

    public void TalkTeacher() {
        StartCoroutine(TalkTeacherRequest("Thank you")); 
    }

    IEnumerator TalkTeacherRequest(string message) {
        yield return StartCoroutine(CommunicateToTeacher.instance.PostApiRequest(CreateSession.instance.session.id.ToString(), classModel.id.ToString(), message));

        teacherResponse.text = CommunicateToTeacher.instance.response.content; 
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.RightShift)) {
            Next(); 
        } else if (Input.GetKeyDown(KeyCode.Comma)) {
            TalkTeacher(); 
        }
    }
}
