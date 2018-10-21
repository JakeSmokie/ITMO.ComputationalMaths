module HumbleMathsFSharpy.Calculators.Interpolation

open HumbleMaths.Calculators.Interpolation
open System

type PairDispatcher = PairDispatcher with
    static member inline ($) (PairDispatcher,        (a, b)) = fun f -> f a b
    static member inline ($) (PairDispatcher, struct (a, b)) = fun f -> f a b
 
module Tuples =
    let inline fst x = (PairDispatcher $ x) (fun a _ -> a)
    let inline snd x = (PairDispatcher $ x) (fun _ b -> b)

type public AlternativeLagrangeInterpolator() =
    interface IFunctionInterpolator with
        member this.InterpolateByPoints(points) = 
            let calcMultipliers first x =
                points
                |> Seq.map (fun p -> Tuples.fst p)
                |> Seq.where (fun second -> second <> first)
                |> Seq.fold (fun acc second -> acc * (x - second) / (first - second)) 1.0

            Func<float,float> (fun (x : float) -> 
                points 
                |> Seq.map (fun p -> Tuples.snd p * calcMultipliers (Tuples.fst p) x)
                |> Seq.sum)