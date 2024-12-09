using System;

namespace xb.gridsystem {
    public struct GridPositionXZ : IEquatable<GridPositionXZ> {

        public int x;
        public int z;

        public GridPositionXZ(int x, int z) {
            this.x = x;
            this.z = z;
        }

        public override readonly bool Equals(object obj) {
            return obj is GridPositionXZ position &&
            position.x == x && position.z == z;
        }

        public readonly bool Equals(GridPositionXZ other) {
            return this == other;
        }

        public override readonly int GetHashCode() {
            return HashCode.Combine(x, z);
        }

        public override readonly string ToString() {
            return $"{x},{z}";
        }

        public static bool operator ==(GridPositionXZ a, GridPositionXZ b) {
            return a.x == b.x && a.z == b.z;
        }
        public static bool operator !=(GridPositionXZ a, GridPositionXZ b) {
            return !(a == b);
        }

        public static GridPositionXZ operator +(GridPositionXZ a, GridPositionXZ b) {
            return new GridPositionXZ(a.x + b.x, a.z + b.z);
        }
        public static GridPositionXZ operator -(GridPositionXZ a, GridPositionXZ b) {
            return new GridPositionXZ(a.x - b.x, a.z - b.z);
        }
    }
}