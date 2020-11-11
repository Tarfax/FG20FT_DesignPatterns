using System;
using UnityEngine;

[CreateAssetMenu(fileName = "FactoryDefintion_", menuName = "FactoryDefinition")]
public class FactoryDefinition : ScriptableObject {

    public FactoryDefinitionType FactoryType;
    public FactoryObject FactoryObject;

    public static Type GetPrefabType(FactoryDefinitionType type) {
        switch (type) {
            case FactoryDefinitionType.Tile:
                return typeof(Tile);
            case FactoryDefinitionType.Hero_Brute:
                return typeof(FactoryObject_Character);
            case FactoryDefinitionType.Enemy_Cute:
                return typeof(FactoryObject_Enemy);
            case FactoryDefinitionType.TextAboveCharacter:
                return typeof(UI_TextMesh);
            default:
                throw new Exception("Type does not exist");
        }
    }

}
