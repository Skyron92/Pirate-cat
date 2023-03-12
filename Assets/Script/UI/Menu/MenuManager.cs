using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private bool isApplication;
    [SerializeField] private GameObject webglMenu;
    [SerializeField] private GameObject appMenu;
    
    void Start() {
        webglMenu.SetActive(!isApplication);
        appMenu.SetActive(isApplication);
    }
}
