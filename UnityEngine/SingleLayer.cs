/******************************************************************************
 * Copyright (C) Leap Motion, Inc. 2011-2017.                                 *
 * Leap Motion proprietary and  confidential.                                 *
 *                                                                            *
 * Use subject to the terms of the Leap Motion SDK Agreement available at     *
 * https://developer.leapmotion.com/sdk_agreement, or another agreement       *
 * between Leap Motion and you, your company or other organization.           *
 ******************************************************************************/

using System;

namespace UnityEngine
{
    /// <summary>
    /// An object you can use to represent a single Unity layer
    /// as a dropdown in the inspector.  Can be converted back and
    /// forth between the integer representation Unity usually
    /// uses in its own methods.
    /// </summary>
    [Serializable]
    public struct SingleLayer : IEquatable<SingleLayer>
    {
        public int value;

        public int mask
        {
            get
            {
                return 1 << this.value;
            }

            set
            {
                if (value == 0)
                {
                    throw new ArgumentException("Single layer can only represent exactly one layer.  The provided mask represents no layers (mask was zero).");
                }

                var newIndex = 0;

                while ((value & 1) == 0)
                {
                    value >>= 1;
                    newIndex++;
                }

                if (value != 1)
                {
                    throw new ArgumentException("Single layer can only represent exactly one layer.  The provided mask represents more than one layer.");
                }

                this.value = newIndex;
            }
        }

        public SingleLayer(int value)
        {
            this.value = value;
        }

        public SingleLayer(string name)
        {
            this.value = LayerMask.NameToLayer(name);
        }

        public override int GetHashCode()
            => this.value.GetHashCode();

        public override bool Equals(object obj)
            => obj is SingleLayer other && this.value == other.value;

        public bool Equals(SingleLayer other)
            => this.value == other.value;

        public override string ToString()
            => LayerMask.LayerToName(this.value);

        public static implicit operator int(in SingleLayer value)
            => value.value;

        public static implicit operator SingleLayer(int value)
            => new SingleLayer(value);

        public static implicit operator SingleLayer(string name)
            => new SingleLayer(name);

        public static bool operator ==(in SingleLayer lhs, in SingleLayer rhs)
            => lhs.value == rhs.value;

        public static bool operator !=(in SingleLayer lhs, in SingleLayer rhs)
            => lhs.value != rhs.value;
    }
}