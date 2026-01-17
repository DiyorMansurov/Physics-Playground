using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{

    [SerializeField] float popScale = 1.2f;
    [SerializeField] float popTime = 0.08f;
    [SerializeField] float shrinkTime = 0.12f;

    public IEnumerator PopAndDestroy()
    {
        Vector3 startScale = transform.localScale;
        Vector3 bigScale = startScale * popScale;

        yield return new WaitForSeconds(2f);

        float t = 0;
        while (t < popTime)
        {
            t += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale, bigScale, t / popTime);
            yield return null;
        }

        t = 0;
        while (t < shrinkTime)
        {
            t += Time.deltaTime;
            transform.localScale = Vector3.Lerp(bigScale, Vector3.zero, t / shrinkTime);
            yield return null;
        }

        Destroy(gameObject);
    }
}
