using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemController : MonoBehaviour
{
    private int scale = 1;
    public float scaleMultipler;
    public float sphereMultiplier;
    public float rotationSpeed;
    public int orbits;
    public GameObject orbit;
    private BorderGenerator[] BG;
    private float outerScale = 0;
    private float arenaSize;
    private PlayerOneController playerOneController;
    void Awake()
    {
        playerOneController = GameObject.Find("Player").GetComponent<PlayerOneController>();
        for(int i = 0; i < orbits; i++) {
            GameObject Orbit = Instantiate(orbit);
            Orbit.name = "Orbit";
            BorderGenerator bg = Orbit.GetComponent<BorderGenerator>();
            outerScale = scale * scaleMultipler;
            bg.scaling = outerScale;
            bg.numberOfSpheres = Mathf.FloorToInt(scale * sphereMultiplier);
            scale += 1;
            if (i % 2 == 0) {
                bg.rotationSpeedX = rotationSpeed;
            } else if (i % 2 != 0) {
                bg.rotationSpeedX = -rotationSpeed;
            }
            if (i % 3 == 0) {
                bg.rotationSpeedY = rotationSpeed;
            } else if (i % 3 != 0) {
                bg.rotationSpeedY = -rotationSpeed;
            }
            if (i % 5 == 0) {
                bg.rotationSpeedZ = rotationSpeed;
            } else if (i % 5 != 0) {
                bg.rotationSpeedZ = -rotationSpeed;
            }
            Orbit.transform.parent = transform;
        }
        arenaSize = outerScale;
        playerOneController.UpdateArenaSize(arenaSize);
    }
}
