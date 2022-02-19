using Microsoft.FSharp.Control;
using Microsoft.FSharp.Core;

namespace CSharpWebApi.Handlers;

public class SampleHandler
{
    private readonly FSharpFunc<string, FSharpAsync<string>> _getGreeting;
    private readonly FSharpFunc<int, FSharpAsync<string>> _getClientName;

    public SampleHandler(FSharpFunc<string, FSharpAsync<string>> getGreeting, FSharpFunc<int, FSharpAsync<string>> getClientName)
    {
        _getGreeting = getGreeting;
        _getClientName = getClientName;
    }

    public async Task<string> GetGreeting(string language, int id)
    {
        var result = FSharpHandlers.SampleHandler.getGreeting(_getGreeting, _getClientName, language, id);
        return await FSharpAsync.StartAsTask(result, FSharpOption<TaskCreationOptions>.None, FSharpOption<CancellationToken>.None);
    }
}