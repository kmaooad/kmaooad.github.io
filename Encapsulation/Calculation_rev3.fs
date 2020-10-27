module OrdersV3

open System

module Domain =

    type Product =
        { Id: int
          Type: string
          Price: decimal }

    type Customer = { Id: int; Zip: string }
    type ProductQty = { Product: Product; Qty: int }

    type OrderItem =
        { Product: Product
          Qty: int
          TaxRate: decimal }

    type Order =
        { Total: decimal
          Customer: Customer
          Items: OrderItem list }



    let calculate (cus: Customer) (products: ProductQty list) taxRateLookup =

        let items =
            products
            |> List.map (fun pq ->
                { Product = pq.Product
                  Qty = pq.Qty
                  TaxRate = taxRateLookup (pq.Product.Type) (cus.Zip) })

        let total =
            items
            |> List.sumBy (fun i ->
                let amount = i.Product.Price * (i.Qty |> decimal)
                let tax = amount * i.TaxRate
                amount + tax)

        { Total = total
          Customer = cus
          Items = items }

module Impl =
    let customerZip customerId = "02903"
    let productType productId = "Clothing"
    let productPrice productId = 99m
    let taxRateLookup productType zipCode = 0.075m

module Client =
    open Impl
    open Domain

    let makeOrder customer (items: ProductQty list) =
       
        calculate customer items taxRateLookup
