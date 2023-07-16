using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;

public class Permissions : MonoBehaviour
{
    public static bool IsLookingForPermissionsToBeEnabled = false;
    public void CheckPermissionsOpenningScene()
    {
        if (!PlayerPrefs.HasKey("firstTime")) //return true if the key exist
        {
            Debug.Log("First time in the game.");
            PlayerPrefs.SetInt("firstTime", 0);
            SceneManager.LoadScene("permission");
        }
        else
        {
            Debug.Log("It is not the first time in the game.");
            CheckPermissions();
        }
    }

    public void CheckPermissions()
    {
#if UNITY_IOS
            StartCoroutine(PermissionCheckerIOS());
#elif UNITY_ANDROID
            PermissionCheckerAndroid();
#endif
    }

    public void RunBackgroundPermissionChecker()
    {
        if (!IsLookingForPermissionsToBeEnabled)
        {
            IsLookingForPermissionsToBeEnabled = true;
            StartCoroutine(CheckIfPermissionGranted());
        }
    }

    IEnumerator PermissionCheckerIOS()
    {
        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);

        if (Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            Callbacks_PermissionGranted("");
        }
        else
        {
            Callbacks_PermissionDenied("");
        }
    }

    void PermissionCheckerAndroid()
    {
        if (Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            Callbacks_PermissionGranted("");
        }
        else
        {
            Debug.LogWarning("User Doesn't have permission, will ask for permission.");
            PermissionCallbacks callbacks = new PermissionCallbacks();
            callbacks.PermissionGranted += Callbacks_PermissionGranted;
            callbacks.PermissionDenied += Callbacks_PermissionDenied;
            callbacks.PermissionDeniedAndDontAskAgain += Callbacks_PermissionDenied;
            Permission.RequestUserPermission(Permission.Camera, callbacks);
        }
    }

    private void Callbacks_PermissionDenied(string obj)
    {
        Debug.LogWarning("Permission denied by user, taking to page 2.");
        if (SceneManager.GetActiveScene().name == "permission")
        {
            PermissionPageManager.Instance.SwitchToPage2();
            RunBackgroundPermissionChecker();
        }
        else
        {
            PermissionPageManager.LoadPage2 = true;
            SceneManager.LoadScene("permission");
        }
    }

    private void Callbacks_PermissionGranted(string obj)
    {
        Debug.Log("Permission granted");
        SceneManager.LoadScene("howtoplay");
    }


    IEnumerator CheckIfPermissionGranted()
    {
        while (SceneManager.GetActiveScene().name == "permission")
        {
            Debug.Log("Checking Permissions");
            yield return new WaitForSeconds(3f);
            CheckPermissions();
        }
    }


}
