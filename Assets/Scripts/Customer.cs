using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    // Customer preferences are on a scale of 0-100
    private int maxAttr = 100;
    private int rand;

    public float cozy;
    public float romantic;
    public float chaotic;
    public float satisfaction;

    void Update() {
        if (Input.GetButtonDown("Submit")) {
            Debug.Log("Generating Customer Stats");
            // spawnCustomer()
            generateCustomerStats();
        }
    }

    public void generateCustomerStats() {
        // TODO: Add Synergies between customer stats ?
        cozy = Random.Range(0, maxAttr);
        romantic = Random.Range(0, maxAttr);
        chaotic = Random.Range(0, maxAttr);
        Debug.Log("Cozy: " + cozy + " Romantic: " + romantic + " Chaotic: " + chaotic);
    }

    public void generateReview() {
        // Customer gives better reviews the better their preferences are catered to
        attributes = [cozy, romantic, chaotic]
        for (int i=0; i<len(attributes); i++) {
            if (attributes[i] <= 25) {
                // bad
            }
            else if (25 < attributes[i] <= 50) {
                // medium
            }
            else if (50 < attributes[i] < 75) {
                // good 
            }
            else { // 75 or higher preference
                // very good
            }
        }
        cozyScore = GameManager.instance.cozy * cozy/100;
        romanticScore = GameManager.instance.romantic * romantic/100;
        chaoticScore = GameManager.instance.chaoticScore * chaotic/100;
        //GameManager.instance.cozy
        //GameManager.instance.romantic
        //Gamemanager.instance.chaotic
    }
}
