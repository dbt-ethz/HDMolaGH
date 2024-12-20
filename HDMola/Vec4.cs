﻿// Unity C# reference source
// Copyright (c) Unity Technologies. For terms of use, see
// https://unity3d.com/legal/licenses/Unity_Reference_Only_License

using System;
using System.Globalization;

namespace Mola
{

    // Representation of four-dimensional vectors.
    public partial struct Vec4 : IEquatable<Vec4>, IFormattable
    {
        // *undocumented*
        public const float kEpsilon = 0.00001F;

        // X component of the vector.
        public float x;
        // Y component of the vector.
        public float y;
        // Z component of the vector.
        public float z;
        // W component of the vector.
        public float w;

        // Access the x, y, z, w components using [0], [1], [2], [3] respectively.
        public float this[int index]
        {

            get
            {
                switch (index)
                {
                    case 0: return x;
                    case 1: return y;
                    case 2: return z;
                    case 3: return w;
                    default:
                        throw new IndexOutOfRangeException("Invalid Vector4 index!");
                }
            }


            set
            {
                switch (index)
                {
                    case 0: x = value; break;
                    case 1: y = value; break;
                    case 2: z = value; break;
                    case 3: w = value; break;
                    default:
                        throw new IndexOutOfRangeException("Invalid Vector4 index!");
                }
            }
        }

        // Creates a new vector with given x, y, z, w components.

        public Vec4(float x, float y, float z, float w) { this.x = x; this.y = y; this.z = z; this.w = w; }
        // Creates a new vector with given x, y, z components and sets /w/ to zero.

        public Vec4(float x, float y, float z) { this.x = x; this.y = y; this.z = z; this.w = 0F; }
        // Creates a new vector with given x, y components and sets /z/ and /w/ to zero.

        public Vec4(float x, float y) { this.x = x; this.y = y; this.z = 0F; this.w = 0F; }

        // Set x, y, z and w components of an existing Vector4.

        public void Set(float newX, float newY, float newZ, float newW) { x = newX; y = newY; z = newZ; w = newW; }

        // Linearly interpolates between two vectors.

        public static Vec4 Lerp(Vec4 a, Vec4 b, float t)
        {
            t = Mathf.Clamp01(t);
            return new Vec4(
                a.x + (b.x - a.x) * t,
                a.y + (b.y - a.y) * t,
                a.z + (b.z - a.z) * t,
                a.w + (b.w - a.w) * t
            );
        }

        // Linearly interpolates between two vectors without clamping the interpolant

        public static Vec4 LerpUnclamped(Vec4 a, Vec4 b, float t)
        {
            return new Vec4(
                a.x + (b.x - a.x) * t,
                a.y + (b.y - a.y) * t,
                a.z + (b.z - a.z) * t,
                a.w + (b.w - a.w) * t
            );
        }

        // Moves a point /current/ towards /target/.

        public static Vec4 MoveTowards(Vec4 current, Vec4 target, float maxDistanceDelta)
        {
            float toVector_x = target.x - current.x;
            float toVector_y = target.y - current.y;
            float toVector_z = target.z - current.z;
            float toVector_w = target.w - current.w;

            float sqdist = (toVector_x * toVector_x +
                toVector_y * toVector_y +
                toVector_z * toVector_z +
                toVector_w * toVector_w);

            if (sqdist == 0 || (maxDistanceDelta >= 0 && sqdist <= maxDistanceDelta * maxDistanceDelta))
                return target;

            var dist = (float)Math.Sqrt(sqdist);

            return new Vec4(current.x + toVector_x / dist * maxDistanceDelta,
                current.y + toVector_y / dist * maxDistanceDelta,
                current.z + toVector_z / dist * maxDistanceDelta,
                current.w + toVector_w / dist * maxDistanceDelta);
        }

        // Multiplies two vectors component-wise.

        public static Vec4 Scale(Vec4 a, Vec4 b)
        {
            return new Vec4(a.x * b.x, a.y * b.y, a.z * b.z, a.w * b.w);
        }

        // Multiplies every component of this vector by the same component of /scale/.

        public void Scale(Vec4 scale)
        {
            x *= scale.x;
            y *= scale.y;
            z *= scale.z;
            w *= scale.w;
        }

        // used to allow Vector4s to be used as keys in hash tables

        public override int GetHashCode()
        {
            return x.GetHashCode() ^ (y.GetHashCode() << 2) ^ (z.GetHashCode() >> 2) ^ (w.GetHashCode() >> 1);
        }

        // also required for being able to use Vector4s as keys in hash tables

        public override bool Equals(object other)
        {
            if (!(other is Vec4)) return false;

            return Equals((Vec4)other);
        }


        public bool Equals(Vec4 other)
        {
            return x == other.x && y == other.y && z == other.z && w == other.w;
        }

        // *undoc* --- we have normalized property now

        public static Vec4 Normalize(Vec4 a)
        {
            float mag = Magnitude(a);
            if (mag > kEpsilon)
                return a / mag;
            else
                return zero;
        }

        // Makes this vector have a ::ref::magnitude of 1.

        public void Normalize()
        {
            float mag = Magnitude(this);
            if (mag > kEpsilon)
                this /= mag;
            else
                this = zero;
        }

