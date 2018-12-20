using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuButtonScript : MonoBehaviour, ISelectHandler {

    protected Button button;
    public string level;
	private AudioSource audioSource;
	public AudioClip selectSound;
	public AudioClip pressSound;

    private void Awake()
    {
        button = gameObject.GetComponent<Button>();
		audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        button.onClick.AddListener(delegate { StageManagerScript.instance.LoadStage(level); });
    }

    public void OnSelect(BaseEventData eventdata)
    {        
        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("Highlighted");

		audioSource.clip = selectSound;
		audioSource.Play();

    }

	public void PressSound()
	{
		audioSource.clip = pressSound;
		audioSource.Play();
	}

}
