using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TitleAnimationScript : MonoBehaviour {

    GameObject[] allLetters;

    public GameObject K, O1, I, Z, O2;

    public List<Sprite> Ks, Os, Is, Zs;

    public Vector2 KPlace, O1Place, IPlace, ZPlace, O2Place;

    private Dictionary<GameObject, Vector2> dictionary = new Dictionary<GameObject, Vector2>();

    private void Awake()
    {
        allLetters = new GameObject[5];

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

        allLetters[0] = O2;
        allLetters[1] = Z;
        allLetters[2] = I;
        allLetters[3] = O1;
        allLetters[4] = K;
        
    }

    private void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            StartCoroutine(StartTitle(0.4f));
        }
    }

    private IEnumerator StartTitle(float time)
    {
        int i = 0;

        for (i = 0; i < allLetters.Length; i++)
        {


            StartLetter(allLetters[i], (1.175f - 0.175f * time));
            Debug.Log("Starting letter:" + allLetters[i].ToString());

            yield return new WaitForSeconds((1.175f - 0.175f * time) * 0.4f);

        }

        yield return new WaitForSeconds((1.175f - 0.175f * time)*0.5f);

        for (i = 0; i < allLetters.Length; i++)
        {
            //allLetters[i].transform.DOPunchPosition(Vector3.up * 20.0f, 1.5f, 5, 1, false);
            allLetters[i].transform.DOJump(allLetters[i].transform.position, 85.0f, 1, 1.7f);
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void StartLetter(GameObject letter, float time)
    {
        //letter.GetComponent<TitleLetterScript>().StartCoroutine(letter.GetComponent<TitleLetterScript>().SlideToTitle(dictionary[letter]));
        letter.GetComponent<TitleLetterScript>().DOSlide(dictionary[letter], time);
    }

    private void ChoseRandomLetter(GameObject go, List<Sprite> list)
    {
        int i;

        i = Random.Range(0, list.Count);

        go.GetComponent<Image>().sprite = list[i];
        list.RemoveAt(i);

    }

}
