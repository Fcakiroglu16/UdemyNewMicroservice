#region

using Refit;
using System.Text.Json;
using System.Text.Json.Serialization;
using ProblemDetails = Microsoft.AspNetCore.Mvc.ProblemDetails;

#endregion

namespace UdemyNewMicroservice.Web.Services;

public class ServiceResult
{
    public ProblemDetails? Fail { get; set; }

    [JsonIgnore] public bool IsSuccess => Fail is null;
    [JsonIgnore] public bool IsFail => !IsSuccess;


    // Static factory methods
    public static ServiceResult Success()
    {
        return new ServiceResult();
    }


    public static ServiceResult Error(ProblemDetails problemDetails)
    {
        return new ServiceResult
        {
            Fail = problemDetails
        };
    }

    public static ServiceResult FailFromProblemDetails(ApiException exception)
    {
        if (string.IsNullOrEmpty(exception.Content))
            return new ServiceResult
            {
                Fail = new ProblemDetails
                {
                    Title = exception.Message
                }
            };


        return new ServiceResult
        {
            Fail = JsonSerializer.Deserialize<ProblemDetails>(exception.Content, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            })
        };
    }

    public static ServiceResult Error(string title, string description)
    {
        return new ServiceResult
        {
            Fail = new ProblemDetails
            {
                Title = title,
                Detail = description
            }
        };
    }

    public static ServiceResult Error(string title)
    {
        return new ServiceResult
        {
            Fail = new ProblemDetails
            {
                Title = title
            }
        };
    }
}

public class ServiceResult<T> : ServiceResult
{
    public T? Data { get; set; }


    public static ServiceResult<T> Success(T data)
    {
        return new ServiceResult<T>
        {
            Data = data
        };
    }


    public new static ServiceResult<T> FailFromProblemDetails(ApiException exception)
    {
        if (string.IsNullOrEmpty(exception.Content))
            return new ServiceResult<T>
            {
                Fail = new ProblemDetails
                {
                    Title = exception.Message
                }
            };


        return new ServiceResult<T>
        {
            Fail = JsonSerializer.Deserialize<ProblemDetails>(exception.Content, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            })
        };
    }


    public new static ServiceResult<T> Error(ProblemDetails problemDetails)
    {
        return new ServiceResult<T>
        {
            Fail = problemDetails
        };
    }

    public new static ServiceResult<T> Error(string title, string description)
    {
        return new ServiceResult<T>
        {
            Fail = new ProblemDetails
            {
                Title = title,
                Detail = description
            }
        };
    }

    public new static ServiceResult<T> Error(string title)
    {
        return new ServiceResult<T>
        {
            Fail = new ProblemDetails
            {
                Title = title
            }
        };
    }
}