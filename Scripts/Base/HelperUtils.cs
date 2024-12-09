using UnityEngine;


namespace xb.utils {
    public static class HelperUtils {


        public static Vector3 ScreenToWorldPosition(Vector3 position) {
            return Camera.main.ScreenToWorldPoint(position);
        }

        public static bool GetScreenPoint(out Vector3 position, float distance = Mathf.Infinity, int layerMask = 0) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, distance, layerMask)) {
                position = hit.point;
                return true;
            }
            position = default;
            return false;
        }

        public static Component GetScreenPointObject<Component>(float distance = Mathf.Infinity,int layerMask = 0) where Component:MonoBehaviour{
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, distance, layerMask)) {
                return  hit.collider.gameObject.GetComponent<Component>();
            }
            return null;
        }

        public static bool GetScreenPointObject<Component>(out Component obj, float distance = Mathf.Infinity, int layerMask = 0) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, distance, layerMask)) {
                obj = hit.collider.gameObject.GetComponent<Component>();
                return true;
            }
            obj = default;
            return false;
        }


        public static Bounds CalculateBounds(GameObject obj, bool recursive = false) {


            // 获取当前对象及其子对象所有Renderer的包围盒
            Renderer[] renderers = recursive ? obj.GetComponentsInChildren<Renderer>() : new Renderer[] { obj.GetComponent<Renderer>() };

            if (renderers.Length == 0) {
                // 如果没有Renderer，返回一个空Bounds
                return new Bounds(obj.transform.position, Vector3.zero);
            }

            // 初始化包围盒为第一个Renderer的bounds
            Bounds bounds = renderers[0].bounds;

            // 将所有子对象的包围盒包含进来
            for (int i = 1; i < renderers.Length; i++) {
                bounds.Encapsulate(renderers[i].bounds);
            }

            return bounds;
        }
    }

}

