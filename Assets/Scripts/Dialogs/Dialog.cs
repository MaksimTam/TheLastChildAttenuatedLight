using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Dialogs
{
   [CreateAssetMenu(fileName = "NewDialog")]
   public class Dialog : ScriptableObject
   {
      public List<string> messages;
      
      //ID generated automatic
      public string uid;
      private void OnValidate()
      {
#if UNITY_EDITOR
         if (uid == "")
         {
            uid = GUID.Generate().ToString();
            UnityEditor.EditorUtility.SetDirty(this);
         }
#endif
      }
   }
}
