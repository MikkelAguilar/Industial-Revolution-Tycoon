using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Employee : MonoBehaviour
{
    [SerializeField] public string employeeName;
    [SerializeField] public Sprite[] spriteArray;
    private SpriteRenderer spriteRenderer;
    string[] names = {"Rayne", "Terrell","Anderson","Davion","Marquise","Alexandra","Kai",
                    "Madison","Ryleigh","Amanda","Kendrick","Karina","Nyasia","Shirley","Reese","Yoselin",
                    "Avery","Jocelynn","Ellis","Jose","Izaiah","Jasper","Darryl","Corbin","Anthony","Alayna","Drew",
                    "John","Landen","Essence","Branson","Sebastian","Isiah","Simeon","Chase","Mario","Gaige","Sammy","Avah",
                    "Riya","Juliana","Skyler","Mckayla","Carter","Ariel","Nathanael","Liliana","Lina","Clarissa","Colin"};
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        int spriteIndex = Random.Range(0, spriteArray.Length);
        spriteRenderer.sprite = spriteArray[spriteIndex];
        int index = Random.Range(0, names.Length);
        employeeName = names[index];
    }
}
