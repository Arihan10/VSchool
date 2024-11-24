using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;


public class CreateSession : MonoBehaviour {
    public static CreateSession instance; 

    public SessionModel session;
    private string apiUrl = "http://195.242.13.194:8001/session"; 

    void Awake() {
        instance = this; 
    }

    private class RequestData {
        public string subject { get; set; }
        public string grade { get; set; }
    }

    public IEnumerator PostApiRequest(string _subject, string _grade) {
        var requestData = new RequestData {
            subject = _subject,
            grade = _grade
        };

        string jsonData = JsonConvert.SerializeObject(requestData); 

        using (UnityWebRequest webRequest = new UnityWebRequest(apiUrl, "POST")) {
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);
            webRequest.uploadHandler = new UploadHandlerRaw(jsonToSend);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");

            Debug.Log("making request"); 

            yield return webRequest.SendWebRequest();

            Debug.Log("made returned"); 

            if (webRequest.result == UnityWebRequest.Result.Success) {
                try {
                    Debug.Log("success"); 
                    string responseText = webRequest.downloadHandler.text;
                    session = JsonConvert.DeserializeObject<SessionModel>(responseText);
                }
                catch (JsonException e) {
                    Debug.LogError($"JSON Parsing Error: {e.Message}");
                }
                catch (Exception e) {
                    Debug.LogError($"General Error: {e.Message}");
                }
            } else {
                Debug.LogError($"Request Error: {webRequest.error}");
            }
        }
    }

    // Example method to get class schedule
    public ScheduleItem GetClassSchedule(int classId) {
        return session.schedule.GetValueOrDefault(classId.ToString());
    }

    // Example method to get all students in a class
    public List<Student> GetClassStudents(int classId) {
        return session.classes.Find(c => c.id == classId)?.students ?? new List<Student>();
    }

    // Example method to get current unit for a class
    public string GetCurrentUnit(int classId) {
        var classModel = session.classes.Find(c => c.id == classId);
        if (classModel != null && classModel.units.Count > classModel.current_unit_index) {
            return classModel.units[classModel.current_unit_index];
        }
        return null;
    }
}