module FSharpWebApi.HttpHandlers

open Microsoft.AspNetCore.Http
open Giraffe
open Giraffe.EndpointRouting

let greetingsHandler compositionRoot : HttpHandler =
    fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
            let! x = compositionRoot.ReadGreetings "Polish" 1
            return! Successful.OK x next ctx
        }


let endpoints compositionRoot =
    [
        GET [
            route  "/Giraffe" (greetingsHandler compositionRoot)
        ]
    ]