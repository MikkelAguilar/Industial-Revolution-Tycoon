using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : MonoBehaviour
{
    [SerializeField] public string machineName;
    public SpriteRenderer spriteRenderer;
    public enum MachineGeneration {
        Unbought,
        Base,
        BasePlus,
        Copper,
        CopperPlus,
        Silver,
        SilverPlus,
        Gold,
        GoldPlus,
    } 
    public MachineGeneration MachineType = MachineGeneration.Unbought;
    public Sprite[] spriteArray;
    public SortedDictionary<MachineGeneration, int> productionDict = new SortedDictionary<MachineGeneration, int>();
    public SortedDictionary<MachineGeneration, int> costToUpgrade = new SortedDictionary<MachineGeneration, int>();
    public int costToModify = 500;
    //public int costToBuy = 600;
    public int productionPerSecond;
    public List<Employee> employeesAssigned = new List<Employee>();
    public List<MachineGeneration> finalTypes;
    GlobalVars globals;
    public bool modified = false;
    public bool modifiable = true;
    private int delayAmount = 1;
    private float timer;
    public int version;
    private int maxEmployees = 3;

    void Start()
    {
        GameObject manager = GameObject.Find("MainManager");
        globals = manager.GetComponent<GlobalVars>();

        spriteRenderer = GetComponent<SpriteRenderer>();

        if (gameObject.tag == "Spinning Wheel") {
            InitialiseSpinningWheel();
        }
        else if (gameObject.tag == "Water Wheel") {
            InitialiseWaterWheel();
        }
        else if (gameObject.tag == "Slide") {
            InitialiseSlide();
        }
    }
    void Update()
    {
        productionPerSecond = productionDict[MachineType];
        handleProduction();
    }
    void handleProduction() {
        timer += Time.deltaTime;

        if (timer >= delayAmount) {
            timer = 0f;
            if (gameObject.tag == "Water Wheel") {
                globals.currentCash += productionPerSecond;
            }
            else if (gameObject.tag == "Slide") {
                foreach (var employee in employeesAssigned)
                {
                    AgentMovement a = employee.GetComponent<AgentMovement>();
                    a.happinessSystem.increaseHappiness(productionPerSecond);
                }
            }
            else {
                globals.currentCash += productionPerSecond * employeesAssigned.Count;
            }
        }
    }
    
    public void UpgradeSpinningWheel() {
        switch (MachineType) {
            case MachineGeneration.Unbought:
                spriteRenderer.sprite = spriteArray[0];
                MachineType = MachineGeneration.Base;
                break;
            case MachineGeneration.Base:
                spriteRenderer.sprite = spriteArray[2];
                MachineType = MachineGeneration.Copper;
                break;
            case MachineGeneration.BasePlus:
                spriteRenderer.sprite = spriteArray[3];
                MachineType = MachineGeneration.CopperPlus;
                break;
            case MachineGeneration.Copper:
                spriteRenderer.sprite = spriteArray[4];
                MachineType = MachineGeneration.Silver;
                break;
            case MachineGeneration.CopperPlus:
                spriteRenderer.sprite = spriteArray[5];
                MachineType = MachineGeneration.SilverPlus;
                break;
            case MachineGeneration.Silver:
                spriteRenderer.sprite = spriteArray[6];
                MachineType = MachineGeneration.Gold;
                break;
            case MachineGeneration.SilverPlus:
                spriteRenderer.sprite = spriteArray[7];
                MachineType = MachineGeneration.GoldPlus;
                break;
        }
    }

    public void DecreaseGeneration() {
        switch (MachineType) {
            case MachineGeneration.Copper:
                spriteRenderer.sprite = spriteArray[0];
                MachineType = MachineGeneration.Base;
                break;
            case MachineGeneration.CopperPlus:
                spriteRenderer.sprite = spriteArray[1];
                MachineType = MachineGeneration.BasePlus;
                break;
            case MachineGeneration.Silver:
                spriteRenderer.sprite = spriteArray[2];
                MachineType = MachineGeneration.Copper;
                break;
            case MachineGeneration.SilverPlus:
                spriteRenderer.sprite = spriteArray[3];
                MachineType = MachineGeneration.CopperPlus;
                break;
            case MachineGeneration.Gold:
                spriteRenderer.sprite = spriteArray[4];
                MachineType = MachineGeneration.Silver;
                break;
            case MachineGeneration.GoldPlus:
                spriteRenderer.sprite = spriteArray[5];
                MachineType = MachineGeneration.SilverPlus;
                break;
        }
    }

    // This is about adding the battery in each generation, ill just leave it a toggle
    public void ToggleModdedMachine() {
        if (!modified) {
            switch (MachineType) {
            case MachineGeneration.Base:
                spriteRenderer.sprite = spriteArray[1];
                MachineType = MachineGeneration.BasePlus;
                break;
            case MachineGeneration.BasePlus:
                spriteRenderer.sprite = spriteArray[0];
                MachineType = MachineGeneration.Base;
                break;
            case MachineGeneration.Copper:
                spriteRenderer.sprite = spriteArray[3];
                MachineType = MachineGeneration.CopperPlus;
                break;
            case MachineGeneration.CopperPlus:
                spriteRenderer.sprite = spriteArray[2];
                MachineType = MachineGeneration.Copper;
                break;
            case MachineGeneration.Silver:
                spriteRenderer.sprite = spriteArray[5];
                MachineType = MachineGeneration.SilverPlus;
                break;
            case MachineGeneration.SilverPlus:
                spriteRenderer.sprite = spriteArray[4];
                MachineType = MachineGeneration.Silver;
                break;
            case MachineGeneration.Gold:
                spriteRenderer.sprite = spriteArray[7];
                MachineType = MachineGeneration.GoldPlus;
                break;
            case MachineGeneration.GoldPlus:
                spriteRenderer.sprite = spriteArray[6];
                MachineType = MachineGeneration.Gold;
                break;
            }
            modified = true;
        }
    }

    // Just incase im setting all the sprites to the enum state at the very start
    public void InitialiseSpinningWheel() {
        modifiable = true;

        if (version == 2) {
            costToModify = 5500;
            productionDict[MachineGeneration.Unbought] = 0;
            productionDict[MachineGeneration.Base] = 5;
            productionDict[MachineGeneration.Copper] = 12;
            productionDict[MachineGeneration.Silver] = 22;
            productionDict[MachineGeneration.Gold] = 40;
            productionDict[MachineGeneration.BasePlus] = 13;
            productionDict[MachineGeneration.CopperPlus] = 20;
            productionDict[MachineGeneration.SilverPlus] = 30;
            productionDict[MachineGeneration.GoldPlus] = 48;

            costToUpgrade[MachineGeneration.Unbought] = 5000;
            costToUpgrade[MachineGeneration.Base] = 4500;
            costToUpgrade[MachineGeneration.Copper] = 6000;
            costToUpgrade[MachineGeneration.Silver] = 11000;
            costToUpgrade[MachineGeneration.Gold] = 0;

            costToUpgrade[MachineGeneration.BasePlus] = 4500;
            costToUpgrade[MachineGeneration.CopperPlus] = 6000;
            costToUpgrade[MachineGeneration.SilverPlus] = 11000;
            costToUpgrade[MachineGeneration.GoldPlus] = 0;
        }
        else {
            costToModify = 1800;
            productionDict[MachineGeneration.Unbought] = 0;
            productionDict[MachineGeneration.Base] = 1;
            productionDict[MachineGeneration.Copper] = 2;
            productionDict[MachineGeneration.Silver] = 4;
            productionDict[MachineGeneration.Gold] = 8;
            productionDict[MachineGeneration.BasePlus] = 3;
            productionDict[MachineGeneration.CopperPlus] = 4;
            productionDict[MachineGeneration.SilverPlus] = 6;
            productionDict[MachineGeneration.GoldPlus] = 10;

            costToUpgrade[MachineGeneration.Unbought] = 1000;
            costToUpgrade[MachineGeneration.Base] = 900;
            costToUpgrade[MachineGeneration.Copper] = 1500;
            costToUpgrade[MachineGeneration.Silver] = 3600;
            costToUpgrade[MachineGeneration.Gold] = 0;

            costToUpgrade[MachineGeneration.BasePlus] = 900;
            costToUpgrade[MachineGeneration.CopperPlus] = 1500;
            costToUpgrade[MachineGeneration.SilverPlus] = 3600;
            costToUpgrade[MachineGeneration.GoldPlus] = 0;
        }

        finalTypes = new List<MachineGeneration> {MachineGeneration.Gold, MachineGeneration.GoldPlus};

        switch (MachineType) {
            case MachineGeneration.Base:
                spriteRenderer.sprite = spriteArray[0];
                break;
            case MachineGeneration.BasePlus:
                spriteRenderer.sprite = spriteArray[1];
                break;
            case MachineGeneration.Copper:
                spriteRenderer.sprite = spriteArray[2];
                break;
            case MachineGeneration.CopperPlus:
                spriteRenderer.sprite = spriteArray[3];
                break;
            case MachineGeneration.Silver:
                spriteRenderer.sprite = spriteArray[4];
                break;
            case MachineGeneration.SilverPlus:
                spriteRenderer.sprite = spriteArray[5];
                break;
            case MachineGeneration.Gold:
                spriteRenderer.sprite = spriteArray[6];
                break;
            case MachineGeneration.GoldPlus:
                spriteRenderer.sprite = spriteArray[7];
                break;
            case MachineGeneration.Unbought:
                spriteRenderer.sprite = spriteArray[8];
                break;
        }
    }
    public void InitialiseWaterWheel() {
        modifiable = false;

        productionDict[MachineGeneration.Unbought] = 0;
        productionDict[MachineGeneration.Base] = 1;
        productionDict[MachineGeneration.Copper] = 3;
        productionDict[MachineGeneration.Silver] = 7;
        productionDict[MachineGeneration.Gold] = 13;

        costToUpgrade[MachineGeneration.Unbought] = 2000;

        costToUpgrade[MachineGeneration.Base] = 6000;
        costToUpgrade[MachineGeneration.Copper] = 15000;
        costToUpgrade[MachineGeneration.Silver] = 20000;
        costToUpgrade[MachineGeneration.Gold] = 0;

        finalTypes = new List<MachineGeneration> {MachineGeneration.Gold};

        switch (MachineType) {
            case MachineGeneration.Base:
                spriteRenderer.sprite = spriteArray[0];
                break;
            case MachineGeneration.Unbought:
                spriteRenderer.sprite = spriteArray[1];
                break;
        }
    }

    public void UpgradeWaterWheel() {
        switch (MachineType) {
            case MachineGeneration.Unbought:
                spriteRenderer.sprite = spriteArray[1];
                MachineType = MachineGeneration.Base;
                break;
            case MachineGeneration.Base:
                MachineType = MachineGeneration.Copper;
                break;
            case MachineGeneration.Copper:
                MachineType = MachineGeneration.Silver;
                break;
            case MachineGeneration.Silver:
                MachineType = MachineGeneration.Gold;
                break;
        }
    }
    public void InitialiseSlide() {
        modifiable = false;

        productionDict[MachineGeneration.Unbought] = 0;
        productionDict[MachineGeneration.Base] = 1;
        productionDict[MachineGeneration.Copper] = 2;
        productionDict[MachineGeneration.Silver] = 3;
        productionDict[MachineGeneration.Gold] = 4;

        costToUpgrade[MachineGeneration.Unbought] = 500;

        costToUpgrade[MachineGeneration.Base] = 1000;
        costToUpgrade[MachineGeneration.Copper] = 2000;
        costToUpgrade[MachineGeneration.Silver] = 4000;
        costToUpgrade[MachineGeneration.Gold] = 0;

        finalTypes = new List<MachineGeneration> {MachineGeneration.Gold};

        switch (MachineType) {
            case MachineGeneration.Base:
                spriteRenderer.sprite = spriteArray[0];
                break;
            case MachineGeneration.Unbought:
                spriteRenderer.sprite = spriteArray[1];
                break;
        }
    }
    public void UpgradeSlide() {
        switch (MachineType) {
            case MachineGeneration.Unbought:
                spriteRenderer.sprite = spriteArray[1];
                MachineType = MachineGeneration.Base;
                break;
            case MachineGeneration.Base:
                MachineType = MachineGeneration.Copper;
                break;
            case MachineGeneration.Copper:
                MachineType = MachineGeneration.Silver;
                break;
            case MachineGeneration.Silver:
                MachineType = MachineGeneration.Gold;
                break;
        }
    }
    public bool machineFull() {
        if (employeesAssigned.Count >= maxEmployees) {
            return true;
        }
        return false;
    }
}
