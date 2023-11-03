using UnityEngine;

public class ColliderCeller : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CapsuleCollider playerCollider = other.GetComponent<CapsuleCollider>();
            if (playerCollider != null)
            {
                CapsuleCollider clonedCollider = gameObject.AddComponent<CapsuleCollider>();
                clonedCollider.center = playerCollider.center;
                clonedCollider.radius = playerCollider.radius;
                clonedCollider.height = playerCollider.height;
                clonedCollider.direction = playerCollider.direction;
                clonedCollider.isTrigger = playerCollider.isTrigger;
                // Вы можете установить другие необходимые свойства клонированного коллайдера здесь

                    CapsuleCollider cc = playerCollider.GetComponent<CapsuleCollider>();

                    if (cc != null)
                    {
                        CapsuleCollider[] caps = new CapsuleCollider[1];
                        caps[0] = cc;
                        print("Have cc.");
                        GetComponent<Cloth>().capsuleColliders = caps;
                    }
                    else print("Didn't get cc.");
                    
                    
                
            }
        }
    }
}



















