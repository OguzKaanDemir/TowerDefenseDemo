using UnityEngine;
using UnityEditor;
using Scripts.Editor.Interfaces;

namespace Scripts.Editor.ObjectManagement
{
    public class ObjectSpawner : IObjectSpawner
    {
        public void SpawnObject(SceneView sceneView, IObjectDeleter objectDeleter, GameObject prefab, Vector3 position)
        {
            objectDeleter.DeleteObject(sceneView, position);

            var instance = (GameObject)PrefabUtility.InstantiatePrefab(prefab, GameObject.Find("Map").transform);
            if (instance == null)
            {
                Debug.LogError("Please select an object");
                return;
            }

            instance.transform.position = position + Vector3.one * 0.5f;
            Undo.RegisterCreatedObjectUndo(instance, "Create Map Object");
        }

    }
}
