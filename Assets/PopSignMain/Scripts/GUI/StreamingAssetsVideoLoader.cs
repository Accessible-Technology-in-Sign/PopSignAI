using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class StreamingAssetsVideoLoader : MonoBehaviour
{
    [SerializeField] private string filePath;

    private void Awake()
    {
        this.GetComponent<VideoPlayer>().url = Application.streamingAssetsPath + filePath;
    }
}
