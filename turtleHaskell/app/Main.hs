module Main where

-- see: https://wiki.haskell.org/OpenGLTutorial1

import Lib
import Graphics.UI.GLUT
import qualified PartTwo as PartTwo
import Data.Time

 
main :: IO ()
main = do
  (_progName, _args) <- getArgsAndInitialize
  _window <- createWindow "Functional Languages WS17, Turtle Graphics Project"
  startTime <- getCurrentTime
  displayCallback $= display startTime
  idleCallback $= Just (display startTime)
  reshapeCallback $= Just reshape
  mainLoop
 
reshape :: ReshapeCallback
reshape size = do
  viewport $= (Position 0 0, size)
  matrixMode $= Projection
  loadIdentity
  ortho2D 0.0 100.0 0.0 100.0
  matrixMode $= Modelview 0
  loadIdentity
  postRedisplay Nothing

display :: UTCTime -> DisplayCallback
display startTime = do 
  clear [ColorBuffer]
  -- dummy input so far
  currentTime <- getCurrentTime
  let timeDiff = diffUTCTime currentTime startTime
  print (realToFrac timeDiff)
  let points = PartTwo.runTurtleProgram (realToFrac  timeDiff) undefined undefined
  renderPrimitive LineStrip $
     mapM_ (\(x, y) -> vertex $ Vertex2 x y) points
  flush