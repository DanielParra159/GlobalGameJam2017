using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemigojefe1 : MonoBehaviour {

    public void Attack1() {
        gameObject.GetComponentInParent<Boss>().Attack1();
    }

    public void Attack2() {
        gameObject.GetComponentInParent<Boss>().Attack2();
    }
}
