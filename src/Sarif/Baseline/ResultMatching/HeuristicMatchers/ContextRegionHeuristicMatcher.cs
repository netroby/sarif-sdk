﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.CodeAnalysis.Sarif.Baseline.ResultMatching.HeuristicMatchers
{
    internal class ContextRegionHeuristicMatcher : HeuristicMatcher
    {
        public ContextRegionHeuristicMatcher() : base(ContextRegionResultComparer.Instance) { }
        
        public class ContextRegionResultComparer : IResultMatchingComparer
        {
            public static readonly ContextRegionResultComparer Instance = new ContextRegionResultComparer();

            public bool Equals(ExtractedResult x, ExtractedResult y)
            {
                IEnumerable<FileContent> xContextRegions = x.Result.Locations.Select(loc => loc.PhysicalLocation.ContextRegion.Snippet);

                HashSet<FileContent> yContextRegions = new HashSet<FileContent>(FileContentEqualityComparer.Instance);
                foreach (FileContent content in y.Result.Locations.Select(loc => loc.PhysicalLocation.ContextRegion.Snippet))
                {
                    yContextRegions.Add(content);
                }

                if (xContextRegions.Count() != yContextRegions.Count())
                {
                    return false;
                }

                foreach (FileContent content in xContextRegions)
                {
                    if (!yContextRegions.Contains(content))
                    {
                        return false;
                    }
                }

                return true;
            }

            public int GetHashCode(ExtractedResult obj)
            {
                int hashCode = -187510987;
                IEnumerable<FileContent> contextRegions = obj.Result.Locations.Select(loc => loc.PhysicalLocation.ContextRegion.Snippet);
                foreach (FileContent content in contextRegions)
                {
                    hashCode ^= FileContentEqualityComparer.Instance.GetHashCode(content);
                }

                return hashCode;
            }

            public bool ResultMatcherApplies(ExtractedResult result)
            {
                bool? applies = result.Result.Locations?.Select(loc => loc.PhysicalLocation?.ContextRegion?.Snippet).Where(snippet => !string.IsNullOrEmpty(snippet?.Text)).Any();

                return applies.HasValue ? applies.Value : false;
            }
        }

    }
}
