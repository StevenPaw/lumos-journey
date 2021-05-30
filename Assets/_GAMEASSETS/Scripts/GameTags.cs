using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameTags
{
    /// <summary>
    /// This class will hold variables and methods related to Tags
    /// We choose a static class, because it doesn't need to be initialized
    /// In a static class all fields and methods need to be static
    /// </summary>
     public static string PLAYER = "Player";

     public static string INTERACTABLEOBJECT = "InteractableObject";

     public static string JUMPPLATTFORM = "JumpPlatform";

   /// <summary>
   /// A static method to find a GameObject by tag
   /// this method seeks only for Gameobjects with that specific Player-tag
   /// </summary>
   /// <returns></returns>
    public static GameObject FindPlayer()
    {
        return GameObject.FindWithTag(PLAYER);
    }

    public static GameObject FindInteractableObject()
    {
        return GameObject.FindWithTag(INTERACTABLEOBJECT);
    }

    public static GameObject FindJumpPlatform()
    {
        return GameObject.FindWithTag(JUMPPLATTFORM);
    }

}
