using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TrackerCalibrator : MonoBehaviour
{
    public Transform _tip;
    public Transform relatedTracker;
    public GameObject hand;
    Vector3 initialPosition;
    Vector3 initialOffset;
    public static Transform steinRock;

    void Start()
    {
        steinRock = transform;
        _tip = GameObject.Find("TIP").transform;
        relatedTracker = GameObject.Find("Kautschuk(Clone)").transform;

        transform.SetParent(_tip);
        transform.localPosition = Vector3.zero;

        StartCoroutine(SetInitialPosition());


        if (SceneManager.GetActiveScene().buildIndex != 4)
            Destroy(gameObject);
    }

    public bool enableUpdate = false;

    IEnumerator SetInitialPosition()
    {
        yield return new WaitForSeconds(5f);
        initialOffset = _tip.position - relatedTracker.position;
        enableUpdate = true;
        UpdatePosition();
    }

    void UpdatePosition()
    {
        if (enableUpdate)
        {
            Vector3 currentOffset = _tip.position - relatedTracker.position;
            Vector3 fixedPosition = initialOffset - currentOffset;

            transform.localPosition = fixedPosition;
        }
    }

    Vector3 lastHandPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            hand = other.gameObject;
            ParentToHand(other.transform, _tip);
        }
            

        if (other.CompareTag("Base"))
            ParentToHand(other.transform, hand.transform);
    }

    void ParentToHand(Transform parent, Transform objectToEnable)
    {
        parent.GetComponent<SphereCollider>().enabled = false;     
        transform.SetParent(parent);
        transform.localPosition = Vector3.zero;
        _tip.GetComponent<TIP>().calibrationRing.fillAmount = 0;
        StartCoroutine(EnableBase(objectToEnable));
    }

    IEnumerator EnableBase(Transform parent)
    {
        float animationTime = 0f;
        
        while (animationTime < 4)
        {
            animationTime += Time.deltaTime;
            _tip.GetComponent<TIP>().calibrationRing.fillAmount = animationTime / 4f;
            yield return null;
        }
       
        parent.GetComponent<SphereCollider>().enabled = true;
    }
}
