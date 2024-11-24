using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;


public class CommunicateToStudents : MonoBehaviour {
    public ConvResponse response;
    private string apiUrl = "http://195.242.13.194:8001/communicate_to_student";

    void Start() {
        StartCoroutine(PostApiRequest());
    }

    private class RequestData {
        public string session_id { get; set; }
        public string student_id { get; set; }
        public string class_id { get; set; }
        public string message { get; set; }
    }

    IEnumerator PostApiRequest() {
        var requestData = new RequestData {
            session_id = "1732444521",
            student_id = "0",
            class_id = "0",
            message = "What is teacher saying?"
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
                    response = JsonConvert.DeserializeObject<ConvResponse>(responseText);
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