using System;
using UnityEngine;

namespace xb.gridsystem {

    public class GridSystem<T> {

        public static readonly GridPosition No_Position = new GridPosition(-1, -1);

        private int width;
        private int height;
        private float cellSize;

        private T[,] gridObjectArray;

        private Vector3 origin;

        public GridSystem(Vector3 origin, int width, int height, float cellSize, Func<GridSystem<T>, GridPosition, T> createGridObject) {
            this.origin = origin;
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;

            gridObjectArray = new T[width, height];

            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    GridPosition gridPosition = new GridPosition(x, y);
                    gridObjectArray[x, y] = createGridObject(this, gridPosition);

                }
            }
        }

        public Vector3 GetGridWorldPosition(GridPosition gridPosition) {
            return new Vector3(gridPosition.x, gridPosition.y, 0) * cellSize + origin;
        }

        public GridPosition GetGridPosition(Vector3 worldPosition) {
            worldPosition = worldPosition - origin + cellSize * 0.5f * new Vector3(1, 1, 0);
            int x = Mathf.FloorToInt(worldPosition.x / cellSize);
            int y = Mathf.FloorToInt(worldPosition.y / cellSize);
            return new GridPosition(x, y);
        }


        private void CreateDebugInfos() {

            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    GridPosition gridPosition = new GridPosition(x, y);

                    Vector3 leftBottom = GetGridWorldPosition(gridPosition);
                    Vector3 rightBottom = GetGridWorldPosition(new GridPosition(x + 1, y));
                    Vector3 leftTop = GetGridWorldPosition(new GridPosition(x, y + 1));
                    Vector3 rightTop = GetGridWorldPosition(new GridPosition(x + 1, y + 1));

                    int duration = 10000;
                    Color color = Color.white;
                    // 左右
                    Debug.DrawLine(leftBottom, rightBottom, color, duration);
                    // 左上
                    Debug.DrawLine(leftBottom, leftTop, color, duration);
                    if (x == width - 1) {
                        Debug.DrawLine(rightBottom, rightTop, color, duration);
                    }
                    if (y == height - 1) {
                        Debug.DrawLine(leftTop, rightTop, color, duration);
                    }

                }
            }
        }

        public int GetWidth() => width;
        public int GetHeight() => height;
        public float GetCellSize() => cellSize;
        public Vector3 GetOrigin() => origin;

        public bool IsValid(GridPosition gridPosition) => IsValid(gridPosition.x, gridPosition.y);

        public bool IsValid(int x, int y) {
            return x >= 0 && x < width
             && y >= 0 && y < height;
        }

        public T this[int row, int column] {
            get {
                return gridObjectArray[row, column];
            }
            set {
                gridObjectArray[row, column] = value;
            }
        }

        public T this[GridPosition gridPosition] {
            get {
                return gridObjectArray[gridPosition.x, gridPosition.y];
            }
            set {
                gridObjectArray[gridPosition.x, gridPosition.y] = value;
            }
        }


    }

}