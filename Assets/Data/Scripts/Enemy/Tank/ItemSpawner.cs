using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// enum itemType
// {
//     ammo,
//     health,
//     bomb
// }

public class ItemSpawner : MonoBehaviour
{
    public GameObject item;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void spawnItem()
    {
        GameObject spawnedItem = Instantiate(item,this.transform.position + new Vector3(1,0.5f, 0) , this.transform.rotation);
    }

}
