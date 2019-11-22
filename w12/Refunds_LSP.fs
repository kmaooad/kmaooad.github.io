module Refunds

type BillingRecord(amount: decimal) =
    member __.Amount = amount

type Payment(amount: decimal) =
    inherit BillingRecord(amount)

type Refund(amount: decimal) =
    inherit BillingRecord(amount)

// Calculate totals by type (refunds, payments, invoice charges etc)

type TotalsCalculator =
    
    abstract Calculate: BillingRecord list -> (string * decimal) list
    
    default __.Calculate(items: BillingRecord list) =
        items
        |> Seq.groupBy (fun i -> i.GetType().Name)
        |> Seq.map (fun (key, items) -> (key, (items |> Seq.sumBy (fun i -> i.Amount))))
        |> List.ofSeq

type TrickyTotalsCalculator =
    inherit TotalsCalculator
    
    override __.Calculate(items: BillingRecord list) =
        let adjustedItems =
            items
            |> List.map (fun i ->
                if i.Amount < 0m then Refund(-i.Amount) :> BillingRecord
                else i)

        base.Calculate(adjustedItems)
