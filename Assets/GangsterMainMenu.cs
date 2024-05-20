using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GangsterMainMenu : MonoBehaviour {
    
    private Killable killable;

    // Start is called before the first frame update
    void Start()
    {
        killable = this.gameObject.GetComponent<Killable>();

        StartCoroutine(MoveGangster());
        
    }

    IEnumerator MoveGangster() {
        while (true) {
            yield return new WaitForSeconds(Random.Range(0.5f, 2.5f));
                
                killable.MenuDie();
            yield return new WaitForSeconds(1.0f);
                killable.MenuReset();

        }
    }
}
