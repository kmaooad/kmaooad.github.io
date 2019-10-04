module Refunds

open System
open FSharp.Data
open FSharp.Json

module DB =
    let insert<'T> (table: string) (entity: 'T): unit = failwith "no impl"
    let select<'T> (table:string) : 'T list = failwith "no impl"

module Billing =

    type Payment =
        { customer_id: int
          date: DateTime
          amount: decimal }

    [<Literal>]
    let Url = "https://fancybilling.kmaooadtech.io"

    let download (customer: int) (from: DateTime) (till: DateTime) =

        let request =
            [ ("customer_id", customer |> string)
              ("from", from |> string)
              ("to", till |> string) ]

        let response = Http.RequestString(Url, httpMethod = "POST", body = FormValues request)

        let payments = response |> Json.deserialize<Payment list>

        payments
        |> List.iter (DB.insert "payments")

module Accounting =

    type Refund = {
        Customer: int
        TxnDate: DateTime
        Amount: decimal
    }

    [<Literal>]
    let Url = "https://fancyacct.kmaooadtech.io"

    let upload (customer:int) = 
        let billingPayments = DB.select<Billing.Payment> "payments"
        
        let accountingPayments = 
               billingPayments 
               |> List.filter (fun bp -> bp.amount < 0) // refund filter out 
               |> List.map (fun bp -> {
                                         Customer = bp.customer_id
                                         TxnDate = bp.date.AddHours(6.0) // track all refunds after 6PM to next day
                                         Amount = -(bp.amount)
                                      })
        let request = Json.serialize accountingPayments
        
        Http.RequestString(Url, httpMethod = "POST", body = TextRequest request)
