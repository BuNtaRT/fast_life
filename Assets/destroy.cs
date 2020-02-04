using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroy : MonoBehaviour {
    void Update()
    {
        bool destroyImag = scrip_controll.destroyImage;
        if (destroyImag == true)
        {
            Destroy(this.gameObject);
        }
    }

}
