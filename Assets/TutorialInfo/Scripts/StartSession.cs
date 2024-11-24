using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

public class StartSession : MonoBehaviour {
    private ConvResponse response;

    void Start() {
        StartCoroutine(PostApiRequest());
    }

    IEnumerator PostApiRequest() {
        int session_id = 1732444521;
        int class_id = 0;
        string apiUrl = $"http://195.242.13.194:8001/session/{session_id}/start_class/{class_id}";
        Debug.Log($"Requesting URL: {apiUrl}");

        using (UnityWebRequest webRequest = new UnityWebRequest(apiUrl, "GET")) {
            // Add these lines to handle the response
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success) {
                try {
                    string responseText = webRequest.downloadHandler.text;
                    Debug.Log($"Raw response: {responseText}");

                    response = JsonConvert.DeserializeObject<ConvTeacherResponse>(responseText);

                    if (response != null) {
                        Debug.Log($"Parsed content: {response.content}");
                        Debug.Log($"Role: {response.role}");
                    }
                    else {
                        Debug.LogError("Response deserialized to null");
                    }
                }
                catch (JsonException e) {
                    Debug.LogError($"JSON Parsing Error: {e.Message}\nStack Trace: {e.StackTrace}");
                }
                catch (Exception e) {
                    Debug.LogError($"General Error: {e.Message}\nStack Trace: {e.StackTrace}");
                }
            }
            else {
                Debug.LogError($"Request Error: {webRequest.error}\nResponse Code: {webRequest.responseCode}");
            }
        }
    }

    // Helper method to get the response content
    public string GetResponseContent() {
        return response?.content;
    }

    // Helper method to get the response role
    public string GetResponseRole() {
        return response?.role;
    }
}