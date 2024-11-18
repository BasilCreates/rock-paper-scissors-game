using UnityEngine;

public class ScissorThrower : MonoBehaviour
{
    // Reference to the Animator component
    private Animator animator;

    void Start()
    {
        // Get the Animator component attached to the GameObject
        animator = GetComponent<Animator>();
        print("grabbed the animator, "+ animator);
    }

    void Update()
    {
        // Check if the F key is pressed
        if (Input.GetKeyDown(KeyCode.F))
        {
            // Trigger the "Scissor attack" animation
            GetComponent<Animator>().SetTrigger("ScissorAttack");
        }
    }
}
