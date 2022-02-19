namespace FSharpWebApi

type CompositionRoot =
    { ReadGreetings: string -> int -> Async<string> }

module CompositionRoot =
    let compose () : CompositionRoot =
        { ReadGreetings =
              FSharpHandlers.SampleHandler.getGreeting
                  FSharpHandlers.Dependencies.Greetings.getGreeting
                  FSharpHandlers.Dependencies.Clients.getClientName }
