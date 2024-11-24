using UnityEngine;
using UnityEngine.UI; 

public class Classroom : MonoBehaviour
{
    public ClassModel classModel;

    [SerializeField] Text className; 

    public void Setup(ClassModel model) {
        classModel = model; 
    }

    public void Next() {
        // Skip teacher resoonse
    }
}
