module Payments

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

        payments |> List.iter (DB.insert "payments")

module Accounting =

    type Payment = {
        Customer: int
        TxnDate: DateTime
        Amount: decimal
    }

    [<Literal>]
    let Url = "https://fancyacct.kmaooadtech.io"

    let upload (customer:int) = 
        let billingPayments = DB.select<Billing.Payment> "payments"
        
        let accountingPayments = billingPayments 
                                        |> List.map (fun bp -> {
                                                                    Customer = bp.customer_id
                                                                    TxnDate = bp.date
                                                                    Amount = bp.amount
                                                                })
        let request = Json.serialize accountingPayments
        
        Http.RequestString(Url, httpMethod = "POST", body = TextRequest request)
