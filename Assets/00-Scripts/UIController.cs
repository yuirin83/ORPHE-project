using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIController : MonoBehaviour
{
    [Header("GameObjects")]
    [Space(2)]
    [SerializeField] GameObject settingPanel;
    [SerializeField] GameObject pose;
    [SerializeField] GameObject referencePanel;

    [SerializeField] GameObject inn_stop;
    RectTransform innStopRect;

    private MouseOver settingButton_MO;

    private TimerDirector timerDirector;

    private InnController inn;

    CustomButton settingButton, startButton, poseButton, referenceButton;

    [SerializeField]
    CustomButton addOneMin, addTenMin, minusOneMin, minusTenMin, resetButton, addThreeSec;

    [Space(10)]
    [Header("Texts")]
    [Space(2)]
    [SerializeField]
    private TextMeshProUGUI startOrStop;
    [SerializeField]
    private TextMeshProUGUI poseOrCounting;

    CustomButton setPin;
    [Space(10)]
    [Header("Images")]
    [Space(2)]
    [SerializeField]  Sprite image_PinOn;
    [SerializeField]  Sprite image_PinOff;
    Kirurobo.UniWindowController uniWindowController;

    [Space(10)]
    [Header("Prefabs")]
    [Space(2)]
    [SerializeField] GameObject _inn0;
    [SerializeField] GameObject _inn1;
    [SerializeField] GameObject _inn2;
    [SerializeField] GameObject _inn3;
    [SerializeField] GameObject _inn4;
    [SerializeField] GameObject _inn5;
    [SerializeField] GameObject _inn6;

    void Start()
    {
        timerDirector = GameObject.Find("Timer").GetComponent<TimerDirector>(); 
        inn = GameObject.Find("InterfaceController").GetComponent<InnController>();
        settingButton = GameObject.Find("SettingButton").GetComponent<CustomButton>();  
        startButton = GameObject.Find("StartButton").GetComponent<CustomButton>();
        poseButton = GameObject.Find("PoseButton").GetComponent<CustomButton>();
        setPin = GameObject.Find("SetPin").GetComponent<CustomButton>();
        referenceButton = GameObject.Find("ReferenceButton").GetComponent<CustomButton>();

        settingPanel.SetActive(false);
        pose.SetActive(false);
        referencePanel.SetActive(false);

        setPin.image.sprite = image_PinOff;
        uniWindowController = GameObject.Find("UniWindowController").GetComponent<Kirurobo.UniWindowController>();

        settingButton_MO = GameObject.Find("SettingButton").GetComponent<MouseOver>();

        innStopRect = inn_stop.GetComponent<RectTransform>();
    }

    void Update()
    {
        StartOrStop();
        PoseOrCounting();
        InnUI();

        settingButton.onClickCallback = () =>
        {
            if(!timerDirector.nowCounting)
            {
                if(settingPanel.activeSelf){
                    settingPanel.SetActive(false);
                }else{
                    if(referencePanel.activeSelf){
                        referencePanel.SetActive(false);
                    }
                    settingPanel.SetActive(true);
                }
            }
        };

        startButton.onClickCallback = () =>
        {
            timerDirector.nowCounting = !timerDirector.nowCounting;
            pose.SetActive(timerDirector.nowCounting);

            //パネルが開いていたら閉じる
            if(settingPanel.activeSelf){
                settingPanel.SetActive(false);
            }
        };

        poseButton.onClickCallback = () =>
        {
            //ポーズを解除して再開する時（true→falseの時）はリフレッシュを行う
            if(timerDirector.isPose) timerDirector.deltaTimeRefresh = true;

            timerDirector.isPose = !timerDirector.isPose;
            
            //パネルが開いていたら閉じる
            if(settingPanel.activeSelf){
                settingPanel.SetActive(false);
            }
        };

        referenceButton.onClickCallback = () =>
        {   
            //パネルが開いていたら閉じる
            if(referencePanel.activeSelf){
                referencePanel.SetActive(false);
            }else{
                if(settingPanel.activeSelf){
                        settingPanel.SetActive(false);
                }
                referencePanel.SetActive(true);
            }
        };

        addOneMin.onClickCallback = () =>
        {
            timerDirector.setTime += 1;
        };

        addTenMin.onClickCallback = () =>
        {
            timerDirector.setTime += 10;
        };

        addThreeSec.onClickCallback = () =>
        {
            timerDirector.setTimeSec += 3;
        };

        minusOneMin.onClickCallback = () =>
        {
            if(timerDirector.setTime > 0){
                timerDirector.setTime -= 1;
            }
        };

        minusTenMin.onClickCallback = () =>
        {
            timerDirector.setTime -= 10;
            if(timerDirector.setTime < 0){
                timerDirector.setTime = 0;
            }
        };

        resetButton.onClickCallback = () =>
        {
            timerDirector.setTime = 0;
        };

        setPin.onClickCallback = () =>
        {
            if(setPin.image.sprite == image_PinOff){
                setPin.image.sprite = image_PinOn;
                //最前面にピン留め
                uniWindowController.isTopmost = true;
            }else{
                setPin.image.sprite = image_PinOff;
                //ピン留め解除
                uniWindowController.isTopmost = false;
            }
        };
    }

    public void StartOrStop()
    {
        if(!timerDirector.nowCounting)
        {
            startOrStop.text = "開始";
            settingButton.image.color = Color.white;
            settingButton_MO.canTouch = true;
        }else{
            startOrStop.text = "停止";
            settingButton.image.color = new Color(0.6f, 0.6f, 0.6f, 1);
            settingButton_MO.canTouch = false;
        }
    }

    public void PoseOrCounting()
    {
        if(!timerDirector.isPose){
            poseOrCounting.text = "一時停止";
        }else{
            poseOrCounting.text = "再開";
        }
    }

    public void InnUI(){
        if(timerDirector.timeUp){
            innStopRect.DOAnchorPos(new Vector2(740f, 200), 1.2f).SetEase(Ease.OutExpo);
        }else if(!timerDirector.timeUp){
            innStopRect.DOAnchorPos(new Vector2(1150f, 200), 1.2f).SetEase(Ease.OutExpo);
        }
    }
}
