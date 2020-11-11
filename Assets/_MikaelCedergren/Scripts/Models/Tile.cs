using System.Collections.Generic;
using UnityEngine;

public class Tile  {
    private static Tile[,] tiles;

    public Vector3 Position { get; private set; }

    private GameObject gameObject;

    private BaseCharacter characterOnTile;

    private Material material;

    private bool isHovered;
    private Color hoverColor = new Color(0.55f, 1.0f, 0.71f);
    private bool isPreviewed;
    private Color previewColor = new Color(0f, 0.75f, 0f, 0.5f);

    private Color occupiedColor = new Color(1.0f, 0.6f, 0.6f);

    public Tile(Vector3 position) {
        if (tiles == null) {
            tiles = new Tile[GameInstance.Width, GameInstance.Height];
        }
        gameObject = Factory.CreateInstance<Tile>(position, Quaternion.Euler(Vector3.zero));
        gameObject.name = "Tile " + Mathf.RoundToInt(position.x) + "," + Mathf.RoundToInt(position.z);
        Position = position;
        tiles[Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.z)] = this;
        material = gameObject.GetComponentInChildren<MeshRenderer>().material;
    }

    public Tile[] GetNeighbours() {
        Tile[] tiles = new Tile[4];
        int x = Mathf.RoundToInt(Position.x);
        int y = Mathf.RoundToInt(Position.z);
        tiles[0] = GetTileAt(x, y + 1);
        tiles[1] = GetTileAt(x + 1, y);
        tiles[2] = GetTileAt(x, y - 1);
        tiles[3] = GetTileAt(x - 1, y);
        return tiles;
    }

    public void Hover() {
        isHovered = true;
        ChangeColor();
    }

    public void Unhover() {
        isHovered = false;
        ChangeColor();
    }

    public void IsPreview(bool preview) {
        isPreviewed = preview;
        ChangeColor();
    }

    public bool IsOccupied() {
        return characterOnTile != null;
    }

    public bool IsWalkable(BaseCharacter baseCharacter) {
        if (characterOnTile == baseCharacter) {
            return true;
        }
        else if (characterOnTile == null) {
            return true;
        }
        return false;
    }

    public void PreviewPath(bool show) {
        if (show == true) {
            material.color = Color.cyan;
            return;
        }
        ChangeColor();
    }

    public void Occupy(BaseCharacter character) {
        characterOnTile = character;
        if (character == null) {
            isPreviewed = false;
        }
        ChangeColor();
    }

    public Hero GetHero() {
        if (characterOnTile is Hero) {
            return characterOnTile as Hero;
        }
        return null;
    }

    public Enemy GetEnemy() {
        if (characterOnTile is Enemy) {
            return characterOnTile as Enemy;
        }
        return null;
    }

    private void ChangeColor() {
        if (isPreviewed == true) {
            material.color = previewColor;
        }
        else if (characterOnTile != null) {
            material.color = occupiedColor;
        }
        else if (isHovered == true) {
            material.color = hoverColor;
        }
        else {
            material.color = Color.white;
        }
    }

    public void Destroy() {
        GameObject.Destroy(gameObject);
        if (tiles != null) {
            tiles = null;
        }
    }

    public override string ToString() {
        return "Tile " + Position.x + "," + Position.z;
    }

    public static Tile GetTileAt(int x, int y) {
        if (tiles == null) {
            return null;
        }
        Tile tile = null;
        if (x >= 0 && x < GameInstance.Width && y >= 0 && y < GameInstance.Height) {
            tile = tiles[x, y];
        }
        return tile;
    }

    public static Tile GetTileAt(Vector3 position) {
        return GetTileAt((int)position.x, (int)position.z);
    }

    public static Tile GetRandomEnemyTile() {
        int width = GameInstance.Width;
        int height = GameInstance.Height;

        Tile tile = null;
        int count = 0;
        while (tile == null) {
            int y = Random.Range(width - 2, width);
            int x = Random.Range(0, height);

            tile = GetTileAt(x, y);
            if (tile.characterOnTile == null) {
                return tile;
            }
            else {
                tile = null;
            }

            count++;
            if (count > 20) {
                break;
            }
        }
        return tile;
    }

    public static IEnumerable<Tile> GetTiles() {
        if (tiles != null) {
            foreach (Tile tile in tiles) {
                if (tile != null)
                    yield return tile;
            }
        }
    }

}
