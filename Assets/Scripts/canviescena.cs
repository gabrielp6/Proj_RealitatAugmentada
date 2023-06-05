using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class canviescena : MonoBehaviour
{
    // Start is called before the first frame update
    /*void Start()
    {
        
    }*/

    public void escenaMapa()
    {
        SceneManager.LoadScene("Mapa", LoadSceneMode.Single);


    }

    public void escenaPrincipal()
    {
        SceneManager.LoadScene("menuprincipal", LoadSceneMode.Single);


    }



    // Update is called once per frame
    /*void Update()
    {
        
    }*/
}
