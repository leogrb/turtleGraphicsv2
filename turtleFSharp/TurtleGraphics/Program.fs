open System

open System.Windows.Forms
open OpenGL
open Turtle.WS17

let contextCreated (args : GlControlEventArgs) =
    Gl.MatrixMode(MatrixMode.Projection)
    Gl.LoadIdentity()
    Gl.Ortho(0.0, 100.0, 0.0, 100.0, 0.0, 100.0)
    Gl.MatrixMode(MatrixMode.Modelview)
    Gl.LoadIdentity()

let render (control : Control) (args : GlControlEventArgs) = 
    Gl.Viewport(0, 0, control.ClientSize.Width, control.ClientSize.Height);
    Gl.Clear(ClearBufferMask.ColorBufferBit);
    

    Gl.Begin(PrimitiveType.LineStrip);

    let exampleProgram,desiredStart = PartOne.Examples.quad
    let points = PartOne.runTurtleProgram desiredStart exampleProgram

    Gl.Color3(1.0f, 1.0f, 1.0f)
    for (x,y) in points do
        Gl.Vertex2(x,y);

    Gl.End();

[<EntryPoint;STAThread>]
let main argv = 
    use form = new Form()
    form.Text <- "Functional Languages WS17, Turtle Graphics Project"
    form.Size <- Drawing.Size(800,600)

    use glControl = new OpenGL.GlControl()
    glControl.Animation <- true
    glControl.ContextCreated.Add contextCreated
    glControl.Render.Add (render glControl)
    glControl.BackColor <- System.Drawing.Color.DimGray
    glControl.ColorBits <- 32u
    glControl.DepthBits <- 24u
    glControl.Dock <- DockStyle.Fill
    
    form.Controls.Add glControl
    Application.Run form

    0 
