module OrdersV2

open System

module Domain =

    type OrderItem =
        { Product: int
          Price: decimal
          Qty: int }

    type TaxRate = { Product: int; Rate: decimal }

    type Order =
        { Total: decimal
          Customer: int
          Items: OrderItem list }

    let calculate customer (items: OrderItem list) (rates: TaxRate list) =
        let productRate p =
            let rate =
                rates |> List.find (fun r -> r.Product = p)

            rate.Rate

        let total () =
            items
            |> List.sumBy (fun i ->
                let amount = i.Price * (i.Qty |> decimal)
                let tax = amount * (productRate i.Product)
                amount + tax)

        { Total = total ()
          Customer = customer
          Items = items }

module Impl =
    let customerZip customerId = "02903"
    let productType productId = "Clothing"
    let productPrice productId = 99m
    let taxRate productType zipCode = 0.075m

module Client =
    open Impl
    open Domain

    type OrderItem = { Product: int; Qty: int }

    let makeOrder customer (items: OrderItem list) =
        let rates =
            items
            |> List.map (fun i ->
                let ptype = productType i.Product
                let zip = customerZip customer
                { Product = i.Product
                  Rate = taxRate i.Product zip })

        let items2 =
            items
            |> List.map (fun i ->
                let price = productPrice i.Product
                { Product = i.Product
                  Qty = i.Qty
                  Price = price })

        calculate customer items2 rates
