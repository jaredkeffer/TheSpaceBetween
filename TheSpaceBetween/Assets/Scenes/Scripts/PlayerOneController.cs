using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerOneController : MonoBehaviour
{
    private Vector2 moveInput = new Vector2(0,0);
    public float speed;
    public float rotationSpeed;
    private Vector3 rotation;
    private Text HUD;
    private float boost = 3f;
    private float boostMeterMax;
    public float boostMeter;
    private float rotationX;
    private float rotationY;
    private bool inputFreeze = false;
    public float bounceBack;
    private float arenaSize;
    public GameObject stopSign;
    private SystemController starSystem;
    private int score = 0;
    void Awake() {
        starSystem = GameObject.Find("System").GetComponent<SystemController>();
        HUD = GameObject.Find("HUD").GetComponentInChildren<Text>();
        // HUD.text = score.ToString();
    }
    void Update()
    {
        moveInput = new Vector2(0,0);
        moveInput = new Vector2(Input.GetAxis("Horizontal") * rotationSpeed, Input.GetAxis("Vertical")) * rotationSpeed;
        
        if (!inputFreeze) {
            if (Mathf.Abs(moveInput.x) > 0 || Mathf.Abs(moveInput.y) > 0) {
                Move(moveInput);
            // Debug.Log(moveInput);
            }
            if (Vector3.Magnitude(transform.position) > arenaSize) {
                inputFreeze = true;
                WallBounce();
            }
            // if (Input.GetKey(KeyCode.W)) {
            //     // rotationX -= rotationSpeed;
            //     rotationX = -rotationSpeed;
            //     moveInput = new Vector2(moveInput.x,rotationX);
            //     Move(moveInput);
            // }
            // if (Input.GetKey(KeyCode.A)) {
            //     // rotationY -= rotationSpeed;
            //     rotationY = -rotationSpeed;
            //     moveInput = new Vector2(rotationY ,moveInput.y);
            //     Move(moveInput);
            // }
            // if (Input.GetKey(KeyCode.S)) {
            //     // rotationX += rotationSpeed;
            //     rotationX = rotationSpeed;
            //     moveInput = new Vector2(moveInput.x,rotationX);
            //     Move(moveInput);
            // }
            // if (Input.GetKey(KeyCode.D)) {
            //     // rotationY += rotationSpeed;
            //     rotationY = rotationSpeed;            
            //     moveInput = new Vector2(rotationY, moveInput.y);
            //     Move(moveInput);
            // }
            if (Input.GetKey(KeyCode.Space)) {
                transform.Translate(Vector3.forward * Time.deltaTime * speed);
            }
            if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.LeftShift)) {
                transform.Translate(Vector3.forward * Time.deltaTime * speed * boost);
            }
        }
        
    }
    private void Move(Vector2 _moveInput) {
        transform.Rotate(-_moveInput.y, _moveInput.x, 0);
    }
    public void WallBounce() {
        StartCoroutine(WallBounce(transform.position));
    }
    public void UpdateArenaSize(float _arenaSize) {
        arenaSize = _arenaSize;
    }
    private IEnumerator WallBounce(Vector3 _position) {
        float t = 0;
        inputFreeze = true;
        Vector3 oldPosition = _position;
        Vector3 targetPosition = new Vector3 (_position.x / bounceBack, _position.y / bounceBack, _position.z / bounceBack);
        Vector3 oldRotation = transform.eulerAngles;
        Vector3 targetRotation = new Vector3(transform.eulerAngles.x - 180, transform.eulerAngles.y, transform.eulerAngles.z);
        GameObject StopSign = Instantiate(stopSign);
        StopSign.transform.position = _position;
        StopSign.transform.LookAt(new Vector3(0,0,0));
        StopSign.transform.localScale = new Vector3(5,5,5);
        SpriteRenderer stopSignSprite = StopSign.GetComponent<SpriteRenderer>();
        // transform.LookAt(new Vector3(0,0,0));
        while (t <= 1) {
            transform.position = Vector3.Lerp(oldPosition, targetPosition, t);
            transform.eulerAngles = Vector3.Lerp(oldRotation, targetRotation, t);
            stopSignSprite.color = new Color(200,200,200,t);
            t += 0.05f;
            yield return new WaitForSeconds(0.01f);
        }
        inputFreeze = false;
        yield return null;
    }
    private IEnumerator Shrink(Transform _transform) {
        Vector3 oldScale = _transform.localScale;
        Vector3 newScale = new Vector3(0,0,0);
        float t = 0;
        while (t <= 1) {
            _transform.localScale = Vector3.Lerp(oldScale, newScale, t);
            t += 0.05f;
            yield return new WaitForSeconds(0.01f);
        }
        Destroy(_transform.gameObject);
        yield return null;
    }
    void OnTriggerEnter(Collider col) {
        if (col.transform.name == "Sphere") {
            score += 1;
            HUD.text = score.ToString();
            // StartCoroutine(Shrink(col.transform));
            Destroy(col.transform.gameObject);
        }
    }
    
}
