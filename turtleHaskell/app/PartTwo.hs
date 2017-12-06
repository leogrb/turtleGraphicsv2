{-# LANGUAGE FlexibleContexts #-}
module PartTwo where

import Text.Parsec

type Value = Float
data Cmd = Forward Value | Left Value | Right Value -- | add imperative constructors

-- completify the domain model...

type Program = [Cmd]

type Vec2 = (Float, Float)

runTurtleProgram :: Float -> (Float,Float) -> Program -> [Vec2]
runTurtleProgram supply startPos program = 
    --error "todo implement"
    [(0.0, 0.0), (6.123031769e-15, 100.0), (98.0, 100.0), (98.0, 4.0), (4.0, 4.0),
    (4.0, 96.0), (94.0, 96.0), (94.0, 8.0), (8.0, 8.0), (8.0, 92.0), (90.0, 92.0),
    (90.0, 12.0), (12.0, 12.0), (12.0, 88.0), (86.0, 88.0), (86.0, 16.0),
    (16.0, 16.0), (16.0, 84.0), (82.0, 84.0), (82.0, 20.0), (20.0, 20.0),
    (20.0, 80.0), (78.0, 80.0), (78.0, 24.0), (24.0, 24.0), (24.0, 76.0),
    (74.0, 76.0), (74.0, 28.0), (28.0, 28.0), (28.0, 72.0), (70.0, 72.0),
    (70.0, 32.0), (32.0, 32.0), (32.0, 68.0), (66.0, 68.0), (66.0, 36.0),
    (36.0, 36.0), (36.0, 64.0), (62.0, 64.0), (62.0, 40.0), (40.0, 40.0),
    (40.0, 60.0), (58.0, 60.0), (58.0, 44.0), (44.0, 44.0), (44.0, 56.0),
    (54.0, 56.0), (54.0, 48.0), (48.0, 48.0), (48.0, 52.0), (50.0, 52.0),
    (50.0, 52.0)]

stringParser:: Parsec String st String
stringParser = many anyChar

parseAb :: Parsec String st (Char,Char)
parseAb = 
    do 
        a <- char 'a'
        b <- char 'b'
        return (a,b)

simpleParse p = parse p ""

test :: IO ()
test = do
    print $ simpleParse stringParser "asdf"