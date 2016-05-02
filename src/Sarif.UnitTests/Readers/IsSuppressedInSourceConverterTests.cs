﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using Microsoft.CodeAnalysis.Sarif.Writers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json;

namespace Microsoft.CodeAnalysis.Sarif.Readers
{
    [TestClass]
    public class InSourceSuppressionConverterTests
    {
        private static readonly Run s_defaultRun = new Run();
        private static readonly Tool s_defaultTool = new Tool();

        public InSourceSuppressionConverterTests()
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = SarifContractResolver.Instance
            };
        }

        private static string GetJson(Action<ResultLogJsonWriter> testContent)
        {
            StringBuilder result = new StringBuilder();
            using (var str = new StringWriter(result))
            using (var json = new JsonTextWriter(str))
            using (var uut = new ResultLogJsonWriter(json))
            {
                testContent(uut);
            }

            return result.ToString();
        }

        [TestMethod]
        public void SuppressionStatus_SuppressedInSource()
        {
            string expected = "{\"version\":\"1.0.0-beta.4\",\"runs\":[{\"tool\":{\"name\":null},\"results\":[{\"suppressionStates\":[\"suppressedInSource\"]}]}]}";
            string actual = GetJson(uut =>
            {
                var run = new Run();

                uut.WriteTool(s_defaultTool);

                uut.WriteResults(new[] { new Result
                    {
                        SuppressionStates = SuppressionStates.SuppressedInSource
                    }
                });
            });
            Assert.AreEqual(expected, actual);

            var settings = new JsonSerializerSettings()
            {
                ContractResolver = new SarifContractResolver()
            };

            var sarifLog = JsonConvert.DeserializeObject<SarifLog>(actual, settings);
            Assert.AreEqual(SuppressionStates.SuppressedInSource, sarifLog.Runs[0].Results[0].SuppressionStates);
        }

        [TestMethod]
        public void SuppressionStatus_SuppressedInSourceAndBaseline()
        {
            string expected = "{\"version\":\"1.0.0-beta.4\",\"runs\":[{\"tool\":{\"name\":null},\"results\":[{\"suppressionStates\":[\"suppressedInSource\",\"suppressedInBaseline\"]}]}]}";
            string actual = GetJson(uut =>
            {
                var run = new Run();

                uut.WriteTool(s_defaultTool);

                uut.WriteResults(new[] { new Result
                    {
                        SuppressionStates = SuppressionStates.SuppressedInSource | SuppressionStates.SuppressedInBaseline
                    }
                });
            });
            Assert.AreEqual(expected, actual);

            var sarifLog = JsonConvert.DeserializeObject<SarifLog>(actual);
            Assert.AreEqual(
                SuppressionStates.SuppressedInSource | SuppressionStates.SuppressedInBaseline, 
                sarifLog.Runs[0].Results[0].SuppressionStates);
        }

        [TestMethod]
        public void BaselineState_None()
        {
            string expected = "{\"version\":\"1.0.0-beta.4\",\"runs\":[{\"tool\":{\"name\":null},\"results\":[{}]}]}";
            string actual = GetJson(uut =>
            {
                var run = new Run();

                uut.WriteTool(s_defaultTool);

                uut.WriteResults(new[] { new Result
                    {
                        BaselineState = Sarif.BaselineState.None
                    }
                });
            });
            Assert.AreEqual(expected, actual);
        }
    }
}
