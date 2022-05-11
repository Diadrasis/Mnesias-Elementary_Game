using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class ElementaryManager : MonoBehaviour
{
    #region Variables
    public Instrument[] ins;

    [Space]
    [Header("UI Elements")]
    public GameObject pnlMainInfo;
    public TextMeshProUGUI txtName;
    public Image imgInstrument;
    public Button btnPlaySound;
    public Sprite sprPlaySound;
    public Sprite sprPauseSound;
    public Button btnReloadSound;
    public TextMeshProUGUI txtMainInfo;
    

    [Space]
    [Header("Map Elements")]
    public OnlineMapsMarker[] playerMarker;
    


    [Space]
    [Header("Main Elements")]
    public AudioSource sourceCam;
    #endregion

    #region UnityMethods

    private void Awake()
    {
        LoadFiles("Instrument/");// we load the instrument addressables from the file we have saved them
        
    }
    // Start is called before the first frame update
    void Start()
    {
        pnlMainInfo.SetActive(false);
        playerMarker = new OnlineMapsMarker[ins.Length]; //we want same length when creating markers same as the array of our instruments
        
        for (int i = 0; i < ins.Length; i++)
        {

            playerMarker[i] = OnlineMapsMarkerManager.CreateItem(ins[i].cordinates);

            playerMarker[i].label = ins[i].nameTitle;
            playerMarker[i].texture = ins[i].imgMarker;
            //playerMarker[i].scale = 0.1f;
        }


        sourceCam = Camera.main.gameObject.GetComponent<AudioSource>();//get audio source from main camera
        SubscribeToButtonsMain();
    }
    #endregion

    #region Methods
    void SubscribeToButtonsMain()
    {
        btnPlaySound.gameObject.GetComponent<Image>().sprite = sprPlaySound;//we access the clip from our AudioSource
        
        //btnReloadSound.onClick.AddListener(() => StartCoroutine(loop()));
        //we "assign" the click on the markers and call the method that shows the instrument information 
        for (int i = 0; i < playerMarker.Length; i++)
        {
            playerMarker[i].OnClick += ShowInformation;
            
        }
        
    }

    //when we click on a marker, depending on the marker and its label, to show the appropriate information of the addressable Instrument we have created and assign
    public void ShowInformation(OnlineMapsMarkerBase marker)
    {
        if (!pnlMainInfo.activeInHierarchy)
        {
            EaseInPanel(pnlMainInfo, new Vector3(1, 1, 1), 1f);//start from scale 0 to 1
            MovePanel(pnlMainInfo, new Vector3(pnlMainInfo.transform.position.x , pnlMainInfo.transform.position.y, pnlMainInfo.transform.position.z), 1f); //currently gameobject is aligned in the middle and we move it to the left

            for (int i = 0; i < ins.Length; i++)
            {
                if (marker.label == ins[i].nameTitle)
                {
                    txtName.text = ins[i].nameTitle;
                    imgInstrument.sprite = ins[i].mainImage;
                    txtMainInfo.text = ins[i].infoText;
                    sourceCam.clip = ins[i].clip;
                    btnPlaySound.gameObject.GetComponent<Image>().sprite = sprPlaySound;
                    btnReloadSound.onClick.AddListener(() => StartCoroutine(loop()));
                    Debug.Log("first info: "+ins[i].clip.name);
                }

            }
            
        }
        else
        {
            
            for (int i = 0; i < ins.Length; i++)
            {
                if (marker.label == ins[i].nameTitle)
                {
                    txtName.text = ins[i].nameTitle;
                    imgInstrument.sprite = ins[i].mainImage;
                    txtMainInfo.text = ins[i].infoText;
                    sourceCam.clip = ins[i].clip;
                    StopSound();
                }
                Debug.Log("second info "+ins[i].clip.name);
            }

        }

    }
    #region Sound
    public void PlaySound()
    {
        
        if (sourceCam.isPlaying)
        {
            btnPlaySound.gameObject.GetComponent<Image>().sprite = sprPlaySound;
            sourceCam.Pause();
        }
        else
        {
            //sourceCam.clip = clip;
            btnPlaySound.gameObject.GetComponent<Image>().sprite = sprPauseSound;
            sourceCam.Play();
        }

        Invoke("IsFinished", sourceCam.clip.length);
        
    }

    //if user listens to the sound, then the image will turn to the play sprite to indicate that the sound has finished playing
    void IsFinished()
    {
        btnPlaySound.gameObject.GetComponent<Image>().sprite = sprPlaySound;
    }
   
    void StopSound()
    {
        if (sourceCam.isPlaying || !sourceCam.isPlaying)
        {
            sourceCam.Stop();
            btnPlaySound.gameObject.GetComponent<Image>().sprite = sprPlaySound;
        }
    }
    IEnumerator loop()
    {
        
        if (sourceCam.isPlaying)
        {
            sourceCam.time = 0;
            /*sourceCam.Stop();
            yield return new WaitForSeconds(0.1f);
            sourceCam.PlayOneShot(clip);*/
        }
        //Debug.Log("Clip: "+clip.name);
        
        yield return null;


    }

    #endregion


    #endregion

    #region UIStuff
    //iTween to do specific movements for ui

    //start from 0 to what scale we put where we call this function
    void EaseInPanel(GameObject target,Vector3 scale, float time)
    {
        target.SetActive(true);
        iTween.ScaleTo(target,scale,time);
        
    }

    //start from a specific position we have when we first created our gameobject and we move it to the position we want, again we assign more where we call the method
    void MovePanel(GameObject target, Vector3 position, float time)
    {
        iTween.MoveTo(target, position, time);
    }
    #endregion

    #region NotInUseYet
    /* public void AudioInit()
     {
         LoadFiles("soundsForGame/");
     }*/

    public void LoadFiles(string fileName)
    {
        ins = Resources.LoadAll(fileName, typeof(Instrument)).Cast<Instrument>().ToArray();
        //clips = Resources.LoadAll(fileName, typeof(AudioClip)).Cast<AudioClip>().ToArray();
        //RandomSound();
        Debug.Log("Folder Name Audio: " + fileName);
    }
    #endregion
}
