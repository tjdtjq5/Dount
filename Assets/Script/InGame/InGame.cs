using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class InGame : MonoBehaviour
{
    public static InGame instance;

    public GameObject selectGame;

    public GameObject lobbyObj;
    public GameObject inGameObj;

    public GameObject by3Obj;
    public GameObject by4Obj;

    [Header("스크립트")]
    public Map map;
    public CreateBuild createBuild;
    public Resource resource;
    public AdComeback adComeback;
    public Restart restart;
    public GoogleSheetRanking googleSheetRanking;

    [Header("게임오버")]
    public GameObject gameOverPannel;
    public GameObject adPannel;
    public GameObject reStartPannel;
    bool isGameover = false;


    int gold;  
    int food;  int maxFood;   int reFood;
    int water; int maxWater;  int reWater;
    int energy;int maxEnergy; int reEnergy;

    int row = 0;

    private void Awake()
    {
        instance = this;
    }

    private void InGameStart()
    {
        isGameover = false;

        gold = ConfigParser.instance.config.DefaultGold;
        food = ConfigParser.instance.config.DefaultFood;
        water = ConfigParser.instance.config.DefaultWater;
        energy = ConfigParser.instance.config.DefaultEnergy;

        maxFood = ConfigParser.instance.config.FoodMax;
        maxWater = ConfigParser.instance.config.WaterMax;
        maxEnergy = ConfigParser.instance.config.EnergyMax;

        reFood = ConfigParser.instance.config.RevivalFood;
        reWater = ConfigParser.instance.config.RevivalWater;
        reEnergy = ConfigParser.instance.config.RevivalEnergy;
    }

    public void ComebackStart()
    {
        SetResource(BuildKind.농장, reFood);
        SetResource(BuildKind.우물, reWater);
        SetResource(BuildKind.발전소, reEnergy);
        Time.instance.TimeReStart();
    }

    public void Restart()
    {
        switch (row)
        {
            case 3:
                By3Game();
                return;
            case 4:
                By4Game();
                return;
            default:
                restart.Exit();
                return;
        }
    }

    public void SelectGameOpen()
    {
        selectGame.SetActive(true);
    }
    public void GameOver()
    {
        if (gameOverPannel.activeSelf)
        {
            return;
        }

        gameOverPannel.SetActive(true);
        googleSheetRanking.SetScore((int)Time.instance.GetTime());

        Time.instance.TimePause();
        if (!isGameover)
        {
            adPannel.SetActive(true);
            adComeback.AdComebackOpen();
        }
        else
        {

            reStartPannel.SetActive(true);
            restart.RestartOpen();
        }
        isGameover = true;
    }
    public void By3Game()
    {
        lobbyObj.SetActive(false);
        inGameObj.SetActive(true);

        by3Obj.SetActive(true);

        row = 3;

        InGameStart();
        map.MapStart(row);
        createBuild.CreateBuildStart();
        Time.instance.TimeStart();
        MyGold.instance.TextSet();
        resource.ResourceStart();
    }
    public void By4Game()
    {
        lobbyObj.SetActive(false);
        inGameObj.SetActive(true);

        by4Obj.SetActive(true);

        row = 4;

        InGameStart();
        map.MapStart(row);
        createBuild.CreateBuildStart();
        Time.instance.TimeStart();
        MyGold.instance.TextSet();
        resource.ResourceStart();
    }

    public int GetResource(BuildKind buildKind)
    {
        switch (buildKind)
        {
            case BuildKind.집:
                return gold;
            case BuildKind.우물:
                return water;
            case BuildKind.농장:
                return food;
            case BuildKind.발전소:
                return energy;
            default:
                return 0;
        }
    }
    public void SetResource(BuildKind buildKind, int resource)
    {
        switch (buildKind)
        {
            case BuildKind.집:
                gold = resource;
                MyGold.instance.TextSet();
                break;
            case BuildKind.우물:
                if (resource < 0)
                {
                    GameOver();
                    return;
                }
                if (resource > maxWater)
                {
                    resource = maxWater;
                }
                water = resource;
                break;
            case BuildKind.농장:
                if (resource < 0)
                {
                    GameOver();
                    return;
                }
                if (resource > maxFood)
                {
                    resource = maxFood;
                }
                food = resource;
                break;
            case BuildKind.발전소:
                if (resource < 0)
                {
                    GameOver();
                    return;
                }
                if (resource > maxEnergy)
                {
                    resource = maxEnergy;
                }
                energy = resource;
                break;
            default:
                break;
        }
    }
    public int GetMaxResource(BuildKind buildKind)
    {
        switch (buildKind)
        {
            case BuildKind.집:
                return 1000000;
            case BuildKind.우물:
                return maxWater;
            case BuildKind.농장:
                return maxFood;
            case BuildKind.발전소:
                return maxEnergy;
            default:
                return 1000000;
        }
    }
}
