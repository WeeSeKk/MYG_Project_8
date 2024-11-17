using UnityEngine;
using DG.Tweening;
using CurvedUI;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class IHMManager : MonoBehaviour
{
    [SerializeField] GameObject carDemo;
    [SerializeField] Canvas carDemoCanvas;
    [SerializeField] CurvedUISettings curvedUISettings;
    [SerializeField] Button returnButton;
    [SerializeField] List<TMP_Text> musicNames;
    [SerializeField] public Grid grid;
    [SerializeField] TMP_Text timerText;
    [SerializeField] TMP_Text musicLengthText;
    [SerializeField] Slider musicSlider;
    public GameObject[,] gridArray;
    float timer;
    float musicLength;
    bool music;
    int uiPosition = 35;
    bool playlistSetup;
    int gridWidth = 1;
    int gridHeight = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        carDemoCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < 0 && music)
        {
            timer -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(timer / 60);
            int seconds = Mathf.FloorToInt(timer % 60);

            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        else
        {
            music = false;
        }
    }

    void OnEnable()
    {
        gridArray = new GameObject[gridWidth, gridHeight];
        EventManager.musicChange += UpdatePlaylistGrid;
        EventManager.musicSlider += UpdateMusicSlider;
    }

    public void OnEnterHoverCar()
    {
        carDemoCanvas.enabled = true;
    }
    public void OnExitHoverCar()
    {
        carDemoCanvas.enabled = false;
    }

    public void OnLoading()
    {
        int startValue = curvedUISettings.Angle;
        int endValue = 360;

        DOTween.To(() => startValue, x =>
        {
            curvedUISettings.Angle = x;

        }, endValue, 2f).SetEase(Ease.OutCirc);
    }

    public void ScrollImage(GameObject image)
    {
        Vector3 startValue = image.transform.localPosition;

        if (startValue.x < uiPosition)
        {
            Vector3 endValue = new Vector3(uiPosition, image.transform.localPosition.y, image.transform.localPosition.z);
            DOTween.To(() => startValue, x =>
            {
                image.transform.localPosition = x;

            }, endValue, 2f).SetEase(Ease.OutCirc);
            returnButton.gameObject.SetActive(false);
        }
        else
        {
            Vector3 endValue = new Vector3(15, image.transform.localPosition.y, image.transform.localPosition.z);
            DOTween.To(() => startValue, x =>
            {
                image.transform.localPosition = x;
            }, endValue, 2f).SetEase(Ease.OutCirc);
            returnButton.gameObject.SetActive(true);
        }
    }

    public void HideShowMainButton(GameObject gameObject)
    {
        Vector3 startValue = gameObject.transform.localPosition;

        if (gameObject.transform.localPosition.x == 0)
        {
            Vector3 endValue = new Vector3(-20, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);
            DOTween.To(() => startValue, x =>
            {
                gameObject.transform.localPosition = x;

            }, endValue, 2f).SetEase(Ease.OutCirc);
        }
        else
        {
            Vector3 endValue = new Vector3(0, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);
            DOTween.To(() => startValue, x =>
            {
                gameObject.transform.localPosition = x;

            }, endValue, 2f).SetEase(Ease.OutCirc);
        }
    }

    public void UpdatePlaylist(string name)
    {
        if (playlistSetup)
        {
            Vector3 bottomPosition = new Vector3(0, -36, 0);

            for (int i = 0; i < musicNames.Count; i++)
            {
                if (musicNames[i].gameObject.transform.localPosition.y == -4)
                {
                    musicNames[i].gameObject.transform.localPosition = bottomPosition;
                }

                Vector3 startPosition = musicNames[i].gameObject.transform.localPosition;
                Vector3 endPosition = new Vector3(0, musicNames[i].gameObject.transform.localPosition.y + 8, 0);

                if (startPosition.y == -36)
                {
                    musicNames[i].text = name;
                }

                musicNames[i].gameObject.transform.DOLocalMoveY(musicNames[i].gameObject.transform.localPosition.y + 8, 2f).SetEase(Ease.OutCirc);

                if (endPosition.y == -28 || endPosition.y == -20)
                {
                    Color startColor = musicNames[i].color;
                    Color endColor = new Color(1, 1, 1, musicNames[i].color.a + 0.5f);
                    float duration = 2f;
                    int index = i;

                    DOTween.To(() => startColor, x =>
                    {
                        startColor = x;
                        musicNames[index].color = startColor;
                    }, endColor, duration).SetEase(Ease.OutCirc);
                }
                else
                {
                    Color startColor = musicNames[i].color;
                    Color endColor = new Color(1, 1, 1, musicNames[i].color.a - 0.5f);
                    float duration = 2f;
                    int index = i;

                    DOTween.To(() => startColor, x =>
                    {
                        startColor = x;
                        musicNames[index].color = startColor;
                    }, endColor, duration).SetEase(Ease.OutCirc);
                }
            }
        }
        else
        {

            playlistSetup = true;
        }
    }

    void UpdatePlaylistGrid(string name, bool next)
    {
        DOTween.Clear();
        if (playlistSetup)
        {
            if (next)
            {
                for (int i = gridHeight - 1; i >= 0; i --)
                {
                    if (gridArray[0, 0] == null && gridArray[0, gridHeight - 1] != null)
                    {
                        GameObject topText = gridArray[0, gridHeight - 1];
                        Vector3 bottomPosition = grid.CellToWorld(new Vector3Int(0, 0));
                        topText.transform.position = bottomPosition;
                        gridArray[0, 0] = topText;
                        gridArray[0, gridHeight - 1] = null;
                    }
                    if (i < gridHeight && gridArray[0, i] != null && gridArray[0, i + 1] == null)
                    {
                        if (i + 1 == 1)
                        {
                            TMP_Text tMP_Text = gridArray[0, i].GetComponent<TMP_Text>();
                            tMP_Text.text = name;
                        }
                        if (i < 2 && i > -1)
                        {
                            TMP_Text tMP_Text = gridArray[0, i].GetComponent<TMP_Text>();
                            Color startColor = tMP_Text.color;
                            Color endColor = new Color(1, 1, 1, tMP_Text.color.a + 0.5f);
                            float duration = 2f;

                            DOTween.To(() => startColor, x =>
                            {
                                startColor = x;
                                tMP_Text.color = startColor;
                            }, endColor, duration).SetEase(Ease.OutCirc);
                        }
                        else
                        {
                            TMP_Text tMP_Text = gridArray[0, i].GetComponent<TMP_Text>();
                            Color startColor = tMP_Text.color;
                            Color endColor = new Color(1, 1, 1, tMP_Text.color.a - 0.5f);
                            float duration = 2f;

                            DOTween.To(() => startColor, x =>
                            {
                                startColor = x;
                                tMP_Text.color = startColor;
                            }, endColor, duration).SetEase(Ease.OutCirc);
                        }

                        GameObject text = gridArray[0, i];
                        Vector3 worldPosition = grid.CellToWorld(new Vector3Int(0, i + 1));
                        text.transform.DOMove(worldPosition, 2f, false).SetEase(Ease.OutCirc);
                        gridArray[0, i + 1] = text;
                        gridArray[0, i] = null;
                    }

                }
            }
            else
            {
                for (int i = 0; i <= gridHeight - 1; i ++)
                {
                    if (gridArray[0, 0] != null && gridArray[0, gridHeight - 1] == null)
                    {
                        GameObject bottomText = gridArray[0, 0];
                        Vector3 topPosition = grid.CellToWorld(new Vector3Int(0, gridHeight - 1));
                        bottomText.transform.position = topPosition;
                        gridArray[0, gridHeight - 1] = bottomText;
                        gridArray[0, 0] = null;
                    }
                    if (i > 0 && gridArray[0, i] != null && gridArray[0, i - 1] == null)
                    {
                        if (i - 1 == 3)
                        {
                            TMP_Text tMP_Text = gridArray[0, i].GetComponent<TMP_Text>();
                            tMP_Text.text = name;
                        }
                        if (i > 2)
                        {
                            TMP_Text tMP_Text = gridArray[0, i].GetComponent<TMP_Text>();
                            Color startColor = tMP_Text.color;
                            Color endColor = new Color(1, 1, 1, tMP_Text.color.a + 0.5f);
                            float duration = 2f;

                            DOTween.To(() => startColor, x =>
                            {
                                startColor = x;
                                tMP_Text.color = startColor;
                            }, endColor, duration).SetEase(Ease.OutCirc);
                        }
                        else
                        {
                            TMP_Text tMP_Text = gridArray[0, i].GetComponent<TMP_Text>();
                            Color startColor = tMP_Text.color;
                            Color endColor = new Color(1, 1, 1, tMP_Text.color.a - 0.5f);
                            float duration = 2f;

                            DOTween.To(() => startColor, x =>
                            {
                                startColor = x;
                                tMP_Text.color = startColor;
                            }, endColor, duration).SetEase(Ease.OutCirc);
                        }
                        
                        GameObject text = gridArray[0, i];
                        Vector3 worldPosition = grid.CellToWorld(new Vector3Int(0, i - 1));
                        text.transform.DOMove(worldPosition, 2f, false).SetEase(Ease.OutCirc);
                        gridArray[0, i - 1] = text;
                        gridArray[0, i] = null;
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < musicNames.Count; i++)
            {
                gridArray[0, i] = musicNames[i].gameObject;
                Vector3 worldPosition = grid.CellToWorld(new Vector3Int(0, i));
                musicNames[i].gameObject.transform.position = worldPosition;

                switch (i)
                {
                    case 3:
                        TMP_Text tMP_Text0 = gridArray[0, i].GetComponent<TMP_Text>();
                        tMP_Text0.text = AudioManager.instance.playlist[AudioManager.instance.playlist.Count - 1].name;
                        break;

                    case 2:
                        TMP_Text tMP_Text1 = gridArray[0, i].GetComponent<TMP_Text>();
                        tMP_Text1.text = AudioManager.instance.playlist[0].name;
                        break;

                    case 1:
                        TMP_Text tMP_Text2 = gridArray[0, i].GetComponent<TMP_Text>();
                        tMP_Text2.text = AudioManager.instance.playlist[1].name;
                        break;

                    default:

                        break;
                }
            }
            playlistSetup = true;
        }
    }

    void UpdateMusicSlider(float musicTime)
    {
        musicLength = musicTime;
        musicSlider.maxValue = musicLength;
        music = true;

        DOTween.To(() => 0f, x => UpdateTimerDisplay(x), musicLength, musicLength).SetEase(Ease.Linear);

        int minutes = Mathf.FloorToInt(musicLength / 60);
        int seconds = Mathf.FloorToInt(musicLength % 60);

        string timeFormatted = string.Format("{0:00}:{1:00}", minutes, seconds);
        musicLengthText.text = timeFormatted;
    }

    void UpdateTimerDisplay(float currentTime)
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);

        string timeFormatted = string.Format("{0:00}:{1:00}", minutes, seconds);
        timerText.text = timeFormatted;

        musicSlider.value = currentTime;
    }

    public void test()
    {
        Debug.Log("sfdghdsfg");
    }
}