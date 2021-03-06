// Copyright (c) Microsoft.  All Rights Reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.Sarif.Readers;

namespace Microsoft.CodeAnalysis.Sarif
{
    /// <summary>
    /// Defines methods to support the comparison of objects of type Run for equality.
    /// </summary>
    [GeneratedCode("Microsoft.Json.Schema.ToDotNet", "0.58.0.0")]
    internal sealed class RunEqualityComparer : IEqualityComparer<Run>
    {
        internal static readonly RunEqualityComparer Instance = new RunEqualityComparer();

        public bool Equals(Run left, Run right)
        {
            if (ReferenceEquals(left, right))
            {
                return true;
            }

            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
            {
                return false;
            }

            if (!Tool.ValueComparer.Equals(left.Tool, right.Tool))
            {
                return false;
            }

            if (!object.ReferenceEquals(left.Invocations, right.Invocations))
            {
                if (left.Invocations == null || right.Invocations == null)
                {
                    return false;
                }

                if (left.Invocations.Count != right.Invocations.Count)
                {
                    return false;
                }

                for (int index_0 = 0; index_0 < left.Invocations.Count; ++index_0)
                {
                    if (!Invocation.ValueComparer.Equals(left.Invocations[index_0], right.Invocations[index_0]))
                    {
                        return false;
                    }
                }
            }

            if (!Conversion.ValueComparer.Equals(left.Conversion, right.Conversion))
            {
                return false;
            }

            if (!object.ReferenceEquals(left.VersionControlProvenance, right.VersionControlProvenance))
            {
                if (left.VersionControlProvenance == null || right.VersionControlProvenance == null)
                {
                    return false;
                }

                if (left.VersionControlProvenance.Count != right.VersionControlProvenance.Count)
                {
                    return false;
                }

                for (int index_1 = 0; index_1 < left.VersionControlProvenance.Count; ++index_1)
                {
                    if (!VersionControlDetails.ValueComparer.Equals(left.VersionControlProvenance[index_1], right.VersionControlProvenance[index_1]))
                    {
                        return false;
                    }
                }
            }

            if (!object.ReferenceEquals(left.OriginalUriBaseIds, right.OriginalUriBaseIds))
            {
                if (left.OriginalUriBaseIds == null || right.OriginalUriBaseIds == null || left.OriginalUriBaseIds.Count != right.OriginalUriBaseIds.Count)
                {
                    return false;
                }

                foreach (var value_0 in left.OriginalUriBaseIds)
                {
                    Uri value_1;
                    if (!right.OriginalUriBaseIds.TryGetValue(value_0.Key, out value_1))
                    {
                        return false;
                    }

                    if (value_0.Value != value_1)
                    {
                        return false;
                    }
                }
            }

            if (!object.ReferenceEquals(left.Files, right.Files))
            {
                if (left.Files == null || right.Files == null || left.Files.Count != right.Files.Count)
                {
                    return false;
                }

                foreach (var value_2 in left.Files)
                {
                    FileData value_3;
                    if (!right.Files.TryGetValue(value_2.Key, out value_3))
                    {
                        return false;
                    }

                    if (!FileData.ValueComparer.Equals(value_2.Value, value_3))
                    {
                        return false;
                    }
                }
            }

            if (!object.ReferenceEquals(left.LogicalLocations, right.LogicalLocations))
            {
                if (left.LogicalLocations == null || right.LogicalLocations == null || left.LogicalLocations.Count != right.LogicalLocations.Count)
                {
                    return false;
                }

                foreach (var value_4 in left.LogicalLocations)
                {
                    LogicalLocation value_5;
                    if (!right.LogicalLocations.TryGetValue(value_4.Key, out value_5))
                    {
                        return false;
                    }

                    if (!LogicalLocation.ValueComparer.Equals(value_4.Value, value_5))
                    {
                        return false;
                    }
                }
            }

            if (!object.ReferenceEquals(left.Graphs, right.Graphs))
            {
                if (left.Graphs == null || right.Graphs == null || left.Graphs.Count != right.Graphs.Count)
                {
                    return false;
                }

                foreach (var value_6 in left.Graphs)
                {
                    Graph value_7;
                    if (!right.Graphs.TryGetValue(value_6.Key, out value_7))
                    {
                        return false;
                    }

                    if (!Graph.ValueComparer.Equals(value_6.Value, value_7))
                    {
                        return false;
                    }
                }
            }

            if (!object.ReferenceEquals(left.Results, right.Results))
            {
                if (left.Results == null || right.Results == null)
                {
                    return false;
                }

                if (left.Results.Count != right.Results.Count)
                {
                    return false;
                }

                for (int index_2 = 0; index_2 < left.Results.Count; ++index_2)
                {
                    if (!Result.ValueComparer.Equals(left.Results[index_2], right.Results[index_2]))
                    {
                        return false;
                    }
                }
            }

            if (!Resources.ValueComparer.Equals(left.Resources, right.Resources))
            {
                return false;
            }

            if (left.InstanceGuid != right.InstanceGuid)
            {
                return false;
            }

            if (left.CorrelationGuid != right.CorrelationGuid)
            {
                return false;
            }

            if (left.LogicalId != right.LogicalId)
            {
                return false;
            }

            if (!Message.ValueComparer.Equals(left.Description, right.Description))
            {
                return false;
            }

            if (left.AutomationLogicalId != right.AutomationLogicalId)
            {
                return false;
            }

            if (left.BaselineInstanceGuid != right.BaselineInstanceGuid)
            {
                return false;
            }

            if (left.Architecture != right.Architecture)
            {
                return false;
            }

            if (left.RichMessageMimeType != right.RichMessageMimeType)
            {
                return false;
            }

            if (left.RedactionToken != right.RedactionToken)
            {
                return false;
            }

            if (left.DefaultFileEncoding != right.DefaultFileEncoding)
            {
                return false;
            }

            if (left.ColumnKind != right.ColumnKind)
            {
                return false;
            }

            if (!object.ReferenceEquals(left.Properties, right.Properties))
            {
                if (left.Properties == null || right.Properties == null || left.Properties.Count != right.Properties.Count)
                {
                    return false;
                }

                foreach (var value_8 in left.Properties)
                {
                    SerializedPropertyInfo value_9;
                    if (!right.Properties.TryGetValue(value_8.Key, out value_9))
                    {
                        return false;
                    }

                    if (!object.Equals(value_8.Value, value_9))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public int GetHashCode(Run obj)
        {
            if (ReferenceEquals(obj, null))
            {
                return 0;
            }

            int result = 17;
            unchecked
            {
                if (obj.Tool != null)
                {
                    result = (result * 31) + obj.Tool.ValueGetHashCode();
                }

                if (obj.Invocations != null)
                {
                    foreach (var value_10 in obj.Invocations)
                    {
                        result = result * 31;
                        if (value_10 != null)
                        {
                            result = (result * 31) + value_10.ValueGetHashCode();
                        }
                    }
                }

                if (obj.Conversion != null)
                {
                    result = (result * 31) + obj.Conversion.ValueGetHashCode();
                }

                if (obj.VersionControlProvenance != null)
                {
                    foreach (var value_11 in obj.VersionControlProvenance)
                    {
                        result = result * 31;
                        if (value_11 != null)
                        {
                            result = (result * 31) + value_11.ValueGetHashCode();
                        }
                    }
                }

                if (obj.OriginalUriBaseIds != null)
                {
                    // Use xor for dictionaries to be order-independent.
                    int xor_0 = 0;
                    foreach (var value_12 in obj.OriginalUriBaseIds)
                    {
                        xor_0 ^= value_12.Key.GetHashCode();
                        if (value_12.Value != null)
                        {
                            xor_0 ^= value_12.Value.GetHashCode();
                        }
                    }

                    result = (result * 31) + xor_0;
                }

                if (obj.Files != null)
                {
                    // Use xor for dictionaries to be order-independent.
                    int xor_1 = 0;
                    foreach (var value_13 in obj.Files)
                    {
                        xor_1 ^= value_13.Key.GetHashCode();
                        if (value_13.Value != null)
                        {
                            xor_1 ^= value_13.Value.GetHashCode();
                        }
                    }

                    result = (result * 31) + xor_1;
                }

                if (obj.LogicalLocations != null)
                {
                    // Use xor for dictionaries to be order-independent.
                    int xor_2 = 0;
                    foreach (var value_14 in obj.LogicalLocations)
                    {
                        xor_2 ^= value_14.Key.GetHashCode();
                        if (value_14.Value != null)
                        {
                            xor_2 ^= value_14.Value.GetHashCode();
                        }
                    }

                    result = (result * 31) + xor_2;
                }

                if (obj.Graphs != null)
                {
                    // Use xor for dictionaries to be order-independent.
                    int xor_3 = 0;
                    foreach (var value_15 in obj.Graphs)
                    {
                        xor_3 ^= value_15.Key.GetHashCode();
                        if (value_15.Value != null)
                        {
                            xor_3 ^= value_15.Value.GetHashCode();
                        }
                    }

                    result = (result * 31) + xor_3;
                }

                if (obj.Results != null)
                {
                    foreach (var value_16 in obj.Results)
                    {
                        result = result * 31;
                        if (value_16 != null)
                        {
                            result = (result * 31) + value_16.ValueGetHashCode();
                        }
                    }
                }

                if (obj.Resources != null)
                {
                    result = (result * 31) + obj.Resources.ValueGetHashCode();
                }

                if (obj.InstanceGuid != null)
                {
                    result = (result * 31) + obj.InstanceGuid.GetHashCode();
                }

                if (obj.CorrelationGuid != null)
                {
                    result = (result * 31) + obj.CorrelationGuid.GetHashCode();
                }

                if (obj.LogicalId != null)
                {
                    result = (result * 31) + obj.LogicalId.GetHashCode();
                }

                if (obj.Description != null)
                {
                    result = (result * 31) + obj.Description.ValueGetHashCode();
                }

                if (obj.AutomationLogicalId != null)
                {
                    result = (result * 31) + obj.AutomationLogicalId.GetHashCode();
                }

                if (obj.BaselineInstanceGuid != null)
                {
                    result = (result * 31) + obj.BaselineInstanceGuid.GetHashCode();
                }

                if (obj.Architecture != null)
                {
                    result = (result * 31) + obj.Architecture.GetHashCode();
                }

                if (obj.RichMessageMimeType != null)
                {
                    result = (result * 31) + obj.RichMessageMimeType.GetHashCode();
                }

                if (obj.RedactionToken != null)
                {
                    result = (result * 31) + obj.RedactionToken.GetHashCode();
                }

                if (obj.DefaultFileEncoding != null)
                {
                    result = (result * 31) + obj.DefaultFileEncoding.GetHashCode();
                }

                result = (result * 31) + obj.ColumnKind.GetHashCode();
                if (obj.Properties != null)
                {
                    // Use xor for dictionaries to be order-independent.
                    int xor_4 = 0;
                    foreach (var value_17 in obj.Properties)
                    {
                        xor_4 ^= value_17.Key.GetHashCode();
                        if (value_17.Value != null)
                        {
                            xor_4 ^= value_17.Value.GetHashCode();
                        }
                    }

                    result = (result * 31) + xor_4;
                }
            }

            return result;
        }
    }
}