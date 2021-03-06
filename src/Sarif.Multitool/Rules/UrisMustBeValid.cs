﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Json.Pointer;

namespace Microsoft.CodeAnalysis.Sarif.Multitool.Rules
{
    public class UrisMustBeValid : SarifValidationSkimmerBase
    {
        private readonly Message _fullDescription = new Message
        {
            Text = RuleResources.SARIF1003_UrisMustBeValid
        };

        public override Message FullDescription => _fullDescription;

        public override ResultLevel DefaultLevel => ResultLevel.Error;

        /// <summary>
        /// SARIF1003
        /// </summary>
        public override string Id => RuleId.UrisMustBeValid;

        protected override IEnumerable<string> MessageResourceNames => new string[]
        {
            nameof(RuleResources.SARIF1003_Default)
        };

        protected override void Analyze(SarifLog log, string logPointer)
        {
            AnalyzeUri(log.SchemaUri, logPointer.AtProperty(SarifPropertyName.Schema));
        }

        protected override void Analyze(FileData fileData, string fileKey, string filePointer)
        {
            string fileUriReference = RemoveUriBaseIdPrefix(fileKey);
            AnalyzeUri(fileUriReference, filePointer);
        }

        private const string UriWithPrefixPattern =
@"^                     # Start of string, followed by
(?<prefix>              # a prefix consisting of
    \#                  #    a pound sign (escaped because otherwise it would signify a comment),
    [^\#]+              #    one or more occurrences of anything that isn't a pound sign,
    \#                  #    and another pound sign,
)
(?<uri>                 # followed by the URI, which is
    .+                  #     everything
)
$                       # up to the end of the string (not strictly needed since the match is greedy).
";

        private static readonly Regex s_uriWithPrefixRegex =
            new Regex(UriWithPrefixPattern, RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);

        private string RemoveUriBaseIdPrefix(string fileKey)
        {
            Match match = s_uriWithPrefixRegex.Match(fileKey);
            return match.Success
                ? match.Groups["uri"].Value
                : fileKey;
        }

        protected override void Analyze(FileLocation fileLocation, string fileLocationPointer)
        {
            AnalyzeUri(fileLocation.Uri, fileLocationPointer.AtProperty(SarifPropertyName.Uri));
        }

        protected override void Analyze(Result result, string resultPointer)
        {
            if (result.WorkItemUris != null)
            {
                Uri[] workItemUris = result.WorkItemUris.ToArray();
                string workItemUrisPointer = resultPointer.AtProperty(SarifPropertyName.WorkItemUris);

                for (int i = 0; i < workItemUris.Length; ++i)
                {
                    AnalyzeUri(workItemUris[i], workItemUrisPointer.AtIndex(i));
                }
            }
        }

        protected override void Analyze(IRule rule, string ruleKey, string rulePointer)
        {
            AnalyzeUri(rule.HelpUri, rulePointer.AtProperty(SarifPropertyName.HelpUri));
        }

        protected override void Analyze(Run run, string runPointer)
        {
            if (run.OriginalUriBaseIds != null)
            {
                string originalUriBaseIdsPointer = runPointer.AtProperty(SarifPropertyName.OriginalUriBaseIds);

                foreach (string key in run.OriginalUriBaseIds.Keys)
                {
                    AnalyzeUri(run.OriginalUriBaseIds[key], originalUriBaseIdsPointer.AtProperty(key));
                }
            }
        }

        protected override void Analyze(Tool tool, string toolPointer)
        {
            AnalyzeUri(tool.DownloadUri, toolPointer.AtProperty(SarifPropertyName.DownloadUri));
        }

        protected override void Analyze(VersionControlDetails versionControlDetails, string versionControlDetailsPointer)
        {
            AnalyzeUri(versionControlDetails.Uri, versionControlDetailsPointer.AtProperty(SarifPropertyName.Uri));
        }

        private void AnalyzeUri(Uri uri, string pointer)
        {
            AnalyzeUri(uri?.OriginalString, pointer);
        }

        private void AnalyzeUri(string uri, string pointer)
        {
            if (uri != null)
            {
                if (!Uri.IsWellFormedUriString(uri, UriKind.RelativeOrAbsolute))
                {
                    LogResult(pointer, nameof(RuleResources.SARIF1003_Default), uri);
                }
            }
        }
    }
}
