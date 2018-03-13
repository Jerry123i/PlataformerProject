using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleAnimationScript : MonoBehaviour {

    public GameObject K, O1, I, Z, O2;

    public List<Sprite> Ks, Os, Is, Zs;

    public Vector2 KPlace, O1Place, IPlace, ZPlace, O2Place;

    private Dictionary<GameObject, Vector2> dictionary = new Dictionary<GameObject, Vector2>();

    private void Awake()
    {
        dictionary.Add(K, KPlace);
        dictionary.Add(O1, O1Place);
        dictionary.Add(I, IPlace);
        dictionary.Add(Z, ZPlace);
        dictionary.Add(O2, O2Place);


        ChoseRandomLetter(K, Ks);
        ChoseRandomLetter(O1, Os);
        ChoseRandomLetter(I, Is);
        ChoseRandomLetter(Z, Zs);
        ChoseRandomLetter(O2, Os);

        StartLetter(O2);
        StartLetter(Z);
        StartLetter(I);
        StartLetter(O1);
        StartLetter(K);

    }
    

    private void StartLetter(GameObject letter)
    {
        letter.GetComponent<TitleLetterScript>().StartCoroutine(letter.GetComponent<TitleLetterScript>().SlideToTitle(dictionary[letter]));
    }

    private void ChoseRandomLetter(GameObject go, List<Sprite> list)
    {
        int i;

        i = Random.Range(0, list.Count);

        go.GetComponent<Image>().sprite = list[i];
        list.RemoveAt(i);

    }

}
