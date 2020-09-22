module Refunds1

open System
open FSharp.Data
open FSharp.Json

module DB =
    let insert<'T> (table: string) (entity: 'T): unit = failwith "not implemented"
    let select<'T> (table: string): 'T list = failwith "not implemented"

module Billing =

    type Payment =
        { customer_id: int
          date: DateTime
          amount: decimal }

    [<Literal>]
    let Url = "https://fancybilling.kmaooadtech.io"

    let download (customer: int) (from: DateTime) (till: DateTime) =

        let request =
            [ ("customer", customer |> string)
              ("from", from |> string)
              ("till", till |> string) ]

        let response =
            Http.RequestString(Url, httpMethod = "POST", body = FormValues request)

        let payments =
            response |> Json.deserialize<Payment list>

        payments |> List.iter (DB.insert "payments")

/// ------
/// Client
/// ------

module Client =
    open Billing

    let customer = 404

    download customer (DateTime(2019, 10, 1)) (DateTime(2019, 10, 31))
