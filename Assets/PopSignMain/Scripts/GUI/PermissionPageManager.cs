using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Permissions))]
public class PermissionPageManager : MonoBehaviour
{
    public static PermissionPageManager Instance;
    public static bool LoadPage2;

    [SerializeField] private GameObject Page1;
    [SerializeField] private GameObject Page2;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        Page1.gameObject.SetActive(!LoadPage2);
        Page2.gameObject.SetActive(LoadPage2);

        if (LoadPage2)
        {
            gameObject.GetComponent<Permissions>().RunBackgroundPermissionChecker();
        }
    }

    public void SwitchToPage2()
    {
        LoadPage2 = true;
        Page1.gameObject.SetActive(false);
        Page2.gameObject.SetActive(true);
    }
}
