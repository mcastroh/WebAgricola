﻿namespace Logistic.Shared.DTO.Responses;

public class ActionResponse<T>
{
    public bool IsSuccess { get; set; }

    public string? Message { get; set; }

    public T? Result { get; set; }
}
