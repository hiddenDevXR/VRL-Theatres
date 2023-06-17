using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class ElevatorManager : MonoBehaviour
{

    bool enableTransition = true;
    Vector3 currentTarget;

    public enum State { Arriving_Up, Arriving_Down, Still };
    public State myState = State.Still;

    static int lastSceneIndex = 0;
    public static string desireSceneName = "";
    public static int desireSceneIndex = 0;
    public static bool isPortal = false;

    public AudioSource audioSource;

    public Transform head;

    public Transform playerRig;
    public static Transform xr_rig;
    public Animator fadeSphereAnimator;
    public Animator animator;
    private Vector3 currentRigPosition = new Vector3(0.0f, 0.0f, 0.0f);

    public enum TATLevels { MainRoom, Matters, Up, Down }
    public TATLevels upLevel, downLevel;

    public NetworkManager networkManager;

    private void Start()
    {
        xr_rig = playerRig;

        head = playerRig.GetComponent<XR_Rig>().head;

        if (animator == null)
            animator = GetComponent<Animator>();

        StartCoroutine(WaitToArrival());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
            GoUp();

        if (Input.GetKeyDown(KeyCode.D))
            GoDown();
    }

    void TraversePortalToScene(int index)
    {
        PrepareToLaunch();
        StartCoroutine(LoadAsyncScene(index));
    }

    public void PrepareToLaunch()
    {
        StartCoroutine(WaitToFade());
        playerRig.transform.SetParent(transform);
        audioSource.Play();
        enableTransition = true;
        audioSource.transform.position = Vector3.zero;
    }

    IEnumerator WaitToFade()
    {
        yield return new WaitForSeconds(8f);
        FadeScene();
    }

    public void FadeScene()
    {
        fadeSphereAnimator.SetBool("fade", true);
    }

    public void GoUp()
    {
        if(enableTransition)
        {
            enableTransition = false;
            animator.SetInteger("AnimIndex", 1);
            TraversePortalToScene((int)upLevel);
        }
    }

    public void GoDown()
    {
        if(enableTransition)
        {
            enableTransition = false;
            animator.SetInteger("AnimIndex", 4);
            TraversePortalToScene((int)downLevel);
        }
    }

    public void GoTo(int index)
    {
        if(enableTransition)
        {
            enableTransition = false;
            animator.SetInteger("AnimIndex", 4);
            TraversePortalToScene(index);
        }
    }

    public Animator elevatoGlass;
    public AudioSource ping;
    public void ReturnToIdle()
    {
        animator.SetInteger("AnimIndex", 0);
        playerRig.transform.SetParent(null);

        if (ping != null)
            ping.Play();

        networkManager.UserArrived();

        enableTransition = true;
        if(elevatoGlass != null)
            elevatoGlass.enabled = true;
    }

    bool enablePreparation = true;

    IEnumerator LoadAsyncScene(int sceneIndex)
    {
        PhotonNetwork.Disconnect();

        while (PhotonNetwork.IsConnected)
            yield return null;

        yield return new WaitForSeconds(20f);

        lastSceneIndex = SceneManager.GetActiveScene().buildIndex;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);
        asyncLoad.allowSceneActivation = false;

        enablePreparation = true;

        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f && enablePreparation)
            {
                enablePreparation = false;
                playerRig.transform.SetParent(null);
                currentRigPosition = playerRig.GetComponent<Rigidbody>().position;
                asyncLoad.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    int currentSceneIndex = 0;
    public int GetCurrentSceneIndex() { return currentSceneIndex; }

    void ManageArrival()
    {
        fadeSphereAnimator.SetBool("fade", false);
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex == 1)
        {
            SetArrivalAnimation(3, -14);
        }
    }

    IEnumerator WaitToArrival()
    {
        yield return new WaitForSeconds(2f);
        ManageArrival();
    }
    bool enableTrack = false;

    void SetArrivalAnimation(int animIndex, int y_position)
    {
        Vector3 fixedPos = new Vector3(currentRigPosition.x, y_position, currentRigPosition.z);
        playerRig.GetComponent<Rigidbody>().MovePosition(fixedPos);
        head.transform.position = new Vector3(currentRigPosition.x, head.transform.position.y, currentRigPosition.z);    
        audioSource.transform.position = new Vector3(audioSource.transform.position.x, y_position, audioSource.transform.position.z);
        audioSource.Play();
        animator.SetInteger("AnimIndex", animIndex);
        enableTrack = true;
    }

    private void FixedUpdate()
    {
        if (enableTrack)
        {
            playerRig.transform.SetParent(transform);
            Vector3 fixedPos = new Vector3(currentRigPosition.x, transform.position.y, currentRigPosition.y);
            playerRig.GetComponent<Rigidbody>().MovePosition(fixedPos);
            head.transform.position = new Vector3(fixedPos.x, head.transform.position.y - 1f, fixedPos.z);         
            enableTrack = false;
        }   
    }
}