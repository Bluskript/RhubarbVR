﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MessagePack;

namespace RNumerics
{
	[MessagePackObject]
	public struct Line2d
	{
		[Key(0)]
		public Vector2d Origin;
		[Key(1)]
		public Vector2d Direction;

		public Line2d(Vector2d origin, Vector2d direction) {
			Origin = origin;
			Direction = direction;
		}

		public Line2d(ref Vector2d origin, ref Vector2d direction) {
			Origin = origin;
			Direction = direction;
		}

		public static Line2d FromPoints(Vector2d p0, Vector2d p1) {
			return new Line2d(p0, (p1 - p0).Normalized);
		}
		public static Line2d FromPoints(ref Vector2d p0, ref Vector2d p1) {
			return new Line2d(p0, (p1 - p0).Normalized);
		}

		// parameter is distance along Line
		public Vector2d PointAt(double d) {
			return Origin + (d * Direction);
		}

		public double Project(Vector2d p) {
			return (p - Origin).Dot(Direction);
		}

		public double DistanceSquared(Vector2d p) {
			var t = (p - Origin).Dot(Direction);
			var proj = Origin + (t * Direction);
			return (proj - p).LengthSquared;
		}



		/// <summary>
		/// Returns:
		///   +1, on right of line
		///   -1, on left of line
		///    0, on the line
		/// </summary>
		public int WhichSide(Vector2d test, double tol = 0) {
			var x0 = test.x - Origin.x;
			var y0 = test.y - Origin.y;
			var x1 = Direction.x;
			var y1 = Direction.y;
			var det = (x0 * y1) - (x1 * y0);
			return det > tol ? +1 : (det < -tol ? -1 : 0);
		}
		public int WhichSide(ref Vector2d test, double tol = 0) {
			var x0 = test.x - Origin.x;
			var y0 = test.y - Origin.y;
			var x1 = Direction.x;
			var y1 = Direction.y;
			var det = (x0 * y1) - (x1 * y0);
			return det > tol ? +1 : (det < -tol ? -1 : 0);
		}



		/// <summary>
		/// Calculate intersection point between this line and another one.
		/// Returns Vector2d.MaxValue if lines are parallel.
		/// </summary>
		/// <returns></returns>
		public Vector2d IntersectionPoint(ref Line2d other, double dotThresh = MathUtil.ZERO_TOLERANCE) {
			// see IntrLine2Line2 for explanation of algorithm
			var diff = other.Origin - Origin;
			var D0DotPerpD1 = Direction.DotPerp(other.Direction);
			if (Math.Abs(D0DotPerpD1) > dotThresh) {                    // Lines intersect in a single point.
				var invD0DotPerpD1 = ((double)1) / D0DotPerpD1;
				var diffDotPerpD1 = diff.DotPerp(other.Direction);
				var s = diffDotPerpD1 * invD0DotPerpD1;
				return Origin + (s * Direction);
			}
			// Lines are parallel.
			return Vector2d.MaxValue;
		}





		// conversion operators
		public static implicit operator Line2d(Line2f v) => new(v.Origin, v.Direction);
		public static explicit operator Line2f(Line2d v) => new((Vector2f)v.Origin, (Vector2f)v.Direction);


	}

	[MessagePackObject]
	public struct Line2f
	{
		[Key(0)]
		public Vector2f Origin;
		[Key(1)]
		public Vector2f Direction;

		public Line2f(Vector2f origin, Vector2f direction) {
			Origin = origin;
			Direction = direction;
		}

		// parameter is distance along Line
		public Vector2f PointAt(float d) {
			return Origin + (d * Direction);
		}

		public float Project(Vector2f p) {
			return (p - Origin).Dot(Direction);
		}

		public float DistanceSquared(Vector2f p) {
			var t = (p - Origin).Dot(Direction);
			var proj = Origin + (t * Direction);
			return (proj - p).LengthSquared;
		}
	}
}
