namespace Grean.Champagne.FSharp

open System
open Grean.Champagne

module Dom =
    let Replace replacementValue equalityComparer source =
        Sequence.Replace(source, replacementValue, (fun x -> equalityComparer x))

    let ReplaceE<'T> replacementValue (equalityComparer : IEquatable<'T>) source =
        Sequence.Replace(source, replacementValue, equalityComparer)