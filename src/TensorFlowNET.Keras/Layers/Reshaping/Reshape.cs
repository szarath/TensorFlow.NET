﻿using Tensorflow.Keras.ArgsDefinition;
using Tensorflow.Keras.Engine;
using static Tensorflow.Binding;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Tensorflow.Keras.Layers
{
    /// <summary>
    /// Layer that reshapes inputs into the given shape.
    /// </summary>
    public class Reshape : Layer
    {
        ReshapeArgs args;
        public Reshape(ReshapeArgs args)
            : base(args)
        {
            this.args = args;
        }

        protected override Tensors Call(Tensors inputs, Tensor state = null, bool is_training = false)
        {
            var shape = new List<int> { inputs.shape[0] };
            shape.AddRange(args.TargetShape.dims);

            var result = array_ops.reshape(inputs, shape.ToArray());
            if (!tf.Context.executing_eagerly())
                result.set_shape(compute_output_shape(inputs.shape));
            return result;
        }

        TensorShape compute_output_shape(TensorShape input_shape)
        {
            if (input_shape.dims[0] == -1)
            {
                input_shape = input_shape.dims[0];
                var output_shape = input_shape.concatenate(args.TargetShape.dims);
                return output_shape;
            }
            else
                throw new NotImplementedException("");
        }
    }
}
