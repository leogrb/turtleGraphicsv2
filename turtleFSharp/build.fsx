open System.Runtime.InteropServices
// include Fake libs
#r "packages/build/Fake/tools/FakeLib.dll"

open Fake

Target "Build" (fun _ -> 
    build id "TurtleGraphics.sln"
)

Target "Run" (fun _ -> 
    tracefn "exec: %d" (Shell.Exec "TurtleGraphics/bin/Debug/TurtleGraphics.exe")
)

RunTargetOrDefault "Build"