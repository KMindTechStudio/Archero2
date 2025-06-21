using UnityEngine;
using UnityEngine.EventSystems;

public class Virutal_Joystick : MonoBehaviour
{
    [Header("Joystick Objects Assign")]
    public GameObject joystickWhole;
    public RectTransform joystickBackground;
    public RectTransform joystickHandle;
    //public RectTransform joystickEffect;
    public float joystickMoveThreshold;

    [Header("Player")]
    private GameObject playerObject;
    private Transform player;
    public float moveSpeed = 3f;

    private Vector2 inputVector;
    private Vector2 moveDirection;
    private Camera mainCamera;
    void Start()
    {
        mainCamera = Camera.main;
        playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.transform;
        Debug.Log("Found Player:" +  player.name);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Input.mousePosition; //Get the current MousePos
            joystickWhole.SetActive(true);
            joystickWhole.transform.position = mousePosition;
        }

        // If touch or mouse input is detected
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePosition = Input.mousePosition; //Get the current MousePos
            Vector2 joystickPosition = joystickBackground.position;

            Vector2 direction = joystickHandle.anchoredPosition - joystickBackground.anchoredPosition;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            inputVector = Vector2.ClampMagnitude(mousePosition - joystickPosition, joystickBackground.sizeDelta.x * 0.5f);
            joystickHandle.localPosition = inputVector;

           if (inputVector.magnitude > joystickMoveThreshold)
            {
                MovePlayer(inputVector.normalized);
            }
           else
            {
                MovePlayer(inputVector.normalized);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            joystickHandle.localPosition = Vector3.zero;
            inputVector = Vector2.zero;
            joystickWhole.SetActive(false);
        }
    }

    public void MovePlayer(Vector2 direction)
    {
        moveDirection = new Vector2(direction.x, direction.y);
        player.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
    }
}
