using System;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace UnityEditor.TestTools
{
    /// <summary>
    /// The `RequirePlatformSupportAttribute` attribute can be applied to test assemblies (affects every test in the assembly), fixtures (affects every test in the fixture), or to individual test methods. Tests under the scope of this attrbitue require Player build support for the specified platforms in order to run.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method)]
    public class RequirePlatformSupportAttribute : NUnitAttribute, IApplyToTest
    {
        /// <summary>
        /// Initializes and returns an instance of RequirePlatformSupportAttribute.
        /// </summary>
        /// <param name="platforms">The <see cref="BuildTarget"/> platform to run the test on.</param>
        public RequirePlatformSupportAttribute(params BuildTarget[] platforms)
        {
            this.platforms = platforms;
        }

        /// <summary>
        /// The build target platform, see [BuildTarget](https://docs.unity3d.com/ScriptReference/BuildTarget.html).
        /// </summary>
        public BuildTarget[] platforms { get; private set; }

        void IApplyToTest.ApplyToTest(Test test)
        {
            test.Properties.Add(PropertyNames.Category, string.Format("RequirePlatformSupport({0})", string.Join(", ", platforms.Select(p => p.ToString()).OrderBy(p => p).ToArray())));

            if (!platforms.All(p => BuildPipeline.IsBuildTargetSupported(BuildTargetGroup.Unknown, p)))
            {
                var missingPlatforms = platforms.Where(p => !BuildPipeline.IsBuildTargetSupported(BuildTargetGroup.Unknown, p)).Select(p => p.ToString()).ToArray();
                string skipReason = "Test cannot be run as it requires support for the following platforms to be installed: " + string.Join(", ", missingPlatforms);

                test.RunState = RunState.Skipped;
                test.Properties.Add(PropertyNames.SkipReason, skipReason);
            }
        }
    }
}
