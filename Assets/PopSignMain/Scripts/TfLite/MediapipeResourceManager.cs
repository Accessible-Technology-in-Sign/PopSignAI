using Mediapipe;
using Mediapipe.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediapipeResourceManager : MonoBehaviour
{
    public static MediapipeResourceManager Instance;

    public ResourceManager resourceManager;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            resourceManager = new StreamingAssetsResourceManager();
        }
    }
}
