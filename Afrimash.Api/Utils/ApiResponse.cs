namespace Afrimash.Api.Utils;

public sealed record ApiResponse<T>(string Message, int Code, T Data);
