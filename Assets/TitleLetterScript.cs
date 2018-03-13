using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleLetterScript : MonoBehaviour {

    public bool placed; 

    public IEnumerator SlideToTitle(Vector2 finalPosition)
    {
        do
        {
            transform.localPosition = Vector2.Lerp(transform.localPosition, finalPosition, Time.deltaTime * 1.50f);

            yield return null;
        } while ((new Vector2(this.transform.position.x, this.transform.position.y) - finalPosition).magnitude > 0.01f);

        placed = true;

    }

	
}
