// Copyright (c) Microsoft.  All Rights Reserved.
// Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.CodeAnalysis.Sarif
{
    /// <summary>
    /// A result produced by an analysis tool.
    /// </summary>
    [DataContract]
    [GeneratedCode("Microsoft.Json.Schema.ToDotNet", "0.16.0.0")]
    public partial class Result : ISarifNode, IEquatable<Result>
    {
        /// <summary>
        /// Gets a value indicating the type of object implementing <see cref="ISarifNode" />.
        /// </summary>
        public SarifNodeKind SarifNodeKind
        {
            get
            {
                return SarifNodeKind.Result;
            }
        }

        /// <summary>
        /// A stable, opaque identifier for the rule that was evaluated to produce the result.
        /// </summary>
        [DataMember(Name = "ruleId", IsRequired = false, EmitDefaultValue = false)]
        public string RuleId { get; set; }

        /// <summary>
        /// The kind of observation this result represents. If this property is not present, its implied value is 'warning'.
        /// </summary>
        [DataMember(Name = "kind", IsRequired = false, EmitDefaultValue = false)]
        public ResultKind Kind { get; set; }

        /// <summary>
        /// A string that describes the result. The first sentence of the message only will be displayed when visible space is limited.
        /// </summary>
        [DataMember(Name = "message", IsRequired = false, EmitDefaultValue = false)]
        public string Message { get; set; }

        /// <summary>
        /// A 'formattedRuleMessage' object that can be used to construct a formatted message that describes the result. If the 'formattedMessage' property is present on a result, the 'fullMessage' property shall not be present. If the 'fullMessage' property is present on an result, the 'formattedMessage' property shall not be present
        /// </summary>
        [DataMember(Name = "formattedRuleMessage", IsRequired = false, EmitDefaultValue = false)]
        public FormattedRuleMessage FormattedRuleMessage { get; set; }

        /// <summary>
        /// One or more locations where the result occurred. Specify only one location unless the problem indicated by the result can only be corrected by making a change at every specified location.
        /// </summary>
        [DataMember(Name = "locations", IsRequired = false, EmitDefaultValue = false)]
        public ISet<Location> Locations { get; set; }

        /// <summary>
        /// A source code fragment that illustrates the result.
        /// </summary>
        [DataMember(Name = "codeSnippet", IsRequired = false, EmitDefaultValue = false)]
        public string CodeSnippet { get; set; }

        /// <summary>
        /// A string that contributes to the unique identity of the result.
        /// </summary>
        [DataMember(Name = "toolFingerprint", IsRequired = false, EmitDefaultValue = false)]
        public string ToolFingerprint { get; set; }

        /// <summary>
        /// An array of 'stack' objects relevant to the result.
        /// </summary>
        [DataMember(Name = "stacks", IsRequired = false, EmitDefaultValue = false)]
        public ISet<Stack> Stacks { get; set; }

        /// <summary>
        /// An array of 'codeFlow' objects relevant to the result.
        /// </summary>
        [DataMember(Name = "codeFlows", IsRequired = false, EmitDefaultValue = false)]
        public ISet<CodeFlow> CodeFlows { get; set; }

        /// <summary>
        /// A grouped set of locations and messages, if available, that represent code areas that are related to this result.
        /// </summary>
        [DataMember(Name = "relatedLocations", IsRequired = false, EmitDefaultValue = false)]
        public ISet<AnnotatedCodeLocation> RelatedLocations { get; set; }

        /// <summary>
        /// A flag indicating whether or not this result was suppressed in source code (or 'unknown' is that information isn't known).
        /// </summary>
        [DataMember(Name = "isSuppressedInSource", IsRequired = false, EmitDefaultValue = false)]
        public IsSuppressedInSource IsSuppressedInSource { get; set; }

        /// <summary>
        /// An array of 'fix' objects, each of which represents a proposed fix to the problem indicated by the result.
        /// </summary>
        [DataMember(Name = "fixes", IsRequired = false, EmitDefaultValue = false)]
        public ISet<Fix> Fixes { get; set; }

        /// <summary>
        /// Key/value pairs that provide additional information about the result.
        /// </summary>
        [DataMember(Name = "properties", IsRequired = false, EmitDefaultValue = false)]
        public IDictionary<string, string> Properties { get; set; }

        /// <summary>
        /// A set of distinct strings that provide additional information about the result.
        /// </summary>
        [DataMember(Name = "tags", IsRequired = false, EmitDefaultValue = false)]
        public ISet<string> Tags { get; set; }

        public override bool Equals(object other)
        {
            return Equals(other as Result);
        }

        public override int GetHashCode()
        {
            int result = 17;
            unchecked
            {
                if (RuleId != null)
                {
                    result = (result * 31) + RuleId.GetHashCode();
                }

                result = (result * 31) + Kind.GetHashCode();
                if (Message != null)
                {
                    result = (result * 31) + Message.GetHashCode();
                }

                if (FormattedRuleMessage != null)
                {
                    result = (result * 31) + FormattedRuleMessage.GetHashCode();
                }

                if (Locations != null)
                {
                    foreach (var value_0 in Locations)
                    {
                        result = result * 31;
                        if (value_0 != null)
                        {
                            result = (result * 31) + value_0.GetHashCode();
                        }
                    }
                }

                if (CodeSnippet != null)
                {
                    result = (result * 31) + CodeSnippet.GetHashCode();
                }

                if (ToolFingerprint != null)
                {
                    result = (result * 31) + ToolFingerprint.GetHashCode();
                }

                if (Stacks != null)
                {
                    foreach (var value_1 in Stacks)
                    {
                        result = result * 31;
                        if (value_1 != null)
                        {
                            result = (result * 31) + value_1.GetHashCode();
                        }
                    }
                }

                if (CodeFlows != null)
                {
                    foreach (var value_2 in CodeFlows)
                    {
                        result = result * 31;
                        if (value_2 != null)
                        {
                            result = (result * 31) + value_2.GetHashCode();
                        }
                    }
                }

                if (RelatedLocations != null)
                {
                    foreach (var value_3 in RelatedLocations)
                    {
                        result = result * 31;
                        if (value_3 != null)
                        {
                            result = (result * 31) + value_3.GetHashCode();
                        }
                    }
                }

                result = (result * 31) + IsSuppressedInSource.GetHashCode();
                if (Fixes != null)
                {
                    foreach (var value_4 in Fixes)
                    {
                        result = result * 31;
                        if (value_4 != null)
                        {
                            result = (result * 31) + value_4.GetHashCode();
                        }
                    }
                }

                if (Properties != null)
                {
                    // Use xor for dictionaries to be order-independent.
                    int xor_0 = 0;
                    foreach (var value_5 in Properties)
                    {
                        xor_0 ^= value_5.Key.GetHashCode();
                        if (value_5.Value != null)
                        {
                            xor_0 ^= value_5.Value.GetHashCode();
                        }
                    }

                    result = (result * 31) + xor_0;
                }

                if (Tags != null)
                {
                    foreach (var value_6 in Tags)
                    {
                        result = result * 31;
                        if (value_6 != null)
                        {
                            result = (result * 31) + value_6.GetHashCode();
                        }
                    }
                }
            }

            return result;
        }

        public bool Equals(Result other)
        {
            if (other == null)
            {
                return false;
            }

            if (RuleId != other.RuleId)
            {
                return false;
            }

            if (Kind != other.Kind)
            {
                return false;
            }

            if (Message != other.Message)
            {
                return false;
            }

            if (!Object.Equals(FormattedRuleMessage, other.FormattedRuleMessage))
            {
                return false;
            }

            if (!Object.ReferenceEquals(Locations, other.Locations))
            {
                if (Locations == null || other.Locations == null)
                {
                    return false;
                }

                if (!Locations.SetEquals(other.Locations))
                {
                    return false;
                }
            }

            if (CodeSnippet != other.CodeSnippet)
            {
                return false;
            }

            if (ToolFingerprint != other.ToolFingerprint)
            {
                return false;
            }

            if (!Object.ReferenceEquals(Stacks, other.Stacks))
            {
                if (Stacks == null || other.Stacks == null)
                {
                    return false;
                }

                if (!Stacks.SetEquals(other.Stacks))
                {
                    return false;
                }
            }

            if (!Object.ReferenceEquals(CodeFlows, other.CodeFlows))
            {
                if (CodeFlows == null || other.CodeFlows == null)
                {
                    return false;
                }

                if (!CodeFlows.SetEquals(other.CodeFlows))
                {
                    return false;
                }
            }

            if (!Object.ReferenceEquals(RelatedLocations, other.RelatedLocations))
            {
                if (RelatedLocations == null || other.RelatedLocations == null)
                {
                    return false;
                }

                if (!RelatedLocations.SetEquals(other.RelatedLocations))
                {
                    return false;
                }
            }

            if (IsSuppressedInSource != other.IsSuppressedInSource)
            {
                return false;
            }

            if (!Object.ReferenceEquals(Fixes, other.Fixes))
            {
                if (Fixes == null || other.Fixes == null)
                {
                    return false;
                }

                if (!Fixes.SetEquals(other.Fixes))
                {
                    return false;
                }
            }

            if (!Object.ReferenceEquals(Properties, other.Properties))
            {
                if (Properties == null || other.Properties == null || Properties.Count != other.Properties.Count)
                {
                    return false;
                }

                foreach (var value_0 in Properties)
                {
                    string value_1;
                    if (!other.Properties.TryGetValue(value_0.Key, out value_1))
                    {
                        return false;
                    }

                    if (value_0.Value != value_1)
                    {
                        return false;
                    }
                }
            }

            if (!Object.ReferenceEquals(Tags, other.Tags))
            {
                if (Tags == null || other.Tags == null)
                {
                    return false;
                }

                if (!Tags.SetEquals(other.Tags))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Result" /> class.
        /// </summary>
        public Result()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Result" /> class from the supplied values.
        /// </summary>
        /// <param name="ruleId">
        /// An initialization value for the <see cref="P: RuleId" /> property.
        /// </param>
        /// <param name="kind">
        /// An initialization value for the <see cref="P: Kind" /> property.
        /// </param>
        /// <param name="message">
        /// An initialization value for the <see cref="P: Message" /> property.
        /// </param>
        /// <param name="formattedRuleMessage">
        /// An initialization value for the <see cref="P: FormattedRuleMessage" /> property.
        /// </param>
        /// <param name="locations">
        /// An initialization value for the <see cref="P: Locations" /> property.
        /// </param>
        /// <param name="codeSnippet">
        /// An initialization value for the <see cref="P: CodeSnippet" /> property.
        /// </param>
        /// <param name="toolFingerprint">
        /// An initialization value for the <see cref="P: ToolFingerprint" /> property.
        /// </param>
        /// <param name="stacks">
        /// An initialization value for the <see cref="P: Stacks" /> property.
        /// </param>
        /// <param name="codeFlows">
        /// An initialization value for the <see cref="P: CodeFlows" /> property.
        /// </param>
        /// <param name="relatedLocations">
        /// An initialization value for the <see cref="P: RelatedLocations" /> property.
        /// </param>
        /// <param name="isSuppressedInSource">
        /// An initialization value for the <see cref="P: IsSuppressedInSource" /> property.
        /// </param>
        /// <param name="fixes">
        /// An initialization value for the <see cref="P: Fixes" /> property.
        /// </param>
        /// <param name="properties">
        /// An initialization value for the <see cref="P: Properties" /> property.
        /// </param>
        /// <param name="tags">
        /// An initialization value for the <see cref="P: Tags" /> property.
        /// </param>
        public Result(string ruleId, ResultKind kind, string message, FormattedRuleMessage formattedRuleMessage, ISet<Location> locations, string codeSnippet, string toolFingerprint, ISet<Stack> stacks, ISet<CodeFlow> codeFlows, ISet<AnnotatedCodeLocation> relatedLocations, IsSuppressedInSource isSuppressedInSource, ISet<Fix> fixes, IDictionary<string, string> properties, ISet<string> tags)
        {
            Init(ruleId, kind, message, formattedRuleMessage, locations, codeSnippet, toolFingerprint, stacks, codeFlows, relatedLocations, isSuppressedInSource, fixes, properties, tags);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Result" /> class from the specified instance.
        /// </summary>
        /// <param name="other">
        /// The instance from which the new instance is to be initialized.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="other" /> is null.
        /// </exception>
        public Result(Result other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            Init(other.RuleId, other.Kind, other.Message, other.FormattedRuleMessage, other.Locations, other.CodeSnippet, other.ToolFingerprint, other.Stacks, other.CodeFlows, other.RelatedLocations, other.IsSuppressedInSource, other.Fixes, other.Properties, other.Tags);
        }

        ISarifNode ISarifNode.DeepClone()
        {
            return DeepCloneCore();
        }

        /// <summary>
        /// Creates a deep copy of this instance.
        /// </summary>
        public Result DeepClone()
        {
            return (Result)DeepCloneCore();
        }

        private ISarifNode DeepCloneCore()
        {
            return new Result(this);
        }

        private void Init(string ruleId, ResultKind kind, string message, FormattedRuleMessage formattedRuleMessage, ISet<Location> locations, string codeSnippet, string toolFingerprint, ISet<Stack> stacks, ISet<CodeFlow> codeFlows, ISet<AnnotatedCodeLocation> relatedLocations, IsSuppressedInSource isSuppressedInSource, ISet<Fix> fixes, IDictionary<string, string> properties, ISet<string> tags)
        {
            RuleId = ruleId;
            Kind = kind;
            Message = message;
            if (formattedRuleMessage != null)
            {
                FormattedRuleMessage = new FormattedRuleMessage(formattedRuleMessage);
            }

            if (locations != null)
            {
                var destination_0 = new HashSet<Location>();
                foreach (var value_0 in locations)
                {
                    if (value_0 == null)
                    {
                        destination_0.Add(null);
                    }
                    else
                    {
                        destination_0.Add(new Location(value_0));
                    }
                }

                Locations = destination_0;
            }

            CodeSnippet = codeSnippet;
            ToolFingerprint = toolFingerprint;
            if (stacks != null)
            {
                var destination_1 = new HashSet<Stack>();
                foreach (var value_1 in stacks)
                {
                    if (value_1 == null)
                    {
                        destination_1.Add(null);
                    }
                    else
                    {
                        destination_1.Add(new Stack(value_1));
                    }
                }

                Stacks = destination_1;
            }

            if (codeFlows != null)
            {
                var destination_2 = new HashSet<CodeFlow>();
                foreach (var value_2 in codeFlows)
                {
                    if (value_2 == null)
                    {
                        destination_2.Add(null);
                    }
                    else
                    {
                        destination_2.Add(new CodeFlow(value_2));
                    }
                }

                CodeFlows = destination_2;
            }

            if (relatedLocations != null)
            {
                var destination_3 = new HashSet<AnnotatedCodeLocation>();
                foreach (var value_3 in relatedLocations)
                {
                    if (value_3 == null)
                    {
                        destination_3.Add(null);
                    }
                    else
                    {
                        destination_3.Add(new AnnotatedCodeLocation(value_3));
                    }
                }

                RelatedLocations = destination_3;
            }

            IsSuppressedInSource = isSuppressedInSource;
            if (fixes != null)
            {
                var destination_4 = new HashSet<Fix>();
                foreach (var value_4 in fixes)
                {
                    if (value_4 == null)
                    {
                        destination_4.Add(null);
                    }
                    else
                    {
                        destination_4.Add(new Fix(value_4));
                    }
                }

                Fixes = destination_4;
            }

            if (properties != null)
            {
                Properties = new Dictionary<string, string>(properties);
            }

            if (tags != null)
            {
                var destination_5 = new HashSet<string>();
                foreach (var value_5 in tags)
                {
                    destination_5.Add(value_5);
                }

                Tags = destination_5;
            }
        }
    }
}