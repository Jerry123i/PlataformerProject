using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TitleLetterScript : MonoBehaviour {

    public bool placed; 

    public void DOSlide(Vector2 finalPosition, float time)
    {
        time = time * Random.Range(0.8f, 1.2f);

        GetComponent<RectTransform>().DOAnchorPosX(finalPosition.x, time);
        StartCoroutine(PunchLetter(time));
    }

    public IEnumerator PunchLetter(float time)
    {        
        placed = true;

        yield return new WaitForSeconds(time);
       
        //transform.DOPunchPosition(Vector3.left*20.0f *Random.Range(0.8f, 1.2f) , 1.0f * Random.Range(0.8f, 1.2f), 4, 0.5f, false);
        
    }

	
}
