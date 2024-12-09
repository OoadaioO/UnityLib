using System;


namespace xb.gridsystem {
    public struct GridPosition : IEquatable<GridPosition> {

        public int x;
        public int y;

        public GridPosition(int x, int y) {
            this.x = x;
            this.y = y;
        }

        public override readonly bool Equals(object obj) {
            return obj is GridPosition position &&
            position.x == x && position.y == y;
        }

        public readonly bool Equals(GridPosition other) {
            return this == other;
        }

        public override readonly int GetHashCode() {
            return HashCode.Combine(x, y);
        }

        public override readonly string ToString() {
            return $"{x},{y}";
        }

        public static bool operator ==(GridPosition a, GridPosition b) {
            return a.x == b.x && a.y == b.y;
        }
        public static bool operator !=(GridPosition a, GridPosition b) {
            return !(a == b);
        }

        public static GridPosition operator +(GridPosition a, GridPosition b) {
            return new GridPosition(a.x + b.x, a.y + b.y);
        }
        public static GridPosition operator -(GridPosition a, GridPosition b) {
            return new GridPosition(a.x - b.x, a.y - b.y);
        }
    }
}