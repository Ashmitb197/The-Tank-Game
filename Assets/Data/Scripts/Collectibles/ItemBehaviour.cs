using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType
{
    bomb,
    health,
    ammo
};

public class ItemBehaviour : MonoBehaviour
{
    // Start is called before the first frame update

    public int collectibleItem;
    void Start()
    {
        collectibleItem = Random.Range(0, sizeof(ItemType)-1);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(0,1,0);

        itemTypeTagUpdation();
    }


    void itemTypeTagUpdation()
    {
        switch(collectibleItem)
        {
            case 0:
                gameObject.tag = "Health";
                break;
            
            case 1:
                gameObject.tag = "Ammo";
                break;

            case 3:
                gameObject.tag = "Shell";
                break;

            default:
                gameObject.tag = "Health";
                break;
        }
    }
    
    void OnTriggerEnter(Collider coll)
    {
        if(coll.tag == "Player")
        {
            this.GetComponent<Animator>().SetBool("StartDying", true);
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
