using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuButtonScript : MonoBehaviour, ISelectHandler {

    protected Button button;
    public string level;

    private void Awake()
    {
        button = gameObject.GetComponent<Button>();        
    }

    private void OnEnable()
    {
        button.onClick.AddListener(delegate { StageManagerScript.instance.LoadStage(level); });
    }    

    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log(name + ": OnSelect");
        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("Highlighted");        
    }

}
