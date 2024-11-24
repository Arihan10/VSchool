using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class Menu : MonoBehaviour
{
    [SerializeField] TMP_InputField subject, level; 

    [SerializeField] GameObject playerRig, menuCam, XRSim;

    float ogMoveSpeed;

    bool sim; 

    private void Start() {
        // playerRig.SetActive(false);
        // menuCam.SetActive(true); 
        ogMoveSpeed = playerRig.GetComponentInChildren<DynamicMoveProvider>().moveSpeed; 
        playerRig.GetComponentInChildren<DynamicMoveProvider>().moveSpeed = 0;

        sim = XRSim.activeSelf;
        XRSim.SetActive(false); 
    }

    public void Generate() {
        StartCoroutine(GenerateSchool()); 
    }

    IEnumerator GenerateSchool() {
        yield return StartCoroutine(SchoolGenerator.instance.GenerateSchool(subject.text, level.text));

        gameObject.SetActive(false);
        // playerRig.SetActive(true);
        // menuCam.SetActive(false); 
        XRSim.SetActive(sim); 
        playerRig.GetComponentInChildren<DynamicMoveProvider>().moveSpeed = ogMoveSpeed; 
    }
}
