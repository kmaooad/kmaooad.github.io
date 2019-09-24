module Rubik

open System

type Color =
    | Red
    | Orange
    | Blue
    | Yellow
    | White
    | Green

type Face = Face of Color

type Cell =
    { color: Color
      face: Face }

type Cubelet =
    | Double of Cell * Cell
    | Triple of Cell * Cell * Cell

type Cube =
    { cubelets: Cubelet list }

let rotateCellCw (f: Face) (c: Cell): Cell = failwith "not implemented"

let rotateCubeletCw (f: Face) (c: Cubelet) =
    match c with
    | Double(c1, c2) -> Double(rotateCellCw f c1, rotateCellCw f c2)
    | Triple(c1, c2, c3) -> Triple(rotateCellCw f c1, rotateCellCw f c2, rotateCellCw f c3)
