using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/*
 * This component is used to hold data about the hand model and it
 * should be attached to both hand model objects.
 */
public class HandData : MonoBehaviour
{
    public enum HandModelType { Left, Right }

    // Identify which hand this component is attached to
    public HandModelType handType;

    // Reference to the object that holds this component
    public Transform root;

    // Hand model animator
    public Animator animator;

    // List of references to all of the finger bones in the model
    public Transform[] fingerBones;
}
