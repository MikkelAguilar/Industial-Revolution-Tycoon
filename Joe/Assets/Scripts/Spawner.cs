using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject characterPrefab;
    [SerializeField] GameObject _employees;
    public void spawnCharacter() {
        GameObject newCharacter = Instantiate(characterPrefab, new Vector3(65f, -6f, 0), Quaternion.identity);
        newCharacter.transform.SetParent(_employees.transform);
    }
}
