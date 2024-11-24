using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json; 


public class CommunicateToTeacher : MonoBehaviour {
    public static CommunicateToTeacher instance; 

    public ConvTeacherResponse response; 
    private string apiUrl = "http://195.242.13.194:8001/communicate_to_teacher";

    private void Awake() {
        instance = this; 
    }

    void Start() {
        // StartCoroutine(PostApiRequest());
    }

    private class RequestData {
        public string session_id { get; set; }
        public string class_id { get; set; }
        public string message { get; set; }
    }

    public IEnumerator PostApiRequest(string _session_id, string _class_id, string _message) {
        var requestData = new RequestData {
            session_id = _session_id,
            class_id = _class_id,
            message = _message
        };

        string jsonData = JsonConvert.SerializeObject(requestData);

        using (UnityWebRequest webRequest = new UnityWebRequest(apiUrl, "POST")) {
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);
            webRequest.uploadHandler = new UploadHandlerRaw(jsonToSend);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success) {
                try {
                    string responseText = webRequest.downloadHandler.text;
                    response = JsonConvert.DeserializeObject<ConvTeacherResponse>(responseText);
                    Debug.Log(response.content);

                }
                catch (JsonException e) {
                    Debug.LogError($"JSON Parsing Error: {e.Message}");
                }
                catch (Exception e) {
                    Debug.LogError($"General Error: {e.Message}");
                }
            }
            else {
                Debug.LogError($"Request Error: {webRequest.error}");
            }
        }
    }
}