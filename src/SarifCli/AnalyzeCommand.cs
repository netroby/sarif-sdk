﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis.Sarif.Driver;
using Microsoft.CodeAnalysis.Sarif.Readers;
using Microsoft.Json.Schema;
using Microsoft.Json.Schema.Validation;
using Newtonsoft.Json;

namespace Microsoft.CodeAnalysis.Sarif.Cli
{
    internal class AnalyzeCommand : AnalyzeCommandBase<SarifValidationContext, AnalyzeOptions>
    {
        public override string Prerelease => VersionConstants.Prerelease;

        private List<Assembly> _defaultPlugInAssemblies;

        public override IEnumerable<Assembly> DefaultPlugInAssemblies
        {
            get
            {
                if (_defaultPlugInAssemblies == null)
                {
                    _defaultPlugInAssemblies = new List<Assembly>
                    {
                        this.GetType().Assembly
                    };
                }

                return _defaultPlugInAssemblies;
            }
        }

        protected override SarifValidationContext CreateContext(AnalyzeOptions options, IAnalysisLogger logger, RuntimeConditions runtimeErrors, string filePath = null)
        {
            SarifValidationContext context = base.CreateContext(options, logger, runtimeErrors, filePath);
            context.SchemaFilePath = options.SchemaFilePath;
            return context;
        }

        protected override void AnalyzeTarget(
            IEnumerable<ISkimmer<SarifValidationContext>> skimmers,
            SarifValidationContext context,
            HashSet<string> disabledSkimmers)
        {
            // The base class knows how to invoke the skimmers that implement smart validation,
            // but it doesn't know how to invoke schema validation, which has its own set of rules,
            // so we do that ourselves.
            //
            // Validate will return false if there are any JSON syntax errors. In that case
            // there's no point in going on.
            bool ok = Validate(context.TargetUri.LocalPath, context.SchemaFilePath, context.Logger);

            if (ok)
            {
                // Deserialize will return null if there are any JSON deserialization errors
                // (which can happen, for example, if a property required by the schema is
                // missing. In that case, again, there's no point in going on.
                context.InputLogContents = File.ReadAllText(context.TargetUri.AbsolutePath);
                context.InputLog = Deserialize(context.InputLogContents);

                if (context.InputLog != null)
                {
                    // Everything's ready, so run all the skimmers.
                    base.AnalyzeTarget(skimmers, context, disabledSkimmers);
                }
            }
        }

        private SarifLog Deserialize(string logContents)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ContractResolver = SarifContractResolver.Instance
            };

            SarifLog log = null;
            try
            {
                return JsonConvert.DeserializeObject<SarifLog>(logContents, settings);
            }
            catch (JsonSerializationException)
            {
                // This exception can happen, for example, if a property required by the schema is
                // missing.
            }

            return log;
        }

        private bool Validate(string instanceFilePath, string schemaFilePath, IAnalysisLogger logger)
        {
            bool ok = true;

            try
            {
                string instanceText = File.ReadAllText(instanceFilePath);
                PerformSchemaValidation(instanceText, instanceFilePath, schemaFilePath, logger);
            }
            catch (JsonSyntaxException ex)
            {
                ReportResult(ex.Result, logger);

                // If the file isn't syntactically valid JSON, we won't be able to run
                // the skimmers, because they rely on being able to deserialized the file
                // into a SarifLog object.
                ok = false;
            }
            catch (SchemaValidationException ex)
            {
                ReportInvalidSchemaErrors(ex, schemaFilePath, logger);
            }
            // The framework will catch all other, unexpected exceptions, and it will
            // cause the tool to exit with a non-0 exit code.

            return ok;
        }

        private void PerformSchemaValidation(
            string instanceText,
            string instanceFilePath,
            string schemaFilePath,
            IAnalysisLogger logger)
        {
            string schemaText = File.ReadAllText(schemaFilePath);
            JsonSchema schema = SchemaReader.ReadSchema(schemaText, schemaFilePath);

            var validator = new Validator(schema);
            Result[] results = validator.Validate(instanceText, instanceFilePath);

            ReportResults(results, logger);
        }

        private static void ReportInvalidSchemaErrors(
            SchemaValidationException ex,
            string schemaFile,
            IAnalysisLogger logger)
        {
            foreach (Result result in ex.Results)
            {
                result.SetAnalysisTargetUri(schemaFile);
                ReportResult(result, logger);
            }
        }

        private static void ReportResults(
            Result[] results,
            IAnalysisLogger logger)
        {
            foreach (Result result in results)
            {
                ReportResult(result, logger);
            }
        }

        private static void ReportResult(
            Result result,
            IAnalysisLogger logger)
        {
            Rule rule = Json.Schema.Sarif.RuleFactory.GetRuleFromRuleId(result.RuleId);
            logger.Log(rule, result);
        }
    }
}
