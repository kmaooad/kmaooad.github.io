module Impl

open FSharp.Data
open FSharp.Json
open Billing
open System

module Config =
    [<Literal>]
    let BillingUrl = "https://fancybilling.kmaooadtech.io"

module DB =
    let insert<'T> (table: string) (entity: 'T): unit = failwith "not implemented"
    let select<'T> (table: string): 'T list = failwith "not implemented"


let httpClientImpl url formData =
    Http.RequestString(url, httpMethod = "POST", body = FormValues formData)
 
let retyingHttpClientImpl url formData httpClient = 
    try httpClient url formData with
    | _ -> "add retry logic here"

let requestFactoryImpl (customer: int) (p: BillingPeriod) =
    let body =
        [ ("customer", customer |> string)
          ("from", p.from |> string)
          ("till", p.till |> string) ]

    (Config.BillingUrl, body)

let responseHandlerImpl (response: string): Payment list =
    response |> Json.deserialize<Payment list>

let requestPaymentsImpl c p iRequestFactory iHttpClient iResponseHandler =
    (c, p)
    ||> iRequestFactory
    ||> iHttpClient
    |> iResponseHandler

let refundRepositoryImpl refunds =
    refunds |> List.iter (DB.insert "refunds")
