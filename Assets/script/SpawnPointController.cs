using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointController : MonoBehaviour
{
   Renderer m_Render;
   UnityEngine.Camera cam1;
   
   void Start()
   {
	   cam1 = Camera.main;
   }
   
   public bool IsVisible() // returns true if the object is in camera view
   {
	   return cam1.IsObjectVisible(GetComponent<SpriteRenderer>());
   }
}
