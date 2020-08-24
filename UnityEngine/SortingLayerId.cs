using System;

namespace UnityEngine
{
    [Serializable]
    public struct SortingLayerId : IEquatable<SortingLayerId>
    {
        public int id;

        public int layer
            => SortingLayer.GetLayerValueFromID(this.id);

        public string name
            => SortingLayer.IDToName(this.id);

        public SortingLayerId(int value)
        {
            this.id = value;
        }

        public SortingLayerId(string name)
        {
            this.id = SortingLayer.NameToID(name);
        }

        public override int GetHashCode()
            => this.id.GetHashCode();

        public override bool Equals(object obj)
            => obj is SortingLayerId other && this.id == other.id;

        public bool Equals(SortingLayerId other)
            => this.id == other.id;

        public override string ToString()
            => this.id.ToString();

        public static implicit operator int(SortingLayerId value)
            => SortingLayer.GetLayerValueFromID(value.id);

        public static implicit operator SortingLayerId(int value)
            => new SortingLayerId(value);

        public static implicit operator SortingLayerId(string value)
            => new SortingLayerId(value);

        public static bool operator ==(SortingLayerId lhs, SortingLayerId rhs)
            => lhs.id == rhs.id;

        public static bool operator !=(SortingLayerId lhs, SortingLayerId rhs)
            => lhs.id != rhs.id;
    }
}