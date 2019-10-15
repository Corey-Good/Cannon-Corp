using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CharacterInfo : MonoBehaviour
{

    static public Dictionary<int, Hashtable> info =
        new Dictionary<int, Hashtable>
        {
            { // Model 1, this is the base model tank
                0, new Hashtable()
                {
                    {"modelCameraOffset", new Vector3(0.0f, 4.75f, -12.0f)},
                    {"movementForce", 7.0f },
                    {"rotationSpeed", 30.0f },
                    {"healthPoints", 65.0f},
                    {"reloadSpeed", 2.0f},
                    {"bulletSpeed", 55.0f},
                    {"bulletArch", 1.7f},
                    {"bulletDamage", 10.0f}

                }
            },
            { // Model 2, this is the futuristic tank
                1, new Hashtable()
                {
                    {"modelCameraOffset", new Vector3(0.0f, 3.5f, -13.0f)},
                    {"movementForce", 10.0f },
                    {"rotationSpeed", 40.0f },
                    {"healthPoints", 55.0f},
                    {"reloadSpeed", 1.55f},
                    {"bulletSpeed", 65.0f},
                    {"bulletArch", 1.3f},
                    {"bulletDamage", 7.0f}
                }
            },
            { // Model 3, this is the catapult
                2, new Hashtable()
                {
                    {"modelCameraOffset", new Vector3(0.0f, 7.0f, -30.0f)},
                    {"movementForce", 5.0f },
                    {"rotationSpeed", 25.0f },
                    {"healthPoints", 90.0f},
                    {"reloadSpeed", 3.25f},
                    {"bulletSpeed", 12.0f},
                    {"bulletArch", 2.0f},
                    {"bulletDamage", 25.0f}
                }
            },
            { // Model 4, this is the Cartoon Tank
                3, new Hashtable()
                {
                    {"modelCameraOffset", new Vector3(0.0f, 11.0f, -30.0f)},
                    {"movementForce", 8.0f },
                    {"rotationSpeed", 35.0f },
                    {"healthPoints", 65.0f},
                    {"reloadSpeed", 1.85f},
                    {"bulletSpeed", 35.0f},
                    {"bulletArch", 1.7f},
                    {"bulletDamage", 12.0f}
                }
            },
            { // Model 5, this is the box tank
                4, new Hashtable()
                {
                    {"modelCameraOffset", new Vector3(0.0f, 2.5f, -7.0f)},
                    {"movementForce", 6.0f },
                    {"rotationSpeed", 30.0f },
                    {"healthPoints", 100.0f},
                    {"reloadSpeed", 2.4f},
                    {"bulletSpeed", 50.0f},
                    {"bulletArch", 1.3f},
                    {"bulletDamage", 10.0f}
                }
            }
           
        };
}


