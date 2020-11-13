// BIF5 FUS - Final Project - PartTwo
// by Leo Gruber (if18b113)
// PartOne extended
#if INTERACTIVE
#r "../packages/FParsec/lib/net40-client/FParsecCS.dll"
#r "../packages/FParsec/lib/net40-client/FParsec.dll"
#else
namespace Turtle
#endif

[<AutoOpen>]
module PartTwo =
    open System

    type Value = float
    type Variable = string

    type BinaryOp = Add | Minus | Mul
    type Comparison = Less | Greater | Equal

    type Cmd =
        | Forward of Variable //move forward by distance specified in variable
        | Left    of Variable //turn left by angle specified in variable
        | Right   of Variable //turn right by angle specified in variable
        | Assign  of Variable * Variable * BinaryOp * Variable
        | Declare of Variable * Value
        | While   of Variable * Comparison * Variable * list<Cmd>  

    type Program = list<Cmd>

    type Vec2 = float * float
    type Angle = float

    type TurtleState = 
        {
            variables : Map<Variable,Value>
            direction : Angle     // direction in degrees
            position  : Vec2       // current position
            trail     : list<Vec2> // points produced so far
            food      : float
        }
    

    module Logics = 
        let computeForwardMove (s : TurtleState) (f : Value) =
            let d = s.direction
            let p = s.position
            let x = fst p
            let y = snd p
            let rad = d * (Math.PI / float 180)
            let pNew = (x + ((sin rad) * f), y + ((cos rad) * f))
            let trailNew = pNew :: s.trail
            {s with position = pNew; trail = trailNew}

        let computeLeftMove (s : TurtleState) (f : Value) =
            let dNew = (s.direction - f) % float 360
            {s with direction = dNew}

        let computeRightMove (s : TurtleState) (f : Value) =
            let dNew = (s.direction + f) % float 360
            {s with direction = dNew}

    
    [<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
    module TurtleState =

        let lookup (s : TurtleState) (varName : string) : Value =
            match Map.tryFind varName s.variables with
                | Some v-> v
                | _ -> failwithf "undeclared variable: %s" varName

        let setVariable (s : TurtleState) (varName : string) (value : float) : TurtleState =
            { s with variables = Map.add varName value s.variables }

        let empty intialPos supplies  =
            {
                variables = Map.empty
                position = intialPos
                trail = [intialPos]
                direction = 0.0
                food = supplies
            }

    let interpretOp (o : BinaryOp) =
        match o with
            | Add -> (+)
            | Minus -> (-)
            | Mul -> (*)

    let interpretCmp (o : Comparison) (arg : float) (comparand : float) =
        match o with
            | Less -> arg < comparand 
            | Greater -> arg > comparand
            | Equal -> arg = comparand

    let rec interpretCmd (state : TurtleState) (c : Cmd) : TurtleState =
        match c with
        | Forward v -> 
            Logics.computeForwardMove state (TurtleState.lookup state v)
        | Left v -> 
            Logics.computeLeftMove state (TurtleState.lookup state v)
        | Right v -> 
            Logics.computeRightMove state (TurtleState.lookup state v)
        | Assign (key,v1,op,v2) -> 
            let val1 = TurtleState.lookup state v1
            let val2 = TurtleState.lookup state v2
            let res = interpretOp op val1 val2
            TurtleState.setVariable state key res
        | Declare (key,v) -> 
            TurtleState.setVariable state key v
        | While (v1,cmp,v2,cmds) -> 
            let val1 = TurtleState.lookup state v1
            let val2 = TurtleState.lookup state v2
            if (interpretCmp cmp val1 val2) then
                let s = interpret state cmds
                interpretCmd s c
            else state

    and interpret (state : TurtleState) (commands : list<Cmd>) =
        List.fold interpretCmd state commands 


    module Examples =

        let spiral =
            let state = TurtleState.empty (0.0,0.0) 0.0 
            let program = [
                Declare("delta",3.0)
                Declare("turn",90.0)
                Declare("lineLen",100.0)
                Declare("minimum",0.0)

                While("lineLen",Comparison.Greater,"minimum", [
                        Forward "lineLen"
                        Right "turn"
                        Assign("lineLen","lineLen",Minus,"delta")
                    ]
                )
            ]
            program, state

        let star =
            let state = TurtleState.empty (50.0,60.0) 0.0 
            let program = [
                Declare("count",0.0)
                Declare("starAngle",144.0)
                Declare("one",1.0)
                Declare("scale",2.0)
                Declare("iterations",100.0)
                While("count",Comparison.Less,"iterations", [
                        Assign("dist","count",Mul,"scale")
                        Forward "dist"
                        Right "starAngle"
                        Assign("count","count",Add,"one")
                    ]
                )
            ]
            program,state

    let runTurtleProgram (initialState : TurtleState) (p : Program) : list<Vec2> =
        let resultState = interpret initialState p
        resultState.trail |> List.rev