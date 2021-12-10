using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class InGameManager : MonoBehaviour
{
    public GameObject karePanel, soruPanel;


    [SerializeField]
    private GameObject karePrefab;

    [SerializeField]
    private Transform karelerPaneli;

    private GameObject[] karelerDizisi = new GameObject[25];

    [SerializeField]
    private Transform soruPaneli;

    [SerializeField]
    private Text soruPaneliText;

    [SerializeField]
    private Sprite[] kareSprites;

    GameObject gecerliKare;

    List<int> bolumDegerleriListesi = new List<int>();
    int bolunenSayi, bolenSayi, butonDegeri, dogruSonuc, kalanCan, kacinciSoru;
    bool butonaBasilsinMi;
    string sorununZorlukDerecesi;

    CanManager canManager;
    PuanManager puanManager;

    [SerializeField]
    private GameObject sonucPaneli;


    private void Awake()
    {
        kalanCan = 3;
        canManager = Object.FindObjectOfType<CanManager>();
        canManager.kalanCanlarýKontrolEt(kalanCan);

        puanManager = Object.FindObjectOfType<PuanManager>();

        sonucPaneli.GetComponent<RectTransform>().localScale = Vector3.zero;
    }


    void Start()
    {
        butonaBasilsinMi = false;
        soruPaneli.GetComponent<RectTransform>().localScale = Vector3.zero;
        KareleriOlustur();
        Invoke("SoruPaneliniAc", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void KareleriOlustur()
    {
        for (int i = 0; i < 25; i++)
        {
            GameObject kare = Instantiate(karePrefab, karelerPaneli);
            kare.transform.GetChild(1).GetComponent<Image>().sprite = kareSprites[Random.Range(0, kareSprites.Length)];
            kare.transform.GetComponent<Button>().onClick.AddListener(() => ButonaBasildi());
            karelerDizisi[i] = kare;
        }
        BölümDegerleri();
        StartCoroutine(DoFadeRoutine());
        
    }

    void ButonaBasildi()
    {
        if (butonaBasilsinMi)
        {
            butonDegeri = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<Text>().text);

            gecerliKare = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

            SonucuKontrolEt();
        }
        
    }

    void SonucuKontrolEt()
    {
        if (dogruSonuc == butonDegeri)
        {
            puanManager.PuanýArtir(sorununZorlukDerecesi);

            bolumDegerleriListesi.RemoveAt(kacinciSoru);

            if(bolumDegerleriListesi.Count>0)
            {
                SoruPaneliniAc();
            }
            else
            {
                OyunBitti();
            }
            

            gecerliKare.transform.GetChild(1).GetComponent<Image>().enabled = true; 
            gecerliKare.transform.GetChild(0).GetComponent<Text>().text="";
            gecerliKare.transform.GetComponent<Button>().interactable = false;

        }
        else
        {
            kalanCan--;
            canManager.kalanCanlarýKontrolEt(kalanCan);
        }

        if(kalanCan <= 0)
        {
            OyunBitti();
        }
    }


    void OyunBitti()
    {
        butonaBasilsinMi = false;
        soruPanel.SetActive(false);
        karePanel.SetActive(false);
        sonucPaneli.GetComponent<RectTransform>().DOScale(1, 0.3f).SetEase(Ease.OutBack);
    }


    IEnumerator DoFadeRoutine()
    {
        foreach (var kare in karelerDizisi)
        {
            kare.GetComponent<CanvasGroup>().DOFade(1, 0.2f);
            yield return new WaitForSeconds(0.07f);
        }
    }

    void BölümDegerleri()
    {
        foreach (var kare in karelerDizisi)
        {
            int rastgeleDeger = Random.Range(1, 13);
            bolumDegerleriListesi.Add(rastgeleDeger);
            kare.transform.GetChild(0).GetComponent<Text>().text = rastgeleDeger.ToString();

        }
        
    }

    void SoruPaneliniAc()
    {

        SoruyuSor();
        butonaBasilsinMi = true;
        soruPaneli.GetComponent<RectTransform>().DOScale(1,0.3f).SetEase(Ease.OutBack);
    }

    void SoruyuSor()
    {
        bolenSayi=Random.Range(2, 11);

        kacinciSoru = Random.Range(0, bolumDegerleriListesi.Count);

        dogruSonuc = bolumDegerleriListesi[kacinciSoru];

        bolunenSayi = bolenSayi * dogruSonuc;


        if(bolunenSayi<=40)
        {
            sorununZorlukDerecesi = "kolay";
        }
        else if (bolunenSayi > 40 && bolunenSayi <= 80)
        {
            sorununZorlukDerecesi = "orta";
        }
        else 
        {
            sorununZorlukDerecesi = "zor";
        }


        soruPaneliText.text = bolunenSayi.ToString() + " : " + bolenSayi.ToString();
    }
}
