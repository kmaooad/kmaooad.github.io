# Final Quiz

## Q1

**What is the issue with this code:**

```fsharp
type TotalsCalculator =
    
    abstract Calculate: BillingRecord list -> (string * decimal) list
    
    default __.Calculate(items: BillingRecord list) =
        // calculate totals here

type BannedTotalsCalculator =
    inherit TotalsCalculator
    
    override __.Calculate(items: BillingRecord list) =
        if items |> anyBanned then failwith "Banned items detected"
        base.Calculate(adjustedItems)
```

a. LSP violation

b. DIP violation

c. Low cohesion

d. Loose coupling

e. Strong dependency

## Q2

**LSP helps to maintain proper ... :**

a. Inheritance and abstractions

b. Dependencies direction

c. Interface cohesion

d. DI

e. Rigidity

## Q3

**Uncontrolled access to _server_ state is caused by:**

a. ISP violation

b. Poor encapsulation

c. Loose coupling

d. Wrong DI container

e. Viscose client

## Q4

**Necessity of cascade changes in _clients_ after changes in _server_ is caused by:**

a. Polymorphism

b. OCP violation

c. Low cohesion

d. Poor abstraction

e. Opaque code

## Q5

**DIP helps to maintain ... :**

a. Good encapsulation

b. Tight coupling

c. Dependencies quality

d. Small and specific interfaces

e. High cohesion

## Q6

**Dependency of _client_ on methods it does not need is caused by:**

a. ISP violation

b. Poor encapsulation

c. Loose coupling

d. Design smell

## Q7

**OCP helps to ... :**

a. Have small interfaces

b. Maintain loose coupling

c. Extend behavior with new use cases without touching old ones

d. Maintain loose dependencies

e. Get more viscose design

## Q8

**Proper abstrations can be maintained with these principles (choose multiple):**

a. SRP

b. OCP

c. LSP

d. ISP

e. DIP

## Q9

**Different behavior depending on the concrete type with a common interface is called ... :**

a. Encapsulation

b. Polymorphism

c. Dependency

d. Rigidity

e. Pattern

## Q10

**Protection of internal state of an object with a limited access through a well-defined interface is called:**

a. Cohesion

b. Immobility

c. Opacity

d. DI

e. Encapsulation

## Q11

**What is the issue with this code:**

```csharp
public class Invoice {
    ...
    public decimal total;
    public void CalculateTotal() { ... }
    ...
}
```

a. DIP violation

b. LSP violation

c. Low cohesion

d. OCP violation

e. Bad encapsulation

## Q12

**Which dependency is preferred:**

```csharp
public class Customer { ... }
public class CustomerRepository { ... }
```

a. Customer -> CustomerRepository

b. CustomerRepository -> Customer

c. Both are good

d. None

## Q13

**What cohesion quality is preferred:**

a. High

b. Low

c. Both are nice

## Q14

**Provide example of _server_ in dependency**

## Q15

**Where is DI applied:**

a. 
```fsharp
let requestPaymentsImpl c p iRequestFactory iHttpClient iResponseHandler =
        (c, p)
        ||> iRequestFactory
        ||> iHttpClient
        |> iResponseHandler
```

b. 
```fsharp
let requestPaymentsImpl (customer: int) (p: BillingPeriod) =
        let request =
            [ ("customer_id", customer |> string)
              ("from", p.from |> prepare)
              ("to", p.till |> prepare) ]
        Http.RequestString(Url, httpMethod = "POST", body = FormValues request) |> Json.deserialize<Payment list>
```

c. Both

d. Neither

## Q16

**Where is DIP applied:**

a. 
```fsharp
let requestPaymentsImpl c p iRequestFactory iHttpClient iResponseHandler =
        (c, p)
        ||> iRequestFactory
        ||> iHttpClient
        |> iResponseHandler
```

b. 
```fsharp
let requestPaymentsImpl (customer: int) (p: BillingPeriod) =
        let request =
            [ ("customer_id", customer |> string)
              ("from", p.from |> prepare)
              ("to", p.till |> prepare) ]
        Http.RequestString(Url, httpMethod = "POST", body = FormValues request) |> Json.deserialize<Payment list>
```

c. Both

d. Neither

## Q17

**Where is LSP applied:**

a. 
```fsharp
let requestPaymentsImpl c p iRequestFactory iHttpClient iResponseHandler =
        (c, p)
        ||> iRequestFactory
        ||> iHttpClient
        |> iResponseHandler
```

b. 
```fsharp
let requestPaymentsImpl (customer: int) (p: BillingPeriod) =
        let request =
            [ ("customer_id", customer |> string)
              ("from", p.from |> prepare)
              ("to", p.till |> prepare) ]
        Http.RequestString(Url, httpMethod = "POST", body = FormValues request) |> Json.deserialize<Payment list>
```

c. Both

d. Neither

## Q18

**How many dependencies does `requestPaymentsImpl` have?**

```fsharp
let requestPaymentsImpl c p iRequestFactory iHttpClient iResponseHandler =
        (c, p)
        ||> iRequestFactory
        ||> iHttpClient
        |> iResponseHandler
```

## Q19

**What principle from SOLID corresponds to high cohesion?**

## Q20

**How much coupled is `requestPaymentsImpl`?**

```fsharp
let requestPaymentsImpl c p iRequestFactory iHttpClient iResponseHandler =
        (c, p)
        ||> iRequestFactory
        ||> iHttpClient
        |> iResponseHandler
```