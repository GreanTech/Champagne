namespace Grean.Champagne.FSharp.UnitTests

open System
open Grean.Champagne
open Grean.Champagne.FSharp
open FsCheck
open FsCheck.Prop
open FsCheck.Xunit
open Xunit.Extensions

type Generators =
    static member Version() =
        Arb.generate<byte>
        |> Gen.map int
        |> Gen.four
        |> Gen.map (fun (ma, mi, bu, re) -> Version(ma, mi, bu, re))
        |> Arb.fromGen

[<AbstractClass>]
type ReplaceTests<'T when 'T : equality>() =
    
    do Arb.register<Generators>() |> ignore

    [<Property>]
    member this.FSharpReplaceWithFunctionIsSameAsCSharpReplaceWithFunc (s : 'T list) v t =
        let expected = s.Replace(v, (fun x -> x = t)) |> Seq.toList
        let actual = s |> Dom.Replace v (fun x -> x = t) |> Seq.toList
        actual = expected

    [<Property>]
    member this.ReplaceReturnsSourceWhenFunctionIsAlwaysFalse (s : 'T list) v =
        let expected = s |> Seq.toList
        let actual = s |> Dom.Replace v (fun _ -> false) |> Seq.toList
        actual = expected

    [<Property>]
    member this.ReplaceReturnCorrectResultBasedOnFunction
        (s : 'T list)
        (oldValue : 'T)
        newValue =

        let comparer = oldValue.Equals
        let expected =
            s
            |> List.map (fun x -> if comparer x then newValue else x)
        let actual = s |> Dom.Replace newValue comparer |> Seq.toList
        actual = expected

    [<Property(MaxTest = 10)>]
    member this.ReplaceWithNullSourceThrows (v : 'T) =        
        throws<ArgumentNullException, _>
            (lazy (Dom.Replace v (fun _ -> true) null |> Seq.toList))

    [<Property>]
    member this.FSharpReplaceWithComparerIsSameAsCSharpReplaceWithEquatable
        (s : 'T list)
        (v : 'T)
        (t : 'T) =

        let equatable = 
            { new IEquatable<'T> with
                member this.Equals x = x = t }
        
        let actual = s |> Dom.ReplaceE v equatable |> Seq.toList

        let expected = s.Replace(v, equatable) |> Seq.toList
        actual = expected

    [<Property>]
    member this.ReplaceReturnsSourceWhenEquatableIsAlwaysFalse (s : 'T list) v =
        let equatable = 
            { new IEquatable<'T> with
                member this.Equals x = false }

        let actual = s |> Dom.ReplaceE v equatable |> Seq.toList
        
        let expected = s |> Seq.toList
        actual = expected

    [<Property>]
    member this.ReplaceReturnsCorrectResultBasedOnEquatable
        (s : 'T list)
        (oldValue : 'T)
        newValue =

        let equatable = 
            { new IEquatable<'T> with
                member this.Equals x = oldValue.Equals x }
        let expected =
            s
            |> List.map (fun x -> if equatable.Equals x then newValue else x)
        let actual = s |> Dom.ReplaceE newValue equatable |> Seq.toList
        actual = expected

    [<Property(MaxTest = 10)>]
    member this.ReplaceEWithNullSourceThrows (v : 'T) =
        let equatable = 
            { new IEquatable<'T> with
                member this.Equals x = false }
        throws<ArgumentNullException, _>
            (lazy (Dom.ReplaceE v equatable null |> Seq.toList))

type ReplaceTestsOfInt()     = inherit ReplaceTests<int>()
type ReplaceTestsOfString()  = inherit ReplaceTests<string>()
type ReplaceTestsOfGuid()    = inherit ReplaceTests<Guid>()
type ReplaceTestsOfVersion() = inherit ReplaceTests<Version>()