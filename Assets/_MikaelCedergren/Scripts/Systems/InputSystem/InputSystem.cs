using MC_Utility.EventSystem;
using UnityEngine;

public class InputSystem {

    private Camera camera;
    private static InputSystem instance;

    public InputSystem(Camera camera) {
        if (instance == null) {
            instance = this;
            this.camera = camera;
            EventSystem<UpdateEvent>.RegisterListener(OnUpdate);
        }
    }

    public void OnUpdate(UpdateEvent updateEvent) {

    }

    public static Vector3 GetMousePositionInWorld(LayerMask layerMask) {
        Ray ray = instance.camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 100f, layerMask) == true) {
            return hitInfo.point;
        }
        return new Vector3(-10f, -10f, -10f);
        //return instance.camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 50f));
    }

    public void Destroy() {
        EventSystem<UpdateEvent>.UnregisterListener(OnUpdate);
    }

}
