using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class HealthCollectible : MonoBehaviour, IDataPersistence
{
    [SerializeField] private string id;

    [SerializeField] private float healthValue;
    [SerializeField] private AudioClip pickUpSound;
    private bool used;

    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    public void LoadData(GameData data)
    {
        data.heartsCollected.TryGetValue(id, out used);
        if(used)
        {
            gameObject.SetActive(false);
        }
    }
    public void SaveData(GameData data)
    {
        if(data.heartsCollected.ContainsKey(id))
        {
            data.heartsCollected.Remove(id);
        }

        data.heartsCollected.Add(id, used);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            used = collision.GetComponent<Health>().AddHealth(healthValue);
            if (used)
            {
                SoundManager.instance.PlaySound(pickUpSound);
                gameObject.SetActive(false);
            }
        }
    }
}
