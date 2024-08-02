using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Syringe"))
        {
            SyringeManager.instance.TakeSyringe(other.gameObject);
        }

        if (other.CompareTag("MoleLevelEndCoroutine"))
        {
            if(SyringeManager.instance.syringeNumber == 3)
            {
                //level bitti yeni level
            }

            else
            {   
                //t�m ��r�ngalar� topla komutu
            }
        }

        if (other.CompareTag("LotusWater"))
        {
            // checkpoint system son check point
            print("lotus failed");
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("ClimbDetector"))
        {
            print("press F");  
            
            if(Input.GetKeyDown(KeyCode.F))
            {
                PlayerController.instance.isWalking = false;
                PlayerController.instance.isRunning = false;
                PlayerController.instance.isClimbing = true;

                //Press F yaz�s� falan yaz�labilir
            }
        }

        if (other.CompareTag("MoleSceneDetector"))
        {
            print("press F");

            if (Input.GetKeyDown(KeyCode.F)) //k�stebek sahnesine ge�mek i�in t�m gereklililkler kar��laan�yorsa 
            {
                //k�stebek sahnesi y�kle
            }
        }
    }
}
