using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IDataPersistence
{
   [Header ("Health")]
   [SerializeField] private float startingHealth;

    public float currentHealth {get; private set;}
    private Animator anim;
    private bool dead;


    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private float numberOfFlashes;
    private SpriteRenderer spriteRend;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;

    [Header("Sounds")]
    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioClip dieSound;

    private bool invulnerable;



    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend= GetComponent<SpriteRenderer>();
    }
    public void LoadData(GameData data)
    {
        this.currentHealth = data.currentHealth;
    }

    public void SaveData(GameData data)
    {
        data.currentHealth = this.currentHealth;
    }

    public void TakeDamage(float _damage)
    {
        if (invulnerable) return;
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        if(currentHealth > 0)
        {
            //player hurt
            anim.SetTrigger("hurt");
            SoundManager.instance.PlaySound(hurtSound);
            //iframes
            StartCoroutine(Invulnerability());
        }
        else
        {
            if (!dead)
            {

                foreach(Behaviour component in components)
                {
                    component.enabled = false;
                }

                anim.SetBool("grounded", true);
                anim.SetTrigger("die");

                dead = true;
                SoundManager.instance.PlaySound(dieSound);
            }
        }
    }


    public bool AddHealth(float _value)
    {
        if (currentHealth < startingHealth)
        {
            currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
            return true;
        }
        else
        {
            return false;
        }
    }

    private IEnumerator Invulnerability() 
    {
        invulnerable=true;
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration/(numberOfFlashes*2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);

        invulnerable= false;    
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public void Respawn()
    {
        dead = false;
        AddHealth(startingHealth);
        anim.ResetTrigger("die");
        anim.Play("Idle");
        StartCoroutine(Invulnerability());

        foreach (Behaviour component in components)
        {
            component.enabled = true;
        }
    }
}
