module Refunds

open System
open FSharp.Data
open FSharp.Json

let ``Not implemented`` a = failwith "not implemented"

module DB =
    let insert<'T> (table: string) (entity: 'T): unit = failwith "no impl"
    let select<'T> (table:string) : 'T list = failwith "no impl"

module Domain =
    type Refund = {
        amount : decimal
        customer : int
        date : DateTime
    }
 
module Billing =

    type BillingPeriod = {
        from: DateTime
        till: DateTime
    }

    type Payment =
        { customer_id: int
          date: DateTime
          amount: decimal }

    [<Literal>]
    let Url = "https://fancybilling.kmaooadtech.io"

    let private adjustTimeZone (d:DateTime) = d.AddHours(-5.0)

    let billingPeriod (from: DateTime) (till: DateTime) : (BillingPeriod) = 
        { from = from |> adjustTimeZone
          till = till |> adjustTimeZone }
    
    let requestPayments (customer: int) (p: BillingPeriod) = 
        let request =
            [ ("customer_id", customer |> string)
              ("from", p.from |> string)
              ("to", p.till |> string) ]
        Http.RequestString(Url, httpMethod = "POST", body = FormValues request)
        |> Json.deserialize<Payment list>

    let toRefund p = { 
        Domain.Refund.amount = p.amount
        Domain.Refund.customer = p.customer_id
        Domain.Refund.date = p.date
    }

    let savePayments pp = 
        pp 
        |> List.filter (fun p -> p.amount < 0m)  
        |> List.iter (toRefund >> DB.insert "refunds")
    
    let download (customer: int) (from: DateTime) (till: DateTime) =
        (customer, billingPeriod from till)
        ||> requestPayments
        |> savePayments

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
               |> List.filter (fun bp -> bp.amount < 0m) // refund filter out 
               |> List.map (fun bp -> {
                                         Customer = bp.customer_id
                                         TxnDate = bp.date.AddHours(6.0) // track all refunds after 6PM to next day
                                         Amount = -(bp.amount)
                                      })
        let request = Json.serialize accountingPayments
        
        Http.RequestString(Url, httpMethod = "POST", body = TextRequest request)
