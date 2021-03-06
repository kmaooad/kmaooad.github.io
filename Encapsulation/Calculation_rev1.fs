module OrdersV1

open System

module Domain =

    type OrderItem =
        { Product: int
          Price: decimal
          Qty: int
          TaxRate: decimal }

    type Order =
        { Total: decimal
          Customer: int
          Items: OrderItem list }

    let calculate customer (items: OrderItem list) =

        let total () =
            items
            |> List.sumBy (fun i ->
                let amount = i.Price * (i.Qty |> decimal)
                let tax = amount * i.TaxRate
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
        let taxedItems =
            items
            |> List.map (fun i ->
                let ptype = productType i.Product
                let zip = customerZip customer
                let rate = taxRate ptype zip
                let price = productPrice i.Product
                { Product = i.Product
                  Qty = i.Qty
                  TaxRate = rate
                  Price = price })

        calculate customer taxedItems
