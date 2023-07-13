﻿namespace Tensorflow.Keras.Engine;

/// <summary>
/// A representation of a Keras in/output during Functional API construction.
/// </summary>
public class KerasTensor
{
    private Tensors _original_tensors;
    public Tensors original_tensors
    {
        get => _original_tensors;
        set => _original_tensors = value;
    }

    private Shape _inferred_value;
    public Shape inferred_value => _inferred_value;

    private string _name;
    private TensorSpec _type_spec;
    public Shape shape => _type_spec.shape;
    public TF_DataType dtype => _type_spec.dtype;

    public KerasTensor(TensorSpec type_spec, Shape inferred_value = null, string name = null)
    {
        _type_spec = type_spec;
        _inferred_value = inferred_value;
        _name = name;
    }

    public static KerasTensor from_tensor(Tensor tensor)
    {
        var type_spec = tensor.ToTensorSpec();
        var kt = new KerasTensor(type_spec, name: tensor.name);
        kt.original_tensors = tensor;
        return kt;
    }

    public override string ToString()
        => _original_tensors.Length switch
        {
            > 1 => "[" + string.Join(", ", _original_tensors.Select(x => $"KerasTensor: shape={x.shape} dtype={x.dtype}")) + "]",
            1 => $"KerasTensor: shape={_original_tensors.shape} {GetInferredValueString()} dtype={_original_tensors.dtype}",
            _ => _original_tensors.ToString(),
        };

    private string GetInferredValueString()
        => _inferred_value == null ? "" : "";

    public static implicit operator Tensors(KerasTensor kt)
        => kt._original_tensors;

    public static implicit operator Tensor(KerasTensor kt)
    { 
        Tensor tensor = kt._original_tensors;
        tensor.IsFromKerasTensor = true;
        return tensor;
    }

    public static implicit operator KerasTensor(Tensor tensor)
        => from_tensor(tensor);

    public static implicit operator KerasTensor(Tensors tensors)
        => from_tensor(tensors.First());
}
