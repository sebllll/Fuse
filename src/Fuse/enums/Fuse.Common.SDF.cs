﻿using System;

namespace Fuse.Common.SDF
{
    public enum CombineSDF
    {
        Union,
        Intersect,
        Difference,
        UnionRound,
        IntersectionRound,
        DifferenceRound,
        UnionChamfer,
        IntersectionChamfer,
        DifferenceChamfer,
        UnionColumns,
        IntersectionColumns,
        DifferenceColumns,
        UnionStairs,
        IntersectionStairs,
        DifferenceStairs,
        Pipe,
        Engrave,
        Groove,
        Tongue
    }
    public enum ExtrudeSDF
    { Extrude, ExtrudeChamfer, ExtrudeRound }

    public enum GridSDF
    { Box, Triangle, Hexagon }
}
