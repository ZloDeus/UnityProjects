using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileStartPositionRandomiser : MonoBehaviour
{
    public GameObject ProjectileClone;
    Rigidbody rbClone;

    private void Update()
    {
        rbClone = ProjectileClone.GetComponent<Rigidbody>();
        float CloneShift = new float();
        CloneShift = Random.Range(-5, 5);

        rbClone.AddForce(CloneShift * 2, 0.0f, 0.0f);
    }

}
