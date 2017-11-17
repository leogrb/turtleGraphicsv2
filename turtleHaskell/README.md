# How to build:

```stack build```

# How to run:

```stack exec turtle-exec```

# FAQ

Q: On Windows an error is printed "turtle-exe.EXE: user error (unknown GLUT entry glutInit)"
A: Download freeglut and place it inside her. also rename it to glut32.dll
source: https://stackoverflow.com/questions/42072958/haskell-with-opengl-unknown-glut-entry-glutinit

Q: What about Haskero support for vscode?
run stack build intero, source: https://gitlab.com/vannnns/haskero/blob/master/client/doc/installation.md