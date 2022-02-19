module FSharpHandlers.SampleHandler

let getGreeting (getGreeting: string -> Async<string>) (getName: int -> Async<string>) (language: string) (clientId: int) : Async<string> =
    async {
        let! greeting = language |> getGreeting
        let! name = clientId |> getName
        return $"{greeting} {name}"
    }
