namespace Grean.Champagne.FSharp

open Grean.Champagne

module Dom =
    let Replace item equalityComparer source =
        Sequence.Replace(source, item, (fun x -> equalityComparer x))