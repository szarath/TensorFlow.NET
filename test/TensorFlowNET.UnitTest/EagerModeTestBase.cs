﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Tensorflow.Binding;

namespace TensorFlowNET.UnitTest
{
    public class EagerModeTestBase : PythonTest
    {
        [TestInitialize]
        public void TestInit()
        {
            if (!tf.executing_eagerly())
                tf.enable_eager_execution();
        }

        [TestCleanup]
        public void TestClean()
        {
        }
    }
}
