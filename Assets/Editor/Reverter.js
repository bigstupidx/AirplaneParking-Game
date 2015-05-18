 @MenuItem ("Tools/Revert to Prefab %r")
static function Revert() {
    var selection = Selection.gameObjects;
 
    if (selection.length > 0) {
        for (var i : int = 0; i < selection.length; i++) {
            PrefabUtility.RevertPrefabInstance(selection[i]);
        }
} else {
         Debug.Log("Cannot revert to prefab - nothing selected");
}
}