using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
                //CheckPointSystem.instance.SetNextMissionTrue();
                SceneManagment.instance.LoadMainScene1();
                //level bitti yeni level
            }

            else
            {   
                //t�m ��r�ngalar� topla komutu
            }
        }

        if (other.CompareTag("LotusWater"))
        {
            SceneManagment.instance.ReloadScene();
        }

        if (other.CompareTag("BridgeMissionDetector"))
        {
            other.gameObject.SetActive(false);
            //CheckPointSystem.instance.SetNextMissionTrue();
        }

        if (other.CompareTag("SyringeBag"))
        {
            //CheckPointSystem.instance.SetNextMissionTrue();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("ClimbDetector"))
        {   

            if(Input.GetKeyDown(KeyCode.F))
            {
                PlayerController.instance.isWalking = false;
                PlayerController.instance.isRunning = false;
                PlayerController.instance.isClimbing = true;

            }
        }

        if (other.CompareTag("MoleSceneDetector"))
        {

            if (Input.GetKeyDown(KeyCode.F)) //k�stebek sahnesine ge�mek i�in t�m gereklililkler kar��laan�yorsa 
            {
                SceneManagment.instance.LoadMoleScene();
            }
        }
    }
}
