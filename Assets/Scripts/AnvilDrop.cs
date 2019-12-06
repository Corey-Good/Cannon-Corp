/************************************************************************/
/* Author:  */
/* Date Created: */
/* Last Modified Date: */
/* Modified By: */
/************************************************************************/

using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public class AnvilDrop : MonoBehaviour
{
    public GameObject centerSpawn;
    private List<GameObject> anvils = new List<GameObject>();
    private GameObject anvil;
    private float time = 0.0f;

    private void FixedUpdate()
    {
        time += Time.deltaTime;

        if (anvils.Count > 20)
        {
            foreach (GameObject anvil in anvils)
            {
                PhotonNetwork.Destroy(anvil);
            }
            anvils.RemoveRange(0, anvils.Count);
        }

        if (time >= 0.65f)
        {
            time = 0.0f;
            anvil = PhotonNetwork.Instantiate("Anvil", centerSpawn.transform.position + new Vector3(Random.Range(-15.0f, 16.0f), 0.0f, Random.Range(-36.0f, 36.0f)), centerSpawn.transform.rotation);
            anvil.GetComponent<Rigidbody>().AddForce(Vector3.down * 10000.0f, ForceMode.Acceleration);
            anvils.Add(anvil);

        }
    }
}