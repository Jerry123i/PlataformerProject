using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TitleLetterScript : MonoBehaviour {

    public bool placed; 

    public void DOSlide(Vector2 finalPosition)
    {
        GetComponent<RectTransform>().DOAnchorPosX(finalPosition.x, 0.6f);
        StartCoroutine(PunchLetter(1.5f));
    }

    public IEnumerator SlideToTitle(Vector2 finalPosition)
    {
        do
        {
            transform.DOMoveX(finalPosition.x, 1.5f, false);
            //transform.localPosition = Vector2.(GetComponent<RectTransform>().anchoredPosition, finalPosition, Time.deltaTime * 1.50f);

            yield return null;
        } while ((new Vector2(GetComponent<RectTransform>().anchoredPosition.x, GetComponent<RectTransform>().anchoredPosition.y) - finalPosition).magnitude > 0.9f);


        placed = true;
        //StartCoroutine(PunchLetter());
        

    }

    public IEnumerator PunchLetter(float time)
    {

        yield return new WaitForSeconds(time);

        transform.DOPunchPosition(Vector3.right*20.0f, 1.0f, 5, 0.5f, false);
        
    }

	
}
