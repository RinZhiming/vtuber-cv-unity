using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Utility
{
    public static class ToggleGroupExtension
    {
        public static List<Toggle> GetToggles(this ToggleGroup group)
        {
            var allToggle = Object.FindObjectsOfType<Toggle>();
            var childToggle = allToggle.Where(t => t.group == group).ToList();
            return childToggle;
        }
        
        public static List<Toggle> GetChildToggles(this ToggleGroup group)
        {
            var allToggle = group.GetComponentsInChildren<Toggle>();
            var childToggle = allToggle.Where(t => t.group == group).ToList();
            return childToggle;
        }

        public static int GetIndexByGlobalToggle(this ToggleGroup group, Toggle toggle)
        {
            var toggles = group.GetToggles();
            
            for (var i = 0; i < toggles.Count; i++)
            {
                if (toggle == toggles[i]) 
                    return i;
            }

            return -1;
        }
        
        public static int GetIndexByChildrenToggle(this ToggleGroup group, Toggle toggle)
        {
            var toggles = group.GetChildToggles();
            
            for (var i = 0; i < toggles.Count; i++)
            {
                if (toggle == toggles[i])
                    return i;
            }

            return -1;
        }
        
        public static Toggle GetToggleByGlobalIndex(this ToggleGroup group, int index)
        {
            var toggles = group.GetToggles();
            return toggles[index];
        }
        
        public static Toggle GetToggleByChildrenIndex(this ToggleGroup group, int index)
        {
            var toggles = group.GetChildToggles();
            return toggles[index];
        }
    }
}