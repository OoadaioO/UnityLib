using System;

using UnityEngine;

namespace xb.gridsystem {
    public class GridSystemXZ<T> {

        public static readonly GridPositionXZ No_Position = new GridPositionXZ(-1, -1);

        private int width;
        private int height;
        private float cellSize;

        private T[,] gridObjectArray;

        private Vector3 origin;

        public GridSystemXZ(Vector3 origin, int width, int height, float cellSize, Func<GridSystemXZ<T>, GridPositionXZ, T> createGridObject) {
            this.origin = origin;
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;

            gridObjectArray = new T[width, height];

            for (int x = 0; x < width; x++) {
                for (int z = 0; z < height; z++) {
                    GridPositionXZ gridPosition = new GridPositionXZ(x, z);
                    gridObjectArray[x, z] = createGridObject(this, gridPosition);

                }
            }

            CreateDebugInfos();
        }

        public Vector3 GetGridWorldPosition(GridPositionXZ gridPosition) {
            return new Vector3(gridPosition.x, 0, gridPosition.z) * cellSize + origin;
        }

        public Bounds GetBounds(GridPositionXZ gridPositionXZ) {
            return new Bounds(
                    GetGridWorldPosition(gridPositionXZ) + cellSize * 0.5f * new Vector3(1, 0, 1), 
                    cellSize * Vector3.one
                );
        }

        public GridPositionXZ GetGridPosition(Vector3 worldPosition) {
            worldPosition = worldPosition - origin;
            int x = Mathf.FloorToInt(worldPosition.x / cellSize);
            int z = Mathf.FloorToInt(worldPosition.z / cellSize);
            return new GridPositionXZ(x, z);
        }


        private void CreateDebugInfos() {

            for (int x = 0; x < width; x++) {
                for (int z = 0; z < height; z++) {
                    GridPositionXZ gridPosition = new GridPositionXZ(x, z);

                    Vector3 leftBottom = GetGridWorldPosition(gridPosition);
                    Vector3 rightBottom = GetGridWorldPosition(new GridPositionXZ(x + 1, z));
                    Vector3 leftTop = GetGridWorldPosition(new GridPositionXZ(x, z + 1));
                    Vector3 rightTop = GetGridWorldPosition(new GridPositionXZ(x + 1, z + 1));

                    int duration = 10000;
                    Color color = Color.red;
                    // 左右
                    Debug.DrawLine(leftBottom, rightBottom, color, duration);
                    // 左上
                    Debug.DrawLine(leftBottom, leftTop, color, duration);
                    if (x == width - 1) {
                        Debug.DrawLine(rightBottom, rightTop, color, duration);
                    }
                    if (z == height - 1) {
                        Debug.DrawLine(leftTop, rightTop, color, duration);
                    }

                }
            }
        }

        public int GetWidth() => width;
        public int GetHeight() => height;
        public float GetCellSize() => cellSize;
        public Vector3 GetOrigin() => origin;

        public bool IsValid(GridPositionXZ gridPosition) => IsValid(gridPosition.x, gridPosition.z);

        public bool IsValid(int x, int z) {
            return x >= 0 && x < width
             && z >= 0 && z < height;
        }

        public T this[int row, int column] {
            get {
                return gridObjectArray[row, column];
            }
            set {
                gridObjectArray[row, column] = value;
            }
        }

        public T this[GridPositionXZ gridPosition] {
            get {
                return gridObjectArray[gridPosition.x, gridPosition.z];
            }
            set {
                gridObjectArray[gridPosition.x, gridPosition.z] = value;
            }
        }


    }
}