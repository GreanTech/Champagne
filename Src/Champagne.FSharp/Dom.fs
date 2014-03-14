namespace Grean.Champagne.FSharp

open System
open Grean.Champagne

module Dom =
    let Replace item equalityComparer source =
        Sequence.Replace(source, item, (fun x -> equalityComparer x))

    let ReplaceE<'T> item (equalityComparer : IEquatable<'T>) source =
        Sequence.Replace(source, item, equalityComparer)