        // Returns this vector with a ::ref::magnitude of 1 (RO).
        public Vec4 normalized
        {

            get
            {
                return Vec4.Normalize(this);
            }
        }

        // Dot Product of two vectors.

        public static float Dot(Vec4 a, Vec4 b) { return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w; }

        // Projects a vector onto another vector.

        public static Vec4 Project(Vec4 a, Vec4 b) { return b * (Dot(a, b) / Dot(b, b)); }

        // Returns the distance between /a/ and /b/.

        public static float Distance(Vec4 a, Vec4 b) { return Magnitude(a - b); }

        // *undoc* --- there's a property now

        public static float Magnitude(Vec4 a) { return (float)Math.Sqrt(Dot(a, a)); }

        // Returns the length of this vector (RO).
        public float magnitude
        {

            get { return (float)Math.Sqrt(Dot(this, this)); }
        }

        // Returns the squared length of this vector (RO).
        public float sqrMagnitude
        {

            get { return Dot(this, this); }
        }

        // Returns a vector that is made from the smallest components of two vectors.

        public static Vec4 Min(Vec4 lhs, Vec4 rhs)
        {
            return new Vec4(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y), Mathf.Min(lhs.z, rhs.z), Mathf.Min(lhs.w, rhs.w));
        }

        // Returns a vector that is made from the largest components of two vectors.

        public static Vec4 Max(Vec4 lhs, Vec4 rhs)
        {
            return new Vec4(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y), Mathf.Max(lhs.z, rhs.z), Mathf.Max(lhs.w, rhs.w));
        }

        static readonly Vec4 zeroVector = new Vec4(0F, 0F, 0F, 0F);
        static readonly Vec4 oneVector = new Vec4(1F, 1F, 1F, 1F);
        static readonly Vec4 positiveInfinityVector = new Vec4(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
        static readonly Vec4 negativeInfinityVector = new Vec4(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);

        // Shorthand for writing @@Vector4(0,0,0,0)@@
        public static Vec4 zero { get { return zeroVector; } }
        // Shorthand for writing @@Vector4(1,1,1,1)@@
        public static Vec4 one { get { return oneVector; } }
        // Shorthand for writing @@Vec3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity)@@
        public static Vec4 positiveInfinity { get { return positiveInfinityVector; } }
        // Shorthand for writing @@Vec3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity)@@
        public static Vec4 negativeInfinity { get { return negativeInfinityVector; } }

        // Adds two vectors.

        public static Vec4 operator +(Vec4 a, Vec4 b) { return new Vec4(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w); }
        // Subtracts one vector from another.

        public static Vec4 operator -(Vec4 a, Vec4 b) { return new Vec4(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w); }
        // Negates a vector.

        public static Vec4 operator -(Vec4 a) { return new Vec4(-a.x, -a.y, -a.z, -a.w); }
        // Multiplies a vector by a number.

        public static Vec4 operator *(Vec4 a, float d) { return new Vec4(a.x * d, a.y * d, a.z * d, a.w * d); }
        // Multiplies a vector by a number.

        public static Vec4 operator *(float d, Vec4 a) { return new Vec4(a.x * d, a.y * d, a.z * d, a.w * d); }
        // Divides a vector by a number.

        public static Vec4 operator /(Vec4 a, float d) { return new Vec4(a.x / d, a.y / d, a.z / d, a.w / d); }

        // Returns true if the vectors are equal.

        public static bool operator ==(Vec4 lhs, Vec4 rhs)
        {
            // Returns false in the presence of NaN values.
            float diffx = lhs.x - rhs.x;
            float diffy = lhs.y - rhs.y;
            float diffz = lhs.z - rhs.z;
            float diffw = lhs.w - rhs.w;
            float sqrmag = diffx * diffx + diffy * diffy + diffz * diffz + diffw * diffw;
            return sqrmag < kEpsilon * kEpsilon;
        }

        // Returns true if vectors are different.

        public static bool operator !=(Vec4 lhs, Vec4 rhs)
        {
            // Returns true in the presence of NaN values.
            return !(lhs == rhs);
        }

        // Converts a [[Vec3]] to a Vector4.

        public static implicit operator Vec4(Vec3 v)
        {
            return new Vec4(v.x, v.y, v.z, 0.0F);
        }

        // Converts a Vector4 to a [[Vec3]].

        public static implicit operator Vec3(Vec4 v)
        {
            return new Vec3(v.x, v.y, v.z);
        }






        public override string ToString()
        {
            return ToString(null, null);
        }


        public string ToString(string format)
        {
            return ToString(format, null);
        }


        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (string.IsNullOrEmpty(format))
                format = "F2";
            if (formatProvider == null)
                formatProvider = CultureInfo.InvariantCulture.NumberFormat;
            return x.ToString(format, formatProvider) + "" + y.ToString(format, formatProvider) + " " + z.ToString(format, formatProvider) + " " + w.ToString(format, formatProvider);
        }

        // *undoc* --- there's a property now

        public static float SqrMagnitude(Vec4 a) { return Vec4.Dot(a, a); }
        // *undoc* --- there's a property now

        public float SqrMagnitude() { return Dot(this, this); }
    }
}
