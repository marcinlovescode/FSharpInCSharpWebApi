namespace FSharpHandlers.Dependencies

open System.Threading.Tasks

module Greetings =
    let getGreeting (language: string) =
        async {
            do! Async.AwaitTask(Task.Delay(100))

            return
                match language.ToUpper() with
                | "POLISH" -> "Cześć"
                | "ENGLISH" -> "Hello"
                | _ -> failwith "Not supported language"
        }

module Clients =
    let getClientName id =
        async {
            do! Async.AwaitTask(Task.Delay(150))

            return
                match id with
                | 1 -> "Admin"
                | 2 -> "Marcin"
                | id -> failwith $"User of id {id} doesn't exist"
        }
