using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class HowToPlay : MonoBehaviour
{
    [SerializeField] private bool isInGame = false;
    public GameObject page1;
    public GameObject page2;
    public GameObject page3;
    public GameObject page4;
    public GameObject page5;
    public GameObject page6;

    int currentPage;

    void Start()
    {
        currentPage = 1;
    }

    public void NavNext()
    {
        switch (currentPage)
        {
            case 1:
                page1.SetActive(false);
                page2.SetActive(true);
                currentPage += 1;
                break;
            case 2:
                page2.SetActive(false);
                page3.SetActive(true);
                currentPage += 1;
                break;
            case 3:
                page3.SetActive(false);
                page4.SetActive(true);
                currentPage += 1;
                break;
            case 4:
                page4.SetActive(false);
                page5.SetActive(true);
                currentPage += 1;
                break;
            case 5:
                page5.SetActive(false);
                page6.SetActive(true);
                currentPage += 1;
                break;
            case 6:
                if (isInGame)
                {
                    currentPage = 1;
                    page6.SetActive(false);
                    page1.SetActive(true);
                    ReplaceNextTexture();
                    this.GetComponent<AnimationManager>().CloseMenu();
                }
                else
                {
                    GoToLevelSelection();
                }
                return;
            default:
                break;
        }
        ReplaceNextTexture();
    }

    private void ReplaceNextTexture()
    {
        RawImage curtCircle = (RawImage)GameObject.Find("Circle" + currentPage).GetComponent<RawImage>();
        RawImage prevCircle = (RawImage)GameObject.Find("Circle" + (currentPage == 1 ? 6 : currentPage - 1)).GetComponent<RawImage>();

        Texture unfilledTexture = prevCircle.texture;
        Texture filledTexture = curtCircle.texture;
        curtCircle.texture = unfilledTexture;
        prevCircle.texture = filledTexture;
    }

    public void NavBack()
    {
        switch (currentPage)
        {
            case 1:
                if (isInGame)
                {
                    this.GetComponent<AnimationManager>().CloseMenu();
                }
                else
                {
                    SceneManager.LoadScene("opening");

                }
                return;
            case 2:
                page2.SetActive(false);
                page1.SetActive(true);
                currentPage -= 1;
                break;
            case 3:
                page3.SetActive(false);
                page2.SetActive(true);
                currentPage -= 1;
                break;
            case 4:
                page4.SetActive(false);
                page3.SetActive(true);
                currentPage -= 1;
                break;
            case 5:
                page5.SetActive(false);
                page4.SetActive(true);
                currentPage -= 1;
                break;
            case 6:
                page6.SetActive(false);
                page5.SetActive(true);
                currentPage -= 1;
                break;
            default:
                break;
        }

        RawImage curtCircle = (RawImage)GameObject.Find("Circle" + currentPage).GetComponent<RawImage>();
        RawImage prevCircle = (RawImage)GameObject.Find("Circle" + (currentPage + 1)).GetComponent<RawImage>();

        Texture unfilledTexture = prevCircle.texture;
        Texture filledTexture = curtCircle.texture;
        curtCircle.texture = unfilledTexture;
        prevCircle.texture = filledTexture;
    }

    public void GoToLevelSelection()
    {
        if (currentPage > 1)
        {
            RawImage firstCircle = (RawImage)GameObject.Find("Circle1").GetComponent<RawImage>();
            RawImage currentCircle = (RawImage)GameObject.Find("Circle" + currentPage).GetComponent<RawImage>();

            Texture unfilledTexture = firstCircle.texture;
            Texture filledTexture = currentCircle.texture;
            currentCircle.texture = unfilledTexture;
            firstCircle.texture = filledTexture;
        }

        CustomizeLevelManager.switchOff();
        SceneManager.LoadScene("map");
    }
}
