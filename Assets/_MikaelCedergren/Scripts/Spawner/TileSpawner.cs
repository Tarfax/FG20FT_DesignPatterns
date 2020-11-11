using MC_Utility.EventSystem;
using UnityEngine;
using UnityEngine.UIElements;

public class TileSpawner {

    private int width;
    private int height;

    private int i = 0;
    private int j = 0;

    private int counter = 0;

    private int spawnEachFrame;
    public TileSpawner(int width, int height, int frameWaiter) {
        this.width = width;
        this.height = height;
        spawnEachFrame = frameWaiter;
        foreach (Tile tile in Tile.GetTiles()) {
            tile.Destroy();
        }

        EventSystem<UpdateEvent>.RegisterListener(Update);
    }

    private void Update(UpdateEvent updateEvent) {
        if (counter > 0 && counter % spawnEachFrame == 0) {
            counter++;
        }
        else {
            counter++;
            return;
        }

        if (FloodFillDiagnoal() == true) {
            EventSystem<UpdateEvent>.UnregisterListener(Update);
        }

    }

    private bool FloodFillDiagnoal() {
        for (int x = i, y = j; y <= i; y++, x--) {
            Factory.CreateInstance<Tile>(new Vector3(x, 0f, y));
        }

        if ((i + 1) < width) {
            i++;
            return false;
        }
        else if ((j + 1) < height) {
            j++;
            return false;
        }

        return true;
    }

}
